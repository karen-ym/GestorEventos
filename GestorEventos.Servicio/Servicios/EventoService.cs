using GestorEventos.Servicios.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using System.Xml.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorEventos.Servicios.Servicios
{
    public interface IEventoService
    {
        public interface IEventoService
        {
            bool DeleteEvento(int idEvento);
            IEnumerable<Evento> GetAllEventos();
            IEnumerable<EventoViewModel> GetAllEventosViewModel();
            IEnumerable<EventoViewModel> GetMisEventos(int IdUsuario);
            Evento GetEventoPorId(int IdEvento);
            int PostNuevoEvento(Evento evento);
            bool PutNuevoEvento(int idEvento, Evento evento);
        }

        public class EventoService : IEventoService
        {
            private string _connectionString;



            public EventoService()
            {

                //Connection string 
                _connectionString = "Password=Db4dmin!;Persist Security Info=True;User ID=dbadmin;Initial Catalog=gestioneventos;Data Source=azunlz2024dbdes01.database.windows.net";


            }


            public IEnumerable<Evento> GetAllEventos()
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    List<Evento> eventos = db.Query<Evento>("SELECT * FROM Eventos WHERE Borrado = 0").ToList();

                    return eventos;

                }
            }

            public IEnumerable<EventoViewModel> GetMisEventos(int idUsuario)
            {

                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    List<EventoViewModel> eventos = db.Query<EventoViewModel>("select eventos.*, EstadosEventos.Descripcion EstadoEvento from eventos left join EstadosEventos on EstadosEventos.IdEstadoEvento = eventos.idEstadoEvento WHERE Eventos.IdUsuario =" + idUsuario.ToString()).ToList();

                    return eventos;

                }
            }


            public IEnumerable<EventoViewModel> GetAllEventosViewModel()
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    List<EventoViewModel> eventos = db.Query<EventoViewModel>("select eventos.*, EstadosEventos.Descripcion EstadoEvento from eventos left join EstadosEventos on EstadosEventos.IdEstadoEvento = eventos.idEstadoEvento").ToList();

                    return eventos;

                }
            }


            public Evento GetEventoPorId(int IdEvento)
            {

                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    Evento eventos = db.Query<Evento>("SELECT * FROM Eventos WHERE Borrado = 0").First();

                    return eventos;

                }
            }


            public bool PostNuevoEvento(Evento evento)
            {
                try
                {
                    List<Evento> lista = this.Eventos.ToList();
                    lista.Add(evento);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

            public bool PutNuevoEvento(int idEvento, Evento evento)
            {
                try
                {
                    var eventoDeLista = this.Eventos.Where(x => x.IdEvento == idEvento).First();

                    eventoDeLista.NombreEvento = evento.NombreEvento;
                    eventoDeLista.FechaEvento = evento.FechaEvento;
                    eventoDeLista.CantidadPersonas = evento.CantidadPersonas;
                    eventoDeLista.IdPersonaAgasajada = evento.IdPersonaAgasajada;

                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

            public bool DeleteEvento(int idEvento)
            {
                try
                {
                    var eventoAEliminar = this.Eventos.Where(x => x.IdEvento == idEvento).First();
                    var listaEventos = this.Eventos.ToList();
                    eventoAEliminar.Visible = false;
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

            public void PostNuevoEventoCompleto(EventoModel eventoModel)
            {
                PersonaService personaService = new PersonaService();
                int idPersonaAgasajada = personaService.AgregarNueva(eventoModel.PersonaAgasajada);

                eventoModel.evento.IdPersonaAgasajada = idPersonaAgasajada;
                eventoModel.evento.Visible = true;

                this.PostNuevoEvento(eventoModel.Evento);

                foreach (Servicio servicio in eventoModel.ListaDeServiciosContratados)
                {
                    ServicioService servicioService = new ServicioService();
                    servicioService.AgregarNuevoServicio(servicio);
                }

            }
        }
    }
}
