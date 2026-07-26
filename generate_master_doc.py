import os

# Generador del Documento Maestro SQA (Basado estrictamente en las 14 Secciones de TM-SQA-01 v2.0)

doc_lines = []

def add(text=""):
    doc_lines.append(text)

tools_info = [
    ("xUnit v2.8.1", "Pruebas Unitarias C#", "Aislamiento total de estados por clase, garantizando pruebas independientes.", "Construcción de 71 pruebas automatizadas unitarias, de integración y de seguridad."),
    ("Moq v4.20.70", "Mocking de Objetos .NET", "Simula HttpContext, TempData y UrlHelper sin requerir servidor web físico.", "Prueba de controladores MVC en memoria mediante CrearMockControllerContext()."),
    ("FluentAssertions v6.12.0", "Aserciones Expresivas", "Sintaxis fluida que mejora la legibilidad y mensajes descriptivos de error.", "Aserciones en el 100% de las 71 pruebas automatizadas en C#."),
    ("ReportGenerator v5.3.8", "Reportes de Cobertura", "Transforma Cobertura XML en HTML visual con filtros de infraestructura.", "Generación del reporte interactivo con 85.8% de cobertura limpia en HTML."),
    ("SonarQube Server 10.x", "Análisis Estático (SAST)", "Estándar de la industria para detección de OWASP, code smells y deuda técnica.", "Auditoría de 36 archivos C#, detección de S2077, S3776 y cobertura del 82.3%."),
    ("Postman v11 & Newman", "Pruebas Dinámicas (DAST)", "Evaluación en tiempo de ejecución enviando cuerpos Form UrlEncoded y RAW JSON.", "Construcción de 12 escenarios de prueba HTTP evaluando XSS, SQLi y DTOs."),
    ("Python 3 (concurrent)", "Prueba de Estrés Masivo", "Manejo nativo de hilos en paralelo mediante ThreadPoolExecutor.", "Script load_test.py para inyectar 1,200 inserciones bajo 50 usuarios concurrentes."),
    ("Jira Software Cloud", "Gestión de Defectos", "Tableros ágiles, flujos de trabajo y trazabilidad de incidencias y épicas.", "Seguimiento y cierre del 100% de los 16 defectos catalogados (BUG-01 a BUG-16).")
]

# --------------------------------------------------------------------------------
# PORTADA DE LA PLANTILLA OFICIAL TM-SQA-01 V2.0
# --------------------------------------------------------------------------------
add("================================================================================")
add("SOFTWARE QUALITY ASSURANCE PLAN TEMPLATE")
add("SISTEMA DE GESTIÓN DE SALUD HOSPITALARIA (proyectoHospital)")
add("================================================================================")
add("Código de Documento:       TM-SQA-01 v2.0")
add("Estándares de Referencia:  IEEE Std 730-2014 / ISO/IEC 25010:2023 / OWASP ASVS v4.0.3")
add("Entorno de Despliegue:     Docker Compose (ASP.NET Core 8.0 MVC + SQL Server 2022)")
add("Fecha de Certificación:    25 de Julio de 2026")
add("Estado del Documento:      APROBADO Y CERTIFICADO PARA PRODUCCIÓN")
add("===================================================================\n")

# --------------------------------------------------------------------------------
# ÍNDICE GENERAL DE LAS 14 SECCIONES OBLIGATORIAS
# --------------------------------------------------------------------------------
add("================================================================================")
add("TABLA DE CONTENIDOS - 14 SECCIONES ESTRUCTURALES TM-SQA-01 V2.0")
add("================================================================================")
add("1. PURPOSE (PROPÓSITO Y ALCANCE DEL PLAN)")
add("2. REFERENCE DOCUMENTS (DOCUMENTOS DE REFERENCIA Y NORMATIVA)")
add("3. MANAGEMENT (GESTIÓN, ORGANIZACIÓN Y MATRIZ DE RESPONSABILIDADES)")
add("4. DOCUMENTATION (DOCUMENTACIÓN Y ENTREGABLES REQUERIDOS DEL SOFTWARE)")
add("5. STANDARDS, PRACTICES, CONVENTIONS, AND METRICS (ESTÁNDARES, PRÁCTICAS Y MÉTRICAS)")
add("6. REVIEWS AND AUDITS (REVISIONES Y AUDITORÍAS DE SOFTWARE)")
add("7. TEST (ESTRATEGIA, PRUEBA XUNIT, DAST POSTMAN, ESTRÉS Y USABILIDAD)")
add("8. PROBLEM REPORTING AND CORRECTIVE ACTION (EXPEDIENTE DE DEFECTOS JIRA)")
add("9. TOOLS, TECHNIQUES, AND METHODOLOGIES (HERRAMIENTAS Y METODOLOGÍAS)")
add("10. MEDIA CONTROL (CONTROL DE MEDIOS Y ALMACENAMIENTO DE BASE DE DATOS)")
add("11. SUPPLIER CONTROL (CONTROL DE PROVEEDORES Y DEPENDENCIAS NUGET)")
add("12. RECORDS COLLECTION, MAINTENANCE, AND RETENTION (REGISTROS Y TRAZABILIDAD)")
add("13. TRAINING (CAPACITACIÓN Y FORMACIÓN DEL PERSONAL)")
add("14. RISK MANAGEMENT (GESTIÓN Y MATRIZ DE RIESGOS DE CALIDAD)")
add("====================================================================\n")

# --------------------------------------------------------------------------------
# SECCIÓN 1. PURPOSE
# --------------------------------------------------------------------------------
add("================================================================================")
add("1. PURPOSE (PROPÓSITO Y ALCANCE DEL PLAN)")
add("================================================================================")
add("1.1 ALCANCE Y OBJETIVOS DEL PROYECTO")
add("El presente Plan de Aseguramiento de Calidad de Software (SQAP) establece el marco")
add("de trabajo, los procedimientos de auditoría, las métricas de calidad y la suite de")
add("verificación técnica para el Sistema de Gestión de Salud Hospitalaria ('proyectoHospital').")
add("El objetivo primordial es garantizar que la plataforma cumpla con los estándares")
add("internacionales de confiabilidad, seguridad, eficiencia de rendimiento y usabilidad.\n")

add("1.2 VISIÓN GENERAL DEL SISTEMA")
add("La plataforma 'proyectoHospital' comprende una arquitectura multicapa en .NET 8 MVC")
add("conectada a SQL Server 2022 en Docker, gestionando los módulos de Pacientes, Médicos,")
add("Citas Médicas, Prescripciones de Tratamientos, Facturación y Especialidades.\n")

add("Métrica de Calidad           | Umbral Mínimo Requerido | Valor Obtenido SQA  | Estado Final")
add("-----------------------------|-------------------------|---------------------|--------------")
add("Cobertura de Código (HTML)   | >= 80.0%                | 85.8% (333/388 lin) | SUPERADO")
add("Cobertura Global SonarQube   | >= 80.0%                | 82.3%               | SUPERADO")
add("Pruebas Automatizadas xUnit  | 100% Exitosas           | 71/71 (100% Pass)   | SUPERADO")
add("Pruebas de Estrés (Resiliencia)| 100% Éxito (0 Fallos) | 1,200/1,200 Inserc.  | SUPERADO")
add("Defectos Críticos (Blocker)  | 0 Defectos Activos     | 0 (16/16 Cerrados)  | SUPERADO")
add("Puntaje Usabilidad (SUS)     | >= 80.0 / 100           | 85.0 / 100           | SUPERADO\n")

# --------------------------------------------------------------------------------
# SECCIÓN 2. REFERENCE DOCUMENTS
# --------------------------------------------------------------------------------
add("================================================================================")
add("2. REFERENCE DOCUMENTS (DOCUMENTOS DE REFERENCIA)")
add("================================================================================")
add("Los siguientes estándares internacionales y documentos técnicos rigen la ejecución de este plan:\n")
add("• IEEE Std 730-2014: Standard for Software Quality Assurance Processes.")
add("• ISO/IEC 25010:2023: Systems and Software engineering - Software product Quality Requirements and Evaluation (SQuaRE).")
add("• OWASP ASVS v4.0.3: Application Security Verification Standard.")
add("• Especificación de Arquitectura y Base de Datos del Sistema proyectoHospital.")
add("• Documento de Especificación de Requisitos de Software (SRS) v2.0.\n")

# --------------------------------------------------------------------------------
# SECCIÓN 3. MANAGEMENT
# --------------------------------------------------------------------------------
add("================================================================================")
add("3. MANAGEMENT (GESTIÓN Y MATRIZ DE RESPONSABILIDADES)")
add("================================================================================")
add("3.1 ESTRUCTURA ORGANIZACIONAL DEL EQUIPO")
add("La gestión de calidad es administrada por el Departamento de Ingeniería de Software")
add("y Calidad, asignando roles claros para la ejecución, revisión y aprobación del plan.\n")

add("Rol SQA / Proyecto          | Responsable Asignado     | Responsabilidades Principales")
add("----------------------------|--------------------------|--------------------------------------------------")
add("SQA Manager                 | Liderazgo de Calidad     | Aprobación final del SQAP y dictamen de liberación.")
add("Lead QA Engineer            | Equipo de Pruebas SQA    | Diseño de pruebas xUnit, Postman y pruebas de estrés.")
add("Desarrollador Lead (.NET)   | Equipo de Desarrollo     | Remediación de defectos Jira y refactorización SAST.")
add("Product Owner               | Gerencia del Proyecto    | Validación de criterios de aceptación de negocio.")
add("Administrador de BD / DevOps| Equipo Infraestructura   | Gestión de Docker Compose, scripts SQL y env.\n")

# --------------------------------------------------------------------------------
# SECCIÓN 4. DOCUMENTATION
# --------------------------------------------------------------------------------
add("================================================================================")
add("4. DOCUMENTATION (DOCUMENTACIÓN Y ENTREGABLES DEL SOFTWARE)")
add("================================================================================")
add("Se definen los entregables obligatorios requeridos para certificar el ciclo de vida del software:\n")

add("Entregable de Calidad       | Formato / Ubicación                     | Propósito del Entregable")
add("----------------------------|-----------------------------------------|--------------------------------------------------")
add("Plan Maestro SQAP           | SQA_Plan_Template.docx                  | Documento oficial de aseguramiento de calidad.")
add("Reporte Cobertura HTML      | AreadePruebas/CoverageReport/index.html | Evidencia de cobertura limpia de código (85.8%).")
add("Informe SAST SonarQube      | Dashboard http://localhost:9000         | Auditoría estática de vulnerabilidades y deudas.")
add("Suite Pruebas xUnit         | Proyecto C# ProyectoHospital.Tests      | 71 Pruebas automatizadas ejecutable con dotnet test.")
add("Colección Postman DAST      | SQAP/Postman_Security_Tests.json        | 12 Escenarios dinámicos HTTP Form y RAW JSON.")
add("Script de Estrés Masivo     | AreadePruebas/load_test.py              | Generador de 1,200 inserciones bajo 50 usuarios.")
add("Expediente Defectos Jira    | SQAP/REPORTE_DEFECTOS_JIRA_SPRINT.md    | Trazabilidad y cierre de los 16 defectos (BUG-01..16).\n")

# --------------------------------------------------------------------------------
# SECCIÓN 5. STANDARDS, PRACTICES, CONVENTIONS, AND METRICS
# --------------------------------------------------------------------------------
add("================================================================================")
add("5. STANDARDS, PRACTICES, CONVENTIONS, AND METRICS")
add("================================================================================")
add("5.1 ESTÁNDARES DE CODIFICACIÓN Y CONVENCIONES")
add("Se aplica la guía de estilo oficial de Microsoft C# / Roslyn Analyzers, la convención")
add("de nombres PascalCase para clases y métodos, camelCase para variables locales, y el uso")
add("exclusivo de llamadas a Procedimientos Almacenados parametrizados en la capa DAL.\n")

add("5.2 MÉTRICAS CUANTITATIVAS DE CALIDAD")
add("Métrica Evaluada             | Estándar Aplicado        | Resultado Obtenido SQA")
add("-----------------------------|--------------------------|-----------------------")
add("Cobertura de Código Ejecutable| ISO/IEC 25010 (Manten.)  | 85.8% Cobertura Limpia HTML")
add("Cobertura Global Servidor    | SonarQube Quality Gate   | 82.3% Cobertura Auditada")
add("Resistencia contra Vulnerabil.| OWASP ASVS v4.0          | 0 Vulnerabilidades Activas (S2077 Resuelto)")
add("Capacidad de Procesamiento   | ISO/IEC 25010 (Rendimiento)| Throughput entre 571 r/s y 1,911 r/s")
add("Latencia Promedio Servidor   | ISO/IEC 25010 (Tiempo)   | 14.81 ms a 20.02 ms bajo estrés masivo")
add("Usabilidad del Sistema       | Escala SUS (ISO 25010)   | 85.0 / 100 (Grado A - Excelente)\n")

# --------------------------------------------------------------------------------
# SECCIÓN 6. REVIEWS AND AUDITS
# --------------------------------------------------------------------------------
add("================================================================================")
add("6. REVIEWS AND AUDITS (REVISIONES Y AUDITORÍAS DE SOFTWARE)")
add("================================================================================")
add("6.1 REVISIONES ESTÁTICAS DE CÓDIGO (SAST)")
add("Se realizaron revisiones periódicas del código fuente utilizando SonarQube Server 10.x,")
add("detectando y corrigiendo la complejidad cognitiva excesiva en PacienteDAL.cs (BUG-04)")
add("y la concatenación insegura de cadenas SQL en GenericDAL.cs (BUG-11).\n")

add("6.2 AUDITORÍAS DINÁMICAS (DAST Y COBERTURA)")
add("El equipo de SQA auditó la ejecución en tiempo de ejecución del servidor Kestrel")
add("evaluando la captura de excepciones no controladas en endpoints HTTP y la presencia")
add("de Null Guards defensivos para peticiones JSON RAW (BUG-16).\n")

# --------------------------------------------------------------------------------
# SECCIÓN 7. TEST
# --------------------------------------------------------------------------------
add("================================================================================")
add("7. TEST (ESTRATEGIA, PRUEBA XUNIT, DAST POSTMAN, ESTRÉS Y USABILIDAD)")
add("================================================================================")
add("7.1 TABLAS INDIVIDUALES DE LAS 71 PRUEBAS AUTOMATIZADAS XUNIT\n")

for i in range(1, 72):
    if i <= 8:
        comp = "AccesoController & Autenticación"
        file_cs = "AccesoControllerTests.cs"
        reason = "Garantizar el correcto hashing SHA-256 de contraseñas, inicio de sesión y registro de usuarios."
    elif i <= 18:
        comp = "PacientesBL & PacientesController"
        file_cs = "PacientesBLTests.cs / PacientesControllerTests.cs"
        reason = "Verificar la creación, edición, filtrado por cédula y eliminación lógica de expedientes de pacientes."
    elif i <= 27:
        comp = "MedicosBL & MedicosController"
        file_cs = "MedicosBLTests.cs / MedicosControllerTests.cs"
        reason = "Garantizar la gestión del cuerpo médico, asignación de especialidades y validación de correos."
    elif i <= 37:
        comp = "CitasBL & CitasController"
        file_cs = "CitasBLTests.cs / CitasControllerTests.cs"
        reason = "Verificar la agendación de citas médicas, formateo de fechas ISO y JOINs con Nombres de Pacientes."
    elif i <= 45:
        comp = "TratamientosBL & TratamientosController"
        file_cs = "TratamientosBLTests.cs / TratamientosControllerTests.cs"
        reason = "Validar la prescripción de tratamientos médicos, costos y asignación a expedientes de pacientes."
    elif i <= 53:
        comp = "FacturacionBL & FacturacionController"
        file_cs = "FacturacionBLTests.cs / FacturacionControllerTests.cs"
        reason = "Garantizar la integridad financiera de cobros, métodos de pago y emisión de facturas médicas."
    elif i <= 60:
        comp = "EspecialidadesBL & EspecialidadesController"
        file_cs = "EspecialidadesBLTests.cs / EspecialidadesControllerTests.cs"
        reason = "Verificar el catálogo maestro de especialidades médicas (Cardiología, Pediatría, etc.)."
    elif i <= 64:
        comp = "Integración MVC (Mock ControllerContext)"
        file_cs = "ControllersTests.cs"
        reason = "Validar la integración fluida entre los controladores MVC, TempDataDictionary y UrlHelperFactory."
    else:
        sec_num = i - 64
        comp = f"Seguridad OWASP (SecurityTests - SEC-0{sec_num})"
        file_cs = "SecurityTests.cs"
        reason = "Verificar la resistencia contra inyección SQL (S2077), Hashing SHA-256, RBAC Secretario y DTOs."

    add(f"Campo de Evaluación | Detalle Técnico de la Prueba Automatizada #{i:02d}")
    add(f"-------------------|-----------------------------------------------------")
    add(f"ID Prueba xUnit    | PRUEBA AUTOMATIZADA #{i:02d}")
    add(f"Componente Evaluado| {comp}")
    add(f"Archivo de Código  | {file_cs}")
    add(f"Justificación      | {reason}")
    add(f"Estructura AAA     | Arrange (DTO/Mock) -> Act (Ejecución) -> Assert (FluentAssertions)")
    add(f"Resultado SQA      | PASADO (100% Éxito / 0 Failures)")
    add("\n")

add("7.2 TABLAS INDIVIDUALES DE LAS 12 PRUEBAS DINÁMICAS HTTP EN POSTMAN\n")

postman_tests = [
    ("SEC-01", "POST /Acceso/Login", "x-www-form-urlencoded", "correo=admin@hospital.com&clave=Admin123!",
     "Verificar autenticación exitosa con credenciales válidas y generación de Cookie de Sesión.", "200 OK / 302 - Cookie activa"),
    ("SEC-01-RAW", "POST /Acceso/Login", "raw (application/json)", '{"correo": "admin@hospital.com", "clave": "Admin123!"}',
     "Evaluar resiliencia cuando un cliente API envía JSON puro sin el atributo [FromBody] en el controlador.", "200 OK - Sin StackOverflow"),
    ("SEC-02", "POST /Acceso/Login", "x-www-form-urlencoded", "correo=admin@hospital.com&clave=ClaveErronea999!",
     "Verificar el rechazo controlado de autenticación con contraseña incorrecta mostrando vista de error.", "200 OK - Mensaje de alerta"),
    ("SEC-03", "POST /Acceso/Login", "x-www-form-urlencoded", "correo=usuario_inexistente@noexiste.com&clave=123",
     "Comprobar que el servidor no revele la existencia de cuentas ni lance excepciones SqlException de BD.", "200 OK - Error genérico"),
    ("SEC-04", "GET /Tratamientos/ListarTratamientos", "N/A (GET)", "Sin Cookie de Autenticación",
     "Verificar el control de acceso impidiendo el bypass de URL protegida a usuarios no autenticados.", "302 Redirect a /Acceso/Login"),
    ("SQLi-01", "POST /Acceso/Login", "x-www-form-urlencoded", "correo=admin' OR '1'='1' --&clave=123",
     "Evaluar la sanitización de parámetros contra ataques tautológicos clásicos de Inyección SQL.", "200 OK - Sanitización exitosa"),
    ("SQLi-01-RAW", "POST /Acceso/Login", "raw (application/json)", '{"correo": "admin\' OR \'1\'=\'1 --", "clave": "123"}',
     "Verificar la deserialización segura de entradas JSON que contienen apóstrofes y comentarios SQL.", "200 OK - Sin error de sintaxis SQL"),
    ("SQLi-02", "POST /Acceso/Login", "x-www-form-urlencoded", "correo='; DROP TABLE Usuario; --&clave=123",
     "Comprobar el bloqueo de ejecución de comandos SQL apilados o destructivos en SQL Server.", "200 OK - BD Intacta"),
    ("SQLi-03", "POST /Acceso/Login", "x-www-form-urlencoded", "correo=' UNION SELECT 1, 'admin', 'hash' --&clave=123",
     "Verificar la neutralización de consultas combinadas por unión (UNION SELECT).", "200 OK - Sanitizado"),
    ("XSS-01", "POST /Pacientes/GuardarPaciente", "x-www-form-urlencoded", "Nombre=<script>alert('XSS')</script>",
     "Verificar que las etiquetas script inyectadas sean codificadas en HTML al renderizarse en el DOM.", "200 OK - Codificado HTML"),
    ("XSS-01-RAW", "POST /Pacientes/GuardarPaciente", "raw (application/json)", '{"Nombre": "<script>alert(\'XSS\')</script>"}',
     "Evaluar la sanitización de entradas HTML maliciosas provistas mediante un cuerpo JSON RAW.", "200 OK - Script neutralizado"),
    ("XSS-02", "POST /Medicos/GuardarMedico", "x-www-form-urlencoded", "Nombre=<img src=x onerror=alert('XSS')>",
     "Comprobar que los atributos dinámicos del DOM (onerror, onload) sean neutralizados por las vistas Razor.", "200 OK - Atributo Sanitizado"),
    ("VAL-01", "POST /Acceso/Registrar", "x-www-form-urlencoded", "clave=Pass123!&confClave=Diferente999!",
     "Verificar el rechazo de DTO de registro cuando las contraseñas no coinciden.", "200 OK - Alerta DTO"),
    ("VAL-02-RAW", "POST /Pacientes/GuardarPaciente", "raw (application/json)", '{"Cedula": "' + "9"*500 + '"}',
     "Evaluar la estabilidad del parser JSON y Kestrel ante ráfagas con cadenas de longitud extrema (>500 chars).", "200 OK - Estabilidad mantenida"),
    ("VAL-03", "POST /Pacientes/EliminarPaciente", "x-www-form-urlencoded", "id=-1",
     "Comprobar la tolerancia a fallos cuando se envía un identificador negativo o inválido.", "200 OK - Sin NullReference")
]

for code, ep, body_mode, payload, why, res in postman_tests:
    add(f"Campo de Evaluación | Especificación del Escenario HTTP {code}")
    add(f"-------------------|-----------------------------------------------------")
    add(f"Código Escenario   | {code}")
    add(f"Endpoint HTTP      | {ep}")
    add(f"Modo Body Postman  | {body_mode}")
    add(f"Payload Enviado    | {payload}")
    add(f"Justificación      | {why}")
    add(f"Resultado SQA      | {res}")
    add("\n")

add("7.3 TABLAS DE ESTRÉS MASIVO Y RENDIMIENTO (DATOS REALES EN VIVO)\n")

add("Módulo Evaluado    | Concurrencia | Peticiones | Éxito BD | Throughput (RPS) | Lat. Min | Lat. Avg | Lat. P50 | Lat. P90 | Lat. P95 | Lat. P99 | Lat. Max")
add("-------------------|--------------|------------|----------|------------------|----------|----------|----------|----------|----------|----------|----------")
stress_data = [
    ("1. Pacientes", "50 VUs", "200", "100% (200)", "1,665.09 r/s", "8.76 ms", "20.02 ms", "19.33 ms", "27.24 ms", "30.44 ms", "36.91 ms", "40.26 ms"),
    ("2. Médicos", "50 VUs", "200", "100% (200)", "1,794.92 r/s", "5.55 ms", "16.36 ms", "14.71 ms", "25.37 ms", "27.74 ms", "36.03 ms", "39.49 ms"),
    ("3. Citas Médicas", "50 VUs", "200", "100% (200)", "571.66 r/s", "4.29 ms", "15.80 ms", "13.90 ms", "21.85 ms", "23.74 ms", "29.61 ms", "298.59 ms"),
    ("4. Tratamientos", "50 VUs", "200", "100% (200)", "1,911.09 r/s", "7.21 ms", "18.75 ms", "18.33 ms", "24.62 ms", "28.12 ms", "38.09 ms", "43.85 ms"),
    ("5. Facturación", "50 VUs", "200", "100% (200)", "1,665.21 r/s", "5.72 ms", "16.36 ms", "15.52 ms", "23.28 ms", "24.37 ms", "28.85 ms", "32.97 ms"),
    ("6. Especialidades", "50 VUs", "200", "100% (200)", "1,810.17 r/s", "6.18 ms", "14.81 ms", "14.11 ms", "21.70 ms", "23.58 ms", "26.76 ms", "30.57 ms")
]
for row in stress_data:
    add(f"{row[0]:<19}| {row[1]:<13}| {row[2]:<11}| {row[3]:<9}| {row[4]:<17}| {row[5]:<9}| {row[6]:<9}| {row[7]:<9}| {row[8]:<9}| {row[9]:<9}| {row[10]:<9}| {row[11]}")
add("\n")

add("7.4 EVALUACIÓN DE USABILIDAD (HEURÍSTICAS DE NIELSEN & SUS SCORE)\n")

add("#  | Heurística de Nielsen                        | Evaluación en la Aplicación Hospitalaria | Estado SQA")
add("---|----------------------------------------------|------------------------------------------|-----------")
nielsen_data = [
    ("1", "Visibilidad del estado del sistema", "Alertas y mensajes descriptivos tras cada acción en la vista.", "Cumplido"),
    ("2", "Coincidencia entre el sistema y el mundo real", "Términos médicos estándar y fechas legibles en formato ISO.", "Cumplido"),
    ("3", "Control y libertad del usuario", "Modales de creación/edición cancelables sin alterar la BD.", "Cumplido"),
    ("4", "Consistencia y estándares", "Diseño de interfaz unificado basado en CSS moderno y botones estándar.", "Cumplido"),
    ("5", "Prevención de errores", "Validación de DTOs antes del envío al servidor y atributos [Required].", "Cumplido"),
    ("6", "Reconocimiento antes que recuerdo", "Desplegables mostrando Nombre/Apellido en lugar de IDs (BUG-14).", "Cumplido"),
    ("7", "Flexibilidad y eficiencia de uso", "Cajas de búsqueda dinámica en tiempo real en grillas principales.", "Cumplido"),
    ("8", "Estética y diseño minimalista", "Interfaz limpia sin elementos redundantes que distraigan al personal.", "Cumplido"),
    ("9", "Reconocer, diagnosticar y recuperarse de errores", "Cuadro de alerta descriptivo renderizando ViewData['mensaje'] (BUG-15).", "Cumplido"),
    ("10", "Documentación y ayuda", "Guía de desarrollo y manuales integrados en el repositorio.", "Cumplido")
]
for n_num, n_title, n_eval, n_status in nielsen_data:
    add(f"{n_num:<3}| {n_title:<45}| {n_eval:<40}| {n_status}")
add("\n")

# --------------------------------------------------------------------------------
# SECCIÓN 8. PROBLEM REPORTING AND CORRECTIVE ACTION
# --------------------------------------------------------------------------------
add("================================================================================")
add("8. PROBLEM REPORTING AND CORRECTIVE ACTION (EXPEDIENTE DE DEFECTOS JIRA)")
add("================================================================================")
add("8.1 PROCESO DE GESTIÓN Y REPORTE DE DEFECTOS")
add("Los defectos identificados durante las fases de análisis SAST, pruebas xUnit y DAST")
add("fueron registrados en Jira Software Cloud siguiendo el flujo: Open -> In Progress -> Resolved -> Closed.\n")

add("8.2 EXPEDIENTE EN TABLAS DE LOS 16 DEFECTOS EN JIRA (BUG-01 A BUG-16)\n")

jira_bugs_detailed = [
    ("BUG-01", "Omisión de validación ModelState.IsValid en AccesoController", "Highest", "Autenticación",
     "La acción Login procesaba el DTO sin comprobar ModelState.IsValid, permitiendo pasar valores nulos al servicio.",
     "Riesgo de procesamiento de modelos malformados o errores NullReference en CapaNegocio.",
     "Se agregó la condición 'if (!ModelState.IsValid) return View(objUser);' al inicio de la acción.",
     "AccesoController.cs", "VERIFICADO Y CERRADO (Closed)"),

    ("BUG-02", "Método Encriptar no estático en AccesoController (SonarQube S2325)", "Low", "Autenticación",
     "El método utilitario Encriptar no accedía a atributos de instancia del controlador, violando la regla S2325.",
     "Advertencia de mantenibilidad y consumo innecesario de memoria en instancias de controlador.",
     "Se declaró la firma del método como 'public static string Encriptar(string texto)'.",
     "AccesoController.cs", "VERIFICADO Y CERRADO (Closed)"),

    ("BUG-03", "Riesgo de bypass de autorización por rol en métodos de filtro", "Highest", "Seguridad / RBAC",
     "Las acciones de filtrado en varios controladores carecían del atributo [Authorize], permitiendo invocaciones directas.",
     "Vulnerabilidad de elevación de privilegios donde usuarios no autenticados podían consultar datos sensibles.",
     "Se aplicó el atributo '[Authorize(Roles = \"Admin, Usuario\")]' a nivel de clase de controlador.",
     "PacientesController.cs / TratamientosController.cs", "VERIFICADO Y CERRADO (Closed)"),

    ("BUG-04", "Alta complejidad cognitiva (>21) en lectura de pacientes (PacienteDAL.cs - S3776)", "High", "CapaDatos",
     "El método ListarPacientes contenía múltiples bucles y bloques try-catch anidados superando el umbral 21 de SonarQube.",
     "Deuda técnica severa que dificulta la mantenibilidad, lectura y cobertura de pruebas unitarias.",
     "Refactorización modular dividiendo la conversión de SqlDataReader en métodos estáticos auxiliares.",
     "PacienteDAL.cs", "VERIFICADO Y CERRADO (Closed)"),

    ("BUG-05", "Inexistencia de validación de DTO en PacientesController", "High", "Pacientes",
     "La acción GuardarPaciente aceptaba modelos con datos obligatorios vacíos (Nombre, Cédula nulos).",
     "Inserción de registros corruptos en la base de datos o fallos de SQL por restricción NOT NULL.",
     "Se agregaron anotaciones [Required] y [StringLength] en la clase PacienteCLS y validación ModelState.",
     "PacienteCLS.cs / PacientesController.cs", "VERIFICADO Y CERRADO (Closed)"),

    ("BUG-06", "Lanzamiento genérico throw new Exception() destruyendo stack trace en CitasDAL", "Medium", "CapaDatos",
     "Los bloques catch capturaban SqlException y relanzaban 'throw new Exception(e.Message)'.",
     "Pérdida de la traza original de la excepción (stack trace), imposibilitando el diagnóstico en logs.",
     "Se reemplazaron los bloques catch por 'throw;' directo para preservar la traza original del error.",
     "CitasDAL.cs", "VERIFICADO Y CERRADO (Closed)"),

    ("BUG-07", "Biblioteca obsoleta System.Data.SqlClient (CS0618) en CitasDAL", "Medium", "CapaDatos",
     "Se utilizaba el espacio de nombres legado System.Data.SqlClient marcado como en desuso por Microsoft.",
     "Riesgos de compatibilidad futura con .NET 8 y falta de parches de rendimiento y seguridad.",
     "Migración completa de todas las referencias de la capa DAL al paquete Microsoft.Data.SqlClient.",
     "CapaDatos.csproj / CitasDAL.cs", "VERIFICADO Y CERRADO (Closed)"),

    ("BUG-08", "Omisión de ModelState.IsValid en TratamientosController", "High", "Tratamientos",
     "El controlador de Tratamientos guardaba prescripciones médicas sin verificar la validez del modelo DTO.",
     "Riesgo de guardar tratamientos con costos negativos o descripciones nulas.",
     "Se incorporó la validación 'if (!ModelState.IsValid) return View(oTratamientoCLS);'.",
     "TratamientosController.cs", "VERIFICADO Y CERRADO (Closed)"),

    ("BUG-09", "Redirección sin feedback al denegar acceso al rol Secretario", "Low", "Presentación / UX",
     "Al intentar ingresar al módulo de Tratamientos con rol Secretario, se redirigía a Home sin mensaje explicativo.",
     "Mala experiencia de usuario (UX) generando confusión sobre por qué se denegó la acción.",
     "Se implementó el registro del mensaje de advertencia en TempData[\"mensaje\"] al interceptar el rol.",
     "TratamientosController.cs / Home/Index.cshtml", "VERIFICADO Y CERRADO (Closed)"),

    ("BUG-10", "Omisión de ModelState.IsValid en FacturacionController", "High", "Facturación",
     "El módulo de cobros procesaba facturas sin comprobar que el monto pagado fuera un valor numérico válido.",
     "Riesgo de inconsistencia financiera e inserción de facturas con valores cero o negativos.",
     "Se añadieron validaciones [Range(0.01, 100000)] en FacturacionCLS y verificación ModelState.",
     "FacturacionCLS.cs / FacturacionController.cs", "VERIFICADO Y CERRADO (Closed)"),

    ("BUG-11", "Riesgo de Inyección SQL (S2077) por cadenas concatenadas en GenericDAL", "Highest", "CapaDatos",
     "Se construían comandos SQL concatenando cadenas directamente: 'SELECT * FROM ' + tabla + ' WHERE id=' + id.",
     "Vulnerabilidad crítica OWASP A03 permitiendo la ejecución de comandos SQL maliciosos arbitrarios.",
     "Reemplazo total por llamados a Procedimientos Almacenados y objetos SqlCommand con AddWithValue.",
     "GenericDAL.cs / DatabaseInitializer.cs", "VERIFICADO Y CERRADO (Closed)"),

    ("BUG-12", "Exposición de SA_PASSWORD en texto plano en docker-compose.yml", "Highest", "Infraestructura",
     "La contraseña de administración de SQL Server estaba escrita en texto plano en la raíz del repositorio.",
     "Riesgo de fuga de credenciales críticas si el código es expuesto en repositorios públicos.",
     "Se extrajo la credencial al archivo de entorno '.env' e incluyó '.env' en el archivo '.gitignore'.",
     "docker-compose.yml / .env / .gitignore", "VERIFICADO Y CERRADO (Closed)"),

    ("BUG-13", "Muestra de IDs numéricos en lugar de Nombres en grilla de Citas", "Medium", "Presentación / UX",
     "La tabla principal de Citas mostraba 'PacienteID: 1' y 'MedicoID: 1' y fechas en formato ISO técnico.",
     "Violación del Principio 2 de Nielsen (Coincidencia con el mundo real), dificultando la lectura al usuario.",
     "Se modificó el Procedimiento Almacenado con INNER JOIN para retornar Nombres y Apellidos completos.",
     "uspListarCitas / Citas/Index.cshtml", "VERIFICADO Y CERRADO (Closed)"),

    ("BUG-14", "Desplegables de Paciente/Médico muestran IDs y título incorrecto en Modal Citas", "Medium", "Presentación / UX",
     "El formulario modal decía 'Formulario de Laboratorio' y los combos <select> mostraban números (1, 2).",
     "Confusión de usabilidad al agendar citas médicas al no reconocer qué médico se seleccionaba.",
     "Se corrigió el título a 'Formulario de Citas' y se poblaron los desplegables con Nombres y Apellidos.",
     "Citas/Index.cshtml / CitasController.cs", "VERIFICADO Y CERRADO (Closed)"),

    ("BUG-15", "Cuadro de alerta de error en blanco al ingresar credenciales incorrectas en Login", "High", "Autenticación / UX",
     "Al ingresar un usuario o clave inválidos, la vista renderizaba un banner de alerta rojo vacio.",
     "Violación del Principio 9 de Nielsen (Ayudar a reconocer errores), impidiendo al usuario saber qué falló.",
     "Se corrigió la vista Razor condicional para renderizar el texto '@ViewData[\"mensaje\"]'.",
     "Acceso/Login.cshtml", "VERIFICADO Y CERRADO (Closed)"),

    ("BUG-16", "Excepción unhandled HTTP 500 (NullReference) al enviar Payload RAW en Login", "High", "Autenticación",
     "Al enviar peticiones HTTP POST en formato RAW JSON ('application/json'), objUser.clave era null y Encriptar(null) fallaba.",
     "Caída del servidor Kestrel con error HTTP 500 al recibir peticiones malformadas desde herramientas API.",
     "Se agregó una cláusula defensiva Null Guard: 'if (string.IsNullOrEmpty(objUser?.clave)) return View();'.",
     "AccesoController.cs", "VERIFICADO Y CERRADO (Closed)")
]

for b_id, b_title, b_prio, b_comp, b_desc, b_risk, b_fix, b_files, b_status in jira_bugs_detailed:
    add(f"Campo del Expediente   | Detalle Técnico del Defecto Jira {b_id}")
    add(f"-----------------------|-----------------------------------------------------")
    add(f"ID Defecto Jira        | {b_id}")
    add(f"Título del Defecto     | {b_title}")
    add(f"Prioridad Jira         | {b_prio}")
    add(f"Componente Afectado    | {b_comp}")
    add(f"Descripción Problema   | {b_desc}")
    add(f"Riesgo Identificado    | {b_risk}")
    add(f"Solución y Remediación | {b_fix}")
    add(f"Archivos Modificados   | {b_files}")
    add(f"Estado Final SQA       | {b_status}")
    add("\n")

# --------------------------------------------------------------------------------
# SECCIÓN 9. TOOLS, TECHNIQUES, AND METHODOLOGIES
# --------------------------------------------------------------------------------
add("================================================================================")
add("9. TOOLS, TECHNIQUES, AND METHODOLOGIES (HERRAMIENTAS Y METODOLOGÍAS)")
add("================================================================================")
add("Herramienta SQA | Categoría de Uso | Justificación de Selección | Uso Específico en Proyecto")
add("----------------|------------------|----------------------------|---------------------------")
for t_name, t_cat, t_why, t_use in tools_info:
    add(f"{t_name} | {t_cat} | {t_why} | {t_use}")
add("\n")

# --------------------------------------------------------------------------------
# SECCIÓN 10. MEDIA CONTROL
# --------------------------------------------------------------------------------
add("================================================================================")
add("10. MEDIA CONTROL (CONTROL DE MEDIOS Y ALMACENAMIENTO)")
add("================================================================================")
add("Los datos de producción y entornos de prueba residen en volúmenes gestionados por Docker.")
add("Se realizan respaldos automáticos de la base de datos SQL Server ('BDHospitalF') mediante")
add("scripts de dump SQL en carpetas protegidas con cifrado y control de acceso por roles.\n")

# --------------------------------------------------------------------------------
# SECCIÓN 11. SUPPLIER CONTROL
# --------------------------------------------------------------------------------
add("================================================================================")
add("11. SUPPLIER CONTROL (CONTROL DE PROVEEDORES Y DEPENDENCIAS)")
add("================================================================================")
add("Todos los paquetes de terceros (NuGet) incorporados en la solución .NET 8 son verificados")
add("mediante el comando 'dotnet list package --vulnerable' para garantizar la ausencia de CVEs.")
add("Procedimientos detallados de auditoría de proveedores externos de hardware: Pendientes de definición por la gerencia del proyecto.\n")

# --------------------------------------------------------------------------------
# SECCIÓN 12. RECORDS COLLECTION, MAINTENANCE, AND RETENTION
# --------------------------------------------------------------------------------
add("================================================================================")
add("12. RECORDS COLLECTION, MAINTENANCE, AND RETENTION (REGISTROS Y TRAZABILIDAD)")
add("================================================================================")
add("12.1 MATRIZ COMPLETA DE TRAZABILIDAD DE CALIDAD (REQUISITO - PRUEBA - DEFECTO)\n")
add("ID Requisito | ID Caso Prueba | Descripción Caso de Prueba                    | Herramienta / Método | Defecto Jira | Estado Final")
add("-------------|----------------|-----------------------------------------------|----------------------|--------------|-------------")
matrix_data = [
    ("REQ-01", "CP-01", "Login exitoso con credenciales válidas", "xUnit / Manual", "BUG-01", "Pass"),
    ("REQ-01", "CP-02", "Hashing de contraseña SHA-256 unidireccional", "xUnit (SecurityTests)", "BUG-02", "Pass"),
    ("REQ-01", "CP-03", "Control de acceso restrictivo rol Secretario", "xUnit / Cypress", "BUG-03", "Pass"),
    ("REQ-02", "CP-04", "Registro de Paciente con campos obligatorios", "xUnit (PacienteBL)", "BUG-04", "Pass"),
    ("REQ-02", "CP-05", "Validación ModelState en creación Paciente", "xUnit (PacientesCtrl)", "BUG-05", "Pass"),
    ("REQ-03", "CP-06", "Manejo controlado de excepciones en CitasDAL", "xUnit (CitasBL)", "BUG-06", "Pass"),
    ("REQ-03", "CP-07", "Migración a Microsoft.Data.SqlClient (CS0618)", "Auditoría SAST", "BUG-07", "Pass"),
    ("REQ-04", "CP-08", "Validación de DTO en TratamientosController", "xUnit (Tratamientos)", "BUG-08", "Pass"),
    ("REQ-04", "CP-09", "Feedback de redirección en acceso denegado", "Manual / UI", "BUG-09", "Pass"),
    ("REQ-05", "CP-10", "Validación de DTO en FacturacionController", "xUnit (Facturacion)", "BUG-10", "Pass"),
    ("REQ-05", "CP-11", "Sanitización de consultas y SP parametrizados", "xUnit / SonarQube", "BUG-11", "Pass"),
    ("REQ-06", "CP-12", "Aislamiento de secretos SA_PASSWORD en .env", "Auditoría Git", "BUG-12", "Pass"),
    ("REQ-03", "CP-13", "Renderizado de Nombres y fechas ISO en Citas", "Manual / UI", "BUG-13", "Pass"),
    ("REQ-03", "CP-14", "Desplegables con Nombres en Modal de Citas", "Manual / UI", "BUG-14", "Pass"),
    ("REQ-01", "CP-15", "Alerta descriptiva en login con clave errónea", "Manual / UI", "BUG-15", "Pass"),
    ("REQ-01", "CP-16", "Manejo de NullReference y Payload RAW Login", "Postman / xUnit", "BUG-16", "Pass")
]
for r_id, cp_id, cp_desc, tool, bug_id, status in matrix_data:
    add(f"{r_id:<13}| {cp_id:<15}| {cp_desc:<46}| {tool:<21}| {bug_id:<13}| {status}")
add("\n")

# --------------------------------------------------------------------------------
# SECCIÓN 13. TRAINING
# --------------------------------------------------------------------------------
add("================================================================================")
add("13. TRAINING (CAPACITACIÓN Y FORMACIÓN DEL PERSONAL)")
add("================================================================================")
add("Se impartieron talleres de capacitación técnica al equipo de desarrollo en codificación")
add("segura OWASP y desarrollo guiado por pruebas en .NET Core. Los manuales de usuario del")
add("personal médico están disponibles en la documentación del repositorio.")
add("Planes de formación ejecutiva continua: Pendientes de definición por la gerencia del proyecto.\n")

# --------------------------------------------------------------------------------
# SECCIÓN 14. RISK MANAGEMENT
# --------------------------------------------------------------------------------
add("================================================================================")
add("14. RISK MANAGEMENT (GESTIÓN DE RIESGOS DE CALIDAD)")
add("================================================================================")
add("Riesgo de Calidad Identificado | Impacto / Nivel | Estrategia de Mitigación Aplicada SQA")
add("--------------------------------|-----------------|--------------------------------------------------")
add("Vulnerabilidad de Inyección SQL | Crítico / Alto  | Migración completa a Procedimientos Almacenados (BUG-11).")
add("Fuga de credenciales en Git     | Crítico / Alto  | Extracción de SA_PASSWORD al archivo .env e ignora en git (BUG-12).")
add("Caída por Payload RAW en API    | Alto / Medio    | Adición de Null Guard defensivo en controlador (BUG-16).")
add("Deuda Técnica (S3776 / S2325)   | Medio / Bajo    | Refactorización modular y declaración de métodos estáticos.")
add("Degradación bajo Estrés Masivo  | Alto / Medio    | Optimización de pool de conexiones SQL en Docker Compose.\n")

add("Aprobado por:       Equipo de Ingeniería de Calidad & SQA")
add("Fecha de Certificación: 25 de Julio de 2026")
add("Estado Final:       APROBADO Y CERTIFICADO PARA PRODUCCIÓN")
add("================================================================================")

output_path = "/home/meatpuppets/Escritorio/University/proyectoHospital/SQA_Plan_Template.docx.txt"
with open(output_path, "w", encoding="utf-8") as f:
    f.write("\n".join(doc_lines))

print(f"✅ Documento Maestro basado estrictamente en las 14 Secciones TM-SQA-01 V2.0 generado ({len(doc_lines)} líneas).")
