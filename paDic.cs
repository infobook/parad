using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgTor.ParAd
{
  /// <summary>
  /// Dictionary and needs info
  /// created 13.01.2017
  /// by M.Tor
  /// </summary>
  public class paDic
  {
    private Dictionary<String, int> _dic;

    public Dictionary<String, int> pDic
    {
      get { return _dic; }
    }

    public bool pIsContains(String aKey)
    {
      return _dic.ContainsKey(aKey);
    }

    public string pAbr;
    public int pLevel;

    public paDic(string aAbr, int aLvl)
    {
      _dic = new Dictionary<string, int>();
      pAbr = aAbr;
      pLevel = aLvl;
    }

    public string About()
    {
      if (pAbr != null)
        return "dictionary - [" + pAbr + "]: level = " + pLevel + "; count =" + _dic.Count;
      else
        return "dictionary: level = " + pLevel + "; count =" + _dic.Count;
    }
  }

  /// <summary>
  /// Set dictionaries for parser.
  /// Created 13.01.2017
  /// by M.Tor
  /// </summary>
  public class paDicSet
  {
    protected List<paDic> _arrDic;
    protected int mCurrDic;

    public paDicSet() 
    {
      _arrDic = new List<paDic>();
      mCurrDic = -1;
    }

    public paDic AddNewDic(string aAbr, int aLvl)
    {
      paDic dic = new paDic(aAbr, aLvl);
      _arrDic.Add(dic);

      return dic;
    }

    public paDic GetFirst()
    {
      mCurrDic = 0;
      return _arrDic[mCurrDic];
    }

    public paDic GetNext()
    {
      paDic ret = null;
      if (mCurrDic != -1 && ++mCurrDic < _arrDic.Count)
      {
        ret = _arrDic[mCurrDic];
      }
      return ret;
    }

    public string About ()
    {
      string ret = "dictionaries set [count=" + _arrDic.Count + "] is:";

      foreach (paDic dic in _arrDic)
        ret += Environment.NewLine + "\t" + dic.About();

      return ret;
    }
  }
}
