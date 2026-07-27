namespace Login.Models
{
    public class Usuario
    {
        public int idUsuario { get; set; }
        public string correo { get; set; }
        public string clave { get; set; }

        public string confClave { get; set; }
    }
}
