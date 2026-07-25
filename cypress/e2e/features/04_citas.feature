# language: es
Característica: Agendamiento de Citas Médicas
  Como usuario autorizado
  Quiero agendar citas médicas para los pacientes
  Para organizar la atención médica en el hospital

  Escenario: Acceso al módulo de Citas
    Dado que el Administrador ha iniciado sesión con correo "admin@hospital.com" y clave "Admin123!"
    Cuando navega al módulo de Citas "/Citas/Citas"
    Entonces la página de Citas debe cargarse correctamente
