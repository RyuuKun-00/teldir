FROM nginx

COPY ./templates/default.conf.template /etc/nginx/templates/default.conf.template

COPY ./nginx.conf /etc/nginx/nginx.conf

EXPOSE ${NGINX_PORT}

CMD ["nginx", "-g", "daemon off;"]
