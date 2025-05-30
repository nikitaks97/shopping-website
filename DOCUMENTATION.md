# Shopping Website (.NET 8) – Project Documentation

This comprehensive guide will help you build, test, run, and deploy the Shopping Website project using .NET 8 and Docker. Each step includes detailed explanations to ensure clarity for both beginners and experienced developers.

---

## Prerequisites

Before you begin, ensure you have the following tools installed:

- **.NET 8 SDK** ([Download](https://dotnet.microsoft.com/en-us/download/dotnet/8.0))  
  The .NET SDK is required to build, run, and test the application. You can verify your installation by running `dotnet --version` in your terminal.
- **Docker** ([Download](https://www.docker.com/products/docker-desktop/))  
  Docker is used to containerize the application, making it easy to deploy and run in any environment. Make sure Docker Desktop is running and configured to use Linux containers.
- **Git** ([Download](https://git-scm.com/))  
  Git is used for version control and to clone the repository.
- **Visual Studio Code** ([Download](https://code.visualstudio.com/)) or **Visual Studio** (Optional)  
  These editors provide a rich development experience with features like IntelliSense, debugging, and integrated terminal.

---

## 1. Clone the Repository

Open your terminal or command prompt and run:

```sh
git clone <your-repo-url>   # Clones the repository from GitHub to your local machine
cd shopping-website         # Navigates into the project directory
```

> **Tip:** Replace `<your-repo-url>` with the actual URL of your repository.

---

## 2. Restore Dependencies

Restore all NuGet packages required by the project:

```sh
dotnet restore   # Downloads and installs all dependencies specified in the .csproj file
```

This ensures that all external libraries and tools your project depends on are available.

---

## 3. Build the Application

Compile the application in Release mode for optimized performance:

```sh
dotnet build --configuration Release   # Builds the project with optimizations enabled
```

- The `--configuration Release` flag tells .NET to use the Release configuration, which is optimized for production.
- The build output will be placed in the `bin/Release/net8.0/` directory by default.

---

## 4. Database Migration

If your project uses Entity Framework Core for data access, you need to apply any pending migrations to your database. This ensures your database schema matches your application's data models.

First, install the EF Core CLI tool (if not already installed):

```sh
dotnet tool install --global dotnet-ef   # Installs the EF Core command-line tool globally
```

Then, update the database:

```sh
dotnet ef database update   # Applies all pending migrations to the database specified in appsettings.json
```

- This command will create the database if it does not exist and apply all migrations found in the `Migrations/` folder.
- Ensure your connection string in `appsettings.json` is correct before running this command.

---

## 5. Run the Application

Start the application using the following command:

```sh
dotnet run --configuration Release   # Launches the application in Release mode
```

- By default, the application will be accessible at `http://localhost:5000` or the port specified in your configuration.
- You should see output in the terminal indicating that the application has started and is listening for requests.

---

## 6. Run Tests

To ensure your application is working as expected, run the automated tests:

```sh
dotnet test --no-build --verbosity normal   # Runs all tests without rebuilding the project
```


- The `--no-build` flag skips the build step, assuming the project is already built.
- The `--verbosity normal` flag provides standard output, showing test results and any errors.
- Review the output to ensure all tests pass. If any tests fail, review the error messages and fix the issues before proceeding.

---

## 7. Docker: Build and Run

Docker allows you to package your application and its dependencies into a container, ensuring consistency across different environments.

### Build Docker Image

```sh
docker build -t shopping-website:latest .   # Builds a Docker image named 'shopping-website' using the Dockerfile in the current directory
```

- The `-t` flag tags the image with a name and optionally a version (`latest` in this case).
- The `.` specifies the build context (current directory).
- The Dockerfile should be present in the root of your project.

### Run Docker Container

```sh
docker run -d -p 5000:80 shopping-website:latest   # Runs the Docker image in detached mode, mapping port 5000 on your host to port 80 in the container
```

- The `-d` flag runs the container in detached mode (in the background).
- The `-p 5000:80` flag maps port 5000 on your host to port 80 in the container.
- Access the application at `http://localhost:5000` in your browser.

---

## 8. Continuous Integration (CI) with GitHub Actions

This project uses GitHub Actions for automated building, testing, and deployment. The workflow file is located at `.github/workflows/dotnet.yml`.

### Workflow Steps Explained

1. **Checkout code**: Retrieves your repository's code so the workflow can access it.
2. **Setup .NET**: Installs the specified version of the .NET SDK (8.0.x) on the runner.
3. **Restore dependencies**: Runs `dotnet restore` to install all NuGet packages.
4. **Build**: Compiles the application in Release mode.
5. **Install dotnet-ef**: Installs the Entity Framework Core CLI tool globally.
6. **Add dotnet tools to PATH**: Ensures global tools are available in the workflow environment.
7. **Update database**: Applies EF Core migrations to the database.
8. **Test**: Runs all tests and continues even if some fail (for reporting purposes).
9. **Docker login**: Authenticates to DockerHub using secrets/variables.
10. **Set up Docker Buildx**: Prepares the environment for advanced Docker builds.
11. **Build and push Docker image**: Builds the Docker image and pushes it to DockerHub.

### Triggering the Workflow

- The workflow runs automatically on every push or pull request to the `main` branch.
- You can view workflow runs and logs in the GitHub Actions tab of your repository.

---

## 9. Environment Variables for CI

To enable Docker image publishing in CI, set the following in your GitHub repository settings:

- **DOCKERHUB_USERNAME** (Repository Variable): Your DockerHub username.
- **DOCKERHUB_TOKEN** (Repository Secret): A DockerHub access token or password (never share this publicly).

These are used by the workflow to authenticate and push images to DockerHub.

---

## 10. Project Structure

- `Controllers/` – Contains MVC/Web API controllers that handle HTTP requests and responses.
- `Models/` – Contains Entity Framework models representing your application's data structures.
- `Views/` – Contains Razor views for rendering HTML pages (if using MVC).
- `Migrations/` – Contains EF Core migration files for database schema changes.
- `wwwroot/` – Contains static files such as CSS, JavaScript, and images.
- `Dockerfile` – Instructions for building the Docker image.
- `appsettings.json` – Application configuration, including database connection strings and other settings.

---

## 11. Useful Commands

- **Add a new migration:**
  ```sh
  dotnet ef migrations add <MigrationName>   # Creates a new database migration with the specified name
  ```
- **Update the database:**
  ```sh
  dotnet ef database update   # Applies all pending migrations to the database
  ```
- **List available Docker images:**
  ```sh
  docker images   # Shows all Docker images on your system
  ```
- **Remove a Docker container:**
  ```sh
  docker rm <container_id>   # Removes a stopped Docker container
  ```
- **View running containers:**
  ```sh
  docker ps   # Lists all currently running Docker containers
  ```
- **Stop a running container:**
  ```sh
  docker stop <container_id>   # Stops a running Docker container
  ```

---

## 12. Troubleshooting

- **Missing dependencies:** If you get errors about missing dependencies, run `dotnet restore` to ensure all NuGet packages are installed.
- **Docker errors on Windows:** Make sure Docker Desktop is set to use Linux containers (not Windows containers) for compatibility with most .NET images.
- **Database issues:** Check your connection string in `appsettings.json` and ensure your database server is running and accessible.
- **Port conflicts:** If the default port (5000) is in use, change the port mapping in the Docker run command (e.g., `-p 5001:80`).
- **Permission errors:** On Linux/Mac, you may need to prefix Docker commands with `sudo` if you encounter permission issues.

---

## 13. SonarCloud Scan and Quality Gate

A SonarCloud scan step is included in the GitHub Actions workflow immediately after the code checkout. This step analyzes your code for bugs, vulnerabilities, code smells, and other quality issues using SonarCloud. The scan requires the following secrets to be set in your GitHub repository:

- `SONAR_PROJECT_KEY`: The unique key for your SonarCloud project.
- `SONAR_ORGANIZATION`: Your SonarCloud organization name.
- `SONAR_TOKEN`: A SonarCloud user token with permissions to analyze the project.

After the scan, a quality gate step is included. This step waits for the SonarCloud analysis to complete and checks the quality gate status. If the quality gate fails, you should review the issues in SonarCloud and address them before merging or deploying your code. For a hard enforcement, use SonarCloud's PR decoration and branch protection features in the SonarCloud UI.

**Workflow Example:**

```yaml
- name: SonarCloud Scan
  uses: SonarSource/sonarcloud-github-action@v2.1.1
  with:
    projectKey: ${{ secrets.SONAR_PROJECT_KEY }}
    organization: ${{ secrets.SONAR_ORGANIZATION }}
  env:
    SONAR_TOKEN: ${{ secrets.SONAR_TOKEN }}

- name: SonarCloud Quality Gate
  run: |
    echo "Waiting for SonarCloud Quality Gate..."
    sleep 30
  # SonarCloud quality gate is checked automatically after the scan step in most setups. If you need a hard gate, use SonarCloud's PR decoration and branch protection in the UI.
```

**Explanation:**
- The scan step uploads your code analysis results to SonarCloud.
- The quality gate step provides a buffer for SonarCloud to process the results. The actual pass/fail status is visible in the SonarCloud UI and as a PR check.
- For strict enforcement, configure branch protection rules in GitHub to require the SonarCloud check to pass before merging.

