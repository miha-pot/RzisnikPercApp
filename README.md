# Customer value viewer

## 🚀 Overview
This app's use case is for an administrator to be able to check what is happening with values for each customer.

## 🛠 Tech Stack
* **Backend:** .NET 8, Minimal API, Entity Framework Core (InMemory)
* **Frontend:** Blazor Web App

## Prerequisites
Requires .NET 8 SDK.

## ⚡ How to Run
1.  **Clone the repository:**
    ```bash
    git clone https://github.com/miha-pot/RzisnikPercApp.git
    ```
2.  **Start the project** : The API and GUI will both start at the same time.

## Arhitecture overview
I used a Clean Architecture approach to separate concerns. Services handle business logic, Repositories handle data access, and SharedDTO ensures loose coupling between the API and GUI.

```
RPApplication
├───RPApplication.Entities
│   ├───DatabaseContext
│   ├───Models
│   └───RequestFeatures
├───RPApplication.GUI
│   ├───Components
│   │   ├───Layout
│   │   └───Pages
│   │       ├───Customers
│   │       └───CustomerValues
│   ├───DTOs
│   ├───Properties
│   ├───ServiceContracts
│   ├───Services
│   └───wwwroot
│       └───bootstrap
├───RPApplication.Repositories
│   └───Extensions
├───RPApplication.RepositoryContracts
├───RPApplication.ServiceContracts
│   └───Mappers
├───RPApplication.Services
│   └───Helpers
├───RPApplication.SharedDTO
└───RPApplication.WebAPI
    ├───Data
    ├───Endpoints
    │   └───v1    
    └───Properties
```

## 🔌 API Endpoints

Here is a summary of the available endpoints:

| Method | Endpoint | Description |
| :--- | :--- | :--- |
| **POST** | `/api/v1/customers` | Returns a list of all customers. |
| **POST** | `/api/v1/customers/create` | Creates a new customer. |
| **DELETE** | `/api/v1/customers/delete/{customerExternalCode}` | Removes a customer. |
| **GET** | `/api/v1/customers/details/{customerId}` | Returns customer data. |
| **PUT**| `/api/v1/customers/update` | Updates customer data. |
| **GET**| `/api/v1/values/{customerCode}` | Returns customer values. |
| **POST**| `/api/v1/values/create` | Creates new customer value. |
