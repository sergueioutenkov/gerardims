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
    public class PilotController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        public ActionResult Index()
        {
            return View(unitOfWork.PilotRepository.GetAll());
        }


        public ActionResult Details(int id = 0)
        {
            Pilot pilot = unitOfWork.PilotRepository.GetByID(id);
            if (pilot == null)
            {
                return HttpNotFound();
            }
            return View(pilot);
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Pilot pilot)
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
                pilot.ImageId = imageId;
            }
            if (ModelState.IsValid)
            {
                unitOfWork.PilotRepository.Insert(pilot);
                unitOfWork.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pilot);
        }


        public ActionResult Edit(int id = 0)
        {
            Pilot pilot = unitOfWork.PilotRepository.GetByID(id);
            if (pilot == null)
            {
                return HttpNotFound();
            }
            return View(pilot);
        }


        [HttpPost]
        public ActionResult Edit(Pilot pilot)
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

                Helpers.FileHelpers.RemoveFile(pilot.ImageId);
                pilot.ImageId = imageId;

            }
            if (ModelState.IsValid)
            {
                unitOfWork.PilotRepository.Update(pilot);
                unitOfWork.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pilot);
        }

        public ActionResult Delete(int id = 0)
        {
            Pilot pilot = unitOfWork.PilotRepository.GetByID(id);
            if (pilot == null)
            {
                return HttpNotFound();
            }
            return View(pilot);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Pilot pilot = unitOfWork.PilotRepository.GetByID(id);

            Helpers.FileHelpers.RemoveFile(pilot.ImageId);

            unitOfWork.ImageRepository.Delete((int)pilot.ImageId);
            unitOfWork.PilotRepository.Delete(pilot);
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