using System;
using System.Data.Entity;
using System.Linq;
using ESHCloudsWeb.DTO;

namespace ESHCloudsWeb.Logic
{
    public class PeopleGroupLogic
    {
        /// <summary>
        /// ESHCloudsV2Context
        /// </summary>
        protected Models.ESHCloudsV2Context ESHCloudsContext
        {
            get { return _ESHCloudsContext.Value; }
        }
        Lazy<Models.ESHCloudsV2Context> _ESHCloudsContext = new Lazy<Models.ESHCloudsV2Context>();

        public PeopleGroupList GetPeopleGroupList(int skip, int take)
        {
            var list = ESHCloudsContext.PeopleGroups
                .OrderBy(r => r.GroupOrder)
                .Include(r=> r.GroupDetails)
                .Select(r => new PeopleGroup
                {
                    FactoryName = r.FactoryMaster.FactoryName,
                    GroupID = r.GroupID,
                    GroupName = r.GroupName,
                    GroupOrder = r.GroupOrder,
                    GroupPeopleList = r.GroupDetails.Select(s=> new PeopleGroupDetail
                    {
                        PeopleID = s.PeopleID,
                        MailType = s.MailType
                    }).ToList(),
                })
                .Skip(skip)
                .Take(take)
                .ToList();

            var count = ESHCloudsContext.PeopleGroups.Count();
            var result = new PeopleGroupList
            {
                PeopleGroupDataList = list,
                Count = count
            };

            return result;
        }

        //public bool CreatePersion(PeopleData data)
        //{
        //    if (string.IsNullOrWhiteSpace(data.CN))
        //        return false;

        //    if (string.IsNullOrWhiteSpace(data.Pasd))
        //        return false;

        //    if (string.IsNullOrWhiteSpace(data.Name))
        //        return false;

        //    if (string.IsNullOrWhiteSpace(data.Mail))
        //        return false;

        //    var depart = db.DepartDatas.SingleOrDefault(r => r.DepartID == data.Depart.DepartID);
        //    if (depart == null)
        //        return false;

        //    var person = new ESHCloudsWeb.DB.PeopleData
        //    {
        //        CN = data.CN,
        //        FactoryID = depart.FactoryID,
        //        DepartData = depart,
        //        PeopleID = data.Name,
        //        Pasd = data.Pasd,
        //        Name = data.Name,
        //        Mail = data.Mail
        //    };

        //    db.PeopleDatas.Add(person);
        //    db.SaveChanges();

        //    return true;
        //}

        //public bool EditPersion(PeopleData data)
        //{
        //    if (string.IsNullOrWhiteSpace(data.CN))
        //        return false;

        //    if (string.IsNullOrWhiteSpace(data.Pasd))
        //        return false;

        //    if (string.IsNullOrWhiteSpace(data.Name))
        //        return false;

        //    if (string.IsNullOrWhiteSpace(data.Mail))
        //        return false;

        //    var person = db.PeopleDatas
        //        .Include(r => r.DepartData)
        //        .SingleOrDefault(r => r.PeopleID == data.PeopleID);

        //    if (person == null)
        //        return false;

        //    var depart = db.DepartDatas.SingleOrDefault(r => r.DepartID == data.Depart.DepartID);
        //    if (depart == null)
        //        return false;

        //    person.CN = data.CN;
        //    person.FactoryID = depart.FactoryID;
        //    person.DepartData = depart;
        //    person.Pasd = data.Pasd;
        //    person.Name = data.Name;
        //    person.Mail = data.Mail;

        //    db.SaveChanges();

        //    return true;
        //}

        //public bool DeletePersion(string personId)
        //{
        //    var person = db.PeopleDatas.SingleOrDefault(r => r.PeopleID == personId);
        //    if (person == null)
        //        return false;

        //    db.PeopleDatas.Remove(person);
        //    db.SaveChanges();
        //    return true;
        //}

        //public List<DepartData> GetDepartList()
        //{
        //    var list = db.DepartDatas
        //        .Select(r => new DepartData
        //        {
        //            DepartID = r.DepartID,
        //            DepartName = r.DepartName
        //        })
        //        .ToList();

        //    return list;
        //}
    }
}