using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MemberCRUD.Logic;
using MemberCRUD.DTO;

namespace MemberCRUD.Controllers
{
    public class PeopleDataController : Controller
    {
        PeopleDataLogic PeopleDataLogic
        {
            get
            {
                if (_peopleDataLogic == null)
                    _peopleDataLogic = new PeopleDataLogic();
                return _peopleDataLogic;
            }
        }
        PeopleDataLogic _peopleDataLogic;

        // GET: PeopleDatas
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PeopleList(int skip, int take)
        {
            var result = PeopleDataLogic.GetPeopleList(skip, take);
            return Json(result);
        }

        public ActionResult Create(PeopleData peopleData)
        {
            var result = PeopleDataLogic.CreatePersion(peopleData);
            return Json(result);
        }

        public ActionResult Edit(PeopleData peopleData)
        {
            var result = PeopleDataLogic.EditPersion(peopleData);
            return Json(result);
        }

        public ActionResult Delete(string personId)
        {
            var result = PeopleDataLogic.DeletePersion(personId);
            return Json(result);
        }

        // GET: PeopleDatas/Details/5
        //    public ActionResult Details(string id)
        //    {
        //        if (id == null)
        //        {
        //            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //        }
        //        PeopleData peopleData = db.PeopleDatas.Find(id);
        //        if (peopleData == null)
        //        {
        //            return HttpNotFound();
        //        }
        //        return View(peopleData);
        //    }

        //    // GET: PeopleDatas/Create
        //    public ActionResult Create()
        //    {
        //        ViewBag.CN = new SelectList(db.DepartDatas, "CN", "DepartName");
        //        return View();
        //    }

        //    // POST: PeopleDatas/Create
        //    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public ActionResult Create([Bind(Include = "CN,FactoryID,DepartID,PeopleID,Pasd,Name,Mail")] PeopleData peopleData)
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            db.PeopleDatas.Add(peopleData);
        //            db.SaveChanges();
        //            return RedirectToAction("Index");
        //        }

        //        ViewBag.CN = new SelectList(db.DepartDatas, "CN", "DepartName", peopleData.CN);
        //        return View(peopleData);
        //    }

        //    // GET: PeopleDatas/Edit/5
        //    public ActionResult Edit(string id)
        //    {
        //        if (id == null)
        //        {
        //            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //        }
        //        PeopleData peopleData = db.PeopleDatas.Find(id);
        //        if (peopleData == null)
        //        {
        //            return HttpNotFound();
        //        }
        //        ViewBag.CN = new SelectList(db.DepartDatas, "CN", "DepartName", peopleData.CN);
        //        return View(peopleData);
        //    }

        //    // POST: PeopleDatas/Edit/5
        //    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public ActionResult Edit([Bind(Include = "CN,FactoryID,DepartID,PeopleID,Pasd,Name,Mail")] PeopleData peopleData)
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            db.Entry(peopleData).State = EntityState.Modified;
        //            db.SaveChanges();
        //            return RedirectToAction("Index");
        //        }
        //        ViewBag.CN = new SelectList(db.DepartDatas, "CN", "DepartName", peopleData.CN);
        //        return View(peopleData);
        //    }

        //    // GET: PeopleDatas/Delete/5
        //    public ActionResult Delete(string id)
        //    {
        //        if (id == null)
        //        {
        //            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //        }
        //        PeopleData peopleData = db.PeopleDatas.Find(id);
        //        if (peopleData == null)
        //        {
        //            return HttpNotFound();
        //        }
        //        return View(peopleData);
        //    }

        //    // POST: PeopleDatas/Delete/5
        //    [HttpPost, ActionName("Delete")]
        //    [ValidateAntiForgeryToken]
        //    public ActionResult DeleteConfirmed(string id)
        //    {
        //        PeopleData peopleData = db.PeopleDatas.Find(id);
        //        db.PeopleDatas.Remove(peopleData);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    protected override void Dispose(bool disposing)
        //    {
        //        if (disposing)
        //        {
        //            db.Dispose();
        //        }
        //        base.Dispose(disposing);
        //    }
    }
}
