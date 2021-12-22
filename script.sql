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
	--director varchar(100),
	--actor varchar(100) NOT NULL,
	releaseYear INT,
	description NVARCHAR(4000),
	duration NVARCHAR(30), --thời lượng
	--num_view int NOT NULL, (dùng table views)
	--author varchar(30) NOT NULL,
	seriesId INT FOREIGN KEY REFERENCES Series(id) ON DELETE SET NULL,
	part INT, -- thứ tự trong series
	nameInSeries NVARCHAR(255),
	status INT -- 0-đang tiến hành - 1-hoàn thành
)
SELECT *FROM Account
SELECT *FROM Movie

SELECT *FROM Category


SELECT *FROM CategoryForMovies

SELECT *FROM Comment
SELECT *FROM Series
SELECT *FROM Episode

SELECT *FROM History

CREATE TABLE ViewsByDate(
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

GO
INSERT [dbo].[Account] ([displayName], [userName], [pass], [email], [role]) VALUES (N'admin', N'admin', N'21232f297a57a5a743894a0e4a801fc3', N'admin@gmail.com', 0)
INSERT [dbo].[Account] ([displayName], [userName], [pass], [email], [role]) VALUES (N'Kiên Trần', N'kien', N'5d2297b2f56654636090aaad75d0578f', N'kientr201@gmail.com', 1)
INSERT [dbo].[Account] ([displayName], [userName], [pass], [email], [role]) VALUES (N'test', N'test', N'202cb962ac59075b964b07152d234b70', N'vip2k18@gmail.com', 0)

INSERT [dbo].[Series] ([name]) VALUES (N'Series Kimetsu no Yaiba')

INSERT [dbo].[Movie] ([name], [anotherName], [image], [releaseYear], [description], [duration], [seriesId], [part], [nameInSeries], [status]) VALUES (N'Kimetsu no Yaiba', N'Thanh Gươm Diệt Quỷ', N'kimetsu-no-yaiba-phan-1.jpg', 2019, N'Từ thời xưa luôn có những truyền thuyết về loài quỷ ăn thịt người rình mò trong các khu rừng khi màn đêm buông xuống. Chính điều này khiến người dân không ai dám vào rừng vào ban đêm. Tuy nhiên, Tanjiro, một cậu trai làm nghề bán củi than sống cùng gia đình trên núi lại không tin vào điều này, cậu quá bận rộn làm nuôi các anh em của mình. Nhưng rồi Tanjiro đã sớm phải tin vào những câu chuyện hảo huyền đó khi hiện thực cay nghiệt đến với cậu...', N'26 Tập', 1, 1, N'Phần 1', 1)

INSERT [dbo].[Category] ([name]) VALUES (N'Hành động')
INSERT [dbo].[Category] ([name]) VALUES (N'Hài hước')
INSERT [dbo].[Category] ([name]) VALUES (N'Tình cảm')
INSERT [dbo].[Category] ([name]) VALUES (N'Trùng sinh')
INSERT [dbo].[Category] ([name]) VALUES (N'Học đường')
INSERT [dbo].[Category] ([name]) VALUES (N'Anime')
INSERT [dbo].[Category] ([name]) VALUES (N'Lịch sử')
INSERT [dbo].[Category] ([name]) VALUES (N'Siêu nhiên')
INSERT [dbo].[Category] ([name]) VALUES (N'Shounen')
INSERT [dbo].[Category] ([name]) VALUES (N'Demon')

INSERT [dbo].[CategoryForMovies] ([movieId], [categoryId]) VALUES (1, 1)
INSERT [dbo].[CategoryForMovies] ([movieId], [categoryId]) VALUES (1, 6)
INSERT [dbo].[CategoryForMovies] ([movieId], [categoryId]) VALUES (1, 7)
INSERT [dbo].[CategoryForMovies] ([movieId], [categoryId]) VALUES (1, 8)
INSERT [dbo].[CategoryForMovies] ([movieId], [categoryId]) VALUES (1, 9)
INSERT [dbo].[CategoryForMovies] ([movieId], [categoryId]) VALUES (1, 10)
