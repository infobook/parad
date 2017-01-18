using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgTor.ParAd
{
  public class paItemAO : paItem
  {
    private AdrObjType _aot;

    public AdrObjType pAdrObjType
    {
      get { return _aot; }
      set { _aot = value; }
    }

    /// <summary>
    /// Is this item may be part or total title of address object 
    /// </summary>
    public bool pIsMayBeAO
    {
      get
      {
        return !pIsSkipIt && !pIsDelim && !pIsIndex && (pIsWord || pIsDigit || pIsLetterDigit);
      }
    }

    /// <summary>
    /// This is address object type. As so, region,city,build and s.o.
    /// </summary>
    public bool pIsAdrObjType
    {
      get { return _aot != null; }
    }

    public bool pIsAdrObjTypeHouse
    {
      get { return _aot != null && _aot.pAbr.Equals("д"); }
    }

    //public bool pIsSocrBase
    //{
    //  get { return pFiasItem != null && pFiasItem.SocrBaseCode > 0 ; }
    //}
    /// <summary>
    /// Abridgment code from FIAS.SOCRBASE table
    /// </summary>
    //public int pAbrCodeR { get; set; }

    /// <summary>
    /// Is this item index?
    /// </summary>
    public bool pIsIndex
    {
      get { return pIsDigit && pItem.Length == 6; }
    }

    //private fItem _ff;
    /// <summary>
    /// Reference to the FIAS item.
    /// </summary>
    //public fiBase pFiasItem
    //{
    //  get; // {return _ff; }
    //  set;
    //}

    public override String pItemProperty
    {
      get
      {
        if (pIsAdrObjType)
        {
          return "AdrObjType = " + _aot.pCode;
        }
        else if (pIsIndex)
        {
          return "index";
        }
        else
        {
          return base.pItemProperty;
        }
      }
    }
    public paItemAO() : base()
    {
      _aot = null;
      //pAbrCodeR = 0;

      //pFiasItem = null;
    }

  }

  public class SetPAIAO
  {
    public paItemAO pPAIAOType;
    public paDic pDic;

    private List<paItemAO> _arrPAI;

    //public List<paItemAO> pArrPAI
    //{
    //  get { return _arrPAI; }
    //}

    public int pCount
    {
      get { return _arrPAI.Count; }
    }

    public String pItemTitle
    {
      get
      {
        StringBuilder ret = new StringBuilder();
        foreach (paItemAO pai in _arrPAI)
        {
          if (ret.Length > 0)
         {
           ret.Append(" ");
          }
          ret.Append(pai.pItemTitle);
        }

        return ret.ToString();
      }
    }

    public SetPAIAO()
    {
      pPAIAOType = null;
      pDic = null;
      _arrPAI = new List<paItemAO>();
    }

    public void Add (paItemAO aPAI)
    {
      _arrPAI.Add(aPAI);
    }

    public void RemoveLast()
    {
      if (_arrPAI.Count > 0)
      {
        _arrPAI.RemoveAt(_arrPAI.Count - 1);
      }
    }

  }

}
