using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RusoCars.Models;
using System.IO;
using System.Web.Helpers;

namespace RusoCars.Controllers
{
    [Authorize]
    public class ImageController : Controller
    {
        private CarsRusoEntities db = new CarsRusoEntities();

        //
        // GET: /Image/

        public ActionResult Index()
        {
            var image = db.Image_B(null);
            return View(image.ToList());

        }


        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            Random auxRandom = new Random();
            if (file != null && file.ContentLength > 0)
            {
                string random = auxRandom.Next(0, 99999999).ToString();
                file = Helpers.FileHelpers.UploadFile(file,
                                                      Server.MapPath("~/Images"),
                                                      random);
                if (file != null)
                {
                    int imageId = SaveImage(file, random);

                }
            }
            return RedirectToAction("Index");
        }

        // GET: /Image/Edit/5

        public ActionResult Edit(int id = 0)
        {
            var auxImage = db.Image_B(id);
            List<Image> image = auxImage.ToList();
            if (image == null)
            {
                return HttpNotFound();
            }
            return View(image.First());
        }

        //
        // POST: /Image/Edit/5

        [HttpPost]
        public ActionResult Edit(Image image)
        {
            if (ModelState.IsValid)
            {
                db.Entry(image).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(image);
        }

        //
        // GET: /Image/Delete/5

        public ActionResult Delete(int id = 0)
        {
            var auxImage = db.Image_B(id);
            List<Image> image = auxImage.ToList();
            if (image == null)
            {
                return HttpNotFound();
            }
            return View(image.First());
        }

        //
        // POST: /Image/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var auxImage = db.Image_B(id);
            List<Image> image = auxImage.ToList();
            db.Images.Remove(image.First());
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        #region functions
        private int SaveImage(HttpPostedFileBase file, string randomString)
        {
            //Save image in DB
            Image image = new Image();
            image.ImageTitle = file.FileName;
            image.ImagePath = "../../Images/" + randomString + file.FileName;
            db.Images.Add(image);
            db.SaveChanges();
            int imageId = db.Images.Max(i => i.ImageId);
            return imageId;
        }

        #endregion
    }
}