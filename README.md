# UserManagement

## About:
UserManagement is a Web API project designed for the management of Users, Permissions for Users, Vehicles connected to the user as well as Expenses for the Vehicles.

Some of the functionalities of the project include creating, reading, updating and deleting data for all four entities. Every user with an account can log in and be authorized to use all the features of the project. If you don't have an account, you could register a new user and use that one. 

Another thing the user is capable of is assigning an expense to a vehicle or a vehicle to a certain user.

When it comes to viewing the data of the entities, searching by id, filtering by any of the parameters and sorting the data by any of the paramteres is supported as well.

One more thing the project supports is scheduling a background proccess once a a registration expense is registered. The backgroudnd proccess is set to execute 10 days before the registration is about to expire. Once it reaches 10 days prior, the proccess sends an e-mail to the user who the vehicle is assigned to. Of course, the user first needs to have an email address registered.  

## Technologies Used:
- ASP .NET 5.0,
- Microsoft SQL Server,
- JWT,
- SendGrid,
- Hangfire

## Prerequisites:
- Microsoft Visual Studio 2022,
- Microsoft SQL Server Management Studio 18,
- Git

## Setup:
### Visual Studio Setup:
Before starting the application, you have to do a few things:


Clone the github repository and rebuild the whole solution in Visual Studio.

After rebuilding the solution, you have to update the database to create all the tables. To update the database, you have to open your solution and choose UserManagement.Database as the default project in the Package Manager Console.
Use this command:

`update-database`

### HangFire Database Setup
After taking care of the database for the project, you also have to take care of the database for Hangfire. Here, you just need to create the database manually before you run the application.

To do this, you need to go to the SQL Server Management Studio, connect, right click on databases in the file exporer and select new database. From there, give the database the name HangFireTest. In order for this to work, the database name has to be the same as in the connection string located in the Startup file. Next time when you run the application, the tables will be created and it will be ready for use.
### Command Line Setup:
If you don't want to run the application through Visual Studio, you will first need to install the ASP.NET 5.0 SDK and Runtime from the official website. After that, open up a command line and navigate to the directory where you project was cloned.
Type in:

`dotnet ef database update`

after updating the database, you can run the following command:

`dotnet watch run`

and the application will run.

You still need to do the steps for making the HangFire database before running the application. 