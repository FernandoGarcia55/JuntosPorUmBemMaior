using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JPBM.Models;
using JPBM.Repository;
using JPBM.Entidades;

namespace JPBM.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(RifaViewModel rifa)
        {
            var res = 0;
            try
            {

                RifaRepository c = new RifaRepository();
                UsuarioRepository u = new UsuarioRepository();
                var listaNumeros = c.GetAll();

                if (rifa.Nome != null || rifa.Numeros != null)
                {
                    var numeros = rifa.Numeros.Split(",");
                    
                    var x = new List<int>();
                    foreach (var n in numeros)
                    {
                        foreach (var ln in listaNumeros)
                        {
                            if (ln.Numero == Convert.ToInt32(n))
                            {
                                if (ln.Vendido == true)
                                {
                                    res = 1;
                                    x.Add(ln.Numero);
                                }
                                else
                                {
                                    Rifa r = new Rifa();
                                    r.NomeId = rifa.NomeId;
                                    r.Pago = rifa.Pago;
                                    r.Vendido = true;
                                    r.Numero = ln.Numero;

                                    c.Update(r);
                                }
                            }
                        }
                    }

                    var result = String.Join(", ", x.ToArray());

                    ViewBag.r = result;
                    
                   
                }
                var listas = c.ListaOrdenada();
                var aux = 1;
                foreach (var lista in listas)
                {
                    ViewData["Lista" + aux] = lista;
                    aux++;
                }
                ViewBag.listaUsuarios = u.GetAll();
            }
            catch(Exception e)
            {
                
                if(e.Message == "Input string was not in a correct format.")
                {
                    res = 2;
                }

                RifaRepository c = new RifaRepository();
                UsuarioRepository u = new UsuarioRepository();
                var listas = c.ListaOrdenada();
                var aux = 1;
                foreach (var lista in listas)
                {
                    ViewData["Lista" + aux] = lista;
                    aux++;
                }
                ViewBag.listaUsuarios = u.GetAll();
            }

            //for (var x = 1; x <= 150; x++)
            //{
            //    Rifa r = new Rifa();
            //    r.Nome = "";
            //    r.Numero = x;
            //    r.Pago = false;
            //    r.Vendido = false;
            //    c.Add(r);
            //}
            ViewBag.res = res;

            return View();
        }

        public IActionResult Usuarios()
        {
            ViewBag.r = TempData["resp"];
            return View();
        }
        public IActionResult CadastroUsuarios(UsuariosViewModel usuario)
        {
             UsuarioRepository r = new UsuarioRepository();
            var usuarios = r.GetAll();
            var res = 0;
            var aux = false;
            foreach(var u in usuarios)
            {
                if (u.Email.Equals(usuario.Email))
                {
                    aux = true;
                    res = 1;
                    TempData["resp"] = res;
                    return RedirectToAction("Usuarios");
                }
        
            }
            if (aux == false)
                r.Add(new Usuarios(usuario.Nome, usuario.Telefone, usuario.Email));


            return RedirectToAction("Index");
        }
        public IActionResult EditarRifa(RifaViewModel rifa)
        {
            var res = 0;
            try
            {

                RifaRepository c = new RifaRepository();
                UsuarioRepository u = new UsuarioRepository();
                var listaNumeros = c.GetAll();

                if (rifa.Nome != null || rifa.Numeros != null)
                {
                    var numeros = rifa.Numeros.Split(",");

                    var x = new List<int>();
                    foreach (var n in numeros)
                    {
                        foreach (var ln in listaNumeros)
                        {
                            if (ln.Numero == Convert.ToInt32(n))
                            {
                                if (ln.Vendido == true)
                                {
                                    res = 1;
                                    x.Add(ln.Numero);
                                }
                                else
                                {
                                    Rifa r = new Rifa();
                                    r.NomeId = rifa.NomeId;
                                    r.Pago = rifa.Pago;
                                    r.Vendido = true;
                                    r.Numero = ln.Numero;

                                    c.Update(r);
                                }
                            }
                        }
                    }

                    var result = String.Join(", ", x.ToArray());

                    ViewBag.r = result;


                }
                var listas = c.ListaOrdenada();
                var aux = 1;
                foreach (var lista in listas)
                {
                    ViewData["Lista" + aux] = lista;
                    aux++;
                }
                ViewBag.listaUsuarios = u.GetAll();
            }
            catch (Exception e)
            {

                if (e.Message == "Input string was not in a correct format.")
                {
                    res = 2;
                }

                RifaRepository c = new RifaRepository();
                UsuarioRepository u = new UsuarioRepository();
                var listas = c.ListaOrdenada();
                var aux = 1;
                foreach (var lista in listas)
                {
                    ViewData["Lista" + aux] = lista;
                    aux++;
                }
                ViewBag.listaUsuarios = u.GetAll();
            }

            //for (var x = 1; x <= 150; x++)
            //{
            //    Rifa r = new Rifa();
            //    r.Nome = "";
            //    r.Numero = x;
            //    r.Pago = false;
            //    r.Vendido = false;
            //    c.Add(r);
            //}
            ViewBag.res = res;

            return View();
        }



        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
