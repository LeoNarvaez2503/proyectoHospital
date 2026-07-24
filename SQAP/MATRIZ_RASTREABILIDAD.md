# MATRIZ DE RASTREABILIDAD DE PRUEBAS (REQ vs. CP vs. BUG)
## Sistema de Gestión Hospitalaria (SUT)

**Proyecto:** Sistema de Gestión Hospitalaria  
**Fase:** Pruebas Dinámicas y Gestión de Defectos (Sprint de 3 Semanas)  
**Evidencia CSV:** [matriz_rastreabilidad.csv](file:///home/meatpuppets/Escritorio/University/proyectoHospital/SQAP/matriz_rastreabilidad.csv)  

---

## 1. DESCRIPCIÓN DE REQUISITOS DEL SISTEMA (REQ)

| ID Requisito | Nombre del Requisito | Descripción | Módulo | Perfil / Rol |
|---|---|---|---|---|
| **REQ-01** | Autenticación y Autorización por Roles | El sistema debe permitir el inicio de sesión y restringir el acceso a controladores según el rol asignado (`Secretario` o `Médico`). | Autenticación | Todos |
| **REQ-02** | Gestión de Pacientes (CRUD) | El sistema debe permitir registrar, consultar y actualizar la información personal e historial de pacientes. | Pacientes | Secretario / Médico |
| **REQ-03** | Agendamiento y Control de Citas Médicas | El sistema debe permitir agendar, filtrar y cambiar el estado de las citas médicas vinculando paciente, médico y fecha. | Citas | Secretario / Médico |
| **REQ-04** | Registro de Tratamientos Médicos | El sistema debe permitir únicamente al rol `Médico` registrar y consultar recetas y diagnósticos de tratamientos. | Tratamientos | Médico |
| **REQ-05** | Emisión de Facturación | El sistema debe permitir únicamente al rol `Secretario` registrar y consultar comprobantes de facturación. | Facturación | Secretario |
| **REQ-06** | Integridad y Seguridad en Capa de Datos | La interacción con la base de datos debe realizarse de forma segura (parametrizada o mediante Stored Procedures) y sin exponenciación de excepciones. | CapaDatos | Sistema |

---

## 2. MATRIZ DE RASTREABILIDAD (REQUISITO <-> CASO DE PRUEBA <-> DEFECTO JIRA)

| ID Requisito | ID Caso Prueba | Título del Caso de Prueba | Tipo de Prueba | Herramienta / Método | Defecto Asociado (Jira Bug) | Estado Final |
|---|---|---|---|---|---|---|
| **REQ-01** | `CP-01` | Autenticación con credenciales válidas (Secretario / Médico) | Funcional / E2E | Manual / Cypress | `BUG-01` (Falta `ModelState.IsValid` en Login) | **Pass** |
| **REQ-01** | `CP-02` | Verificación de hashing de contraseña en encriptación | Unitaria | xUnit (`AccesoControllerTests`) | `BUG-02` (Método `Encriptar` no estático) | **Pass** |
| **REQ-01** | `CP-03` | Control de acceso restrictivo a Tratamientos por rol Secretario | Integración / UI | Cypress / Manual | `BUG-03` (Riesgo Bypass Filtro Atributo) | **Pass** |
| **REQ-02** | `CP-04` | Registro de Paciente con campos obligatorios completos | Unitaria / Integración | xUnit (`PacienteBLTests` + Moq) | `BUG-04` (Complejidad Cognitiva en `PacienteDAL`) | **Pass** |
| **REQ-02** | `CP-05` | Validación de duplicados o cédula inválida en Paciente | Unitaria | xUnit (`PacienteBLTests`) | `BUG-05` (Inexistencia de validación DTO en `PacientesController`) | **Pass** |
| **REQ-03** | `CP-06` | Agendamiento exitoso de Cita Médica | Unitaria / Integración | xUnit (`CitasBLTests` + Moq) | `BUG-06` (Lanzamiento genérico `Exception` en `CitasDAL`) | **Pass** |
| **REQ-03** | `CP-07` | Filtrado de Citas Médicas por rango de fecha o estado | Unitaria | xUnit (`CitasBLTests`) | `BUG-07` (Obsolecencia `SqlCommand` vs `Microsoft.Data.SqlClient`) | **Pass** |
| **REQ-04** | `CP-08` | Asignación de Tratamiento por perfil Médico | Unitaria / Integración | xUnit (`TratamientosBLTests` + Moq) | `BUG-08` (Omisión `ModelState.IsValid` en `TratamientosController`) | **Pass** |
| **REQ-04** | `CP-09` | Intento de acceso a Tratamientos por perfil Secretario (Acceso Denegado) | Funcional / E2E | Manual / Cypress | `BUG-09` (Defecto de redirección sin feedback de autorización) | **Pass** |
| **REQ-05** | `CP-10` | Generación e impresión de Factura por perfil Secretario | Unitaria / Integración | xUnit (`FacturacionBLTests` + Moq) | `BUG-10` (Omisión `ModelState.IsValid` en `FacturacionController`) | **Pass** |
| **REQ-06** | `CP-11` | Prevención de Inyección SQL en consultas de datos | Análisis Estático / Unitaria | SonarQube / xUnit (`GenericDAL`) | `BUG-11` (`S2077` String formatting SQL Injection en `GenericDAL`) | **Pass** |
| **REQ-06** | `CP-12` | Protección de Credenciales de BD en Entorno Docker | Auditoría Seguridad | SonarQube / Docker | `BUG-12` (`S6703` Password `SA_PASSWORD` en texto plano) | **Pass** |

---

## 3. RESUMEN DE COBERTURA DE REQUISITOS
- **Total de Requisitos del Sistema:** 6
- **Total de Casos de Prueba Diseñados:** 12
- **% Cobertura de Requisitos por Pruebas:** **100%** (Todos los requisitos cuentan con al menos 2 casos de prueba asociados).
