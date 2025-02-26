#!/usr/bin/env sh
set -eu


envsubst < /etc/nginx/templates/default.conf.template > /etc/nginx/nginx.conf