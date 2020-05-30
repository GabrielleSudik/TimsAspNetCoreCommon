CREATE PROCEDURE [dbo].[spFood_All]

AS

BEGIN

	SET NOCOUNT ON;

	SELECT [Id], [Title], [Description], [Price]
	FROM dbo.Food;

END

--Tim reminds us that SELECT * in a stored proc is usually way too much.
--But it's fine in a little demo like this.

--COOL TIP!
--Start with SELECT * FROM dbo.TableName;
--Then right-click the star, refactor, expand all. YOU GET THE FULL LIST.