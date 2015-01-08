using ESHCloudsWeb.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ESHCloudsWeb.Logic
{
    public class PeopleDataLogic
    {
        /// <summary>
        /// ESHCloudsV2Context
        /// </summary>
        protected Models.ESHCloudsV2Context ESHCloudsContext
        {
            get { return _ESHCloudsContext.Value; }
        }
        Lazy<Models.ESHCloudsV2Context> _ESHCloudsContext = new Lazy<Models.ESHCloudsV2Context>();

        //public PeopleDataLogic()
        //{
        //    db.Configuration.ProxyCreationEnabled = false;
        //}

        public PeopleList GetPeopleList(int skip, int take)
        {
            var list = ESHCloudsContext.PeopleDatas
                .OrderByDescending(r => r.PeopleID)
                .Include(r => r.DepartData)
                .Select(r => new PeopleData
                {
                    Depart = new DepartData
                    {
                        DepartID = r.DepartID,
                        DepartName = r.DepartData.DepartName,
                    },
                    PeopleID = r.PeopleID,
                    UserID = r.UserID,
                    UserPasd = r.UserPasd,
                    Name = r.Name,
                    Mail = r.Mail
                })
                .Skip(skip)
                .Take(take)
                .ToList();

            var count = ESHCloudsContext.PeopleDatas.Count();
            var result = new PeopleList
            {
                PeopleDataList = list,
                Count = count
            };

            return result;
        }

        public bool CreatePersion(PeopleData data)
        {
            if (string.IsNullOrWhiteSpace(data.UserID))
                return false;

            if (string.IsNullOrWhiteSpace(data.UserPasd))
                return false;

            if (string.IsNullOrWhiteSpace(data.Name))
                return false;

            if (string.IsNullOrWhiteSpace(data.Mail))
                return false;

            var depart = ESHCloudsContext.DepartDatas.SingleOrDefault(r => r.DepartID == data.Depart.DepartID);
            if (depart == null)
                return false;

            var hasUserID = ESHCloudsContext.PeopleDatas
                .Any(r =>
                    r.DepartData.FactoryMaster.CN == depart.FactoryMaster.CN &&
                    r.UserID == data.UserID);
            if (hasUserID == true)
                return false;

            var person = new Models.PeopleData
            {
                DepartData = depart,
                UserID = data.UserID,
                UserPasd = data.UserPasd,
                Name = data.Name,
                Mail = data.Mail
            };

            ESHCloudsContext.PeopleDatas.Add(person);
            ESHCloudsContext.SaveChanges();

            return true;
        }

        public bool EditPersion(PeopleData data)
        {
            if (string.IsNullOrWhiteSpace(data.UserID))
                return false;

            if (string.IsNullOrWhiteSpace(data.UserPasd))
                return false;

            if (string.IsNullOrWhiteSpace(data.Name))
                return false;

            if (string.IsNullOrWhiteSpace(data.Mail))
                return false;

            var person = ESHCloudsContext.PeopleDatas
                .Include(r => r.DepartData)
                .SingleOrDefault(r => r.PeopleID == data.PeopleID);

            if (person == null)
                return false;

            var depart = ESHCloudsContext.DepartDatas.SingleOrDefault(r => r.DepartID == data.Depart.DepartID);
            if (depart == null)
                return false;

            var hasUserID = ESHCloudsContext.PeopleDatas
                .Any(r =>
                    r.DepartData.FactoryMaster.CN == depart.FactoryMaster.CN &&
                    r.UserID == data.UserID &&
                    r.UserID != person.UserID);
            if (hasUserID == true)
                return false;

            person.DepartData = depart;
            person.UserID = data.UserID;
            person.UserPasd = data.UserPasd;
            person.Name = data.Name;
            person.Mail = data.Mail;

            ESHCloudsContext.SaveChanges();

            return true;
        }

        public bool DeletePersion(int peopleID)
        {
            var people = ESHCloudsContext.PeopleDatas.SingleOrDefault(r => r.PeopleID == peopleID);
            if (people == null)
                return false;

            ESHCloudsContext.PeopleDatas.Remove(people);
            ESHCloudsContext.SaveChanges();
            return true;
        }

        public List<DepartData> GetDepartList()
        {
            List<DepartData> list = new List<DepartData>
            {
                new DepartData
                {
                    DepartID = 0,
                    DepartName = "請選擇..."
                }
            };

            list.AddRange(ESHCloudsContext.DepartDatas
                .Select(r => new DepartData
                {
                    DepartID = r.DepartID,
                    DepartName = r.DepartName
                })
                .ToList());

            return list;
        }
    }
}