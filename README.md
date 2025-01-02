# Store Project

A fully functional **e-commerce platform** built with **ASP.NET Core**, **Redis**, and **MongoDB**. This platform supports **multi-vendor functionality** and offers various management features like vendor, product, category, order, and comment management. It also integrates with **Zarinpal** for handling payments.

## Features

- **Multi-Vendor Support**: Allows multiple vendors to sell products, each managing their own inventory.
- **Vendor Management**: Admins can add, update, or delete vendor accounts, and manage product listings.
- **Product Management**: Admins and vendors can add, update, and remove products.
- **Category Management**: Create and manage categories and subcategories for better product organization.
- **User Management**: Manage users, including registration, authentication, and role-based access.
- **Comment Management**: Users can comment and leave reviews on products, which can be managed by admins.
- **User Notes for Products**: Users can add personal notes about products.
- **Zarinpal Integration**: Integrated with Zarinpal for secure payment processing of orders.
- **Order Management**: Admins can manage orders including status updates and history tracking.

## Technologies Used

- **ASP.NET Core**: The main framework for building the web application.
- **Redis**: Used for caching, session management, and improving performance.
- **MongoDB**: A NoSQL database used for storing product, user, vendor, and order data.
- **Zarinpal**: A popular payment gateway for processing payments on the platform.

## Setup Instructions

### Prerequisites

Ensure you have the following installed:

- [ASP.NET Core SDK](https://dotnet.microsoft.com/download/dotnet)
- [MongoDB](https://www.mongodb.com/try/download/community)
- [Redis](https://redis.io/download)
- [Zarinpal Merchant Account](https://www.zarinpal.com/)

### Installation

Clone the repository:
```bash
git clone https://github.com/mahdijafariii/shop-management.git
cd shop-management
```
Restore project dependencies:
```
dotnet restore
```
Set up your MongoDB and Redis instances:
Run MongoDB locally or use a cloud instance.
Set up Redis locally or use a Redis cloud service.
Update the appsettings.json file with your MongoDB, Redis, and Zarinpal configurations:
```
{
  "MongoDbSettings": {
    "ConnectionString": "mongodb://localhost:27017",
    "DatabaseName": "StoreDb"
  },
  "RedisSettings": {
    "ConnectionString": "localhost"
  },
  "ZarinpalSettings": {
    "MerchantId": "your-zarinpal-merchant-id",
    "CallbackUrl": "your-callback-url"
  }
}
```
```
dotnet run
```
