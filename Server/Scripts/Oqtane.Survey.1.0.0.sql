/*  
Create OqtaneSurvey tables
*/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OqtaneSurvey]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[OqtaneSurvey](
	[SurveyId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[ModuleId] [int] NOT NULL,
	[SurveyName] [nvarchar](256) NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](256) NOT NULL,
	[ModifiedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_OqtaneSurvey] PRIMARY KEY CLUSTERED 
(
	[SurveyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OqtaneSurveyAnswer]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[OqtaneSurveyAnswer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SurveyItemId] [int] NOT NULL,
	[AnswerValue] [nvarchar](500) NULL,
	[AnswerValueDateTime] [datetime] NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_OqtaneSurveyAnswer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OqtaneSurveyItem]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[OqtaneSurveyItem](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Survey] [int] NOT NULL,
	[ItemLabel] [nvarchar](50) NOT NULL,
	[ItemType] [nvarchar](50) NOT NULL,
	[ItemValue] [nvarchar](50) NULL,
	[Position] [int] NOT NULL,
	[Required] [int] NOT NULL,
	[SurveyChoiceId] [int] NULL,
 CONSTRAINT [PK_OqtaneSurveyItem] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OqtaneSurveyItemOption]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[OqtaneSurveyItemOption](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SurveyItem] [int] NOT NULL,
	[OptionLabel] [nvarchar](500) NOT NULL,
 CONSTRAINT [PK_OqtaneSurveyItemOption] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_OqtaneSurvey_Module]') AND parent_object_id = OBJECT_ID(N'[dbo].[OqtaneSurvey]'))
ALTER TABLE [dbo].[OqtaneSurvey]  WITH CHECK ADD  CONSTRAINT [FK_OqtaneSurvey_Module] FOREIGN KEY([ModuleId])
REFERENCES [dbo].[Module] ([ModuleId])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_OqtaneSurvey_Module]') AND parent_object_id = OBJECT_ID(N'[dbo].[OqtaneSurvey]'))
ALTER TABLE [dbo].[OqtaneSurvey] CHECK CONSTRAINT [FK_OqtaneSurvey_Module]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_OqtaneSurvey_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[OqtaneSurvey]'))
ALTER TABLE [dbo].[OqtaneSurvey]  WITH CHECK ADD  CONSTRAINT [FK_OqtaneSurvey_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_OqtaneSurvey_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[OqtaneSurvey]'))
ALTER TABLE [dbo].[OqtaneSurvey] CHECK CONSTRAINT [FK_OqtaneSurvey_User]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_OqtaneSurveyAnswer_SurveyItem]') AND parent_object_id = OBJECT_ID(N'[dbo].[OqtaneSurveyAnswer]'))
ALTER TABLE [dbo].[OqtaneSurveyAnswer]  WITH CHECK ADD  CONSTRAINT [FK_OqtaneSurveyAnswer_SurveyItem] FOREIGN KEY([SurveyItemId])
REFERENCES [dbo].[OqtaneSurveyItem] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_OqtaneSurveyAnswer_SurveyItem]') AND parent_object_id = OBJECT_ID(N'[dbo].[OqtaneSurveyAnswer]'))
ALTER TABLE [dbo].[OqtaneSurveyAnswer] CHECK CONSTRAINT [FK_OqtaneSurveyAnswer_SurveyItem]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_OqtaneSurveyItem_OqtaneSurvey]') AND parent_object_id = OBJECT_ID(N'[dbo].[OqtaneSurveyItem]'))
ALTER TABLE [dbo].[OqtaneSurveyItem]  WITH CHECK ADD  CONSTRAINT [FK_OqtaneSurveyItem_OqtaneSurvey] FOREIGN KEY([Survey])
REFERENCES [dbo].[OqtaneSurvey] ([SurveyId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_OqtaneSurveyItem_OqtaneSurvey]') AND parent_object_id = OBJECT_ID(N'[dbo].[OqtaneSurveyItem]'))
ALTER TABLE [dbo].[OqtaneSurveyItem] CHECK CONSTRAINT [FK_OqtaneSurveyItem_OqtaneSurvey]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_OqtaneSurveyItemOption_SurveyItem]') AND parent_object_id = OBJECT_ID(N'[dbo].[OqtaneSurveyItemOption]'))
ALTER TABLE [dbo].[OqtaneSurveyItemOption]  WITH CHECK ADD  CONSTRAINT [FK_OqtaneSurveyItemOption_SurveyItem] FOREIGN KEY([SurveyItem])
REFERENCES [dbo].[OqtaneSurveyItem] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_OqtaneSurveyItemOption_SurveyItem]') AND parent_object_id = OBJECT_ID(N'[dbo].[OqtaneSurveyItemOption]'))
ALTER TABLE [dbo].[OqtaneSurveyItemOption] CHECK CONSTRAINT [FK_OqtaneSurveyItemOption_SurveyItem]
GO
