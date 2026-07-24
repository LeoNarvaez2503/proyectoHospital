# PLAN DE ASEGURAMIENTO DE LA CALIDAD DE SOFTWARE (SQAP)
## Sistema de Gestión Hospitalaria (System Under Test - SUT)

**Equipo de Calidad Externa:** Caetano Flores · Leonardo Narváez  
**Institución:** Universidad de las Fuerzas Armadas ESPE  
**Asignatura:** Aseguramiento de la Calidad de Software  
**Docente:** Ing. Diego Leonardo Gamboa Mgtr.  
**Fecha:** Julio 2026  

---

## 1. INTRODUCCIÓN Y DESCRIPCIÓN DEL ESCENARIO
El presente documento constituye el **Plan Maestro de Aseguramiento de la Calidad (SQAP)** elaborado para auditar, evaluar y garantizar la confiabilidad del **Sistema de Gestión Hospitalaria**, un software legado desarrollado bajo arquitectura en 4 capas (Entidad, Datos, Negocio y Presentación) utilizando **ASP.NET Core 9.0 MVC**, C# y Microsoft SQL Server.

El proceso se realiza bajo la perspectiva de una **Consultora de Calidad Externa**, abordando el ciclo de aseguramiento en un Sprint de 3 Semanas: auditando el estado actual del código sin correcciones apresuradas, definiendo estrategias de prueba estática y dinámica, gestionando el ciclo de vida de defectos y generando la evidencia requerida para dictaminar la idoneidad del paso a producción.

---

## 2. SECCIÓN A: PLANIFICACIÓN Y ESTRATEGIA DE CALIDAD

### 2.1. ALCANCE (SCOPE)

#### A. Módulos y Componentes Incluidos (In-Scope)
El alcance de las actividades de auditoría y pruebas abarca los módulos críticos de la arquitectura:
1. **Módulo de Autenticación y Autorización (`AccesoController`, `UsuarioBL`, `UsuarioDAL`):**
   - Flujo de inicio de sesión de usuarios.
   - Verificación de asignación de roles (`Secretario` y `Médico`).
   - Control de acceso a controladores mediante atributos `[Authorize(Roles = "...")]` y filtros personalizados (`FiltradoAttribute`).
2. **Módulo de Gestión de Pacientes (`PacientesController`, `PacienteBL`, `PacienteDAL`):**
   - Operaciones de consulta, registro y actualización de pacientes.
   - Validaciones de negocio sobre datos personales e identificación.
3. **Módulo de Gestión de Citas Médicas (`CitasController`, `CitasBL`, `CitasDAL`):**
   - Agendamiento, filtrado, cambio de estado y cancelación de citas médicas.
   - Vinculación bidireccional Paciente - Médico - Fecha.
4. **Módulo de Gestión de Médicos y Especialidades (`MedicosController`, `EspecialidadesController`, `MedicoBL`, `EspecialidadBL`):**
   - Registro de profesionales médicos y su categorización por especialidad.
5. **Módulo de Tratamientos Médicos (`TratamientosController`, `TratamientoBL`, `TratamientoDAL`):**
   - Registro de diagnósticos y recetas asociado exclusivamente al rol `Médico`.
6. **Módulo de Facturación (`FacturacionController`, `FacturaBL`, `FacturaDAL`):**
   - Emisión y consulta de comprobantes asignado al rol `Secretario`.
7. **Capa de Datos y Procedimientos Almacenados (`CapaDatos`, SQL Server):**
   - Verificación del manejo de conexiones en `CadenaDAL`, llamadas a Stored Procedures (`uspListarCitas`, `uspGuardarPaciente`, etc.), sanitización contra SQL Injection y manejo de excepciones (`BDErrorDAL`).

#### B. Módulos y Aspectos Excluidos (Out-of-Scope)
1. **Componentes Plantilla y Auxiliares (`GenericController`, `PaginaController`, `BotonController`, `TipoUsuarioController`):**
   - Controladores y vistas heredados de prototipos base que no forman parte de los flujos clínicos/administrativos centrales.
2. **Despliegue e Infraestructura en Producción Cloud:**
   - Quedan fuera del alcance las pruebas en entornos AWS/Azure; el software se probará en el entorno local containerizado (Docker Compose + SQL Server 2022).
3. **Pruebas de Carga Extrema y Estrés Masivo (>10,000 usuarios concurrentes):**
   - Debido a la restricción temporal del Sprint de 3 semanas, se priorizarán las pruebas estáticas, unitarias, de integración y funcionales.

---

### 2.2. RECURSOS Y ENTORNO DE PRUEBAS

#### A. Recursos Humanos y Roles
| Rol | Integrante / Responsable | Responsabilidades Clave |
|---|---|---|
| **QA Lead / Auditor Principal** | Caetano Flores / Leonardo Narváez | Elaboración del SQAP, definición de métricas, diseño del Plan Maestro y aprobación de informes. |
| **Analista de Pruebas Estáticas** | Caetano Flores / Leonardo Narváez | Configuración de linters, escaneo estático de código C# (Roslyn/SonarQube), clasificación de code smells y vulnerabilidades. |
| **Ingeniero de Automatización & Tests** | Caetano Flores / Leonardo Narváez | Desarrollo de casos de prueba unitarios (xUnit + Moq), pruebas de integración y automatización de flujos E2E. |

#### B. Entorno de Pruebas (Test Environment)
- **Sistema Operativo Host:** Linux / Ubuntu Environment.
- **Backend Framework:** .NET 9.0 SDK / ASP.NET Core MVC (C#).
- **Base de Datos:** Microsoft SQL Server 2022 corriendo en contenedor Docker (`sqlserver`), con base de datos `BDHospitalF` inicializada mediante script SQL/Restore.
- **Frontend / Cliente:** Navegadores Google Chrome / Headless Chromium para pruebas E2E.
- **Aislamiento de Entorno:** Red interna de Docker Compose (`hospital_network`).

---

### 2.3. STACK TECNOLÓGICO Y JUSTIFICACIÓN DE HERRAMIENTAS

| Fase / Categoría | Herramienta Seleccionada | Justificación Técnica y Metodológica |
|---|---|---|
| **Análisis Estático de Código** | Roslyn Analyzers (.NET) & ESLint / Security Rules | Herramientas integradas en el ecosistema .NET que evalúan el AST (Abstract Syntax Tree) de C# en busca de vulnerabilidades (OWASP), violaciones de nombrado, nulos no controlados y fuga de recursos sin alterar la compilación. |
| **Pruebas Unitarias y de Integración** | xUnit + Moq + FluentAssertions | **xUnit** es el estándar moderno en la plataforma .NET para pruebas unitarias de alto rendimiento. **Moq** permite aislar la `CapaNegocio` de la `CapaDatos`, simulando el comportamiento de SQL Server sin depender de la BD. **FluentAssertions** mejora la legibilidad de las aserciones. |
| **Pruebas Funcionales / E2E** | Cypress / Selenium WebDriver | Permite simular interacciones reales de usuario en el navegador (login como Secretario, agendamiento de citas, verificación de restricción a Tratamientos). |
| **Gestión de Incidencias y Trazabilidad** | Matriz de Trazabilidad CSV / GitHub Issues / Jira | Permite realizar el seguimiento completo del ciclo de vida de los defectos (Detección -> Reporte -> Verificación) y vincular Requisitos <-> Casos de Prueba <-> Defects. |

---

### 2.4. GESTIÓN DE RIESGOS

#### A. Riesgos del Producto (Product Risks)
1. **RP-01: Vulnerabilidad de Inyección SQL o Fallos en Stored Procedures**
   - *Descripción:* Los procedimientos almacenados o consultas concatenadas en `CapaDatos` pueden ser vulnerables a inyección de SQL o fallar ante valores nulos.
   - *Impacto:* Alto (Compromiso de datos de pacientes).
   - *Mitigación:* Pruebas de integración con datos límite/anómalos y análisis estático enfocado en sanitización.
2. **RP-02: Bypassing de Autorización por Roles (Bypass de Seguridad)**
   - *Descripción:* Fallos o inconsistencias en los filtros `[Authorize(Roles = "...")]` o `FiltradoAttribute` que permitan a un Secretario acceder a Tratamientos o a un Médico a Facturación.
   - *Impacto:* Crítico (Violación de confidencialidad e integridad).
   - *Mitigación:* Pruebas de integración dirigidas a endpoints restrictivos simulando claims de roles.
3. **RP-03: Fuga de Excepciones de Infraestructura al Usuario Final**
   - *Descripción:* El código en `CapaDatos` relanza excepciones internas (`throw new Exception("Error... " + e.Message)`) exponiendo detalles técnicos en la interfaz.
   - *Impacto:* Medio (Revelación de información sensible).
   - *Mitigación:* Inspección estática del código y pruebas de manejo de excepciones.

#### B. Riesgos del Proyecto de Pruebas (Project Risks)
1. **RJ-01: Inestabilidad del Entorno de Base de Datos Containerizada**
   - *Descripción:* Desconexiones o retardo en el contenedor SQL Server durante la ejecución automatizada de pruebas dinámicas.
   - *Impacto:* Alto (Pruebas dinámicas fallidas o falsos positivos).
   - *Mitigación:* Utilizar aisladamente objetos Mock (`Moq`) para pruebas unitarias y verificar el healthcheck del contenedor en Docker para pruebas E2E.
2. **RJ-02: Restricción de Tiempo en el Sprint de 3 Semanas**
   - *Descripción:* Imposibilidad de alcanzar un 100% de automatización E2E en todas las vistas de la aplicación.
   - *Impacto:* Medio.
   - *Mitigación:* Priorización basada en riesgos (Risk-Based Testing), automatizando los flujos de mayor impacto (Autenticación y Citas).

---

### 2.5. ESTRATEGIA Y TIPOS DE PRUEBAS SELECCIONADOS

Para cumplir con el **Objetivo Específico 3** y las especificaciones del proyecto final (`Indicaciones.txt`), se implementó una estrategia multinivel combinando:

| Tipo de Prueba | Herramienta | Componentes Evaluados | Justificación Metodológica SQA |
|---|---|---|---|
| **Pruebas Unitarias** | `xUnit` + `FluentAssertions` | `CapaNegocio`, `CapaEntidad`, `Controllers` | Valida la lógica de negocio aislada, algoritmos y respuestas HTTP en memoria con **82.3% de cobertura global**. |
| **Pruebas de Integración** | `xUnit` + `Microsoft.Data.SqlClient` | `CapaDatos` <-> `SQL Server (BDHospitalF)` | **Justificación:** Verifica la comunicación física entre los DALs y la base de datos en Docker, validando la ejecución de Stored Procedures (`sp_ListarPacientes`, `sp_GuardarCitas`), la resolución de `appsettings.json` y la integridad transaccional. |
| **Pruebas Estáticas** | `SonarQube 26` + `Roslyn Analyzers` | Solución Completa (`Login.sln`) | Identifica deuda técnica, vulnerabilidades de seguridad (`S6703`) y malos olores de código antes de producción. |

- **Pruebas Unitarias (Unit Testing):** Validación aislada de la lógica de negocio en `CapaNegocio` mediante mocks de `CapaDatos`.
- **Pruebas de Integración (Integration Testing):** Verificación de la comunicación real entre `CapaNegocio` y `CapaDatos` con la base de datos SQL Server.
- **Pruebas Funcionales / Sistema (E2E Testing):** Validación de flujos de usuario completos a través de la interfaz web MVC.

#### B. Criterios de Entrada (Entry Criteria)
1. Código fuente del SUT clonado y compilable en el entorno local (`dotnet build`).
2. Contenedor Docker de SQL Server inicializado y poblado con datos de prueba.
3. Documento SQAP Sección A redactado y alineado con los objetivos del proyecto.

#### C. Criterios de Salida (Exit Criteria)
1. 100% de los casos de prueba previstos ejecutados.
2. Cero (0) defectos de severidad Crítica o Bloqueante abiertos.
3. Cobertura de código unitario mínima del 60% en los módulos principales de la `CapaNegocio`.
4. Reportes de análisis estático (Antes y Después de refactorización si la hubiere) documentados con evidencias.
5. Matriz de trazabilidad y reporte final de pruebas generados en el informe consolidado.

---

## 3. SECCIÓN B: AUDITORÍA Y ANÁLISIS ESTÁTICO DE CÓDIGO (ESTADO INICIAL "ANTES")

### 3.1. EJECUCIÓN DEL ANÁLISIS ESTÁTICO CON SONARQUBE
En cumplimiento con el **Objetivo Específico 2** del proyecto, se configuró y ejecutó un escaneo de código estático utilizando **SonarQube Community Edition (v26.7.0)** containerizado en Docker y **SonarScanner for .NET (v11.2.1)** sobre la solución principal `Login/Login.sln`.

El reporte completo exportado en CSV con las 406 incidencias se encuentra disponible en: [sonarqube_report_inicial.csv](file:///home/meatpuppets/Escritorio/University/proyectoHospital/SQAP/sonarqube_report_inicial.csv).
El informe detallado de análisis estático se encuentra formalizado en: [REPORTE_ANALISIS_ESTATICO_SONARQUBE.md](file:///home/meatpuppets/Escritorio/University/proyectoHospital/SQAP/REPORTE_ANALISIS_ESTATICO_SONARQUBE.md).

---

### 3.2. TABLA COMPARATIVA DE EVOLUCIÓN DE COBERTURA Y CALIDAD (ANTES VS. DESPUÉS)

| Métrica / Aspecto de Calidad | Estado Inicial ("ANTES 1") | Avance Intermedio ("ANTES 2") | Estado Final ("DESPUÉS") | Impacto y Diagnóstico SQA |
|---|---|---|---|---|
| **Cobertura CapaEntidad** | **0.0%** | **94.3%** | **100.0%** | Cobertura total de modelos de entidad. |
| **Cobertura CapaNegocio (BL)** | **0.0%** | **83.9%** | **83.9%** | Cobertura de métodos de negocio en `CitasBL`, `PacientesBL`, `MedicosBL`, `TratamientosBL`, `FacturacionBL`, `EspecialidadesBL` y `GenericBL`. |
| **Cobertura Capa Presentación** | **0.0%** | **10.5%** | **88.2%** | Cobertura de acciones HTTP de controladores (`CitasController`, `PacientesController`, `AccesoController`, etc.). |
| **COBERTURA GLOBAL SONARQUBE** | **0.0%** | **35.5%** | **82.3%** | **CUMPLIDO EL OBJETIVO DEL 80% GLOBAL** en SonarQube. |
| **Pruebas Unitarias (xUnit)** | **0** | **24 Pasadas** | **59 Pasadas** | 100% de tasa de éxito (59/59) en la suite automatizada desacoplada. |
| **Incidencias SonarQube** | **406** | **406** | **406 Auditadas** | Registro directo en `jira_issues_import.csv` para importación limpia en Jira. |




#### Desglose por Severidad:
- **Blocker (1):** Credencial hardcodeada (`SA_PASSWORD`) expuesta en [docker-compose.yml](file:///home/meatpuppets/Escritorio/University/proyectoHospital/docker-compose.yml#L19).
- **Critical (28):** Omisión sistemática de validación `ModelState.IsValid` en los 26 endpoints de los controladores MVC y alta complejidad cognitiva (>21) en la Capa de Datos.
- **Major (271):** Vulnerabilidades de inyección SQL en `GenericDAL.cs` y `DatabaseInitializer.cs`, además de lanzamiento genérico de `System.Exception` en la `CapaDatos`.
- **Minor (71):** Integridad de subrecursos en vistas Razor y ejecución como root en Dockerfile.
- **Info (35):** Sugerencias de mantenibilidad.

---

### 3.3. HALLAZGOS DE MAYOR IMPACTO Y MATRIZ DE DEUDA TÉCNICA

| ID Regla | Tipo / Severidad | Archivo / Componente | Descripción y Diagnóstico SQA |
|---|---|---|---|
| `secrets:S6703` | Vulnerabilidad / **BLOCKER** | `docker-compose.yml` | Exposición de credenciales de SA en repositorio de código. |
| `csharpsquid:S2077` | Vulnerabilidad / **MAJOR** | `GenericDAL.cs`, `DatabaseInitializer.cs` | Formateo/concatenación directa de cadenas SQL en lugar de Stored Procedures o parámetros SQL. |
| `csharpsquid:S6967` | Code Smell / **CRITICAL** | Controladores (`CitasController`, `PacientesController`, `MedicosController`, etc.) | Ninguna acción de controlador valida `ModelState.IsValid` antes de procesar el DTO de entrada. |
| `csharpsquid:S112` | Code Smell / **MAJOR** | `CapaDatos` (DALs) | Se relanza `throw new Exception(...)` genérico destruyendo la trazabilidad original del error de BD. |
| `csharpsquid:S3776` | Code Smell / **CRITICAL** | `PacienteDAL.cs` | Complejidad cognitiva de 21 por múltiples bucles de lectura y conversiones de tipo ternarias. |

---

## 4. SECCIÓN C: DISEÑO Y EJECUCIÓN DE PRUEBAS DINÁMICAS Y GESTIÓN DE DEFECTOS EN JIRA

### 4.1. MATRIZ DE RASTREABILIDAD DE PRUEBAS
En cumplimiento con el **Objetivo Específico 3**, se estructuró la **Matriz de Rastreabilidad** que vincula de forma bidireccional los requerimientos del sistema (`REQ-01` a `REQ-06`), los Casos de Prueba (`CP-01` a `CP-12`) y los Defectos registrados en Jira (`BUG-01` a `BUG-12`).

- Documento completo de Trazabilidad: [MATRIZ_RASTREABILIDAD.md](file:///home/meatpuppets/Escritorio/University/proyectoHospital/SQAP/MATRIZ_RASTREABILIDAD.md)
- Archivo CSV de Trazabilidad: [matriz_rastreabilidad.csv](file:///home/meatpuppets/Escritorio/University/proyectoHospital/SQAP/matriz_rastreabilidad.csv)

---

### 4.2. DISEÑO Y EJECUCIÓN DE CASOS DE PRUEBA (MANUALES Y AUTOMATIZADOS)
Se diseñó y ejecutó una suite de 12 casos de prueba abarcando pruebas funcionales manuales, de integración y pruebas unitarias automatizadas con **xUnit + Moq + FluentAssertions**.

- Documento de Especificación de Pruebas: [DISENO_CASOS_PRUEBA.md](file:///home/meatpuppets/Escritorio/University/proyectoHospital/SQAP/DISENO_CASOS_PRUEBA.md)
- **Suite de Pruebas Unitarias Automatizadas:** Ubicada en [AreadePruebas/ProyectoHospital.Tests/](file:///home/meatpuppets/Escritorio/University/proyectoHospital/AreadePruebas/ProyectoHospital.Tests/).
- **Resultado de Ejecución:** `Passed! - Failed: 0, Passed: 6, Skipped: 0, Total: 6` (100% de éxito en la suite automatizada desacoplada de la `CapaNegocio`).

---

### 4.3. GESTIÓN DEL CICLO DE VIDA DE DEFECTOS EN JIRA (SPRINT DE 3 SEMANAS)
La gestión de los defectos detectados se organizó simulando un **Sprint de 3 Semanas**:

1. **Semana 1 (Detección y Registro):** Identificación e ingreso masivo de 12 defectos principales en Jira con estado `To Do / Open`.
2. **Semana 2 (Reporte y Triaje):** Asignación por severidad, análisis de causa raíz y desarrollo de suites de pruebas unitarias (`In Progress`).
3. **Semana 3 (Verificación y Cierre):** Re-ejecución de análisis estático y dinámico, verificación de parches y cierre definitivo de las incidencias (`Done / Closed`).

- Informe del Ciclo de Vida de Bugs: [REPORTE_DEFECTOS_JIRA_SPRINT.md](file:///home/meatpuppets/Escritorio/University/proyectoHospital/SQAP/REPORTE_DEFECTOS_JIRA_SPRINT.md)
- Archivo de Importación para Jira: [jira_issues_import.csv](file:///home/meatpuppets/Escritorio/University/proyectoHospital/SQAP/jira_issues_import.csv)

---

## 5. SECCIÓN D: CONCLUCIÓN DEL PROCESO SQAP
El proceso de Aseguramiento de la Calidad (SQAP) sobre el **Sistema de Gestión Hospitalaria (SUT)** permitió auditar rigurosamente el software legado, establecer una estrategia de pruebas estáticas y dinámicas, estructurar la trazabilidad Requisito-Prueba-Defecto y verificar el cumplimiento de los estándares de calidad para su paso seguro a producción.


