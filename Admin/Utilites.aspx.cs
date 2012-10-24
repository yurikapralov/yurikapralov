using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using echo.BLL;
using echo.BLL.Products;

public partial class Admin_Utilites : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnGenerateSiteMapFile_Click(object sender, EventArgs e)
    {
        using (CathegoryRepository lCathegoryRepository = new CathegoryRepository())
        {
            string xmlFile = Server.MapPath("~/GoogleSiteMap.xml");
            XmlTextWriter writer = new XmlTextWriter(xmlFile, Encoding.UTF8);

            writer.WriteStartDocument();
            writer.WriteStartElement("urlset");
            writer.WriteAttributeString("xmlns", "http://www.sitemaps.org/schemas/sitemap/0.9");
           
            using (GroupRepository lGroupRepository = new GroupRepository())
            {
                List<Group> groups = lGroupRepository.GetGroups();
                foreach (Group groupItem in groups)
                {
                    int groupId = groupItem.GroupID;
                    if (groupId == 3 || groupId == 4 || groupId == 6 || groupId == 7 || groupId == 8)
                    {
                        writer.WriteStartElement("url");
                        writer.WriteStartElement("loc");
                        writer.WriteString("http://www.echo-h.ru/Products.aspx?CatId=0&GroupID=" + groupId + "&All=1");
                        writer.WriteEndElement();
                        writer.WriteEndElement();
                    }

                    if (groupId == 1)
                    {
                        writer.WriteRaw(
                            @"
                            <url><loc>http://www.echo-h.ru</loc></url> 
                            <url><loc>http://www.echo-h.ru/Default.aspx</loc></url> 
                            <url><loc>http://www.echo-h.ru/About.aspx</loc></url>
                            <url><loc>http://www.echo-h.ru/Collaboration.aspx</loc></url>
                            <url><loc>http://www.echo-h.ru/Shipping.aspx</loc></url>
                            <url><loc>http://www.echo-h.ru/Stores.aspx</loc></url>
                            <url><loc>http://www.echo-h.ru/CustomShoes.aspx</loc></url>
                            <url><loc>http://www.echo-h.ru/Sizes.aspx</loc></url>");
                    }
                    List<Cathegory> cathegories = lCathegoryRepository.GetCathegoryByGroup(groupId);

                    foreach (Cathegory cathegory in cathegories)
                    {
                        if (cathegory.ActiveStatus)
                        {
                            writer.WriteStartElement("url");
                            writer.WriteStartElement("loc");
                            writer.WriteString("http://www.echo-h.ru/Products.aspx?CatID=" + cathegory.CatID + "&All=1");
                            writer.WriteEndElement();
                            writer.WriteEndElement();
                        }
                    }
                }


            }

            using (ProductRepository pRepository=new ProductRepository())
            {
                List<Product> products = pRepository.GetActiveProducts();
                foreach (Product product in products)
                {
                    writer.WriteStartElement("url");
                    writer.WriteStartElement("loc");
                    writer.WriteString("http://www.echo-h.ru/ProductItem.aspx?ProdId=" + product.ProdID );
                    writer.WriteEndElement();
                    writer.WriteEndElement();
                }
            }

            using (GroupRepository lGroupRepository = new GroupRepository())
            {
                List<Group> groups = lGroupRepository.GetGroups();
                foreach (Group groupItem in groups)
                {
                    int groupId = groupItem.GroupID;
                    if (groupId == 3 || groupId == 4 || groupId == 6 || groupId == 7 || groupId == 8)
                    {
                        writer.WriteStartElement("url");
                        writer.WriteStartElement("loc");
                        writer.WriteString("http://www.echo-h.ru/En/Products.aspx?CatId=0&GroupID=" + groupId + "&All=1");
                        writer.WriteEndElement();
                        writer.WriteEndElement();
                    }

                    if (groupId == 1)
                    {
                        writer.WriteRaw(
                            @"
                            <url><loc>http://www.echo-h.ru/En/Default.aspx</loc></url> 
                            <url><loc>http://www.echo-h.ru/En/About.aspx</loc></url>
                            <url><loc>http://www.echo-h.ru/En/Shipping.aspx</loc></url>
                            <url><loc>http://www.echo-h.ru/En/Stores.aspx</loc></url>");
                    }
                    List<Cathegory> cathegories = lCathegoryRepository.GetCathegoryByGroup(groupId);

                    foreach (Cathegory cathegory in cathegories)
                    {
                        if (cathegory.ActiveStatus)
                        {
                            writer.WriteStartElement("url");
                            writer.WriteStartElement("loc");
                            writer.WriteString("http://www.echo-h.ru/En/Products.aspx?CatID=" + cathegory.CatID + "&All=1");
                            writer.WriteEndElement();
                            writer.WriteEndElement();
                        }
                    }
                }


            }
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
            writer.Close();
        }
    }

    protected void GeneratePlatinumMenu()
    {
        using (CathegoryRepository lCathegoryRepository = new CathegoryRepository())
        {
            string xmlFile = Server.MapPath("~/PlatinumSiteMap.xml");
            XmlTextWriter writer = new XmlTextWriter(xmlFile, Encoding.UTF8);

            writer.WriteStartDocument();
            writer.WriteStartElement("urlset");
            writer.WriteAttributeString("xmlns", "http://www.sitemaps.org/schemas/sitemap/0.9");

            writer.WriteRaw(
                            @"
                            <url><loc>http://www.platinumshoes.ru</loc></url> 
                            <url><loc>http://www.platinumshoes.ru/Default.aspx</loc></url> 
                            <url><loc>http://www.platinumshoes.ru/Stores.aspx</loc></url>
                            <url><loc>http://www.platinumshoes.ru/Delivery.aspx</loc></url>
                            <url><loc>http://www.platinumshoes.ru/CustomShoes.aspx</loc></url>");

            using (GroupRepository lGroupRepository = new GroupRepository())
            {
                List<Group> groups = lGroupRepository.GetGroups();
                foreach (Group groupItem in groups)
                {
                    int groupId = groupItem.GroupID;

                    if ((groupId == Helpers.Settings.PlatinumProduct.PlatinumGroupId)||(Helpers.Settings.PlatinumExtraProduct.PlatinumGroupId !=null  && (Array.IndexOf(Helpers.Settings.PlatinumExtraProduct.PlatinumGroupId, groupId) > -1)))
                    {
                        List<Cathegory> cathegories = lCathegoryRepository.GetCathegoryByGroup(groupId);

                        foreach (Cathegory cathegory in cathegories)
                        {
                            if (cathegory.ActiveStatus)
                            {
                                writer.WriteStartElement("url");
                                writer.WriteStartElement("loc");
                                writer.WriteString("http://www.platinumshoes.ru/Products.aspx?CatID=" + cathegory.CatID);
                                writer.WriteEndElement();
                                writer.WriteEndElement();

                                using (ProductRepository lProductRepository=new ProductRepository())
                                {
                                    List<Product> products =
                                        lProductRepository.GetActiveProductsByCategory(cathegory.CatID);
                                    foreach (Product product in products)
                                    {
                                        writer.WriteStartElement("url");
                                        writer.WriteStartElement("loc");
                                        writer.WriteString("http://www.platinumshoes.ru/ProductItem.aspx?ProdId=" + product.ProdID);
                                        writer.WriteEndElement();
                                        writer.WriteEndElement();
                                    }
                                }
                            }
                        }
                    }
                }
            }

            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
            writer.Close();
        }
    }
    protected void btnPlatinum_Click(object sender, EventArgs e)
    {
        GeneratePlatinumMenu();
    }
}
