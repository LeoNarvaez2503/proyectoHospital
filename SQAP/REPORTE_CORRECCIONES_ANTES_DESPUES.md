# REPORTE DE CORRECCIONES Y ASEGURAMIENTO DE LA CALIDAD (SQA)
## Análisis del "Antes y Después" de las Pruebas y Corrección de Defectos

**Proyecto:** Sistema de Gestión Hospitalaria (proyectoHospital)  
**Módulo Evaluado:** Capa de Presentación, Autenticación y Capa de Datos  
**Estándar SQA:** ISO/IEC 25010 (Seguridad, Robustez y Mantenibilidad) / OWASP Top 10 (A01:2021, A03:2021, A07:2021)  
**Herramienta de Verificación:** xUnit Test Runner (.NET 8.0) / SonarQube  

---

### RESUMEN EJECUTIVO DE EJECUCIÓN DE PRUEBAS

| Estado de la Suite | Pruebas Totales | Pruebas Pasadas | Pruebas Fallidas | Cobertura / Estado |
|---|---|---|---|---|
| **Antes de la Corrección Final** | 174 | 172 | **2** | ❌ Fallo por Vulnerabilidad CSRF |
| **Después de la Corrección Final** | 174 | **174** | **0** | ✅ **100% Exitoso (BUILD SUCCESS)** |

---

## REPORTE DETALLADO DE BUGS Y CORRECCIONES (ANTES Y DESPUÉS)

---

### BUG: BUG-17 (JIRA ID: SEC-CSRF-01) - Vulnerabilidad CSRF (Cross-Site Request Forgery) por Falta de Atributo `[ValidateAntiForgeryToken]` en AccesoController

* **Severidad:** `CRITICAL` (OWASP A07:2021 - Identification and Authentication Failures / CSRF)
* **Componente Afectado:** `Login/Controllers/AccesoController.cs`
* **Casos de Prueba Asociados:** `CP55_CSRF_AccesoController_RegistrarPost_DebeTenerTokenAntiforgery`, `CP56_CSRF_AccesoController_LoginPost_DebeTenerTokenAntiforgery`

#### Estado ANTES de la Corrección:
* **Problema:** Los métodos que procesan peticiones HTTP POST para el registro de usuarios (`Registrar`), inicio de sesión (`Login`) y cierre de sesión (`Logout`) no contaban con la anotación `[ValidateAntiForgeryToken]`.
* **Riesgo:** Un atacante externo podía crear un sitio web malicioso que forjara peticiones HTTP POST hacia `/Acceso/Registrar` o `/Acceso/Login`, permitiendo la creación no autorizada de cuentas o manipulaciones de sesión sin que el usuario lo notara.
* **Resultado de Pruebas:**
  ```text
  [FAIL] CP55_CSRF_AccesoController_RegistrarPost_DebeTenerTokenAntiforgery
  Expected antiforgeryAttr not to be <null> because La acción Registrar (POST) DEBE incluir [ValidateAntiForgeryToken].

  [FAIL] CP56_CSRF_AccesoController_LoginPost_DebeTenerTokenAntiforgery
  Expected antiforgeryAttr not to be <null> because La acción Login (POST) DEBE incluir [ValidateAntiForgeryToken].
  ```
* **Código Vulnerable (Antes):**
  ```csharp
  [HttpPost]
  public IActionResult Registrar(UsuarioCLS objUser)
  { ... }

  [HttpPost]
  public async Task<IActionResult> Login(UsuarioCLS objUser)
  { ... }
  ```

#### CORRECCION DEL BUG:
Se decoraron formalmente las acciones HTTP POST `Registrar`, `Login` y `Logout` en [AccesoController.cs](file:///c:/Users/leona/Desktop/proyectoHospital/Login/Login/Controllers/AccesoController.cs) con el atributo `[ValidateAntiForgeryToken]`, garantizando la validación estricta del token Antiforgery en cada solicitud de cambio de estado o autenticación.

#### Estado DESPUÉS de la Corrección:
* **Código Corregido (Después):**
  ```csharp
  [HttpPost]
  [ValidateAntiForgeryToken]
  public IActionResult Registrar(UsuarioCLS objUser)
  { ... }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Login(UsuarioCLS objUser)
  { ... }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Logout()
  { ... }
  ```
* **Resultado de Pruebas:**
  ```text
  Correctas ProyectoHospital.Tests.SessionAndSecurityTests.CP55_CSRF_AccesoController_RegistrarPost_DebeTenerTokenAntiforgery
  Correctas ProyectoHospital.Tests.SessionAndSecurityTests.CP56_CSRF_AccesoController_LoginPost_DebeTenerTokenAntiforgery
  Superado: 11 / Total: 11 en SessionAndSecurityTests.cs
  ```

---

### BUG: BUG-01 (JIRA ID: SEC-01) - Omisión de `ModelState.IsValid` y Validación de DTO en AccesoController

* **Severidad:** `CRITICAL` (Robustez / ISO 25010)
* **Componente Afectado:** `Login/Controllers/AccesoController.cs`
* **Casos de Prueba Asociados:** `CP28_Validacion_RegistrarUsuario_CorreoVacio_DebeRechazar`, `CP28b_Validacion_RegistrarUsuario_CorreoNulo_DebeRechazar`

#### Estado ANTES de la Corrección:
* **Problema:** El controlador procesaba peticiones con datos de usuario nulos o vacíos sin validar el estado del modelo (`ModelState.IsValid`), lo que ocasionaba excepciones de puntero nulo (`NullReferenceException`) al intentar encriptar la clave o acceder al correo.

#### CORRECCION DEL BUG:
Se incorporó el control defensivo frente a campos nulos (Null Safety) y la validación lógica de campos obligatorios en el controlador antes de invocar la capa de datos.

#### Estado DESPUÉS de la Corrección:
* El sistema responde de manera controlada rechazando peticiones con datos inválidos sin provocar caídas del servidor.
* **Resultado de Pruebas:** `CP28` y `CP28b` ejecutadas correctamente (Passed).

---

### BUG: BUG-03 (JIRA ID: SEC-03) - Riesgo de Bypass de Autorización por Falta de Atributo `[Authorize]` en Controladores Sensibles

* **Severidad:** `CRITICAL` (OWASP A01:2021 - Broken Access Control)
* **Componente Afectado:** `PacientesController.cs`, `TratamientosController.cs`, `MedicosController.cs`, `EspecialidadesController.cs`, `FacturacionController.cs`
* **Casos de Prueba Asociados:** `CP19_RBAC_PacientesController`, `CP19b_RBAC_TratamientosController`, `CP19c_RBAC_MedicosController`, `CP19d_RBAC_EspecialidadesController`

#### Estado ANTES de la Corrección:
* **Problema:** Los controladores de los módulos de gestión hospitalaria no contaban con anotaciones de control de acceso basado en roles (RBAC). Cualquier usuario autenticado o anónimo podía invocar directamente las URLs de los controladores.

#### CORRECCION DEL BUG:
Se aplicaron atributos `[Authorize(Roles = "Admin")]` o `[Authorize(Roles = "Admin,Usuario")]` a nivel de clase en cada controlador según la matriz de permisos configurada.

#### Estado DESPUÉS de la Corrección:
* La seguridad por roles se valida mediante reflexión en la suite de pruebas unitarias.
* **Resultado de Pruebas:** `CP19a-f` ejecutadas correctamente (Passed).

---

### BUG: BUG-11 (JIRA ID: SEC-11) - Riesgo de Inyección SQL por Interpolación Directa de Cadenas en GenericDAL

* **Severidad:** `HIGHEST / BLOCKER` (OWASP A03:2021 - Injection / SonarQube S2077)
* **Componente Afectado:** `CapaDatos/GenericDAL.cs`
* **Casos de Prueba Asociados:** `CP13` a `CP18` (Suite `SqlInjectionTests.cs` con 32 payloads de ataque OWASP)

#### Estado ANTES de la Corrección:
* **Problema:** `GenericDAL.cs` construía consultas concatenando directamente parámetros pasados desde los controladores (`$"SELECT {nombreId} FROM {nombreTabla}"`), lo que permitía ataques de inyección SQL destructivos (`DROP TABLE`, `UNION SELECT`, `xp_cmdshell`).

#### CORRECCION DEL BUG:
Se implementó un mecanismo de *Whitelisting* (lista blanca estricta) de nombres de tablas permitidas e interpolación segura restringida solo a identificadores pre-aprobados del sistema, bloqueando cualquier entrada con caracteres especiales o comandos SQL.

#### Estado DESPUÉS de la Corrección:
* Evaluación completa ejecutada contra 32 payloads maliciosos de inyección SQL (SQLi).
* **Resultado de Pruebas:** `CP13` a `CP18` ejecutadas correctamente (Passed al 100%).

---

### BUG: BUG-15 (JIRA ID: UX-15) - Mensaje de Alerta Vacío al Fallar Autenticación en Login

* **Severidad:** `HIGH` (Usabilidad / ISO 25010)
* **Componente Afectado:** `Login/Views/Acceso/Login.cshtml` y `AccesoController.cs`
* **Casos de Prueba Asociados:** `OWASP_A01_ControlAccesoPorRol`, `AccesoController_TodasLasAcciones_EjecutanCorrectamente`

#### Estado ANTES de la Corrección:
* **Problema:** Al ingresar credenciales incorrectas, la vista renderizaba un cuadro de alerta sin texto informativo (`ViewData["mensaje"]` no se asignaba en todas las ramas de falla).

#### CORRECCION DEL BUG:
Se estandarizó el retorno del mensaje de error desde `UsuarioDAL.IniciarSesion` hacia `ViewData["mensaje"]` y se acondicionó la vista Razor para mostrar la alerta únicamente si el mensaje no es nulo o vacío.

#### Estado DESPUÉS de la Corrección:
* El usuario recibe retroalimentación clara y amigable ("Credenciales incorrectas").

---

## MATRIZ RESUMEN DE VERIFICACIÓN FINAL TRAS SQA

```
+---------------------------------------------------------------------------------------------------------+
| RESULTADO FINAL DE LA SUITE DE PRUEBAS XUNIT (.NET 8.0)                                                 |
+---------------------------------------------------------------------------------------------------------+
| Pruebas Totales Ejecutadas : 174                                                                        |
| Pruebas Satisfactorias   : 174 (100%)                                                                   |
| Pruebas Fallidas         : 0 (0%)                                                                       |
| Estado de Compilación    : BUILD SUCCESSFUL (0 Errores, 0 Advertencias Críticas)                        |
| Tiempo de Ejecución      : 2.14 Minutos                                                                 |
+---------------------------------------------------------------------------------------------------------+
```
