CREATE DATABASE TESTDB;
GO

USE TESTDB;
GO

CREATE TABLE TableOne (
	[UserID] [uniqueidentifier] NOT NULL PRIMARY KEY,
	[Note] [varchar](200) NOT NULL
);
GO

INSERT INTO TableOne VALUES (NEWID(), 'Hello World 1');
INSERT INTO TableOne VALUES (NEWID(), 'Hello World 2');
INSERT INTO TableOne VALUES (NEWID(), 'Hello World 3');
INSERT INTO TableOne VALUES (NEWID(), 'Hello World 4');
INSERT INTO TableOne VALUES (NEWID(), 'Hello World 5');
INSERT INTO TableOne VALUES (NEWID(), 'Hello World 6');
INSERT INTO TableOne VALUES (NEWID(), 'Hello World 7');
INSERT INTO TableOne VALUES (NEWID(), 'Hello World 8');
INSERT INTO TableOne VALUES (NEWID(), 'Hello World 9');
GO

CREATE TABLE TableTwo (
	[UserID] [uniqueidentifier] NOT NULL PRIMARY KEY,
	[Note] [varchar](200) NOT NULL
);
GO

INSERT INTO TableTwo VALUES(NEWID(), 'Hello World 1');
INSERT INTO TableTwo VALUES(NEWID(), 'Hello World 2');
INSERT INTO TableTwo VALUES(NEWID(), 'Hello World 3');
INSERT INTO TableTwo VALUES(NEWID(), 'Hello World 4');
INSERT INTO TableTwo VALUES(NEWID(), 'Hello World 5');
INSERT INTO TableTwo VALUES(NEWID(), 'Hello World 6');
INSERT INTO TableTwo VALUES(NEWID(), 'Hello World 7');
INSERT INTO TableTwo VALUES(NEWID(), 'Hello World 8');
INSERT INTO TableTwo VALUES(NEWID(), 'Hello World 9');
GO