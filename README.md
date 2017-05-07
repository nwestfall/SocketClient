# SocketClient

[![Build status](https://ci.appveyor.com/api/projects/status/x38vgwi9h3ko0yl5/branch/master?svg=true)](https://ci.appveyor.com/project/nwestfall/socketclient/branch/master)

Simple .NET Core console application to test sending data to a socket connection

## Running the application

To run the application, either download the solution and build on your machine or click the build icon above and in the "artifacts" are a Windows and Mac executable.

#### To Run on Windows
 - Open Command Prompt
 - Navigate to folder containing executable
 - type `./SocketClient.exe`
 
#### To Run on Mac
 - Open Terminal
 - Navigate to folder containing executable
 - type `./SocketClient`
 
Once running, you will have to enter the following information
 - IP Address of Socket Server
 - Port of Socket Server
 - Full path to file location of XML or TXT file to use to send data to Socket Server
 - How long to wait inbetween sending data (in seconds)
 
You can stop sending data any time by clicking any key
