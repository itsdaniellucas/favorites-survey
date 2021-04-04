#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["FavoritesSurvey.SVC.ResponseStats/FavoritesSurvey.SVC.ResponseStats.csproj", "FavoritesSurvey.SVC.ResponseStats/"]
COPY ["FavoritesSurvey.Models/FavoritesSurvey.Models.csproj", "FavoritesSurvey.Models/"]
COPY ["FavoritesSurvey.Core/FavoritesSurvey.Core.csproj", "FavoritesSurvey.Core/"]
COPY ["FavoritesSurvey.DAL/FavoritesSurvey.DAL.csproj", "FavoritesSurvey.DAL/"]
COPY ["FavoritesSurvey.BLL/FavoritesSurvey.BLL.csproj", "FavoritesSurvey.BLL/"]
RUN dotnet restore "FavoritesSurvey.SVC.ResponseStats/FavoritesSurvey.SVC.ResponseStats.csproj"
COPY . .
WORKDIR "/src/FavoritesSurvey.SVC.ResponseStats"
RUN dotnet build "FavoritesSurvey.SVC.ResponseStats.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FavoritesSurvey.SVC.ResponseStats.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FavoritesSurvey.SVC.ResponseStats.dll"]