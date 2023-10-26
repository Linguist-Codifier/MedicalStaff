#References: https://aka.ms/customizecontainer

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MedStaff.DaS.Communicator/MedStaff.DaS.Communicator.csproj", "MedStaff.DaS.Communicator/"]
RUN dotnet restore "MedStaff.DaS.Communicator/MedStaff.DaS.Communicator.csproj"
COPY . .
WORKDIR "/src/MedStaff.DaS.Communicator"
RUN dotnet build "MedStaff.DaS.Communicator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MedStaff.DaS.Communicator.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MedStaff.DaS.Communicator.dll"]