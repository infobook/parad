using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace ProgTor.ParAd
{
  /// <summary>
  /// This is experience taked program.
  /// </summary>
  public class Experience
  {
    /// <summary>
    /// Array word for skip.
    /// </summary>
    [XmlArrayItem("Skip", typeof(String))]
    public ArrayList ArrSkip { get; set; }

    public Experience ()
    {
      ArrSkip = new ArrayList();
    }

    public static Experience Load(string aFileName)
    {
      Experience exp = new Experience();

      FileStream f = new FileStream(aFileName, FileMode.Open);
      XmlSerializer xs = new XmlSerializer(typeof(Experience));
      exp = (Experience)xs.Deserialize(f);

      f.Close();

      return exp;
    }

    public bool Save(string aFileName)
    {
      bool ret = false;

      if (aFileName != null && aFileName.Length > 0)
      {
        try
        {
          TextWriter tr = new StreamWriter(aFileName, false, Encoding.Default);
          XmlSerializer xs = new XmlSerializer(typeof(Experience));
          xs.Serialize(tr, this);
          tr.Close();
          ret = true;
        }
        catch (Exception ex)
        {

        }
      }

      return ret;
    }

    public bool IsSkipWord(String aSrc)
    {
      String src = aSrc.ToUpper();
      foreach (string ss in ArrSkip)
      {
        if (src.Equals(ArrSkip))
          return true;
      }

      return false;
    }

  }
}
