FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /src
COPY ["src/ArabianCo.Web.Host/ArabianCo.Web.Host.csproj", "src/ArabianCo.Web.Host/"]
COPY ["src/ArabianCo.Web.Core/ArabianCo.Web.Core.csproj", "src/ArabianCo.Web.Core/"]
COPY ["src/ArabianCo.Application/ArabianCo.Application.csproj", "src/ArabianCo.Application/"]
COPY ["src/ArabianCo.Core/ArabianCo.Core.csproj", "src/ArabianCo.Core/"]
COPY ["src/ArabianCo.EntityFrameworkCore/ArabianCo.EntityFrameworkCore.csproj", "src/ArabianCo.EntityFrameworkCore/"]
WORKDIR "/src/src/ArabianCo.Web.Host"
RUN dotnet restore 

WORKDIR /src
COPY ["src/ArabianCo.Web.Host", "src/ArabianCo.Web.Host"]
COPY ["src/ArabianCo.Web.Core", "src/ArabianCo.Web.Core"]
COPY ["src/ArabianCo.Application", "src/ArabianCo.Application"]
COPY ["src/ArabianCo.Core", "src/ArabianCo.Core"]
COPY ["src/ArabianCo.EntityFrameworkCore", "src/ArabianCo.EntityFrameworkCore"]
WORKDIR "/src/src/ArabianCo.Web.Host"
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:7.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "ArabianCo.Web.Host.dll"]
