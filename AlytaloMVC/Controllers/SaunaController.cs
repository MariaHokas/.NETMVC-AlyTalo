using AlytaloMVC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlytaloMVC.Controllers
{
    public class SaunaController : Controller
    {
        // GET: Sauna
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetList()
        {
            AlyTaloEntities entities = new AlyTaloEntities();
            var model = (from v in entities.Sauna
                             //orderby p.HuoneID descending
                         select new
                         {
                             v.SaunaID,
                             v.Sauna_Nimi,
                             v.LampoNyt,
                             v.LampoPaalla,
                             v.SaunaOn,
                             v.SaunaOff,
                             v.Tila
                         }).ToList();

            string json = JsonConvert.SerializeObject(model);
            entities.Dispose();
            //välimuistin hallinta
            Response.Expires = -1;
            Response.CacheControl = "no-cache";

            return Json(json, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaunaOff(string id)
        {
            AlyTaloEntities entities = new AlyTaloEntities();

            bool OK = false;
            Sauna dbItem = (from s in entities.Sauna
                            where s.SaunaID.ToString() == id
                            select s).FirstOrDefault();

            if (dbItem != null)
            {
                dbItem.LampoNyt = 20;
                dbItem.Tila = "OFF";

                entities.SaveChanges();
                OK = true;
            }

            //entiteettiolion vapauttaminen
            entities.Dispose();

            // palautetaan 'json' muodossa
            return Json(OK, JsonRequestBehavior.AllowGet);

        }
        public ActionResult SaunaON(string id)
        {
            AlyTaloEntities entities = new AlyTaloEntities();

            bool OK = false;
            Sauna dbItem = (from s in entities.Sauna
                            where s.SaunaID.ToString() == id
                            select s).FirstOrDefault();

            if (dbItem != null)
            {

                dbItem.Tila = "ON";

                entities.SaveChanges();
                OK = true;
            }

            //entiteettiolion vapauttaminen
            entities.Dispose();

            // palautetaan 'json' muodossa
            return Json(OK, JsonRequestBehavior.AllowGet);

        }

        public ActionResult SaunaMiinus(string id)
        {
            AlyTaloEntities entities = new AlyTaloEntities();

            bool OK = false;
            Sauna dbItem = (from s in entities.Sauna
                            where s.SaunaID.ToString() == id
                            select s).FirstOrDefault();

            if (dbItem != null)
            {
                dbItem.Tila = "ON";
                dbItem.LampoNyt = dbItem.LampoNyt - 5;

                if (dbItem.LampoNyt > 19 && dbItem.LampoNyt < 91)

                    entities.SaveChanges();
                OK = true;
            }

            //entiteettiolion vapauttaminen
            entities.Dispose();

            // palautetaan 'json' muodossa
            return Json(OK, JsonRequestBehavior.AllowGet);

        }
        public ActionResult SaunaPlus(string id)
        {
            AlyTaloEntities entities = new AlyTaloEntities();

            bool OK = false;
            Sauna dbItem = (from s in entities.Sauna
                            where s.SaunaID.ToString() == id
                            select s).FirstOrDefault();

            if (dbItem != null)
            {
                dbItem.Tila = "ON";
                dbItem.LampoNyt = dbItem.LampoNyt + 5;

                if (dbItem.LampoNyt > 19 && dbItem.LampoNyt < 91)

                entities.SaveChanges();
                OK = true;
            }

            //entiteettiolion vapauttaminen
            entities.Dispose();

            // palautetaan 'json' muodossa
            return Json(OK, JsonRequestBehavior.AllowGet);

        }

    }
}