using ESHCloudsWeb.DTO;
using ESHCloudsWeb.Logic;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;

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

        public ActionResult GetPeopleGroupPeopleList(List<int> peopleIDs, List<CreatePeopleGroupPeople> selectedList)
        {
            var result = PeopleGroupLogic.GetPeopleGroupPeopleList(peopleIDs, selectedList);
            return Json(result);
        }

        public ActionResult GetMailTypeList()
        {
            var result = PeopleGroupLogic.GetMailTypeList();
            return Json(result);
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

        [HttpPost]
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
    }
}
