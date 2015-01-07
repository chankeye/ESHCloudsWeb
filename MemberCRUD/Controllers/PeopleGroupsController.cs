using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ESHCloudsWeb.DB;
using MemberCRUD.Logic;

namespace MemberCRUD.Controllers
{
    public class PeopleGroupsController : Controller
    {
        PeopleGroupLogic PeopleGroupLogic
        {
            get
            {
                if (_peopleGroupLogic == null)
                    _peopleGroupLogic = new PeopleGroupLogic();
                return _peopleGroupLogic;
            }
        }
        PeopleGroupLogic _peopleGroupLogic;

        // GET: PeopleDatas
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PeopleGroupList(int skip, int take)
        {
            var result = PeopleGroupLogic.GetPeopleGroupList(skip, take);
            return Json(result);
        }

        public ActionResult DropOrderItem()
        {
            return null;
        }

        //private ESHCloudsEntities db = new ESHCloudsEntities();

        //// GET: PeopleGroups
        //public ActionResult Index()
        //{
        //    return View(db.PeopleGroups.ToList());
        //}

        //// GET: PeopleGroups/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: PeopleGroups/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "CN,GroupID,GroupName,GroupOrder")] PeopleGroup peopleGroup)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.PeopleGroups.Add(peopleGroup);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(peopleGroup);
        //}

        //// GET: PeopleGroups/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    PeopleGroup peopleGroup = db.PeopleGroups.Find(id);
        //    if (peopleGroup == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(peopleGroup);
        //}

        //// POST: PeopleGroups/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "CN,GroupID,GroupName,GroupOrder")] PeopleGroup peopleGroup)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(peopleGroup).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(peopleGroup);
        //}

        //// GET: PeopleGroups/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    PeopleGroup peopleGroup = db.PeopleGroups.Find(id);
        //    if (peopleGroup == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(peopleGroup);
        //}

        //// POST: PeopleGroups/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    PeopleGroup peopleGroup = db.PeopleGroups.Find(id);
        //    db.PeopleGroups.Remove(peopleGroup);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
