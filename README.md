# 🛍️ Gestor de Productos - Prueba Técnica ASP.NET Core + JavaScript

Aplicación web para gestionar productos (crear, editar, eliminar y listar) usando:

- ASP.NET Core Web API
- Entity Framework Core + SQL Server
- JavaScript (Vanilla)
- HTML + CSS 

---

## 🚀 Requisitos

Antes de ejecutar el proyecto, asegurarse de tener instalado:

- .NET 6.0 SDK o superior

---

## ⚙️ Instrucciones para ejecutar el proyecto

### 1. Clonar el repositorio

git clone https://github.com/IsaiasMarreroDevs/Gestor-De-Productos.git
cd GestorProductos

### 2. Configurar la conexión a la base de datos

Abrir el archivo appsettings.json y modificar la cadena de conexión si tu SQL Server no es localhost:

"ConnectionStrings": {
  "DefaultConnection": "Server=TU_SERVIDOR_SQL;Database=Prueba;Trusted_Connection=True;TrustServerCertificate=True"
}

### 3. Crear la base de datos y la tabla

Puedes ejecutar este script en SQL Server Management Studio o Azure Data Studio:

CREATE DATABASE Prueba;
GO

USE Prueba;
GO

CREATE TABLE [dbo].[Productos] (
  [ProductoId] [INT] PRIMARY KEY IDENTITY(1,1) NOT NULL,,
  [Nombre] [varchar] (50) NOT NULL,
	[Precio] [decimal] (10,3) NOT NULL,
  [FechaCreacion] [datetime2] (7) NOT NULL
);

### 4. Ejecutar la API

Desde la raíz del proyecto, ejecuta:

dotnet run

Esto iniciará el servidor en una dirección como:

https://localhost:7232

### 5. Usar la aplicación web

Abre tu navegador y visita:

https://localhost:7232/wwwroot/index.html

Desde allí podrás:

- Ver los productos existentes
- Crear nuevos productos
- Editar productos existentes
- Eliminar productos
- Interactuar con una interfaz sencilla y funcional

---

## 📁 Estructura del Proyecto

GestorProductos/
├── Controllers/         → Web API RESTful en C#
├── Models/              → Entidades y DbContext (EF Core)
├── Services/            → Lógica 
├── wwwroot/             → Interfaz HTML + CSS + JS
├── Program.cs           → Configuración principal
├── appsettings.json     → Conexión a SQL Server

---

## 📝 Notas Finales

- Este proyecto fue desarrollado como una prueba técnica básica.
- No se utilizaron frameworks de frontend, solo HTML + JS puro.

---

## 🙋 Autor

Isaías Marrero.  
Desarrollador en formación | Prueba técnica ASP.NET + SQL + JS

