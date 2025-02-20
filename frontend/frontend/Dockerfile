FROM node:latest as dependencies
WORKDIR /frontend
COPY package.json package-lock.json ./
RUN npm install

FROM node:latest as builder
WORKDIR /frontend
COPY . .
COPY --from=dependencies /frontend/node_modules ./node_modules
RUN npm run build

FROM node:latest as runner
WORKDIR /frontend
ENV NODE_ENV production

COPY --from=builder /frontend/public ./public
COPY --from=builder /frontend/package.json ./package.json
COPY --from=builder /frontend/.next ./.next
COPY --from=builder /frontend/node_modules ./node_modules

EXPOSE 4002
CMD ["npm","run", "start"]