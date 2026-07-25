# language: es
Característica: Autenticación y Control de Acceso
  Como usuario del sistema hospitalario
  Quiero iniciar y cerrar sesión
  Para acceder a las funcionalidades del sistema según mi rol

  # ------------------------------------------------------------------
  # FLUJOS PRINCIPALES (SUCCESS PATHS)
  # ------------------------------------------------------------------
  Escenario: Inicio de sesión exitoso como Admin
    Dado que el usuario navega a la página de inicio de sesión
    Cuando ingresa el correo "admin@hospital.com" y la contraseña "Admin123!"
    Y hace clic en el botón "Iniciar Sesión"
    Entonces debe ser redirigido al dashboard principal en "/Home/Index"

  Escenario: Cierre de sesión exitoso
    Dado que el usuario ha iniciado sesión como "admin@hospital.com" con contraseña "Admin123!"
    Cuando hace clic en el botón de cerrar sesión
    Entonces debe ser redirigido a la página de login "/Acceso/Login"

  # ------------------------------------------------------------------
  # FLUJOS ALTERNOS Y DE VALIDACIÓN (NEGATIVE & EDGE PATHS)
  # ------------------------------------------------------------------
  Escenario: Intento de inicio de sesión con contraseña incorrecta
    Dado que el usuario navega a la página de inicio de sesión
    Cuando ingresa el correo "admin@hospital.com" y la contraseña "PasswordErrada123"
    Y hace clic en el botón "Iniciar Sesión"
    Entonces debe permanecer en la página de login
    Y el sistema debe controlar el error mostrando el contenedor de alerta sin romper la página

  Escenario: Intento de registro con contraseñas no coincidentes mantiene la vista del formulario
    Dado que el usuario navega a la página de inicio de sesión
    Y conmuta al panel de Registro usando el botón de deslizamiento
    Cuando llena el formulario de registro con correo "test.nuevo@hospital.com", clave "Clave123!" y confirmación diferente "ClaveDiferente456!"
    Y envía el formulario de registro
    Entonces el sistema debe mantener al usuario en el formulario de registro y permanecer estable
