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
using RusoCars.Helpers;
using RusoCars.DAL;

namespace RusoCars.Controllers
{
    [Authorize]
    public class NewsController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        //
        // GET: /News/
        public ActionResult Index()
        {

            return View(unitOfWork.NewsRepository.GetAll());
        }

        //
        // GET: /News/Details/5

        public ActionResult Details(int id = 0)
        {
            News news = unitOfWork.NewsRepository.GetByID(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }
        //
        // GET: /News/Create
        [ValidateInput(false)]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /News/Create

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(News news)
        {
            Random auxRandom = new Random();
            foreach (string fileName in Request.Files)
            {
                string random = auxRandom.Next(0, 99999999).ToString();
                HttpPostedFileBase file = Helpers.FileHelpers.UploadNewsImage(Request.Files[fileName],
                                                                       Server.MapPath("~/Images"),
                                                                       random);
                if (file == null)
                    continue;
                int imageId = Helpers.FileHelpers.SaveImage(file, random);
                news.ImageId = imageId;
            }
            if (ModelState.IsValid)
            {

                unitOfWork.NewsRepository.Insert(news);
                unitOfWork.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(news);
        }


        //
        // GET: /News/Edit/5
        [ValidateInput(false)]
        public ActionResult Edit(int id = 0)
        {
            News news = unitOfWork.NewsRepository.GetByID(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        //
        // POST: /News/Edit/5

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(News news)
        {
            Random auxRandom = new Random();

            foreach (string fileName in Request.Files)
            {
                string random = auxRandom.Next(0, 99999999).ToString();
                HttpPostedFileBase file = Helpers.FileHelpers.UploadNewsImage(Request.Files[fileName],
                                                                         Server.MapPath("~/Images"),
                                                                         random);
                if (file == null)
                    continue;
                int imageId = Helpers.FileHelpers.SaveImage(file, random);
                Helpers.FileHelpers.RemoveFile(news.ImageId);
                news.ImageId = imageId;
            
            }
            if (ModelState.IsValid)
            {

                unitOfWork.NewsRepository.Update(news);
                unitOfWork.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(news);
        }

        //
        // GET: /News/Delete/5

        public ActionResult Delete(int id = 0)
        {
            News news = unitOfWork.NewsRepository.GetByID(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        //
        // POST: /News/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {

            News news = unitOfWork.NewsRepository.GetByID(id);


            Helpers.FileHelpers.RemoveFile(news.ImageId);
            unitOfWork.ImageRepository.Delete((int)news.ImageId);

            unitOfWork.NewsRepository.Delete(news);
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