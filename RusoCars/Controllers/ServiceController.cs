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
    public class ServiceController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        //
        // GET: /Service/

        public ActionResult Index()
        {
            var services = unitOfWork.ServiceRepository.dbSet.Include(s => s.ImageAfter).Include(s => s.ImageBefore);
            return View(services.ToList());
        }

        //
        // GET: /Service/Details/5

        public ActionResult Details(int id = 0)
        {
            Service service = unitOfWork.ServiceRepository.GetByID(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        //
        // GET: /Service/Create

        public ActionResult Create()
        {

            return View();
        }

        //
        // POST: /Service/Create

        [HttpPost]
        
        public ActionResult Create(Service service)
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
                if (fileName.Contains("BeforeImage"))
                {

                    int imageBefore = Helpers.FileHelpers.SaveImage(file, random);
                    service.BeforeImage = imageBefore;
                }
                else if (fileName.Contains("AfterImage"))
                {
                    int imageAfter = Helpers.FileHelpers.SaveImage(file, random);
                    service.AfterImage = imageAfter;
                }
            }
            if (ModelState.IsValid)
            {
                unitOfWork.ServiceRepository.Insert(service);
                unitOfWork.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(service);
        }

        //
        // GET: /Service/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Service service = unitOfWork.ServiceRepository.GetByID(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        //
        // POST: /Service/Edit/5

        [HttpPost]
        public ActionResult Edit(Service service)
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
                if (fileName.Contains("BeforeImage"))
                {
                    int imageBefore = Helpers.FileHelpers.SaveImage(file, random);
                    Helpers.FileHelpers.RemoveFile(service.BeforeImage);
                    service.BeforeImage = imageBefore;
                }
                else if (fileName.Contains("AfterImage"))
                {
                    int imageAfter = Helpers.FileHelpers.SaveImage(file, random);
                    Helpers.FileHelpers.RemoveFile(service.AfterImage);
                    service.AfterImage = imageAfter;
                }
            }
            if (ModelState.IsValid)
            {
                unitOfWork.ServiceRepository.Update(service);
                unitOfWork.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(service);
        }

        //
        // GET: /Service/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Service service = unitOfWork.ServiceRepository.GetByID(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        //
        // POST: /Service/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Service service = unitOfWork.ServiceRepository.GetByID(id);

            Helpers.FileHelpers.RemoveFile(service.AfterImage);
            Helpers.FileHelpers.RemoveFile(service.BeforeImage);

            unitOfWork.ImageRepository.Delete((int)service.AfterImage);
            unitOfWork.ImageRepository.Delete((int)service.BeforeImage);
            unitOfWork.ServiceRepository.Delete(service);
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