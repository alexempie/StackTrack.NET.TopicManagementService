@echo off

SETLOCAL ENABLEDELAYEDEXPANSION

for /F "eol=# tokens=* delims=" %%a in (%~dp0\default-config.env %~dp0\default-config-standalone.env) do (
    if not "%%a"=="" set "%%a"
)

:parse
IF "%~1"=="" GOTO end_parse
IF "%~1"=="-db_sa_password" set "DB_SA_PASSWORD=%~2" & SHIFT
IF "%~1"=="-db_name" set "DB_NAME=%~2" & SHIFT
IF "%~1"=="-db_user_username" set "DB_USER_USERNAME=%~2" & SHIFT
IF "%~1"=="-db_user_password" set "DB_USER_PASSWORD=%~2" & SHIFT
SHIFT
GOTO parse
:end_parse

echo Setting up docker secrets...
docker secret inspect db_sa_password 1>nul 2>nul || (echo %DB_SA_PASSWORD% | docker secret create db_sa_password -)
docker secret inspect db_name 1>nul 2>nul || (echo %DB_NAME% | docker secret create db_name -)
docker secret inspect db_user_username 1>nul 2>nul || (echo %DB_USER_USERNAME% | docker secret create db_user_username -)
docker secret inspect db_user_password 1>nul 2>nul || (echo %DB_USER_PASSWORD% | docker secret create db_user_password -)
docker secret inspect db_server 1>nul 2>nul || (echo %DB_SERVER% | docker secret create db_server -)

echo Docker secrets setup complete.
docker network ls | findstr /C:"%NETWORK_NAME%" >nul

echo Creating new network...
IF ERRORLEVEL 1 (
    docker network create --driver overlay %NETWORK_NAME%
    echo Network created.
) ELSE (
    echo Network already exists.
)

echo Building containers...
docker build -t topicmanagementservice -f %~dp0\docker\standalone\app\Dockerfile.app .
docker build -t topicmanagementservice-db -f %~dp0\docker\standalone\db\Dockerfile.db .

echo Deploying containers...
docker stack deploy -c %~dp0\docker\standalone\docker-compose-standalone.yml topicmanagementstack