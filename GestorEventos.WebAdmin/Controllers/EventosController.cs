using GestorEventos.Servicios.Servicios;
using GestorEventos.Servicios.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace GestorEventos.WebAdmin.Controllers
{
    public class EventosController : Controller
    {
        // comentario: utiliza dos servicios, IEventoService y IPersonaService, inyectados a través del constructor.
        private IEventoService eventoService;
        private IPersonaService personaService;

        public EventosController(IEventoService _eventoService, IPersonaService _personaService)
        {
            this.eventoService = _eventoService;
            this.personaService = _personaService;
        }

        // GET: EventosController
        public ActionResult Index()
        {
            //comentario: devuelve una vista con una lista de todos los eventos, obtenida a través del servicio eventoService.
            var eventos = this.eventoService.GetAllEventosViewModel();

            return View(eventos);
        }

        // GET: EventosController/Details/5
        public ActionResult Details(int id)
        {
            //comentario: muestra los detalles de un evento específico.
            return View();
        }

        // GET: EventosController/Create
        public ActionResult Create()
        {
            //comentario: devuelve la vista para crear un nuevo evento.
            return View();
        }

        // POST: EventosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            //comentario: Recibe los datos del formulario a través de IFormCollection, crea una nueva persona y un nuevo evento, y los guarda utilizando los servicios correspondientes. Si tiene éxito,
            //redirige a la acción Index. En caso de error, devuelve la misma vista.

            try
            {
       
                Persona personaAgasajada = new Persona();
                personaAgasajada.Nombre = collection["Nombre"].ToString();
                personaAgasajada.Apellido = collection["Apellido"].ToString();
                personaAgasajada.Email = collection["Email"].ToString();
                personaAgasajada.Telefono = collection["Telefono"].ToString();
                personaAgasajada.Borrado = false;
                personaAgasajada.Direccion = collection["Direccion"].ToString();

                int IdPersonaAgasajada = personaService.AgregarNueva(personaAgasajada);




                Evento eventoNuevo = new Evento();
                eventoNuevo.IdPersonaAgasajada = IdPersonaAgasajada;

                eventoNuevo.CantidadPersonas = int.Parse(collection["CantidadPersonas"].ToString());
                eventoNuevo.Visible = true;
                eventoNuevo.IdUsuario = int.Parse(HttpContext.User.Claims.First(x => x.Type == "userGestor").Value); // HttpContext.User.Identity.Id;
                eventoNuevo.FechaEvento = DateTime.Parse(collection["FechaEvento"].ToString());
                eventoNuevo.IdTipoEvento = int.Parse(collection["IdTipoEvento"].ToString());
                eventoNuevo.NombreEvento = collection["NombreEvento"].ToString();
                eventoNuevo.IdEstadoEvento = 2; //Pendiente de Aprobacion


                this.eventoService.PostNuevoEvento(eventoNuevo);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EventosController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }
        //La acción GET Edit muestra la vista de edición de un evento específico. La acción POST Edit maneja la actualización de un evento específico

        // POST: EventosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EventosController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EventosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
