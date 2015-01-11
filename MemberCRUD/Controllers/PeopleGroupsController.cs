using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using ESHCloudsWeb.DTO;
using ESHCloudsWeb.Logic;
using ESHCloudsWeb.Models;

namespace ESHCloudsWeb.Controllers
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

        public ActionResult PeopleGroupList(int skip, int take, string keyWord, int factoryId = 0)
        {
            var result = PeopleGroupLogic.GetPeopleGroupList(skip, take, keyWord, factoryId);
            return Json(result);
        }

        public ActionResult DropOrderItem(int oldIndex, int newIndex, int page, int pageSize)
        {
            var result = PeopleGroupLogic.DropOrderItem(oldIndex, newIndex, page, pageSize);
            return Json(result);
        }

        public ActionResult GetFactoryList()
        {
            var result = PeopleGroupLogic.GetFactoryList();
            return Json(result);
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult GetPeopleSelectList(List<int> peopleIDs, string keyWord)
        {
            var result = PeopleGroupLogic.GetPeopleSelectList(peopleIDs, keyWord);
            return Json(result);
        }

        public ActionResult CreatePeopleGroup(int factoryId, string groupName, List<CreatePeopleGroupPeople> peopleList)
        {
            var result = PeopleGroupLogic.CreatePeopleGroup(factoryId, groupName, peopleList);
            return Json(result);
        }

        public ActionResult GetPeopleGroupPeopleList(List<int> peopleIDs)
        {
            var result = PeopleGroupLogic.GetPeopleGroupPeopleList(peopleIDs);
            return Json(result);
        }

        public ActionResult GetMailTypeList()
        {
            var list = new List<object>
            {
                new
                {
                    Id = 1,
                    Name = "TO"
                },
                new
                {
                    Id = 2,
                    Name = "CC"
                }
            };
            return Json(list);
        }

        // GET: PeopleGroups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View();
        }

        [HttpPost]
        public ActionResult EditInit(int id)
        {
            var result = PeopleGroupLogic.EditGroupInit(id);
            return Json(result);
        }

        [HttpPost]
        public ActionResult EditPeoplesInit(int id)
        {
            var result = PeopleGroupLogic.EditGroupPeopleListInit(id);
            return Json(result);
        }

        public ActionResult EditPeopleGroup(int id, int factoryId, string groupName, List<CreatePeopleGroupPeople> peopleList)
        {
            var result = PeopleGroupLogic.EditPeopleGroup(id, factoryId, groupName, peopleList);
            return Json(result);
        }

        public ActionResult Delete(int id)
        {
            var result = PeopleGroupLogic.Delete(id);
            return Json(result);
        }

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
