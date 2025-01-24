# Arquitectura ONION

El proyecto está estructurado en varias capas para mantener la separación de responsabilidades, facilitar el mantenimiento y mejorar la escalabilidad.

---

## **Capa de Aplicación (Application Layer)**

Esta capa contiene toda la lógica de negocio y las definiciones de contratos para las interfaces de servicio. Aquí se gestionan las siguientes responsabilidades principales:

- **Servicios de negocio**: Implementación de reglas y procesos específicos del dominio.
- **Interfaces**: Contratos que definen las operaciones disponibles en los repositorios y servicios.
- **DTOs (Data Transfer Objects)**: Clases diseñadas para transferir datos entre capas.
- **Validaciones**: Asegurar que los datos cumplen con las reglas de negocio.
- **Mapeo de datos**: Convierte los datos de entrada en DTOs y los datos de salida en respuestas comprensibles para el cliente.

---

## **Capa de Dominio (Domain Layer)**

Es el núcleo de la aplicación, que encapsula los conceptos principales del negocio. Contiene:

- **Modelos de datos**: Representaciones de las entidades del dominio, como las tablas de la base de datos.
- **Enumeraciones (Enums)**: Valores predefinidos utilizados para representar estados o categorías dentro del dominio.
- **Reglas del dominio**: Lógica específica que afecta directamente a los modelos, como validaciones y comportamientos.

---

## **Capa de Persistencia (Persistence Layer)**

Gestiona la interacción con la base de datos y es responsable de la persistencia de datos. Sus responsabilidades incluyen:

- **Contexto de la base de datos**: Configuración y gestión del acceso a la base de datos utilizando herramientas como Entity Framework Core.
- **Repositorios**: Implementación de métodos para acceder, consultar y manipular datos en las tablas.
- **Migraciones**: Gestión del esquema de la base de datos, permitiendo aplicar o revertir cambios estructurales.

---

Este diseño en capas asegura que el proyecto sea modular, reutilizable y fácil de probar. Cada capa se comunica únicamente a través de contratos y no accede directamente a las capas inferiores, promoviendo la adherencia a los principios de arquitectura limpia.


## **Capa de API (API Layer)**
La capa de API actúa como el punto de entrada principal para los clientes que interactúan con la aplicación. Su propósito es exponer los servicios y operaciones del sistema de manera segura y estructurada.

### Responsabilidades principales:
- **Controladores**: Implementan los endpoints HTTP para gestionar las solicitudes y respuestas.
- **Validación de solicitudes**: Verifica que los datos recibidos cumplan con los requisitos antes de procesarlos.
- **Manejo de errores**: Proporciona respuestas claras y coherentes en caso de errores o excepciones.

### Ejemplo de uso:
- Recibe una solicitud HTTP (por ejemplo, `GET /api/task`).
- Valida los datos de la solicitud.
- Llama a los servicios de la capa de Aplicación para realizar la lógica de negocio.
- Devuelve la respuesta al cliente en un formato estándar (como JSON).

### Beneficios:
- Separa la lógica de presentación de las capas internas del sistema.
- Facilita la integración con diferentes clientes (web, móvil, etc.).
- Proporciona un único punto de acceso para los consumidores del sistema.

---

# Documentación de la API de Tareas

## Endpoints

---

### **1. Crear Tarea**
**POST** `/api/task`

Crea una nueva tarea con los detalles proporcionados.

- **Cuerpo de la solicitud:**
  ```json
  {
  "descriptionAboutTask": "Descripcion de la tarea",
  "statusTask": estado de la tarea ,
  "DuaDate": "fecha de venciemiento",
  "additionalData": nivel de acceso (int)
}

## **2. Obtener Todas las Tareas**
**GET** `/api/task`

Recupera todas las tareas.

- **Respuesta:**
  - **200 OK**: Devuelve una lista de tareas.

---

## **3. Filtrar Tareas por Estado**
**GET** `/api/task/{status}`

Recupera tareas filtradas por su estado.

- **Parámetros de ruta:**
  - `status` (enum): El estado por el cual filtrar (por ejemplo, `Pendiente`, `Completada`).

- **Respuesta:**
  - **200 OK**: Devuelve una lista de tareas con el estado especificado.
  - **400 Bad Request**: Si el filtro falla.

---

## **4. Obtener Tarea por ID**
**GET** `/api/task/{id}`

Recupera una tarea específica por su ID.

- **Parámetros de ruta:**
  - `id` (GUID): El ID de la tarea a recuperar.

- **Respuesta:**
  - **200 OK**: Devuelve la tarea con el ID especificado.
  - **404 Not Found**: Si no se encuentra la tarea con el ID proporcionado.

---

## **5. Eliminar Tarea**
**DELETE** `/api/task/{id}`

Elimina una tarea específica por su ID.

- **Parámetros de ruta:**
  - `id` (GUID): El ID de la tarea a eliminar.

- **Respuesta:**
  - **204 No Content**: Si la tarea se elimina correctamente.
  - **404 Not Found**: Si no se encuentra la tarea con el ID proporcionado.

---

## **6. Actualizar Tarea**
**PUT** `/api/task`

Actualiza una tarea con el ID y los detalles proporcionados.

- **Parámetros de consulta:**
  - `id` (GUID): El ID de la tarea a actualizar.

- **Cuerpo de la solicitud:**
  ```json
  { 
    "description": "Nueva descripción de la tarea",
    "status": "Completada"
  }