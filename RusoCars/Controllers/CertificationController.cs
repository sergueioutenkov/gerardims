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
    public class CertificationController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();


        //
        // GET: /Certification/

        public ActionResult Index()
        {
            return View(unitOfWork.CertificationRepository.GetAll());
        }

        //
        // GET: /Certification/Details/5

        public ActionResult Details(int id = 0)
        {
            Certification certification = unitOfWork.CertificationRepository.GetByID(id);
            if (certification == null)
            {
                return HttpNotFound();
            }
            return View(certification);
        }

        //
        // GET: /Certification/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Certification/Create

        [HttpPost]
        public ActionResult Create(Certification certification)
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

                if (ModelState.IsValid)
                {
                    certification.ImageId = imageId;
                    unitOfWork.CertificationRepository.Insert(certification);
                    unitOfWork.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(certification);
        }

        //
        // GET: /Certification/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Certification certification = unitOfWork.CertificationRepository.GetByID(id);
            if (certification == null)
            {
                return HttpNotFound();
            }
            return View(certification);
        }

        //
        // POST: /Certification/Edit/5

        [HttpPost]
        public ActionResult Edit(Certification certification)
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
                Helpers.FileHelpers.RemoveFile(certification.ImageId);
                certification.ImageId = imageId;

                if (ModelState.IsValid)
                {
                    certification.ImageId = imageId;
                    unitOfWork.CertificationRepository.Update(certification);
                    unitOfWork.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(certification);
        }

        //
        // GET: /Certification/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Certification certification = unitOfWork.CertificationRepository.GetByID(id);
            if (certification == null)
            {
                return HttpNotFound();
            }
            return View(certification);
        }

        //
        // POST: /Certification/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Certification certification = unitOfWork.CertificationRepository.GetByID(id);

            Helpers.FileHelpers.RemoveFile(certification.ImageId);
           
            unitOfWork.ImageRepository.Delete((int)certification.ImageId);
            unitOfWork.CertificationRepository.Delete(certification);
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