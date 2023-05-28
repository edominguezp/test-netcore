FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM base AS final
WORKDIR /app
COPY ./publish/ /app
ENTRYPOINT ["dotnet", "Tanner.Core.API.dll"]