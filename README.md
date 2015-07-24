# Clear Measure MVP Master Bootcamp 
Current Build Status [![](http://build.clear-measure.com/app/rest/builds/buildType:(id:BootCamp_CompileAndTest)/statusIcon)](http://teamcity/viewType.html?buildTypeId=btN&guest=1)

**This project is the starting point for folks taking part in the Clear Measure 
Bootcamp for MVC.** 

This course is a feature-driven walkthrough that guides participants through
a series of improvements to an existing code base, demonstrating best pracitices and 
and working through scenarios targetting 300- and 400-level developers.

## Getting Started

- Clone the repo
- Configure your db (see pre-requisites below)
- Run the click to build script
- **Build** the application in Visual Studio 2015
- **Run** the application from VS

## Application Overview

The starting point is an expense report application with a very limited feature set, and
room to improve. You will work through defining and prioritizing new features, then work 
in pairs to build these out. Your product owner will present you with new requirements
while you map out how things will be built, pair programming and working through a series
of iterations, writing tests and leveraging CI. Roll up the sleeves!

## Roadmap

The MVC Framework 6 is part of ASP.NET 5 and continues to evolve. The RTM (go-live) 
release is [scheduled for Q1 of 2016](https://github.com/aspnet/Home/wiki/Roadmap). 
We are building out this sample and giving guidance to developers throughout this 
evolution and stabilization, and will be updating this solution from time-to-time 
along the way.

> We are currently in the process of migrating from MVC 5 to MVC 6. You can follow
> our progress at our blog and through the issues here on GitHub.

## Pre-requisites

You will need to have the following installed for this project to work:

 - [Visual Studio 2015 RTM] (https://www.visualstudio.com/downloads/download-visual-studio-vs)
 - [SQL Server Express 2014] (https://www.microsoft.com/en-ca/server-cloud/products/sql-server-editions/sql-server-express.aspx) with an instance named SQLEXPRESS2014 (for other options, see note below)
 - A GitHub account ([good thing you're already here](https://github.com/join))

If you do not have SQL Server Express 2014 or have named your instance otherwise, create 
a user-level environment variable called "dbServer" with the correct connection string
information.


