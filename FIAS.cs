using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CommandAS.QueryLib;

namespace ProgTor.ParAd
{
  /// <summary>
  /// Class implement all interaction with FIAS DB.
  /// 
  /// created: 02.11.2016 by M.Tor
  /// modified: 11.11.2016 by M.Tor
  /// </summary>
  public class FIAS
  {
    public const String SESSION_NAME = "";

    private Performer _qs;

    private DataTable _socrbase;

    public DataTable SocrBase
    {
        get { return _socrbase; }
    }

    public FIAS(Performer aQS)
    {
        _qs = aQS;

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
    public bool IsSorcbase(String aSrc)
    {
      if (_socrbase.Rows.Count > 0)
      {
        var qr = from r in _socrbase.AsEnumerable()
                 where r.Field<String>("SCNAME").Contains(aSrc)
                 select r;
        foreach (DataRow dr in qr)
        {

        }
      }
      return false;
    }

    public bool LoadSocrBase()
    {
      if (_qs.SetCurrentQueryByCode(45))
      {
        _qs.SetCurrentQueryParam("OREDR_BY", "SCNAME");
        if (_qs.Execute() && _qs.pResultSet.Tables.Count > 0)
        {
          _socrbase = _qs.pResultSet.Tables[0];
          return true;
        }
      }

      return false;
    }
  }
}
