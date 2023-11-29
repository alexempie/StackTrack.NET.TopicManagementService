#!/bin/bash

source $(dirname "$0")/default-config.env
source $(dirname "$0")/default-config-standalone.env

while [[ "$#" -gt 0 ]]; do
    case $1 in
        -db_sa_password) DB_SA_PASSWORD="$2"; shift ;;
        -db_name) DB_NAME="$2"; shift ;;
        -db_user_username) DB_USER_USERNAME="$2"; shift ;;
        -db_user_password) DB_USER_PASSWORD="$2"; shift ;;
        *) echo "Unknown parameter passed: $1"; exit 1 ;;
    esac
    shift
done

echo "Creating Docker secrets if not exists..."
docker secret inspect db_sa_password &>/dev/null || (echo $DB_SA_PASSWORD | docker secret create db_sa_password -)
docker secret inspect db_name &>/dev/null || (echo $DB_NAME | docker secret create db_name -)
docker secret inspect db_user_username &>/dev/null || (echo $DB_USER_USERNAME | docker secret create db_user_username -)
docker secret inspect db_user_password &>/dev/null || (echo $DB_USER_PASSWORD | docker secret create db_user_password -)
docker secret inspect db_server &>/dev/null || (echo $DB_SERVER | docker secret create db_server -)

echo "Docker secrets setup complete."
network_exists=$(docker network ls --filter name=$NETWORK_NAME --format "{{.Name}}")

echo "Creating new network..."
if [ -z "$network_exists" ]; then
    docker network create --driver overlay $NETWORK_NAME
    echo "Network created."
else
    echo "Network already exists."
fi

echo "Building containers..."
docker build -t topicmanagementservice -f $(dirname "$0")/docker/standalone/app/Dockerfile.app .
docker build -t topicmanagementservice-db -f $(dirname "$0")/docker/standalone/db/Dockerfile.db .

echo "Deploying container..."
docker stack deploy -c $(dirname "$0")/docker/standalone/docker-compose-standalone.yml topicmanagementstack