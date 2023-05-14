#!/bin/bash

# Get the container ID of the PostgreSQL container
CONTAINER_ID=$(docker ps -q --filter "name=postgres")

# Get the current date and time
DATE=$(date +"%Y-%m-%d_%H-%M-%S")

# Create a backup directory
BACKUP_DIR="Services/BackgroundJobs/DataBaseBackup/Backups"
mkdir -p $BACKUP_DIR

# Create a backup file
BACKUP_FILE="$BACKUP_DIR/postgres_backup_$DATE.sql"

# Backup the database
docker exec -it $CONTAINER_ID pg_dumpall > $BACKUP_FILE

# Compress the backup file
gzip $BACKUP_FILE

# Rename the compressed backup file
mv $BACKUP_FILE.gz $BACKUP_DIR

# Print a success message
echo "PostgreSQL database backed up successfully to $PWD/$BACKUP_DIR."