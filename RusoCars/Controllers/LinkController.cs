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
    public class LinkController : Controller
    {

        private UnitOfWork unitOfWork = new UnitOfWork();
        //
        // GET: /Link/

        public ActionResult Index()
        {
            var links = unitOfWork.LinkRepository.dbSet.Include(l => l.Image).Include(l => l.LinksCategory);
            return View(links.ToList());
        }

        //
        // GET: /Link/Details/5

        public ActionResult Details(int id = 0)
        {
            Link link = unitOfWork.LinkRepository.GetByID(id);
            if (link == null)
            {
                return HttpNotFound();
            }
            return View(link);
        }

        //
        // GET: /Link/Create

        public ActionResult Create()
        {
            ViewBag.LinkCategoryId = new SelectList(unitOfWork.LinksCategoryRepository.GetAll(), "LinksCategoryId", "Name");
            return View();
        }

        //
        // POST: /Link/Create

        [HttpPost]
        public ActionResult Create(Link link)
        {
            Random auxRandom = new Random();
            foreach (string fileName in Request.Files)
            {
                string random = auxRandom.Next(0, 99999999).ToString();
                HttpPostedFileBase file = Helpers.FileHelpers.UploadFile(Request.Files[fileName],
                                                                       Server.MapPath("~/Images"),
                                                                       random);
                if (file == null)
                    continue;
                int imageId = Helpers.FileHelpers.SaveImage(file, random);
                link.ImageId = imageId;
            }
            if (ModelState.IsValid)
            {
                unitOfWork.LinkRepository.Insert(link);
                unitOfWork.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LinkCategoryId = new SelectList(unitOfWork.LinksCategoryRepository.GetAll(), "LinksCategoryId", "Name", link.LinkCategoryId);
            return View(link);
        }

        //
        // GET: /Link/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Link link = unitOfWork.LinkRepository.GetByID(id);
            if (link == null)
            {
                return HttpNotFound();
            }
            ViewBag.LinkCategoryId = new SelectList(unitOfWork.LinksCategoryRepository.GetAll(), "LinksCategoryId", "Name", link.LinkCategoryId);
            return View(link);
        }

        //
        // POST: /Link/Edit/5

        [HttpPost]
        public ActionResult Edit(Link link)
        {
            Random auxRandom = new Random();

            foreach (string fileName in Request.Files)
            {

                string random = auxRandom.Next(0, 99999999).ToString();
                HttpPostedFileBase file = Helpers.FileHelpers.UploadFile(Request.Files[fileName],
                                                                         Server.MapPath("~/Images"),
                                                                         random);
                if (file == null)
                    continue;
                int imageId = Helpers.FileHelpers.SaveImage(file, random);
                Helpers.FileHelpers.RemoveFile(link.ImageId);
                link.ImageId = imageId;

                
            }
            if (ModelState.IsValid)
            {

                unitOfWork.LinkRepository.Update(link);
                unitOfWork.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LinkCategoryId = new SelectList(unitOfWork.LinksCategoryRepository.GetAll(), "LinksCategoryId", "Name", link.LinkCategoryId);
            return View(link);

        }
        //
        // GET: /Link/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Link link = unitOfWork.LinkRepository.GetByID(id);
            if (link == null)
            {
                return HttpNotFound();
            }
            return View(link);
        }

        //
        // POST: /Link/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Link link = unitOfWork.LinkRepository.GetByID(id);
            Helpers.FileHelpers.RemoveFile(link.ImageId);

            unitOfWork.ImageRepository.Delete((int)link.ImageId);
            unitOfWork.LinkRepository.Delete(link);
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