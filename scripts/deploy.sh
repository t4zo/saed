#!/bin/bash
cd /var/www/saed/src/SAED.Web
dotnet restore
dotnet build --configuration Release --no-restore
dotnet publish --configuration Release --no-build
cd /var/www/saed/src/
dotnet-ef database update -s SAED.Web -p SAED.Persistence