CREATE PROCEDURE [dbo].[spOrders_GetById]
	@Id int
AS

BEGIN
	
	SET NOCOUNT ON;

	SELECT [Id], [OrderName], [OrderDate], [FoodId], [Quantity], [Total]
	FROM dbo.[Order]
	WHERE Id = @Id;

END

--Don't forget to * then Expand Wildcard.