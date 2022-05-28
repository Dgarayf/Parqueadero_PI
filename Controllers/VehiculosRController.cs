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
    public class VehiculosRController : Controller
    {
        private ParqueaderoEntities5 db = new ParqueaderoEntities5();



        public ActionResult Sacar(String confirmar)
        {
            var consultica = db.VehiculosR.ToList();

            if (confirmar != "")
            {
                foreach (var item in consultica)
                {
                    if (item.Placa == confirmar)
                    {
                        DeleteConfirmed(item.Id_Vehiculo);
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
            var consultica = db.VehiculosR.ToList();
            var consultica2 = db.Usuarios.ToList();

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
                        ViewBag.tipo = item.TipoVehiculo;
                        ViewBag.placa = item.Placa;
                        ViewBag.id = item.Id_Usuario;
                    }
                }

                //Valida la id del usuario
                foreach (var item in consultica2)
                {
                    if (item.Id_Usuario == ViewBag.id)
                    {
                        ViewBag.nombre = item.Nombres;
                        ViewBag.apellido = item.Apellidos;
                        ViewBag.tDocumento = item.TipoDocumento;
                        ViewBag.nDocumento = item.NumeroDocumeto;
                        ViewBag.tUsuario = item.TipoUsuario;
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

        // GET: VehiculosR
        public ActionResult Index()
        {
            var vehiculosR = db.VehiculosR.Include(v => v.Usuarios);
            return View(vehiculosR.ToList());
        }

        // GET: VehiculosR/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehiculosR vehiculosR = db.VehiculosR.Find(id);
            if (vehiculosR == null)
            {
                return HttpNotFound();
            }
            return View(vehiculosR);
        }

        // GET: VehiculosR/Create
        public ActionResult Create()
        {
            ViewBag.Id_Usuario = new SelectList(db.Usuarios, "Id_Usuario", "Nombres");
            return View();
        }

        // POST: VehiculosR/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Vehiculo,Fecha,TipoVehiculo,Color,Placa,Marca,Id_Usuario")] VehiculosR vehiculosR)
        {
            if (ModelState.IsValid)
            {
                db.VehiculosR.Add(vehiculosR);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Usuario = new SelectList(db.Usuarios, "Id_Usuario", "Nombres", vehiculosR.Id_Usuario);
            return View(vehiculosR);
        }

        // GET: VehiculosR/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehiculosR vehiculosR = db.VehiculosR.Find(id);
            if (vehiculosR == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Usuario = new SelectList(db.Usuarios, "Id_Usuario", "Nombres", vehiculosR.Id_Usuario);
            return View(vehiculosR);
        }

        // POST: VehiculosR/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Vehiculo,Fecha,TipoVehiculo,Color,Placa,Marca,Id_Usuario")] VehiculosR vehiculosR)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vehiculosR).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Usuario = new SelectList(db.Usuarios, "Id_Usuario", "Nombres", vehiculosR.Id_Usuario);
            return View(vehiculosR);
        }

        // GET: VehiculosR/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehiculosR vehiculosR = db.VehiculosR.Find(id);
            if (vehiculosR == null)
            {
                return HttpNotFound();
            }
            return View(vehiculosR);
        }

        // POST: VehiculosR/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VehiculosR vehiculosR = db.VehiculosR.Find(id);
            db.VehiculosR.Remove(vehiculosR);
            db.SaveChanges();
            return RedirectToAction("Index");
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
