#!/bin/bash

# Get the container ID of the PostgreSQL container
CONTAINER_ID=$(docker ps -q --filter "name=postgres")

# Get the backup file
BACKUP_FILE="$PWD/Backups/postgres_backup_YYYY-MM-DD_HH-MM-SS.sql"

# Restore the database
docker exec -it $CONTAINER_ID psql -U postgres -d postgres -f $BACKUP_FILE

# Print a success message
echo "PostgreSQL database restored successfully."