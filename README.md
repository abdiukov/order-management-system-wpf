#  Warehouse order management system 

Order management system, which includes 3rd party login authentication for extra security. Allows the administrator to place orders, change order status, check whether the order can be fulfilled, and cancel existing orders. The application then reflects those changes onto the SQL database.

# How to set up:

1. Create the database. That can be done by executing the create_database.sql file in Microsoft SQL Server Management Studio. 

2. Open the .sln file located inside the 'app' folder with Visual Studio 2019.

3. (Optional) Test the connection by running the "AutomatedTesting" project.
`
4. Run the "LoginAuth" project.

5. Log in. Email - "user@user.com" , Password - "password" 

## Credits

- Auth0 Login Client - > https://github.com/auth0/auth0-oidc-client-net. Apache-2.0 License.
