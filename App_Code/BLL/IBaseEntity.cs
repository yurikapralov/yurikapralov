using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace echo.BLL
{

    /// <summary>
    /// Общий для всех сущностей интерфейс
    /// </summary>
    public interface IBaseEntity
    {
        bool IsValid
        { get;}

        string SetName
        { get; set;}
    }
}
