#!/bin/bash

log() {
    echo "$(date +'%Y-%m-%d %H:%M:%S') $1"
}

export DB_SERVER=$(cat /run/secrets/db_server | tr -d '\n\r\t ')
export DB_NAME=$(cat /run/secrets/db_name | tr -d '\n\r\t ')
export DB_USER_USERNAME=$(cat /run/secrets/db_user_username | tr -d '\n\r\t ')
export DB_USER_PASSWORD=$(cat /run/secrets/db_user_password | tr -d '\n\r\t ')

log "Running app..."
exec dotnet TopicManagementService.API.dll
APP_PID=$!

wait $APP_PID