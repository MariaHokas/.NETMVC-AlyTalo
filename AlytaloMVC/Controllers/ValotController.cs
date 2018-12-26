using AlytaloMVC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlytaloMVC.Controllers
{
    public class ValotController : Controller
    {
        public bool OK { get; private set; }

        // GET: Valot
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetList()
        {
            AlyTaloEntities entities = new AlyTaloEntities();
            var model = (from v in entities.Valot
                             //orderby p.HuoneID descending
                         select new
                         {
                             v.ValoID,
                             v.Huone,
                             v.ValotPois,
                             v.Valot33,
                             v.Valot66,
                             v.Valot100,
                             v.Tila
                         }).ToList();

            string json = JsonConvert.SerializeObject(model);
            entities.Dispose();
            //välimuistin hallinta
            Response.Expires = -1;
            Response.CacheControl = "no-cache";

            return Json(json, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ValotOff(string id)
        {
            AlyTaloEntities entities = new AlyTaloEntities();

            bool OK = false;
            Valot dbItem = (from v in entities.Valot
                            where v.ValoID.ToString() == id
                            select v).FirstOrDefault();

            if (dbItem != null)
            {

                dbItem.Tila = "Valot Pois";

                entities.SaveChanges();
                OK = true;
            }

            //entiteettiolion vapauttaminen
            entities.Dispose();

            // palautetaan 'json' muodossa
            return Json(OK, JsonRequestBehavior.AllowGet);

        }
        public ActionResult Valot33(string id)
        {
            AlyTaloEntities entities = new AlyTaloEntities();

            bool OK = false;
            Valot dbItem = (from v in entities.Valot
                            where v.ValoID.ToString() == id
                            select v).FirstOrDefault();

            if (dbItem != null)
            {

                dbItem.Tila = "Valot33";

                entities.SaveChanges();
                OK = true;
            }

            //entiteettiolion vapauttaminen
            entities.Dispose();

            // palautetaan 'json' muodossa
            return Json(OK, JsonRequestBehavior.AllowGet);

        }
        public ActionResult Valot66(string id)
        {
            AlyTaloEntities entities = new AlyTaloEntities();

            bool OK = false;
            Valot dbItem = (from v in entities.Valot
                            where v.ValoID.ToString() == id
                            select v).FirstOrDefault();

            if (dbItem != null)
            {

                dbItem.Tila = "Valot66";

                entities.SaveChanges();
                OK = true;
            }

            //entiteettiolion vapauttaminen
            entities.Dispose();

            // palautetaan 'json' muodossa
            return Json(OK, JsonRequestBehavior.AllowGet);

        }
        public ActionResult Valot100(string id)
        {
            AlyTaloEntities entities = new AlyTaloEntities();

            bool OK = false;
            Valot dbItem = (from v in entities.Valot
                            where v.ValoID.ToString() == id
                            select v).FirstOrDefault();

            if (dbItem != null)
            {

                dbItem.Tila = "Valot100";

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