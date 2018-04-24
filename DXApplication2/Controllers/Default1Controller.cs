using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DXApplication2.Models;

namespace DXApplication2.Controllers
{
    public class Default1Controller : Controller
    {
        private BIBLIOTECAEntities db = new BIBLIOTECAEntities();

        // GET: /Default1/
        public ActionResult Index()
        {
            var biblioteca = db.BIBLIOTECA.Include(b => b.LIBRO).Include(b => b.USUARIO);
            return View(biblioteca.ToList());
        }

        // GET: /Default1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BIBLIOTECA biblioteca = db.BIBLIOTECA.Find(id);
            if (biblioteca == null)
            {
                return HttpNotFound();
            }
            return View(biblioteca);
        }

        // GET: /Default1/Create
        public ActionResult Create()
        {
            ViewBag.ID_LIBRO = new SelectList(db.LIBRO, "ID", "NOMBRE");
            ViewBag.NOMBRE_USUARIO = new SelectList(db.USUARIO, "nombre", "APELLIDO");
            return View();
        }

        // POST: /Default1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,NOMBRE_USUARIO,ID_LIBRO")] BIBLIOTECA biblioteca)
        {
            if (ModelState.IsValid)
            {
                db.BIBLIOTECA.Add(biblioteca);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_LIBRO = new SelectList(db.LIBRO, "ID", "NOMBRE", biblioteca.ID_LIBRO);
            ViewBag.NOMBRE_USUARIO = new SelectList(db.USUARIO, "nombre", "APELLIDO", biblioteca.NOMBRE_USUARIO);
            return View(biblioteca);
        }

        // GET: /Default1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BIBLIOTECA biblioteca = db.BIBLIOTECA.Find(id);
            if (biblioteca == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_LIBRO = new SelectList(db.LIBRO, "ID", "NOMBRE", biblioteca.ID_LIBRO);
            ViewBag.NOMBRE_USUARIO = new SelectList(db.USUARIO, "nombre", "APELLIDO", biblioteca.NOMBRE_USUARIO);
            return View(biblioteca);
        }

        // POST: /Default1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,NOMBRE_USUARIO,ID_LIBRO")] BIBLIOTECA biblioteca)
        {
            if (ModelState.IsValid)
            {
                db.Entry(biblioteca).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_LIBRO = new SelectList(db.LIBRO, "ID", "NOMBRE", biblioteca.ID_LIBRO);
            ViewBag.NOMBRE_USUARIO = new SelectList(db.USUARIO, "nombre", "APELLIDO", biblioteca.NOMBRE_USUARIO);
            return View(biblioteca);
        }

        // GET: /Default1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BIBLIOTECA biblioteca = db.BIBLIOTECA.Find(id);
            if (biblioteca == null)
            {
                return HttpNotFound();
            }
            return View(biblioteca);
        }

        // POST: /Default1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BIBLIOTECA biblioteca = db.BIBLIOTECA.Find(id);
            db.BIBLIOTECA.Remove(biblioteca);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
