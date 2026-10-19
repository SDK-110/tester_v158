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

                // 归一化旧配置：程序目录内的文件改存相对路径；
                // 绝对路径失效时按文件名到程序目录找回，避免程序目录搬移后定位失败。
                if (NormalizeProjectPaths())
                {
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

        // 获取工程文件完整路径列表（配置里存相对路径，调用方需要可直接加载的绝对路径）
        public List<string> GetProjectNames()
        {
            return projectData.ProjectNames
                .Select(p => test_antdui.ProjectPath.ResolvePath(p))
                .ToList();
        }

        // 把配置项归一化为"程序目录内相对路径 / 外部绝对路径"，返回是否有变化
        private bool NormalizeProjectPaths()
        {
            bool changed = false;
            for (int i = 0; i < projectData.ProjectNames.Count; i++)
            {
                string stored = projectData.ProjectNames[i];
                string resolved = test_antdui.ProjectPath.ResolvePath(stored);
                string normalized = test_antdui.ProjectPath.ToStoredPath(resolved);
                if (!string.Equals(stored, normalized, StringComparison.Ordinal))
                {
                    projectData.ProjectNames[i] = normalized;
                    changed = true;
                }
            }
            return changed;
        }

        // 添加项目名称
        public void AddProject(string projectName)
        {
            try
            {
                if (!string.IsNullOrEmpty(projectName) &&
                    !projectData.ProjectNames.Any(p => p.Equals(projectName, StringComparison.OrdinalIgnoreCase)))
                {
                    projectData.ProjectNames[0] = test_antdui.ProjectPath.ToStoredPath(projectName);
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