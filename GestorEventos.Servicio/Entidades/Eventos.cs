using GestorEventos.Servicios.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorEventos.Servicios.Entidades
{
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
    }

    public class EventoModel // Version compleja del evento.
    {
        public Evento evento { get; set; }
        public Persona PersonaContacto { get; set; }
        public Persona PersonaAgasajada { get; set; }
        public IEnumerable<Servicio> ListaDeServiciosContratados { get; set; }
    }
}
 