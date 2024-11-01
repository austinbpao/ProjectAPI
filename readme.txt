Deployment information:
This site is using an angular front end hosted in IIS using this guide:https://levelup.gitconnected.com/how-to-deploy-angular-app-to-an-iis-web-server-complete-setup-337997486423
The API is written in c# using ASP.NET
The Data is stored in a Microsoft SQL server  




Notes:
Currently, the SubmitTypedTable method has a list of columns and a list of values.
This is more or less mimicking a full data row, so this should be refactored.  Most likely by allowing the classes to implement a toDataRow method?

That method is also currently structured to only create one row, and should maybe be made more flexible

Recipe has
    Ingredients
    Directions
    Servings
    Prep Time
    Cook Time
    Total Time
Recipe can 
    scale servings on whole people
    Output recipe as custom string

Recipe List can 
    Gather Ingredients
    Print recipes in sequence

Ingredient has
    Name
    Measurement

Ingredient can
    change from one measure to another

Measurement has 
    Quantity

Measurement can
    change from one type to another