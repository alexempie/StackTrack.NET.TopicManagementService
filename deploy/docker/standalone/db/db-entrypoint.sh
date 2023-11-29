#!/bin/bash

log() {
    echo "$(date +'%Y-%m-%d %H:%M:%S') $1"
}

export MSSQL_SA_PASSWORD=$(cat /run/secrets/db_sa_password | tr -d '\n\r\t ')

/opt/mssql/bin/sqlservr &
SQL_PID=$!
log "SQL Server process started with PID $SQL_PID"

execute_sql() {
    /opt/mssql-tools/bin/sqlcmd -U SA -P $MSSQL_SA_PASSWORD -Q"$1" -b > /dev/null 2>&1
}

log "Waiting for SQL Server to start..."
counter=0
until execute_sql "SELECT 1"
do
    sleep 60
    ((counter++))
    if [ $counter -ge 36 ]; then
        log "SQL Server startup timed out."
        exit 1
    fi
    log "Retrying sqlcmd... Attempt: $counter"
done

log "SQL Server started."

DB_NAME=$(cat /run/secrets/db_name | tr -d '\n\r\t ')

log "Publishing the database..."
/opt/sqlpackage/sqlpackage \
    /a:Publish \
    /tsn:localhost \
    /tdn:$DB_NAME \
    /tu:sa \
    /tp:$MSSQL_SA_PASSWORD \
    /sf:/deploy/TopicManagementService.Database.dacpac \
    /p:BlockWhenDriftDetected=false \
    /TargetEncryptConnection:False \

if [ $? -eq 0 ]; then
    log "Database published successfully."
else
    log "Database publishing failed."
fi

DB_USER_USERNAME=$(cat /run/secrets/db_user_username | tr -d '\n\r\t ')
DB_USER_PASSWORD=$(cat /run/secrets/db_user_password | tr -d '\n\r\t ')

if [[ -z "$DB_USER_USERNAME" || -z "$DB_USER_PASSWORD" ]]; then
    log "Error: Missing DB_USER_USERNAME or DB_USER_PASSWORD environment variables."
    exit 1
fi

ESCAPED_DB_USER_PASSWORD=$(echo $DB_USER_PASSWORD | sed "s/'/''/g")

ADD_USER_SQL_QUERY="
DECLARE @dbUserUsername NVARCHAR(128) = '$DB_USER_USERNAME';
DECLARE @dbUserPassword NVARCHAR(128) = '$ESCAPED_DB_USER_PASSWORD';
DECLARE @dbName NVARCHAR(128) = '$DB_NAME';
DECLARE @sql NVARCHAR(MAX);

IF NOT EXISTS (SELECT name FROM master.sys.server_principals WHERE name = @dbUserUsername)
BEGIN
    SET @sql = 
        N'CREATE LOGIN ' + QUOTENAME(@dbUserUsername) + N' WITH PASSWORD = N''' + @dbUserPassword + N''';' +
        N' USE ' + QUOTENAME(@dbName) + N';' + 
        N' CREATE USER ' + QUOTENAME(@dbUserUsername) + N' FOR LOGIN ' + QUOTENAME(@dbUserUsername) + N';' +
        N' ALTER ROLE db_owner ADD MEMBER ' + QUOTENAME(@dbUserUsername) + N';';
    
    EXEC sp_executesql @sql;
END
GO
"

log "Creating and configuring database user with db_owner role..."
execute_sql "$ADD_USER_SQL_QUERY"

if [ $? -eq 0 ]; then
    log "Database user with db_owner role created and configured successfully."
else
    log "Failed to create and configure database user with db_owner role."
    exit 1
fi

wait $SQL_PID