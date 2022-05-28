using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Parqueadero.Models;

namespace Parqueadero.Controllers
{
    public class VehiculosController : Controller
    {
        private ParqueaderoEntities4 db = new ParqueaderoEntities4();

        public ActionResult Sacar(String confirmar)
        {
            var consultica = db.Vehiculos.ToList();

            if (confirmar != "")
            {
                foreach (var item in consultica)
                {
                    if (item.Placa == confirmar)
                    {
                        DeleteConfirmed(item.Id);
                    }
                }

                ViewBag.mensaje = "El vehiculo a salido del sistema exitosamente";
                return View("~/Views/Home/Index.cshtml");
            }
            else
            {
                ViewBag.mensaje = "Error inesperado";
                return View("Prueba");
            }
        }

        //Realiza la consulta de las placas
        public ActionResult Consulta(String placa)
        {
            var consultica = db.Vehiculos.ToList();
            var consultica2 = db.Estudiantes.ToList();

            ViewBag.id = 0;

            if (placa != "")
            {
                //Valida la placa del vehiculo
                foreach (var item in consultica)
                {
                    if (item.Placa == placa)
                    {
                        ViewBag.color = item.Color;
                        ViewBag.fecha = item.Fecha;
                        ViewBag.marca = item.Marca;
                        ViewBag.tipo = item.TypVehiculo;
                        ViewBag.placa = item.Placa;
                        ViewBag.id = item.IdEstudiante;
                    }
                }

                //Valida la id del usuario
                foreach (var item in consultica2)
                {
                    if (item.Id == ViewBag.id)
                    {
                        ViewBag.nombre = item.Nombre;
                        ViewBag.apellido = item.Apellido;
                        ViewBag.tDocumento = item.TypDocument;
                        ViewBag.nDocumento = item.NumeroDocumento;
                        ViewBag.tUsuario = item.TypUsuario;  
                    }
                }

                //Valida la existencia del vehiculo
                if (ViewBag.id != 0)
                {
                    return View("Prueba");
                }
                else
                {
                    ViewBag.mensaje = "La placa no se encuentra en la base de datos";
                    return View("~/Views/Home/Index.cshtml");
                }
            }
            else
            {
                ViewBag.mensaje = "Debe ingresar una placa";
                return View("~/Views/Home/Index.cshtml");
            }
        }

        // GET: Vehiculos
        public ActionResult Index()
        {
            return View(db.Vehiculos.ToList());
        }

        // GET: Vehiculos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehiculos vehiculos = db.Vehiculos.Find(id);
            if (vehiculos == null)
            {
                return HttpNotFound();
            }
            return View(vehiculos);
        }

        // GET: Vehiculos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Vehiculos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Fecha,TypVehiculo,Color,Marca,Placa, IdEstudiante")] Vehiculos vehiculos)
        {
            if (ModelState.IsValid)
            {
                db.Vehiculos.Add(vehiculos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vehiculos);
        }

        // GET: Vehiculos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehiculos vehiculos = db.Vehiculos.Find(id);
            if (vehiculos == null)
            {
                return HttpNotFound();
            }
            return View(vehiculos);
        }

        // POST: Vehiculos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Fecha,TypVehiculo,Color,Marca,Placa")] Vehiculos vehiculos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vehiculos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vehiculos);
        }

        // GET: Vehiculos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehiculos vehiculos = db.Vehiculos.Find(id);
            if (vehiculos == null)
            {
                return HttpNotFound();
            }
            return View(vehiculos);
        }

        // POST: Vehiculos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vehiculos vehiculos = db.Vehiculos.Find(id);
            db.Vehiculos.Remove(vehiculos);
            db.SaveChanges();
            ViewBag.mensaje = "El vehiculo a salido del sistema exitosamente";
            return RedirectToAction("~/Views/Home/Index.cshtml");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
