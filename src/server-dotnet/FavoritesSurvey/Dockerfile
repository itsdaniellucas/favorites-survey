#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 1433

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["FavoritesSurvey/FavoritesSurvey.csproj", "FavoritesSurvey/"]
COPY ["FavoritesSurvey.Core/FavoritesSurvey.Core.csproj", "FavoritesSurvey.Core/"]
COPY ["FavoritesSurvey.DAL/FavoritesSurvey.DAL.csproj", "FavoritesSurvey.DAL/"]
COPY ["FavoritesSurvey.Models/FavoritesSurvey.Models.csproj", "FavoritesSurvey.Models/"]
COPY ["FavoritesSurvey.BLL/FavoritesSurvey.BLL.csproj", "FavoritesSurvey.BLL/"]
RUN dotnet restore "FavoritesSurvey/FavoritesSurvey.csproj"
COPY . .
WORKDIR "/src/FavoritesSurvey"
RUN dotnet build "FavoritesSurvey.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FavoritesSurvey.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FavoritesSurvey.dll"]