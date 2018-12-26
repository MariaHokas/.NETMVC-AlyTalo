using AlytaloMVC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlytaloMVC.Controllers
{
    public class HallintaController : Controller
    {
        public bool OK { get; private set; }

        // GET: Hallinta
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetSauna()
        {
            AlyTaloEntities entities = new AlyTaloEntities();
            var model = (from v in entities.Sauna
                         select new
                         {
                             v.SaunaID,
                             v.Sauna_Nimi

                         }).ToList();

            string json = JsonConvert.SerializeObject(model);
            entities.Dispose();
            Response.Expires = - 1;

            return Json(json, JsonRequestBehavior.AllowGet);
         
        }

        public ActionResult NewSauna(Sauna pro)
        {
            AlyTaloEntities entities = new AlyTaloEntities();
            int id = pro.SaunaID;

            bool OK = false;

            if (pro.SaunaID == 0)

            {

                // kyseessä on uuden asiakkaan lisääminen, kopioidaan kentät
                Sauna dbItem = new Sauna()
                {
                    SaunaID = pro.SaunaID,
                    Sauna_Nimi = pro.Sauna_Nimi,

                };

                // tallennus tietokantaan
                entities.Sauna.Add(dbItem);
                entities.SaveChanges();
                OK = true;
                
            }
            ModelState.Clear();
            entities.Dispose();
            return Json(OK, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Delete(Sauna pro)
        {
            AlyTaloEntities entities = new AlyTaloEntities();
            //haetaan id:n perusteella rivi SQL tietokannasta

            Sauna dbItem = (from p in entities.Sauna
                                    where p.SaunaID == pro.SaunaID
                                    select p).FirstOrDefault();
            {
                // tietokannasta poisto
                entities.Sauna.Remove(dbItem);
                entities.SaveChanges();
                OK = true;
            }
            entities.Dispose();

            return Json(OK, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetValot()
        {
            AlyTaloEntities entities = new AlyTaloEntities();
            var model = (from v in entities.Valot
                         select new
                         {
                             v.ValoID,
                             v.Huone

                         }).ToList();

            string json = JsonConvert.SerializeObject(model);
            entities.Dispose();
            Response.Expires = -1;

            return Json(json, JsonRequestBehavior.AllowGet);

        }

        public ActionResult NewValot(Valot pro)
        {
            AlyTaloEntities entities = new AlyTaloEntities();
            int id = pro.ValoID;

            bool OK = false;

            if (pro.ValoID == 0)

            {

                // kyseessä on uuden asiakkaan lisääminen, kopioidaan kentät
                Valot dbItem = new Valot()
                {
                    ValoID = pro.ValoID,
                    Huone = pro.Huone,

                };

                // tallennus tietokantaan
                entities.Valot.Add(dbItem);
                entities.SaveChanges();
                OK = true;

            }
            ModelState.Clear();
            entities.Dispose();
            return Json(OK, JsonRequestBehavior.AllowGet);

        }

        public ActionResult DeleteValot(Valot pro)
        {
            AlyTaloEntities entities = new AlyTaloEntities();
            //haetaan id:n perusteella rivi SQL tietokannasta

            Valot dbItem = (from p in entities.Valot
                            where p.ValoID == pro.ValoID
                            select p).FirstOrDefault();

            {
                // tietokannasta poisto
                entities.Valot.Remove(dbItem);
                entities.SaveChanges();
                OK = true;
            }
            entities.Dispose();

            return Json(OK, JsonRequestBehavior.AllowGet);
        }
    }
}