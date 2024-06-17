using GestorEventos.Servicios.Entidades;
using GestorEventos.Servicios.Servicios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestorEventos.API.Controllers
{    
    [ApiController]
    [Route("api/[controller]")]

    public class ServiciosController : Controller
    {
        [HttpGet]
        public IActionResult GetServicios(){
            ServiciosService serviciosService = new ServiciosService();

            return Ok(serviciosService.GetServicios());
        }

        [HttpGet("{idServicio:int}")]
        public IActionResult GetServicioPorId(int idServicio) {

            ServiciosService serviciosService = new ServiciosService();

            var servicio = serviciosService.GetServiciosPorId(idServicio);

            if (servicio == null){
                return NotFound();
            }
            else {
                return Ok(servicio);
            }
        }

        [HttpPost("nuevo")] // La solicitud POST se utiliza para crear un nuevo objeto ServiciosVM.
        public IActionResult PostNuevoServicio([FromBody] ServiciosVM servicionuevo) {

            ServiciosService serviciosService = new ServiciosService();
            serviciosService.AgregarServicio(servicionuevo);
            
            return Ok();
        }
    }
}
