/*  
  Make [dbo].[OqtaneSurvey].[UserId] INTEGER NULL
*/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OqtaneSurvey]') AND type in (N'U'))
BEGIN

ALTER TABLE [dbo].[OqtaneSurvey] ALTER COLUMN [UserId] INTEGER NULL

END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OqtaneSurveyAnswer]') AND type in (N'U'))
BEGIN

ALTER TABLE [dbo].[OqtaneSurveyAnswer] ALTER COLUMN [UserId] INTEGER NULL

END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[OqtaneSurveyAnswer]') AND name = 'AnonymousCookie')
BEGIN

ALTER TABLE [dbo].[OqtaneSurveyAnswer] ADD [AnonymousCookie] nvarchar(500) NULL

END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO