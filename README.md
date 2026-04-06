GitHub Connector API (.NET)

Project Description:
GitHub Connector is an ASP.NET Core Web API project that integrates with GitHub using OAuth 2.0 authentication. The application allows users to log in using their GitHub account, retrieve their repositories, and create issues in a repository using GitHub REST APIs.

Features:

* GitHub OAuth Login
* Fetch User Repositories
* Create Issues in Repository
* Clean Architecture (Domain, Application, Infrastructure)
* HttpClient for External API Calls
* Dependency Injection
* Serilog Logging
* Swagger API Documentation

Project Structure:
Controllers → API endpoints
Application → Interfaces and Services
Domain → Models / Entities
Infrastructure → GitHub OAuth Service
Logs → Application Logs
Program.cs → Application Startup

OAuth Flow:

1. User hits /api/auth/login
2. Redirect to GitHub login
3. GitHub returns authorization code
4. Application exchanges code for access token
5. Access token used to call GitHub APIs
6. User can fetch repositories or create issues

API Endpoints:

1. GET /api/auth/login
   Redirects user to GitHub login

2. GET /api/auth/callback?code=xxx
   Returns GitHub access token

3. GET /api/github/repos
   Header: accessToken
   Returns user repositories

4. POST /api/github/issue
   Header: accessToken
   Creates issue in repository

Setup Instructions:

1. Clone the repository
2. Add Environment Variables:
   GitHub__ClientId
   GitHub__ClientSecret
3. Run the project using dotnet run
4. Open Swagger to test APIs

Technologies Used:

* ASP.NET Core Web API
* OAuth 2.0
* GitHub REST API
* HttpClient
* Dependency Injection
* Serilog
* Swagger

Future Improvements:

* Store Access Token in Database
* Add JWT Authentication
* Add Frontend (React)
* Docker Deployment

Author:
Akansha Saxena
