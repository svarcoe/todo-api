# Stage 0, based on Node.js, to build and compile Angular
FROM node as node
WORKDIR /app
RUN npm i -g @angular/cli@6.0.0
COPY package.json /app/
RUN npm install
COPY ./ /app/
EXPOSE 4200 49153
ENTRYPOINT ["ng", "serve", "--host", "0.0.0.0"]

# Stage 1, based on Nginx, to have only the compiled app, ready for production with Nginx
# FROM nginx
# COPY --from=node /app/dist/ /usr/share/nginx/html
# COPY ./nginx-custom.conf /etc/nginx/conf.d/default.conf