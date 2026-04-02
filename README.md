# 📦 Inventario API - Repuestos

Esta es una **Web API** desarrollada con **ASP.NET Core** para la gestión de un inventario de repuestos. El proyecto utiliza una arquitectura de capas simple con el patrón **Repository** y acceso a datos mediante **ADO.NET**.

## 🚀 Tecnologías utilizadas

* **Lenguaje:** C# 13
* **Framework:** .NET 10.0
* **Base de Datos:** SQL Server Express
* **Acceso a Datos:** `Microsoft.Data.SqlClient` (ADO.NET)
* **Documentación:** Swagger / OpenAPI

## 🛠️ Características (Endpoints)

Actualmente, la API permite:

* `GET /api/repuesto`: Obtener el listado de repuestos activos (Soporta filtro opcional `?buscar=texto`).
* `GET /api/repuesto/{id}`: Buscar un repuesto específico por su ID (Solo si está activo).
* `POST /api/repuesto`: Registrar un nuevo repuesto en la base de datos (devuelve 201 Created).
* `PUT /api/repuesto/{id}`: Actualizar datos de un repuesto existente (204 No Content).
* `DELETE /api/repuesto/{id}`: Realiza un **Borrado Lógico** cambiando el estado a inactivo (204 No Content).

## 🛡️ Manejo de Errores y Seguridad

* **Manejo Global:** Respuestas estandarizadas en JSON para errores de conexión o recursos no encontrados.
* **Seguridad SQL:** Uso de parámetros para evitar ataques de SQL Injection.
* **Borrado Lógico:** Los datos no se eliminan físicamente, se preservan para integridad referencial.

## ⚙️ Configuración inicial

1. Clonar el repositorio.
2. Configurar la cadena de conexión en `appsettings.json`.
3. Ejecutar el script SQL (ubicado en `/Scripts`) para crear la tabla `Repuestos`. 
   * **Nota:** Asegúrese de incluir la columna `Activo BIT DEFAULT 1`.
4. Correr la aplicación con `dotnet watch`.


