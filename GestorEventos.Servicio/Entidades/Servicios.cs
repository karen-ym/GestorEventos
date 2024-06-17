using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorEventos.Servicios.Entidades
{
    public class ServiciosVM
    {
        public int IdServicio { get; set; }

        // ? = declarar la variable como nulable
        public string? Descripcion { get; set; }

        public decimal PrecioServicio { get; set; }
    }
}
