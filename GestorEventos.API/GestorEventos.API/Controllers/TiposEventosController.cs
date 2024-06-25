using GestorEventos.Servicios.Entidades;
using GestorEventos.Servicios.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace GestorEventos.API.Controllers
{
    [ApiController] // Indica que el controlador es un controlador web API
    [Route("[controller]")] // Crea una ruta de base para el controlador ("/TiposEventos")
    public class TiposEventosController : Controller // LOS CONTROLADORES SE ENCARGAN DE MANEJAR SOLICITUDES HTTP
    {
        [HttpGet]
        public IActionResult Get()
        {
            TipoEventoService tipoEventoService = new TipoEventoService(); // Crea instancia TipoEventoService
            return Ok(tipoEventoService.GetTipoEventos()); // Devuelve 200 OK una lista de eventos
        }

        [HttpGet("{idTipoEvento:int}")]  // La solicitud GET se utiliza para recuperar un solo objeto TipoEvento.
        public IActionResult Get(int idTipoEvento)
        {
            TipoEventoService tipoEventoService = new TipoEventoService();
            TipoEvento tipoEvento = null;

            tipoEvento = tipoEventoService.GetTipoEventoPorId(idTipoEvento);

            if (tipoEvento == null){
                return NotFound(); // 404
            } else { 
                return Ok(tipoEvento); // 200
            }
        }
    }
}
