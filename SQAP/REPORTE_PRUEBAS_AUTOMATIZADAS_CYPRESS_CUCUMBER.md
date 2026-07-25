# REPORTE Y ESPECIFICACIÓN DE PRUEBAS AUTOMATIZADAS E2E CON CYPRESS Y CUCUMBER (BDD)
## Sistema de Gestión Hospitalaria (SUT - proyectoHospital)

**Documento:** Reporte de Automatización BDD (Behavior-Driven Development)  
**Estándar de Calidad:** IEEE 829 / SQAP  
**Framework de Pruebas:** Cypress v15.19.0 + @badeball/cypress-cucumber-preprocessor  
**Estado de Ejecución:** 🟢 **100% PASSED** (10/10 Escenarios Aprobados)

---

## 1. OBJETIVO Y ALCANCE

El propósito de esta suite de pruebas automatizadas es verificar de forma End-to-End (E2E) la interfaz de usuario, los flujos principales de negocio (Happy Paths) y los flujos alternos/negativos de validación y control de acceso (Edge Paths) en el sistema hospitalario.

---

## 2. ARQUITECTURA DEL PROYECTO DE PRUEBAS

La suite está organizada bajo la metodología BDD, separando las especificaciones Gherkin en lenguaje natural en la carpeta `cypress/e2e/features/` y la implementación de pasos Cypress en JavaScript dentro de `cypress/e2e/step_definitions/`:

```
proyectoHospital/
├── cypress/
│   ├── e2e/
│   │   ├── features/                          # Archivos de Especificación Gherkin
│   │   │   ├── 01_autenticacion.feature       # Login, Logout, Errores y Registro Slider
│   │   │   ├── 02_pacientes.feature           # Listado, Creación y Campos Vacíos
│   │   │   ├── 03_medicos.feature              # Administración de Médicos y Control RBAC
│   │   │   └── 04_citas.feature                # Agendamiento de Citas Médicas
│   │   └── step_definitions/                  # Implementación Técnica de Pasos en JavaScript
│   │       ├── autenticacion_steps.js
│   │       ├── pacientes_steps.js
│   │       ├── medicos_steps.js
│   │       └── citas_steps.js
│   └── support/
│       └── e2e.js
├── SQAP/
│   └── REPORTE_PRUEBAS_AUTOMATIZADAS_CYPRESS_CUCUMBER.md
├── cypress.config.js                          # Configuración de Cypress + Webpack Preprocessor
└── package.json                               # Dependencias de Automatización
```

---

## 3. MATRIZ DE ESCENARIOS DE PRUEBA EXECUTADOS

| ID Escenario | Módulo / Feature | Tipo de Flujo | Descripción del Caso de Prueba | Resultado |
| :--- | :--- | :--- | :--- | :---: |
| **TC-CY-001** | Autenticación | **Principal** | Inicio de sesión exitoso como Admin (`admin@hospital.com`) | 🟢 PASS |
| **TC-CY-002** | Autenticación | **Principal** | Cierre de sesión (Logout) exitoso | 🟢 PASS |
| **TC-CY-003** | Autenticación | **Alterno / Negativo** | Intento de inicio de sesión con contraseña incorrecta (Verifica control de alerta sin error de sistema) | 🟢 PASS |
| **TC-CY-004** | Autenticación | **Alterno / Negativo** | Intento de registro con contraseñas no coincidentes (Verifica que se mantenga en el formulario de registro animado) | 🟢 PASS |
| **TC-CY-005** | Autenticación | **Seguridad** | Protección de rutas sin autenticación (Intento de acceso a `/Medicos/Index` redirige a `/Acceso/Login`) | 🟢 PASS |
| **TC-CY-006** | Pacientes | **Principal** | Carga y visualización del listado de pacientes | 🟢 PASS |
| **TC-CY-007** | Pacientes | **Principal** | Registro de un nuevo paciente completando el formulario modal | 🟢 PASS |
| **TC-CY-008** | Pacientes | **Alterno / Negativo** | Envío de formulario modal con campos vacíos (Verifica estabilidad del sistema sin cierres abruptos) | 🟢 PASS |
| **TC-CY-009** | Médicos | **Principal** | Acceso permitido al módulo de Médicos para el rol Admin | 🟢 PASS |
| **TC-CY-010** | Médicos | **Seguridad / RBAC** | Denegación de acceso al módulo de Médicos para el rol Usuario (`/Acceso/Denegado`) | 🟢 PASS |
| **TC-CY-011** | Citas | **Principal** | Carga y acceso correcto al módulo de Agendamiento de Citas | 🟢 PASS |

---

## 4. DETALLE DE FUNCIONALIDADES Y FLUJOS ALTERNOS DESTACADOS

### 4.1 Retención en el Formulario de Registro (Slider Animation)
En el escenario `TC-CY-004`, el sistema conmuta la interfaz al panel deslizante de registro (`.cont.s--signup`). Al enviar contraseñas no coincidentes (`clave != confClave`), Cypress verifica que:
1. El usuario **no** sea redirigido a `/Home/Index`.
2. La vista mantenga el formulario de registro activo y legible.
3. El usuario pueda decidir corregir sus credenciales o conmutar de nuevo al inicio de sesión sin perder la estabilidad del navegador.

### 4.2 Control de Acceso Basado en Roles (RBAC)
En el escenario `TC-CY-010`, Cypress inicia sesión con un usuario de rol de menor privilegio (`usuario@hospital.com`) e intenta forzar la navegación a `/Medicos/Index`. Se comprueba que el servidor intercepte la solicitud y redirija a `/Acceso/Denegado`.

---

## 5. COMANDOS DE EJECUCIÓN

### Ejecución en Modo Consola (Headless):
```cmd
npx cypress run
```

### Ejecución en Modo Interactivo (GUI):
```cmd
npx cypress open
```

---

## 6. CONCLUSIÓN DE CALIDAD (SQAP)

La suite automatizada en Cypress + Cucumber cubre tanto los caminos felices como los flujos de fallo y validación del frontend. La tasa de éxito del 100% confirma la estabilidad de la interfaz y la integración correcta con los controladores MVC de ASP.NET Core 8.
