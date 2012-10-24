using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using System.Web;


namespace echo.BLL.Products
{
    /// <summary>
    /// Summary description for GroupRepository
    /// </summary>
    public class GroupRepository:BaseProductRepository
    {
        public Group GetGroupById(int groupId)
        {
            try
            {

            
            string key = CacheKey + "Group_ById=" + groupId;

            if (EnableCaching && Cache[key] != null)
                return (Group) Cache[key];

            Group iGroup = Productctx.Groups.Where(p => p.GroupID == groupId).FirstOrDefault();

            if(EnableCaching)
                CacheData(key,iGroup,CacheDuration);

            return iGroup;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public List<Group> GetGroups()
        {
            string key = CacheKey + "Groups";

            if (EnableCaching && Cache[key] != null)
                return (List<Group>)Cache[key];

            List<Group>iGroup = Productctx.Groups.OrderBy(p=>p.GroupOrder).ToList();

            if (EnableCaching)
                CacheData(key, iGroup, CacheDuration);

            return iGroup;
        }

        public Group AddGroup(Group _group)
        {
            try
            {
                if(_group.EntityState==EntityState.Detached)
                    Productctx.AddToGroups(_group);
                PurgeCacheItems(CacheKey);

                return Productctx.SaveChanges() > 0 ? _group : null;
            }
            catch (Exception ex)
            {
                ActiveExceptions.Add(CacheKey+"_"+_group.GroupID,ex);
                return null;
            }
        }

        public bool DeleteGroup(Group _group)
        {
            try
            {
                Productctx.DeleteObject(_group);
                Productctx.SaveChanges();
                PurgeCacheItems(CacheKey);
                return true;
            }
            catch (Exception ex)
            {
                ActiveExceptions.Add(CacheKey + "_" + _group.GroupID, ex);
                return false;
            }
        }

        public bool DeleteGroup(int groupId)
        {
            return DeleteGroup(GetGroupById(groupId));
        }

    }
}