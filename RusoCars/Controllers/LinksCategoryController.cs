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
    public class LinksCategoryController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        //
        // GET: /LinksCategory/

        public ActionResult Index()
        {
            return View(unitOfWork.LinksCategoryRepository.GetAll());
        }

        //
        // GET: /LinksCategory/Details/5

        public ActionResult Details(int id = 0)
        {
            LinksCategory linkscategory = unitOfWork.LinksCategoryRepository.GetByID(id);
            if (linkscategory == null)
            {
                return HttpNotFound();
            }
            return View(linkscategory);
        }

        //
        // GET: /LinksCategory/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /LinksCategory/Create

        [HttpPost]
        public ActionResult Create(LinksCategory linkscategory)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.LinksCategoryRepository.Insert(linkscategory);
                unitOfWork.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(linkscategory);
        }

        //
        // GET: /LinksCategory/Edit/5

        public ActionResult Edit(int id = 0)
        {
            LinksCategory linkscategory = unitOfWork.LinksCategoryRepository.GetByID(id);
            if (linkscategory == null)
            {
                return HttpNotFound();
            }
            return View(linkscategory);
        }

        //
        // POST: /LinksCategory/Edit/5

        [HttpPost]
        public ActionResult Edit(LinksCategory linkscategory)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.LinksCategoryRepository.Update(linkscategory);
                unitOfWork.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(linkscategory);
        }

        //
        // GET: /LinksCategory/Delete/5

        public ActionResult Delete(int id = 0)
        {
            LinksCategory linkscategory = unitOfWork.LinksCategoryRepository.GetByID(id);
            if (linkscategory == null)
            {
                return HttpNotFound();
            }
            return View(linkscategory);
        }

        //
        // POST: /LinksCategory/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            unitOfWork.LinksCategoryRepository.Delete(id);
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