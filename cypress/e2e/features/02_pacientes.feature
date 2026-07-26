# language: es
Característica: Gestión de Pacientes y Sanitización de Formulario
  Como Administrador del sistema
  Quiero administrar el registro de pacientes
  Para mantener actualizada la información de los pacientes del hospital con datos limpios y válidos

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

  Escenario: Registro de paciente con número de teléfono inválido (con caracteres alfabéticos)
    Dado que el Administrador ha iniciado sesión con correo "admin@hospital.com" y clave "Admin123!"
    Y se encuentra en el módulo de Pacientes "/Pacientes/Index"
    Cuando intenta registrar un paciente con teléfono "TELEFONO_INVALIDO_123"
    Entonces el sistema debe denegar el registro o mostrar mensaje de validación de campo numérico

  Escenario: Registro de paciente con correo electrónico malformado
    Dado que el Administrador ha iniciado sesión con correo "admin@hospital.com" y clave "Admin123!"
    Y se encuentra en el módulo de Pacientes "/Pacientes/Index"
    Cuando intenta registrar un paciente con email "correo_sin_formato_valido"
    Entonces la interfaz debe señalar el error en el campo email y mantener la estabilidad
