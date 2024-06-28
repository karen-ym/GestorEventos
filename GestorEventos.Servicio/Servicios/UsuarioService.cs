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
    public interface IUsuarioService
    {
        int AgregarNuevoUsuario(Usuario usuario);
        Usuario? GetUsuarioPorGoogleSubject(string googleSubject);
        Usuario? GetUsuarioPorId(int IdUsuario);
        IEnumerable<Usuario> GetUsuarios();
    }

    public class UsuarioService : IUsuarioService
    {
        private string _connectionString;
        public UsuarioService()
        {

            _connectionString = "Password=pass0110!;Persist Security Info=True;User ID=admin_1;Initial Catalog=baseGestorServicios;Data Source=servidor-eventos-app.database.windows.net";
        }

        public IEnumerable<Usuario> GetUsuarios()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                List<Usuario> usuarios = db.Query<Usuario>("SELECT * FROM Usuarios").ToList();

                return usuarios;
            }
        }

        public Usuario? GetUsuarioPorId(int IdUsuario)
        {

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                Usuario usuarios = db.Query<Usuario>("SELECT * FROM Usuarios WHERE IdUsuario = " + IdUsuario.ToString()).FirstOrDefault();

                return usuarios;
            }


        }

        public Usuario? GetUsuarioPorGoogleSubject(string googleSubject)
        {

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                Usuario usuarios = db.Query<Usuario>("SELECT * FROM Usuarios WHERE GoogleIdentificador = '" + googleSubject.ToString() + "'").FirstOrDefault();
                return usuarios;
            }
        }
        public int AgregarNuevoUsuario(Usuario usuario)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Usuarios (GoogleIdentificador, Nombre, Apellido, NombreCompleto,  Email) VALUES ( @GoogleIdentificador, @Nombre, @Apellido, @NombreCompleto, @Email); SELECT CAST(SCOPE_IDENTITY() AS int)";
                int idUsuario = (int)db.ExecuteScalar(query, usuario);
                return idUsuario;
            }
        }

    }
}