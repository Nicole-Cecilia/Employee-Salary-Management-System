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

 Date: 03/07/2025 12:00:26
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for bonus
-- ----------------------------
DROP TABLE IF EXISTS `bonus`;
CREATE TABLE `bonus`  (
  `Eno` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '员工工号',
  `Byear` year NULL DEFAULT NULL COMMENT '年度',
  `aaS` decimal(10, 2) NULL DEFAULT NULL COMMENT '年度累计收入',
  `Bonus` decimal(10, 2) NULL DEFAULT NULL COMMENT '年终奖金额',
  PRIMARY KEY (`Eno`) USING BTREE,
  CONSTRAINT `fk_bonus_emp_info` FOREIGN KEY (`Eno`) REFERENCES `emp_info` (`Eno`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of bonus
-- ----------------------------

SET FOREIGN_KEY_CHECKS = 1;
