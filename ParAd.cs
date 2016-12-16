using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using CommandAS.Tools;
using CommandAS.QueryLib;

namespace ProgTor.ParAd
{
  public class AdrObjType
  {
    public int pCode;
    public String pAbr;
    private String[] _arrNames;

    public String[] pArrNames
    {
      get { return _arrNames; }
    }
    public void SetNames(String aNames)
    {
      if (aNames != null)
        _arrNames = aNames.Split(";".ToCharArray());
      else
        _arrNames = null;
    }

    public AdrObjType() : this(0, null, null)
    {  }

    public AdrObjType(int aCode, String aAbr, String aNames)
    {
      pCode = aCode;
      pAbr = aAbr;
      SetNames(aNames);
    }

  }

  public class ParAd
  {
    private ArrayList _arrAOT;

    private String _src;
    private ArrayList _arrPAI;
    private ArrayList _arrPAI2;

    private FIAS _fias;
    private Experience _exp;

    public String pSourceText
    {
      set
      {
        _src = value;
      }
      get
      {
        return _src;
      }
    }

    public ArrayList pArrAOT
    {
      get { return _arrAOT; }
    }

    public ArrayList pArrPaItems
    {
      get { return _arrPAI; }
    }

    public ArrayList pArrPaItems2
    {
      get { return _arrPAI2; }
    }

    public ParAd(FIAS aFIAS, Experience aExp)
    {
      _fias = aFIAS;
      _exp = aExp;
      _arrAOT = new ArrayList();
      _arrPAI = new ArrayList();
      _arrPAI2 = new ArrayList();
    }

    private paItem _addDelimAndNextPAItem(paItem aPAI, paItem.Delim aDelim)
    {
      aPAI = _addPAItem(aPAI);

      aPAI.pDelim = aDelim;
      _arrPAI.Add(aPAI);
      return new paItem();
    }

    private paItem _addPAItem(paItem aPAI)
    {
      if (!aPAI.pIsEmpty)
      {
        aPAI.CheckForm_1aya();
        _arrPAI.Add(aPAI);
        aPAI = new paItem();
      }

      return aPAI;
    }

    private StringBuilder _replaceLat2Cyr(StringBuilder aSB)
    {
      StringBuilder wsb = aSB.Replace('a', 'а');
      wsb = wsb.Replace('A', 'А');
      wsb = wsb.Replace('x', 'х');
      wsb = wsb.Replace('X', 'Х');
      wsb = wsb.Replace('e', 'е');
      wsb = wsb.Replace('E', 'Е');
      wsb = wsb.Replace('c', 'с');
      wsb = wsb.Replace('C', 'С');
      wsb = wsb.Replace('t', 'т');
      wsb = wsb.Replace('T', 'Т');
      wsb = wsb.Replace('b', 'в');
      wsb = wsb.Replace('B', 'В');
      wsb = wsb.Replace('h', 'н');
      wsb = wsb.Replace('H', 'Н');
      wsb = wsb.Replace('m', 'м');
      wsb = wsb.Replace('M', 'М');
      wsb = wsb.Replace('k', 'к');
      wsb = wsb.Replace('K', 'К');
      wsb = wsb.Replace('o', 'о');
      wsb = wsb.Replace('O', 'О');
      wsb = wsb.Replace('p', 'р');
      wsb = wsb.Replace('P', 'Р');

      return wsb;
    }

    /// <summary>
    /// Load arrya of address objects type from the file.
    /// </summary>
    /// <param name="aFN"></param>
    public void LoadAdrObjTypes(String aFN)
    {
      _arrAOT.Clear();

      if (File.Exists(aFN))
      {
        using (TextReader rd = File.OpenText(aFN))
        {
          while (rd.Peek() > -1)
          {
            string rl = rd.ReadLine().Trim();
            if (rl != null && rl.Length > 0)
            {
              string[] ss = rl.Split("\t".ToCharArray());
              if (ss.Length == 3)
                _arrAOT.Add(new AdrObjType(CASTools.ConvertToInt32Or0(ss[0]), ss[1], ss[2]));
            }
          }
        }
      }
    }

    public void Run()
    {
      StepOne_Characters();

      foreach (paItem pa in _arrPAI)
      {
        if (pa.pIsWord)
        {
          if (_exp.IsSkipWord(pa.pItem.ToString()))
          { // skip it if it in the experience !!!
            pa.pIsSkipIt = true;
          }
          else if (pa.pItem.Length > 3)
          { // check if word like this AAAAAA - skip it !!!
            char[] ca = pa.pItem.ToString().ToCharArray();
            pa.pIsSkipIt = true;
            for (int ii = 1; ii < ca.Length; ii++)
            {
              if (ca[ii] != ca[ii - 1])
              {
                pa.pIsSkipIt = false;
                break;
              }
            }

          }
        }

      }

      Step_FindAdrObjType();

      StepTwo_FIAS();
    }

    public void StepOne_Characters()
    {
      StringBuilder wsb = new StringBuilder(_src.Trim()); //.ToLower());

      wsb = _replaceLat2Cyr(wsb);

      paItem pa = new paItem();

      _arrPAI.Clear();

      for (int ii = 0; ii < wsb.Length; ii++)
      {
        if (char.IsWhiteSpace(wsb[ii]))
        {
          pa = _addPAItem(pa);
          /// skip such combination:
          /// "г. Москва" - space skip
          /// "д. 10, кв. 11" -spaces skip
          /// "one      tow" - skip more then one space
          if (_arrPAI.Count > 0 && ((paItem)_arrPAI[_arrPAI.Count-1]).pIsDelim)
          //if (pa.pIsDelim)
            continue;  // if previous is whitespace skip this

          pa.pDelim = paItem.Delim.WhiteSpase;
          _arrPAI.Add(pa);
          pa = new paItem();
        }
        else if (wsb[ii] == '.')
        {
          pa.pIsAbr = true;
          pa = _addDelimAndNextPAItem(pa, paItem.Delim.Point);
        }
        else if (wsb[ii] == ',')
        {
          pa = _addDelimAndNextPAItem(pa, paItem.Delim.Comma);
        }
        else if (wsb[ii] == ':')
        {
          pa = _addDelimAndNextPAItem(pa, paItem.Delim.Colon);
        }
        else if (wsb[ii] == ';')
        {
          pa = _addDelimAndNextPAItem(pa, paItem.Delim.Semicolon);
        }
        else if (wsb[ii] == '№')
        {
          pa = _addDelimAndNextPAItem(pa, paItem.Delim.No);
        }
        else if (wsb[ii] == '(' || wsb[ii] == ')' 
          || wsb[ii] == '[' || wsb[ii] == ']' 
          || wsb[ii] == '{' || wsb[ii] == '}' 
          || wsb[ii] == '<' || wsb[ii] == '>')
        {
          pa = _addDelimAndNextPAItem(pa, paItem.Delim.Bracket);
        }
        else if (char.IsLetter(wsb[ii]) || wsb[ii] == '-')
        {
          pa.AppendNextLetter(wsb[ii]);
        }
        else if (char.IsDigit(wsb[ii]))
        {
          pa.AppendNextDigit(wsb[ii]);
        }
        else if (wsb[ii] == '/' || wsb[ii] == '\\')
        {
          pa.pItem.Append(wsb[ii]);
          pa.pIsInsideSlash = true;
        }
        else
        {

        }
      }

      /// add last item if this not empty
      if (!pa.pIsEmpty)
        _arrPAI.Add(pa);
    }

    public void StepTwo_FIAS()
    {
      //short lvl = 0;
      //short code = 0;
      _fias.Clear();

      //foreach (paItem pa in _arrPAI)
      //{
      //  if (pa.pIsSkipIt)
      //    continue;

      //  if (pa.pIsWord)
      //  {
      //    fiBase fi = _fias.FindInSorcbase(pa.pItem.ToString(), lvl);
      //    if (fi != null)
      //    {
      //      //pa.pFiasItem = fi;
      //      fi.pPAISrc = pa;
      //      _fias.AddFiasItem(fi);
      //      lvl = fi.Level;
      //    }
      //  }
      //}

      #region search region
      {
        fiAdrObj fi = null;
        foreach (paItem pa in _arrPAI)
        {
          if (pa.pIsSkipIt)
            continue;

          if (pa.pIsWord)
          {
            fi = _fias.FindInRegion(pa.pItem.ToString());
            if (fi != null)
            {
              //pa.pFiasItem = fi;
              fi.pPAISrc = pa;
              _fias.AddFiasItem(fi);
              break;
            }
          }
        }

        if (fi == null)
        {
          // region not found !!!
        }
      }
      #endregion

      _
    }

    public void Step_FindAdrObjType()
    {
      foreach (paItem pa in _arrPAI)
      {
        if (pa.pIsSkipIt)
          continue;

        if (pa.pIsWord)
        {
          foreach(AdrObjType aot in _arrAOT)
          {
            if (Regex.IsMatch(pa.pItem.ToString(), @"\b"+aot.pAbr+@"\b"))
            {
              pa.pAdrObjType = aot.pCode;
            }
          }
        }
      }
    }

    public void StepThree_PaItem2()
    {
      _arrPAI2.Clear();

      foreach (paItem pai in _arrPAI)
      {
        //if ()
      }
    }


    [DebuggerStepThrough]
    static public short ConvertToInt16Or0(object aObj)
    {
      short ret = 0;
      try
      {
        ret = Convert.ToInt16(aObj);
      }
      catch { }
      return ret;
    }
  }

}
