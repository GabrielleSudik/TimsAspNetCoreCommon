/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

--This is a bit loose of a way to do it,
--but fine for our tutorial.

IF NOT EXISTS (SELECT * FROM dbo.Food)
BEGIN
    INSERT INTO dbo.Food(Title, [Description], Price)
    VALUES ('Bento Box', 'Eel, seaweed salad, rice, tempura', 9.95),
    ('Sushi Plate', '8 pieces - Chef choice', 12.95),
    ('Sashimi Plate', 'Salmon, yellowtail, tuna', 15.95);
END
GO
