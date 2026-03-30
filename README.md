📦 Inventario API - Repuestos

Esta es una Web API desarrollada con ASP.NET Core para la gestión de un inventario de repuestos. El proyecto utiliza una arquitectura de capas simple con el patrón Repository y acceso a datos mediante ADO.NET.
🚀 Tecnologías utilizadas

    Lenguaje: C#
    Framework: .NET 10.0
    Base de Datos: SQL Server Express
    Acceso a Datos: Microsoft.Data.SqlClient (ADO.NET)
    Documentación: Swagger / OpenAPI

🛠️ Características (Endpoints)

Actualmente, la API permite:

    GET /api/repuesto: Obtener el listado completo de repuestos.
    GET /api/repuesto/{id}: Buscar un repuesto específico por su ID.
    POST /api/repuesto: Registrar un nuevo repuesto en la base de datos (devuelve 201 Created).
    PUT/api/repuesto/{id}: Actualiza datos de un repuesto.(204 No Content)
    DELETE/api/repuesto/{id}: Elimina un registro físicamente.(204 No Content)

⚙️ Configuración inicial

    Clonar el repositorio.
    Configurar la cadena de conexión en appsettings.json.
    Ejecutar el script SQL (ubicado en /Scripts) para crear la base de datos InventarioDB y la tabla Repuestos.
    Correr la aplicación con dotnet watch.

📌 Próximos pasos

    Agregar validaciones avanzadas con FluentValidation.
