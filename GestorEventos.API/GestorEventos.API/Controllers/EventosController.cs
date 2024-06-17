using GestorEventos.Servicios.Entidades;
using GestorEventos.Servicios.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace GestorEventos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventosController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetEventos()
        {
            EventoService eventosService = new EventoService();
            return Ok(eventosService.GetAllEventos());
        }

        [HttpGet("{idEvento:int}")]
        public IActionResult GetEventoPorId(int idEvento)
        {
            EventoService eventosService = new EventoService();
            var evento = eventosService.GetEventoPorId(idEvento);

            if (evento == null) {
                return NotFound();
            }
            else {
                return Ok(evento);
            }
     
        }

        [HttpPost("NuevoEvento")]
        public IActionResult PostNuevoEvento([FromBody] Evento evento){
            EventoService eventoService = new EventoService();
            
            bool resultado = eventoService.PostNuevoEvento(evento);

            if (resultado)
            {
                return Ok();
            }
            else {
                return NotFound();
            }
        }

        [HttpPut("{idEvento:int}/Modificar")]
        public IActionResult PutNuevoEvento(int idEvento, [FromBody] Evento evento) {
            EventoService eventoService = new EventoService();
            bool resultado = eventoService.PutNuevoEvento(idEvento, evento);

            if (resultado)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{idEvento:int}/Borrar")]
        public IActionResult DeleteEvento(int idEvento) { 
        
            EventoService eventoService = new EventoService();
            bool resultado = eventoService.DeleteEvento(idEvento);

            if (resultado)
            {
                return Ok();
            }
            else { 
                return UnprocessableEntity();
            }
        }
    }
}
