CREATE DATABASE MeetingRoomsDB  
ON                
(
  NAME = 'MeetingRoomsDB',       
  FILENAME = 'D:\prog\MeetingRoomsDB\MeetingRoomsDB.mdf',    
  SIZE = 10MB,                   
  MAXSIZE = 50MB,        
  FILEGROWTH = 10MB        
)
LOG ON              
( 
  NAME = 'LogMeetingRoomsDB',           
  FILENAME = 'D:\prog\MeetingRoomsDB\LogMeetingRoomsDB.ldf',       
  SIZE = 5MB,                       
  MAXSIZE = 25MB,                   
  FILEGROWTH = 5MB                   
)               
COLLATE Cyrillic_General_CI_AS 

USE MeetingRoomsDB
GO

CREATE TABLE MeetingRooms
(
	[Id] INT IDENTITY NOT NULL,
	[Title] VARCHAR(MAX) NOT NULL,
	[Location] VARCHAR(MAX) NOT NULL
)
GO

CREATE TABLE Reservations
(
	[Id] INT IDENTITY NOT NULL,
	[RoomId] INT NOT NULL,
	[StartTime] TIME NOT NULL,
	[EndTime] TIME NOT NULL
)
GO

ALTER TABLE MeetingRooms
ADD CONSTRAINT PK_MeetingRoomsId
PRIMARY KEY (Id)
GO

ALTER TABLE Reservation
ADD CONSTRAINT PK_ReservationId
PRIMARY KEY (Id)
GO

ALTER TABLE Reservation
ADD CONSTRAINT FK_ReservationRoomId
FOREIGN KEY (RoomId) REFERENCES MeetingRooms(Id);
GO

INSERT INTO MeetingRooms             
(Title,[Location])
VALUES
('Small room','2 floor door ¹203');
GO
INSERT INTO MeetingRooms             
(Title,[Location])
VALUES
('Midd room','2 floor door ¹204');
GO
INSERT INTO MeetingRooms             
(Title,[Location])
VALUES
('Small room','3 floor door ¹303');
GO
INSERT INTO MeetingRooms             
(Title,[Location])
VALUES
('Huge room','3 floor door ¹305');
GO
INSERT INTO MeetingRooms             
(Title,[Location])
VALUES
('Big room','4 floor door ¹407');
GO

SELECT * FROM MeetingRooms

INSERT INTO Reservations            
(RoomId,StartTime,EndTime)
VALUES
('1','19:00','19:45');
GO

INSERT INTO Reservations             
(RoomId,StartTime,EndTime)
VALUES
('1','19:50','20:20');
GO

INSERT INTO Reservations             
(RoomId,StartTime,EndTime)
VALUES
('2','19:00','19:45');
GO


	