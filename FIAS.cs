using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using CommandAS.Tools;
using CommandAS.QueryLib;

namespace ProgTor.ParAd
{
  /// <summary>
  /// Class implement all interaction with FIAS DB.
  /// 
  /// created: 02.11.2016 by M.Tor
  /// modified: 29.11.2016 by M.Tor
  /// </summary>
  public class FIAS
  {
//    public const String SESSION_NAME = "parad.sq3";

    private Performer _qs;

    private DataTable _socrbase;
    private DataTable _reg;

    private ArrayList _arrFI;

    public DataTable SocrBase
    {
        get { return _socrbase; }
    }

    public DataTable Region
    {
      get { return _reg; }
    }

    public ArrayList pArrFiasItems
    {
      get { return _arrFI; }
    }

    public FIAS(Performer aQS)
    {
        _qs = aQS;
        _arrFI = new ArrayList();

    }

    public bool LoadSession(String aFileName)
    {
      return _qs.Load(aFileName);
    }
    //public bool LoadSession()
    //{
    //  return _qs.Load(SESSION_NAME);
    //}

    /// <summary>
    /// 
    /// cLevel, cName, cSName, cCode
    /// </summary>
    /// <param name="aSrc"></param>
    /// <returns></returns>
    public fiBase FindInSorcbase(String aSrc, short aLevelGrThen )
    {
      if (_socrbase.Rows.Count > 0)
      {
        //String lvl = aLevelGrThen.ToString();
        //int lvl = aLevelGrThen;
        // first equals compare
        IEnumerable<DataRow> qr = from r in _socrbase.AsEnumerable()
                                  where (r.Field<short>("cLvl") > aLevelGrThen && r.Field<String>("cSName2").Equals(aSrc))
                                  orderby r.Field<short>("cLvl")
          select r;
        // second contains
        if (qr.Count() == 0)
        {
          qr = from r in _socrbase.AsEnumerable()
               where (r.Field<short>("cLvl") > aLevelGrThen && r.Field<String>("cSName2").Contains(aSrc))
               orderby r.Field<short>("cLvl")
               select r;
        }
        // third contains in full name, for examples "дом"
        if (qr.Count() == 0 && aSrc.Length > 2)
        {
          qr = from r in _socrbase.AsEnumerable()
               where (r.Field<short>("cLvl") > aLevelGrThen && r.Field<String>("cName").Contains(aSrc))
               orderby r.Field<short>("cLvl")
               select r;
        }

        if (qr.Count() > 0)
        {
          DataRow dr = qr.First();
          fiBase fi = new fiBase();
          fi.ShortNameType = dr["cSName"].ToString();
          fi.SocrBaseCode = ParAd.ConvertToInt16Or0(dr["cCode"]);
          fi.Level = ParAd.ConvertToInt16Or0(dr["cLvl"]);
          return fi;
        }
      }
      return null;
    }

    public fiAdrObj FindInRegion (String aSrc)
    {
      if (_reg != null && _reg.Rows.Count > 0)
      {
        IEnumerable<DataRow> qr = from r in _reg.AsEnumerable()
                                  where r.Field<String>("cUName").Equals(aSrc.ToUpper())
                                  select r;

        if (qr.Count() > 0)
        {
          DataRow dr = qr.First();
          fiAdrObj fi = new fiAdrObj();
          fi.aoGUID = dr["cGUID"].ToString();
          fi.RegionCode = dr["cRegCode"].ToString();
          fi.ShortNameType = dr["cSName"].ToString();
          fi.FormalName = dr["cFName"].ToString();
          fi.ActStatus = CASTools.ConvertToInt32Or0(dr["cStatus"]);
          fi.LiveStatus = CASTools.ConvertToInt32Or0(dr["cLive"]);
          fi.Level = ParAd.ConvertToInt16Or0(dr["cLevel"]);
          return fi;
        }
      }

      return null;
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
    /// cGUID, cRegCode, cSName, cFName, cUName, cStatus, cLive, cLevel, cIsLoadNext
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

    public void FindAddrObjs(String aAddrObjsSet)
    {
      OleDbCommand cmd = _qs.pWDB.NewOleDbCommand();

      cmd.CommandType = CommandType.StoredProcedure;
    }
 
    public int AddFiasItem(fiBase aFI)
    {
      return AddFiasItem(aFI, null);
    }
    public int AddFiasItem(fiBase aFI, fiBase aFIParent)
    {
      if (aFIParent == null)
      {
        return _arrFI.Add(aFI);

      }
      else
      {
        return aFIParent.AddChild(aFI);
      }

    }

    public void Clear()
    {
      _arrFI.Clear();
    }
  }
}
