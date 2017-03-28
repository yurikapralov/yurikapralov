using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using echo.BLL;
using echo.BLL.Products;

/// <summary>
/// Сводное описание для MenuCreator
/// </summary>
public class MenuCreator
{
    public static void GenerateRusXmlMenu()
    {
        using (CathegoryRepository lCathegoryRepository = new CathegoryRepository())
        {
            string xmlFile = HttpContext.Current.Server.MapPath("~/SiteMap.xml");
            XmlTextWriter writer = new XmlTextWriter(xmlFile, Encoding.UTF8);

            writer.WriteStartDocument();
            writer.WriteStartElement("siteMap");
            writer.WriteStartElement("siteMapNode");
            writer.WriteStartElement("siteMapNode");
            writer.WriteAttributeString("url", "Default.aspx");
            writer.WriteAttributeString("type", "MainRuMenu");
            using (GroupRepository lGroupRepository = new GroupRepository())
            {
                List<Group> groups = lGroupRepository.GetGroups();
                foreach (Group groupItem in groups)
                {
                    int groupId = groupItem.GroupID;
                    if (groupId != 1 && groupItem.Avaliable)
                    {
                        writer.WriteStartElement("siteMapNode");
                        writer.WriteAttributeString("title", groupItem.GroupNameRus);
                        if (groupItem.HaveGeneralLink)
                        {
                            writer.WriteAttributeString("url", "Products.aspx?CatId=0&GroupID=" + groupId);
                        }

                        if (groupId == Helpers.Settings.PlatinumProduct.PlatinumGroupId)
                        {
                            writer.WriteAttributeString("type", "platinum");
                            writer.WriteAttributeString("url", "http://PlatinumShoes.ru");
                        }

                        if (Helpers.Settings.PlatinumExtraProduct.PlatinumGroupId != null &&
                            (Array.IndexOf(Helpers.Settings.PlatinumExtraProduct.PlatinumGroupId, groupId) > -1))
                        {
                            writer.WriteAttributeString("type", "platinumextra");
                        }
                        /*
                        if (groupId == 1)
                        {
                            writer.WriteRaw(
                                @"<siteMapNode title='Главная' url='Default.aspx'/>
                                <siteMapNode title='О Компании' url='About.aspx'/>
                                <siteMapNode title='Сотрудничество' url='Collaboration.aspx'/>
                                <siteMapNode title='Способы доставки' url='Shipping.aspx'/>
                                <siteMapNode title='Наши магазины' url='Stores.aspx'/>
                                <siteMapNode title='Обувь на заказ' url='CustomShoes.aspx'/>
                                <siteMapNode title='Как снимать мерки для сапог' url='Sizes.aspx'/>
                                <siteMapNode title='Написать нам' url='mailto:info@echo-h.ru'/>");
                        }*/
                        List<Cathegory> cathegories = lCathegoryRepository.GetCathegoryByGroup(groupId);

                        foreach (Cathegory cathegory in cathegories)
                        {
                            if (cathegory.ActiveStatus)
                            {
                                writer.WriteStartElement("siteMapNode");
                                writer.WriteAttributeString("title", cathegory.CatNameRus);
                                writer.WriteAttributeString("url", "Products.aspx?CatID=" + cathegory.CatID);
                                if (groupId == Helpers.Settings.PlatinumProduct.PlatinumGroupId)
                                {
                                    writer.WriteAttributeString("type", "platinum");
                                }
                                if (cathegory.Marked)
                                    writer.WriteAttributeString("marked", "1");
                                writer.WriteEndElement();
                            }
                        }
                        writer.WriteEndElement();
                    }
                }
                writer.WriteEndElement();


            }
            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
            writer.Close();
        }


    }

    public static void GenerateSiteMap()
    {
        string xmlFile = HttpContext.Current.Server.MapPath("~/SiteMap1.xml");

        using (CathegoryRepository lCathegoryRepository = new CathegoryRepository())
        using(XmlTextWriter writer = new XmlTextWriter(xmlFile, Encoding.UTF8))
        {
            writer.WriteStartDocument();
            writer.WriteStartElement("urlset");
            writer.WriteAttributeString("xmlns", "http://www.sitemaps.org/schemas/sitemap/0.9");
            WriteUrl(writer, "About.aspx");
            WriteUrl(writer, "Stores.aspx");
            WriteUrl(writer, "Shipping.aspx");
            WriteUrl(writer, "PayMethods.aspx");
            WriteUrl(writer, "CreditInfo.aspx");
            using (GroupRepository lGroupRepository = new GroupRepository())
            {
                List<Group> groups = lGroupRepository.GetGroups();
                foreach (Group groupItem in groups)
                {
                    int groupId = groupItem.GroupID;
                    if (groupId != 1 && groupItem.Avaliable)
                    {
                        WriteUrl(writer, "Products.aspx?CatId=0&amp;GroupID=" + groupId + "&amp;all=1");
                    }

                    List<Cathegory> cathegories = lCathegoryRepository.GetCathegoryByGroup(groupId);

                    foreach (Cathegory cathegory in cathegories)
                    {
                        if (cathegory.ActiveStatus)
                        {
                            WriteUrl(writer, "Products.aspx?CatID=" + cathegory.CatID+ "&amp;all=1");
                        }
                    }
                }
            }

            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
        }
    }

    private static void WriteUrl(XmlTextWriter writer, string url)
    {
        writer.WriteRaw(string.Format(@"
            <url>
              <loc>http://echo-h.ru/{0}</loc>
              <changefreq>weekly</changefreq>
           </url>
        ", url));
    }

    public static void GenerateEngXmlMenu()
    {
        using (CathegoryRepository lCathegoryRepository = new CathegoryRepository())
        {
            string xmlFile = HttpContext.Current.Server.MapPath("~/SiteMapEnglish.xml");
            XmlTextWriter writer = new XmlTextWriter(xmlFile, Encoding.UTF8);

            writer.WriteStartDocument();
            writer.WriteStartElement("siteMap");
            writer.WriteStartElement("siteMapNode");
            writer.WriteStartElement("siteMapNode");
            writer.WriteAttributeString("url", "Default.aspx");
            writer.WriteAttributeString("type", "MainRuMenu");
            using (GroupRepository lGroupRepository = new GroupRepository())
            {
                List<Group> groups = lGroupRepository.GetGroups();
                foreach (Group groupItem in groups)
                {
                    if (groupItem.AvaliableInEngilsh)
                    {
                        writer.WriteStartElement("siteMapNode");
                        writer.WriteAttributeString("title", groupItem.GroupNameEng);
                        int groupId = groupItem.GroupID;
                        if (groupItem.HaveGeneralLink)
                        {
                            writer.WriteAttributeString("url", "Products.aspx?CatId=0&GroupID=" + groupId);
                        }

                        if (groupId == 1)
                        {
                            writer.WriteRaw(
                                @"<siteMapNode title='Main Page' url='Default.aspx'/>
                            <siteMapNode title='About Company' url='About.aspx'/>
                            <siteMapNode title='Delivery' url='Shipping.aspx'/>
                            <siteMapNode title='Our Stores' url='Stores.aspx'/>");
                        }
                        List<Cathegory> cathegories = lCathegoryRepository.GetCathegoryByGroup(groupId);

                        foreach (Cathegory cathegory in cathegories)
                        {
                            if (cathegory.ActiveStatus)
                            {
                                writer.WriteStartElement("siteMapNode");
                                writer.WriteAttributeString("title", cathegory.CatNameEng);
                                writer.WriteAttributeString("url", "Products.aspx?CatID=" + cathegory.CatID);
                                writer.WriteEndElement();
                            }
                        }
                        writer.WriteEndElement();
                    }
                }
                writer.WriteEndElement();


            }
            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
            writer.Close();
        }


    }
}