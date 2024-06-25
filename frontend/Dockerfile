# Stage 1: Compile and Build angular codebase

FROM node:latest as build

WORKDIR /usr/local/app/
COPY ./ /usr/local/app/

RUN npm install
RUN npm run build


# Stage 2: Serve app with nginx server

FROM nginx:latest
COPY ./nginx.conf /etc/nginx/conf.d/default.conf
COPY --from=build /usr/local/app/dist/sky-explorer /usr/share/nginx/html

EXPOSE 80