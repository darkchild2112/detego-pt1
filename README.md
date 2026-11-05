# Detego Pt.1

## Running the Project

This project was built using **.NET 10, C#, React and Aspire**, developed in **Visual Studio 2026**.

### Prerequisites

You'll need the following installed:

- **.NET 10 SDK** – [Download](https://dotnet.microsoft.com/download/dotnet/10.0)
- **Visual Studio 2026** – With the ASP.NET and web development workload installed.
- **Node.js and npm** – [Download](https://nodejs.org/)
- **Aspire CLI** – Used to run the API and frontend together.

#### Installing Aspire

Install Aspire using PowerShell:

```powershell
winget install Microsoft.Aspire
```

Or using the .NET CLI:

```bash
dotnet tool install -g Aspire.Cli
```

Verify it's installed:

```bash
aspire --version
```

See the [Aspire documentation](https://aspire.dev/) for more information.

### Running the Application

1. Open the solution in Visual Studio 2026.
2. Set `Detego.AppHost` as the startup project.
3. Press **F5** to run the application.

Aspire will start both the API and React frontend and open the Aspire Dashboard.

From the dashboard:

- **API** – Click the API URL to open Swagger and test the endpoints.
- **Website** – Click the website URL to open the React frontend, which connects to the API.

Alternatively, you can run the application from the command line using `aspire run` from the repository root.

### What I'd Do With More Time

- **Service layer** – Move the logic from `GetHashesHandler` into a service. This would make sense for a larger project where the functionality might be reused, but felt unnecessary for something this small.
- **Playwright tests** – Add E2E tests for the frontend.
- **Frontend unit tests** – Add tests for the React components.
- **TypeScript** – A conscious decision not to use it initially as I knew it would slow me down.
- **Search highlighting** – Highlight matching keywords in the results table.
- **Linting and Prettier** – Improve code consistency.
- **CI pipeline** – Add GitHub Actions to build and test the project automatically.
- **SQLite in-memory testing** – Consider using an in-memory database for integration tests rather than a copy of the main database. I avoided this because setting up the schema and test data would have taken extra time.
- **Frontend feedback** – Add loading indicators and a "No search results found" message.

### When I Used AI

The bulk of the work was done by me, including the frontend, API and Aspire setup.

I mainly used ChatGPT to help debug a few issues, usually where I was looking for a complicated problem and the answer was something simple.

A couple of examples:

- **CORS** – Spent time wondering why the frontend couldn't access the API. Turned out I'd left a trailing `/` on the original configured URL. 
- **SQLite integration tests** – Had a few issues resolving the database file path because I'd forgotten the tests execute from a nested directory inside the build output.

I used ChatGPT to tidy up this README and write the Aspire installation instructions. I also used Cluade Code to do a Code Review at the end, which found a couple of bugs and gave some tidy-up and code styling suggestions

Everything else, including the implementation and technical decisions, were my own.

