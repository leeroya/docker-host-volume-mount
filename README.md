# Docker Host Volume Mount

## Overview

In the project src folder is the source files with a Employees.txt file. This file is where the data will be loaded and stored.
In the EmployeesWebNewCore folder has the API source files and the dockerfile.

### Visual Studio 

Open

### Jetbrains Rider


## Docker 

  docker build -t docker-command -f Manual.Dockerfile .


  docker run -d -p 5000:80 --rm -v ${PWD}:/app/data dotnet-command


This is to showcase a example of volume mounting a file to persist data outside of your dotnet core api
