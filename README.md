# 📌 App de Control de Supervisores

Aplicación web para registrar, validar y centralizar los reportes diarios de supervisores, con control de horarios, kilómetros, actividades y calificaciones.  
Incluye backend en **.NET 7** y frontend en **Angular 17**.

---

## 📍 Problemática que resuelve

En muchas empresas que dependen de la supervisión de personal en campo, los reportes diarios se registran en papel o en planillas dispersas. Esto genera:

- Pérdida de información.
- Errores en horarios y cálculo de horas trabajadas.
- Dificultad para validar datos en tiempo real.
- Falta de un historial centralizado.
- Procesos lentos para revisión por parte de gerentes.

**Esta app resuelve eso** ofreciendo una plataforma unificada donde:
- Los supervisores registran sus reportes desde cualquier dispositivo.
- Los datos se validan automáticamente para evitar errores.
- Los gerentes pueden acceder a reportes en tiempo real.
- Toda la información queda centralizada y segura.

---

## 📋 Requerimientos y Funcionalidades

### 🔹 Funcionalidades principales
- **Registro de reportes diarios** con:
  - Controlador asignado (usuario logueado).
  - Vehículo utilizado.
  - Fecha laboral automática.
  - Hora de inicio y fin.
  - Kilómetros iniciales y finales (con cálculo automático).
- **Detalles de reporte**:
  - Objetivo asignado.
  - Vigilador responsable.
  - Horario de entrada y salida.
  - Actividad realizada.
  - Calificaciones por ítem (0 a 5).
- **Validaciones automáticas**:
  - Horas no solapadas entre detalles.
  - Horarios dentro del rango del reporte.
  - Vigilador no asignado a distintos objetivos en el mismo horario.
  - Calificaciones válidas (0 a 5).
- **Cálculos automáticos**:
  - Total de horas trabajadas.
  - Horas de supervisión, apoyo y traslado.
- **Roles de usuario**:
  - `gerente` → Acceso total.
  - `controlador/supervisor` → Crea y edita sus reportes.

---

## 🚀 Tecnologías utilizadas

### **Backend**
- .NET 7 / C#
- Entity Framework Core
- MariaDB
- Identity + JWT
- Swagger para documentación de API

### **Frontend**
- Angular 17
- TypeScript
- Bootstrap 5
- ngx-toastr (notificaciones)
- sweetalert2 (confirmaciones)

---

## 📂 Estructura del repositorio

```
/backend        → API en .NET
/frontend       → Aplicación Angular
```

Repositorio: [https://github.com/MDelgado23/SupervisoresApp.git](https://github.com/MDelgado23/SupervisoresApp.git)

---

## ⚙️ Requisitos previos

Antes de iniciar, asegurate de tener instalado:

- [Node.js](https://nodejs.org/) (v18 o superior)
- [Angular CLI](https://angular.io/cli)
- [.NET 7 SDK](https://dotnet.microsoft.com/download/dotnet/7.0)
- [MariaDB](https://mariadb.org/) o MySQL
- Git

---

## 🛠 Configuración del Backend (.NET)

1. **Clonar repositorio**
   ```bash
   git clone https://github.com/MDelgado23/SupervisoresApp.git
   cd backend
   ```

2. **Configurar cadena de conexión**
   - Editar `appsettings.json`:
     ```json
     "ConnectionStrings": {
       "DefaultConnection": "server=localhost;database=supervisores_db;user=root;password=tu_password"
     },
     "Jwt": {
       "Key": "clave_secreta_super_segura",
       "Issuer": "http://localhost:5000"
     }
     ```

3. **Restaurar dependencias**
   ```bash
   dotnet restore
   ```

4. **Aplicar migraciones**
   ```bash
   dotnet ef database update
   ```

5. **Levantar el servidor**
   ```bash
   dotnet run
   ```
   Por defecto se ejecutará en:
   ```
   Backend API: http://localhost:5000
   Swagger UI:  http://localhost:5000/swagger
   ```

---

## 🖥 Configuración del Frontend (Angular)

1. **Entrar a la carpeta frontend**
   ```bash
   cd ../frontend
   ```

2. **Instalar dependencias**
   ```bash
   npm install
   ```

3. **Configurar variables de entorno**
   - Editar `src/environments/environment.ts`:
     ```ts
     export const environment = {
       production: false,
       apiUrl: 'http://localhost:5000/api'
     };
     ```

4. **Levantar servidor de desarrollo**
   ```bash
   ng serve
   ```
   La app estará disponible en:
   ```
   http://localhost:4200
   ```

---

## 🔐 Autenticación y Roles

- El login se realiza vía **JWT**.
- Roles disponibles:
  - `gerente` → Acceso total.
  - `controlador` → Crea y edita sus reportes.
  - `supervisor` → Visualización y seguimiento.

En Swagger podés autenticarte desde el botón **"Authorize"** pegando el token obtenido al loguearte.

---

## 📖 Uso de Swagger

1. Levantar el backend.
2. Ir a:  
   ```
   http://localhost:5000/swagger
   ```
3. Autenticarse con un token (opcional para endpoints protegidos).
4. Probar endpoints directamente desde la interfaz.

---

## 📦 Despliegue en Producción

### **Backend**
- Se puede desplegar en [Render](https://render.com/) o en un VPS.
- Usar base de datos MariaDB en Railway o similar.
- Configurar variables de entorno:
  - `ConnectionStrings__DefaultConnection`
  - `Jwt__Key`
  - `Jwt__Issuer`

### **Frontend**
- Compilar para producción:
  ```bash
  ng build --configuration production
  ```
- Subir carpeta `dist/frontend/browser` a:
  - Firebase Hosting
  - Vercel
  - Netlify

---

## 📅 Futuras mejoras

- Geolocalización en tiempo real.
- Adjuntar imágenes en detalles.
- Dashboard con métricas.
- Exportación de reportes a PDF/Excel.

---

## 👨‍💻 Autor

**Martin Delgado**  
Desarrollador Full Stack  
📧 martinfernandodelgado23@gmail.com 
🌐 [GitHub](https://github.com/MDelgado23)
