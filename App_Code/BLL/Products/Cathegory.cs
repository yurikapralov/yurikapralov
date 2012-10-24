using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace echo.BLL.Products
{
    /// <summary>
    /// Summary description for Cathegory
    /// </summary>
    public partial  class Cathegory:IBaseEntity 
    {

        private string _setName = "Cathegories";


        public string SetName
        {
            get { return _setName; }
            set { _setName = value; }
        }

        public bool IsValid
        {
            get
            {
                if(!string.IsNullOrEmpty(CatNameEng) && !string.IsNullOrEmpty(CatNameRus) &&
                    !string.IsNullOrEmpty(HeaderImageEngURL) && !string.IsNullOrEmpty(HeaderImageRusURL) 
                    && GroupId>0 && TemplateId>0)
                    return true;
                return false;
            }
        }

        public int GroupId
        {
            get
            {
                if(GroupReference.EntityKey !=null)
                {
                    return int.Parse(GroupReference.EntityKey.EntityKeyValues[0].Value.ToString());
                }
                return 0;
            }
            set
            {
                if (GroupReference.EntityKey != null)
                    GroupReference.EntityKey = null;
                GroupReference.EntityKey = new EntityKey("ProductsEntities.Groups", "GroupID", value);
            }
        }

        public int TemplateId
        {
            get
            {
                if(TemplateReference.EntityKey!=null)
                {
                    return int.Parse(TemplateReference.EntityKey.EntityKeyValues[0].Value.ToString());
                }
                return 0;
            }
            set
            {
                if (TemplateReference.EntityKey !=null)
                    TemplateReference.EntityKey = null;
                TemplateReference.EntityKey = new EntityKey("ProductsEntities.Templates", "TempleID", value);
            }
        }

        public string GroupNameRus
        {
            get { return Group.GroupNameRus; }
        }

        public string FullRusName
        {
            get { return this.CatNameRus + " / " + this.GroupNameRus; }
        }

       /* public string TempleName
        {
            get
            {
                return Template.TempleName;
            }

        }*/
    }
}
