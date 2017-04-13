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
    public class ClientController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        //
        // GET: /Client/
        public ActionResult Index()
        {
            return View(unitOfWork.ClientRepository.GetAll());
        }

        //
        // GET: /Client/Details/5

        public ActionResult Details(int id = 0)
        {
            Client client = unitOfWork.ClientRepository.GetByID(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        //
        // GET: /Client/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Client/Create

        [HttpPost]
        public ActionResult Create(Client client)
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
                    client.ImageId = imageId;
                    unitOfWork.ClientRepository.Insert(client);
                    unitOfWork.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(client);
        }

        //
        // GET: /Client/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Client client = unitOfWork.ClientRepository.GetByID(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        //
        // POST: /Client/Edit/5

        [HttpPost]
        public ActionResult Edit(Client client)
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
                Helpers.FileHelpers.RemoveFile(client.ImageId);
                client.ImageId = imageId;
             
            }
            if (ModelState.IsValid)
            {

                unitOfWork.ClientRepository.Update(client);
                unitOfWork.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(client);
        }

        //
        // GET: /Client/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Client client = unitOfWork.ClientRepository.GetByID(id); 
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        //
        // POST: /Client/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Client client = unitOfWork.ClientRepository.GetByID(id);

            Helpers.FileHelpers.RemoveFile(client.ImageId);
          
            unitOfWork.ImageRepository.Delete((int)client.ImageId);
            unitOfWork.ClientRepository.Delete(client);
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