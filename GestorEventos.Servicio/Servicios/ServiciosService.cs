using GestorEventos.Servicios.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorEventos.Servicios.Servicios
{
    public class ServiciosService // La clase ServiciosService es responsable de gestionar los servicios.
    {
        public IEnumerable<ServiciosVM> Servicios { get; set; }

        // Constructor
        public ServiciosService() {
            this.Servicios = new List<ServiciosVM> {
                new ServiciosVM { IdServicio = 1, Descripcion = "Bar hopping", PrecioServicio = 25000},
                new ServiciosVM { IdServicio = 2, Descripcion = "Servicio de Transporte", PrecioServicio = 20000},
                new ServiciosVM { IdServicio = 3, Descripcion = "Entradas de Boliches Incluidas", PrecioServicio = 10000}
            };
        }

        public IEnumerable<ServiciosVM> GetServicios() { 
            return this.Servicios;
        }

        public ServiciosVM GetServiciosPorId(int IdServicio) {
            var servicios = Servicios.Where(x => x.IdServicio == IdServicio);

            if (servicios == null){
                return null;
            }

            return servicios.First();
        }

        public bool AgregarServicio(ServiciosVM servicio) {

            // Se intenta agregar el nuevo objeto ServiciosVM a la colección.
            try
            {
                List<ServiciosVM> lista = this.Servicios.ToList(); // Convierte a la colección de objetos ServiciosVM en una lista.
                lista.Add(servicio); // Se agrega obj a la lista.
                // this.Servicios.ToList().Add(servicio); // Se agrega obj a la coleccion - por ahora no sirve para nada
                return true;
            } catch (Exception ex) { 
                return false; 
            }
            
        }
    }

}
