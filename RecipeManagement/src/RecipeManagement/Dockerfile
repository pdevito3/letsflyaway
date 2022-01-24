FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY ["RecipeManagement.csproj", "./"]
RUN dotnet restore "./RecipeManagement.csproj"

# Copy everything else and build
COPY . ./
RUN dotnet build "RecipeManagement.csproj" -c Release -o /app/build

FROM build-env AS publish
RUN dotnet publish "RecipeManagement.csproj" -c Release -o /app/out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=publish /app/out .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
#EXPOSE 443

ENTRYPOINT ["dotnet", "/app/RecipeManagement.dll"]
