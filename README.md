Copy repository to your local enviorment

Procedure of running the application (You can use both PostgreSQL and MSSQL)
Using PostgreSQL:
1. Connect to your database server via PG admin client or any other.
2. Open the application.
3. Go to application.Development.json, navigate to "NPGSQLCONNECTION" connection string and replase user and password by your credentials.
4. Simply run the application by tapping Ctrl+F5 or in another way convenient for you.
5. Browser will open automatically on Swagger page.

Using MSSQL:
1. Connect to your database server via SQL Server client.
2. Open the application.
3. Go to Program.cs -> comment 18th row and uncomment 17th row.
4. Simply run the application by tapping Ctrl+F5 or in another way convenient for you.
5. Browser will open automatically on Swagger page.


Procedure of running the application via Docker (You can use only PostgreSQL)
1. Open your Docker Desktop application.
2. Open Windows PowerShell.
3. Navigate to application root folder: cd {rootFolder}
4. Enter: 'docker-compose up' and wait till postgres and asp.net containers will start.
5. Now you can use Swagger by this address: http://localhost:8081/swagger/index.html
