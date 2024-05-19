CREATE DATABASE BoardGames
USE BoardGames
--
DROP TABLE GoLGameState
DROP TABLE OwnedAbilities
DROP TABLE Abilities
DROP TABLE GoLPawns
DROP TABLE GolGameRooms
DROP TABLE Cards
DROP TABLE Tiles
DROP TABLE CardSets
DROP TABLE SiBGameState
DROP TABLE SiBPawns
DROP TABLE SiBGameRooms
DROP TABLE PlayersAchievements
DROP TABLE Achievements
DROP TABLE Players
--


CREATE TABLE Players(
PlayerId INT,
PlayerName VARCHAR(100),

CONSTRAINT PK_Players PRIMARY KEY(PlayerId)
)

-- achievements
CREATE TABLE Achievements(
AchievementId INT IDENTITY(1,1),
AchievementGameId INT NOT NULL,
AchievementTitle VARCHAR(100) NOT NULL,
AchievementDescr VARCHAR(150) NOT NULL,

CONSTRAINT PK_Achievements PRIMARY KEY(AchievementId),
CONSTRAINT UK_Achievements_AchievementTitle UNIQUE(AchievementTitle)
)

CREATE TABLE PlayersAchievements(
PlAchId INT IDENTITY(1,1),
PlayerId INT NOT NULL,
AchievementId INT NOT NULL,

CONSTRAINT PK_PlayersAchievements PRIMARY KEY(PlAchId),
CONSTRAINT FK_PlayersAchievements_Players FOREIGN KEY(PlayerId) REFERENCES Players(PlayerId) ON DELETE CASCADE,
CONSTRAINT FK_PlayersAchievements_Achievements FOREIGN KEY(AchievementId) REFERENCES Achievements(AchievementId) ON DELETE CASCADE
)


-- sib
-- for hosting/joining
CREATE TABLE SiBGameRooms(
SiBGameRoomId INT IDENTITY(1,1),
SiBCode VARCHAR(50) NOT NULL,
SiBWaitingPlayersCount INT NOT NULL,
SiBSelectedPlayersCount INT NOT NULL,

CONSTRAINT PK_SiBGameRoom PRIMARY KEY(SiBGameRoomId)

)

CREATE TABLE SiBPawns(
SiBPawnId INT IDENTITY(1,1),
GridRowIndex INT NOT NULL,
GridColumnIndex INT NOT NULL,
PlayerId INT NOT NULL,

CONSTRAINT PK_SiBPawns PRIMARY KEY(SiBPawnId),
CONSTRAINT FK_SiBPawns_Players FOREIGN KEY(PlayerId) REFERENCES Players(PlayerId) ON DELETE CASCADE
)

-- update log by id
CREATE TABLE SiBGameState(
SiBGameStateId INT IDENTITY(1,1),
SiBGameRoomId INT NOT NULL,
SiBPawnId INT NOT NULL,
CurrentPlayer INT NOT NULL REFERENCES Players(PlayerId),


CONSTRAINT FK_SiBGameState_SiBGameRooms FOREIGN KEY(SiBGameRoomId) REFERENCES SiBGameRooms(SiBGameRoomId) ON DELETE CASCADE, 
CONSTRAINT FK_SiBGameState_Players FOREIGN KEY(SiBPawnId) REFERENCES SiBPawns(SiBPawnId) ON DELETE CASCADE,
CONSTRAINT UK_Pawn_Room UNIQUE(SiBGameRoomId, SiBPawnId)
)




-- GoL
CREATE TABLE CardSets(
CardSetsId INT,
CardSetName VARCHAR(50) NOT NULL,

CONSTRAINT PK_CardSets PRIMARY KEY(CardSetsId),
CONSTRAINT UK_CardSets_CardSetName UNIQUE(CardSetName)
)

INSERT INTO CardSets VALUES (0, 'Health'), (1, 'Career'), (2, 'Social')

CREATE TABLE Tiles(
TileId INT IDENTITY(1,1),
TileColor INT NOT NULL,
CentreXPosition FLOAT,
CentreYPosition FLOAT,

CONSTRAINT PK_Tiles PRIMARY KEY(TileId),
CONSTRAINT UK_Tiles_TileColor UNIQUE(TileColor),
CONSTRAINT FK_CardSets FOREIGN KEY(TileColor) REFERENCES CardSets(CardSetsId) ON DELETE CASCADE
)

CREATE TABLE Cards(
CardId INT IDENTITY(1,1),
CardType INT NOT NULL,
PathToTexture VARCHAR(150) NOT NULL,
CardEffect VARCHAR(150) NOT NULL,
IsConsideredBad Tinyint NOT NULL,

CONSTRAINT PK_Cards PRIMARY KEY(CardId),
CONSTRAINT FK_Cards_CardSets FOREIGN KEY(CardType) REFERENCES CardSets(CardSetsId) ON DELETE CASCADE
)



CREATE TABLE GolGameRooms(
GoLGameRoomId INT IDENTITY(1,1),
GoLCode VARCHAR(50) NOT NULL,
GoLWaitingPlayersCount INT NOT NULL,
GoLSelectedPlayersCount INT NOT NULL,

CONSTRAINT PK_GoLGameRooms PRIMARY KEY(GoLGameRoomId)

)

-- custom?
CREATE TABLE GoLPawns(
GoLPawnId INT IDENTITY(1,1),
CenterXPosition FLOAT NOT NULL,
CenterYPosition FLOAT NOT NULL,
TileId INT NOT NULL,
PlayerId INT NOT NULL,


CONSTRAINT PK_GoLPawns PRIMARY KEY(GoLPawnId),
CONSTRAINT FL_GoLPawns_Tiles FOREIGN KEY(TileId) REFERENCES Tiles(TileId) ON DELETE CASCADE,
CONSTRAINT FK_GoLPawns_Players FOREIGN KEY(PlayerId) REFERENCES Players(PlayerId) ON DELETE CASCADE
)

CREATE TABLE Abilities(
AbilityId INT IDENTITY(1,1),
AbilityName VARCHAR(50) NOT NULL,
AbilityDescr VARCHAR(150) NOT NULL,

CONSTRAINT PK_Abilities PRIMARY KEY(AbilityId),
CONSTRAINT UK_Abilities_AbilityName UNIQUE(AbilityName)
)


CREATE TABLE OwnedAbilities(
OwnedAbId INT PRIMARY KEY IDENTITY(1,1),
PlayerId INT NOT NULL,
AbilityId INT NOT NULL,
CountOwned INT DEFAULT 0,

CONSTRAINT FK_OwnedAbilities_Players FOREIGN KEY(PlayerId) REFERENCES Players(PlayerId) ON DELETE CASCADE,
CONSTRAINT FK_OwnedAbilities_Abilities FOREIGN KEY(AbilityId) REFERENCES Abilities(AbilityId) ON DELETE CASCADE,
UNIQUE(PlayerId, AbilityId)

)

CREATE TABLE GoLGameState(
GoLGameStateId INT IDENTITY(1,1) PRIMARY KEY,
GoLGameRoomId INT NOT NULL REFERENCES GoLGameRooms(GoLGameRoomId),
GoLPawnId INT NOT NULL REFERENCES GoLPawns(GoLPawnId),
Happiness INT NOT NULL,
Money INT NOT NULL,
CurrentPlayer INT NOT NULL REFERENCES Players(PlayerId),
OwnedAbId INT NOT NULL REFERENCES OwnedAbilities(OwnedAbId),
CardId INT NOT NULL REFERENCES Cards(CardId),
UNIQUE(GoLGameRoomId, GoLPawnId, OwnedAbId)
)



