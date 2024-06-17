using GestorEventos.Servicios.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace GestorEventos.Servicios.Servicios
{
    // La clase TipoEventoService representa un servicio para el tipo de evento.
    public class TipoEventoService
    {
        public IEnumerable<TipoEvento> TiposDeEvento { get; set; } // IEnumerable = itera sobre TiposDeEvento

        public TipoEventoService() {

            TiposDeEvento = new List<TipoEvento>
            {
                new TipoEvento { IdTipoEvento = 1, Descripcion = "Despedida de solteros"},
                new TipoEvento { IdTipoEvento = 2, Descripcion = "Despedida de solteras"}
            };

        }

        public IEnumerable<TipoEvento> GetTipoEvento()
        {
            return this.TiposDeEvento;
        }

        public TipoEvento GetTipoEventoPorId(int IdTipoEvento) {
            var tiposDeEvento = TiposDeEvento.Where(x => x.IdTipoEvento == IdTipoEvento);

            if (tiposDeEvento == null) {
                return null;
            } else {
                return tiposDeEvento.First();
            }

        }
    }


}
