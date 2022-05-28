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
    public class AccesoController : Controller
    {
        private ParqueaderoEntities6 db = new ParqueaderoEntities6();
        // GET: Acceso
        public ActionResult Registro()
        {
            ViewBag.mensaje = "";
            return View();
        }
        public ActionResult Acc(String usuario, String contraseña)
        {
            var consulta = db.login.ToList();
            var cont = 0;

            if(usuario!="" && contraseña != "")
            {
                foreach(var cons in consulta)
                {
                    if(cons.Correo == usuario && cons.Contraseña == contraseña)
                    {
                        cont = 1;
                        Session["Correo"] = cons.Correo;
                    }
                }   
            }

            if(cont == 1)
            {
                return View("~/Views/Home/Index.cshtml");
            }
            else
            {
                ViewBag.mensaje = "No existe Usuario";
                return View("Registro");
            }
        }
    }
}