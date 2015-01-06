using MemberCRUD.DTO;
using MemberCRUD.Logic;
using System.Web.Mvc;

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

        public ActionResult GetDepartList()
        {
            return Json(PeopleDataLogic.GetDepartList());
        }
    }
}
