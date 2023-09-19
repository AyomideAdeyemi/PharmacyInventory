#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80


FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/backend/PharmacyInventory_WebApi/PharmacyInventory_WebApi.csproj", "src/backend/PharmacyInventory_WebApi/"]
COPY ["src/backend/PharmacyInventory_Application/PharmacyInventory_Application.csproj", "src/backend/PharmacyInventory_Application/"]
COPY ["src/backend/PharmacyInventory_Domain/PharmacyInventory_Domain.csproj", "src/backend/PharmacyInventory_Domain/"]
COPY ["src/backend/PharmacyInventory_Shared/PharmacyInventory_Shared.csproj", "src/backend/PharmacyInventory_Shared/"]
COPY ["src/backend/PharmacyInventory_Infrastructure/PharmacyInventory_Infrastructure.csproj", "src/backend/PharmacyInventory_Infrastructure/"]
RUN dotnet restore "src/backend/PharmacyInventory_WebApi/PharmacyInventory_WebApi.csproj"
COPY . .
WORKDIR "/src/src/backend/PharmacyInventory_WebApi"
RUN dotnet build "PharmacyInventory_WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PharmacyInventory_WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PharmacyInventory_WebApi.dll"]