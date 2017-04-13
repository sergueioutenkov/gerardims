using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RusoCars.Models;
using System.Collections;
using RusoCars.DAL;

namespace RusoCars.Controllers
{
    [Authorize]
    public class TeamController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        //
        // GET: /Team/

        public ActionResult Index()
        {
            return View(unitOfWork.TeamRepository.GetAll());
        }

        //
        // GET: /Team/Details/5

        public ActionResult Details(int id = 0)
        {
            Team team = unitOfWork.TeamRepository.GetByID(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        //
        // GET: /Team/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Team/Create

        [HttpPost]
        public ActionResult Create(Team team)
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
                team.ImageId = imageId;
            }
            if (ModelState.IsValid)
            {

                unitOfWork.TeamRepository.Insert(team);
                unitOfWork.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(team);
        }

        //
        // GET: /Team/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Team team = unitOfWork.TeamRepository.GetByID(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        //
        // POST: /Team/Edit/5

        [HttpPost]
        public ActionResult Edit(Team team)
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
                Helpers.FileHelpers.RemoveFile(team.ImageId);
                team.ImageId = imageId;
            
            }
            if (ModelState.IsValid)
            {
                unitOfWork.TeamRepository.Update(team);
                unitOfWork.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(team);
        }

        //
        // GET: /Team/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Team team = unitOfWork.TeamRepository.GetByID(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        //
        // POST: /Team/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Team team = unitOfWork.TeamRepository.GetByID(id);

            Helpers.FileHelpers.RemoveFile(team.ImageId);

            unitOfWork.ImageRepository.Delete((int)team.ImageId);
            unitOfWork.TeamRepository.Delete(team);
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