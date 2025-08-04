# Stage 1: Build Angular app
FROM node:20-alpine AS build
WORKDIR /app

# More efficient layer caching
COPY package.json package-lock.json ./
RUN npm ci

COPY . .
RUN npm run build -- --project loansimulator.client --configuration production --base-href=/

# Stage 2: Serve app with Nginx
FROM nginx:alpine

# Copy Angular build output (including 'browser' folder)
COPY --from=build /app/dist/loansimulator.client /usr/share/nginx/html

# Copy optimized nginx.conf
COPY --chown=nginx:nginx nginx.conf /etc/nginx/conf.d/default.conf

# Health check
HEALTHCHECK --interval=30s --timeout=3s CMD wget -q -O /dev/null http://localhost/ || exit 1

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
