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
        // Password=wordPASS#;Persist Security Info=True;User ID=admin_1;Initial Catalog=DDBBEventos;Data Source=servidor-eventos-app.database.windows.net

        // Constructor
        public PersonaService() {

            _connectionString = "Password=wordPASS#;Persist Security Info=True;User ID=admin_1;Initial Catalog=DDBBEventos;Data Source=servidor-eventos-app.database.windows.net";
        }

        public IEnumerable<Persona> GetPersonasDePrueba() {

            // "using" garantiza que la conexión a la base de datos se cierre correctamente
            using (IDbConnection db = new SqlConnection(_connectionString)) // 
            {
                // Utiliza Dapper (dependencias/paquetes) para ejecutar una consulta SQL y mapear los resultados a una lista de objetos Persona
                List<Persona> personas = db.Query<Persona>("SELECT * FROM Personas WHERE Borrado = 0").ToList();
                return personas;
            }
        }

        /*public Persona? GetPersonaDePruebaSegunId(int IdPersona)
        {
            // al profe le marca el mismo error en clase, pero a el le corría el código
            using (IDbConnection db = new SqlConnection(_connectionString)) {
                Persona persona = db.Query<Persona>("SELECT * FROM Personas WHERE IdPersona = " + IdPersona.ToString().FirstOrDefault());
                return persona;
            }
        }*/

        public int AgregarNuevaPersona(Persona persona) {
            using (IDbConnection db = new SqlConnection(_connectionString)) {
                string query = "INSERT INTO Personas (Nombre, Apellido, Direccion, Telefono, Email) VALUES ( @Nombre, @Apellido, @Direccion, @Telefono, @Email); SELECT Inserted.IdPersona";
                db.Execute(query, persona);

                return persona.IdPersona;
            }
        }

        public bool ModificarPersona(int idPersona, Persona persona)
        {
            using (IDbConnection db = new SqlConnection(_connectionString)) {
                string query = "UPDATE Personas SET Nombre = @Nombre, Apellido = @Apellido, Direccion = @Direccion, Telefono = @Telefono, Email = @Email WHERE IdPersona = " + idPersona.ToString(); // se concatena idPersona pq dapper puede dar error  
                db.Execute(query, persona);
                return true;
            }
        }

        public bool BorrarLogicamentePersona (int idPersona) {
            using (IDbConnection db = new SqlConnection(_connectionString)) {
                string query = "UPDATE Personas SET Borrado = 1 where IdPersona = " + idPersona.ToString();
                db.Execute(query);
                return true;
            }
        }
    }
}
//nuevo comentario