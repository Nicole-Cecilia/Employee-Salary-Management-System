using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace EmployeeSalaryManagementSystem
{
    public partial class AdminForm : Form
    {
        private string _username;

        private Label lblTitle;
        private GroupBox gbBaseInfo;
        private GroupBox gbAttendance;
        private GroupBox gbAnnual;
        private GroupBox gbReport;
        private GroupBox gbAuth;

        private Button[] dynamicButtons;

        public AdminForm(string username)
        {
            _username = username;
            InitializeComponent();
            Load += AdminForm_Load;
        }

        private void InitializeComponent()
        {
            lblTitle = new Label();
            gbBaseInfo = new GroupBox();
            gbAttendance = new GroupBox();
            gbAnnual = new GroupBox();
            gbReport = new GroupBox();
            gbAuth = new GroupBox();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("微软雅黑", 16F, FontStyle.Bold);
            lblTitle.Location = new Point(12, 9);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(283, 58);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "员工工资管理系统";
            // 
            // gbBaseInfo
            // 
            gbBaseInfo.Location = new Point(40, 70);
            gbBaseInfo.Name = "gbBaseInfo";
            gbBaseInfo.Size = new Size(240, 290);
            gbBaseInfo.TabIndex = 1;
            gbBaseInfo.TabStop = false;
            gbBaseInfo.Text = "基础信息管理";
            // 
            // gbAttendance
            // 
            gbAttendance.Location = new Point(290, 70);
            gbAttendance.Name = "gbAttendance";
            gbAttendance.Size = new Size(240, 290);
            gbAttendance.TabIndex = 2;
            gbAttendance.TabStop = false;
            gbAttendance.Text = "考勤加班管理";
            // 
            // gbAnnual
            // 
            gbAnnual.Location = new Point(540, 70);
            gbAnnual.Name = "gbAnnual";
            gbAnnual.Size = new Size(240, 290);
            gbAnnual.TabIndex = 3;
            gbAnnual.TabStop = false;
            gbAnnual.Text = "年度查询";
            // 
            // gbReport
            // 
            gbReport.Location = new Point(790, 70);
            gbReport.Name = "gbReport";
            gbReport.Size = new Size(240, 160);
            gbReport.TabIndex = 4;
            gbReport.TabStop = false;
            gbReport.Text = "薪资计算";
            // 
            // gbAuth
            // 
            gbAuth.Location = new Point(790, 240);
            gbAuth.Name = "gbAuth";
            gbAuth.Size = new Size(240, 120);
            gbAuth.TabIndex = 5;
            gbAuth.TabStop = false;
            gbAuth.Text = "系统操作";
            // 
            // AdminForm
            // 
            ClientSize = new Size(1071, 416);
            Controls.Add(lblTitle);
            Controls.Add(gbBaseInfo);
            Controls.Add(gbAttendance);
            Controls.Add(gbAnnual);
            Controls.Add(gbReport);
            Controls.Add(gbAuth);
            Name = "AdminForm";
            Text = "管理员界面";
            ResumeLayout(false);
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
            // 1. 基础信息管理按钮
            CreateButton(gbBaseInfo, "部门信息管理", 20, 110, btnDeptManagement_Click);
            CreateButton(gbBaseInfo, "员工信息管理", 20, 30, btnEmpManagement_Click);
            CreateButton(gbBaseInfo, "工种薪资管理", 20, 70, btnJobtypeManagement_Click);

            // 2. 考勤加班管理按钮
            CreateButton(gbAttendance, "员工请假管理", 20, 30, btnLeaveManagement_Click);
            CreateButton(gbAttendance, "月度考勤管理", 20, 70, btnMonthlyAttendance_Click);
            CreateButton(gbAttendance, "月度加班管理", 20, 110, btnMonthlyOvertime_Click);
            CreateButton(gbAttendance, "月度员工薪资", 20, 150, btnMonthlySalary_Click);

            // 3. 年度查询按钮
            CreateButton(gbAnnual, "年度部门薪资查询", 20, 30, btnAnnualDeptSalary_Click);
            CreateButton(gbAnnual, "年度员工薪资查询", 20, 70, btnAnnualEmpSalary_Click);
            CreateButton(gbAnnual, "生成员工年终奖金", 20, 110, btnAnnualBonus_Click);

            // 4. 报表管理按钮
            CreateButton(gbReport, "月度企业工资报表", 20, 30, btnMonthlyReport_Click);
            CreateButton(gbReport, "年度企业工资报表", 20, 70, btnAnnualReport_Click);

            // 5. 系统操作按钮
            CreateButton(gbAuth, "修改密码", 20, 30, btnChangePassword_Click);
            CreateButton(gbAuth, "退出登录", 20, 70, btnLogout_Click);
        }

        // 封装按钮创建逻辑
        private Button CreateButton(GroupBox container, string text, int x, int y, EventHandler clickEvent)
        {
            Button btn = new Button
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(200, 30),
                Font = new Font("微软雅黑", 9)
            };
            btn.Click += clickEvent;
            container.Controls.Add(btn);
            return btn;
        }

        private void btnDeptManagement_Click(object? sender, EventArgs? e)
        {
            DataManagementForm form = new DataManagementForm("dept_info", "部门信息管理", new string[] { "Dno", "Dname", "Eno" }, new string[] { "部门编号", "部门名称", "负责人工号" });
            form.ShowDialog();
        }

        private void btnEmpManagement_Click(object? sender, EventArgs? e)
        {
            DataManagementForm form = new DataManagementForm("emp_info", "员工信息管理", new string[] { "Eno", "Ename", "Esex", "Ebirth", "Etitle", "Egrade", "Dno", "Ehire_date", "Ede" }, new string[] { "员工工号", "姓名", "性别", "出生年月", "职务", "职称", "所在部门编号", "入职时间", "月专项扣除" });
            form.ShowDialog();
        }

        private void btnJobtypeManagement_Click(object? sender, EventArgs? e)
        {
            DataManagementForm form = new DataManagementForm("jobtype_rule", "工种薪资管理", new string[] { "Etitle", "Egrade", "baseS" }, new string[] { "工种", "等级", "基本工资" });
            form.ShowDialog();
        }

        private void btnLeaveManagement_Click(object? sender, EventArgs? e)
        {
            DataManagementForm form = new DataManagementForm("el", "员工请假管理", new string[] { "Eno", "Ldate" }, new string[] { "员工工号", "请假日期" });
            form.ShowDialog();
        }

        private void btnMonthlyAttendance_Click(object? sender, EventArgs? e)
        {
            InputForm inputForm = new InputForm("请输入查询月份（格式：YYYY-MM）", "月薪考勤管理");
            if (inputForm.ShowDialog() == DialogResult.OK)
            {
                string month = inputForm.InputValue.Trim();
                if (!System.Text.RegularExpressions.Regex.IsMatch(month, @"^\d{4}-\d{2}$"))
                {
                    MessageBox.Show("输入格式错误，请严格按照 YYYY-MM 格式输入，如 2025-06", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    ExecuteAttendanceSql(month);
                }
                catch (MySqlException ex)
                {
                    if (ex.Number != 1062)
                    {
                        MessageBox.Show($"处理考勤数据时出错：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"处理考勤数据时出错：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 无论插入是否成功，都尝试查询数据
                DataManagementForm form = new DataManagementForm("attendance", "月度考勤管理",
                    new string[] { "Eno", "Amonth", "late", "early", "absent" },
                    new string[] { "员工工号", "考勤月份", "迟到次数", "早退次数", "旷工次数" },
                    $"Amonth = '{month}'");
                form.ShowDialog();
            }
        }

        private bool ExecuteAttendanceSql(string targetMonth)
        {
            string connectionString = "Server=localhost;Database=employ_salary;Uid=root;Pwd=YOUR_PASSWORD;";
            string sql = @"
                INSERT INTO attendance (Eno, Amonth, late, early, absent)
                SELECT 
                    a.Eno AS Eno,
                    DATE_FORMAT(a.date_thisyear, '%Y-%m') AS Amonth,
                    SUM(CASE WHEN a.IsLate = 1 THEN 1 ELSE 0 END) AS late,
                    SUM(CASE WHEN a.IsEarly = 1 THEN 1 ELSE 0 END) AS early,
                    SUM(CASE WHEN a.IsAbsent = 1 THEN 1 ELSE 0 END) AS absent
                FROM (
                    SELECT 
                        e.Eno AS Eno,
                        d.date_thisyear AS date_thisyear,
                        ea1.Atime1 AS Atime1,
                        ea2.Atime2 AS Atime2,
                        el.Ldate AS Ldate,
                        r.baseS AS baseS,
                        TIMESTAMPDIFF(MINUTE, CONCAT(CAST(d.date_thisyear AS DATE), ' 09:30:00'), ea1.Atime1) AS late_minutes,
                        TIMESTAMPDIFF(MINUTE, ea2.Atime2, CONCAT(CAST(d.date_thisyear AS DATE), ' 18:30:00')) AS early_minutes,
                        CASE 
                            WHEN (TIMESTAMPDIFF(MINUTE, CONCAT(CAST(d.date_thisyear AS DATE), ' 09:30:00'), ea1.Atime1) BETWEEN 1 AND 10) 
                                AND ((SELECT COUNT(0) FROM ea1 t 
                                      WHERE t.Eno = e.Eno 
                                        AND DATE_FORMAT(t.Atime1, '%Y-%m') = DATE_FORMAT(d.date_thisyear, '%Y-%m')
                                        AND TIMESTAMPDIFF(MINUTE, CONCAT(CAST(t.Atime1 AS DATE), ' 09:30:00'), t.Atime1) BETWEEN 1 AND 10) > 2) 
                                THEN 1
                            WHEN TIMESTAMPDIFF(MINUTE, CONCAT(CAST(d.date_thisyear AS DATE), ' 09:30:00'), ea1.Atime1) BETWEEN 11 AND 30 
                                THEN 1
                            ELSE 0 
                        END AS IsLate,
                        CASE 
                            WHEN TIMESTAMPDIFF(MINUTE, ea2.Atime2, CONCAT(CAST(d.date_thisyear AS DATE), ' 18:30:00')) BETWEEN 1 AND 10 
                                THEN 1
                            WHEN TIMESTAMPDIFF(MINUTE, ea2.Atime2, CONCAT(CAST(d.date_thisyear AS DATE), ' 18:30:00')) BETWEEN 11 AND 30 
                                THEN 1
                            ELSE 0 
                        END AS IsEarly,
                        CASE 
                            WHEN ((ea1.Atime1 IS NULL OR ea2.Atime2 IS NULL) AND el.Ldate IS NULL) 
                                THEN 1
                            WHEN TIMESTAMPDIFF(MINUTE, CONCAT(CAST(d.date_thisyear AS DATE), ' 09:30:00'), ea1.Atime1) > 30 
                                THEN 1
                            WHEN TIMESTAMPDIFF(MINUTE, ea2.Atime2, CONCAT(CAST(d.date_thisyear AS DATE), ' 18:30:00')) > 30 
                                THEN 1
                            ELSE 0 
                        END AS IsAbsent
                    FROM 
                        emp_info e
                    JOIN jobtype_rule r 
                        ON e.Etitle = r.Etitle AND e.Egrade = r.Egrade
                    JOIN tyday d 
                        ON d.is_workday = 1  
                        AND DATE_FORMAT(d.date_thisyear, '%Y-%m') = @target_month  
                    LEFT JOIN ea1 
                        ON e.Eno = ea1.Eno AND CAST(d.date_thisyear AS DATE) = CAST(ea1.Atime1 AS DATE)
                    LEFT JOIN ea2 
                        ON e.Eno = ea2.Eno AND CAST(d.date_thisyear AS DATE) = CAST(ea2.Atime2 AS DATE)
                    LEFT JOIN el 
                        ON e.Eno = el.Eno AND d.date_thisyear = el.Ldate
                ) a
                GROUP BY 
                    a.Eno, 
                    DATE_FORMAT(a.date_thisyear, '%Y-%m');";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    MySqlCommand command = new MySqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@target_month", targetMonth);
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"插入考勤数据时出错：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void btnMonthlyOvertime_Click(object? sender, EventArgs? e)
        {
            InputForm inputForm = new InputForm("请输入查询月份（格式：YYYY-MM）", "月度加班管理");
            if (inputForm.ShowDialog() == DialogResult.OK)
            {
                string month = inputForm.InputValue.Trim();
                if (!System.Text.RegularExpressions.Regex.IsMatch(month, @"^\d{4}-\d{2}$"))
                {
                    MessageBox.Show("输入格式错误，请严格按照 YYYY-MM 格式输入，如 2025-06", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    ExecuteOvertimeSql(month);
                }
                catch (MySqlException ex)
                {
                    if (ex.Number != 1062)
                    {
                        MessageBox.Show($"处理加班数据时出错：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"处理加班数据时出错：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DataManagementForm form = new DataManagementForm("overtime", "月度加班管理",
                    new string[] { "Eno", "Omonth", "wdhour", "wehour", "hlhour" },
                    new string[] { "员工工号", "加班月份", "平日加班小时数", "周末加班小时数", "节假日加班小时数" },
                    $"Omonth = '{month}'");
                form.ShowDialog();
            }
        }

        private bool ExecuteOvertimeSql(string targetMonth)
        {
            string connectionString = "Server=localhost;Database=employ_salary;Uid=root;Pwd=YOUR_PASSWORD;";
            string sql = @"
                INSERT INTO overtime (Eno, Omonth, wdhour, wehour, hlhour)
                SELECT 
                    `e`.`Eno` AS `Eno`,
                    DATE_FORMAT(`ot`.`OTDate`, '%Y-%m') AS `Omonth`,
                    SUM(CASE WHEN `ot`.`IsWeekday` = 1 THEN `ot`.`OTHours` ELSE 0 END) AS `wdhour`,
                    SUM(CASE WHEN `ot`.`IsWeekend` = 1 THEN `ot`.`OTHours` ELSE 0 END) AS `wehour`,
                    SUM(CASE WHEN `ot`.`IsHoliday` = 1 THEN `ot`.`OTHours` ELSE 0 END) AS `hlhour`
                FROM (
                    SELECT 
                        `e`.`Eno` AS `Eno`,
                        `d`.`date_thisyear` AS `OTDate`,
                        `d`.`is_workday` AS `is_workday`,
                        `d`.`is_weekend` AS `is_weekend`,
                        `d`.`is_holiday` AS `is_holiday`,
                        `r`.`baseS` AS `baseS`,
                        `ea1`.`Atime1` AS `Atime1`,
                        `ea2`.`Atime2` AS `Atime2`,
                        CASE 
                            WHEN `d`.`is_workday` = 1 THEN 
                                GREATEST(TIMESTAMPDIFF(MINUTE, DATE_FORMAT(`ea2`.`Atime2`, '%Y-%m-%d 19:30:00'), `ea2`.`Atime2`) / 60, 0)
                            ELSE 
                                GREATEST(TIMESTAMPDIFF(MINUTE, `ea1`.`Atime1`, `ea2`.`Atime2`) / 60, 0)
                        END AS `OTHours`,
                        `d`.`is_workday` AS `IsWeekday`,
                        `d`.`is_weekend` AS `IsWeekend`,
                        `d`.`is_holiday` AS `IsHoliday`
                    FROM 
                        `emp_info` `e`
                    JOIN `jobtype_rule` `r` 
                        ON `e`.`Etitle` = `r`.`Etitle` AND `e`.`Egrade` = `r`.`Egrade`
                    JOIN `tyday` `d` 
                        ON (`d`.`is_workday` = 1 OR `d`.`is_weekend` = 1 OR `d`.`is_holiday` = 1)
                        AND DATE_FORMAT(`d`.`date_thisyear`, '%Y-%m') = @target_month  
                    LEFT JOIN `ea1` 
                        ON `e`.`Eno` = `ea1`.`Eno` AND CAST(`d`.`date_thisyear` AS DATE) = CAST(`ea1`.`Atime1` AS DATE)
                    LEFT JOIN `ea2` 
                        ON `e`.`Eno` = `ea2`.`Eno` AND CAST(`d`.`date_thisyear` AS DATE) = CAST(`ea2`.`Atime2` AS DATE)
                    WHERE 
                        ((`d`.`is_workday` = 1 AND `ea2`.`Atime2` > DATE_FORMAT(`d`.`date_thisyear`, '%Y-%m-%d 19:30:00'))
                        OR (`d`.`is_weekend` = 1 AND `ea1`.`Atime1` IS NOT NULL AND `ea2`.`Atime2` IS NOT NULL)
                        OR (`d`.`is_holiday` = 1 AND `ea1`.`Atime1` IS NOT NULL AND `ea2`.`Atime2` IS NOT NULL))
                ) `ot`
                JOIN `emp_info` `e` 
                    ON `ot`.`Eno` = `e`.`Eno`
                GROUP BY 
                    `e`.`Eno`, 
                    DATE_FORMAT(`ot`.`OTDate`, '%Y-%m');";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    MySqlCommand command = new MySqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@target_month", targetMonth);
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"插入加班数据时出错：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void btnMonthlySalary_Click(object sender, EventArgs e)
        {
            InputForm inputForm = new InputForm("请输入查询月份（格式：YYYY-MM）", "月度员工薪资");
            if (inputForm.ShowDialog() == DialogResult.OK)
            {
                string month = inputForm.InputValue.Trim();
                if (!System.Text.RegularExpressions.Regex.IsMatch(month, @"^\d{4}-\d{2}$"))
                {
                    MessageBox.Show("输入格式错误，请严格按照 YYYY-MM 格式输入，如 2025-06", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    ExecuteMonthlySalaryInsertSql(month);
                }
                catch (MySqlException ex)
                {
                    if (ex.Number != 1062)
                    {
                        MessageBox.Show($"处理薪资数据时出错：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"处理薪资数据时出错：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DataTable salaryTable = GetMonthlySalaryDataForShow(month);

                string[] columnNames = { "Eno", "Smonth", "baseS", "Oall", "Ade", "grossS", "aaS", "aa_de", "aa_tax", "tax", "netS" };
                string[] displayNames = { "员工工号", "月份", "基础工资", "加班津贴", "考勤扣款", "应发工资", "年度累计收入", "年度累计扣除", "年度累计个税", "当月个税", "实际工资" };

                DataManagementForm form = new DataManagementForm(
                    "msalary",
                    "月度员工薪资报表",
                    columnNames,
                    displayNames,
                    $"Smonth = '{month}'"
                );
                form.dataGridView1.DataSource = salaryTable;

                for (int i = 0; i < columnNames.Length; i++)
                {
                    if (form.dataGridView1.Columns.Contains(columnNames[i]))
                    {
                        form.dataGridView1.Columns[columnNames[i]].HeaderText = displayNames[i];
                    }
                }

                form.ShowDialog();
            }
        }

        private bool ExecuteMonthlySalaryInsertSql(string targetMonth)
        {
            string connectionString = "Server=localhost;Database=employ_salary;Uid=root;Pwd=YOUR_PASSWORD;";
            string sql = @"
                INSERT INTO msalary (
                    Eno, Smonth, baseS, Oall, Ade, grossS, 
                    aaS, aa_de, aa_tax, tax, netS
                )
                WITH 
                MonthlyDeduction AS (
                    SELECT 
                        e.Eno,
                        @target_month AS Smonth,
                        ROUND(
                            (LEAST(a.late, 2) * 10) + (GREATEST(a.late - 2, 0) * 30) +
                            (LEAST(a.early, 2) * 10) + (GREATEST(a.early - 2, 0) * 30) +
                            (a.absent * (r.baseS / 21.75 * 2)),
                            2
                        ) AS Ade
                    FROM emp_info e
                    JOIN jobtype_rule r 
                        ON e.Etitle = r.Etitle AND e.Egrade = r.Egrade
                    LEFT JOIN attendance a 
                        ON e.Eno = a.Eno 
                        AND a.Amonth = @target_month
                ),
                MonthlyOvertime AS (
                    SELECT 
                        e.Eno,
                        @target_month AS Smonth,
                        ROUND(
                            (
                                (o.wdhour * 1.5) +
                                (o.wehour * 2) +
                                (o.hlhour * 3)
                            ) * (r.baseS / 21.75 / 9),
                            2
                        ) AS Oall
                    FROM emp_info e
                    JOIN jobtype_rule r 
                        ON e.Etitle = r.Etitle AND e.Egrade = r.Egrade
                    LEFT JOIN overtime o 
                        ON e.Eno = o.Eno 
                        AND o.Omonth = @target_month
                ),
                MonthlyData AS (
                    SELECT 
                        ei.Eno,
                        @target_month AS Smonth,
                        jr.baseS,
                        COALESCE(mo.Oall, 0) AS Oall,
                        COALESCE(md.Ade, 0) AS Ade,
                        (jr.baseS + COALESCE(mo.Oall, 0) - COALESCE(md.Ade, 0)) AS grossS,
                        ei.Ede
                    FROM emp_info ei
                    JOIN jobtype_rule jr 
                        ON ei.Etitle = jr.Etitle AND ei.Egrade = jr.Egrade
                    LEFT JOIN MonthlyOvertime mo 
                        ON ei.Eno = mo.Eno AND mo.Smonth = @target_month
                    LEFT JOIN MonthlyDeduction md 
                        ON ei.Eno = md.Eno AND md.Smonth = @target_month
                ),
                PreviousMonthCumulative AS (
                    SELECT 
                        Eno,
                        aaS AS prev_aaS,
                        aa_de AS prev_aa_de,
                        aa_tax AS prev_aa_tax
                    FROM msalary
                    WHERE Smonth = DATE_FORMAT(STR_TO_DATE(CONCAT(@target_month, '-01'), '%Y-%m-%d') - INTERVAL 1 MONTH, '%Y-%m')
                ),
                NewCumulative AS (
                    SELECT 
                        md.Eno,
                        md.Smonth,
                        md.baseS,
                        md.Oall,
                        md.Ade,
                        md.grossS,
                        md.Ede,
                        COALESCE(pmc.prev_aaS, 0) + md.grossS AS aaS,
                        COALESCE(pmc.prev_aa_de, 0) + 5000 + md.Ede AS aa_de,
                        COALESCE(pmc.prev_aa_tax, 0) AS pre_aa_tax
                    FROM MonthlyData md
                    LEFT JOIN PreviousMonthCumulative pmc 
                        ON md.Eno = pmc.Eno
                ),
                TaxAndNet AS (
                    SELECT 
                        nc.Eno,
                        nc.Smonth,
                        nc.baseS,
                        nc.Oall,
                        nc.Ade,
                        nc.grossS,
                        nc.aaS,
                        nc.aa_de,
                        nc.pre_aa_tax,
                        (nc.aaS - nc.aa_de) AS cumulative_taxable_income,
                        ROUND(
                            GREATEST(
                                (nc.aaS - nc.aa_de) * 
                                CASE 
                                    WHEN (nc.aaS - nc.aa_de) <= 36000 THEN 0.03
                                    WHEN (nc.aaS - nc.aa_de) <= 144000 THEN 0.1
                                    ELSE 0.2 
                                END 
                                - 
                                CASE 
                                    WHEN (nc.aaS - nc.aa_de) <= 36000 THEN 0
                                    WHEN (nc.aaS - nc.aa_de) <= 144000 THEN 2520
                                    ELSE 16920 
                                END 
                                - nc.pre_aa_tax,
                                0
                            ),
                            2
                        ) AS tax,
                        ROUND(nc.grossS - 
                            GREATEST(
                                (nc.aaS - nc.aa_de) * 
                                CASE 
                                    WHEN (nc.aaS - nc.aa_de) <= 36000 THEN 0.03
                                    WHEN (nc.aaS - nc.aa_de) <= 144000 THEN 0.1
                                    ELSE 0.2 
                                END 
                                - 
                                CASE 
                                    WHEN (nc.aaS - nc.aa_de) <= 36000 THEN 0
                                    WHEN (nc.aaS - nc.aa_de) <= 144000 THEN 2520
                                    ELSE 16920 
                                END 
                                - nc.pre_aa_tax,
                                0
                            ),
                            2
                        ) AS netS
                    FROM NewCumulative nc
                )
                SELECT 
                    Eno, 
                    Smonth, 
                    baseS, 
                    Oall, 
                    Ade, 
                    grossS, 
                    aaS, 
                    aa_de, 
                    pre_aa_tax + tax AS aa_tax, 
                    tax, 
                    netS
                FROM TaxAndNet;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(sql, connection);
                command.Parameters.AddWithValue("@target_month", targetMonth);
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        private DataTable GetMonthlySalaryDataForShow(string month)
        {
            string connectionString = "Server=localhost;Database=employ_salary;Uid=root;Pwd=YOUR_PASSWORD;";
            string query = "SELECT * FROM msalary WHERE Smonth = @month";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@month", month);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                return dataTable;
            }
        }

        private void btnAnnualDeptSalary_Click(object? sender, EventArgs? e)
        {
            InputForm inputForm = new InputForm("请输入年份,部门编号（如 2025 101）", "年度部门薪资查询");
            if (inputForm.ShowDialog() == DialogResult.OK)
            {
                string[] parts = inputForm.InputValue.Split(' ');
                if (parts.Length == 2)
                {
                    string year = parts[0].Trim();
                    string deptNo = parts[1].Trim();
                    SalaryReportForm form = new SalaryReportForm(
                        "年度部门薪资查询",
                        year,
                        deptNo,
                        isAnnualDept: true
                    );
                    form.ShowDialog();
                }
                else
                {
                    MessageBox.Show("输入格式错误！需为 年份 部门编号", "提示");
                }
            }
        }

        private void btnAnnualEmpSalary_Click(object? sender, EventArgs? e)
        {
            InputForm inputForm = new InputForm("请输入年份,员工编号（如 2025 E123456）", "年度员工薪资查询");
            if (inputForm.ShowDialog() == DialogResult.OK)
            {
                string[] parts = inputForm.InputValue.Split(' ');
                if (parts.Length == 2)
                {
                    string year = parts[0].Trim();
                    string empNo = parts[1].Trim();
                    SalaryReportForm form = new SalaryReportForm(
                        "年度员工薪资查询",
                        year,
                        empNo,
                        isAnnualEmp: true
                    );
                    form.ShowDialog();
                }
                else
                {
                    MessageBox.Show("输入格式错误！需为 年份 员工编号", "提示");
                }
            }
        }

        private void btnAnnualBonus_Click(object? sender, EventArgs? e)
        {
            InputForm inputForm = new InputForm("请输入查询年份（格式：YYYY）", "生成员工年终奖金");
            if (inputForm.ShowDialog() == DialogResult.OK)
            {
                string year = inputForm.InputValue.Trim();
                if (!System.Text.RegularExpressions.Regex.IsMatch(year, @"^\d{4}$"))
                {
                    MessageBox.Show("输入格式错误，请严格按照 YYYY 格式输入，如 2025", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    string targetMonth = $"{year}-12";
                    ExecuteAnnualBonusSql(targetMonth);
                }
                catch (MySqlException ex)
                {
                    if (ex.Number != 1062)
                    {
                        MessageBox.Show($"处理年终奖金数据时出错：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"处理年终奖金数据时出错：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DataTable bonusTable = GetAnnualBonusDataForShow(year);
                DataManagementForm form = new DataManagementForm(
                    "bonus",
                    "年终奖金查询",
                    new string[] { "Eno", "Byear", "aaS", "Bonus" },
                    new string[] { "员工工号", "年份", "年度应发薪资", "年终奖金" },
                    $"Byear = '{year}'"
                );
                form.dataGridView1.DataSource = bonusTable;
                form.dataGridView1.ReadOnly = true;
                form.ShowDialog();
            }
        }
        private DataTable GetAnnualBonusDataForShow(string year)
        {
            string connectionString = "Server=localhost;Database=employ_salary;Uid=root;Pwd=YOUR_PASSWORD;";
            string query = "SELECT * FROM bonus WHERE Byear = @year";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@year", year);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                return dataTable;
            }
        }
        private bool ExecuteAnnualBonusSql(string targetMonth)
        {
            string connectionString = "Server=localhost;Database=employ_salary;Uid=root;Pwd=YOUR_PASSWORD;";
            string sql = @"
                INSERT INTO bonus (Eno, Byear, aaS, Bonus)
                SELECT 
                    Eno,
                    SUBSTRING(Smonth, 1, 4) AS Byear,  
                    aaS,
                    aaS / 12 AS Bonus  
                FROM msalary
                WHERE Smonth = @targetMonth;";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    MySqlCommand command = new MySqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@targetMonth", targetMonth);
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"插入年终奖金数据时出错：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void btnMonthlyReport_Click(object? sender, EventArgs? e)
        {
            InputForm inputForm = new InputForm("请输入查询月份（格式：YYYY-MM）", "月度企业工资报表");
            if (inputForm.ShowDialog() == DialogResult.OK)
            {
                string month = inputForm.InputValue.Trim();
                // 格式验证
                if (!System.Text.RegularExpressions.Regex.IsMatch(month, @"^\d{4}-\d{2}$"))
                {
                    MessageBox.Show("输入格式错误，请严格按照 YYYY-MM 格式输入，如 2025-06", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                SalaryReportForm form = new SalaryReportForm("月度企业工资报表", month, "", false, false, false, true, false);
                form.ShowDialog();
            }
        }

        private void btnAnnualReport_Click(object? sender, EventArgs? e)
        {
            InputForm inputForm = new InputForm("请输入查询年份（格式：YYYY）", "年度企业工资报表");
            if (inputForm.ShowDialog() == DialogResult.OK)
            {
                string year = inputForm.InputValue.Trim();
                // 格式验证
                if (!System.Text.RegularExpressions.Regex.IsMatch(year, @"^\d{4}$"))
                {
                    MessageBox.Show("输入格式错误，请严格按照 YYYY 格式输入，如 2025", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                SalaryReportForm form = new SalaryReportForm("年度企业工资报表", year, "", false, false, false, false, true);
                form.ShowDialog();
            }
        }

        private void btnChangePassword_Click(object? sender, EventArgs? e)
        {
            ChangePasswordForm form = new ChangePasswordForm(_username, true);
            form.ShowDialog();
        }

        private void btnLogout_Click(object? sender, EventArgs? e)
        {
            this.Close();
        }
    }
}