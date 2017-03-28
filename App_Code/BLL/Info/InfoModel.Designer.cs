﻿//------------------------------------------------------------------------------
// <auto-generated>
//    Этот код был создан из шаблона.
//
//    Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//    Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Data.EntityClient;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;

[assembly: EdmSchemaAttribute()]
namespace echo.BLL.Info
{
    #region Контексты
    
    /// <summary>
    /// Нет доступной документации по метаданным.
    /// </summary>
    public partial class InfoEntities : ObjectContext
    {
        #region Конструкторы
    
        /// <summary>
        /// Инициализирует новый объект InfoEntities, используя строку соединения из раздела "InfoEntities" файла конфигурации приложения.
        /// </summary>
        public InfoEntities() : base("name=InfoEntities", "InfoEntities")
        {
            OnContextCreated();
        }
    
        /// <summary>
        /// Инициализация нового объекта InfoEntities.
        /// </summary>
        public InfoEntities(string connectionString) : base(connectionString, "InfoEntities")
        {
            OnContextCreated();
        }
    
        /// <summary>
        /// Инициализация нового объекта InfoEntities.
        /// </summary>
        public InfoEntities(EntityConnection connection) : base(connection, "InfoEntities")
        {
            OnContextCreated();
        }
    
        #endregion
    
        #region Разделяемые методы
    
        partial void OnContextCreated();
    
        #endregion
    
        #region Свойства ObjectSet
    
        /// <summary>
        /// Нет доступной документации по метаданным.
        /// </summary>
        public ObjectSet<Information> Informations
        {
            get
            {
                if ((_Informations == null))
                {
                    _Informations = base.CreateObjectSet<Information>("Informations");
                }
                return _Informations;
            }
        }
        private ObjectSet<Information> _Informations;
    
        /// <summary>
        /// Нет доступной документации по метаданным.
        /// </summary>
        public ObjectSet<Rate> Rates
        {
            get
            {
                if ((_Rates == null))
                {
                    _Rates = base.CreateObjectSet<Rate>("Rates");
                }
                return _Rates;
            }
        }
        private ObjectSet<Rate> _Rates;
    
        /// <summary>
        /// Нет доступной документации по метаданным.
        /// </summary>
        public ObjectSet<MoscowDelivery> MoscowDeliveris
        {
            get
            {
                if ((_MoscowDeliveris == null))
                {
                    _MoscowDeliveris = base.CreateObjectSet<MoscowDelivery>("MoscowDeliveris");
                }
                return _MoscowDeliveris;
            }
        }
        private ObjectSet<MoscowDelivery> _MoscowDeliveris;
    
        /// <summary>
        /// Нет доступной документации по метаданным.
        /// </summary>
        public ObjectSet<echoNews> echoNews
        {
            get
            {
                if ((_echoNews == null))
                {
                    _echoNews = base.CreateObjectSet<echoNews>("echoNews");
                }
                return _echoNews;
            }
        }
        private ObjectSet<echoNews> _echoNews;

        #endregion

        #region Методы AddTo
    
        /// <summary>
        /// Устаревший метод для добавления новых объектов в набор EntitySet Informations. Взамен можно использовать метод .Add связанного свойства ObjectSet&lt;T&gt;.
        /// </summary>
        public void AddToInformations(Information information)
        {
            base.AddObject("Informations", information);
        }
    
        /// <summary>
        /// Устаревший метод для добавления новых объектов в набор EntitySet Rates. Взамен можно использовать метод .Add связанного свойства ObjectSet&lt;T&gt;.
        /// </summary>
        public void AddToRates(Rate rate)
        {
            base.AddObject("Rates", rate);
        }
    
        /// <summary>
        /// Устаревший метод для добавления новых объектов в набор EntitySet MoscowDeliveris. Взамен можно использовать метод .Add связанного свойства ObjectSet&lt;T&gt;.
        /// </summary>
        public void AddToMoscowDeliveris(MoscowDelivery moscowDelivery)
        {
            base.AddObject("MoscowDeliveris", moscowDelivery);
        }
    
        /// <summary>
        /// Устаревший метод для добавления новых объектов в набор EntitySet echoNews. Взамен можно использовать метод .Add связанного свойства ObjectSet&lt;T&gt;.
        /// </summary>
        public void AddToechoNews(echoNews echoNews)
        {
            base.AddObject("echoNews", echoNews);
        }

        #endregion

    }

    #endregion

    #region Сущности
    
    /// <summary>
    /// Нет доступной документации по метаданным.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="InfoModel", Name="echoNews")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class echoNews : EntityObject
    {
        #region Фабричный метод
    
        /// <summary>
        /// Создание нового объекта echoNews.
        /// </summary>
        /// <param name="id">Исходное значение свойства Id.</param>
        /// <param name="newsDate">Исходное значение свойства NewsDate.</param>
        /// <param name="avail">Исходное значение свойства avail.</param>
        public static echoNews CreateechoNews(global::System.Int32 id, global::System.DateTime newsDate, global::System.Boolean avail)
        {
            echoNews echoNews = new echoNews();
            echoNews.Id = id;
            echoNews.NewsDate = newsDate;
            echoNews.avail = avail;
            return echoNews;
        }

        #endregion

        #region Свойства-примитивы
    
        /// <summary>
        /// Нет доступной документации по метаданным.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 Id
        {
            get
            {
                return _Id;
            }
            set
            {
                if (_Id != value)
                {
                    OnIdChanging(value);
                    ReportPropertyChanging("Id");
                    _Id = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("Id");
                    OnIdChanged();
                }
            }
        }
        private global::System.Int32 _Id;
        partial void OnIdChanging(global::System.Int32 value);
        partial void OnIdChanged();
    
        /// <summary>
        /// Нет доступной документации по метаданным.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.DateTime NewsDate
        {
            get
            {
                return _NewsDate;
            }
            set
            {
                OnNewsDateChanging(value);
                ReportPropertyChanging("NewsDate");
                _NewsDate = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("NewsDate");
                OnNewsDateChanged();
            }
        }
        private global::System.DateTime _NewsDate;
        partial void OnNewsDateChanging(global::System.DateTime value);
        partial void OnNewsDateChanged();
    
        /// <summary>
        /// Нет доступной документации по метаданным.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Header
        {
            get
            {
                return _Header;
            }
            set
            {
                OnHeaderChanging(value);
                ReportPropertyChanging("Header");
                _Header = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Header");
                OnHeaderChanged();
            }
        }
        private global::System.String _Header;
        partial void OnHeaderChanging(global::System.String value);
        partial void OnHeaderChanged();
    
        /// <summary>
        /// Нет доступной документации по метаданным.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Body
        {
            get
            {
                return _Body;
            }
            set
            {
                OnBodyChanging(value);
                ReportPropertyChanging("Body");
                _Body = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Body");
                OnBodyChanged();
            }
        }
        private global::System.String _Body;
        partial void OnBodyChanging(global::System.String value);
        partial void OnBodyChanged();
    
        /// <summary>
        /// Нет доступной документации по метаданным.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Boolean avail
        {
            get
            {
                return _avail;
            }
            set
            {
                OnavailChanging(value);
                ReportPropertyChanging("avail");
                _avail = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("avail");
                OnavailChanged();
            }
        }
        private global::System.Boolean _avail;
        partial void OnavailChanging(global::System.Boolean value);
        partial void OnavailChanged();

        #endregion

    
    }
    
    /// <summary>
    /// Нет доступной документации по метаданным.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="InfoModel", Name="Information")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Information : EntityObject
    {
        #region Фабричный метод
    
        /// <summary>
        /// Создание нового объекта Information.
        /// </summary>
        /// <param name="id">Исходное значение свойства Id.</param>
        public static Information CreateInformation(global::System.Int32 id)
        {
            Information information = new Information();
            information.Id = id;
            return information;
        }

        #endregion

        #region Свойства-примитивы
    
        /// <summary>
        /// Нет доступной документации по метаданным.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 Id
        {
            get
            {
                return _Id;
            }
            set
            {
                if (_Id != value)
                {
                    OnIdChanging(value);
                    ReportPropertyChanging("Id");
                    _Id = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("Id");
                    OnIdChanged();
                }
            }
        }
        private global::System.Int32 _Id;
        partial void OnIdChanging(global::System.Int32 value);
        partial void OnIdChanged();
    
        /// <summary>
        /// Нет доступной документации по метаданным.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String News
        {
            get
            {
                return _News;
            }
            set
            {
                OnNewsChanging(value);
                ReportPropertyChanging("News");
                _News = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("News");
                OnNewsChanged();
            }
        }
        private global::System.String _News;
        partial void OnNewsChanging(global::System.String value);
        partial void OnNewsChanged();

        #endregion

    
    }
    
    /// <summary>
    /// Нет доступной документации по метаданным.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="InfoModel", Name="MoscowDelivery")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class MoscowDelivery : EntityObject
    {
        #region Фабричный метод
    
        /// <summary>
        /// Создание нового объекта MoscowDelivery.
        /// </summary>
        /// <param name="id">Исходное значение свойства Id.</param>
        /// <param name="price">Исходное значение свойства Price.</param>
        public static MoscowDelivery CreateMoscowDelivery(global::System.Int32 id, global::System.Decimal price)
        {
            MoscowDelivery moscowDelivery = new MoscowDelivery();
            moscowDelivery.Id = id;
            moscowDelivery.Price = price;
            return moscowDelivery;
        }

        #endregion

        #region Свойства-примитивы
    
        /// <summary>
        /// Нет доступной документации по метаданным.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 Id
        {
            get
            {
                return _Id;
            }
            set
            {
                if (_Id != value)
                {
                    OnIdChanging(value);
                    ReportPropertyChanging("Id");
                    _Id = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("Id");
                    OnIdChanged();
                }
            }
        }
        private global::System.Int32 _Id;
        partial void OnIdChanging(global::System.Int32 value);
        partial void OnIdChanged();
    
        /// <summary>
        /// Нет доступной документации по метаданным.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Decimal Price
        {
            get
            {
                return _Price;
            }
            set
            {
                OnPriceChanging(value);
                ReportPropertyChanging("Price");
                _Price = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("Price");
                OnPriceChanged();
            }
        }
        private global::System.Decimal _Price;
        partial void OnPriceChanging(global::System.Decimal value);
        partial void OnPriceChanged();

        #endregion

    
    }
    
    /// <summary>
    /// Нет доступной документации по метаданным.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="InfoModel", Name="Rate")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Rate : EntityObject
    {
        #region Фабричный метод
    
        /// <summary>
        /// Создание нового объекта Rate.
        /// </summary>
        /// <param name="id">Исходное значение свойства Id.</param>
        /// <param name="rateUS">Исходное значение свойства RateUS.</param>
        public static Rate CreateRate(global::System.Int32 id, global::System.Decimal rateUS)
        {
            Rate rate = new Rate();
            rate.Id = id;
            rate.RateUS = rateUS;
            return rate;
        }

        #endregion

        #region Свойства-примитивы
    
        /// <summary>
        /// Нет доступной документации по метаданным.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 Id
        {
            get
            {
                return _Id;
            }
            set
            {
                if (_Id != value)
                {
                    OnIdChanging(value);
                    ReportPropertyChanging("Id");
                    _Id = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("Id");
                    OnIdChanged();
                }
            }
        }
        private global::System.Int32 _Id;
        partial void OnIdChanging(global::System.Int32 value);
        partial void OnIdChanged();
    
        /// <summary>
        /// Нет доступной документации по метаданным.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Decimal RateUS
        {
            get
            {
                return _RateUS;
            }
            set
            {
                OnRateUSChanging(value);
                ReportPropertyChanging("RateUS");
                _RateUS = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("RateUS");
                OnRateUSChanged();
            }
        }
        private global::System.Decimal _RateUS;
        partial void OnRateUSChanging(global::System.Decimal value);
        partial void OnRateUSChanged();

        #endregion

    
    }

    #endregion

    
}
