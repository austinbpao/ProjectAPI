USE Grocery;
GO
DROP PROCEDURE GetDrinkList;
DROP PROCEDURE GetDrinkListIngredients;
DROP PROCEDURE GetDrinkListByIDs;
DROP PROCEDURE AddDrinks;
DROP PROCEDURE AddIngredients;
DROP PROCEDURE GetIngredients;
DROP PROCEDURE GetIngredientsByID;
DROP PROCEDURE DeleteIngredients;
DROP PROCEDURE GetDrinkOrderList;
DROP PROCEDURE AddDrinkOrders;
DROP PROCEDURE UpdateDrinkOrders;
DROP PROCEDURE DeleteDrinkOrder;
DROP PROCEDURE GetRecipeListByIDs;
DROP FUNCTION dbo.RecipeInStock;
DROP TYPE IdTableType;
DROP TYPE RecipeTableType;
DROP TYPE IngredientTableType;
DROP TYPE DrinkOrderTableType;
GO

CREATE TYPE dbo.IdTableType as Table (Id int);
GO

CREATE TYPE dbo.RecipeTableType as Table (
Id int,
Name NVarChar(300),
CookTime BIGINT DEFAULT 0,
PrepTime BIGINT DEFAULT 0,
Servings INT DEFAULT 4,
Category nvarchar(20) DEFAULT 'Drink')
GO

CREATE TYPE dbo.IngredientTableType as Table(
Id int,
Name nvarchar(254),
QuantityInStock decimal(18,0))
GO

CREATE FUNCTION dbo.RecipeInStock (@recipeID int)
RETURNS bit
AS
BEGIN
	DECLARE @InStock bit
	IF EXISTS
		(SELECT ID FROM INGREDIENT
		WHERE ID in 
			(Select Ingredient.ID FROM RecipeIngredients
			inner join MeasuredIngredient
			on MeasuredIngredient.id = RecipeIngredients.MeasuredIngredientID
			Inner Join Ingredient
			on Ingredient.id = MeasuredIngredient.IngredientID
			WHERE RecipeID = @recipeID)
			AND Ingredient.QuantityInStock = 0)
		SET @InStock = 0
	ElSE 
		SET @InStock = 1
	RETURN @InStock;
END;
GO

CREATE TYPE dbo.DrinkOrderTableType as Table(
Id int,
RecipeId int,
CustomerName NVARCHAR(200),
Instructions NVARCHAR(800),
Complete bit,
Category nvarchar(20))
GO

Create PROCEDURE GetDrinkList AS 
SET NOCOUNT ON;
SELECT R.ID, R.Name, R.Servings, R.Category, R.SubCategory, dbo.RecipeInStock(R.ID) as InStock
FROM Recipe R with (nolock)
WHERE R.Category = 'Drink'
and dbo.RecipeInStock(R.ID) = 1;
GO

CREATE PROCEDURE GetDrinkListIngredients AS
SELECT R.ID as RecipeId, R.Name as RecipeName, I.ID as IngredientId, I.Name as IngredientName, U.Name as UnitName, MI.Quantity, dbo.RecipeInStock(R.ID) as InStock, QuantityInStock 
FROM Recipe R WITH (NOLOCK)
INNER JOIN RecipeIngredients RI WITH (NOLOCK)
ON R.ID = RI.RecipeID
INNER JOIN MeasuredIngredient MI WITH (NOLOCK)
ON MI.ID = RI.MeasuredIngredientID
INNER JOIN Ingredient I WITH (NOLOCK)
ON MI.IngredientID = I.ID
INNER JOIN UnitOfMeasure U WITH (NOLOCK)
ON U.ID = MI.MeasureID
WHERE R.Category = 'Drink'
and dbo.RecipeInStock(R.ID) = 1;
GO

CREATE PROCEDURE GetDrinkListByIDs (@IdTable dbo.IdTableType READONLY) AS
SELECT R.ID, R.Name, R.Servings, R.Category, R.SubCategory 
FROM Recipe R  WITH (NOLOCK)
INNER JOIN @IdTable Id
ON Id.Id = R.ID
WHERE R.Category = 'Drink'
and dbo.RecipeInStock(R.ID) = 1;
GO

CREATE PROCEDURE AddDrinks (@RecipeTable dbo.RecipeTableType READONLY) AS
INSERT INTO Recipe (Name, CookTime, PrepTime, Servings, Category)
Output INSERTED.ID, inserted.Name, inserted.CookTime, inserted.PrepTime, inserted.Servings, inserted.Category
Select Name, CookTime, PrepTime, Servings, Category from @RecipeTable;
GO

CREATE PROCEDURE AddIngredients (@IngredientTable dbo.IngredientTableType READONLY) AS
INSERT INTO Ingredient (Name, QuantityInStock)
Output inserted.ID, inserted.Name, inserted.QuantityInStock
SELECT Name, QuantityInStock FROM @IngredientTable;
GO

CREATE PROCEDURE GetIngredients AS
SELECT ID, Name, QuantityInStock
FROM Ingredient
GO

CREATE PROCEDURE GetIngredientsByID (@IngredientTable dbo.IngredientTableType READONLY) AS
SELECT ID, Name, QuantityInStock
FROM Ingredient
WHERE Ingredient.ID in (SELECT ID FROM @IngredientTable)
GO

CREATE PROCEDURE DeleteIngredients (@IngredientTable dbo.IngredientTableType READONLY) AS
DELETE FROM Ingredient
OUTPUT deleted.ID, deleted.Name, deleted.QuantityInStock
WHERE Ingredient.ID IN (SELECT ID FROM @IngredientTable)
GO

CREATE PROCEDURE AddDrinkOrders (@DrinkOrderTable dbo.DrinkOrderTableType READONLY) AS
INSERT INTO RecipeOrder (RecipeID, CustomerName, Instructions, Complete, Category)
Output INSERTED.ID, INSERTED.RecipeId, INSERTED.CustomerName, INSERTED.Instructions, INSERTED.Complete, INSERTED.Category
SELECT RecipeId, CustomerName, Instructions, Complete, Category FROM @DrinkOrderTable;

SELECT Id, Name, Servings, Category, SubCategory FROM Recipe
WHERE Id = (Select RecipeId from @DrinkOrderTable);

SELECT R.ID as RecipeId, R.Name as RecipeName, I.ID as IngredientId, I.Name as IngredientName, U.Name as UnitName, MI.Quantity
FROM Recipe R WITH (NOLOCK)
INNER JOIN RecipeIngredients RI WITH (NOLOCK)
ON R.ID = RI.RecipeID
INNER JOIN MeasuredIngredient MI WITH (NOLOCK)
ON MI.ID = RI.MeasuredIngredientID
INNER JOIN Ingredient I WITH (NOLOCK)
ON MI.IngredientID = I.ID
INNER JOIN UnitOfMeasure U WITH (NOLOCK)
ON U.ID = MI.MeasureID
WHERE R.Category = 'Drink'
AND R.ID in (SELECT RecipeID from @DrinkOrderTable)
GO

CREATE PROCEDURE UpdateDrinkOrders (@DrinkOrderTable dbo.DrinkOrderTableType READONLY) AS
UPDATE RecipeOrder
SET Complete = DOT.Complete
, RecipeID = DOT.RecipeId
, Instructions = DOT.Instructions
, CustomerName = DOT.CustomerName
, Category = DOT.Category
OUTPUT INSERTED.id, INSERTED.RecipeID, INSERTED.CustomerName, INSERTED.Instructions, INSERTED.Complete, INSERTED.Category
FROM RecipeOrder RO
INNER JOIN @DrinkOrderTable DOT
ON DOT.Id = RO.ID;

SELECT Id, Name, Servings FROM Recipe
WHERE Id = (Select RecipeId from @DrinkOrderTable);

SELECT R.ID as RecipeId, R.Name as RecipeName, I.ID as IngredientId, I.Name as IngredientName, U.Name as UnitName, MI.Quantity
FROM Recipe R WITH (NOLOCK)
INNER JOIN RecipeIngredients RI WITH (NOLOCK)
ON R.ID = RI.RecipeID
INNER JOIN MeasuredIngredient MI WITH (NOLOCK)
ON MI.ID = RI.MeasuredIngredientID
INNER JOIN Ingredient I WITH (NOLOCK)
ON MI.IngredientID = I.ID
INNER JOIN UnitOfMeasure U WITH (NOLOCK)
ON U.ID = MI.MeasureID
WHERE R.Category = 'Drink'
AND R.ID in (SELECT RecipeID from @DrinkOrderTable)
GO

CREATE PROCEDURE GetDrinkOrderList AS
DROP TABLE IF EXISTS #OrderList;

SELECT ID, RecipeID, CustomerName, Instructions, Complete
INTO #OrderList
FROM RecipeOrder;

SELECT * From #OrderList;

SELECT ID, Name, Servings, Category, SubCategory
FROM Recipe
WHERE Category='Drink'
AND ID in (SELECT RecipeID from #OrderList);

SELECT R.ID as RecipeId, R.Name as RecipeName, I.ID as IngredientId, I.Name as IngredientName, U.Name as UnitName, MI.Quantity
FROM Recipe R WITH (NOLOCK)
INNER JOIN RecipeIngredients RI WITH (NOLOCK)
ON R.ID = RI.RecipeID
INNER JOIN MeasuredIngredient MI WITH (NOLOCK)
ON MI.ID = RI.MeasuredIngredientID
INNER JOIN Ingredient I WITH (NOLOCK)
ON MI.IngredientID = I.ID
INNER JOIN UnitOfMeasure U WITH (NOLOCK)
ON U.ID = MI.MeasureID
WHERE R.Category = 'Drink'
AND R.ID in (SELECT RecipeID from #OrderList)
GO

CREATE PROCEDURE DeleteDrinkOrder (@DrinkOrderTable dbo.DrinkOrderTableType READONLY)
AS
DELETE FROM RecipeOrder
OUTPUT DELETED.id, DELETED.RecipeID, DELETED.CustomerName, DELETED.Instructions, DELETED.Complete, DELETED.Category
WHERE ID IN (SELECT ID FROM @DrinkOrderTable)
GO

--EXEC GetDrinkList;
--EXEC GetDrinkListIngredients;
--EXEC GetDrinkOrderList;



--Food Related Sprocs
DROP PROCEDURE IF EXISTS GetRecipes;
DROP PROCEDURE IF EXISTS GetRecipeListIngredients;
DROP PROCEDURE IF EXISTS GetRecipeListByIDs;
GO

CREATE PROCEDURE GetRecipes
AS
SELECT R.ID, R.Name, R.Servings, R.Category, R.SubCategory, dbo.RecipeInStock(R.ID) as InStock
FROM Recipe R with (nolock)
WHERE R.Category = 'Food'
--and dbo.RecipeInStock(R.ID) = 1;
GO

CREATE PROCEDURE GetRecipeListIngredients AS
SELECT R.ID as RecipeId, R.Name as RecipeName, I.ID as IngredientId, I.Name as IngredientName, U.Name as UnitName, MI.Quantity, dbo.RecipeInStock(R.ID) as InStock, QuantityInStock 
FROM Recipe R WITH (NOLOCK)
INNER JOIN RecipeIngredients RI WITH (NOLOCK)
ON R.ID = RI.RecipeID
INNER JOIN MeasuredIngredient MI WITH (NOLOCK)
ON MI.ID = RI.MeasuredIngredientID
INNER JOIN Ingredient I WITH (NOLOCK)
ON MI.IngredientID = I.ID
INNER JOIN UnitOfMeasure U WITH (NOLOCK)
ON U.ID = MI.MeasureID
WHERE R.Category = 'Food'
--and dbo.RecipeInStock(R.ID) = 1;
GO

CREATE PROCEDURE GetRecipeListByIDs (@IdTable dbo.IdTableType READONLY) AS
SELECT R.ID, R.Name, R.Servings, R.Category, R.SubCategory 
FROM Recipe R  WITH (NOLOCK)
INNER JOIN @IdTable Id
ON Id.Id = R.ID
WHERE R.Category = 'Food'
--and dbo.RecipeInStock(R.ID) = 1;
GO

Exec GetRecipes;
Exec GetRecipeListIngredients;