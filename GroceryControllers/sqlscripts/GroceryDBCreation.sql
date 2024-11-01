USE Grocery;


DROP TABLE RecipeOrder;
DROP TABLE RecipeDirections;
DROP TABLE RecipeIngredients;
DROP TABLE Recipe;
DROP TABLE MeasuredIngredient;
DROP TABLE Ingredient;
DROP TABLE UnitofMeasure;


CREATE TABLE UnitOfMeasure (
ID INT NOT NULL PRIMARY KEY IDENTITY(1,1),
Name nvarchar(50),
Abbreviation nvarchar(25))

CREATE TABLE Ingredient (
ID INT NOT NULL PRIMARY KEY IDENTITY (1,1),
Name nvarchar(254) NOT NULL,
QuantityInStock decimal DEFAULT 0);

INSERT INTO UnitOfMeasure (Name, Abbreviation) VALUES ('',''), ('Cup', 'C'), ('Tablespoon','Tbsp'), ('Teaspoon', 'Tsp'), ('Ounce', 'Oz'), ('Pound','Lb'), ('Dash','Dash')

Create Table MeasuredIngredient (
ID INT NOT NULL PRIMARY KEY IDENTITY (1,1),
IngredientID int,
MeasureID int,
Quantity decimal(9,4),
CONSTRAINT FK_Measure_Ing_ID FOREIGN KEY (IngredientID) REFERENCES Ingredient(ID),
CONSTRAINT FK_Measure_Unit_ID FOREIGN KEY (MeasureID) REFERENCES UnitOfMeasure(ID))

Create Table Recipe (
ID INT NOT NULL PRIMARY KEY IDENTITY (1,1),
Name NVarChar(300),
CookTime BIGINT DEFAULT 0,
PrepTime BIGINT DEFAULT 0,
Servings INT DEFAULT 4,
Category nvarchar(20) DEFAULT 'Food',
SubCategory nvarchar(20))

CREATE TABLE RecipeIngredients(
ID INT NOT NULL PRIMARY KEY IDENTITY (1,1),
RecipeID INT NOT NULL,
MeasuredIngredientID INT NOT NULL,
CONSTRAINT FK_RecipeI_Recipe_ID FOREIGN KEY (RecipeID) REFERENCES Recipe(ID),
CONSTRAINT FK_RecipeI_MeasuredI_ID FOREIGN KEY (MeasuredIngredientID) REFERENCES MeasuredIngredient(ID))

CREATE TABLE RecipeDirections (
ID INT NOT NULL PRIMARY KEY IDENTITY (1,1),
RecipeID INT NOT NULL,
SequenceNumber INT NOT NULL,
Instruction NVARCHAR(500),
CONSTRAINT FK_RecipeD_Recipe_ID FOREIGN KEY (RecipeID) REFERENCES Recipe(ID))

CREATE TABLE RecipeOrder(
ID INT NOT NULL PRIMARY KEY IDENTITY (1,1),
RecipeID INT NOT NULL,
CustomerName NVARCHAR(200),
Instructions NVARCHAR(800),
Complete bit,
Category nvarchar(20)
CONSTRAINT FK_Order_Recipe_ID FOREIGN KEY (RecipeID) REFERENCES Recipe(ID))

--Food Recipes
INSERT INTO Recipe (Name, CookTime, PrepTime, Servings) VALUES ('Beef Stew', '60', '30', '6'), ('Bibimbap', '20', '20', '4'), ('Caramel Chicken', '20', '20', '6'), ('Shawarma', '30', '20', '6'), ('Honey Soy Grilled Salmon', '15', '20', '4'), ('Walnut-Rosemary Crusted Salmon', '15', '20', '4'), ('Garlic-Ginger Chicken Thighs', '15', '20', '8'), ('Cheesy Egg Casserole', '50', '10', '10'), ('Alfredo, Chicken', '30', '30', '6'), ('Alfredo, Shrimp', '30', '30', '6'), ('Pot Roast', '360', '15', '6'), ('Pulled Pork', '360', '30', '6'), ('Spaghetti and Meat Balls', '45', '15', '6'), ('Chik Patties', '15', '15', '6'), ('Whole Chicken', '90', '15', '6')

--Drink Recipes
INSERT INTO RECIPE (Name, CookTime, PrepTime, Servings, Category, SubCategory) VALUES
('Tommy''s Margarita', '0', '5', '1', 'Drink', 'Cocktails')
,('Whiskey Sour', '0', '5', '1', 'Drink', 'Cocktails')
, ('Aviation', '0', '5', '1', 'Drink', 'Cocktails')
, ('Jack Rose', '0', '5', '1', 'Drink', 'Cocktails')
--, ('Brandy Alexander', '0', '5', '1', 'Drink', 'Cocktails')
, ('Old Fashioned', '0', '5', '1', 'Drink', 'Cocktails')
--, ('Cinnamon Sour', '0', '5', '1', 'Drink', 'Cocktails')
, ('Limoncello Lavender Mule', '0', '5', '1', 'Drink', 'Cocktails')
--, ('Barrel Aged Bulleit Rye', '0', '5', '1', 'Drink', 'Whiskey')
--, ('Mimosa', '0', '5', '1', 'Drink', 'Cocktails')
, ('Whiskey Run Creek Edelweiss', '0', '5', '1', 'Drink', 'Wine')
--, ('Whiskey Run Creek Frontenac Rose', '0', '5', '1', 'Drink', 'Wine')
, ('7 Deadly Cab', '0', '5', '1', 'Drink', 'Wine')
, ('Avaline Rose', '0', '5', '1', 'Drink', 'Wine')
, ('Seaglass Suavignon Blanc', '0', '5', '1', 'Drink', 'Wine')
, ('Robert Mondavi Private Selection Bourbon Barrel Cabernet Sauvignon', '0', '5', '1', 'Drink', 'Wine')
, ('Soaring Wings Dragon''s Red', '0', '5', '1', 'Drink', 'Wine')
--, ('Dr. Pepper', '0', '5', '1', 'Drink', 'Soda')
, ('Canada Dry Ginger Ale', '0', '5', '1', 'Drink', 'Soda')
, ('Sprite', '0', '5', '1', 'Drink', 'Soda')
--, ('Mill Blend', '0', '5', '1', 'Drink', 'Pour Over Coffee')
, ('French Vanilla', '0', '5', '1', 'Drink', 'Pour Over Coffee')
, ('Cellar 426 Rocky''s Red', '0', '5', '1', 'Drink', 'Wine')
, ('Cellar 426 Sweet n'' Sexy', '0', '5', '1', 'Drink', 'Wine')
, ('Earl Grey', '0', '5', '1', 'Drink', 'Tea')
, ('Bengal Spice', '0', '5', '1', 'Drink', 'Tea')
, ('Herbal Green', '0', '5', '1', 'Drink', 'Tea')
, ('The Mill''s Rasberry Green Tea', '0', '5', '1', 'Drink', 'Tea')
, ('Republic of Tea Black Raspberry Green Tea', '0', '5', '1', 'Drink', 'Tea')
, ('Republic of Tea Wild Blueberry', '0', '5', '1', 'Drink', 'Tea')
, ('Republic of Tea Peach Blossom Oolong', '0', '5', '1', 'Drink', 'Tea')
, ('Republic of Tea Blackberry Sage', '0', '5', '1', 'Drink', 'Tea')
, ('Harney & Sons London Fog', '0', '5', '1', 'Drink', 'Tea')
--, ('Lagavulin 8 Year Scotch', '0', '5', '1', 'Drink', 'Whiskey')
--, ('Jefferson''s Reserve Very Small Batch Bourbon', '0', '5', '1', 'Drink', 'Whiskey')
, ('Diet Dr Pepper', '0', '5' ,'1', 'Drink', 'Soda')





--All Ingredients
Insert Into Ingredient (Name) VALUES ('Apple Brandy'), ('Bacon'), ('Bacon, Thick Cut'), ('Baking Powder'), ('Baking Soda'), ('Basil'), 
('Bay Leaves'), ('Beans, Navy'), ('Beans, Refried'), ('Beef Chuck Roast'), ('Bitters, Angostura'), ('Bread Crumbs, Italian'), 
('Broth, Beef'), ('Brown Sugar, Dark'), ('Brown Sugar, Light'), ('Butter, Unsalted'), ('Butter, Unsalted'), ('Buttermilk'), 
('Canola Oil'), ('Carrots'), ('Celery'), ('Champagne'), ('Chicken, Boneless Skinless Breast'), ('Chocolate'), 
('Chocolate Chips, Semisweet'), ('Cinnamon'), ('Coconut Oil'), ('Cognac'), ('Cornstarch'), ('Cream of Tartar'), ('Cream, Heavy'), 
('Creme de Cacao'), ('Egg'), ('Elbow Macaroni'), ('Extra-Virgin Olive Oil'), ('Flour, All-Purpose'), ('Garlic'), ('Garlic Powder'), 
('Gin, Kirkland London Dry'), ('Ginger Beer'), ('Green Onion'), ('Grenadine, 2:1'), ('Groud Beef, 80%'), ('Honey'), ('Limoncello'), 
('Maraschino Liquer'), ('Milk, 2%'), ('Milk, Whole'), ('Oil, Olive'), ('Oil, Vegetable'), ('Onion, Red'), ('Onion, White'), 
('Onion, Yellow'), ('Paprika'), ('Parsley, Dried'), ('Parsley, Fresh'), ('Peanut Butter Chips'), ('Peanut Butter, Creamy'), 
('Peanut Butter, Crunchy'), ('Pepper, Ground Black'), ('Pepper, Jalapeno'), ('Pepper, Red Bell'), ('Pepper, Sobrano'), 
('Potato, Russet'), ('Reese''s Pieces'), ('Rice, White'), ('Salt'), ('Salt, Sea'), ('Sharp Cheddar'), ('Simple Syrup, 2:1'), 
('Simple Syrup, 2:1, Cinnamon'), ('Simple Syrup, Lavender'), ('Sour Cream'), ('Super Juice, Lemon'), ('Super Juice, Lime'), 
('Super Juice, Orange'), ('Sweet Potatoes'), ('Thyme'), ('Tomato Puree'), ('Vanilla'), ('Vinegar, Apple Cider'), ('Vinegar, White'), 
('Violet Liquer'), ('Water'), ('Whiskey, Kirkland Blended Scotch'), ('Whiskey, Rye, Bulleit'), ('White Sugar'), ('Wine, Red'), 
('Worcestershire sauce'), ('Yukon Gold Potatoes'), ('Tequila, Reposado, Casamigas'), ('Agave Nectar')

--Update Drink Ingredient Stock
DROP TABLE IF EXISTS #IngredientUpdate;
CREATE TABLE #IngredientUpdate (Name nvarchar(254) NOT NULL,
QuantityInStock decimal DEFAULT 0)

INSERT INTO #IngredientUpdate VALUES ('Bitters, Angostura', 5), ('Apple Brandy', 26), 
('Cognac', 26), ('Gin, London Dry, Kirkland', 50), ('Ginger Beer', 16), --('Cream, Heavy', 16),
('Creme de Cacao', 15), 
('Grenadine, 2:1', 16), ('Limoncello', 16), ('Maraschino Liquer', 16), ('Simple Syrup, 2:1, Cinnamon', 12),
('Super Juice, Lemon', 16), ('Super Juice, Lime', 16), ('Violet Liquer', 12), ('Whiskey, Kirkland Blended Scotch', 56), ('Whiskey, Rye, Bulleit', 26),
('Simple Syrup, 2:1', 16), ('Gin, Kirkland London Dry', 30), ('Simple Syrup, Lavender', 10), ('Tequila, Reposado, Casamigas', 26), ('Agave Nectar', 8)

UPDATE Ingredient
Set QuantityInStock = #IngredientUpdate.QuantityInStock
FROM Ingredient
INNER JOIN #IngredientUpdate
on Ingredient.Name = #IngredientUpdate.Name;

--Beef Stew MeasuredIngredient
INSERT INTO MeasuredIngredient (Quantity, MeasureID, IngredientID) VALUES (1, 6, 10), (1, 2, 88), (0.5, 2, 37), (4, 3, 67), (2, 3, 59), (2, 1, 7), (1, 3, 6), (3, 3, 49), (2, 3, 78), (0.25, 2, 36), (8, 1, 64), (3, 2, 13)

--Drink MeasuredIngredient
INSERT INTO MeasuredIngredient (Quantity, MeasureID, IngredientID) VALUES (1, 5, 70), (1.5, 5, 70), (0.75, 5, 70), (1.5, 5, 85), (0.75, 5, 74), (1.5, 5, 39), (0.5, 5, 74), (.5, 5, 46), (.5, 5, 83), (1.5, 5, 1), (0.75, 5, 42), (1.5, 5, 28), (0.5, 5, 32), (0.5, 5, 31), (2, 5, 86), (0.75, 5, 75), (0.75, 5, 71), (4, 5, 40), (0.5, 5, 72), (0.5, 5, 45), (0.5, 5, 75), (1, 5, 42), (1, 5, 39), (3, 7, 11), (1.5, 5, 70), (1.5, 5, 86), (3, 5, 22), (2, 5, 84), (0.5, 5, 76), (0.5, 5, 70), (2, 5, 91), (0.5,5,92), (1, 5, 74)

--Beef Stew
INSERT INTO RecipeIngredients (RecipeID, MeasuredIngredientID) VALUES (1,1), (1,2), (1,3), (1,4), (1,5), (1,6), (1,7), (1,8), (1,9), (1,10), (1,11), (1,12) 

--Drinks
DECLARE @ID int

SELECT @ID = ID FROM RECIPE WHERE NAME LIKE '%Tommy''s Margarita%';
INSERT INTO RecipeIngredients (RecipeID, MeasuredIngredientID) VALUES 
(@ID, 43), (@ID, 44), (@ID, 45)

SELECT @ID = ID FROM RECIPE WHERE NAME LIKE '%Whiskey Sour%';
INSERT INTO RecipeIngredients (RecipeID, MeasuredIngredientID) VALUES 
(@ID, 16), (@ID, 17), (@ID, 15)

--Aviation
SELECT @ID = ID FROM RECIPE WHERE NAME LIKE '%Aviation%';
INSERT INTO RecipeIngredients (RecipeID, MeasuredIngredientID) VALUES 
(@ID, 19), (@ID, 18), (@ID, 20), (@ID, 21)

--Jack Rose
SELECT @ID = ID FROM RECIPE WHERE NAME LIKE '%Jack Rose%';
INSERT INTO RecipeIngredients (RecipeID, MeasuredIngredientID) VALUES 
(@ID, 22), (@ID, 17), (@ID, 23)

--Brandy Alexander
SELECT @ID = ID FROM RECIPE WHERE NAME LIKE '%Brandy Alexander%';
INSERT INTO RecipeIngredients (RecipeID, MeasuredIngredientID) VALUES 
(@ID, 24), (@ID, 25), (@ID, 26)

--Old Fashioned
SELECT @ID = ID FROM RECIPE WHERE NAME LIKE '%Old Fashioned%';
INSERT INTO RecipeIngredients (RecipeID, MeasuredIngredientID) VALUES 
(@ID, 27), (@ID, 36), (@ID,37)

--Cinnamon Sour
SELECT @ID = ID FROM RECIPE WHERE NAME LIKE '%Cinnamon Sour%';
INSERT INTO RecipeIngredients (RecipeID, MeasuredIngredientID) VALUES 
(@ID, 38), (@ID, 28), (@ID, 29)

--Limoncello Lavender Mule
SELECT @ID = ID FROM RECIPE WHERE NAME LIKE '%Limoncello Lavender Mule%';
INSERT INTO RecipeIngredients (RecipeID, MeasuredIngredientID) VALUES 
(@ID,35), (@ID, 30), (@ID, 31), (@ID, 32)

----Mimosa
--SELECT @ID = ID FROM RECIPE WHERE NAME LIKE '%Mimosa%';
--INSERT INTO RecipeIngredients (RecipeID, MeasuredIngredientID) VALUES 
--(@ID, 39), (@ID, 40), (@ID, 41), (@ID,42)

--Order
--INSERT INTO RecipeOrder (RecipeID, CustomerName, Instructions, Complete) Values (16, 'Austin Paolini', 'Make it a double', 0)

SELECT * FROM UnitOfMeasure;
SELECT * FROM Recipe;
SELECT * FROM Ingredient;
SELECT * FROM MeasuredIngredient;

SELECT MI.ID, MI.Quantity, U.Name, I.Name
FROM MeasuredIngredient MI
INNER JOIN Ingredient I on I.ID = MI.IngredientID
INNER JOIN UnitOfMeasure U on U.ID = MI.MeasureID;

SELECT R.Name, R.CookTime, R.PrepTime, R.Servings, I.Name as IngredientName, U.Name as UnitName, MI.Quantity
FROM Recipe R
INNER JOIN RecipeIngredients RI
ON R.ID = RI.RecipeID
INNER JOIN MeasuredIngredient MI
ON MI.ID = RI.MeasuredIngredientID
INNER JOIN Ingredient I
ON MI.IngredientID = I.ID
INNER JOIN UnitOfMeasure U
ON U.ID = MI.MeasureID


SELECT R.ID, R.Name, I.Name as IngredientName, U.Name as UnitName, MI.Quantity
FROM Recipe R
INNER JOIN RecipeIngredients RI
ON R.ID = RI.RecipeID
INNER JOIN MeasuredIngredient MI
ON MI.ID = RI.MeasuredIngredientID
INNER JOIN Ingredient I
ON MI.IngredientID = I.ID
INNER JOIN UnitOfMeasure U
ON U.ID = MI.MeasureID
WHERE R.Category = 'Drink'


SELECT R.ID, R.Name From Recipe R where Category ='Drink'

Select ID, Name, quantityInStock 
From Ingredient
where Ingredient.ID in 
	(select ingredientId 
	from MeasuredIngredient
	where  ID in 
		(select MeasuredIngredientID 
		from RecipeIngredients 
		where RecipeID in 
			(select id 
			from Recipe 
			where Category = 'Drink')))