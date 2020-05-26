CREATE PROCEDURE [dbo].[spOrders_UpdateName]
	@Id int,
	@OrderName nvarchar(50)
AS

BEGIN

	SET NOCOUNT ON;

	UPDATE dbo.[Order]
	SET OrderName = @OrderName
	WHERE Id = @Id;

END
