using ESHCloudsWeb.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

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

        // 新增群組
        public bool CreatePeopleGroup(int factoryId, string groupName, List<CreatePeopleGroupPeople> peopleList)
        {
            if (string.IsNullOrWhiteSpace(groupName))
                return false;

            var hasFactory = ESHCloudsContext.FactoryMasters.SingleOrDefault(r => r.FactoryID == factoryId);
            if (hasFactory == null)
                return false;

            var order = ESHCloudsContext.PeopleGroups.Max(r => r.GroupOrder) + 1;
            var newPeopleGroup = new Models.PeopleGroup
            {
                FactoryID = factoryId,
                GroupName = groupName,
                GroupOrder = order
            };
            ESHCloudsContext.PeopleGroups.Add(newPeopleGroup);
            ESHCloudsContext.SaveChanges();

            foreach (var item in peopleList)
            {
                var sql = "Insert into [GroupDetail] (GroupID, PeopleID, MailType) values(" + newPeopleGroup.GroupID +
                          ", " + item.PeopleID + " , '" + item.MailType + "')";
                ESHCloudsContext.Database.ExecuteSqlCommand(sql);
            }

            return true;
        }

        // 取得群組列表
        public PeopleGroupList GetPeopleGroupList(int skip, int take, string keyWord, int factoryId)
        {
            IQueryable<Models.PeopleGroup> peoplegroups = ESHCloudsContext.PeopleGroups;

            // 判斷keyword
            if (string.IsNullOrWhiteSpace(keyWord) == false)
            {
                var names = ESHCloudsContext.GroupDetails
                    .Select(r => r.PeopleData.Name)
                    .Where(r => r.Contains(keyWord));

                var details =
                    ESHCloudsContext.GroupDetails
                    .Where(r => names.Contains(r.PeopleData.Name))
                    .Select(r => r.GroupID);

                peoplegroups = ESHCloudsContext.PeopleGroups
                    .Where(r =>
                        details.Contains(r.GroupID) ||
                        r.GroupName.Contains(keyWord));
            }

            // 判斷factoryId
            if (factoryId != 0)
                peoplegroups = peoplegroups.Where(r => r.FactoryID == factoryId);

            var list = peoplegroups
                .OrderBy(r => r.GroupOrder)
                .Include(r => r.GroupDetails)
                .Select(r => new
                {
                    r.FactoryMaster.FactoryName,
                    r.GroupID,
                    r.GroupName,
                    r.GroupOrder,
                    PeopleList = r.GroupDetails.Select(s => new
                    {
                        PeopleName = s.PeopleData.Name,
                        MailType = s.MailType
                    }).ToList(),
                })
                .Skip(skip)
                .Take(take)
                .ToList();

            var peopleGroupDataList = list.Select(r => new PeopleGroup
            {
                FactoryName = r.FactoryName,
                GroupID = r.GroupID,
                GroupName = r.GroupName,
                GroupOrder = r.GroupOrder,
            }).ToList();

            // 組合群組成員
            foreach (var item in list)
            {
                var to = "";
                var cc = "";
                foreach (var people in item.PeopleList)
                {
                    if (people.MailType.ToUpper() == "TO")
                    {
                        if (string.IsNullOrWhiteSpace(to))
                            to = people.PeopleName;
                        else
                            to = to + "," + people.PeopleName;
                    }
                    else if (people.MailType.ToUpper() == "CC")
                    {
                        if (string.IsNullOrWhiteSpace(cc))
                            cc = people.PeopleName;
                        else
                            cc = cc + "," + people.PeopleName;
                    }
                }
                var peopleGroup = peopleGroupDataList.Single(r => r.GroupID == item.GroupID);
                peopleGroup.GroupPeopleList = new List<string> { to, cc };
            }

            var count = ESHCloudsContext.PeopleGroups.Count();
            var result = new PeopleGroupList
            {
                PeopleGroupDataList = peopleGroupDataList,
                Count = count
            };

            return result;
        }

        // 拖曳排序
        public bool DropOrderItem(int oldIndex, int newIndex, int page, int pageSize)
        {
            oldIndex = oldIndex + (page - 1) * pageSize;
            newIndex = newIndex + (page - 1) * pageSize;
            if (oldIndex < newIndex)
            {
                var currGroup = ESHCloudsContext.PeopleGroups.SingleOrDefault(r => r.GroupOrder == oldIndex);
                if (currGroup == null)
                    return false;

                var peopleGroupList = ESHCloudsContext.PeopleGroups
                    .Where(r =>
                        r.GroupOrder <= newIndex &&
                        r.GroupOrder > oldIndex &&
                        r.GroupID != currGroup.GroupID
                    ).ToList();

                foreach (var peopleGroup in peopleGroupList)
                    peopleGroup.GroupOrder--;

                currGroup.GroupOrder = newIndex;
                ESHCloudsContext.SaveChanges();
            }
            else
            {
                var currGroup = ESHCloudsContext.PeopleGroups.SingleOrDefault(r => r.GroupOrder == oldIndex);
                if (currGroup == null)
                    return false;

                var peopleGroupList = ESHCloudsContext.PeopleGroups
                    .Where(r =>
                        r.GroupOrder < oldIndex &&
                        r.GroupOrder >= newIndex &&
                        r.GroupID != currGroup.GroupID
                    ).ToList();

                foreach (var peopleGroup in peopleGroupList)
                    peopleGroup.GroupOrder++;

                currGroup.GroupOrder = newIndex;
                ESHCloudsContext.SaveChanges();
            }

            return true;
        }

        // 取得廠區列表
        public List<FactoryData> GetFactoryList()
        {
            var list = new List<FactoryData>
            {
                new FactoryData
                {
                    FactoryID = 0,
                    FactoryName = "所有廠區"
                }
            };

            list.AddRange(ESHCloudsContext.FactoryMasters
                .Select(r => new FactoryData
                {
                    FactoryID = r.FactoryID,
                    FactoryName = r.FactoryName
                })
                .ToList());

            return list;
        }

        // 取得人員列表
        public List<PeopleSelector> GetPeopleSelectList(List<int> peopleIDs = null, string keyWord = "")
        {
            var peoples = ESHCloudsContext.PeopleDatas.Select(r => new PeopleSelector
            {
                Checked = false,
                Name = r.DepartData.DepartName + " " + r.Name,
                Id = r.PeopleID
            });

            if (string.IsNullOrWhiteSpace(keyWord) == false)
                peoples = peoples.Where(r => r.Name.Contains(keyWord));

            var list = peoples.ToList();

            if (peopleIDs != null)
            {
                foreach (var id in peopleIDs)
                    list.Single(r => r.Id == id).Checked = true;
            }

            return list;
        }

        // 取得新增群組時的人員列表
        public List<CreatePeopleGroupPeople> GetPeopleGroupPeopleList(List<int> peopleIDs = null)
        {
            if (peopleIDs == null)
                return new List<CreatePeopleGroupPeople>();

            return ESHCloudsContext.PeopleDatas
                .Where(r => peopleIDs.Contains(r.PeopleID))
                .Select(r => new CreatePeopleGroupPeople
                {
                    PeopleID = r.PeopleID,
                    PeopleName = r.Name,
                    DepartName = r.DepartData.DepartName,
                    MailType = "TO"
                })
                .ToList();
        }

        public EditPeopleGroup EditGroupInit(int id)
        {
            var group = ESHCloudsContext
                .PeopleGroups
                .SingleOrDefault(r => r.GroupID == id);
            if (group == null)
                return new EditPeopleGroup();

            var result = new EditPeopleGroup
            {
                FactoryID = group.FactoryID,
                GroupName = group.GroupName,
            };

            return result;
        }

        public List<CreatePeopleGroupPeople> EditGroupPeopleListInit(int id)
        {
            var group = ESHCloudsContext
                .PeopleGroups
                .Include(r => r.GroupDetails)
                .SingleOrDefault(r => r.GroupID == id);
            if (group == null)
                return new List<CreatePeopleGroupPeople>();

            var result = new List<CreatePeopleGroupPeople>();
            foreach (var item in group.GroupDetails)
            {
                result.Add(new CreatePeopleGroupPeople
                {
                    PeopleID = item.PeopleID,
                    DepartName = item.PeopleData.DepartData.DepartName,
                    PeopleName = item.PeopleData.Name,
                    MailType = item.MailType
                });
            }

            return result;
        }

        // 修改群組
        public bool EditPeopleGroup(int id, int factoryId, string groupName, List<CreatePeopleGroupPeople> peopleList)
        {
            if (string.IsNullOrWhiteSpace(groupName))
                return false;

            var hasFactory = ESHCloudsContext.FactoryMasters.Any(r => r.FactoryID == factoryId);
            if (hasFactory == false)
                return false;

            var group = ESHCloudsContext.PeopleGroups.SingleOrDefault(r => r.GroupID == id);
            if (group == null)
                return false;

            group.FactoryID = factoryId;
            group.GroupName = groupName;

            ESHCloudsContext.SaveChanges();

            var deleteSql = "Delete [GroupDetail] where GroupID = " + id;
            ESHCloudsContext.Database.ExecuteSqlCommand(deleteSql);

            foreach (var item in peopleList)
            {
                var insertSql = "Insert into [GroupDetail] (GroupID, PeopleID, MailType) values(" + id +
                          ", " + item.PeopleID + " , '" + item.MailType + "')";
                ESHCloudsContext.Database.ExecuteSqlCommand(insertSql);
            }

            return true;
        }

        public bool Delete(int id)
        {
            var group = ESHCloudsContext.PeopleGroups.SingleOrDefault(r => r.GroupID == id);
            if (group == null)
                return false;

            if (group.AlarmMasters.Count() != 0)
                return false;

            try
            {
                var deleteSql = "Delete [GroupDetail] where GroupID = " + id;
                ESHCloudsContext.Database.ExecuteSqlCommand(deleteSql);

                ESHCloudsContext.PeopleGroups.Remove(group);
                ESHCloudsContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public List<object> GetMailTypeList()
        {
            return new List<object>
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
        }
        //public List<CreatePeopleGroupPeople> GetCreatePeopleGroupPeopleList(List<int> peopleIds = null )
        //{
        //       ESHCloudsContext.PeopleDatas.
        //}

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