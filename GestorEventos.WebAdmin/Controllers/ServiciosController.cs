using GestorEventos.Servicios.Entidades;
using GestorEventos.Servicios.Servicios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestorEventos.WebAdmin.Controllers
{
    public class ServiciosController : Controller
    {
        //GET: ServiciosController
        /*public ActionResult Index()
        {
            ServiciosService serviciosService = new ServiciosService();
            //serviciosService.GetServicios();
            return View(serviciosService.GetServicios());
        }*/

        // GET: ServiciosController/Details/5
        /*public ActionResult Details(int id)
        {
            ServiciosService serviciosService = new ServiciosService();
            return View(serviciosService.GetServiciosPorId(id));
        }*/

        // GET: ServiciosController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ServiciosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                ServicioService servicioService = new ServicioService();

                Servicio servicio = new Servicio();

                servicio.Descripcion = collection["Descripcion"].ToString();
                servicio.PrecioServicio = decimal.Parse(collection["PrecioServicio"].ToString());

                //servicioService.AgregarNuevoServicio(servicio);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ServiciosController/Edit/5
        public ActionResult Edit(int id)
        {
            ServicioService serviciosService = new ServicioService();
            return View(serviciosService.GetServiciosPorId(id));
        }

        // POST: ServiciosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                ServicioService servicioService = new ServicioService();

                Servicio servicio = new Servicio();

                servicio.IdServicio = int.Parse(collection["IdServicio"].ToString());
                servicio.Descripcion = collection["Descripcion"].ToString();
                servicio.PrecioServicio = decimal.Parse(collection["PrecioServicio"].ToString());

                servicioService.ModificarServicio(id, servicio);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ServiciosController/Delete/5
        public ActionResult Delete(int id)
        {
            ServicioService servicioService = new ServicioService();
            Servicio servicio = servicioService.GetServiciosPorId(id);
            return View(servicio);
        }

        // POST: ServiciosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                ServicioService servicioService = new ServicioService(); 
                servicioService.BorrarLogicamenteServicio(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
