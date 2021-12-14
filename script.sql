CREATE DATABASE WebMovie1
GO
USE WebMovie1
GO
CREATE TABLE Account(
	id INT PRIMARY KEY IDENTITY,
	displayName NVARCHAR(100), --tên hiển thị
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
	--director varchar(100),
	--actor varchar(100) NOT NULL,
	releaseYear INT,
	description NVARCHAR(4000),
	duration NVARCHAR(30), --thời lượng
	--num_view int NOT NULL, (dùng table views)
	--author varchar(30) NOT NULL,
	seriesId INT FOREIGN KEY REFERENCES Series(id) ON DELETE SET NULL,
	part INT,
	status INT, -- 0-đang tiến hành - 1-hoàn thành
)
CREATE TABLE viewsByDate(
	id INT PRIMARY KEY IDENTITY,
	movieId INT FOREIGN KEY REFERENCES Movie(id) ON DELETE CASCADE,
	day DATE,
	numView INT
)
CREATE TABLE MovieRate(
	id INT PRIMARY KEY IDENTITY,
	accountId INT FOREIGN KEY REFERENCES Account(id) ON DELETE CASCADE,
	movieId INT FOREIGN KEY REFERENCES Movie(id) ON DELETE CASCADE,
	rateNumber INT -- 1->10
)
CREATE TABLE CategoryDetails(
	id INT PRIMARY KEY IDENTITY,
	movieId INT FOREIGN KEY REFERENCES Movie(id) ON DELETE CASCADE,
	categoryId INT FOREIGN KEY REFERENCES Category(id) ON DELETE CASCADE
)
CREATE TABLE episode(
	id INT PRIMARY KEY IDENTITY,
	episodeNumber INT,
	episodeName varchar(20),
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
CREATE TABLE follow(
	id INT PRIMARY KEY IDENTITY,
	accountId INT FOREIGN KEY REFERENCES Account(id) ON DELETE CASCADE,
	movieId INT FOREIGN KEY REFERENCES Movie(id) ON DELETE CASCADE
)
CREATE TABLE history(
	id INT PRIMARY KEY IDENTITY,
	accountId INT FOREIGN KEY REFERENCES Account(id) ON DELETE CASCADE,
	episodeId INT FOREIGN KEY REFERENCES episode(id) ON DELETE CASCADE
)

GO
INSERT [dbo].[Account] ([displayName], [userName], [pass], [email], [role]) VALUES (N'admin', N'admin', N'21232f297a57a5a743894a0e4a801fc3', N'admin@gmail.com', 0)
INSERT [dbo].[Account] ([displayName], [userName], [pass], [email], [role]) VALUES (N'Kiên Trần', N'kien', N'5d2297b2f56654636090aaad75d0578f', N'kientr201@gmail.com', 1)
INSERT [dbo].[Account] ([displayName], [userName], [pass], [email], [role]) VALUES (N'test', N'test', N'202cb962ac59075b964b07152d234b70', N'vip2k18@gmail.com', 0)