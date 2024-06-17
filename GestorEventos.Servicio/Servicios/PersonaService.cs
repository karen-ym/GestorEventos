using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using GestorEventos.Servicios.Entidades;

namespace GestorEventos.Servicios.Servicios
{
    public class PersonaService
    {
        // IEnumerable<T>: enumerador para una colección de objetos del tipo Persona. Iterar con un foreach.
        // get; set son getters y setters automaticos. 

        public IEnumerable<Persona> PersonasDePrueba { get; set; }

        private String _connectionString; // BBDD

        // Constructor
        public PersonaService() {

            _connectionString = ""; //string que sale de crear la bbdd - no explica como hacerlo

            /*PersonasDePrueba = new List<Persona>
            {
                    new Persona { 
                    IdPersona = 1,
                    Nombre = "Esteban",
                    Apellido = "Quito",
                    Direccion = "452 Diaz",
                    Email = "dskjds@gmail.com",
                    Telefono = "2154236412"
                    },
                    new Persona {
                    IdPersona = 2,
                    Nombre = "Raquel",
                    Apellido = "Sita",
                    Direccion = "452 Diaz",
                    Email = "iyuiy@gmail.com",
                    Telefono = "4545645"
                    }
            };*/
        }

        public IEnumerable<Persona> GetPersonasDePrueba() {

            // "using" garantiza que la conexión a la base de datos se cierre correctamente
            using (IDbConnection db = new SqlConnection(_connectionString)) // 
            {
                // Utiliza Dapper (dependencias/paquetes) para ejecutar una consulta SQL y mapear los resultados a una lista de objetos Persona
                List<Persona> personas = db.Query<Persona>("SELECT * FROM Personas").ToList();
                return personas;
            }
        }

        public Persona? GetPersonaDePruebaSegunId(int IdPersona)
        {
            using (IDbConnection db = new SqlConnection(_connectionString)) {
                Persona persona = db.Query<Persona>("SELECT * FROM Personas WHERE IdPersona = " + IdPersona.ToString().FirstOrDefault());
                return persona;
            }

                /*try
                {
                    Persona persona = PersonasDePrueba.Where(x => x.IdPersona == IdPersona).First();
                    return persona;
                }
                catch (Exception ex) {
                    return null;
                }*/
        }
    }
}
