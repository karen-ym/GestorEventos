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
            return Ok(personaService.GetPersonas());
        }

        [HttpGet("{idPersona:int}")]
        public IActionResult GetPersonaPorId(int idPersona) { 
            PersonaService personaService = new PersonaService();
            Persona? persona = personaService.GetPersonaDePruebaSegunId(idPersona);

            var persona = this.personaService.GetPersonaSegunId(idPersona);

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

        [HttpPatch("borradoLogico/{idPersona:int}")]
        public ActionResult BorradoLogicoPersona(int idPersona)
        {
            personaService.BorrarLogicamentePersona(idPersona);
            return Ok();
        }

        [HttpDelete("borradoFisico/{idPersona:int}")]
        public IActionResult BorradoFisico(int idPersona)
        {
            personaService.BorrarFisicamentePersona(idPersona);
            return Ok();
        }
    }
}
