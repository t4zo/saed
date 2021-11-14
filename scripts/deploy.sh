#!/bin/bash
cd /var/www/saed/src/SAED.Web
dotnet restore
libman restore
sed -i -e 's/T.wrapper.querySelector(".dataTable-input")/document.querySelector(".dataTable-custom-input")/g' ./wwwroot/libs/vanilla-datatables/dist/vanilla-dataTables.min.js
dotnet build --configuration Release --no-restore
dotnet publish --configuration Release --no-build
cd /var/www/saed/src/
dotnet-ef database update -s SAED.Web -p SAED.Persistence