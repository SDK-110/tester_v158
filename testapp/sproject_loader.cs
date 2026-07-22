using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace SprojFileBrowser
{
    // 包装 projectNames 列表和文档数量记录字段的类
    [Serializable]
    public class ProjectData
    {
        public List<string> ProjectNames { get; set; }
        public int DocumentCount { get; set; }

        public ProjectData()
        {
            ProjectNames = new List<string>();
            DocumentCount = 0;
        }
    }

    public sealed class ProjectLoader
    {
        private static readonly ProjectLoader instance = new ProjectLoader();
        private ProjectData projectData = new ProjectData();
        private readonly string xmlFilePath;
        private readonly string dllFilePath;
        private readonly string sprojFilePath;
        private readonly XmlSerializer serializer = new XmlSerializer(typeof(ProjectData));

        // 静态构造函数，确保线程安全
        static ProjectLoader() { }

        // 私有构造函数
        private ProjectLoader()
        {
            try
            {
                // 获取程序所在目录
                string appDirectory = Application.StartupPath;

                // 初始化文件路径
                xmlFilePath = Path.Combine(appDirectory, "project_files.XML");
                string projectName = "project_tester_name";
                dllFilePath = Path.Combine(appDirectory, $"{projectName}.dll");
                sprojFilePath = Path.Combine(appDirectory, $"{projectName}.sproj");

                // 检查并重命名DLL文件
                if (File.Exists(dllFilePath) && !File.Exists(sprojFilePath))
                {
                    File.Move(dllFilePath, sprojFilePath);
                }

                // 加载或初始化projectData
                if (File.Exists(xmlFilePath))
                {
                    using (FileStream fs = new FileStream(xmlFilePath, FileMode.Open))
                    {
                        projectData = (ProjectData)serializer.Deserialize(fs);
                    }
                }
                else
                {
                    // 添加默认项并保存
                    projectData.ProjectNames.Add("project_tester_name.sproj");
                    projectData.DocumentCount = 1;
                    SaveProjects();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"初始化ProjectLoader时发生错误: {ex.Message}", "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                projectData = new ProjectData(); // 确保对象处于有效状态
            }
        }

        // 公共静态属性获取单例实例
        public static ProjectLoader Instance
        {
            get { return instance; }
        }

        // 获取projectNames列表
        public List<string> GetProjectNames()
        {
            return new List<string>(projectData.ProjectNames); // 返回副本，保持封装性
        }

        // 添加项目名称
        public void AddProject(string projectName)
        {
            try
            {
                if (!string.IsNullOrEmpty(projectName) &&
                    !projectData.ProjectNames.Any(p => p.Equals(projectName, StringComparison.OrdinalIgnoreCase)))
                {
                    projectData.ProjectNames[0] = projectName;
                    projectData.DocumentCount = projectData.ProjectNames.Count;
                    SaveProjects();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"添加项目时发生错误: {ex.Message}", "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 保存项目列表到XML文件
        private void SaveProjects()
        {
            try
            {
                using (FileStream fs = new FileStream(xmlFilePath, FileMode.Create))
                {
                    serializer.Serialize(fs, projectData);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"保存项目列表时发生错误: {ex.Message}", "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}