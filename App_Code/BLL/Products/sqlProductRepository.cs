using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using echo.BLL;
using echo.BLL.Products;

/// <summary>
/// Сводное описание для sqlProductRepository
/// </summary>
public class sqlProductRepository:sqlBase
{
	public sqlProductRepository()
	{
	    this.ConnectionString = "echoConnectionString";
	}

    protected sqlProduct GetProductFromReader(IDataReader reader)
    {
        sqlProduct product=new sqlProduct(
            (int)reader["ProdId"],
            (int)reader["ProdTypeId"],
            (int)reader["PlatformID"],
            (string)reader["ProductNameRus"],
            (string)reader["ProductNameEng"],
            reader["ProductDescriptionRus"] == DBNull.Value ? "" : (string)reader["ProductDescriptionRus"],
            reader["ProductDescriptionEng"] == DBNull.Value ? "" : (string)reader["ProductDescriptionEng"],
            (string)reader["ThumbURL"],
            (decimal)reader["OrigPrice"],
            reader["WHSPrice"] == DBNull.Value ? 0 : (decimal)reader["WHSPrice"],
            reader["SalePrice"] == DBNull.Value ? 0 : (decimal)reader["SalePrice"],
            (DateTime)reader["DateCreated"],
            reader["DateUpdated"] == DBNull.Value ? DateTime.MinValue : (DateTime)reader["DateUpdated"],
            (string)reader["SortedName"],
            (bool)reader["Available"]);
        return product;
    }

    //Формируем список групп отображенных в платинум    
    private string GetGroups()
    {
        List<int> groups = new List<int>();
        if (Helpers.Settings.PlatinumProduct.PlatinumGroupId > 0)
            groups.Add(Helpers.Settings.PlatinumProduct.PlatinumGroupId);
        if (Helpers.Settings.PlatinumExtraProduct.PlatinumGroupId != null)
            foreach (int gid in Helpers.Settings.PlatinumExtraProduct.PlatinumGroupId)
                groups.Add(gid);
        if (groups.Count == 0)
            return null;

        string grouplist = "";

        foreach (int group in groups)
        {
            grouplist += group.ToString() + ",";
        }

        grouplist = grouplist.Substring(0, grouplist.Length - 1);

        grouplist = "(" + grouplist + ")";

        return grouplist;
    }

    private List<sqlProduct> GetNewPlatinumProductsByPtypeSQL(int pType,ProductSorted sorted)
    {

        //Формируем список групп отображенных в платинум
        string grouplist = GetGroups();

        //Определяем период, соответствующий новинкам
        DateTime period = DateTime.Now.AddDays(-90);

        //pType соответствует templateId

        //Задаем порядок сортировки
        string sOrder = "";

        switch (sorted)
        {
            case ProductSorted.DateAsc:
                sOrder = "p.DateCreated";
                break;
            case ProductSorted.DateDesc:
                sOrder = "p.DateCreated Desc";
                break;
            case ProductSorted.NameAsc:
                sOrder = "p.SortedName";
                break;
            case ProductSorted.NameDesc:
                sOrder = "p.SortedName Desc";
                break;
            case ProductSorted.PriceAsc:
                sOrder = "p.OrigPrice";
                break;
            case ProductSorted.PriceDesc:
                sOrder = "p.OrigPrice Desc";
                break;
            case ProductSorted.UpdateAsc:
                sOrder = "p.DateUpdated";
                break;
            case ProductSorted.UpdateDesc:
                sOrder = "p.DateUpdated Desc";
                break;
        }

        using (SqlConnection cn=new SqlConnection(GetActualConnectionString()))
        {
            string sql =
                string.Format(
                    @"select p.ProdID, p.ProdTypeID, p.PlatformID, p.ProductNameRus,p.ProductNameEng,
                    p.ProductDescriptionRus ,p.ProductDescriptionEng, p.ThumbURL, p.OrigPrice, p.WHSPrice,
                    p.SalePrice, p.DateCreated, p.DateUpdated, p.SortedName, p.Available 
                    from dbo.echoProducts p 
                    join dbo.echoCollection cl on p.ProdID=cl.ProdID
                    join dbo.echoCathegory c on cl.CatID=c.CatID
                    where p.DateCreated>@dateCreated and c.TempleID=@pType and c.GroupID in {0}
                    Order by {1}",grouplist,sOrder);
            SqlCommand cmd=new SqlCommand(sql,cn);
            cmd.Parameters.Add("@dateCreated", SqlDbType.DateTime).Value = period;
            cmd.Parameters.Add("@pType", SqlDbType.Int).Value = pType;
            cn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<sqlProduct> products=new List<sqlProduct>();
            while (reader.Read())
            {
                products.Add(GetProductFromReader(reader));
            }
            return products;
        }
    }

    public List<sqlProduct>  GetNewPlatinumProductsByPtype (int pType,ProductSorted sorted)
    {
        string key = CacheKey + "_NewPlatProductsByPtypeId=" + pType + "_SortedBy_" + sorted;

        if(EnableCaching && Cache[key]!=null)
        {
            return (List<sqlProduct>) Cache[key];
        }
        List<sqlProduct> products = null;
        products = GetNewPlatinumProductsByPtypeSQL(pType, sorted);

        if(EnableCaching)
            CacheData(key, products, CacheDuration);

        return products;
    }

    private List<int> GetTemplatesIdsForPlatinumNewsSQL()
    {
        //Формируем список групп отображенных в платинум
        string grouplist = GetGroups();

        //Определяем период, соответствующий новинкам
        DateTime period = DateTime.Now.AddDays(-90);

        using (SqlConnection cn = new SqlConnection(GetActualConnectionString()))
        {
            string sql =
                string.Format(
                    @"select distinct TempleID from dbo.echoCathegory c
                     join dbo.echoCollection cl on cl.CatID=c.CatID 
                     join dbo.echoProducts p on p.ProdID=cl.ProdID 
                     where p.DateCreated>@dateCreated  and c.GroupID in {0}", grouplist);
            SqlCommand cmd = new SqlCommand(sql, cn);
            cmd.Parameters.Add("@dateCreated", SqlDbType.DateTime).Value = period;
            cn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<int> templates = new List<int>();
            while (reader.Read())
            {
                templates.Add((int)reader[0]);
            }
            return templates;
        }
    }

    public List<int> GetTemplatesIdsForPlatinumNews()
    {
        string key = CacheKey + "_TemplatesIdsForPlatinumNews";

        if (EnableCaching && Cache[key] != null)
        {
            return (List<int>)Cache[key];
        }
        List<int> templates = null;
        templates = GetTemplatesIdsForPlatinumNewsSQL();

        if (EnableCaching)
            CacheData(key, templates, CacheDuration);

        return templates;
    }

    private static sqlProductRepository _sqlproductInstance = null;

    public static sqlProductRepository SqlproductInstance
    {
        get
        {
            if(_sqlproductInstance==null)
                _sqlproductInstance=new sqlProductRepository();
            return _sqlproductInstance;
        }
    }
}