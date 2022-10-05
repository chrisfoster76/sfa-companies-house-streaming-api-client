﻿CREATE TABLE [dbo].[Officers]
(
	[Timepoint] BIGINT NOT NULL PRIMARY KEY,
	[PublishedAt] DATETIME2 NOT NULL,
	[ResourceUri] VARCHAR(255),
	[ResourceKind] VARCHAR(100), 
	[Type] VARCHAR(100),
    [Data] NVARCHAR(MAX) NULL
)