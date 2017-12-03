IF NOT EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[dbo].[Regions]'))
BEGIN
	EXEC sp_rename 'dbo.Region', 'Regions'
END

IF NOT EXISTS(SELECT *
          FROM   INFORMATION_SCHEMA.COLUMNS
          WHERE  TABLE_NAME = 'Customers'
                 AND COLUMN_NAME = 'CreatedDate')
BEGIN
	ALTER TABLE [dbo].[Customers]
		ADD [CreatedDate] DATE NULL;
END