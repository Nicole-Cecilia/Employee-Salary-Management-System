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

 Date: 03/07/2025 12:00:19
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for attendance
-- ----------------------------
DROP TABLE IF EXISTS `attendance`;
CREATE TABLE `attendance`  (
  `Eno` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `Amonth` varchar(7) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `late` tinyint NULL DEFAULT NULL,
  `early` tinyint NULL DEFAULT NULL,
  `absent` tinyint NULL DEFAULT NULL,
  PRIMARY KEY (`Eno`, `Amonth`) USING BTREE,
  CONSTRAINT `fk_attendance_emp_info` FOREIGN KEY (`Eno`) REFERENCES `emp_info` (`Eno`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of attendance
-- ----------------------------

SET FOREIGN_KEY_CHECKS = 1;
