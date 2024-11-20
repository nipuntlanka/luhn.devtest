FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /
COPY ["Luhn.DevTest.Api/Luhn.DevTest.Api.csproj", "Luhn.DevTest.Api/"]
RUN dotnet restore "Luhn.DevTest.Api/Luhn.DevTest.Api.csproj"
COPY . .
WORKDIR "/Luhn.DevTest.Api"
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Luhn.DevTest.Api.dll"]