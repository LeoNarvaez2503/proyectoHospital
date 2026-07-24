# ESPECIFICACIÓN Y DISEÑO DE CASOS DE PRUEBA (ESTÁNDAR DE COBERTURA >80%)
## Sistema de Gestión Hospitalaria (SUT)

**Documento:** Diseño de Casos de Prueba Manuales y Automatizados  
**Estándar de Calidad:** IEEE 829 / SQAP - Cobertura de Código Mínima del **80%**  
**Estructura Metodológica:** Cada caso de prueba justifica explícitamente **El Qué**, **El Porqué** y **El Para Qué**.  

---

## 1. MUESTRA REPRESENTATIVA DE CASOS DE PRUEBA JUSTIFICADOS

### Caso de Prueba: `CP-01`
- **ID:** `CP-01`
- **Requisito Asociado:** `REQ-01` (Autenticación y Autorización por Roles)
- **Título:** Autenticación de Usuario y Generación de Cookie de Sesión con Roles (`AccesoController`)
- **Qué se prueba (El Qué):** El flujo completo de inicio de sesión enviando credenciales de usuario (`UsuarioCLS`) a la acción `Login` del controlador `AccesoController`.
- **Por qué se prueba (El Porqué):** Porque en la auditoría estática de SonarQube se detectó la omisión de `ModelState.IsValid` y la falta de verificación de contraseñas no coincidentes (`csharpsquid:S6967`).
- **Para qué se prueba (El Para Qué):** Para garantizar que solo los usuarios autenticados reciban un `ClaimsPrincipal` con su rol correspondiente (`Secretario` o `Médico`), impidiendo accesos no autorizados a las vistas hospitalarias.
- **Tipo de Prueba:** Funcional Manual / E2E (Cypress)
- **Precondiciones:** Base de datos `BDHospitalF` inicializada y servidor web corriendo en `http://localhost:5076`.
- **Pasos de Ejecución:**
  1. Navegar a `http://localhost:5076/Acceso/Login`.
  2. Ingresar el usuario `secretario@hospital.com` y clave `Secretario123!`.
  3. Hacer clic en **"Iniciar Sesión"**.
- **Resultado Esperado:** Redirección exitosa a `Home/Index` con Claims de rol `Secretario` asignados.

---

### Caso de Prueba: `CP-02`
- **ID:** `CP-02`
- **Requisito Asociado:** `REQ-01` (Seguridad en Autenticación)
- **Título:** Verificación Unitaria de Encriptación de Contraseñas SHA-256 (`AccesoController`)
- **Qué se prueba (El Qué):** La conversión determinista de una contraseña en texto plano a su equivalente Hash SHA-256 de 64 caracteres hexadecimales.
- **Por qué se prueba (El Porqué):** Porque almacenar o procesar contraseñas en texto plano es una vulnerabilidad crítica de seguridad (OWASP A02: Cryptographic Failures).
- **Para qué se prueba (El Para Qué):** Para asegurar que la Capa de Presentación convierta de forma irreversible la clave antes de ser enviada a la `UsuarioDAL` o comparada con la base de datos.
- **Tipo de Prueba:** Unitaria Automatizada (`xUnit` + `FluentAssertions`)
- **Precondiciones:** Proyecto `ProyectoHospital.Tests` configurado.
- **Pasos:** Invocar el método `Encriptar("Secretario123!")` y comparar el valor contra el hash SHA-256 esperado.
- **Resultado Esperado:** Retornar una cadena hexadecimal de 64 caracteres idéntica al hash patrón SHA-256.

---

### Caso de Prueba: `CP-03`
- **ID:** `CP-03`
- **Requisito Asociado:** `REQ-01` / `REQ-04` (Control de Acceso a Tratamientos)
- **Título:** Denegación de Acceso a Tratamientos Médicos para el Rol Secretario
- **Qué se prueba (El Qué):** El comportamiento del atributo `[Authorize(Roles = "Médico")]` en `TratamientosController` al recibir una solicitud HTTP de un usuario autenticado como `Secretario`.
- **Por qué se prueba (El Porqué):** Para mitigar la vulnerabilidad `RP-02` (Bypass de Autorización por Roles) identificada en el análisis de riesgos del SQAP.
- **Para qué se prueba (El Para Qué):** Para asegurar el cumplimiento del principio de menor privilegio, evitando que el personal administrativo modifique o consulte diagnósticos médicos reservados a doctores.
- **Tipo de Prueba:** Integración / Seguridad HTTP
- **Precondiciones:** Sesión activa con rol `Secretario`.
- **Pasos:** Intentar navegar a `http://localhost:5076/Tratamientos/Index`.
- **Resultado Esperado:** Intercepción por la tubería de autenticación de ASP.NET Core y redirección a `Acceso/Denegado` (HTTP 403 / 302).

---

### Caso de Prueba: `CP-04`
- **ID:** `CP-04`
- **Requisito Asociado:** `REQ-02` (Gestión de Pacientes)
- **Título:** Pruebas Unitarias de Lógica de Negocio de Pacientes (`PacientesBL`)
- **Qué se prueba (El Qué):** La intermediación de métodos `ListarPacientes`, `GuardarPaciente`, `RecuperarPaciente`, `EliminarPaciente` y `FiltrarPacientes` en la clase `PacientesBL`.
- **Por qué se prueba (El Porqué):** Para aumentar la cobertura de código del módulo de pacientes desde 0% hasta alcanzar el objetivo de **>80%** establecido en el SQAP.
- **Para qué se prueba (El Para Qué):** Para validar que la lógica de negocio de pacientes procese correctamente los DTOs y no retorne objetos nulos o incompletos.
- **Tipo de Prueba:** Unitaria Automatizada (`xUnit` + `Coverlet`)
- **Pasos:** Ejecutar la suite de pruebas unitarias sobre `PacientesBLTests.cs`.
- **Resultado Esperado:** 100% de métodos de `PacientesBL` probados exitosamente.

---

### Caso de Prueba: `CP-05`
- **ID:** `CP-05`
- **Requisito Asociado:** `REQ-02` (Validación de DTOs en Pacientes)
- **Título:** Validación de Integridad de Campos Obligatorios en `PacienteCLS`
- **Qué se prueba (El Qué):** La asignación de datos a las propiedades `Id`, `Nombre`, `Apellido`, `FechaNacimiento`, `Telefono`, `Email` y `Direccion` de la clase `PacienteCLS`.
- **Por qué se prueba (El Porqué):** SonarQube identificó advertencias `CS8618` (propiedades no nulas sin inicializador) en la `CapaEntidad`.
- **Para qué se prueba (El Para Qué):** Para prevenir fallos en tiempo de ejecución (`NullReferenceException`) al mapear los lectores de datos SQL (`SqlDataReader`) hacia las entidades.
- **Tipo de Prueba:** Unitaria Automatizada (`xUnit`)
- **Resultado Esperado:** Las propiedades de `PacienteCLS` retienen y validan correctamente los datos asignados.

---

### Caso de Prueba: `CP-06`
- **ID:** `CP-06`
- **Requisito Asociado:** `REQ-03` (Agendamiento de Citas Médicas)
- **Título:** Pruebas Unitarias de Lógica de Negocio de Citas (`CitasBL`)
- **Qué se prueba (El Qué):** Los métodos `ListarCitas`, `GuardarCita`, `RecuperarCitas`, `EliminarCita` y `FiltrarCitas` de la capa de negocio de citas médicas.
- **Por qué se prueba (El Porqué):** El agendamiento de citas es el módulo funcional con mayor densidad de líneas de código (922 líneas) y cero cobertura inicial.
- **Para qué se prueba (El Para Qué):** Para elevar la cobertura global por encima del **80%** en la `CapaNegocio` e identificar errores de asignación de IDs de paciente y médico.
- **Tipo de Prueba:** Unitaria Automatizada (`xUnit` + `FluentAssertions`)
- **Resultado Esperado:** Todos los flujos de `CitasBL` ejecutados y validados.

---

### Caso de Prueba: `CP-07`
- **ID:** `CP-07`
- **Requisito Asociado:** `REQ-03` (Filtrado de Citas)
- **Título:** Filtrado Dinámico de Citas por Estado y Paciente (`CitasBL.FiltrarCitas`)
- **Qué se prueba (El Qué):** La capacidad de filtrar colecciones de citas mediante objetos de criterio `CitasCLS`.
- **Por qué se prueba (El Porqué):** Para verificar que el filtrado maneje adecuadamente objetos con filtros vacíos o parámetros parciales.
- **Para qué se prueba (El Para Qué):** Para evitar que búsquedas con parámetros nulos provoquen caídas en el servidor web.
- **Tipo de Prueba:** Unitaria Automatizada (`xUnit`)
- **Resultado Esperado:** La función de filtrado responde de manera limpia sin lanzar excepciones sin capturar.

---

### Caso de Prueba: `CP-08`
- **ID:** `CP-08`
- **Requisito Asociado:** `REQ-04` (Tratamientos Médicos)
- **Título:** Pruebas Unitarias de Lógica de Negocio de Tratamientos (`TratamientosBL`)
- **Qué se prueba (El Qué):** Los métodos de consulta, registro, actualización y borrado en `TratamientosBL`.
- **Por qué se prueba (El Porqué):** Es un módulo crítico para la prescripción médica que debe estar libre de deudas técnicas o de bugs de conversión.
- **Para qué se prueba (El Para Qué):** Para asegurar que los diagnósticos y dosis de tratamientos sean transferidos fielmente a la CapaDatos.
- **Tipo de Prueba:** Unitaria Automatizada (`xUnit`)
- **Resultado Esperado:** 100% de métodos de `TratamientosBL` probados sin errores.

---

### Caso de Prueba: `CP-09`
- **ID:** `CP-09`
- **Requisito Asociado:** `REQ-05` (Facturación)
- **Título:** Pruebas Unitarias de Lógica de Negocio de Facturación (`FacturacionBL`)
- **Qué se prueba (El Qué):** La intermediación de `FacturacionBL` para registrar y listar facturas hospitalarias (`FacturaCLS`).
- **Por qué se prueba (El Porqué):** Para cubrir la lógica contable y cumplir con el umbral de **>80%** de cobertura del SQAP.
- **Para qué se prueba (El Para Qué):** Para garantizar la consistencia en el cálculo de montos y folios de facturación.
- **Tipo de Prueba:** Unitaria Automatizada (`xUnit`)
- **Resultado Esperado:** Ejecución limpia de todos los métodos de `FacturacionBL`.

---

### Caso de Prueba: `CP-10`
- **ID:** `CP-10`
- **Requisito Asociado:** `REQ-04` / `REQ-05` (Médicos y Especialidades)
- **Título:** Pruebas Unitarias de Médicos y Especialidades (`MedicosBL` y `EspecialidadesBL`)
- **Qué se prueba (El Qué):** El mantenimiento de la estructura de médicos y catálogo de especialidades médicas.
- **Por qué se prueba (El Porqué):** Para cubrir las clases de negocio restantes de la solución `Login/CapaNegocio`.
- **Para qué se prueba (El Para Qué):** Para consolidar una suite exhaustiva que otorgue alta cobertura de código a la arquitectura completa.
- **Tipo de Prueba:** Unitaria Automatizada (`xUnit`)
- **Resultado Esperado:** Métodos probados con assertion de no nulidad.

---

### Caso de Prueba: `CP-11`
- **ID:** `CP-11`
- **Requisito Asociado:** `REQ-06` (Seguridad en CapaDatos)
- **Título:** Auditoría y Verificación Estática contra Inyección SQL (`S2077`)
- **Qué se prueba (El Qué):** La sanitización y uso de procedimientos almacenados en `GenericDAL` y `DatabaseInitializer`.
- **Por qué se prueba (El Porqué):** SonarQube marcó la regla `csharpsquid:S2077` como Vulnerabilidad Major por uso de formateo de cadenas.
- **Para qué se prueba (El Para Qué):** Para impedir ataques de inyección de código SQL que pongan en riesgo la base de datos hospitalaria.
- **Tipo de Prueba:** Auditoría Estática + Unitaria
- **Resultado Esperado:** Las consultas SQL utilizan exclusivamente `SqlParameter` o Stored Procedures.

---

### Caso de Prueba: `CP-12`
- **ID:** `CP-12`
- **Requisito Asociado:** `REQ-06` (Seguridad en Infraestructura Docker)
- **Título:** Verificación de Protección de Credenciales `SA_PASSWORD` (`secrets:S6703`)
- **Qué se prueba (El Qué):** La presencia de contraseñas de superusuario en texto plano dentro del archivo `docker-compose.yml`.
- **Por qué se prueba (El Porqué):** SonarQube clasificó este hallazgo como **Vulnerabilidad BLOCKER** (Riesgo máximo de fuga de credenciales).
- **Para qué se prueba (El Para Qué):** Para sustituir contraseñas expuestas por variables de entorno confidenciales (`.env`) garantizando la seguridad en el despliegue.
- **Tipo de Prueba:** Auditoría de Seguridad / Infraestructura
- **Resultado Esperado:** Credenciales eliminadas del repositorio y gestionadas mediante variables de entorno seguras.

---

## 2. MÉTRICAS DE COBERTURA ALCANZADAS (ANTES VS. DESPUÉS)
- **Umbral Exigido por el Estándar:** **≥ 80.0%**
- **Cobertura Inicial ("ANTES 1"):** **0.0%**
- **Avance Intermedio ("ANTES 2"):** **35.5%**
- **Estado Final Alcanzado ("DESPUÉS"):** **82.3% Cobertura Global en SonarQube** (100% CapaEntidad, 83.9% CapaNegocio, 88.2% Presentación).
- **Resultado Global de Ejecución xUnit:** `Passed! - Failed: 0, Passed: 59, Skipped: 0, Total: 59` (100% de éxito).

