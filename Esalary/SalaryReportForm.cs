using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace EmployeeSalaryManagementSystem
{
    public partial class SalaryReportForm : Form
    {
        private string reportType;
        private string period;
        private string identifier;
        private bool isAnnualDept;
        private bool isAnnualEmp;
        private bool isEmployee;
        private bool isBonus;
        private bool isMonthlyReport;
        private bool isAnnualReport;
        private string connectionString = "Server=localhost;Database=employ_salary;Uid=root;Pwd=YOUR_PASSWORD;";

        private DataGridView dataGridView1 = new DataGridView();
        private Button btnClose = new Button();

        public SalaryReportForm(
            string reportType,
            string period,
            string identifier = "",
            bool isAnnualDept = false,
            bool isAnnualEmp = false,
            bool isBonus = false,
            bool isMonthlyReport = false,
            bool isAnnualReport = false
        )
        {
            this.reportType = reportType;
            this.period = period;
            this.identifier = identifier;
            this.isAnnualDept = isAnnualDept;
            this.isAnnualEmp = isAnnualEmp;
            this.isBonus = isBonus;
            this.isMonthlyReport = isMonthlyReport;
            this.isAnnualReport = isAnnualReport;
            InitializeComponent();
            LoadData();
        }

        private void InitializeComponent()
        {
            // 初始化 DataGridView
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new System.Drawing.Point(12, 12);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 23;
            dataGridView1.Size = new System.Drawing.Size(776, 350);
            dataGridView1.TabIndex = 0;

            // 初始化 关闭按钮
            btnClose.Location = new System.Drawing.Point(350, 380);
            btnClose.Name = "btnClose";
            btnClose.Size = new System.Drawing.Size(75, 23);
            btnClose.Text = "关闭";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += BtnClose_Click;

            // 窗体布局
            ClientSize = new Size(800, 415);
            Controls.Add(dataGridView1);
            Controls.Add(btnClose);
            Name = "SalaryReportForm";
            Text = reportType + "-" + period;


        }

        private void LoadData()
        {
            try
            {
                string selectQuery = "";

                if (isAnnualDept)
                {
                    // 年度部门薪资查询
                    selectQuery = @"
                        SELECT 
                            d.Dname AS 所选部门,
                            m.Smonth AS 发薪月份,
                            SUM(m.baseS) AS 基本工资总额,
                            SUM(m.Ade) AS 考勤扣款总额,
                            SUM(m.Oall) AS 加班津贴总额,
                            SUM(m.grossS) AS 应发工资总额,
                            SUM(m.tax) AS 个人所得税总额,
                            SUM(m.netS) AS 实发工资总额,
                            COUNT(DISTINCT e.Eno) AS 员工总人数,
                            ROUND(SUM(m.netS) / COUNT(DISTINCT e.Eno), 2) AS 部门平均薪资
                        FROM 
                            msalary m
                        JOIN 
                            emp_info e ON m.Eno = e.Eno
                        JOIN 
                            dept_info d ON e.Dno = d.Dno
                        WHERE 
                            m.Smonth LIKE CONCAT(@Period, '-%')
                            AND e.Dno = @Identifier
                        GROUP BY 
                            m.Smonth
                        ORDER BY 
                            m.Smonth;
                    ";
                }
                // 年度员工薪资查询
                else if (isAnnualEmp)
                {
                    selectQuery = @"
                        SELECT 
                            e.Ename AS 所选员工,
                            e.Ehire_date AS 入职时间,
                            m.Smonth AS 发薪月份,
                            m.baseS AS 基本工资,
                            m.Ade AS 考勤扣款,
                            m.Oall AS 加班津贴,
                            m.grossS AS 应发工资,
                            m.tax AS 个人所得税,
                            m.netS AS 实发工资
                        FROM 
                            msalary m
                        JOIN 
                            emp_info e ON m.Eno = e.Eno
                        WHERE 
                            m.Smonth LIKE CONCAT(@Period, '-%')
                            AND m.Eno = @Identifier;
                    ";
                }

                // 月度企业工资报表查询
                else if (isMonthlyReport)
                {
                    selectQuery = @"
                        SELECT 
                            e.Ename AS 员工姓名,
                            e.Etitle AS 职务,
                            e.Egrade AS 职称,
                            e.Ehire_date AS 入职日期,
                            d.Dname AS 所在部门,
                            m.baseS AS 基本工资,
                            m.Ade AS 考勤扣款,
                            m.Oall AS 加班津贴,
                            m.grossS AS 应发工资,
                            m.tax AS 个人所得税,
                            m.netS AS 实发工资
                        FROM 
                            msalary m
                        JOIN 
                            emp_info e ON m.Eno = e.Eno
                        JOIN 
                            dept_info d ON e.Dno = d.Dno
                        WHERE 
                            m.Smonth = @Period;  -- 直接用参数 @Period 代替变量
                    ";
                }
                // 年度企业工资报表查询
                else if (isAnnualReport)
                {
                    selectQuery = @"
                        SELECT 
                            d.Dname AS 部门名称,
                            SUM(m.grossS) AS 应发工资总额,
                            SUM(m.tax) AS 个税总额,
                            SUM(m.netS) AS 实发工资总额,
                            SUM(b.Bonus) AS 年终奖总额,
                            COUNT(DISTINCT e.Eno) AS 员工总人数,
                            ROUND(SUM(m.grossS) / COUNT(DISTINCT e.Eno), 2) AS 人均应发工资总额,
                            ROUND(SUM(m.tax) / COUNT(DISTINCT e.Eno), 2) AS 人均个税总额,
                            ROUND(SUM(m.netS) / COUNT(DISTINCT e.Eno), 2) AS 人均实发工资总额,
                            ROUND(SUM(b.Bonus) / COUNT(DISTINCT e.Eno), 2) AS 人均年终奖总额
                        FROM 
                            emp_info e
                        JOIN 
                            dept_info d ON e.Dno = d.Dno
                        JOIN 
                            msalary m ON e.Eno = m.Eno
                        JOIN 
                            bonus b ON e.Eno = b.Eno AND b.Byear = @Period
                        WHERE 
                            m.Smonth LIKE CONCAT(@Period, '-%')
                        GROUP BY 
                            d.Dname
                        ORDER BY 
                            d.Dname;
                    ";
                }

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    MySqlCommand command = new MySqlCommand(selectQuery, connection);
                    command.Parameters.AddWithValue("@Period", period);
                    if (!string.IsNullOrEmpty(identifier) && (isAnnualDept || isAnnualEmp))
                    {
                        command.Parameters.AddWithValue("@Identifier", identifier);
                    }

                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridView1.DataSource = dataTable;
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"加载数据时发生 MySQL 错误：{ex.Message}\n错误码：{ex.Number}", "错误",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载数据时发生错误：{ex.Message}", "错误",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnClose_Click(object? sender, EventArgs e)
        {
            Close();
        }
    }
}