using GestorEventos.Servicios.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace GestorEventos.Servicios.Servicios
{
    public interface IEventosServiciosService
    {
        IEnumerable<EventoServicio> GetServiciosPorEvento(int IdEvento);
        int PostNuevoEventoServicio(EventoServicio relacionEventoServicio);
    }

    public class EventosServiciosService : IEventosServiciosService
    {
        private string _connectionString;

        public EventosServiciosService()
        {
            _connectionString = "Password=pass0110!;Persist Security Info=True;User ID=admin_1;Initial Catalog=baseGestorServicios;Data Source=servidor-eventos-app.database.windows.net";
        }

        public int PostNuevoEventoServicio(EventoServicio relacionEventoServicio)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    string query = "INSERT INTO dbo.EventosServicios(IdEvento, IdServicio, Borrado)" +
                                    "VALUES(   @IdEvento,  @IdServicio,   0)";
                    db.Execute(query, relacionEventoServicio);


                    return relacionEventoServicio.IdEventoServicio;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public IEnumerable<EventoServicio> GetServiciosPorEvento(int IdEvento)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                List<EventoServicio> eventos = db.Query<EventoServicio>("select * from eventosServicios WHERE IdEvento =" + IdEvento.ToString()).ToList();

                return eventos;
            }
        }

    }
}
