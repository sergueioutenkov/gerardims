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
    public class TextController : Controller
    {
        CarsRusoEntities db = new CarsRusoEntities();
        private UnitOfWork unitOfWork = new UnitOfWork();

        //
        // GET: /Text/

        public ActionResult Index()
        {

            return View(unitOfWork.TextRepository.GetAll());
        }

        //
        // GET: /Text/Details/5

        public ActionResult Details(int id = 0)
        {
            Text text = unitOfWork.TextRepository.GetByID(id,null);
            if (text == null)
            {
                return HttpNotFound();
            }
            return View(text);
        }

        //
        // GET: /Text/Create

        public ActionResult Create()
        {
            ViewBag.LanguageId = new SelectList(db.Languages, "LanguageId", "Name");
            return View();
        }

        //
        // POST: /Text/Create

        [HttpPost]
        public ActionResult Create(Text text)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.TextRepository.Insert(text);
                unitOfWork.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LanguageId = new SelectList(db.Languages, "LanguageId", "Name", text.LanguageId);
            return View(text);
        }

        //
        // GET: /Text/Edit/5
       [ValidateInput(false)]
        public ActionResult Edit(int id = 0)
        {
          
            Text text = unitOfWork.TextRepository.GetByID(id,null);
            if (text == null)
            {
                return HttpNotFound();
            }
            ViewBag.LanguageId = new SelectList(db.Languages, "LanguageId", "Name", text.LanguageId);
            return View(text);
        }

        //
        // POST: /Text/Edit/5

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Text text)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.TextRepository.Update(text);
                unitOfWork.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LanguageId = new SelectList(db.Languages, "LanguageId", "Name", text.LanguageId);
            return View(text);
        }

        //
        // GET: /Text/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Text text = unitOfWork.TextRepository.GetByID(id,null);
            if (text == null)
            {
                return HttpNotFound();
            }
            return View(text);
        }

        //
        // POST: /Text/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Text text = unitOfWork.TextRepository.GetByID(id,null);
            unitOfWork.TextRepository.Delete(id);
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