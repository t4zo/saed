# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
RUN dotnet tool install -g Microsoft.Web.LibraryManager.Cli
WORKDIR /var/www/app

ENV ASPNETCORE_URLS=http://*:$PORT
ENV PATH="$PATH:/root/.dotnet/tools"

# copy csproj and restore as distinct layers
COPY "*.sln" .
COPY "./tests/UnitTests/*.csproj" "./tests/UnitTests/"
COPY "./src/SAED.Api/*.csproj" "./src/SAED.Api/"
COPY "./src/SAED.Core/*.csproj" "./src/SAED.Core/"
COPY "./src/SAED.Persistence/*.csproj" "./src/SAED.Persistence/"
COPY "./src/SAED.Web/*.csproj" "./src/SAED.Web/"
RUN dotnet restore

# copy everything else and build app
COPY . .
WORKDIR /var/www/app/src/SAED.Web
RUN libman restore
RUN sed -i -e 's/T.wrapper.querySelector(".dataTable-input")/document.querySelector(".dataTable-custom-input")/g' wwwroot/libs/vanilla-datatables/dist/vanilla-dataTables.min.js

FROM build AS publish
WORKDIR /var/www/app/src/SAED.Web
RUN dotnet build -c Release
RUN dotnet publish -c Release -o /var/www/app/publish --no-build
#RUN dotnet publish -c Release --no-build / Original

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:6.0-jammy-arm64v8
LABEL maintainer="Tacio de Souza Campos"
WORKDIR /var/www/app
COPY --from=publish /var/www/app/publish .
ENTRYPOINT ["dotnet", "SAED.Web.dll"]
