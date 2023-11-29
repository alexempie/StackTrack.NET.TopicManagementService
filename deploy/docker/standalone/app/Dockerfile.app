FROM mcr.microsoft.com/dotnet/sdk:7.0 AS app-builder
WORKDIR /app
COPY ./src/TopicManagementService.Common ./TopicManagementService.Common
COPY ./src/TopicManagementService.Core ./TopicManagementService.Core
COPY ./src/TopicManagementService.Infrastructure ./TopicManagementService.Infrastructure
COPY ./src/TopicManagementService.Application ./TopicManagementService.Application
COPY ./src/TopicManagementService.API ./TopicManagementService.API
RUN dotnet publish ./TopicManagementService.API/TopicManagementService.API.csproj -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=app-builder /app/out .
COPY ./deploy/docker/standalone/app/app-entrypoint.sh /deploy/app-entrypoint.sh