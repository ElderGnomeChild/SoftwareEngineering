DROP TABLE IF EXISTS Teacher;
DROP TABLE IF EXISTS Student;
DROP TABLE IF EXISTS Game;
DROP TABLE IF EXISTS PlayGame;


CREATE TABLE Teacher (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    fname VARCHAR(32) NOT NULL,
    lname VARCHAR(32) NOT NULL,
    password VARCHAR(32)
);

CREATE TABLE Student ( 
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    fname VARCHAR(32) NOT NULL,
    lname VARCHAR(32) NOT NULL,
    birthdate DATE NOT NULL,
    total_xp INTEGER NOT NULL,
    teacher_id INTEGER,

    CONSTRAINT fk_column FOREIGN KEY (teacher_id) REFERENCES Teacher (id)
);

CREATE TABLE Game (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    points_possible INTEGER NOT NULL,
    name VARCHAR(32) NOT NULL,
    type VARCHAR(3) NOT NULL
);

CREATE TABLE PlayGame (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    student_id INTEGER NOT NULL,
    game_id INTEGER NOT NULL,
    points_scored INTEGER NOT NULL,

    CONSTRAINT fk_column FOREIGN KEY (student_id) REFERENCES Student (id),
    CONSTRAINT fk_column FOREIGN KEY (game_id) REFERENCES Game (id)
);

-- FILLER TO TEST DATABASE

INSERT INTO Teacher (fname, lname, password) VALUES 
    ("Jeffrey", "TeacherBoy", "teachersRcool123"),
    ("Vincent", "Adultman", "notAchild"),
    ("Kathy", "Lenth", "password"),
    ("Helen", "Hu", "homework")
;

INSERT INTO Student (fname, lname, birthdate, total_xp, teacher_id) VALUES
    ("Tom", "Sime", "2011-08-25", 0, 1),
    ("Chase", "Gladden", "2012-11-20", 0, 2),
    ("Lindsey", "Flower", "2010-02-11", 0, 3),
    ("Stephen", "Pants", "2014-03-15", 0, 4),
    ("Jeffrey", "Bureau", "2012-05-01", 0, 1),
    ("Dalton", "Croissant", "2016-02-05", 0, 2),
    ("Ben", "Smith", "2009-12-02", 0, 3),
    ("Jenna", "Burgquist", "2008-03-12", 0, 4),
    ("Jamie", "Sullivan", "2010-07-23", 0, 1),
    ("Sarah", "Johnson", "2011-06-21", 0, 2),
    ("Ian", "Hopper", "2010-10-31", 0, 3),
    ("Daisy", "Smith", "2011-01-31", 0, 4)
;

INSERT INTO Game (points_possible, name, type) VALUES
    (100, "Level 1", "add"),
    (100, "Level 2", "sub"),
    (100, "Level 3", "plc"),
    (100, "Level 4", "a/s"),
    (100, "Level 5", "add"),
    (100, "Level 6", "sub"),
    (100, "Level 7", "plc"),
    (100, "Level 8", "add"),
    (100, "Level 9", "sub"),
    (100, "Level 10", "a/s")
;

INSERT INTO PlayGame (student_id, game_id, points_scored) VALUES 
    (1, 1, 100),
    (1, 2, 99),
    (1, 3, 25),
    (1, 4, 84),
    (1, 5, 100),
    (1, 6, 95),
    (1, 7, 65),
    (1, 8, 81),
    (1, 9, 85),
    (1, 10, 73),
    (2, 1, 45),
    (2, 2, 84),
    (2, 3, 85),
    (2, 4, 98),
    (3, 1, 18),
    (4, 1, 99),
    (4, 2, 98),
    (4, 3, 97),
    (5, 1, 23),
    (6, 1, 35),
    (5, 2, 95),
    (5, 3, 53),
    (6, 2, 42),
    (6, 3, 51),
    (6, 4, 62),
    (7, 1, 100),
    (7, 2, 95),
    (7, 3, 81),
    (8, 1, 72),
    (8, 2, 68),
    (8, 3, 56),
    (9, 1, 44),
    (9, 2, 99),
    (9, 3, 51),
    (10, 1, 74),
    (10, 2, 99),
    (10, 3, 100)
;