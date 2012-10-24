using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Сводное описание для sqlProduct
/// </summary>
public class sqlProduct 
{
   
    private int _prodId = 0;
    private int _prodTypeId = 0;
    private int _platformId = 0;
    private string _productNameRus = "";
    private string _productNameEng = "";
    private string _productDescriptionRus = "";
    private string _productDescriptionEng = "";
    private string _thumbUrl = "";
    private decimal _origprice = 0;
    private decimal _whsprice = 0;
    private decimal _saleprice = 0;
    private DateTime _datecreated=DateTime.Now;
    private DateTime _dateupdated = DateTime.Now;
    private string _sortedName = "";
    private bool _avilable = true;

    public sqlProduct(int prodId, int prodTypeId, int platformId, string productNameRus, string productNameEng, 
        string productDescriptionRus, string productDescriptionEng, string thumbUrl, decimal origprice, decimal whsprice, 
        decimal saleprice,DateTime datecreated,DateTime dateupdated, string sortedName, bool avilable)
    {
        _prodId = prodId;
        _prodTypeId = prodTypeId;
        _platformId = platformId;
        _productNameRus = productNameRus;
        _productNameEng = productNameEng;
        _productDescriptionRus = productDescriptionRus;
        _productDescriptionEng = productDescriptionEng;
        _thumbUrl = thumbUrl;
        _origprice = origprice;
        _whsprice = whsprice;
        _saleprice = saleprice;
        _datecreated = datecreated;
        _dateupdated = dateupdated;
        _sortedName = sortedName;
        _avilable = avilable;
    }

    public int ProdId
    {
        get { return _prodId; }
        set { _prodId = value; }
    }

    public int ProdTypeId
    {
        get { return _prodTypeId; }
        set { _prodTypeId = value; }
    }

    public int PlatformId
    {
        get { return _platformId; }
        set { _platformId = value; }
    }

    public string ProductNameRus
    {
        get { return _productNameRus; }
        set { _productNameRus = value; }
    }

    public string ProductNameEng
    {
        get { return _productNameEng; }
        set { _productNameEng = value; }
    }

    public string ProductDescriptionRus
    {
        get { return _productDescriptionRus; }
        set { _productDescriptionRus = value; }
    }

    public string ProductDescriptionEng
    {
        get { return _productDescriptionEng; }
        set { _productDescriptionEng = value; }
    }

    public string ThumbUrl
    {
        get { return _thumbUrl; }
        set { _thumbUrl = value; }
    }

    public decimal Origprice
    {
        get { return _origprice; }
        set { _origprice = value; }
    }

    public decimal Whsprice
    {
        get { return _whsprice; }
        set { _whsprice = value; }
    }

    public decimal Saleprice
    {
        get { return _saleprice; }
        set { _saleprice = value; }
    }

    public DateTime Datecreated
    {
        get { return _datecreated; }
        set { _datecreated = value; }
    }

    public DateTime Dateupdated
    {
        get { return _dateupdated; }
        set { _dateupdated = value; }
    }

    public string SortedName
    {
        get { return _sortedName; }
        set { _sortedName = value; }
    }

    public bool Avilable
    {
        get { return _avilable; }
        set { _avilable = value; }
    }
}

