# language: es
Característica: Gestión de Pacientes
  Como Administrador del sistema
  Quiero administrar el registro de pacientes
  Para mantener actualizada la información de los pacientes del hospital

  # ------------------------------------------------------------------
  # FLUJOS PRINCIPALES (SUCCESS PATHS)
  # ------------------------------------------------------------------
  Escenario: Visualizar la lista de pacientes registrados
    Dado que el Administrador ha iniciado sesión con correo "admin@hospital.com" y clave "Admin123!"
    Cuando navega al módulo de Pacientes "/Pacientes/Index"
    Entonces la página de Pacientes debe cargarse correctamente

  Escenario: Registrar un nuevo paciente desde la interfaz
    Dado que el Administrador ha iniciado sesión con correo "admin@hospital.com" y clave "Admin123!"
    Y se encuentra en el módulo de Pacientes "/Pacientes/Index"
    Cuando llena y envía el formulario con los datos del paciente:
      | nombre    | apellido | fechaNacimiento | telefono   | email                   | direccion       |
      | Mateo     | Silva    | 1995-03-20      | 0991112233 | mateo.silva@cypress.com | Av. Patria N100 |
    Entonces el paciente "Mateo" debe ser registrado exitosamente

  # ------------------------------------------------------------------
  # FLUJOS ALTERNOS Y DE VALIDACIÓN (NEGATIVE & EDGE PATHS)
  # ------------------------------------------------------------------
  Escenario: Intento de registro de paciente sin ingresar datos (Campos Vacíos)
    Dado que el Administrador ha iniciado sesión con correo "admin@hospital.com" y clave "Admin123!"
    Y se encuentra en el módulo de Pacientes "/Pacientes/Index"
    Cuando abre el modal de registro y hace clic en Enviar sin llenar los campos
    Entonces el modal debe permanecer abierto o el sistema debe mantener la estabilidad en la página
