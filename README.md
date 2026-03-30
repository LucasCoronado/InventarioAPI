# 📦 Inventario API - Repuestos

Esta es una **Web API** desarrollada con **ASP.NET Core** para la gestión de un inventario de repuestos. El proyecto utiliza una arquitectura de capas simple con el patrón **Repository** y acceso a datos mediante **ADO.NET**.

## 🚀 Tecnologías utilizadas
* **Lenguaje:** C#
* **Framework:** .NET 10.0
* **Base de Datos:** SQL Server Express
* **Acceso a Datos:** Microsoft.Data.SqlClient (ADO.NET)
* **Documentación:** Swagger / OpenAPI

## 🛠️ Características (Endpoints)
Actualmente, la API permite:
* `GET /api/repuesto`: Obtener el listado completo de repuestos.
* `GET /api/repuesto/{id}`: Buscar un repuesto específico por su ID.
* `POST /api/repuesto`: Registrar un nuevo repuesto en la base de datos (devuelve 201 Created).
* `PUT/api/repuesto/{id}`: Actualizar datos de un repuesto existente.(204 No Content)
* `DELETE/api/repuesto/{id}`: Eliminar un registro físicamente del sistema.(204 No Content)

## ⚙️ Configuración inicial
1. Clonar el repositorio.
2. Configurar la cadena de conexión en `appsettings.json`.
3. Ejecutar el script SQL (ubicado en `/Data/Scripts`) para crear la tabla `Repuestos`.
4. Correr la aplicación con `dotnet watch`.
