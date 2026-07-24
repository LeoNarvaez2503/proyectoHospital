# Guía de Levantamiento y Desarrollo del Proyecto - Sistema Hospitalario

Esta guía contiene los requisitos e instrucciones detalladas para levantar y desarrollar el proyecto **proyectoHospital** en cualquier sistema operativo (**Linux, macOS o Windows**).

---

## 📋 Requisitos Previos

Antes de comenzar, asegúrate de contar con los siguientes programas instalados en tu sistema operativo:

### 1. Requisito Principal (Recomendado)
- **Docker y Docker Compose**:
  - **Windows / macOS**: Instalar [Docker Desktop](https://www.docker.com/products/docker-desktop/).
  - **Linux (Ubuntu/Debian)**: Instalar Docker Engine y el plugin Docker Compose (`sudo apt install docker.io docker-compose-v2`).

### 2. Requisitos Opcionales (Solo para desarrollo local sin Docker)
- **.NET 8.0 SDK**: [Descargar .NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- **Git**: Para clonar el repositorio ([Descargar Git](https://git-scm.com/)).

---

## 🚀 Opción 1: Levantamiento Rápido con Docker (Cualquier OS)

Esta es la opción recomendada, ya que empaqueta la base de datos SQL Server 2022 y la aplicación .NET 8.0 en contenedores sin necesidad de instalar SQL Server localmente.

### Pasos a ejecutar en tu terminal:

1. **Clonar el repositorio** (si aún no lo has hecho):
   ```bash
   git clone https://github.com/LeoNarvaez2503/proyectoHospital.git
   cd proyectoHospital
   ```

2. **Levantar los servicios con Docker Compose**:
   ```bash
   docker compose up --build
   ```
   *(Nota: En sistemas Linux, si tu usuario no está en el grupo docker, ejecuta con `sudo docker compose up --build`)*.

3. **Abrir la aplicación**:
   Una vez que los contenedores estén corriendo, abre tu navegador e ingresa a:
   👉 **http://localhost:5076**

---

## 💻 Opción 2: Desarrollo Local con .NET SDK (Modo Híbrido)

Si deseas modificar código en tiempo real y depurar la aplicación usando tu IDE (Visual Studio, VS Code, JetBrains Rider) sin empaquetar la app en Docker cada vez:

### Pasos:

1. **Levantar únicamente la Base de Datos SQL Server en Docker**:
   ```bash
   docker compose up -d sqlserver
   ```

2. **Navegar a la carpeta de la solución**:
   ```bash
   cd Login
   ```

3. **Restaurar las dependencias del proyecto**:
   ```bash
   dotnet restore
   ```

4. **Ejecutar la aplicación .NET**:
   ```bash
   dotnet run --project Login/Login.csproj
   ```

5. **Acceder a la aplicación**:
   Abre tu navegador en la URL mostrada en la terminal (usualmente `http://localhost:5076` o `http://localhost:5000`).

---

## 🔑 Credenciales de Prueba Preconfiguradas

La base de datos se inicializa automáticamente con la estructura necesaria y las siguientes cuentas de prueba para cada rol:

| Rol | Correo Electrónico | Contraseña | Permisos de Acceso |
| :--- | :--- | :--- | :--- |
| **Admin** | `admin@hospital.com` | `Admin123!` | Acceso completo (Médicos, Pacientes, Citas, Tratamientos, Facturación, Especialidades) |
| **Usuario** | `usuario@hospital.com` | `Usuario123!` | Acceso intermedio (Pacientes, Citas, Facturación) |
| **Secretario** | `secretario@hospital.com` | `Secretario123!` | Rol inicial / consulta básica (`Home`, `Privacy`) |

> **Nota**: También puedes registrar un nuevo usuario desde el formulario **"Registrarse"** de la aplicación web.

---

## 🛠️ Comandos Útiles para la Terminal

### Ver logs en tiempo real:
```bash
docker compose logs -f app
```

### Detener los servicios:
```bash
docker compose down
```

### Reconstruir los contenedores desde cero (Sin caché):
Útil si hiciste cambios de código o configuración y Docker no los toma automáticamente:
```bash
docker compose build --no-cache
docker compose up -d
```

### Reiniciar la base de datos desde cero (Borrar datos almacenados):
```bash
docker compose down -v
docker compose up --build
```

---

## 🧪 Pruebas y Auditoría de Calidad (SQAP)

El proyecto cuenta con un entorno configurado para el **Aseguramiento de la Calidad de Software (SQAP)** mediante análisis estático y dinámico:

### 1. Auditoría Estática con SonarQube
Para ejecutar el análisis estático de código, detectar vulnerabilidades y calcular la deuda técnica:
1. Inicia el servidor local de SonarQube en Docker (`docker start sonarqube`).
2. Sigue los pasos detallados en la guía de pruebas: [GUIA_ANALISIS_ESTATICO_SONARQUBE.md](file:///home/meatpuppets/Escritorio/University/proyectoHospital/AreadePruebas/GUIA_ANALISIS_ESTATICO_SONARQUBE.md).
3. Revisa los hallazgos en el Dashboard: `http://localhost:9000/dashboard?id=proyectoHospital`.

### 2. Documentación y Evidencias SQAP
Toda la documentación metodológica, el plan maestro, los reportes de análisis estático y los CSV para importar en Jira se encuentran organizados en la carpeta [SQAP/](file:///home/meatpuppets/Escritorio/University/proyectoHospital/SQAP).

