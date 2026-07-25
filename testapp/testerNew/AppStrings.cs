using System.Collections.Generic;

namespace test_antdui
{
    public static class AppStrings
    {
        public static string CurrentLang { get; private set; } = "en";
        public static bool IsZh => CurrentLang == "zh";

        private static readonly Dictionary<string, Dictionary<string, string>> _s = new Dictionary<string, Dictionary<string, string>>()
        {
            // ── Menu ──
            ["menu_settings"] = Make("系统设置", "Settings"),
            ["menu_stats"] = Make("产能统计", "Production"),
            ["menu_clear"] = Make("清空产能", "Clear Data"),
            ["menu_shift"] = Make("切换班次", "Switch Shift"),
            ["menu_help"] = Make("帮助", "Help"),

            // ── Stats ──
            ["stats_total"] = Make("累计统计", "Total Stats"),
            ["stats_total_sub"] = Make("总测试", "Total"),
            ["stats_hourly"] = Make("本时产能", "Hourly Stats"),
            ["stats_hourly_sub"] = Make("本时", "Hour"),
            ["yield"] = Make("良率", "Yield"),
            ["day_shift"] = Make("白班", "Day"),
            ["night_shift"] = Make("夜班", "Night"),
            ["shift_format"] = Make("{0}班", "{0}"),
            ["op_title"] = Make("操作信息", "Operator"),
            ["op_not_logged"] = Make("未登录", "Not Logged"),
            ["op_id"] = Make("工号: {0}", "ID: {0}"),
            ["hour_title"] = Make("⏱ {0:D2}:00-{1:D2}:00 产能", "⏱ {0:D2}:00-{1:D2}:00"),
            ["total_vs_hourly"] = Make("累计: {0} | PASS: {1} | FAIL: {2}\n本时: {3} | PASS: {4} | FAIL: {5}",
                                       "Total: {0} | PASS: {1} | FAIL: {2}\nHour: {3} | PASS: {4} | FAIL: {5}"),

            // ── Status Bar ──
            ["status_operator"] = Make("操作工: {0}", "Op: {0}"),
            ["status_total"] = Make("累计: {0}", "Total: {0}"),
            ["status_hour"] = Make("本时: {0}", "Hour: {0}"),
            ["status_ready"] = Make("● 系统就绪", "● Ready"),
            ["status_line"] = Make("● 操作工: {0}  |  累计: {1} (PASS:{2} FAIL:{3})  |  本时: {4}",
                                    "● Op: {0}  |  Total: {1} (PASS:{2} FAIL:{3})  |  Hour: {4}"),

            // ── Buttons ──
            ["btn_load"] = Make("📂 加载模板", "📂 Load"),
            ["btn_start"] = Make("▶ 开始测试", "▶ Start"),
            ["btn_save"] = Make("💾 保存报告", "💾 Save"),
            ["btn_stop"] = Make("⏹ 停止", "⏹ Stop"),
            ["chk_save_excel"] = Make("保存Excel报告", "Save Excel"),
            ["chk_append"] = Make("追加记录", "Append"),
            ["chk_stop_on_fail"] = Make("Fail时停止", "Stop on Fail"),
            ["chk_log_append"] = Make("LOG追加", "Log Append"),

            // ── Window Bar ──
            ["window_title"] = Make("Tester", "Tester"),
            ["window_subtitle"] = Make("PCBA 测试平台", "PCBA Test Platform"),
            ["search_placeholder"] = Make("输入关键字搜索...", "Search..."),

            // ── Barcode ──
            ["barcode_placeholder"] = Make("扫描条码 / 手动输入 SN...", "Scan barcode / enter SN..."),
            ["barcode_format"] = Make("格式: {0}", "Format: {0}"),
            ["barcode_waiting"] = Make("等待扫描...", "Waiting..."),
            ["barcode_match"] = Make("✅ 匹配成功", "✅ Match"),
            ["barcode_no_match"] = Make("❌ 格式不匹配", "❌ Mismatch"),
            ["barcode_status"] = Make("格式: {0}  |  {1}", "Format: {0}  |  {1}"),
            ["sn_entered"] = Make("SN: {0} 已录入", "SN: {0} entered"),
            ["barcode_scanned"] = Make("条码录入: {0}", "Barcode: {0}"),
            ["barcode_invalid"] = Make("条码格式不匹配！", "Barcode format mismatch!"),

            // ── Scanner ──
            ["scanner_connected"] = Make("📡 {0} 已连接", "📡 {0} connected"),
            ["scanner_disconnected"] = Make("📡 未连接", "📡 Offline"),

            // ── Chart ──
            ["chart_title"] = Make("今日产能分布  (点击任一时段查看详情)", "Today's Production  (click hour for details)"),

            // ── Alert ──
            ["alert_ready"] = Make("系统就绪", "System Ready"),
            ["alert_ready_sn"] = Make("SN: {0} | 等待扫描条码", "SN: {0} | Scan barcode"),
            ["alert_testing"] = Make("⚠️ 测试中", "⚠️ Testing"),
            ["alert_testing_sn"] = Make("SN: {0} | 测试中...", "SN: {0} | Testing..."),
            ["alert_pass"] = Make("✅ 测试通过", "✅ All Passed"),
            ["alert_pass_sn"] = Make("SN: {0} | 全通过", "SN: {0} | Passed"),
            ["alert_fail"] = Make("❌ 测试失败", "❌ Failed"),
            ["alert_fail_sn"] = Make("SN: {0} | {1} 项失败", "SN: {0} | {1} failed"),

            // ── Table Columns ──
            ["col_id"] = Make("序号", "No."),
            ["col_name"] = Make("测试项目", "Test Item"),
            ["col_low"] = Make("下限", "Low"),
            ["col_high"] = Make("上限", "High"),
            ["col_value"] = Make("实测值", "Measured"),
            ["col_result"] = Make("结果", "Result"),
            ["col_duration"] = Make("时长", "Time"),

            // ── Dialogs ──
            ["dlg_load_filter"] = Make("测试文件|*.sproj;*.xlsx;*.xls", "Test Files|*.sproj;*.xlsx;*.xls"),
            ["dlg_load_title"] = Make("选择测试模板", "Select Test Template"),
            ["dlg_save_filter"] = Make("Excel文件|*.xlsx", "Excel Files|*.xlsx"),
            ["dlg_save_title"] = Make("保存测试结果", "Save Test Results"),
            ["dlg_save_filename"] = Make("PCBA测试结果_{0:yyyyMMdd_HHmmss}.xlsx", "PCBA_Result_{0:yyyyMMdd_HHmmss}.xlsx"),
            ["dlg_confirm"] = Make("提示", "Info"),
            ["dlg_error"] = Make("错误", "Error"),
            ["dlg_save_ok"] = Make("保存成功", "Saved"),
            ["dlg_no_template"] = Make("请先加载测试模板", "Load a template first"),
            ["dlg_load_fail"] = Make("加载失败: {0}", "Load failed: {0}"),
            ["dlg_save_success"] = Make("保存成功", "Saved successfully"),

            // ── Test Log ──
            ["log_test_start"] = Make("=== 点击开始测试 ===", "=== Test Started ==="),
            ["log_test_complete"] = Make("=== 测试全部完成 ===", "=== Test Complete ==="),
            ["log_test_steps"] = Make("--- 开始执行测试步骤 ---", "--- Running Test Steps ---"),
            ["log_init_fail"] = Make("初始化失败，终止测试", "Init failed, aborting"),
            ["log_no_template"] = Make("错误: 没有加载测试模板", "Error: No template loaded"),
            ["log_items"] = Make("开始执行测试，共 {0} 项", "Running {0} test items"),

            ["log_all_pass"] = Make("★★★ 所有测试通过 ★★★", "★★★ ALL PASSED ★★★"),
            ["log_fail"] = Make("★★★ 测试失败: {0} 项未通过 ★★★", "★★★ FAILED: {0} items ★★★"),
            ["log_template_loaded"] = Make("已加载测试模板: {0}", "Loaded template: {0}"),
            ["log_report_saved"] = Make("测试报告已保存", "Report saved"),
            ["log_report_fail"] = Make("保存报告失败: {0}", "Save report failed: {0}"),

            // ── Test Functions ──
            ["test_voltage"] = Make("测量电压: 范围 {0}V ~ {1}V", "Measure Voltage: {0}V ~ {1}V"),
            ["test_voltage_result"] = Make("实测值: {0:F3}V, 判定: {1}", "Value: {0:F3}V, Result: {1}"),
            ["test_current"] = Make("测量电流: 范围 {0}A ~ {1}A", "Measure Current: {0}A ~ {1}A"),
            ["test_current_result"] = Make("实测值: {0:F4}A, 判定: {1}", "Value: {0:F4}A, Result: {1}"),
            ["test_resistance"] = Make("测量电阻: 范围 {0}Ω ~ {1}Ω", "Measure Resistance: {0}Ω ~ {1}Ω"),
            ["test_resistance_result"] = Make("实测值: {0:F2}Ω, 判定: {1}", "Value: {0:F2}Ω, Result: {1}"),
            ["test_short"] = Make("执行短路测试...", "Short Circuit Test..."),
            ["test_short_result_pass"] = Make("正常", "Normal"),
            ["test_short_result_fail"] = Make("短路", "Shorted"),
            ["test_open"] = Make("执行开路测试...", "Open Circuit Test..."),
            ["test_open_result_pass"] = Make("正常", "Normal"),
            ["test_open_result_fail"] = Make("开路", "Open"),
            ["test_flash"] = Make("执行Flash写入测试...", "Flash Write Test..."),
            ["test_flash_result_pass"] = Make("成功", "OK"),
            ["test_flash_result_fail"] = Make("失败", "Fail"),
            ["test_insulation"] = Make("执行绝缘测试...", "Insulation Test..."),
            ["test_init"] = Make("初始化: 开始...", "Init: Start..."),
            ["test_init_done"] = Make("初始化: 完成", "Init: Done"),
            ["test_cleanup"] = Make("清理: 开始...", "Cleanup: Start..."),
            ["test_cleanup_done"] = Make("清理: 完成", "Cleanup: Done"),

            // ── Param Error ──
            ["param_error"] = Make("参数解析错误", "Param Error"),
            ["parse_error"] = Make("解析错误", "Parse Error"),

            // ── Table States ──
            ["state_running"] = Make("运行中", "Running"),
            ["state_skip"] = Make("跳过", "Skipped"),

            // ── Menu Messages ──
            ["menu_shift_switched"] = Make("已切换至{0}", "Switched to {0}"),
            ["menu_help_text"] = Make("testerNew PCBA 测试平台",
                                      "testerNew PCBA Test Platform v1.5"),
        };

        private static Dictionary<string, string> Make(string zh, string en) => new Dictionary<string, string>()
        {
            ["zh"] = zh,
            ["en"] = en
        };

        public static string Get(string key)
        {
            if (_s.TryGetValue(key, out var pair) && pair.TryGetValue(CurrentLang, out var val))
                return val;
            return $"?{key}?";
        }

        public static string Get(string key, params object[] args) => string.Format(Get(key), args);

        public static void SetLanguage(string lang)
        {
            if (lang == "zh" || lang == "en")
                CurrentLang = lang;
        }

        public static void Toggle()
        {
            CurrentLang = IsZh ? "en" : "zh";
        }
    }
}
