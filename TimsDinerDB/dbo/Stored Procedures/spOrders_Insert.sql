CREATE PROCEDURE [dbo].[spOrders_Insert]
	@OrderName nvarchar(50),
	@OrderDate datetime2(7),
	@FoodId int,
	@Quantity int,
	@Total money,
	@Id int output --output = returns a variable!
AS

BEGIN

	SET NOCOUNT ON;

	INSERT INTO dbo.[Order] (OrderName, OrderDate, FoodId, Quantity, Total)
	VALUES (@OrderName, @OrderDate, @FoodId, @Quantity, @Total);

	SET @Id = SCOPE_IDENTITY();

END

--tip for insert statements:
--name your variables the same as the columns, then copypaste and add @s

--SCOPE_IDENTITY(); is a method in SQL that will get the latest created Id for the table.
--we marked @Id above as an output, so when it's created, the st proc will send it back.
