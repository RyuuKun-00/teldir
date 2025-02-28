#!/usr/bin/env sh
set -eu

envsubst < /etc/nginx/default.conf.template > /etc/nginx/nginx.conf

exec "$@"