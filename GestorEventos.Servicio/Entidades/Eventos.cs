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
        //comentario: Representa un evento básico con todas sus propiedades fundamentales.
        public int IdEvento { get; set; }
        public string NombreEvento { get; set; }
        public DateTime FechaEvento { get; set; }
        public int CantidadPersonas { get; set; }
        public int IdTipoEvento { get; set; }
        public int IdPersonaAgasajada { get; set; }

        public int IdUsuario { get; set; }

        public bool Visible { get; set; }
        public bool Borrado { get; set; }

        public int IdEstadoEvento { get; set; }

    }
    public class EventoViewModel : Evento
    {
        //comentario: Extiende Evento para añadir información adicional útil para las vistas.
        public string EstadoEvento { get; set; }
    }

    public class EventoModel : Evento
    {
        // comentario: incluye detalles adicionales sobre la persona agasajada y los servicios contratados, permitiendo gestionar
        // y mostrar datos más completos y detallados sobre el evento.
        public int IdPersona { get; set; }

        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }

        public bool Borrado { get; set; }

        public List<Servicio> ListaDeServiciosContratados { get; set; }

        public IEnumerable<Servicio>? ListaDeServiciosDisponibles { get; set; }
        public Persona personaAgasajada { get; set;}
        public Evento Evento { get; set; }
    }
}
 