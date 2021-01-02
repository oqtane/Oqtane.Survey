/*  
Create OqtaneSurvey table
*/

CREATE TABLE [dbo].[OqtaneSurvey](
	[SurveyId] [int] IDENTITY(1,1) NOT NULL,
	[ModuleId] [int] NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](256) NOT NULL,
	[ModifiedOn] [datetime] NOT NULL,
  CONSTRAINT [PK_OqtaneSurvey] PRIMARY KEY CLUSTERED 
  (
	[SurveyId] ASC
  )
)
GO

/*  
Create foreign key relationships
*/
ALTER TABLE [dbo].[OqtaneSurvey] WITH CHECK ADD CONSTRAINT [FK_OqtaneSurvey_Module] FOREIGN KEY([ModuleId])
REFERENCES [dbo].Module ([ModuleId])
ON DELETE CASCADE
GO