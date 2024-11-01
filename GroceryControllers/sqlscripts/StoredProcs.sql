USE Grocery;
GO
ALTER PROCEDURE GetRecipeById
	@Id int
AS

SET NOCOUNT ON;
SELECT R.Name, R.CookTime, R.PrepTime, R.Servings
FROM Recipe R with (nolock)

SELECT I.Name as IngredientName, U.Name as UnitName, MI.Quantity
FROM RecipeIngredients RI with (nolock)
INNER JOIN MeasuredIngredient MI with (nolock)
ON MI.ID = RI.MeasuredIngredientID
INNER JOIN Ingredient I with (nolock)
ON MI.IngredientID = I.ID
INNER JOIN UnitOfMeasure U with (nolock)
ON U.ID = MI.MeasureID
WHERE RI.RecipeID = @Id

SELECT SequenceNumber, Instruction
FROM RecipeDirections
WHERE RecipeId = @Id
GO

EXEC GetRecipeById @Id = 1;
GO