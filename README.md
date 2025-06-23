# Contacts Manager

This repository contains a minimal full-stack example for managing contacts using React, Redux Toolkit, .NET 8 Web API and PostgreSQL. The project is split into two folders:

- `frontend` – React application bootstrapped with Vite and TypeScript
- `backend` – ASP.NET Core minimal API

## Development

Ensure Docker and Node.js are installed. Start all services with docker-compose:

```bash
docker-compose up --build
```

The API will be available at `http://localhost:5000` and the React app at `http://localhost:3000`.

## API Endpoints

- `GET /api/contacts` – list contacts
- `GET /api/contacts/{id}` – get a single contact
- `POST /api/contacts` – create contact
- `PUT /api/contacts/{id}` – update contact
- `DELETE /api/contacts/{id}` – delete contact

This is only a starting point and not feature complete.
