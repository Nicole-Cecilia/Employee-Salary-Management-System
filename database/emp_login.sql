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

 Date: 03/07/2025 12:01:11
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for emp_login
-- ----------------------------
DROP TABLE IF EXISTS `emp_login`;
CREATE TABLE `emp_login`  (
  `Eno` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `Epass` char(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT '123456',
  PRIMARY KEY (`Eno`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of emp_login
-- ----------------------------
INSERT INTO `emp_login` VALUES ('E1234567', '123456');
INSERT INTO `emp_login` VALUES ('E2023001', '123');
INSERT INTO `emp_login` VALUES ('E2345678', '123456');
INSERT INTO `emp_login` VALUES ('E3456789', '123456');
INSERT INTO `emp_login` VALUES ('E4567890', '123456');
INSERT INTO `emp_login` VALUES ('E5678901', '123456');
INSERT INTO `emp_login` VALUES ('E6789012', '123456');
INSERT INTO `emp_login` VALUES ('E7890123', '123456');

SET FOREIGN_KEY_CHECKS = 1;
