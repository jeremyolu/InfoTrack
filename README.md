# InfoTrack Assessment Project

## Prerequisites

Before running the application, ensure you have the following installed:

* **.NET 9 Runtime/SDK** (required for the API)
* **Node.js v20.19.5** (required for the UI)

---

## Endpoints

### API

* **HTTPS:** https://localhost:7206

### UI

* **URL:** http://localhost:5173/

CORS has been configured to allow requests from the UI endpoint.

---

## Running the API

1. Open a terminal.
2. Navigate to the API project directory.
3. Run the following command:

```bash
dotnet run --launch-profile https
```

The API will start on:

```
https://localhost:7206
```

---

## Running the UI

1. Open a new terminal.
2. Navigate to the UI project directory.
3. Install the required packages:

```bash
npm install
```

4. Start the development server:

```bash
npm run dev
```

The UI should start on:

```
http://localhost:5173/
```

If it starts on a different port, update the UI configuration as required.

---

## Test User Accounts

The following accounts can be used to log in:

| Username     | Password       |
| ------------ | -------------- |
| `jeremy.olu` | `Password123*` |
| `joe.bloggs` | `PasswordAbc!` |

These users can also be found in the `UserRepository` class within the API project.
