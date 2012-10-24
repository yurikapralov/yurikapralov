using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;

/// <summary>
/// Класс дающий доступ к настройкам конфигурации сайта
/// </summary>

namespace echo.BLL
{
    public class EchoSection:ConfigurationSection
    {
        [ConfigurationProperty("defaultConnectionStringName", DefaultValue = "echoConnectionString")]
        public string DefaultConnectionStringName
        {
            get { return (string)base["defaultConnectionStringName"]; }
            set { base["defaultConnectionStringName"] = value; }
        }

        public string ConnectionString
        {
            get
            {
                string connStringName = Helpers.Settings.DefaultConnectionStringName;
                return WebConfigurationManager.ConnectionStrings[connStringName].ConnectionString;
            }
        }
        [ConfigurationProperty("defaultCacheDuration", DefaultValue = "600")]
        public int DefaultCacheDuration
        {
            get { return (int)base["defaultCacheDuration"]; }
            set { base["defaultCacheDuration"] = value; }
        }
        /// <summary>
        /// Элемент, описывающий параметры  почтовых сообщений
        /// </summary>
        [ConfigurationProperty("contactForm", IsRequired = true)]
        public ContactFormElement ContactForm
        {
            get { return (ContactFormElement)base["contactForm"]; }
        }
        
        /// <summary>
        /// Элемент, описывающий параметры заказов
        /// </summary>
        [ConfigurationProperty("store")]
        public StoreElement Store
        {
            get { return (StoreElement)base["store"]; }
        }

        /// <summary>
        /// Элемент, описывающий параметры нахождения  основной продукции platinum
        /// </summary>
        [ConfigurationProperty("platinumproduct")]
        public PlatinumMainProductElement PlatinumProduct
        {
            get { return (PlatinumMainProductElement)base["platinumproduct"]; }
        }


        /// <summary>
        /// Элемент, описывающий параметры нахождения дополнительных групп для platinum
        /// </summary>
        [ConfigurationProperty("platinumextraproduct")]
        public PlatinumExtraProductElement PlatinumExtraProduct
        {
            get { return (PlatinumExtraProductElement)base["platinumextraproduct"]; }
        }
    }

    /// <summary>
    /// Класс описывающий параметры  почтовых сообщений
    /// </summary>
    public class ContactFormElement : ConfigurationElement
    {
        [ConfigurationProperty("mailSubject", DefaultValue = "Интернет - магазин Echo Of Hollywood")]
        public string MailSubject
        {
            get { return (string)base["mailSubject"]; }
            set { base["mailSubject"] = value; }
        }
        [ConfigurationProperty("mailFrom", IsRequired = true)]
        public string MailFrom
        {
            get { return (string)base["mailFrom"]; }
            set { base["mailFrom"] = value; }
        }
        [ConfigurationProperty("mailCopy",DefaultValue="")]
        public string MailCopy
        {
            get { return (string)base["mailCopy"]; }
            set { base["mailCopy"] = value; }
        }

    }
    /// <summary>
    /// Класс описывающий параметры заказов
    /// </summary>
    public class StoreElement : ConfigurationElement
    {
        [ConfigurationProperty("defaultOrderListInterval", DefaultValue = "30")]
        public int DefaultOrderListInterval
        {
            get { return (int)base["defaultOrderListInterval"]; }
            set { base["defaultOrderListInterval"] = value; }
        }

        [ConfigurationProperty("defaultTitle", DefaultValue = "")]
        public string DefaultTitle
        {
            get { return (string)base["defaultTitle"]; }
            set { base["defaultTitle"] = value; }
        }
    }

    /// <summary>
    /// Класс описывающий параметры нахождения основной продукции platinum
    /// </summary>
    
    public class PlatinumMainProductElement : ConfigurationElement
    {
        [ConfigurationProperty("platinumGroupId",IsRequired=true)]
        public int PlatinumGroupId
        {
            get { return (int) base["platinumGroupId"]; }
            set { base["platinumGroupId"] = value; }
        }

        [ConfigurationProperty("showInEcho",DefaultValue="true")]
        public bool ShowInEcho
        {
            get { return (bool) base["showInEcho"]; }
            set { base["showInEcho"] = value; }
        }
    }


    /// <summary>
    /// Класс описывающий параметры нахождения дополнительных групп для platinum
    /// </summary>

    public class PlatinumExtraProductElement : ConfigurationElement
    {
        [ConfigurationProperty("platinumGroupId", IsRequired = true)]
        public string strPlatinumGroupId
        {
            get { return (string)base["platinumGroupId"]; }
            set { base["platinumGroupId"] = value; }
        }

        public int[] PlatinumGroupId
        {
            get
            {
                if (this.strPlatinumGroupId.Length == 0)
                    return null;
                string[] strAr = (this.strPlatinumGroupId).Split(',');
                int[] retAr = new int[strAr.Length];
                for (int i = 0; i < strAr.Length; i++)
                {
                    retAr[i] = int.Parse(strAr[i]);
                }
                return retAr;
            }
            set
            {
                string[] strAr = new string[value.Length];
                for (int i = 0; i < value.Length; i++)
                {
                    strAr[i] = value[i].ToString();
                }
                this.strPlatinumGroupId = String.Join(",", strAr);
            }
        }

        [ConfigurationProperty("showInEcho", DefaultValue = "true")]
        public bool ShowInEcho
        {
            get { return (bool)base["showInEcho"]; }
            set { base["showInEcho"] = value; }
        }
    }
}

