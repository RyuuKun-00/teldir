FROM nginx

COPY ./templates/default.conf.template /etc/nginx/default.conf.template

EXPOSE ${NGINX_PORT}

COPY entrypoint.sh /docker-entrypoint.d/

ENTRYPOINT [ "/docker-entrypoint.sh" ]

CMD ["nginx", "-g", "daemon off;"]
