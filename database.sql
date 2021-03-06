USE master
GO
DROP DATABASE IF EXISTS WebMovie
GO
CREATE DATABASE WebMovie
GO
USE WebMovie
GO
CREATE TABLE Account(
	id INT PRIMARY KEY IDENTITY,
	displayName NVARCHAR(100), --tên hiển thị
	image VARCHAR(255),
	userName VARCHAR(100),
	pass VARCHAR(1000),
	email VARCHAR(255),
	role INT -- 0-admin - 1-viewer
)
CREATE TABLE Category(
	id INT PRIMARY KEY IDENTITY,
	name NVARCHAR(30)
)
CREATE TABLE Series(
	id INT PRIMARY KEY IDENTITY,
	name NVARCHAR(100)
)
CREATE TABLE Movie(
	id INT PRIMARY KEY IDENTITY,
	name NVARCHAR(255),
	anotherName NVARCHAR(255),
	image VARCHAR(255),
	releaseYear INT,
	description NVARCHAR(4000),
	duration INT, --thời lượng ? tập
	seriesId INT FOREIGN KEY REFERENCES Series(id) ON DELETE SET NULL,
	part INT, -- thứ tự trong series
	nameInSeries NVARCHAR(255),
	updatedDate DATETIME DEFAULT GETDATE(),
	status INT -- 0-đang tiến hành - 1-hoàn thành
)
CREATE TABLE ViewsByDate(
	id INT PRIMARY KEY IDENTITY,
	movieId INT FOREIGN KEY REFERENCES Movie(id) ON DELETE CASCADE,
	day DATE DEFAULT GETDATE(),
	numView INT
)
CREATE TABLE MovieRate(
	id INT PRIMARY KEY IDENTITY,
	accountId INT FOREIGN KEY REFERENCES Account(id) ON DELETE CASCADE,
	movieId INT FOREIGN KEY REFERENCES Movie(id) ON DELETE CASCADE,
	rateNumber INT -- 1->10
)
CREATE TABLE CategoryForMovies(
	id INT PRIMARY KEY IDENTITY,
	movieId INT FOREIGN KEY REFERENCES Movie(id) ON DELETE CASCADE,
	categoryId INT FOREIGN KEY REFERENCES Category(id) ON DELETE CASCADE
)
CREATE TABLE Episode(
	id INT PRIMARY KEY IDENTITY,
	episodeNumber INT,
	episodeName NVARCHAR(255),
	movieId INT FOREIGN KEY REFERENCES Movie(id) ON DELETE CASCADE,
	video VARCHAR(255)
)
CREATE TABLE Comment(
	id INT PRIMARY KEY IDENTITY,
	movieId INT FOREIGN KEY REFERENCES Movie(id) ON DELETE CASCADE,
	accountId INT FOREIGN KEY REFERENCES Account(id) ON DELETE CASCADE,
	content NVARCHAR(4000),
	commentDate DATETIME DEFAULT GETDATE(),
	fatherComment INT FOREIGN KEY REFERENCES Comment(id)
)
CREATE TABLE Follow(
	id INT PRIMARY KEY IDENTITY,
	accountId INT FOREIGN KEY REFERENCES Account(id) ON DELETE CASCADE,
	movieId INT FOREIGN KEY REFERENCES Movie(id) ON DELETE CASCADE
)
CREATE TABLE History(
	id INT PRIMARY KEY IDENTITY,
	accountId INT FOREIGN KEY REFERENCES Account(id) ON DELETE CASCADE,
	episodeId INT FOREIGN KEY REFERENCES episode(id) ON DELETE CASCADE
)
