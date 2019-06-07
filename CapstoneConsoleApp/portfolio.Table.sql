CREATE TABLE [dbo].[Table]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [symbol] NCHAR(10) NOT NULL, 
    [price] NCHAR(10) NOT NULL, 
    [change] NCHAR(10) NOT NULL, 
    [pchange] NCHAR(10) NOT NULL, 
    [currency] NCHAR(10) NOT NULL, 
    [scrapeTime] NCHAR(10) NOT NULL DEFAULT (getdate()), 
    [volume] NCHAR(10) NOT NULL, 
    [marketCap] NCHAR(10) NOT NULL
)
