#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MedStaff.PaS.Communicator/MedStaff.PaS.Communicator.csproj", "MedStaff.PaS.Communicator/"]
RUN dotnet restore "MedStaff.PaS.Communicator/MedStaff.PaS.Communicator.csproj"
COPY . .
WORKDIR "/src/MedStaff.PaS.Communicator"
RUN dotnet build "MedStaff.PaS.Communicator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MedStaff.PaS.Communicator.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MedStaff.PaS.Communicator.dll"]