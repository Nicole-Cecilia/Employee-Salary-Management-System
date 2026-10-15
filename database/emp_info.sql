/*
 Navicat Premium Dump SQL

 Source Server         : localhost_3306
 Source Server Type    : MySQL
 Source Server Version : 80026 (8.0.26)
 Source Host           : localhost:3306
 Source Schema         : employ_salary

 Target Server Type    : MySQL
 Target Server Version : 80026 (8.0.26)
 File Encoding         : 65001

 Date: 03/07/2025 12:01:04
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for emp_info
-- ----------------------------
DROP TABLE IF EXISTS `emp_info`;
CREATE TABLE `emp_info`  (
  `Eno` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '员工编号',
  `Ename` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '姓名',
  `Esex` char(4) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '性别',
  `Ebirth` date NULL DEFAULT NULL,
  `Etitle` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '职务',
  `Egrade` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '职称',
  `Dno` int NOT NULL COMMENT '关联部门表',
  `Ehire_date` date NOT NULL COMMENT '入职时间',
  `Ede` decimal(10, 2) NULL DEFAULT 0.00 COMMENT '每月专项附加扣除',
  PRIMARY KEY (`Eno` DESC) USING BTREE,
  INDEX `fk_emp_info_jobtype_rule`(`Etitle` ASC, `Egrade` ASC) USING BTREE,
  INDEX `fk_emp_info_dept_info`(`Dno` ASC) USING BTREE,
  CONSTRAINT `fk_emp_info_dept_info` FOREIGN KEY (`Dno`) REFERENCES `dept_info` (`Dno`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `fk_emp_info_jobtype_rule` FOREIGN KEY (`Etitle`, `Egrade`) REFERENCES `jobtype_rule` (`Etitle`, `Egrade`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of emp_info
-- ----------------------------
INSERT INTO `emp_info` VALUES ('E7890123', '张磊', '男', '2000-10-25', 'UI设计师', '初级', 102, '2023-01-26', 1000.00);
INSERT INTO `emp_info` VALUES ('E6789012', '周航', '男', '1999-06-03', '产品经理', '初级', 102, '2022-10-11', 2500.00);
INSERT INTO `emp_info` VALUES ('E5678901', '孙悦', '女', '1993-02-25', '软件工程师', '初级', 101, '2023-02-18', 1000.00);
INSERT INTO `emp_info` VALUES ('E4567890', '赵鸣', '男', '1996-12-10', '数据分析师', '高级', 101, '2020-05-15', 3000.00);
INSERT INTO `emp_info` VALUES ('E3456789', '郑瑶', '女', '1994-09-05', '测试工程师', '初级', 103, '2022-06-08', 1500.00);
INSERT INTO `emp_info` VALUES ('E2345678', '吴晨', '男', '1997-07-22', 'UI设计师', '中级', 102, '2021-03-12', 2000.00);
INSERT INTO `emp_info` VALUES ('E2023001', '李华', '男', '1990-05-12', '软件工程师', '中级', 101, '2020-07-01', 1000.00);
INSERT INTO `emp_info` VALUES ('E1234567', '陈雨', '女', '1995-04-18', '产品经理', '高级', 102, '2020-11-20', 4000.00);

SET FOREIGN_KEY_CHECKS = 1;
