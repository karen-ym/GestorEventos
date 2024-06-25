using GestorEventos.Servicios.Entidades;
using GestorEventos.Servicios.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace GestorEventos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {

        /// <summary>
        /// K- Trae todos los eventos disponibles
        /// </summary>
        /// <returns>Lista</returns>

        [HttpGet]
        public IActionResult GetEventos()
        {
            EventoService eventosService = new EventoService();
            return Ok(eventosService.GetAllEventos());
        }

        /// <summary>
        /// Traerá solo un evento según el Id del Evento
        /// </summary>
        /// <param name="idEvento"> Id del Evento</param>
        /// <returns></returns>

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

        [HttpPost("Nuevo")]
        public IActionResult PostNuevoEvento([FromBody] Evento evento){
            EventoService eventoService = new EventoService();
            
            int resultado = eventoService.PostNuevoEvento(evento);

            if (resultado > 0)
            {
                return Ok();
            }
            else {
                return UnprocessableEntity();
            }
        }

        [HttpPost("Nuevo EventoCompleto")]
        public IActionResult PostNuevoEventoModel([FromBody] EventoModel evento)
        {

            EventoService eventoService = new EventoService();
            eventoService.PostNuevoEventoCompleto(evento);
            return Ok();
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
                return UnprocessableEntity();
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
