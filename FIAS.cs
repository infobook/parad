using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CommandAS.Tools;
using CommandAS.QueryLib;

namespace ProgTor.ParAd
{
  /// <summary>
  /// Class implement all interaction with FIAS DB.
  /// 
  /// created: 02.11.2016 by M.Tor
  /// modified: 14.11.2016 by M.Tor
  /// </summary>
  public class FIAS
  {
    public const String SESSION_NAME = "parad.sq3";

    private Performer _qs;

    private DataTable _socrbase;
    private DataTable _reg;

    public DataTable SocrBase
    {
        get { return _socrbase; }
    }

    public DataTable Region
    {
      get { return _reg; }
    }

    public FIAS(Performer aQS)
    {
        _qs = aQS;

    }

    public bool LoadSession(String aFileName)
    {
      return _qs.Load(aFileName);
    }
    public bool LoadSession()
    {
      return _qs.Load(SESSION_NAME);
    }

    /// <summary>
    /// 
    /// cLevel, cName, cSName, cCode
    /// </summary>
    /// <param name="aSrc"></param>
    /// <returns></returns>
    public int IsSorcbase(String aSrc, ref int aLevelGrThen )
    {
      if (_socrbase.Rows.Count > 0)
      {
        //String lvl = aLevelGrThen.ToString();
        int lvl = aLevelGrThen;
        // first equals compare
        IEnumerable<DataRow> qr = from r in _socrbase.AsEnumerable()
                                  where (r.Field<short>("cLvl") > lvl && r.Field<String>("cSName2").Equals(aSrc))
                                  orderby r.Field<short>("cLvl")
          select r;
        // second contains
        if (qr.Count() == 0)
        {
          qr = from r in _socrbase.AsEnumerable()
               where (r.Field<short>("cLvl") > lvl && r.Field<String>("cSName2").Contains(aSrc))
               orderby r.Field<short>("cLvl")
               select r;
        }
        // third contains in full name, for examples "дом"
        if (qr.Count() == 0 && aSrc.Length > 2)
        {
          qr = from r in _socrbase.AsEnumerable()
               where (r.Field<short>("cLvl") > lvl && r.Field<String>("cName").Contains(aSrc))
               orderby r.Field<short>("cLvl")
               select r;
        }

        if (qr.Count() > 0)
        {
          DataRow dr = qr.First();
          aLevelGrThen = CASTools.ConvertToInt32Or0(dr["cLvl"]);
          return CASTools.ConvertToInt32Or0(dr["cCode"]);
        }
      }
      return 0;
    }

    public bool LoadAll()
    {
      //if (!LoadSession())
      //  return false;

      if (!LoadSocrBase())
        return false;

      if (!LoadRegion())
        return false;

      _qs.pWDB.ConnectionClose();

      return true;
    }

    public bool LoadSocrBase()
    {
      if (_qs.SetCurrentQueryByCode(45))
      {
        _qs.SetCurrentQueryParam("OREDR_BY", String.Empty);
        if (_qs.Execute() && _qs.pResultSet.Tables.Count > 0)
        {
          _socrbase = _qs.pResultSet.Tables[0];
          foreach (DataRow dr in _socrbase.Rows)
          {
            String st = dr["cSName"].ToString().ToLower();
            if (st.Last().Equals('.'))
              dr["cSName2"] = st.Substring(0, st.Length - 1);
            else
              dr["cSName2"] = st;

          }
          return true;
        }
      }

      return false;
    }

    /// <summary>
    /// Load region first level. Parents undefined!
    /// </summary>
    /// <returns>true if okay, else false</returns>
    public bool LoadRegion()
    {
      if (_qs.SetCurrentQueryByCode(105))
      {
        if (_qs.Execute() && _qs.pResultSet.Tables.Count > 0)
        {
          _reg = _qs.pResultSet.Tables[0];

          return true;
        }
      }

      return false;
    }
  }
}
