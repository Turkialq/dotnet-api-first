BACKUPDIR=/home/xcad/backup


# backup all mysql/mariadb containers
# backup all mysql/mariadb containers and all databases

CONTAINER=$(docker ps --format '{{.Names}}:{{.Image}}' | grep 'mysql\|mariadb' | cut -d":" -f1)

@@ -21,19 +21,35 @@ if [ ! -d $BACKUPDIR ]; then
fi

for i in $CONTAINER; do
    MYSQL_DATABASE=$(docker exec $i env | grep MYSQL_DATABASE |cut -d"=" -f2)

    MYSQL_PWD=$(docker exec $i env | grep MYSQL_ROOT_PASSWORD |cut -d"=" -f2)

    docker exec -e MYSQL_DATABASE=$MYSQL_DATABASE -e MYSQL_PWD=$MYSQL_PWD \
        $i /usr/bin/mysqldump -u root $MYSQL_DATABASE \
        | gzip > $BACKUPDIR/$i-$MYSQL_DATABASE-$(date +"%Y%m%d%H%M").sql.gz
    docker exec -e MYSQL_PWD=$MYSQL_PWD $i /usr/bin/mysqldump \
        --all-databases --ignore-database=mysql -u root \
        | gzip > $BACKUPDIR/$i-$(date +"%Y%m%d%H%M").sql.gz

    OLD_BACKUPS=$(ls -1 $BACKUPDIR/$i*.gz |wc -l)
    if [ $OLD_BACKUPS -gt $DAYS ]; then
        find $BACKUPDIR -name "$i*.gz" -daystart -mtime +$DAYS -delete
    fi
done

# backup all postgres containers and all databases

POSTGRES_CONTAINER=$(docker ps --format '{{.Names}}:{{.Image}}' | grep 'postgres' | cut -d":" -f1)

for i in $CONTAINER; do

    POSTGRES_USER=$(docker exec $i env | grep POSTGRES_USER |cut -d"=" -f2)

    docker exec -t $i pg_dumpall --exclude-database=template1 \
    -c -U $POSTGRES_USER | gzip > $BACKUPDIR/$i-$(date +"%Y%m%d%H%M").sql.gz

    OLD_POSTGRES_BACKUPS=$(ls -1 $BACKUPDIR/$i*.gz |wc -l)
    if [ $OLD_POSTGRES_BACKUPS -gt $DAYS ]; then
        	find $BACKUPDIR -name "$i*.gz" -daystart -mtime +$DAYS -delete
    fi
done

# bitwarden backup
