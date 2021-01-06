# How to set up:

1. Create the database. That can be done by executing the create_database.sql file in SQL Server Management Studio. 

2. Open the .sln file located inside the 'app' folder with your preferred IDE (for instance Visual Studio 2019).

3. Replace the connection strings in each App.config file (4 in total).

- connectionString="Data Source=DESKTOP-SDCBG14\SQLEXPRESS.." needs to be replaced with the connection string that is used to connect to your SQL database. 

4. (Optional) Test the connection by running the "UnitTestProject1".

5. Run the "UI" project.
