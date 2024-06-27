using GestorEventos.Servicios.Entidades;
using GestorEventos.Servicios.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace GestorEventos.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonaController : Controller
    {
        private IPersonaService personaService;

        public PersonaController(IPersonaService _personaService) {
            personaService = _personaService;
        }

        [HttpGet]
        public IActionResult Get() {
            return Ok(personaService.GetPersonas());
            // return Ok(personaService.GetPersonasDePrueba());
        }

        [HttpGet("{idPersona:int}")]
        public IActionResult GetPersonaPorId(int idPersona) {
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
            personaService.AgregarNueva(persona);
            return Ok();
        }

        [HttpPut("{idPersona:int}")]
        public IActionResult PutPersona(int idPersona, [FromBody] Persona persona) {
            personaService.Modificar(idPersona, persona);
            return Ok();
        }

        [HttpPatch("borradologico/{idPersona:int}")]
        public IActionResult BorradoLogicoPersona(int idPersona)
        {

            personaService.BorrarL(idPersona);

            return Ok();
        }

        [HttpDelete("{idPersona:int}")]
        public IActionResult BorradoFisico(int idPersona)
        {
            personaService.BorrarF(idPersona);

            return Ok();
        }
    }
        
}

