# language: es
Característica: Agendamiento de Citas Médicas y Validación de Fechas/Horarios
  Como usuario autorizado
  Quiero agendar citas médicas para los pacientes
  Para organizar la atención médica en el hospital de manera segura y sin solapamientos ni fechas erróneas

  Escenario: Acceso al módulo de Citas
    Dado que el Administrador ha iniciado sesión con correo "admin@hospital.com" y clave "Admin123!"
    Cuando navega al módulo de Citas "/Citas/Citas"
    Entonces la página de Citas debe cargarse correctamente

  Escenario: Control de error al agendar cita con fecha pasada o no disponible
    Dado que el Administrador ha iniciado sesión con correo "admin@hospital.com" y clave "Admin123!"
    Cuando navega al módulo de Citas "/Citas/Citas"
    Y tenta agendar una cita con una fecha pasada "2020-01-01"
    Entonces el sistema debe impedir la selección o denegar la creación de la cita pasada
