# Safe Auto CodeKata

This project was completed in 48 hours for a CodeKata provided by Safe Auto.

There are two separate projects in this repository:

* driving-stats-frontend
* DrivingStatsBackend

The *driving-stats-frontend* is an Angular project programmed in Visual Studio Code.

The *DrivingStatsBackend* is a Dotnet Core Web Api project programmed in Visual Studio 2019.

## How to Test This Project

Open the *DrivingStatsBackend* solution in Visual Studio and run it under Debug configuration. Then, open the *driving-stats-frontend* folder in a command line and run "ng serve --open". The backend should be running on https://localhost:44315. The frontend should be running on https://localhost:4200.

Now, with the angular project open, use the *Upload* page to upload any one of the .txt files in this project's root directory or your own, and test the output.

## Rules for Uploaded Files

The formatting rules for uploaded file can be found on the *Rules* page of the Angular website.

## Rules from SafeAuto

> CodeKata
>
>Use Angular for frontend and .Net for backend. The goal of this exercise is to get a >better glimpse into your thought process. While this is a simple exercise, think of it >as a large project, so put whatever patterns you think would be necessary for an >enterprise level application.
>
>Create a ReadMe file to explain any details you think would help verify your work.
>Submit solution to GitHub or any other publicly accessible code repository.
>
>The code will process an input file.
>
>Each line in the input file will start with a command. There are two possible commands. .>The first command is Driver, which will register a new Driver in the app. Example: >Driver Dan The second command is Trip, which will record a trip attributed to a driver. .>The line will be space delimited with the following fields: the command (Trip), driver >name, start time, stop time, miles driven. Times will be given in the format of >hours:minutes. We'll use a 24-hour clock and will assume that drivers never drive past >midnight (the start time will always be before the end time). Example: Trip Dan 07:15 >07:45 17.3 Discard any trips that average a speed of less than 5 mph or greater than 100 >mph. Generate a report containing each driver with total miles driven and average speed. >Sort the output by most miles driven to least. Round miles and miles per hour to the >nearest integer.
>
>Example input:
>
>Driver Dan
>
>Driver Alex
>
>Driver Bob
>
>Trip Dan 07:15 07:45 17.3
>
>Trip Dan 06:12 06:32 21.8
>
>Trip Alex 12:01 13:16 42.0
>
>Expected output:
>
>Alex: 42 miles @ 34 mph
>
>Dan: 39 miles @ 47 mph
>
>Bob: 0 miles
