# language: es
Característica: Sanitización de Entradas y Auditoría de Seguridad
  Como auditor de seguridad del sistema hospitalario
  Quiero comprobar que el sistema valide, sanitice y rechace entradas maliciosas o erróneas
  Para garantizar la integridad del sistema y prevenir vulnerabilidades XSS, SQLi y errores de entrada

  # ------------------------------------------------------------------
  # PREVENCIÓN DE XSS (CROSS-SITE SCRIPTING)
  # ------------------------------------------------------------------
  Escenario: Rechazo y sanitización de script inyectado <script> en campo de texto
    Dado que el Administrador ha iniciado sesión con correo "admin@hospital.com" y clave "Admin123!"
    Y se encuentra en el módulo de Pacientes "/Pacientes/Index"
    Cuando intenta registrar un paciente con el nombre "<script>alert('XSS_ATTACK')</script>" y apellido "Prueba"
    Entonces el sistema no debe ejecutar el script malicioso
    Y la página debe mantenerse estable y sanitizada

  Escenario: Sanitización de etiquetas HTML con atributos de eventos manipulados
    Dado que el Administrador ha iniciado sesión con correo "admin@hospital.com" y clave "Admin123!"
    Y se encuentra en el módulo de Pacientes "/Pacientes/Index"
    Cuando intenta registrar un paciente con dirección "<img src=x onerror=alert('xss_image')>"
    Entonces el sistema debe codificar o neutralizar las etiquetas HTML en la respuesta
    Y no se debe desencadenar ninguna alerta no deseada

  # ------------------------------------------------------------------
  # PREVENCIÓN DE INYECCIÓN SQL (SQLi)
  # ------------------------------------------------------------------
  Escenario: Detección y bloqueo de payload SQLi en formulario de inicio de sesión
    Dado que el usuario navega a la página de inicio de sesión
    Cuando ingresa el correo "' OR '1'='1" y la contraseña "' OR '1'='1"
    Y hace clic en el botón "Iniciar Sesión"
    Entonces el sistema debe denegar el acceso y permanecer en la página de login
    Y el servidor no debe retornar excepciones no controladas de SQL

  Escenario: Inserción de payload SQLi en la búsqueda de registros
    Dado que el Administrador ha iniciado sesión con correo "admin@hospital.com" y clave "Admin123!"
    Y se encuentra en el módulo de Pacientes "/Pacientes/Index"
    Cuando realiza una búsqueda con el término "'; DROP TABLE Pacientes;--"
    Entonces el sistema debe responder de manera segura sin exponer errores de base de datos

  # ------------------------------------------------------------------
  # VALIDACIÓN ESTRICTA DE TIPOS Y CARACTERES INVÁLIDOS
  # ------------------------------------------------------------------
  Escenario: Rechazo de caracteres especiales no permitidos en nombres de usuario
    Dado que el Administrador ha iniciado sesión con correo "admin@hospital.com" y clave "Admin123!"
    Y se encuentra en el módulo de Pacientes "/Pacientes/Index"
    Cuando llena el campo de teléfono con letras y caracteres especiales "ABC-##$$--!!"
    Entonces el formulario debe indicar error de formato o rechazar el envío del dato no numérico

  Escenario: Manejo de cadenas extremadamente largas (Stress / Buffer Overflow test)
    Dado que el Administrador ha iniciado sesión con correo "admin@hospital.com" y clave "Admin123!"
    Y se encuentra en el módulo de Pacientes "/Pacientes/Index"
    Cuando ingresa un nombre con una cadena de 1000 caracteres "AAAAA..."
    Entonces el sistema debe truncar la entrada o rechazarla sin provocar caída del servidor
