using System;
using System.Data;
using System.Data.SqlClient;
using CapaEntidad;

namespace CapaDatos
{
    public class UsuarioDAL : CadenaDAL
    {
        public bool RegistrarUsuario(UsuarioCLS usuario, out string mensaje)
        {
            bool respuesta = false;
            mensaje = string.Empty;

            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_RegistrarUsuario", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@correo", usuario.correo);
                        cmd.Parameters.AddWithValue("@clave", usuario.clave);
                        cmd.Parameters.Add("Registrado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                        
                        cmd.ExecuteNonQuery();

                        respuesta = Convert.ToBoolean(cmd.Parameters["Registrado"].Value);
                        mensaje = cmd.Parameters["Mensaje"].Value?.ToString() ?? "Error en el registro";
                    }
                }
                catch (Exception e)
                {
                    mensaje = e.Message;
                }
            }

            return respuesta;
        }

        public bool IniciarSesion(UsuarioCLS usuario, out string mensaje, out int idUsuario, out string rol)
        {
            bool respuesta = false;
            mensaje = "";
            idUsuario = 0;
            rol = "";

            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_ValidarUsuario", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@correo", usuario.correo);
                        cmd.Parameters.AddWithValue("@clave", usuario.clave);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                idUsuario = reader.GetInt32(0);
                                rol = reader.GetString(1);
                                respuesta = true;
                                mensaje = "Inicio de sesión exitoso";
                            }
                            else
                            {
                                respuesta = false;
                                mensaje = "Correo o contraseña incorrectos";
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    respuesta = false;
                    mensaje = "Correo o contraseña incorrectos";
                }
            }

            return respuesta;
        }
    }
}
