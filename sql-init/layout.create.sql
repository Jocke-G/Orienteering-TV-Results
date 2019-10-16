CREATE DATABASE `layout`;
CREATE USER 'layout-service'@'%' IDENTIFIED BY 'secret';
GRANT CREATE,ALTER,SELECT,INSERT,UPDATE ON layout.* TO 'layout-service'@'%';

USE `layout`;
CREATE TABLE `layout` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(50) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `layout_row` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `layout_id` int(11) DEFAULT NULL,
  `ordinal` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `IX_LAYOUT_ID_ORDINAL` (`layout_id`,`ordinal`),
  CONSTRAINT `FK_ROW_LAYOUT` FOREIGN KEY (`layout_id`) REFERENCES `layout` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `layout_cell` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `row_id` int(11) DEFAULT NULL,
  `ordinal` int(11) NOT NULL,
  `class_name` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `IX_CELL_ROW_ORDINAL` (`row_id`,`ordinal`),
  CONSTRAINT `FK_CELL_ROW` FOREIGN KEY (`row_id`) REFERENCES `layout_row` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;