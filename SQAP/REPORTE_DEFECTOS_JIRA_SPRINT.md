# INFORME DE GESTIÓN DE DEFECTOS Y CICLO DE VIDA EN JIRA (SPRINT DE 3 SEMANAS)

**Proyecto:** Sistema de Gestión Hospitalaria (SUT)  
**Herramienta de Gestión:** Jira Software / GitHub Issues / CSV Import Matrix  
**Metodología:** Sprint de Calidad de 3 Semanas (Detección -> Reporte -> Verificación)  
**Archivo de Importación:** [jira_issues_import.csv](file:///home/meatpuppets/Escritorio/University/proyectoHospital/SQAP/jira_issues_import.csv)  

---

## 1. SIMULACIÓN Y TRAZABILIDAD DEL SPRINT DE 3 SEMANAS

La gestión del ciclo de vida de los defectos encontrados (provenientes del análisis estático de SonarQube y las pruebas dinámicas) se distribuyó metodológicamente a lo largo de 3 semanas:

```
+-----------------------------------------------------------------------------------+
| SPRINT DE CALIDAD (3 SEMANAS)                                                      |
+-----------------------------------------------------------------------------------+
| SEMANA 1: PLANIFICACIÓN Y AUDITORÍA ESTÁTICA (El "Plan")                          |
| - Identificación de 406 incidencias en SonarQube y 12 Defectos principales.       |
| - Registro masivo de Bugs en Jira con estado inicial: "TO DO / OPEN".             |
+-----------------------------------------------------------------------------------+
| SEMANA 2: DISEÑO Y EJECUCIÓN DINÁMICA (La "Acción")                               |
| - Priorización y triaje de defectos (Blocker, Critical, Major).                   |
| - Creación de pruebas unitarias xUnit/Moq para aislar y verificar la CapaNegocio. |
| - Cambio de estado en Jira a: "IN PROGRESS / UNDER REVIEW".                        |
+-----------------------------------------------------------------------------------+
| SEMANA 3: CIERRE, VERIFICACIÓN Y EVIDENCIAS (El "Entregable")                    |
| - Re-ejecución de análisis estático y pruebas dinámicas unitarias.                |
| - Verificación de correcciones y cierre definitivo en Jira: "DONE / CLOSED".      |
+-----------------------------------------------------------------------------------+
```

---

## 2. MATRIZ DE CICLO DE VIDA DE DEFECTOS (BUG TRACKING MATRIX)

| ID Defecto (Jira) | Resumen del Defecto | Severidad | Componente | Semana 1 (Detección) | Semana 2 (Reporte/Triaje) | Semana 3 (Verificación) | Estado Final |
|---|---|---|---|---|---|---|---|
| `BUG-01` | Falta de validación `ModelState.IsValid` en `AccesoController` | Critical | Presentación | TO DO | IN PROGRESS | DONE | **Closed / Verified** |
| `BUG-02` | Método `Encriptar` no marcado como estático (`S2325`) | Minor | Acceso | TO DO | IN PROGRESS | DONE | **Closed / Verified** |
| `BUG-03` | Riesgo de bypass de autorización en filtro de controlador | Critical | Seguridad | TO DO | IN PROGRESS | DONE | **Closed / Verified** |
| `BUG-04` | Alta complejidad cognitiva (>21) en `PacienteDAL.cs` | Critical | CapaDatos | TO DO | IN PROGRESS | DONE | **Closed / Verified** |
| `BUG-05` | Falta de validación DTO en `PacientesController` | Critical | Presentación | TO DO | IN PROGRESS | DONE | **Closed / Verified** |
| `BUG-06` | Lanzamiento genérico `throw new Exception()` en `CitasDAL` | Major | CapaDatos | TO DO | IN PROGRESS | DONE | **Closed / Verified** |
| `BUG-07` | Uso de biblioteca obsoleta `SqlCommand` en lugar de SqlClient | Major | CapaDatos | TO DO | IN PROGRESS | DONE | **Closed / Verified** |
| `BUG-08` | Omisión `ModelState.IsValid` en `TratamientosController` | Critical | Presentación | TO DO | IN PROGRESS | DONE | **Closed / Verified** |
| `BUG-09` | Redirección sin mensaje claro al denegar rol Secretario | Medium | Presentación | TO DO | IN PROGRESS | DONE | **Closed / Verified** |
| `BUG-10` | Omisión `ModelState.IsValid` en `FacturacionController` | Critical | Presentación | TO DO | IN PROGRESS | DONE | **Closed / Verified** |
| `BUG-11` | RIESGO DE INYECCIÓN SQL (`S2077`) por formateo de cadenas | Major | CapaDatos | TO DO | IN PROGRESS | DONE | **Closed / Verified** |
| `BUG-12` | EXPOSICIÓN DE CREDENCIAL `SA_PASSWORD` (`S6703`) | **BLOCKER** | Infrastructure | TO DO | IN PROGRESS | DONE | **Closed / Verified** |
| `BUG-13` | Muestra de IDs numericos en lugar de Nombres (Paciente/Medico) y fecha ISO en Citas | `Medium` | Presentacion | `UX-Citas` | Closed |
| `BUG-14` | Desplegables de Paciente/Medico muestran IDs y titulo incorrecto en Modal de Citas | `Medium` | Presentacion | `UX-Modal` | Closed |
| `BUG-15` | Cuadro de alerta de error en blanco al ingresar credenciales incorrectas en Login | `High` | Autenticacion | `UX-Login` | Closed |



---

## 3. MÉTRICAS DE GESTIÓN DE DEFECTOS
- **Total de Defectos Reportados:** 13
- **Defectos Críticos / Blocker:** 7
- **Defectos Resueltos y Verificados (Sprint W3):** 12 (**100%**)
- **Densidad de Defectos por Módulo:**
  - `CapaPresentacion` (Controladores MVC): 5 Defectos (41.6%)
  - `CapaDatos` (DALs): 5 Defectos (41.6%)
  - `Infraestructura` (Docker/Config): 2 Defectos (16.8%)
