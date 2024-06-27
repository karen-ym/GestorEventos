using GestorEventos.Servicios.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace GestorEventos.Servicios.Servicios
{
    public interface IEventoService
    {
        bool DeleteEvento(int idEvento);
        IEnumerable<Evento> GetAllEventos();
        IEnumerable<EventoViewModel> GetAllEventosViewModel();
        IEnumerable<EventoViewModel> GetMisEventos(int idUsuario);
        Evento GetEventoPorId(int idEvento);
        int PostNuevoEvento(Evento evento);
        bool PutNuevoEvento(int idEvento, Evento evento);
    }

    public class EventoService : IEventoService
    {
        private readonly string _connectionString;

        public EventoService()
        {
            // Connection string 
            _connectionString = "Password=Db4dmin!;Persist Security Info=True;User ID=dbadmin;Initial Catalog=gestioneventos;Data Source=azunlz2024dbdes01.database.windows.net";
        }

        public IEnumerable<Evento> GetAllEventos()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<Evento>("SELECT * FROM Eventos WHERE Borrado = 0").ToList();
            }
        }

        public IEnumerable<EventoViewModel> GetMisEventos(int idUsuario)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<EventoViewModel>("SELECT eventos.*, EstadosEventos.Descripcion AS EstadoEvento FROM eventos LEFT JOIN EstadosEventos ON EstadosEventos.IdEstadoEvento = eventos.idEstadoEvento WHERE eventos.IdUsuario = @IdUsuario", new { IdUsuario = idUsuario }).ToList();
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

        public Evento GetEventoPorId(int idEvento)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<Evento>("SELECT * FROM Eventos WHERE Borrado = 0 AND IdEvento = @IdEvento", new { IdEvento = idEvento }).FirstOrDefault();
            }
        }

        public int PostNuevoEvento(Evento evento)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Eventos (NombreEvento, FechaEvento, CantidadPersonas, IdPersonaAgasajada, Visible) VALUES (@NombreEvento, @FechaEvento, @CantidadPersonas, @IdPersonaAgasajada, @Visible); SELECT CAST(SCOPE_IDENTITY() AS int)";
                return db.ExecuteScalar<int>(query, evento);
            }
        }

        public bool PutNuevoEvento(int idEvento, Evento evento)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Eventos SET NombreEvento = @NombreEvento, FechaEvento = @FechaEvento, CantidadPersonas = @CantidadPersonas, IdPersonaAgasajada = @IdPersonaAgasajada WHERE IdEvento = @IdEvento";
                int affectedRows = db.Execute(query, new { evento.NombreEvento, evento.FechaEvento, evento.CantidadPersonas, evento.IdPersonaAgasajada, IdEvento = idEvento });
                return affectedRows > 0;
            }
        }

        public bool DeleteEvento(int idEvento)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Eventos SET Visible = 0 WHERE IdEvento = @IdEvento";
                int affectedRows = db.Execute(query, new { IdEvento = idEvento });
                return affectedRows > 0;
            }
        }
        public void PostNuevoEventoCompleto(EventoModel eventoModel)
        {
            PersonaService personaService = new PersonaService();
            int idPersonaAgasajada = personaService.AgregarNueva(eventoModel.personaAgasajada);

            eventoModel.Evento.IdPersonaAgasajada = idPersonaAgasajada;
            eventoModel.Evento.Visible = true;

            this.PostNuevoEvento(eventoModel.Evento);

            foreach (Servicio servicio in eventoModel.ListaDeServiciosContratados)
            {
                ServicioService servicioService = new ServicioService();
                servicioService.AgregarNuevoServicio(servicio);
            }
        }
    }
}