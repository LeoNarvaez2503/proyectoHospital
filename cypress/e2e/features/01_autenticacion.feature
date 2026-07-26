# language: es
Característica: Autenticación, Control de Acceso y Validación de Entradas
  Como usuario del sistema hospitalario
  Quiero iniciar y cerrar sesión de manera segura
  Para acceder a las funcionalidades del sistema según mi rol y prevenir accesos no autorizados

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

  Escenario: Intento de inicio de sesión con formato de correo electrónico inválido
    Dado que el usuario navega a la página de inicio de sesión
    Cuando ingresa el correo "correo_invalido_sin_dominio" y la contraseña "Admin123!"
    Y hace clic en el botón "Iniciar Sesión"
    Entonces el navegador o el sistema debe marcar el correo como inválido o impedir el ingreso

  Escenario: Intento de registro con correo que contiene inyección XSS
    Dado que el usuario navega a la página de inicio de sesión
    Y conmuta al panel de Registro usando el botón de deslizamiento
    Cuando llena el formulario de registro con correo "<script>alert('xss_reg')</script>@test.com", clave "Clave123!" y confirmación diferente "Clave123!"
    Y envía el formulario de registro
    Entonces el sistema debe sanitizar la entrada y no ejecutar ningún script en pantalla
