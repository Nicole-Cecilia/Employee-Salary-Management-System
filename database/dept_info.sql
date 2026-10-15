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

 Date: 03/07/2025 12:00:36
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for dept_info
-- ----------------------------
DROP TABLE IF EXISTS `dept_info`;
CREATE TABLE `dept_info`  (
  `Dno` int NOT NULL COMMENT '部门编号',
  `Dname` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '部门名称',
  `Eno` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '负责人工号',
  PRIMARY KEY (`Dno`) USING BTREE,
  INDEX `fk_dept_info_emp_info`(`Eno` ASC) USING BTREE,
  CONSTRAINT `fk_dept_info_emp_info` FOREIGN KEY (`Eno`) REFERENCES `emp_info` (`Eno`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of dept_info
-- ----------------------------
INSERT INTO `dept_info` VALUES (101, '研发部', 'E2023001');
INSERT INTO `dept_info` VALUES (102, '产品部', 'E1234567');
INSERT INTO `dept_info` VALUES (103, '运营部', 'E3456789');

SET FOREIGN_KEY_CHECKS = 1;
