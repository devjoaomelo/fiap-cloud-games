# ---------- build ----------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "./src/FCG.API/FCG.API.csproj"
RUN dotnet publish "./src/FCG.API/FCG.API.csproj" \
    -c Release -o /app/publish --no-self-contained --no-restore
COPY ./src/FCG.API/appsettings.docker.json /app/publish/appsettings.docker.json

# ---------- runtime ----------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet","FCG.API.dll"]
