using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RusoCars.Models;
using RusoCars.DAL;

namespace RusoCars.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();


        //
        // GET: /Category/

        public ActionResult Index()
        {

            return View(unitOfWork.CategoryRepository.GetAll());
        }

        //
        // GET: /Category/Details/5

        public ActionResult Details(int id = 0)
        {

            Category category = unitOfWork.CategoryRepository.GetByID(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        //
        // GET: /Category/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Category/Create

        [HttpPost]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.CategoryRepository.Insert(category);
                unitOfWork.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }

        //
        // GET: /Category/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Category category = unitOfWork.CategoryRepository.GetByID(id);

            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        //
        // POST: /Category/Edit/5

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.CategoryRepository.Update(category);
                unitOfWork.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        //
        // GET: /Category/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Category category = unitOfWork.CategoryRepository.GetByID(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        //
        // POST: /Category/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            unitOfWork.CategoryRepository.Delete(id);
            List<Subcategory> subcategories = unitOfWork.SubcategoryRepository.GetAllInCategory(id);
            foreach (Subcategory subcategory in subcategories)
            {
                List<Image> ImageList = unitOfWork.SubcategoryRepository.GetImages(id);
                foreach (Image image in ImageList)
                    Helpers.FileHelpers.RemoveFile(image.ImagePath);
            }

            unitOfWork.CategoryRepository.Delete(id);
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