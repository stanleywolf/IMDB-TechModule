﻿using System.Linq;
using System.Net;
using System.Web.Mvc;
using IMDB.Models;

namespace IMDB.Controllers
{
    [ValidateInput(false)]
    public class FilmController : Controller
    {
        private IMDBDbContext db = new IMDBDbContext();

        [HttpGet]
        [Route("")]
        public ActionResult Index()
        {
            var films = db.Films.ToList();
            return View(films);
        }

        [HttpGet]
        [Route("create")]
        public ActionResult Create()
        {

            return View();

        }

        [HttpPost]
        [Route("create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Film film)
        {
            if (this.ModelState.IsValid)
            {
                db.Films.Add(film);
                db.SaveChanges();
                return Redirect("/");
            }
            return View(film);

        }

        [HttpGet]
        [Route("edit/{id}")]
        public ActionResult Edit(int? id)
        {
            var film = db.Films.Find(id);
            if (id == null)
            {
                return HttpNotFound();
            }
            return View(film);
        }

        [HttpPost]
        [Route("edit/{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult EditConfirm(int? id, Film filmModel)
        {
            var filnFromDB = db.Films.Find(id);
            if (filnFromDB == null)
            {
                return HttpNotFound();
            }
            if (this.ModelState.IsValid)
            {
                filnFromDB.Name = filmModel.Name;
                filnFromDB.Genre = filmModel.Genre;
                filnFromDB.Director = filmModel.Director;
                filnFromDB.Year = filmModel.Year;

                db.SaveChanges();

                return Redirect("/");
            }
            return View("Edit",filmModel);
        }

        [HttpGet]
        [Route("delete/{id}")]
        public ActionResult Delete(int? id)
        {
            var film = db.Films.Find(id);
            if (id == null)
            {
                return HttpNotFound();
            }
            return View(film);        
        }

        [HttpPost]
        [Route("delete/{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int? id, Film filmModel)
        {
            var filnFromDB = db.Films.Find(id);
            if (filnFromDB == null)
            {
                return HttpNotFound();
            }            
                db.Films.Remove(filnFromDB);
                db.SaveChanges();

                return Redirect("/");           
        }
    }
}