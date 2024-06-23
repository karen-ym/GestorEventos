using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
//using static System.Runtime.InteropServices.JavaScript.JSType;
using GestorEventos.Servicios.Entidades;

namespace GestorEventos.Servicios.Servicios
{
    //agregue esto
    public interface IPersonaService
    {
        int AgregarNueva(Persona persona);
        bool BorrarL(int idPersona);
        bool BorrarF(int idPersona);
        bool Modificar(int idPersona, Persona persona);
    }
    public class PersonaService
    {
        // IEnumerable<T>: enumerador para una colección de objetos del tipo Persona. Iterar con un foreach.
        // get; set son getters y setters automaticos. 

        public IEnumerable<Persona> PersonasDePrueba { get; set; }

        private String _connectionString; // BBDD

        // Constructor
        public PersonaService()
        {

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

        public IEnumerable<Persona> GetPersonasDePrueba()
        {

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
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                Persona persona = db.Query<Persona>("SELECT * FROM Personas WHERE IdPersona = " + IdPersona.ToString()).FirstOrDefault();
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

        public int AgregarNueva(Persona persona)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                //falta agregar
                string query = "";
                persona.IdPersona= db.QuerySingle<int>(query, persona);

                return persona.IdPersona;
            }
        }
        public bool BorrarL (int idPersona)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query= "UPDATE Personas SET Borrado = 1 where IdPersona = " + idPersona.ToString();
                db.Execute(query);

                return true;
            }
        }
        public bool BorrarF(int idPersona)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM dbo.Personas WHERE IdPersona = " + idPersona.ToString();
                db.Execute(query);

                return true;
            }
        }
        public bool Modificar(int idPersona, Persona persona)
        {

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Personas SET Nombre = @Nombre, Apellido = @Apellido, Direccion = @Direccion, Telefono = @Telefono, Email = @Email  WHERE IdPersona = " + idPersona.ToString();
                db.Execute(query, persona);

                return true;
            }
        }
    }
}
// probando