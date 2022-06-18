# UserManagement

ABOUT PROJECT:
Project User management is designed in the first place to manage users and their accounts and beside that, additional feature is to manage vehicles and their expenses.
This project includes adding, updating and deleting users. Every user with account can log in with authorization which will allow him to use mentioned
additional feature like adding, updating and deleting vehicles. For every vehicle, user can assign specific expense as well as update and delete existing one.
For example, if the cost is registration, and it expires in 10 days, the user will receive a notification about this information to the e-mail address he
entered when he was registering his account. Users, vehicles and expenses can be sorted, filtered and searched by basic information like first name, last name and username for users. For vehicles, we use name, license plate and manufacturer as parameter to search, filter and sort. For expenses we use name and price.

TECHNOLOGIES:
ASP .NET 5.0 Web API,
Microsoft SQL Server,
JWT

PREREQUISITIES:
Microsoft Visual Studio 2022 IDE,
Microsoft SQL Server Management Studio 18,
Git

STARTING THE APPLICATION:
Before starting the application, you have to do a few things. Clone the github repository and rebuild the whole solution in Visual Studio. After rebuilding the solution, you have to update database to create all the tables. To update database, you have to open your .sln and choose UserManagement.Database as default project in Package manager console.
