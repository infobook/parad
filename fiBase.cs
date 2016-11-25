using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgTor.ParAd
{
  public class fiBase
  {
    /// <summary>
    /// "Уровень адресного объекта" содержит номер уровня 
    /// классификации адресных объектов. 
    /// </summary>
    public short Level;
    /*
     Перечень уровней адресных объектов и соответствующих им типов адресных объектов определен в таблице SOCRBASE ФИАС
      Условно выделены следующие уровни адресных объектов:
       1 – уровень региона
       2 – уровень автономного округа (устаревшее)
       3 – уровень района
       35 – уровень городских и сельских поселений
       4 – уровень города
       5 – уровень внутригородской территории (устаревшее)
       6 – уровень населенного пункта
       65 – планировочная структура
       7 – уровень улицы
       75 – земельный участок
       8 – здания, сооружения, объекта незавершенного строительства
       9 – уровень помещения в пределах здания, сооружения
       90 – уровень дополнительных территорий (устаревшее)
       91 – уровень объектов на дополнительных территориях (устаревшее)
   */

    public short SocrBaseCode;

    /// <summary>
    /// Краткое наименование типа объекта
    /// </summary>
    public string ShortNameType;

    /// <summary>
    /// Arrya children item. 
    /// </summary>
    public ArrayList pChildren;

    /// <summary>
    /// Reference to the paItem
    /// </summary>
    public paItem pPAISrc;

    public fiBase()
    { }

    protected virtual void _init()
    {
      Level = 0;
      ShortNameType = null;
  
      SocrBaseCode = 0;

      pChildren = null;
      pPAISrc = null;
    }

    public override string ToString()
    {
      return ShortNameType + " level=" + Level + " socr=" + SocrBaseCode;
    }

    public int AddChild(fiBase aFI)
    {
      if (pChildren == null)
        pChildren = new ArrayList();

      return pChildren.Add(aFI);
    }
  }
}
