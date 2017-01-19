using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Data;
using CommandAS.Tools;
using CommandAS.QueryLib;

namespace ProgTor.ParAd
{
  public class AdrObjType
  {
    public int pCode;
    public String pAbr;
    private String[] _arrNames;
    //private fiAdrObj[] _arrFIAO;

    public String[] pArrNames
    {
      get { return _arrNames; }
    }

    //public fiAdrObj[] pArrFIAO
    //{
    //  get { return _arrFIAO; }
    //}

    public void SetNames(String aNames)
    {
      if (aNames != null)
      {
        _arrNames = aNames.Split(";".ToCharArray());
        //_arrFIAO = new fiAdrObj[_arrNames.Length];
      }
      else
      {
        _arrNames = null;
        //_arrFIAO = null;
      }
    }

    public AdrObjType() : this(0, null, null)
    {  }

    public AdrObjType(int aCode, String aAbr, String aNames)
    {
      pCode = aCode;
      pAbr = aAbr;
      SetNames(aNames);
    }

    //public void FindFIAS(FIAS aF)
    //{
    //  if (_arrNames == null)
    //    return;

    //  for (int ii=0; ii < _arrNames.Length; ii++)
    //  {
    //    _arrFIAO[ii] = aF.FindInSorcbaseByFullName(_arrNames[ii], 0);
    //  }
    //}
  }

  public class ParAd
  {
    private List<AdrObjType> _arrAOT;

    private String _src;
    private List<paItemAO> _arrPAI;
    private List<SetPAIAO> _arrSPAI;

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

    public List<AdrObjType> pArrAOT
    {
      get { return _arrAOT; }
    }

    public List<paItemAO> pArrPaItems
    {
      get { return _arrPAI; }
    }

    public List<SetPAIAO> pArrSetPAIAO
    {
      get { return _arrSPAI; }
    }

    public DataTable pResult;

    public ParAd(FIAS aFIAS, Experience aExp)
    {
      _fias = aFIAS;
      _exp = aExp;
      _arrAOT = new List<AdrObjType>();
      _arrPAI = new List<paItemAO>();
      _arrSPAI = new List<SetPAIAO>();
      pResult = null;
    }

    private paItemAO _addDelimAndNextPAItem(paItemAO aPAI, paItem.Delim aDelim)
    {
      aPAI = _addPAItem(aPAI);

      aPAI.pDelim = aDelim;
      _arrPAI.Add(aPAI);
      return new paItemAO();
    }

    private paItemAO _addPAItem(paItemAO aPAI)
    {
      if (!aPAI.pIsEmpty)
      {
        aPAI.CheckForm_1aya();
        _arrPAI.Add(aPAI);
        aPAI = new paItemAO();
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
      pResult = null;

      StepOne_Characters();

      foreach (paItemAO pa in _arrPAI)
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

      W_StepFour_PaItemInDictionary();

      if (_arrSPAI.Count > 0)
      {
        StringBuilder aos = new StringBuilder(_arrSPAI[0].pItemTitle);
        for (int ii = 1; ii < _arrSPAI.Count; ii++)
          aos.Append(";").Append(_arrSPAI[ii].pItemTitle);

        pResult = _fias.FindAddrObjs(aos.ToString());
      }
    }

    //public void Clear()
    //{
    //  _arrPAI.Clear();
    //  _arrSPAI.Clear();
    //}

    public void StepOne_Characters()
    {
      StringBuilder wsb = new StringBuilder(_src.Trim()); //.ToLower());

      wsb = _replaceLat2Cyr(wsb);

      paItemAO pa = new paItemAO();

      _arrPAI.Clear();
      _arrSPAI.Clear();

      for (int ii = 0; ii < wsb.Length; ii++)
      {
        if (char.IsWhiteSpace(wsb[ii]))
        {
          pa = _addPAItem(pa);
          /// skip such combination:
          /// "г. Москва" - space skip
          /// "д. 10, кв. 11" -spaces skip
          /// "one      tow" - skip more then one space
          if (_arrPAI.Count > 0 && ((paItemAO)_arrPAI[_arrPAI.Count-1]).pIsDelim)
          //if (pa.pIsDelim)
            continue;  // if previous is whitespace skip this

          pa.pDelim = paItem.Delim.WhiteSpase;
          _arrPAI.Add(pa);
          pa = new paItemAO();
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
      _fias.Clear();

      fiAdrObj fi = null;
      foreach (paItemAO pa in _arrPAI)
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

    public void Step_FindAdrObjType()
    {
      foreach (paItemAO pa in _arrPAI)
      {
        if (pa.pIsSkipIt)
          continue;

        if (pa.pIsWord)
        {
          foreach(AdrObjType aot in _arrAOT)
          {
            if (Regex.IsMatch(pa.pItem.ToString(), @"\b"+aot.pAbr+@"\b"))
            {
              pa.pAdrObjType = aot;
            }
          }
        }
      }
    }

    //public void StepThree_PaItem2()
    //{
    //  string param = string.Empty;
    //  bool isAddSem = false;
    //  foreach (paItemAO pai in _arrPAI)
    //  {
    //    if (pai.pIsAdrObjType)
    //    {
    //      isAddSem = param.Length > 0;
    //    }
    //    //else if (pai.pIsDelim)

    //  }
    //}


    /// <summary>
    /// Разбераем адресную строку, точнее набор элементов по словарю!
    /// M.Tor
    /// 18.01.2017
    /// </summary>
    public void W_StepFour_PaItemInDictionary()
    {
      /// ищем с конца разбираемой строки или до первого сокращения
      /// типа адресного объекта "д" (дом) или до первого слова более 3 символов,
      /// предпологая, что сокращение пропущено, и до первого элемента
      bool isEndAO = false;
      List<paItemAO> rwa = new List<paItemAO>();
      for(int ii = _arrPAI.Count-1; ii >= 0; ii--)
      {
        if (isEndAO)
        {
          if (!_arrPAI[ii].pIsAdrObjType && _arrPAI[ii].pIsMayBeAO)
            rwa.Add(_arrPAI[ii]);
        }
        else
        {
          if (_arrPAI[ii].pIsAdrObjTypeHouse)
          {
            isEndAO = true;
          }
          else if (!_arrPAI[ii].pIsAdrObjType && _arrPAI[ii].pIsWord && _arrPAI[ii].pItemTitle.Length > 3)
          {
            isEndAO = true;
            rwa.Add(_arrPAI[ii]);
          }
        }
      }

      if (rwa.Count == 0)
        return;

      // перевернем rwa --> wa
      List<paItemAO> wa = new List<paItemAO>();
      for (int ii = rwa.Count - 1; ii >= 0; ii--)
        wa.Add(rwa[ii]);

      int offset = 0;
      int fini;
      paDic dic = _fias.pDicSet.GetFirst();
      while (offset < wa.Count)
      {
        fini = wa.Count;
        SetPAIAO spai = null;
        while (fini > offset)
        {
          // набираем строку:
          StringBuilder sbf = new StringBuilder(wa[offset].pItem.ToString());
          for (int ii = offset+1; ii < fini; ii++)
            sbf.Append(" ").Append(wa[ii].pItem);
          // проверяем есть ли она в словаре?
          if (dic.pIsContainsUp(sbf.ToString()))
          {
            /// ДА:
            /// создаем набор элементов
            spai = new SetPAIAO();
            spai.pDic = dic;
            for (int ii = offset; ii < fini; ii++)
              spai.Add(wa[ii]);
            /// добавляем его в массив
            _arrSPAI.Add(spai);
            /// и смещаемся
            offset = fini;
          }
          else
          {
            /// если НЕТ: уменьшаем длину
            fini--;
          }
        }
        /// берем следующий словарь:
        dic = _fias.pDicSet.GetNext();
        if (dic == null)
          break;
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
