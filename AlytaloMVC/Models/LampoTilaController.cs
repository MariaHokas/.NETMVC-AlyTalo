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
                         orderby p.HuoneID descending
                         select new
                         {
                             p.HuoneID,
                             p.HuoneenNimi,
                             p.Lampotila

                         }).Take(1);

            string json = JsonConvert.SerializeObject(model);
            entities.Dispose();
            //välimuistin hallinta
            Response.Expires = -1;
            Response.CacheControl = "no-cache";

            return Json(json, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Lampo(HuoneLampotila pro)
        {
            AlyTaloEntities entities = new AlyTaloEntities();

            HuoneLampotila dbItem = (from p in entities.HuoneLampotila
                                     where p.Lampotila != null
                                     orderby p.HuoneID descending
                                     select p).First();

            if ( pro.Lampotila > 17 && pro.Lampotila < 25)

            {

                dbItem.Lampotila = pro.Lampotila;
                entities.HuoneLampotila.Add(dbItem);
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