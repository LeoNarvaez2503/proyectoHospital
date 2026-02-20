# Sistema de Gestión Hospitalaria

**Equipo:** Caetano Flores · Leonardo Narváez  
**Institución:** Universidad de las Fuerzas Armadas ESPE  

---

## Descripción

Sistema web de gestión hospitalaria desarrollado con **ASP.NET Core MVC** bajo una arquitectura de cuatro capas. Permite administrar los procesos internos de un hospital incluyendo el control de pacientes, médicos, citas, tratamientos y facturación, con un sistema de roles y autenticación que distingue entre Secretario y Médico.

---

## Funcionalidades

- Gestión de **Pacientes**, **Médicos** y **Especialidades**
- Registro y seguimiento de **Citas Médicas** y **Tratamientos**
- Módulo de **Facturación**
- Autenticación de usuarios con **roles diferenciados** (Secretario / Médico)
- Permisos por controlador según rol asignado

---

## Tecnologías Utilizadas

![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core_MVC-512BD4?style=flat&logo=dotnet&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?style=flat&logo=microsoftsqlserver&logoColor=white)
![Bootstrap](https://img.shields.io/badge/Bootstrap-7952B3?style=flat&logo=bootstrap&logoColor=white)

| Tecnología | Uso |
|---|---|
| **ASP.NET Core MVC** | Framework principal del backend |
| **SQL Server** | Base de datos con procedimientos almacenados |
| **Entity Framework Core** | ORM para mapeo objeto-relacional |
| **Bootstrap / Materialize CSS** | Diseño y responsividad del frontend |
| **JavaScript** | Interactividad en el cliente |
| **C#** | Lenguaje de programación |

---

## Arquitectura por Capas

El sistema sigue el patrón de arquitectura de **cuatro capas** para separar responsabilidades y facilitar el mantenimiento:

| Capa | Descripción |
|---|---|
| **Capa de Entidad** | Clases que representan las tablas de la base de datos |
| **Capa de Datos** | Acceso y manipulación de datos mediante procedimientos almacenados |
| **Capa de Negocio** | Lógica de validaciones y procesamiento de datos |
| **Capa de Presentación** | Controladores y vistas MVC que interactúan con el usuario |

---

## Estructura del Proyecto

```
proyectoHospital/
├── CapaEntidad/       # Modelos: UsuarioCLS, MedicoCLS, CitasCLS, PacienteCLS...
├── CapaDatos/         # DAL: acceso a BD con procedimientos almacenados
├── CapaNegocio/       # BL: lógica de negocio e intermediario entre capas
└── Login/             # Capa de Presentación: controladores, vistas y autenticación
```

---

## Roles y Permisos

- **Secretario:** acceso a Citas, Pacientes y Facturación.
- **Médico:** acceso a Citas, Pacientes y Tratamientos.

La autenticación se gestiona mediante atributos `[Authorize(Roles = "Rol")]` en los controladores.

---

## Ejemplo de Capa de Entidad

```csharp
public class CitasCLS
{
    public int idCita { get; set; }
    public int idPaciente { get; set; }
    public int idMedico { get; set; }
    public DateTime fecha { get; set; }
    public string estado { get; set; }
}
```

## Ejemplo de Capa de Datos

```csharp
public class CitasDAL : CadenaDAL
{
    public List<CitasCLS> ListarCitas()
    {
        List<CitasCLS> lista = null;
        using (SqlConnection cn = new SqlConnection(cadenaDato))
        {
            cn.Open();
            using (SqlCommand cmd = new SqlCommand("uspListarCitas", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                lista = new List<CitasCLS>();
                while (dr.Read())
                {
                    lista.Add(new CitasCLS
                    {
                        idCita = dr.GetInt32(0),
                        idPaciente = dr.GetInt32(1),
                        idMedico = dr.GetInt32(2),
                        fecha = dr.GetDateTime(3),
                        estado = dr.GetString(4)
                    });
                }
            }
        }
        return lista;
    }
}
```

## Ejemplo de Capa de Negocio

```csharp
public class CitasBL
{
    public List<CitasCLS> ListarCitas() => new CitasDAL().ListarCitas();
    public int GuardarCita(CitasCLS obj) => new CitasDAL().GuardarCitas(obj);
    public int EliminarCita(int id) => new CitasDAL().EliminarCitas(id);
    public CitasCLS RecuperarCitas(int id) => new CitasDAL().RecuperarCitas(id);
}
```


**Proyecto de Sistema de Gestión Hospitalaria** en ASP.NET Core MVC con una arquitectura por capaz para mejorar la escalabilidad y mantenimiento del software.

**Descripción:** 

El sistema permitirá administrar información clave de un hospital en la cual incluimos:
-   Pacientes.
-   Médicos.
-   Citas Médicas.
-   Tratamientos.
-   Facturación.
-   Usuarios

**Tecnologías utilizadas:**
-   ASP.NET Core MVC.
-   Base de datos: SQL Server con procedimientos almacenados.
-   Frontend: Bootstraop o Materialize CSS para mejorar la interfaz.
-   Interactividad: JavaScript.
-   ORM: Entity Framework Core

El sistema va seguir la siguiente estructura:

1. **Capa de Entidad**: En esta capa van las clases que representarán las tablas de las bases de datos.
2. **Capa de Datos**: Acceso y manipulación a la base de datos mediante procedimientos almacenados.
3. **Capa de Negocio**: Lógica de validaciones y procesamiento de datos.
4. **Capa Presentación**: Controladores y vistas que maneja la interaccion con el usuario.

**Estructura del proyecto**:
Hemos creado dos tipos de usuarios la cuales son **Secretario** y **Médico**, cada uno contara con permisos específicos para poder acceder a los controladores.

1. **Capa de Presentación**: Contiene los controladores y vistas.
2. **Capa de Negocio**: Contiene la lógica de negocio y llama a las funciones de la capa de datos.
3. **Capa de Datos**: Contiene la lógica para interactuar con la base de datos (procedimientos almacenados).
4. **Capa de Entidad**: Contiene los modelos que representan las tablas de la base de datos.

# Capa Entidad

Esta capa contiene las clases que representan las tablas de la base de datos. Cada clase debe tener propiedades que coincidan con las columnas de la tabla correspondiente.

Ejemplo de la estructura:

    -CapaEntidad/
        -UsuarioCLS.cs
        -MedicoCLS.cs
        -CitasCLS.cs
        -PacienteCLS.cs
        -TratamientoCLS.cs
        -FacturaCLS.cs
        -EspecialidadCLS.cs

Ejemplo de clase CitasCLS:

    public class CitasCLS
    {
        public int idCita { get; set; }
        public int idPaciente { get; set; }
        public int idMedico { get; set; }
        public DateTime fecha { get; set; }
        public string estado { get; set; }
    }

# Capa de Datos

Esta capa contiene las clases que interactúan directamente con la base de datos. Aquí se implementan los procedimientos almacenados.

Ejemplo de la estructura:

    - CapaDatos/
        - UsuarioDAL.cs
        - MedicoDAL.cs
        - CitasDAL.cs
        - PacienteDAL.cs
        - TratamientoDAL.cs
        - FacturaDAL.cs
        - EspecialidadDAL.cs

Ejemplo de clase CitasDAL:

    public class CitasDAL : CadenaDAL
    {
        public List<CitasCLS> ListarCitas()
        {
            List<CitasCLS> lista = null;
            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspListarCitas", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        SqlDataReader dr = cmd.ExecuteReader();
                        lista = new List<CitasCLS>();
                        while (dr.Read())
                        {
                            CitasCLS citas = new CitasCLS();
                            citas.idCita = dr.IsDBNull(0) ? -1 : dr.GetInt32(0);
                            citas.idPaciente = dr.IsDBNull(1) ? -1 : dr.GetInt32(1);
                            citas.idMedico = dr.IsDBNull(2) ? -1 : dr.GetInt32(2);
                            citas.fecha = dr.GetDateTime(3);
                            citas.estado = dr.IsDBNull(4) ? "" : dr.GetString(4);
                            lista.Add(citas);
                        }
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Error al listar citas: " + e.Message);
                }
                return lista;
            }
        }
    }

# Capa de Negocio

Esta capa contiene la lógica de negocio y actúa como intermediario entre la capa de presentación y la capa de datos. Aquí se llaman las funciones de la capa de datos según sea necesario.

Ejemplo de la estructura:

    - Negocio/
        - UsuarioBL.cs
        - MedicoBL.cs
        - CitasBL.cs
        - PacienteBL.cs
        - TratamientoBL.cs
        - FacturaBL.cs
        - EspecialidadBL.cs

Ejemplo de clase CitasBL:

    public class CitasBL
    {
        public List<CitasCLS> ListarCitas()
        {
            CitasDAL objCitasDAL = new CitasDAL();
            return objCitasDAL.ListarCitas();
        }
        public int GuardarCita(CitasCLS objCitasCLS)
        {
            CitasDAL objCitasDAL = new CitasDAL();
            return objCitasDAL.GuardarCitas(objCitasCLS);
        }
        public int EliminarCita(int id)
        {
            CitasDAL objCitasDAL = new CitasDAL();
            return objCitasDAL.EliminarCitas(id);
        }
        public CitasCLS RecuperarCitas(int idCita)
        {
            CitasDAL objCitasDAL = new CitasDAL();
            return objCitasDAL.RecuperarCitas(idCita);
        }
        public List<CitasCLS> FiltrarCitas(CitasCLS objCitasCLS)
        {
            CitasDAL objCitasDAL = new CitasDAL();
            return objCitasDAL.FiltrarCitas(objCitasCLS);
        }
    }

# Capa de presentación

Esta capa contiene los controladores y vistas. Cada controlador corresponde a una tabla y llama a las funciones de la capa de negocio.

Ejemplo de la estructura:

    - Presentacion/
        - Controladores/
            - AccesoController.cs
            - CitasController.cs
            - PacientesController.cs
            - TratamientosController.cs
            - FacturacionController.cs
            - EspecialidadesController.cs
            - MedicosController.cs
            - HomeController.cs
        - Vistas/
            - Acceso/
            - Login.cshtml
            - Registrar.cshtml
            - Citas/
            - Index.cshtml
            - Crear.cshtml
            - Pacientes/
            - Index.cshtml
            - Detalles.cshtml
            - Tratamientos/
            - Index.cshtml
            - Facturacion/
            - Index.cshtml
            - Especialidades/
            - Index.cshtml
            - Medicos/
            - Index.cshtml
            - Home/
            - Index.cshtml
            - Privacy.cshtml

Ejemplo de CitasController:

        public class CitasController : Controller
        {
            public IActionResult Citas()
            {
                return View();
            }
            public List<CitasCLS> ListarCitas()
            {
                CitasBL objCitasBL = new CitasBL();
                return objCitasBL.ListarCitas();
            }
            public int GuardarCita(CitasCLS objCitasCLS)
            {
                CitasBL objCitasBL = new CitasBL();
                return objCitasBL.GuardarCita(objCitasCLS);
            }

            public int EliminarCita(int id)
            {
                CitasBL objCitasBL = new CitasBL();
                return objCitasBL.EliminarCita(id);
            }

            public CitasCLS RecuperarCitas(int id)
            {
                CitasBL objCitasBL = new CitasBL();
                return objCitasBL.RecuperarCitas(id);
            }

            public List<CitasCLS> FiltrarCitas(CitasCLS objCitasCLS)
            {
                CitasBL objCitasBL = new CitasBL();
                return objCitasBL.FiltrarCitas(objCitasCLS);
            }
        }

# Permisos y Roles

- **Usuario (Secretario)**:

    - Puede acceder a CitasController, PacientesController y FacturacionController.

    - No puede acceder a TratamientosController ni a EspecialidadesController.

- **Médico**:

    - Puede acceder a CitasController, PacientesController y TratamientosController.

    - No puede acceder a FacturacionController ni a EspecialidadesController.

# Autenticación:
- **Inicio de sesión**: Los usuarios deben iniciar sesión para interactuar con la aplicación, excepto en las páginas Home/Index y Home/Privacy.

- **Roles**: Los roles se asignan durante el inicio de sesión y se verifican en los controladores usando [Authorize(Roles = "Rol")].

# Vistas y Grid 

- **Vistas**: Cada controlador tiene vistas asociadas (por ejemplo, Citas/Index.cshtml).

- **DataGrid**: Se usa para la paginación y visualización de datos en las vistas.

Resumen de la estructura:

| **Capa** | **Descripción** |
|------|-------------|
| Modelos | Representan las tablas de la base de datos |
| Datos | Lógica para interactuar con la base de datos (procedimientos almacenados) |
| Negocio | Lógica de negocio que llama a las funciones de la capa de datos |
| Presentación | Controladores y vistas que interactúan con el usuario |