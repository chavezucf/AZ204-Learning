## **📜 1. Complete README for AZ204-Learning Repo**


# AZ204-Learning 🚀

This repository serves as my learning space for the **AZ-204: Developing Solutions for Microsoft Azure** certification. It contains multiple projects related to Azure development, including .NET Web APIs, Docker, SQL Server, Event Grid, and more.

Each project is stored in its own subfolder and follows its own setup and deployment process.


## 📂 Project Structure

```md
AZ204-Learning/
│── MyWebApiApp/        # .NET Web API deployed on Azure with ACR
│── AnotherProject/     # Future projects will be added here
│── SomeOtherProject/   # Event Grid, Functions, etc.
│── .gitignore          # Global ignores
│── README.md           # This documentation
```


## 🔥 Current Project: **MyWebApiApp**
📌 **Overview:** This is a .NET Web API that is deployed on **Azure using Azure Container Registry (ACR)**.

📖 **Related Blog Post:** [From Confusion to Clarity: Deploying a .NET Web API on Azure with ACR](https://blog.miguelchavezweb.com/posts/from-confusion-to-clarity---deploying-a-.net-web-api-on-azure-with-acr/)

### 🚀 How to Run Locally
1. Install [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0).
2. Clone this repository:
   ```sh
   git clone https://github.com/your-username/AZ204-Learning.git
   ```
3. Navigate to the project folder:
   ```sh
   cd AZ204-Learning/MyWebApiApp
   ```
4. Restore dependencies and run the app:
   ```sh
   dotnet restore
   dotnet run
   ```
5. The API should now be running at `http://localhost:5000`.



### 🏗️ Deployment to Azure with ACR
This project is deployed using **Azure Container Registry (ACR)**. Follow the detailed steps in the [blog post](https://blog.miguelchavezweb.com/posts/from-confusion-to-clarity---deploying-a-.net-web-api-on-azure-with-acr/).



## 🎯 Future Projects
This repository will be expanded to include:
✅ **Azure Functions & Event Grid**  
✅ **Azure SQL Database Integration**  
✅ **Dockerized Microservices**  
✅ **Security & Authentication (Azure AD, Managed Identities)**  
✅ **More AZ-204 topics...**



## 🛠️ Adding a New Project
To add a new project:
1. Navigate to the repo root:
   ```sh
   cd AZ204-Learning
   ```
2. Create a new folder:
   ```sh
   mkdir NewProjectName
   cd NewProjectName
   ```
3. Initialize a new .NET project (if applicable):
   ```sh
   dotnet new webapi -n NewProjectName
   ```
4. Add a **project-specific `.gitignore`** inside the folder.
5. Commit and push:
   ```sh
   git add .
   git commit -m "Added NewProjectName"
   git push origin main
   ```

## 📢 Contributing
This repository is for personal learning, and I will be adding new projects over time based on AZ-204 topics.

If you have suggestions or improvements, feel free to open an issue or message me.
