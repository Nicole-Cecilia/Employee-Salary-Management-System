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

 Date: 03/07/2025 12:01:17
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for jobtype_rule
-- ----------------------------
DROP TABLE IF EXISTS `jobtype_rule`;
CREATE TABLE `jobtype_rule`  (
  `Etitle` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '工种',
  `Egrade` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '等级',
  `baseS` decimal(10, 2) NOT NULL COMMENT '基本工资',
  PRIMARY KEY (`Etitle`, `Egrade`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of jobtype_rule
-- ----------------------------
INSERT INTO `jobtype_rule` VALUES ('UI设计师', '中级', 15000.00);
INSERT INTO `jobtype_rule` VALUES ('UI设计师', '初级', 10000.00);
INSERT INTO `jobtype_rule` VALUES ('UI设计师', '高级', 22000.00);
INSERT INTO `jobtype_rule` VALUES ('产品经理', '中级', 18000.00);
INSERT INTO `jobtype_rule` VALUES ('产品经理', '初级', 10000.00);
INSERT INTO `jobtype_rule` VALUES ('产品经理', '高级', 30000.00);
INSERT INTO `jobtype_rule` VALUES ('数据分析师', '中级', 15000.00);
INSERT INTO `jobtype_rule` VALUES ('数据分析师', '初级', 10000.00);
INSERT INTO `jobtype_rule` VALUES ('数据分析师', '高级', 22000.00);
INSERT INTO `jobtype_rule` VALUES ('测试工程师', '中级', 12000.00);
INSERT INTO `jobtype_rule` VALUES ('测试工程师', '初级', 8000.00);
INSERT INTO `jobtype_rule` VALUES ('测试工程师', '高级', 18000.00);
INSERT INTO `jobtype_rule` VALUES ('软件工程师', '中级', 20000.00);
INSERT INTO `jobtype_rule` VALUES ('软件工程师', '初级', 12000.00);
INSERT INTO `jobtype_rule` VALUES ('软件工程师', '高级', 30000.00);

SET FOREIGN_KEY_CHECKS = 1;
