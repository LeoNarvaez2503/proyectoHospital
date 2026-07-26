# language: es
Característica: Gestión de Médicos, Control de Roles (RBAC) y Sanitización
  Como usuario del sistema hospitalario
  Quiero acceder al módulo de médicos según los permisos de mi rol
  Para garantizar la seguridad de la información del personal médico y la sanitización de búsquedas

  Escenario: Acceso permitido al módulo de Médicos para el rol Admin
    Dado que el Administrador ha iniciado sesión con correo "admin@hospital.com" y clave "Admin123!"
    Cuando navega al módulo de Médicos "/Medicos/Index"
    Entonces la página de Médicos debe cargarse correctamente

  Escenario: Denegación de acceso al módulo de Médicos para el rol Usuario
    Dado que un usuario con rol Usuario inicia sesión con correo "usuario@hospital.com" y clave "Usuario123!"
    Cuando intenta navegar al módulo de Médicos "/Medicos/Index"
    Entonces debe ser redirigido a la página de acceso denegado "/Acceso/Denegado"

  Escenario: Sanitización en la búsqueda de médicos contra inyección de comandos
    Dado que el Administrador ha iniciado sesión con correo "admin@hospital.com" y clave "Admin123!"
    Cuando navega al módulo de Médicos "/Medicos/Index"
    Y realiza una búsqueda de médicos con el parámetro "<script>alert('medico_xss')</script>"
    Entonces la tabla de médicos no debe ejecutar la carga útil inyectada y permanecer estable
