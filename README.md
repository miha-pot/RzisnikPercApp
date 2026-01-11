# Customer value viewer

## 🚀 Overview
This app's use case is for an administrator to be able to check what is happening with values for each customer.

## 🛠 Tech Stack
* **Backend:** .NET 8, Minimal API, Entity Framework Core (InMemory)
* **Frontend:** Blazor Web App

## ⚡ How to Run
1.  **Clone the repository:**
    ```bash
    git clone https://github.com/miha-pot/RzisnikPercApp.git
    ```
2.  **Start the project** : The API and GUI will both start at the same time.

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
