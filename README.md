# 员工工资管理系统 (Employee Salary Management System)

基于 **C# WinForms (.NET 8.0)** + **MySQL** 的桌面端员工工资管理系统，为数据库课程设计项目。

## 功能模块

### 管理员端
- 基础信息管理：部门信息、员工信息、工种薪资规则
- 考勤加班管理：员工请假、月度考勤统计、月度加班统计、月度薪资计算
- 年度查询：部门/员工年度薪资查询、年终奖金生成
- 报表输出：月度企业工资报表、年度企业工资报表
- 系统操作：修改密码、退出登录

### 员工端
- 上班/下班打卡
- 年度薪资查询
- 年终奖金查询
- 修改密码

## 技术栈

- **前端**：C# WinForms (.NET 8.0 Windows)
- **数据库**：MySQL 8.0
- **数据访问**：MySql.Data (Connector/NET 8.0)

## 项目结构

```
├── Esalary.sln                  # Visual Studio 解决方案
├── Esalary/                     # WinForms 项目
│   ├── Esalary.csproj
│   ├── Program.cs               # 程序入口
│   ├── LoginForm.cs             # 登录窗口（管理员/员工）
│   ├── AdminForm.cs             # 管理员主界面
│   ├── EmployeeForm.cs          # 员工主界面
│   ├── DataManagementForm.cs    # 通用数据管理（增删改查）
│   ├── DataEntryForm.cs         # 通用数据录入表单
│   ├── InputForm.cs             # 通用输入弹窗
│   ├── ChangePasswordForm.cs    # 修改密码
│   └── SalaryReportForm.cs      # 薪资报表展示
├── database/                    # MySQL 建表脚本与测试数据
│   ├── admin_login.sql          # 管理员登录表
│   ├── emp_login.sql            # 员工登录表
│   ├── emp_info.sql             # 员工信息表
│   ├── dept_info.sql            # 部门信息表
│   ├── jobtype_rule.sql         # 工种薪资规则表
│   ├── ea1.sql                  # 上班打卡记录表
│   ├── ea2.sql                  # 下班打卡记录表
│   ├── el.sql                   # 请假记录表
│   ├── attendance.sql           # 月度考勤统计表
│   ├── overtime.sql              # 月度加班统计表
│   ├── msalary.sql              # 月度薪资表（含累计个税）
│   ├── bonus.sql                # 年终奖金表
│   └── tyday.sql                # 2025年日历表（工作日/周末/节假日）
└── 数据库课程设计A报告.docx      # 课程设计报告（需手动下载）
```

## 快速开始

### 1. 数据库准备

```sql
-- 创建数据库
CREATE DATABASE employ_salary DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci;
USE employ_salary;

-- 按顺序执行 database/ 目录下的 SQL 文件
-- 注意外键依赖顺序：dept_info → emp_info → jobtype_rule → ...
source database/dept_info.sql;
source database/emp_info.sql;
source database/jobtype_rule.sql;
source database/admin_login.sql;
source database/emp_login.sql;
source database/ea1.sql;
source database/ea2.sql;
source database/el.sql;
source database/attendance.sql;
source database/overtime.sql;
source database/msalary.sql;
source database/bonus.sql;
source database/tyday.sql;
```

### 2. 修改数据库连接

在所有 `.cs` 文件中搜索连接字符串，将 `YOUR_PASSWORD` 替换为你的 MySQL root 密码：

```csharp
private string connectionString = "Server=localhost;Database=employ_salary;Uid=root;Pwd=YOUR_PASSWORD;";
```

### 3. 运行

使用 Visual Studio 2022 打开 `Esalary.sln`，还原 NuGet 包后运行。

## 默认账号

| 角色 | 用户名 | 密码 |
|------|--------|------|
| 管理员 | admin | 666 |
| 管理员 | wym | 123456 |
| 员工 | E1234567 | 123456 |
| 员工 | E2023001 | 123 |

## 薪资计算规则

- **基本工资**：按工种+等级查 `jobtype_rule.baseS`
- **加班津贴**：平日 1.5 倍、周末 2 倍、节假日 3 倍时薪
- **考勤扣款**：迟到/早退 10 分钟内每次扣 10 元，10-30 分钟每次扣 30 元；旷工扣双倍日工资
- **个人所得税**：按累计预扣法计算（起征点 5000/月，含专项附加扣除）
- **年终奖金**：年度累计应发工资 / 12

## 课程设计报告

`数据库课程设计A报告.docx` 为 Word 二进制文件，未包含在 Git 仓库中，可向作者索取。

## License

仅用于学习交流。