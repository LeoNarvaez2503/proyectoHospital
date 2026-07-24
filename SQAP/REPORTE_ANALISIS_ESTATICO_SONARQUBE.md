# REPORTE DE ANÁLISIS ESTÁTICO DE CÓDIGO (SONARQUBE) - ESTADO INICIAL ("ANTES")

**Proyecto:** Sistema de Gestión Hospitalaria (SUT)  
**Herramienta:** SonarQube Community Edition v26.7.0 / SonarScanner for .NET 11.2.1  
**Fecha de Ejecución:** 24 de Julio de 2026  
**Auditor:** Consultoría Externa SQA (Caetano Flores / Leonardo Narváez)  
**Archivo de Evidencias Brutas:** [sonarqube_report_inicial.csv](file:///home/meatpuppets/Escritorio/University/proyectoHospital/SQAP/sonarqube_report_inicial.csv)  

---

## 1. RESUMEN EJECUTIVO DE MÉTRICAS Y DEUDA TÉCNICA

La auditoría de análisis estático se ejecutó sobre el 100% de los proyectos del **System Under Test (SUT)** (`Login/Login.sln`, abarcando `CapaEntidad`, `CapaDatos`, `CapaNegocio` y `Login` MVC presentation layer).

### 1.1. TABLA COMPARATIVA DE EVOLUCIÓN DE COBERTURA: ANTES VS. DESPUÉS

| Métrica / Capa de Software | Estado Inicial ("ANTES 1") | Avance Intermedio ("ANTES 2") | Estado Final ("DESPUÉS") | Objetivo Cumplido | Diagnóstico y Resultado SQA |
|---|---|---|---|---|---|
| **Cobertura CapaEntidad** | **0.0%** | **94.3%** | **100.0%** | ✅ **≥ 80%** | Cobertura total de instanciación y propiedades de modelos de entidad. |
| **Cobertura CapaNegocio (BL)** | **0.0%** | **83.9%** | **83.9%** | ✅ **≥ 80%** | Cobertura de métodos de negocio en todas las clases BL (`CitasBL`, `PacientesBL`, `MedicosBL`, etc.). |
| **Cobertura Capa Presentación (Controllers)** | **0.0%** | **10.5%** | **88.2%** | ✅ **≥ 80%** | Pruebas unitarias de acciones y respuestas en todos los controladores MVC. |
| **COBERTURA GLOBAL EN SONARQUBE** | **0.0%** | **35.5%** | **82.3%** | ✅ **≥ 80.0%** | **Superado el objetivo del 80% global** registrando Cobertura XML en SonarQube. |
| **Pruebas Automatizadas (xUnit)** | **0** | **24 Pasadas** | **59 Pasadas** | ✅ 100% Éxito | Tasa de éxito del 100% (59/59 pasadas) en `AreadePruebas/ProyectoHospital.Tests/`. |
| **Incidencias Auditadas** | **406** | **406** | **406** | ✅ 100% Auditadas | Mapeo y registro completo en la matriz de evidencias e importación de Jira. |




### Distribución por Severidad
```
[BLOCKER]  ██ 1  (0.25%)   - Contraseña de BD expuesta en docker-compose
[CRITICAL] ████████████ 28 (6.90%)  - Omisión de validación de ModelState y Alta Complejidad
[MAJOR]    ████████████████████████████████ 271 (66.75%) - Deuda técnica, lanzamiento de Excepciones y SQL
[MINOR]    ██████████ 71 (17.49%) - Estilo, nombrado e integridad de scripts
[INFO]     ████ 35 (8.62%)   - Sugerencias de optimización
```

---

## 2. CLASIFICACIÓN Y DETALLE DE HALLAZGOS CRÍTICOS Y VULNERABILIDADES

### 2.1. Vulnerabilidades de Seguridad (Vulnerabilities)

| ID / Regla | Severidad | Componente / Archivo | Línea | Descripción del Hallazgo y Riesgo Asociado |
|---|---|---|---|---|
| `secrets:S6703` | **BLOCKER** | [docker-compose.yml](file:///home/meatpuppets/Escritorio/University/proyectoHospital/docker-compose.yml#L19) | 19 | **Hardcoded Password Credential:** La contraseña `SA_PASSWORD` de la base de datos SQL Server está expuesta en texto plano. |
| `csharpsquid:S2077` | **MAJOR** | [GenericDAL.cs](file:///home/meatpuppets/Escritorio/University/proyectoHospital/Login/CapaDatos/GenericDAL.cs#L32) | 32 | **SQL Injection Risk:** Uso de formateo de cadenas o concatenación directa en la consulta SQL en lugar de consultas parametrizadas. |
| `csharpsquid:S2077` | **MAJOR** | [DatabaseInitializer.cs](file:///home/meatpuppets/Escritorio/University/proyectoHospital/Login/Login/Data/DatabaseInitializer.cs#L21) | 21 | **SQL Injection Risk:** Concatenación de comandos SQL en la inicialización de esquemas. |
| `docker:S6471` | **MINOR** | [Dockerfile](file:///home/meatpuppets/Escritorio/University/proyectoHospital/Dockerfile#L16) | 16 | **Root Privileges:** La imagen de contenedor ejecuta el proceso con usuario `root` por defecto, violando el principio de menor privilegio. |
| `Web:S5725` | **MINOR** | [_Layout.cshtml](file:///home/meatpuppets/Escritorio/University/proyectoHospital/Login/Login/Views/Shared/_Layout.cshtml#L11) | 11 | **Subresource Integrity:** Falta de atributos `integrity` y `crossorigin="anonymous"` en recursos CDN externos. |

---

### 2.2. Violaciones Críticas a las Buenas Prácticas (Critical Code Smells & Bugs)

#### A. Omisión Sistemática de Validación de Entrada (`csharpsquid:S6967`)
*   **Afectación:** Presente en **26 métodos** a lo largo de **TODOS los controladores MVC**:
    - `AccesoController.cs` (Líneas 31, 68)
    - `CitasController.cs` (Líneas 21, 27, 33, 39)
    - `PacientesController.cs` (Líneas 22, 28, 34, 40)
    - `MedicosController.cs` (Líneas 22, 28, 34, 40)
    - `FacturacionController.cs` (Líneas 21, 27, 33, 39)
    - `TratamientosController.cs` (Líneas 22, 28, 34, 40)
    - `EspecialidadesController.cs` (Líneas 21, 27, 33, 39)
*   **Diagnóstico SQA:** Ninguno de los controladores verifica `ModelState.IsValid` antes de procesar las peticiones HTTP ni antes de pasar los objetos DTO a la `CapaNegocio`. Esto permite el procesamiento de datos nulos o malformados, pudiendo desencadenar `NullReferenceException` o datos corruptos en la base de datos.

#### B. Lanzamiento Genérico de Excepciones y Exposición de Errores (`csharpsquid:S112`)
*   **Afectación:** **30 ocurrencias** distribuidas en la `CapaDatos` (`CitasDAL.cs`, `PacienteDAL.cs`, `MedicosDAL.cs`, `FacturacionDAL.cs`, `TratamientosDAL.cs`, `EspecialidadesDAL.cs`).
*   **Diagnóstico SQA:** El código captura cualquier `Exception` y relanza `throw new Exception("Error al... " + e.Message);`. Esto destruye el stack trace original, genera desacoplamiento deficiente de errores e interrumpe la auditoría adecuada de fallos.

#### C. Alta Complejidad Cognitiva (`csharpsquid:S3776`)
*   **Afectación:** `PacienteDAL.cs` (Línea 134).
*   **Diagnóstico SQA:** Complejidad cognitiva de **21** (superando el umbral recomendado de 15), debido a múltiples bucles anidados `while(dr.Read())` y condicionales ternarios anidados de conversión de tipos.

---

## 3. DISTRIBUCIÓN DE HALLAZGOS POR MÓDULO/ARCHIVO

| Archivo / Componente | Capa de Arquitectura | Cantidad de Issues | Severidad Principal |
|---|---|---|---|
| `CitasDAL.cs` | CapaDatos | **36** | Major (Lanzamiento de `Exception`) |
| `PacienteDAL.cs` | CapaDatos | **36** | Critical / Major (Complejidad Cognitiva) |
| `EspecialidadesDAL.cs` | CapaDatos | **35** | Major |
| `FacturacionDAL.cs` | CapaDatos | **35** | Major |
| `MedicosDAL.cs` | CapaDatos | **35** | Major |
| `TratamientosDAL.cs` | CapaDatos | **35** | Major |
| `UsuarioDAL.cs` | CapaDatos | **13** | Major |
| `generic.js` | Presentación (JS) | **11** | Major / Minor (Uso de `eval` o desuso de `const/let`) |
| `CitasBL.cs` | CapaNegocio | **10** | Major (Métodos no estáticos) |
| `Controladores MVC (Todos)` | Presentación | **28** | **CRITICAL** (Falta de `ModelState.IsValid`) |

---

## 4. CONCLUSIÓN DE LA AUDITORÍA ESTÁTICA
El SUT presenta una **Deuda Técnica Inicial Significativa** con **406 incidencias**, una **cobertura nula de código del 0.0% (922 líneas evaluables sin pruebas)** y un **12.0% de duplicación de código**. 

Los principales riesgos identificados residen en:
1. **Ausencia total de pruebas automatizadas iniciales (0.0% Coverage).**
2. **Falta de validación de ModelState en controladores (26 instancias de severidad Crítica).**
3. **Exposición de credenciales de base de datos en docker-compose (Vulnerabilidad Blocker).**
4. **Manejo inadecuado de excepciones en la Capa de Datos (30 relanzamientos de `Exception` genéricos).**

> [!NOTE]
> Todos los datos de este informe han sido registrados en la matriz de evidencias del proyecto y están respaldados en el archivo [sonarqube_report_inicial.csv](file:///home/meatpuppets/Escritorio/University/proyectoHospital/SQAP/sonarqube_report_inicial.csv).

