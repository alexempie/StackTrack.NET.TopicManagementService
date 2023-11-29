#!/bin/bash

log() {
    echo "$(date +'%Y-%m-%d %H:%M:%S') $1"
}

log "Starting db entrypoint"
/bin/bash /deploy/db-entrypoint.sh &
SQL_PID=$!

log "Starting app entrypoint"
/bin/bash /deploy/app-entrypoint.sh &
APP_PID=$!

wait $SQL_PID
wait $APP_PID