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
        //comentario: La interfaz IEventoService define los métodos que el servicio debe implementar
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
        private string _connectionString;
        //comentario: inicializa la cadena de conexión a la base de datos.
        public EventoService()
        {
            // Connection string 
            _connectionString = "Password=pass0110!;Persist Security Info=True;User ID=admin_1;Initial Catalog=baseGestorServicios;Data Source=servidor-eventos-app.database.windows.net";
        }

        public IEnumerable<Evento> GetAllEventos()
        {
            //comentario: Este método obtiene todos los eventos que no han sido borrados lógicamente "borrado=0"
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<Evento>("SELECT * FROM Eventos WHERE Borrado = 0").ToList();
            }
        }

        public IEnumerable<EventoViewModel> GetMisEventos(int idUsuario)
        {
            //comentario: obtiene los eventos de un usuario especifico 
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<EventoViewModel>("SELECT eventos.*, EstadosEventos.Descripcion AS EstadoEvento FROM eventos LEFT JOIN EstadosEventos ON EstadosEventos.IdEstadoEvento = eventos.idEstadoEvento WHERE eventos.IdUsuario = @IdUsuario", new { IdUsuario = idUsuario }).ToList();
            }
        }

        public IEnumerable<EventoViewModel> GetAllEventosViewModel()
        {
            //comentario: obtiene todos los eventos y sus estados, devolviéndolos como una lista
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
            //comentario: inserta un nuevo evento en la base de datos y devuelve el ID del evento recién creado.
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Eventos (NombreEvento, FechaEvento, CantidadPersonas, IdPersonaAgasajada, Visible) VALUES (@NombreEvento, @FechaEvento, @CantidadPersonas, @IdPersonaAgasajada, @Visible); SELECT CAST(SCOPE_IDENTITY() AS int)";
                return db.ExecuteScalar<int>(query, evento);
            }
        }

        public bool PutNuevoEvento(int idEvento, Evento evento)
        {
            //comentario: actualiza un evento existente en la base de datos.
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
            //comentario: creación de un evento completo, incluyendo la persona agasajada y los servicios contratados, añade una nueva persona usando PersonaService
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