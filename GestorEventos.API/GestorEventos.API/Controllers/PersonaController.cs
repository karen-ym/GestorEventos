using GestorEventos.Servicios.Entidades;
using GestorEventos.Servicios.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace GestorEventos.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonaController : ControllerBase
    {
        private readonly IPersonaService personaService;

        public PersonaController(IPersonaService personaService)
        {
            this.personaService = personaService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var personas = personaService.GetPersonas();
            return Ok(personas);
        }

        [HttpGet("{idPersona:int}")]
        public IActionResult GetPersonaPorId(int idPersona)
        {
            var persona = personaService.GetPersonaSegunId(idPersona);
            if (persona == null)
            {
                return NotFound();
            }
            return Ok(persona);
        }

        [HttpPost]
        public IActionResult PostPersona([FromBody] Persona persona)
        {
            personaService.AgregarNueva(persona);
            return Ok();
        }

        [HttpPut("{idPersona:int}")]
        public IActionResult PutPersona(int idPersona, [FromBody] Persona persona)
        {
            personaService.Modificar(idPersona, persona);
            return Ok();
        }

        [HttpPatch("borradoLogico/{idPersona:int}")]
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
