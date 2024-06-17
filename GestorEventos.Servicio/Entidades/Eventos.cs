using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorEventos.Servicios.Entidades
{
    // EXPERIMENTO GITHUB - karen
    public class Evento
    {
        public int IdEvento { get; set; }
        public string NombreEvento { get; set; }

        public DateTime FechaEvento { get; set; }

        public int CantidadPersonas { get; set; }

        public int IdTipoDespedida { get; set; }

        public int IdPersonaAgasajada { get; set; }

        public int IdPersonaContacto { get; set; }

        public bool Visible { get; set; } // Borrado lógico.

        //prueba 
        public bool borrado { get; set; }

    }
}
