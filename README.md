# Back_horario

# 🗓️ Sistema de Gestión de Horarios

Este proyecto es una aplicación web para gestionar horarios académicos semanales, desarrollada con un backend en **ASP.NET Core** y un frontend en **Vue 3**. Soporta autenticación mediante JWT, registro de usuarios, asignación de roles y funcionalidades completas para manejar grupos, temas, horarios y actividades.

## 🚀 Tecnologías utilizadas

### Backend (.NET 8)
- ASP.NET Core Web API
- Entity Framework Core
- JWT (JSON Web Tokens) para autenticación
- BCrypt.Net para hashing de contraseñas
- SQL Server

---

## 🧠 Características

- Inicio de sesión con verificación de contraseña hasheada (bcrypt)
- Gestión de usuarios con roles (Admin, Usuario)
- Creación y administración de:
  - Grupos
  - Temas con colores personalizados
  - Horarios (con tareas, descripciones, lugar, grupo, tema y usuario)
  - Actividades dentro de un horario
- Vista principal basada en calendario semanal
- Seeder para inicializar la base de datos con datos estáticos
- Servicios y repositorios genéricos con logging y filtros opcionales

---

## ⚙️ Instalación y configuración

### Requisitos
- .NET 8 SDK
- Node.js y npm
- SQL Server
- Visual Studio o VS Code

### Backend

1. Clona el repositorio:

```bash
git clone https://github.com/tuusuario/proyecto-horarios.git
```

2. Navega al proyecto:

```bash
cd proyecto-horarios/Back_horario
```

3. Restaura paquetes y ejecuta migraciones:

```bash
dotnet restore
dotnet ef database update
```

4. Ejecuta el proyecto:

```bash
dotnet run
```

---

## 🔧 Migraciones con Entity Framework Core

Para aplicar cambios al modelo o agregar nuevos datos iniciales:

1. **Agregar una migración**:

```bash
dotnet ef migrations add NombreDeLaMigracion
```

2. **Aplicar la migración a la base de datos**:

```bash
dotnet ef database update
```

3. Si usas Visual Studio con consola de administrador de paquetes (PMC):

```powershell
Add-Migration NombreDeLaMigracion
Update-Database
```

> ⚠️ Evita usar valores dinámicos (`DateTime.Now`, `Guid.NewGuid()`) en `HasData()` para no tener advertencias o errores en las migraciones.

---

## 🔑 Usuarios de prueba (semilla)

```text
Admin:
  Correo: juan.perez@admin.com
  Contraseña: admin123

Usuario:
  Correo: ana.lopez@usuario.com
  Contraseña: usuario123
```

---

## ✅ Notas adicionales

- Si usas `HasData()` con contraseñas hasheadas, asegúrate de que los valores sean estáticos.
- Agrega `.vs/`, `bin/`, `obj/` y `node_modules/` a `.gitignore`.

---

## 📬 Contribuciones

Si deseas contribuir, crea un fork, haz tus cambios en una nueva rama y abre un Pull Request 🚀.

---

## 🛡️ Licencia

Este proyecto está bajo la Licencia MIT.

