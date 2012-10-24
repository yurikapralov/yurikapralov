using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using echo.BLL.Products;


namespace echo.BLL
{
    /// <summary>
    /// Вспомогательный класс преобразующий меню из XML документа в ссылочный формат
    /// </summary>
    public static class XMLMenu
    {
        public static string GetRusMenu(XmlNodeList nodeList, int level)
        {
            StringBuilder str = new StringBuilder("");

            foreach (XmlNode node in nodeList)
            {
                if (node.NodeType == XmlNodeType.Element)
                {
                    if (((XmlElement)node).HasAttribute("type"))
                    {
                        if (node.Attributes["type"].Value == "MainRuMenu")
                            str.Append(GetSubRusMenu(node.ChildNodes, level + 1));
                    }
                }
                if (node.HasChildNodes)
                    str.Append(GetRusMenu(node.ChildNodes, level + 1));
            }
            return str.ToString();
        }
        
        // метод проверяющий наличие аттрибутов platinum и необходимость отображать их в меню главного сайта
        private static  bool PlatinumCheck(XmlNode node)
        {
             if (((XmlElement) node).HasAttribute("type"))
             {
                 if(node.Attributes["type"].Value=="platinum" && !Helpers.Settings.PlatinumProduct.ShowInEcho)
                     return false;
                 if (node.Attributes["type"].Value == "platinumnews" && !Helpers.Settings.PlatinumExtraProduct.ShowInEcho)
                     return false;
             }
            return true;
        }

        private static string GetSubRusMenu(XmlNodeList nodeList, int level)
        {
            StringBuilder str = new StringBuilder("");

            foreach (XmlNode node in nodeList)
            {
                if (node.NodeType == XmlNodeType.Element)
                {
                    if (PlatinumCheck(node))
                    
                    {
                        if (((XmlElement) node).HasAttribute("title") && !((XmlElement) node).HasAttribute("url"))
                        {
                            if (node.HasChildNodes)
                                str.Append("<br/><h3>" + node.Attributes["title"].Value + "</h3>");
                        }
                        else if (((XmlElement) node).HasAttribute("title") && ((XmlElement) node).HasAttribute("url"))
                        {
                            if (node.Attributes["url"].Value.IndexOf("GroupID") != -1)
                                str.Append("<br/><h3>");
                            /*if (node.Attributes["url"].Value == "mailto:info@echo-h.ru")
                                str.Append("<a href='");
                            else
                                str.Append("<a href='..");*/
                            if (((XmlElement)node).HasAttribute("marked"))
                                str.Append("<a href='" + node.Attributes["url"].Value + "'><span class='marked'>" +
                                       node.Attributes["title"].Value + "</span></a>");
                            else 
                                str.Append("<a href='" + node.Attributes["url"].Value + "'>" +
                                       node.Attributes["title"].Value + "</a>");



                            if (node.Attributes["url"].Value.IndexOf("GroupID") != -1)
                                str.Append("</h3>");
                            else
                                str.Append("<br/>");
                        }
                        if (node.HasChildNodes)
                            str.Append(GetSubRusMenu(node.ChildNodes, level + 1));
                    }
                    else
                    {
                        if (((XmlElement)node).HasAttribute("title") && !((XmlElement)node).HasAttribute("url"))
                        {
                            if (node.HasChildNodes)
                                str.Append("<br/><h3>" + node.Attributes["title"].Value + "</h3>");
                        }
                        else if (((XmlElement)node).HasAttribute("title") && ((XmlElement)node).HasAttribute("url"))
                        {
                            if (node.Attributes["url"].Value.IndexOf("http://PlatinumShoes.ru") != -1)
                            {
                                str.Append("<br/><h3>");
                                if (((XmlElement)node).HasAttribute("marked"))
                                    str.Append("<a href='http://Platinumshoes.ru/'><span class='marked'>" +
                                           node.Attributes["title"].Value + "</span></a>");
                                else
                                    str.Append("<a href='http://Platinumshoes.ru/'>" +
                                           node.Attributes["title"].Value + "</a>");
                                 str.Append("</h3>");
                            }
                            else
                            {
                                if (((XmlElement)node).HasAttribute("marked"))
                                    str.Append("<a href='http://Platinumshoes.ru/" + node.Attributes["url"].Value + "'><span class='marked'>" +
                                           node.Attributes["title"].Value + "</span></a>");
                                else
                                    str.Append("<a href='http://Platinumshoes.ru/" + node.Attributes["url"].Value + "'>" +
                                           node.Attributes["title"].Value + "</a>");
                                str.Append("<br/>");
                            }

                                
                        }
                        if (node.HasChildNodes)
                            str.Append(GetSubRusMenu(node.ChildNodes, level + 1));
                    }
                }
            }
            return str.ToString();
        }

        public static string GetV2Menu(XmlNodeList nodeList, int level)
        {
            StringBuilder str = new StringBuilder("");

            foreach (XmlNode node in nodeList)
            {
                if (node.NodeType == XmlNodeType.Element)
                {
                    if (((XmlElement)node).HasAttribute("type"))
                    {
                        if (node.Attributes["type"].Value == "MainRuMenu")
                            str.Append(GetSubV2Menu(node.ChildNodes, level + 1));
                    }
                }
                if (node.HasChildNodes)
                    str.Append(GetSubV2Menu(node.ChildNodes, level + 1));
            }
            return str.ToString();
        }

        private static string GetSubV2Menu(XmlNodeList nodeList, int level)
        {
            StringBuilder str = new StringBuilder("");

            foreach (XmlNode node in nodeList)
            {
                if (node.NodeType == XmlNodeType.Element)
                {
                    if (((XmlElement)node).HasAttribute("title") && !((XmlElement)node).HasAttribute("url"))
                    {
                        if (node.Attributes["title"].Value=="Информация")
                            return "";
                        if (node.HasChildNodes)
                            str.Append("<br/><h3>" + node.Attributes["title"].Value + "</h3>");
                    }
                    else if (((XmlElement)node).HasAttribute("title") && ((XmlElement)node).HasAttribute("url"))
                    {
                        if (node.Attributes["url"].Value.IndexOf("GroupID") != -1)
                            str.Append("<br/><h3>");
                        /*if (node.Attributes["url"].Value == "mailto:info@echo-h.ru")
                            str.Append("<a href='");
                        else
                            str.Append("<a href='..");*/

                        str.Append("<a href='" + node.Attributes["url"].Value + "'>" + node.Attributes["title"].Value + "</a>");

                        if (node.Attributes["url"].Value.IndexOf("GroupID") != -1)
                            str.Append("</h3>");
                        else
                            str.Append("<br/>");
                    }
                    if (node.HasChildNodes)
                        str.Append(GetSubRusMenu(node.ChildNodes, level + 1));
                }
            }
            return str.ToString();
        }

        public static string GetPlatinumMenu(XmlNodeList nodeList, int level,bool isNews)
        {
            StringBuilder str = new StringBuilder("");

            string type = isNews ? "platinumextra" : "platinum";

            foreach (XmlNode node in nodeList)
            {
                if (node.NodeType == XmlNodeType.Element)
                {
                    if (((XmlElement)node).HasAttribute("type"))
                    {
                        if (node.Attributes["type"].Value == type)
                        {
                            if (type == "platinumextra")
                            {
                                string extratitle = node.Attributes["title"].Value;
                                string extraurl = ((XmlElement)node).HasAttribute("url") ? "href='"+node.Attributes["url"].Value+"'" : "";
                                str.Append(string.Format("<li><a {1}>{0}</a><ul>",extratitle,extraurl));
                            }
                            str.Append(GetSubPlatinumMenu(node.ChildNodes, level + 1));
                            if (type == "platinumextra")
                            {
                                str.Append("</ul></li>");
                            }
                        }
                    }
                }
                if (node.HasChildNodes)
                    str.Append(GetPlatinumMenu(node.ChildNodes, level + 1,isNews));
            }
            return str.ToString();
        }

        private static string GetSubPlatinumMenu(XmlNodeList nodeList, int level)
        {
            StringBuilder str = new StringBuilder("");

            foreach (XmlNode node in nodeList)
            {
                if (node.NodeType == XmlNodeType.Element)
                {
                    
                    if (((XmlElement)node).HasAttribute("title") && ((XmlElement)node).HasAttribute("url"))
                    {
                        str.Append("<li><a href='" + node.Attributes["url"].Value + "'>" + node.Attributes["title"].Value + "</a></li>");

                       
                    }
                }
            }
            return str.ToString();
        }

        public static string GetPlatinumNewMenu()
        {

            List<int> templateIds = sqlProductRepository.SqlproductInstance.GetTemplatesIdsForPlatinumNews();
                if(templateIds==null)
                    return "";
                string menu="<li><a href='#' style='border-left-style:none;'>Новинки</a><ul>";
                foreach(int templateId in templateIds)
                {
                    using(TemplateRepository trep=new TemplateRepository())
                    {
                        string templaName=trep.GetTemplateById(templateId).TempleName;
                        menu+=string.Format("<li><a href='NewProducts.aspx?ptype={0}'>{1} - Новинки </a></li>",templateId,templaName);
                    }
                }
                menu+="</ul></li>";
                return menu;
        }

        ///
        /// Функции для меню сайта с новым дизайном (старые потом можно будет убрать)

        public static string GetRusMenu2(XmlNodeList nodeList, int level)
        {
            StringBuilder str = new StringBuilder("");

            foreach (XmlNode node in nodeList)
            {
                if (node.NodeType == XmlNodeType.Element)
                {
                    if (((XmlElement)node).HasAttribute("type"))
                    {
                        if (node.Attributes["type"].Value == "MainRuMenu")
                            str.Append(GetSubRusMenu2(node.ChildNodes, level + 1));
                    }
                }
                if (node.HasChildNodes)
                    str.Append(GetRusMenu2(node.ChildNodes, level + 1));
            }
            return str.ToString();
        }

        private static string GetSubRusMenu2(XmlNodeList nodeList, int level)
        {
            StringBuilder str = new StringBuilder("");
            string str_help = "";
            string str_help1 = "";

            foreach (XmlNode node in nodeList)
            {
                if (node.NodeType == XmlNodeType.Element)
                {
                    if (PlatinumCheck(node))
                    {
                        if (((XmlElement)node).HasAttribute("title") && !((XmlElement)node).HasAttribute("url"))
                        {
                            if (node.HasChildNodes)
                                str.Append("<li><h3>" + node.Attributes["title"].Value + "</h3></li>");
                        }
                        else if (((XmlElement)node).HasAttribute("title") && ((XmlElement)node).HasAttribute("url"))
                        {
                            str_help = (node.Attributes["url"].Value.IndexOf("GroupID") != -1) ? "<h3>" : "";
                            str_help1 = (node.Attributes["url"].Value.IndexOf("GroupID") != -1) ? "</h3>" : "";
                            
                            if (((XmlElement)node).HasAttribute("marked"))
                                str.Append("<li><a href='" + node.Attributes["url"].Value + "'>"+str_help+"<span class='marked'>" +
                                       node.Attributes["title"].Value + "</span>"+str_help1+"</a></li>");
                            else
                                str.Append("<li><a href='" + node.Attributes["url"].Value + "'>" + str_help +
                                       node.Attributes["title"].Value + str_help1+ "</a></li>");

                        }
                        if (node.HasChildNodes)
                            str.Append("<div class='sub_menu'>" + GetSubRusMenu2(node.ChildNodes, level + 1) + "</div>");
                    }
                    else
                    {
                        if (((XmlElement)node).HasAttribute("title") && !((XmlElement)node).HasAttribute("url"))
                        {
                            if (node.HasChildNodes)
                                str.Append("<li><h3>" + node.Attributes["title"].Value + "</h3></li>");
                        }
                        else if (((XmlElement)node).HasAttribute("title") && ((XmlElement)node).HasAttribute("url"))
                        {
                            if (node.Attributes["url"].Value.IndexOf("http://PlatinumShoes.ru") != -1)
                            {
                                str.Append("<li>");
                                if (((XmlElement)node).HasAttribute("marked"))
                                    str.Append("<a href='http://Platinumshoes.ru/'><h3><span class='marked'>" +
                                           node.Attributes["title"].Value + "</span></h3></a>");
                                else
                                    str.Append("<a href='http://Platinumshoes.ru/'><h3>" +
                                           node.Attributes["title"].Value + "</h3></a>");
                                str.Append("</li>");
                            }
                            else
                            {
                                if (((XmlElement)node).HasAttribute("marked"))
                                    str.Append("<li><a href='http://Platinumshoes.ru/" + node.Attributes["url"].Value + "'><span class='marked'>" +
                                           node.Attributes["title"].Value + "</span></a></li>");
                                else
                                    str.Append("<li><a href='http://Platinumshoes.ru/" + node.Attributes["url"].Value + "'>" +
                                           node.Attributes["title"].Value + "</a></li>");
                            }


                        }
                        if (node.HasChildNodes)
                            str.Append("<div class='sub_menu'>"+GetSubRusMenu2(node.ChildNodes, level + 1)+"</div>");
                    }
                }
            }
            return str.ToString();
        }
       
    }
}