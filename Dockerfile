FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app
EXPOSE 80

COPY *.sln .
COPY "/LibraryJobInsert.Application/LibraryJobInsert.Application.csproj" "/LibraryJobInsert.Application/"
COPY "/LibraryJobInsert.Domain/LibraryJobInsert.Domain.csproj" "/LibraryJobInsert.Domain/"
COPY "/LibraryJobInsert.Worker/LibraryJobInsert.Worker.csproj" "/LibraryJobInsert.Worker/"
COPY "/LibraryJobInsert.Infrastructure/LibraryJobInsert.Infrastructure.csproj" "/LibraryJobInsert.Infrastructure/"

RUN dotnet restore "/LibraryJobInsert.Worker/LibraryJobInsert.Worker.csproj"


COPY . ./
WORKDIR /app/LibraryJobInsert.Worker
RUN dotnet publish -c Release -o publish 


FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/LibraryJobInsert.Worker/publish ./
ENTRYPOINT ["dotnet", "LibraryJobInsert.Worker.dll"]