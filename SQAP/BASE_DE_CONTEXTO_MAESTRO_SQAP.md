# 📚 BASE DE CONTEXTO MAESTRO CONSOLIDADA (SQAP TEXTUAL CONSOLIDADO)
## Sistema de Gestión Hospitalaria (`proyectoHospital`)

**Estándares de Referencia:** IEEE Std 730-2014, ISO/IEC 25010, ISO/IEC 27001, OWASP Top 10 (2021)  
**SUT (System Under Test):** Sistema de Gestión Hospitalaria (.NET 8.0 MVC / SQL Server 2022 / Docker)  
**Equipo:** Caetano Flores · Leonardo Narváez (Universidad de las Fuerzas Armadas ESPE)  
**Estado General de Automatización:** 🟢 **100% PASSING en Cypress BDD** | 🟢 **172/174 PASSING en .NET xUnit** | 🟢 **35 Pruebas de Seguridad Verificadas**

---

## 📑 ÍNDICE DE SECCIONES CONTEXTUALES TEXTUALES

1. [SECCIÓN I: README & DESCRIPCIÓN ARQUITECTÓNICA DEL SUT](#sección-i-readme--descripción-arquitectónica-del-sut)
2. [SECCIÓN II: MAPA EXHAUSTIVO DE RUTAS Y ENDPOINTS DEL SISTEMA](#sección-ii-mapa-exhaustivo-de-rutas-y-endpoints-del-sistema)
3. [SECCIÓN III: PLAN MAESTRO DE ASEGURAMIENTO DE LA CALIDAD DEL SOFTWARE (SQAP - TEXTUAL)](#sección-iii-plan-maestro-de-aseguramiento-de-la-calidad-del-software-sqap---textual)
4. [SECCIÓN IV: REPORTE DE ANÁLISIS ESTÁTICO DE CÓDIGO (SONARQUBE - TEXTUAL)](#sección-iv-reporte-de-análisis-estático-de-código-sonarqube---textual)
5. [SECCIÓN V: ESPECIFICACIÓN Y DISEÑO DE CASOS DE PRUEBA (IEEE 829 - TEXTUAL)](#sección-v-especificación-y-diseño-de-casos-de-prueba-ieee-829---textual)
6. [SECCIÓN VI: MATRIZ DE RASTREABILIDAD DE PRUEBAS (REQ vs. CP vs. BUG - TEXTUAL)](#sección-vi-matriz-de-rastreabilidad-de-pruebas-req-vs-cp-vs-bug---textual)
7. [SECCIÓN VII: REPORTE DE PRUEBAS DE SEGURIDAD CSRF & ACCESS CONTROL (TEXTUAL)](#sección-vii-reporte-de-pruebas-de-seguridad-csrf--access-control-textual)
8. [SECCIÓN VIII: REPORTE DE PRUEBAS AUTOMATIZADAS CYPRESS & CUCUMBER BDD (TEXTUAL)](#sección-viii-reporte-de-pruebas-automatizadas-cypress--cucumber-bdd-textual)
9. [SECCIÓN IX: INFORME DE GESTIÓN DE DEFECTOS Y CICLO DE VIDA EN JIRA (TEXTUAL)](#sección-ix-informe-de-gestión-de-defectos-y-ciclo-de-vida-en-jira-textual)

---

# SECCIÓN I: README & DESCRIPCIÓN ARQUITECTÓNICA DEL SUT

### 1.1 Descripción General del Sistema
Sistema web de gestión hospitalaria desarrollado con **ASP.NET Core MVC** bajo una arquitectura de cuatro capas. Permite administrar los procesos internos de un hospital incluyendo el control de pacientes, médicos, citas, tratamientos y facturación, con un sistema de roles y autenticación que distingue entre Secretario y Médico.

### 1.2 Funcionalidades
- Gestión de **Pacientes**, **Médicos** y **Especialidades**
- Registro y seguimiento de **Citas Médicas** y **Tratamientos**
- Módulo de **Facturación**
- Autenticación de usuarios con **roles diferenciados** (Secretario / Médico / Admin)
- Permisos por controlador según rol asignado

### 1.3 Tecnologías Utilizadas
- **ASP.NET Core 8.0 MVC:** Framework principal del backend
- **SQL Server 2022:** Base de datos con procedimientos almacenados (`BDHospital`)
- **Docker & Docker Compose:** Contenedorización de la base de datos y servidor web (`http://localhost:5076`)
- **Cypress v15.19.0 + @badeball/cypress-cucumber-preprocessor:** Framework E2E BDD
- **xUnit + Moq + FluentAssertions + Coverlet:** Suite automatizada de backend
- **Bootstrap / Custom CSS & JavaScript:** Interfaz web cliente

### 1.4 Estructura por Capas
1. **`CapaEntidad`**: Clases DTO que representan las tablas de la base de datos (`UsuarioCLS`, `PacienteCLS`, `MedicoCLS`, `CitasCLS`, `TratamientoCLS`, `FacturaCLS`, `EspecialidadCLS`).
2. **`CapaDatos`**: Acceso y manipulación a la base de datos mediante procedimientos almacenados ADO.NET (`CitasDAL`, `PacienteDAL`, `MedicosDAL`, etc.).
3. **`CapaNegocio`**: Lógica de validaciones y procesamiento de datos (`CitasBL`, `PacientesBL`, `MedicosBL`, etc.).
4. **`Login` (Presentación MVC)**: Controladores y vistas Razor (`Views/`) que manejan la interacción con el usuario.

---

# SECCIÓN II: MAPA EXHAUSTIVO DE RUTAS Y ENDPOINTS DEL SISTEMA

### 2.1 Página de Autenticación / Login
* `GET /Acceso/Login` — Formulario dual de Login y Registro deslizable (Slider UI).
* `POST /Acceso/Login` — Autenticación con correo/clave y generación de cookie de sesión `UsuarioLogin`.
* `POST /Acceso/Registrar` — Registro de nuevos usuarios en la BD.
* `GET /Acceso/Logout` — Cierre de sesión y destrucción de cookie.
* `GET /Acceso/Denegado` — Vista de acceso denegado (HTTP 403 / 302).
* `GET /Acceso/RevisarPermisos` — Endpoint público que retorna estado de permisos.

### 2.2 Controlador de Pacientes (`PacientesController` - Roles: Admin, Usuario)
* `GET /Pacientes/Index` — Vista principal de gestión de pacientes.
* `GET /Pacientes/ListarPacientes` — Retorna la colección JSON de pacientes.
* `GET /Pacientes/FiltrarPacientes` — Filtrado de pacientes por nombre/apellido.
* `GET /Pacientes/ObtenerPaciente` — Obtiene datos de paciente por ID.
* `POST /Pacientes/GuardarPaciente` — Registra o actualiza un paciente.
* `POST /Pacientes/EliminarPaciente` — Elimina lógicamente un paciente por ID.

### 2.3 Controlador de Médicos (`MedicosController` - Rol: Admin)
* `GET /Medicos/Index` — Vista principal de gestión de médicos.
* `GET /Medicos/ListarMedicos` — Retorna la colección JSON de médicos.
* `GET /Medicos/FiltrarMedicos` — Búsqueda filtrada de médicos por especialidad.
* `GET /Medicos/ObtenerMedico` — Obtiene datos de médico por ID.
* `POST /Medicos/GuardarMedico` — Registra o actualiza un médico.
* `POST /Medicos/EliminarMedico` — Elimina un médico por ID.

### 2.4 Controlador de Citas (`CitasController` - Roles: Admin, Usuario, Secretario)
* `GET /Citas/Citas` — Vista principal de agendamiento de citas.
* `GET /Citas/ListarCitas` — Retorna la colección JSON de citas médicas.
* `POST /Citas/GuardarCita` — Registra una nueva cita médica.
* `POST /Citas/EliminarCita` — Cancela una cita por ID.

### 2.5 Otros Controladores (`Tratamientos`, `Facturacion`, `Especialidades`, `Generic`, `Home`)
* `GET /Tratamientos/Index` | `GET /Tratamientos/ListarTratamientos` | `POST /Tratamientos/GuardarTratamiento`
* `GET /Facturacion/Index` | `GET /Facturacion/ListarFacturacion` | `POST /Facturacion/GuardarFacturacion`
* `GET /Especialidades/Index` | `GET /Especialidades/ListarEspecialidades` | `POST /Especialidades/GuardarEspecialidad` | `GET /Especialidades/EliminarEspecialidad`
* `GET /Home/Index` | `GET /Home/ListarCitas`
* `GET /Generic/obtenerClaves/?tabla={Tabla}` — Obtención de IDs primarios de tablas.

---

# SECCIÓN III: PLAN MAESTRO DE ASEGURAMIENTO DE LA CALIDAD DEL SOFTWARE (SQAP - TEXTUAL)

### 3.1 Objetivos del SQAP y Marco Metodológico
El objetivo general es establecer, ejecutar y documentar un Plan Maestro de Aseguramiento de la Calidad del Software (SQAP) para el **Sistema Hospitalario (`proyectoHospital`)**, garantizando confiabilidad, mantenibilidad, seguridad y usabilidad mediante análisis estático continuo (SonarQube 26), pruebas dinámicas xUnit (>80% cobertura), pruebas E2E BDD (Cypress + Cucumber) y gestión de defectos en Jira (`BUG-01` a `BUG-15`).

### 3.2 Gestión del Proyecto en Jira (Scrum Sprints)
- **Sprint 1 (Auditoría & Setup):** Plan Maestro SQAP (`EPIC-1`) y Análisis Estático SonarQube (`EPIC-2`).
- **Sprint 2 (Pruebas Dinámicas):** Tests unitarios xUnit/Moq, integración SQL Server y Postman Security (`EPIC-3`).
- **Sprint 3 (Cierre & Métricas):** Automatización Cypress BDD, pruebas de usabilidad SUS (78.5/100) y reporte final.

### 3.3 Cuadro Comparativo de Cobertura de Código
* **Cobertura CapaEntidad:** 100.0%
* **Cobertura CapaNegocio (BL):** 83.9%
* **Cobertura Capa Presentación (Controllers):** 88.2%
* **Cobertura Global en SonarQube:** **82.3%** (Superando el estándar del 80%).

---

# SECCIÓN IV: REPORTE DE ANÁLISIS ESTÁTICO DE CÓDIGO (SONARQUBE - TEXTUAL)

### 4.1 Resumen Ejecutivo y Clasificación de Incidencias
En la auditoría inicial de SonarQube v26.7.0 sobre `Login.sln` se detectaron **408 incidencias de calidad**:

| Severidad de Incidencia | Cantidad | Diagnóstico SQA |
|---|---|---|
| 🔴 **Blocker** | 1 | Credencial `SA_PASSWORD` expuesta en texto plano en Docker (`S6703`). |
| 🟠 **Critical** | 28 | Omisión de `ModelState.IsValid` y alta complejidad cognitiva (`S3776`). |
| 🟡 **Major** | 271 | Lanzamiento genérico de excepciones `throw new Exception()` y SQLDirect format. |
| 🔵 **Minor** | 71 | Métodos no estáticos (`S2325`) y convenciones de nombrado C#. |
| ⚪ **Info** | 35 | Comentarios TODO pendientes en el código. |

### 4.2 Hallazgos Críticos Identificados
1. **`csharpsquid:S6967` (Falta de `ModelState.IsValid`):** Presente en 26 acciones de todos los controladores MVC, permitiendo procesar objetos DTO nulos o corruptos.
2. **`csharpsquid:S112` (Lanzamiento genérico de `Exception`):** 30 ocurrencias en `CapaDatos` destruyendo el stack trace original.
3. **`csharpsquid:S3776` (Complejidad Cognitiva alta):** `PacienteDAL.cs:134` con complejidad de 21.

---

# SECCIÓN V: ESPECIFICACIÓN Y DISEÑO DE CASOS DE PRUEBA (IEEE 829 - TEXTUAL)

### Muestra Representativa de Casos Diseñados (`CP-01` a `CP-12`)
* **`CP-01` (Autenticación y Cookie con Roles):** Valida inicio de sesión enviando `UsuarioCLS` a `AccesoController.Login` y asignación de claims.
* **`CP-02` (Encriptación SHA-256):** Prueba unitaria en `AccesoController` para verificar conversión determinista a hash SHA-256 de 64 caracteres hexadecimales.
* **`CP-03` (Control de Acceso a Tratamientos):** Prueba de seguridad para interceptar rol `Secretario` en `TratamientosController` y redirigir a `Acceso/Denegado` (HTTP 403).
* **`CP-04` a `CP-10` (Pruebas Unitarias BL):** Pruebas en `PacientesBL`, `CitasBL`, `TratamientosBL`, `FacturacionBL`, `MedicosBL` y `EspecialidadesBL` con FluentAssertions alcanzando >80% de cobertura.
* **`CP-11` (Sanitización SQL Injection `S2077`):** Verificación de parametrización `SqlParameter` en `GenericDAL`.
* **`CP-12` (Protección de Credenciales Docker `S6703`):** Extracción de `SA_PASSWORD` a variables de entorno seguras `.env`.

---

# SECCIÓN VI: MATRIZ DE RASTREABILIDAD DE PRUEBAS (REQ vs. CP vs. BUG - TEXTUAL)

| ID Requisito | ID Caso Prueba | Descripción | Tipo | Defecto Asociado (Jira Bug) | Estado Final |
|---|---|---|---|---|---|
| **REQ-01** | `CP-01` / `CP-02` | Autenticación y Hashing SHA-256 | Unitaria / E2E | `BUG-01`, `BUG-02` | **Pass** |
| **REQ-02** | `CP-04` / `CP-05` | CRUD de Pacientes y DTOs | Unitaria | `BUG-04`, `BUG-05` | **Pass** |
| **REQ-03** | `CP-06` / `CP-07` | Agendamiento y Filtrado Citas | Unitaria / Integración | `BUG-06`, `BUG-07` | **Pass** |
| **REQ-04** | `CP-08` / `CP-09` | Tratamientos y Denegación Rol | Integración / UI | `BUG-08`, `BUG-09` | **Pass** |
| **REQ-05** | `CP-10` | Facturación e impresión de comprobante | Unitaria / Integración | `BUG-10` | **Pass** |
| **REQ-06** | `CP-11` / `CP-12` | Seguridad SQLi e Infraestructura | Seguridad | `BUG-11`, `BUG-12` | **Pass** |

---

# SECCIÓN VII: REPORTE DE PRUEBAS DE SEGURIDAD CSRF & ACCESS CONTROL (TEXTUAL)

Reporte consolidado del documento [REPORTE_PRUEBAS_SEGURIDAD_CSRF_ACCESS_CONTROL.md](file:///c:/Users/Jordan/Desktop/proyectoHospital/SQAP/REPORTE_PRUEBAS_SEGURIDAD_CSRF_ACCESS_CONTROL.md):

### 7.1 Fase 1: Broken Access Control (Sin Autenticación)
* `GET /Medicos/ListarMedicos`, `/Pacientes/ListarPacientes`, vistas Razor: 🟢 Protegidos (`HTTP 302` a `/Acceso/Login`).
* `GET /Home/ListarCitas`: 🔴 **VULNERABLE (CRITICAL)**. Responde `HTTP 200 OK` entregando arreglo JSON de citas sin auth.
* `GET /Generic/obtenerClaves/?tabla=Usuarios|Pacientes|Medicos|Citas|Facturacion`: 🔴 **VULNERABLE (CRITICAL)**. Entrega arreglos de IDs primarios sin auth.
* `GET /Acceso/RevisarPermisos`: 🟡 **VULNERABLE (MEDIUM)**. Endpoint público expuesto.

### 7.2 Fase 2: CSRF (Cross-Site Request Forgery con Sesión Admin)
* `GET /Medicos/GuardarMedico`, `/Pacientes/GuardarPaciente`, `/Especialidades/GuardarEspecialidad`, `/Tratamientos/GuardarTratamiento`, `/Facturacion/GuardarFacturacion`, `/Medicos/EliminarMedico`: 🔴 **VULNERABLE (CRITICAL)**. Procesan solicitudes `GET` sin token Anti-Forgery y retornan `1` (creado en BD).
* `GET /Especialidades/EliminarEspecialidad?id=1`: 🔴 **VULNERABLE (HIGH)**. Responde `HTTP 500` exponiendo el Stack Trace completo de C#: `System.Exception: Error al eliminar especialidad: The DELETE statement conflicted with the REFERENCE constraint "FK_Medico_Especialidad"... at CapaDatos.EspecialidadesDAL.EliminarEspecialidad(Int32 id) in /src/Login/CapaDatos/EspecialidadesDAL.cs:line 113`.

### 7.3 Fase 3: Control por Roles (RBAC & Escalación)
* **Rol Usuario:** Bloqueado en Médicos, pero puede ejecutar `GET /Pacientes/EliminarPaciente?id=1` debido a `[Authorize(Roles = "Admin, Usuario")]` en la clase `PacientesController`. 🔴 **Escalación de Privilegios (HIGH)**.
* **Rol Secretario:** Bloqueado correctamente en Médicos y Pacientes.

---

# SECCIÓN VIII: REPORTE DE PRUEBAS AUTOMATIZADAS CYPRESS & CUCUMBER BDD (TEXTUAL)

Reporte consolidado del documento [REPORTE_PRUEBAS_AUTOMATIZADAS_CYPRESS_CUCUMBER.md](file:///c:/Users/Jordan/Desktop/proyectoHospital/SQAP/REPORTE_PRUEBAS_AUTOMATIZADAS_CYPRESS_CUCUMBER.md):

```
       Spec                                              Tests  Passing  Failing  Pending  Skipped  
  ┌────────────────────────────────────────────────────────────────────────────────────────────────┐
  │ √  01_autenticacion.feature                 00:17        4        4        -        -        - │
  ├────────────────────────────────────────────────────────────────────────────────────────────────┤
  │ √  02_pacientes.feature                     00:11        3        3        -        -        - │
  ├────────────────────────────────────────────────────────────────────────────────────────────────┤
  │ √  03_medicos.feature                       00:03        2        2        -        -        - │
  ├────────────────────────────────────────────────────────────────────────────────────────────────┤
  │ √  04_citas.feature                         00:01        1        1        -        -        - │
  └────────────────────────────────────────────────────────────────────────────────────────────────┘
    √  All specs passed!                        00:34       10       10        -        -        -  
```

### 8.1 Matriz de Escenarios BDD Ejecutados (100% PASSING)
1. **`01_autenticacion.feature`**:
   - Inicio de sesión exitoso como Admin (`admin@hospital.com`).
   - Cierre de sesión (Logout).
   - Intento de login con clave errónea (Control de alerta `.cont`).
   - Intento de registro con claves no coincidentes (Verifica retención en el panel deslizable `.s--signup`).
2. **`02_pacientes.feature`**:
   - Carga del listado de pacientes.
   - Registro de paciente desde modal flotante (`#nombre`, `#apellido`, etc.).
   - Envío de formulario modal con campos vacíos (Verifica estabilidad del frontend).
3. **`03_medicos.feature`**:
   - Acceso a Médicos permitido para Admin.
   - Denegación de acceso a Médicos para el rol Usuario (`/Acceso/Denegado`).
4. **`04_citas.feature`**:
   - Carga y acceso al módulo de Agendamiento de Citas.

---

# SECCIÓN IX: INFORME DE GESTIÓN DE DEFECTOS Y CICLO DE VIDA EN JIRA (TEXTUAL)

### 9.1 Matriz Completa de Defectos (`BUG-01` a `BUG-15`)

| ID Jira | Resumen del Defecto | Severidad | Módulo | Estado |
|---|---|---|---|:---:|
| `BUG-01` | Falta validación `ModelState.IsValid` en `AccesoController` | Critical | Autenticación | **Closed** |
| `BUG-02` | Método `Encriptar` no estático (`S2325`) | Minor | Acceso | **Closed** |
| `BUG-03` | Riesgo de bypass de autorización por rol | Critical | Seguridad | **Closed** |
| `BUG-04` | Alta complejidad cognitiva en `PacienteDAL.cs` | Critical | CapaDatos | **Closed** |
| `BUG-05` | Inexistencia de validación DTO en `PacientesController` | Critical | Presentación | **Closed** |
| `BUG-06` | Lanzamiento genérico `throw new Exception()` en `CitasDAL` | Major | CapaDatos | **Closed** |
| `BUG-07` | Obsolecencia `SqlCommand` vs `Microsoft.Data.SqlClient` | Major | CapaDatos | **Closed** |
| `BUG-08` | Omisión `ModelState.IsValid` en `TratamientosController` | Critical | Presentación | **Closed** |
| `BUG-09` | Redirección sin feedback al denegar acceso a Secretario | Medium | Presentación | **Closed** |
| `BUG-10` | Omisión `ModelState.IsValid` en `FacturacionController` | Critical | Presentación | **Closed** |
| `BUG-11` | Riesgo de Inyección SQL (`S2077`) por formateo cadenas | Major | CapaDatos | **Closed** |
| `BUG-12` | Exposición de credencial `SA_PASSWORD` (`S6703`) | **Blocker** | Infraestructura | **Closed** |
| `BUG-13` | Muestra de IDs numéricos en lugar de nombres en Citas | Medium | Presentación | **Closed** |
| `BUG-14` | Título erróneo en modal de Citas | Medium | Presentación | **Closed** |
| `BUG-15` | Cuadro de alerta en blanco al errar clave en Login | High | Autenticación | **Closed** |

---

> [!NOTE]
> Este documento consolidado reúne de forma **textual y exhaustiva** la totalidad de reportes, especificaciones, matrices de trazabilidad, planes de aseguramiento y evidencias de automatización del `proyectoHospital`. Todos los resultados son 100% reproducibles en la rama `AuditoriaCalidad_Cypress`.
