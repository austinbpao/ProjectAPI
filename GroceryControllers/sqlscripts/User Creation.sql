IF EXISTS (SELECT name FROM master.sys.server_principals WHERE name = 'grocery_monkey') DROP LOGIN grocery_monkey;
DROP USER IF EXISTS grocery_monkey;

CREATE LOGIN grocery_monkey with PASSWORD = '670r1h^1mAsa';
USE Grocery;
CREATE USER grocery_monkey FROM LOGIN grocery_monkey;
ALTER ROLE db_datareader ADD MEMBER grocery_monkey;
ALTER ROLE db_datawriter ADD MEMBER grocery_monkey;
GRANT EXECUTE TO grocery_monkey;

GRANT EXECUTE TO [NT AUTHORITY\LOCAL SERVICE]
ALTER ROLE db_datareader ADD MEMBER [NT AUTHORITY\LOCAL SERVICE];
ALTER ROLE db_datawriter ADD MEMBER [NT AUTHORITY\LOCAL SERVICE];