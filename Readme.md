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