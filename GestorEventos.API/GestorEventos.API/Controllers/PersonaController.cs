using GestorEventos.Servicios.Entidades;
using GestorEventos.Servicios.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace GestorEventos.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonaController : Controller
    {
        [HttpGet]
        public IActionResult Get() {
            PersonaService personaService = new PersonaService();
            return Ok(personaService.GetPersonasDePrueba());
        }

        [HttpGet("{idPersona:int}")]
        public IActionResult GetPersonaPorId(int idPersona) { 
            PersonaService personaService = new PersonaService();
            Persona? persona = personaService.GetPersonaDePruebaSegunId(idPersona);

            if (persona == null) { 
                return NotFound();
            }
            else {
                return Ok(persona);
            }
        }

        [HttpPost]
        public IActionResult PostPersona([FromBody] Persona persona) {
            PersonaService personaService = new PersonaService();

            // personaService.AddNuevaPersona(persona);

            return Ok();
        }
    }
}
