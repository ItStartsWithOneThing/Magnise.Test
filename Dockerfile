
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Magnise.Test.API/Magnise.Test.API.csproj", "Magnise.Test.API/"]
COPY ["Magnise.Test.BL/Magnise.Test.BL.csproj", "Magnise.Test.BL/"]
COPY ["Magnise.Test.DAL/Magnise.Test.DAL.csproj", "Magnise.Test.DAL/"]
RUN dotnet restore "Magnise.Test.API/Magnise.Test.API.csproj"
COPY . .
WORKDIR "/src/Magnise.Test.API"
RUN dotnet build "Magnise.Test.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Magnise.Test.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Magnise.Test.API.dll"]