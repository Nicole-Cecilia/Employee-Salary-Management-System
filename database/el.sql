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

 Date: 03/07/2025 12:00:58
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for el
-- ----------------------------
DROP TABLE IF EXISTS `el`;
CREATE TABLE `el`  (
  `Eno` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '工号',
  `Ldate` date NOT NULL COMMENT '请假日期',
  PRIMARY KEY (`Eno`, `Ldate`) USING BTREE,
  CONSTRAINT `fk_el_emp_info` FOREIGN KEY (`Eno`) REFERENCES `emp_info` (`Eno`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of el
-- ----------------------------
INSERT INTO `el` VALUES ('E2345678', '2025-05-06');

SET FOREIGN_KEY_CHECKS = 1;
