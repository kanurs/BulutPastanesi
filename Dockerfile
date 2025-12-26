# .NET 8.0 kullanıyoruz 
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["BulutPastanesi.csproj", "./"]
RUN dotnet restore "BulutPastanesi.csproj"
COPY . .
RUN dotnet publish "BulutPastanesi.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "BulutPastanesi.dll"]