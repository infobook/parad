using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgTor.ParAd
{
  /// <summary>
  /// FIAS item
  /// </summary>
  public class fiAdrObj : fiBase
  {
    /// <summary>
    /// Глобальный уникальный идентификатор адресного объекта
    /// </summary>
    public string aoGUID;

    /// <summary>
    /// Формализованное наименование
    /// </summary>
    public string FormalName;

    /*
    Классификационный код адресного объекта отражает иерархию его 
    подчиненности и выделяет его среди объектов данного уровня, 
    подчиненных одному и тому же старшему объекту. 
    Классификационный код любого адресного объекта, 
    начиная от регионов и заканчивая элементом улично-дорожной 
    сети представляется в следующем виде:
     
    СС+РРР+ГГГ+ППП+СССС+УУУУ+ДДДД (или ЗЗЗЗ)+ОООО
    , где:
    */
    public string GetCode
    {
      get
      {
        //СС+РРР+ГГГ+ППП+СССС+УУУУ
        return String.Format(
          "{0,2:00} {1,3:000} {2,3:000} {3,3:000} {4,4:0000} {5,4:0000}"
          , RegionCode, AreaCode, CityCode, PlaceCode, PlanCode, StreetCode
        );
      }
    }
    /// <summary>
    /// СС – код субъекта Российской Федерации  – региона
    /// </summary>
    public string RegionCode;
    public short AutoCode; // Код автономии
    /// <summary>
    /// РРР – код района;
    /// </summary>
    public short AreaCode;
    /// <summary>
    /// ГГГ – код города;
    /// </summary>
    public short CityCode;
    public short CtarCode; //Код внутригородского района
    /// <summary>
    /// ППП код населенного пункта;
    /// </summary>
    public short PlaceCode;
    /// <summary>
    /// СССС - код элемента планировочной структуры; 
    /// </summary>
    public short PlanCode;
    /// <summary>
    /// УУУУ - код улицы;
    /// </summary>
    public short StreetCode;

    /// <summary>
    /// ДДДД (или ЗЗЗЗ).  
    /// ДДДД  тип и номер здания, сооружения, объекта незавершенного строительства 
    /// в случае адресации домов. 
    /// ЗЗЗЗ - номер земельного участка в случае адресации земельных участков;
    /// </summary>
    public short dddd;
    /// <summary>
    /// ОООО - тип и номер помещения в пределах здания, сооружения
    /// </summary>
    public short oooo;

    /// <summary>
    /// Статус последней исторической записи в жизненном цикле адресного объекта:
    /// 0 – Не последняя
    /// 1 - Последняя
    /// </summary>
    public int ActStatus;

    /// <summary>
    /// Статус актуальности адресного объекта ФИАС на текущую дату:
    /// 0 – Не актуальный
    /// 1 - Актуальный
    /// </summary>
    public int LiveStatus;


    public fiAdrObj()
    {
      _init();
    }

    protected override void _init()
    {
      base._init();

      aoGUID = null;
      FormalName = null;

      RegionCode = null;
      AutoCode = 0;
      AreaCode = 0;
      CityCode = 0;
      CtarCode = 0;
      PlaceCode = 0;
      PlanCode = 0;
      StreetCode = 0;

      dddd = 0;
      oooo = 0;
    }



    public override string ToString()
    {
      return ShortNameType + " " + FormalName + " ["+GetCode+
        "] level="+Level + " socr="+SocrBaseCode;
    }
  }
}
