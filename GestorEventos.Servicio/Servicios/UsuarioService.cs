using GestorEventos.Servicios.Entidades;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorEventos.Servicios.Servicios
{
    public interface IUsuarioService
    {
        // se qurias empezar 
        int AgregarNuevo (Usuario usuario);

    }
}
