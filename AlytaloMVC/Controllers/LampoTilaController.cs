using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlytaloMVC.Models
{
    public class LampoTilaController : Controller
    {
        public bool OK { get; private set; }
        public string ErrorMessage { get; private set; }


        // GET: LampoTila
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetList()
        {
            AlyTaloEntities entities = new AlyTaloEntities();
            var model = (from p in entities.HuoneLampotila
                         select new
                         {
                             p.HuoneID,
                             p.HuoneenNimi,
                             p.Lampotila

                         }).ToList();

            string json = JsonConvert.SerializeObject(model);
            entities.Dispose();
            //välimuistin hallinta
            Response.Expires = -1;
            Response.CacheControl = "no-cache";

            return Json(json, JsonRequestBehavior.AllowGet);
        }


        public ActionResult LampoMiinus(string id)
        {
            AlyTaloEntities entities = new AlyTaloEntities();

            bool OK = false;
            HuoneLampotila dbItem = (from p in entities.HuoneLampotila
                                     where p.HuoneID.ToString() == id
                                     select p).FirstOrDefault();

            if (dbItem != null)
            {
                dbItem.Lampotila = dbItem.Lampotila - 1;

                if ( dbItem.Lampotila > 17 && dbItem.Lampotila < 25)

                    
                        entities.SaveChanges();
                        OK = true;
                    

            }
            //entiteettiolion vapauttaminen
            entities.Dispose();

      
            // palautetaan 'json' muodossa
            return Json(OK, JsonRequestBehavior.AllowGet);



        }
        public ActionResult LampoPlus(string id)
        {
            AlyTaloEntities entities = new AlyTaloEntities();

            bool OK = false;
            HuoneLampotila dbItem = (from p in entities.HuoneLampotila
                                     where p.HuoneID.ToString() == id
                                     orderby p.HuoneID descending
                                     select p).FirstOrDefault();

            if (dbItem != null)
            {
                dbItem.Lampotila = dbItem.Lampotila + 1;

                if (dbItem.Lampotila > 17 && dbItem.Lampotila < 25)

                {
                    entities.SaveChanges();
                    OK = true;
                }

            }
            //entiteettiolion vapauttaminen
            entities.Dispose();


            // palautetaan 'json' muodossa
            return Json(OK, JsonRequestBehavior.AllowGet);



        }
    }
}