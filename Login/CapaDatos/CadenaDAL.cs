using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using Microsoft.Extensions.Configuration;

namespace CapaDatos
{
    public class CadenaDAL
    {
        public string cadenaDato { get; set; }
        public CadenaDAL()
        {
            IConfigurationBuilder cfg = new ConfigurationBuilder();
            cfg.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));
            cfg.AddEnvironmentVariables();
            var root = cfg.Build();
            cadenaDato = root.GetConnectionString("cn");
        }

    }
}
