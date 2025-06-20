﻿# Casino Vet – Adriel Maldonado

Este proyecto es una simulación del juego de la ruleta. Permite a un usuario realizar diferentes tipos de apuestas con su saldo disponible, visualizar su saldo actual y guardar el estado de la partida.

## Tecnologías usadas

- **Frontend:** Vue 3 + Vite + Vuetify
- **Backend:** ASP.NET Core Web API (.NET 8)
- **Base de datos:** SQL Server

---

## Cómo ejecutar el proyecto

### Backend (ASP.NET Core)

1. Abre el proyecto en **Visual Studio**.
2. Ejecuta con **IIS Express** (F5).
3. Swagger estará disponible en:  
   https://localhost:44346/swagger/index.html

---

### Frontend (Vue 3)

1. Entra en la carpeta del frontend:
   ```cd frontend-ruleta```
2. Instala dependencias:
   ```npm install```
3. Ejecuta el servidor de desarrollo:
   ```npm run dev```
4. Visita en tu navegador::
   ```http://localhost:5173 (o el puerto que indique en consola)```

---

### Endpoint de prueba
- `GET https://localhost:44346/api/Ruleta/girar`

---

### Usuario de prueba:
Para probar la funcionalidad puedes usar:
- **Nombre de usuario:** Gambito

## Funcionalidades principales

- Apuestas por número específico, color, o paridad + color.
- Validación del monto ingresado contra el saldo disponible.
- Guardado de partidas con persistencia de saldo.
- Respuesta visual inmediata (snackbar) en cada acción del usuario.
- API REST con consulta de usuario de prueba.

---

### Notas Adicionales

- La API puede ser testeada desde Swagger.
- Se maneja validación tanto en el frontend como en el backend.
- El diseño es responsivo y funcional.
