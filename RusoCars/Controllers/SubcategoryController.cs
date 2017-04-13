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
using RusoCars.DAL;


namespace RusoCars.Controllers
{
    [Authorize]
    public class SubcategoryController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        public PartialViewResult AddImageUploader()
        {
            return PartialView("_fileUploaderPlusTextBox");
        }
        //
        // GET: /Subcategory/

        public ActionResult Index()
        {

            return View(unitOfWork.SubcategoryRepository.GetAllWithCategories());
        }

        //
        // GET: /Subcategory/Details/5

        public ActionResult Details(int id = 0)
        {
            Subcategory subcategory = unitOfWork.SubcategoryRepository.GetByID(id, null);
            if (subcategory == null)
            {
                return HttpNotFound();
            }
            var images = unitOfWork.SubcategoryRepository.GetImages(id);
            ViewBag.Images = images;
            return View(subcategory);
        }

        //
        // GET: /Subcategory/Create

        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(unitOfWork.CategoryRepository.GetAll(), "CategoryId", "Name");
            return View();
        }

        //
        // POST: /Subcategory/Create

        [HttpPost]
        public ActionResult Create(Subcategory subcategory)
        {
            bool firstTime = true;
            Random auxRandom = new Random();
            foreach (string fileName in Request.Files)
            {
                string random = auxRandom.Next(0, 99999999).ToString();
                HttpPostedFileBase file = Helpers.FileHelpers.UploadFile(Request.Files[fileName],
                                                                       Server.MapPath("~/Images"),
                                                                       random);
                if (file == null)
                    continue;
                string numbers = fileName.Substring(12);
                string textBox = "TextBox" + numbers;
                Image image = new Image();
                image.ImageTitle =  Request.Form[textBox];
                image.ImagePath = "../../Images/" + random + file.FileName;
                if (firstTime)
                {

                    unitOfWork.SubcategoryRepository.Insert(subcategory.CategoryId, null, null, subcategory.Name, image.ImageTitle, image.ImagePath);
                    firstTime = false;
                }
                else
                {
                    int SubcategoryId = unitOfWork.SubcategoryRepository.getMaxId();
                    unitOfWork.SubcategoryRepository.Insert(subcategory.CategoryId, SubcategoryId, null, subcategory.Name, image.ImageTitle, image.ImagePath);
                }
            }
            if (ModelState.IsValid)
            {
                unitOfWork.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(unitOfWork.CategoryRepository.GetAll(), "CategoryId", "Name", subcategory.CategoryId);
            return View(subcategory);
        }

        //
        // GET: /Subcategory/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Subcategory subcategory = unitOfWork.SubcategoryRepository.GetByID(id);
            if (subcategory == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(unitOfWork.CategoryRepository.GetAll(), "CategoryId", "Name", subcategory.CategoryId);
            var images = unitOfWork.SubcategoryRepository.GetImages(id);
            ViewBag.Images = images;
            return View(subcategory);
        }

        //
        // POST: /Subcategory/Edit/5

        [HttpPost]
        public ActionResult Edit(Subcategory subcategory)
        {
            if (ModelState.IsValid)
            {
                List<Image> ImageList = unitOfWork.SubcategoryRepository.GetImages(subcategory.SubcategoryId);

                foreach (Image image in ImageList)
                {

                    string checkboxValue = Request.Form.GetValues("Checkbox" + image.ImageId)[0];
                    if (checkboxValue.Equals("true"))
                    {
                        Helpers.FileHelpers.RemoveFile(image.ImageId);
                        unitOfWork.SubcategoryRepository.DeleteWithImages(null, image.ImageId);
                    }
                }

                Random auxRandom = new Random();
                foreach (string fileName in Request.Files)
                {
                    string random = auxRandom.Next(0, 99999999).ToString();
                    HttpPostedFileBase file = Helpers.FileHelpers.UploadFile(Request.Files[fileName],
                                                                           Server.MapPath("~/Images"),
                                                                           random);
                    if (file == null)
                        continue;
                    string numbers = fileName.Substring(12);
                    string textBox = "TextBox" + numbers;
                    Image image = new Image();
                    image.ImageTitle = Request.Form[textBox];
                    image.ImagePath = "../../Images/" + random + file.FileName;
                    unitOfWork.SubcategoryRepository.Insert(subcategory.CategoryId, subcategory.SubcategoryId, null, subcategory.Name, image.ImageTitle, image.ImagePath);
                }


                unitOfWork.SubcategoryRepository.Update(subcategory);
                unitOfWork.SaveChanges();


                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(unitOfWork.CategoryRepository.GetAll(), "CategoryId", "Name", subcategory.CategoryId);
            return View(subcategory);
        }

        //
        // GET: /Subcategory/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Subcategory subcategory = unitOfWork.SubcategoryRepository.GetByID(id);
            if (subcategory == null)
            {
                return HttpNotFound();
            }
            return View(subcategory);
        }

        //
        // POST: /Subcategory/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            List<Image> ImageList = unitOfWork.SubcategoryRepository.GetImages(id);
            foreach (Image image in ImageList)
                Helpers.FileHelpers.RemoveFile(image.ImagePath);

            Subcategory subcategory = unitOfWork.SubcategoryRepository.GetByID(id);
            unitOfWork.SubcategoryRepository.Delete(subcategory);
            unitOfWork.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }

    }
}