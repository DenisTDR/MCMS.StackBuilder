#!/bin/sh

set -e
#echo "uid=${NETCORE_USER_UID}"
groupadd --gid ${NETCORE_USER_UID} netcore && useradd --uid ${NETCORE_USER_UID} -g netcore netcore

mkdir -p ${PERSISTED_KEYS_DIRECTORY}
chmod -R u=rwX,g=rX,o= /app/persisted
chown -RL netcore:netcore /app/persisted

cd /app/bin
exec sudo -E -u netcore dotnet MCMS.StackBuilder.dll