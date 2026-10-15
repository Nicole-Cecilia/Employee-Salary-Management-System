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

 Date: 03/07/2025 12:01:31
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for overtime
-- ----------------------------
DROP TABLE IF EXISTS `overtime`;
CREATE TABLE `overtime`  (
  `Eno` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `Omonth` varchar(7) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `wdhour` decimal(5, 2) NULL DEFAULT NULL,
  `wehour` decimal(5, 2) NULL DEFAULT NULL,
  `hlhour` decimal(5, 2) NULL DEFAULT NULL,
  PRIMARY KEY (`Eno`, `Omonth`) USING BTREE,
  CONSTRAINT `fk_overtime_emp_info` FOREIGN KEY (`Eno`) REFERENCES `emp_info` (`Eno`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of overtime
-- ----------------------------

SET FOREIGN_KEY_CHECKS = 1;
