using StorageSample.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace StorageSample.Controllers
{
    public class DocumentController : Controller
    {
        DocumentService docService = new DocumentService();

        [HttpPost]
        public async Task<ActionResult> Upload()
        {
            bool isSavedSuccessfully = true;
            string url = "";
            try
            {
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];

                    var imageUrl = await docService.UploadImageAsync(file);
                    url = imageUrl;
                }
            }
            catch (Exception ex)
            {
                isSavedSuccessfully = false;
            }


            if (isSavedSuccessfully)
            {
                return Json(new { Url = url });
            }
            else
            {
                return Json(new { Message = "Error in saving file" });
            }
        }

        public JsonResult RelationShip(Guid id, string url)
        {
            //using (var db = new object())
            //{
            //    // ...
            //    db.SaveChanges();
            //}

            return Json(new { data = true });
        }
    }
}