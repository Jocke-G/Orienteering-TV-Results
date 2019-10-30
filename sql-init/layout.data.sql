USE `layout`;

INSERT INTO `layout`(`name`)
VALUES ("TV1"),
("TV2"),
("TV3"),
("TV4"),
("TV5"),
("TV6"),
("TV7"),
("TV8"),
("TV9"),
("TV10"),
("TV11"),
("TV12");

INSERT INTO `layout_row`(`layout_id`, `ordinal`)
VALUES (1, 1),
(1, 2),
(2, 1),
(2, 2),
(3, 1),
(3, 2),
(4, 1),
(4, 2),
(5, 1),
(5, 2),
(6, 1),
(6, 2),
(7, 1),
(7, 2),
(8, 1),
(8, 2),
(9, 1),
(9, 2),
(10, 1),
(10, 2),
(11, 1),
(11, 2),
(12, 1),
(12, 2);

INSERT INTO `layout_cell`(`row_id`, `ordinal`, `cell_type`, `class_name`)
VALUES (1, 1, "Class", "D10"),
(1, 2, "Class", "D12"),
(2, 1, "Class", "H10"),
(2, 2, "Class", "H12"),
(3, 1, "Finish", NULL),
(4, 1, "Class", "D16"),
(4, 2, "Class", "H16");
