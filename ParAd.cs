using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using CommandAS.QueryLib;

namespace ProgTor.ParAd
{
  /// <summary>
  /// 
  /// </summary>
  public class paItem // : INotifyPropertyChanged
  {
    public enum Delim
    {
      Undefine = 0,
      WhiteSpase = 1,
      Point = 2,
      Comma = 3,
      Bracket = 4,
      No = 5
    }

    /// <summary>
    /// This is item, itself value of looking.
    /// </summary>
    public StringBuilder pItem {get; set;}

    /// <summary>
    /// Get item title.
    /// </summary>
    public String pItemTitle
    {
      get 
      {
        
        if (pItem != null)
        {
          return pItem.ToString();
        }
        else if (pIsDelim)
        {
          return "     --->";
        }
        else
          return "null";
      }
    }
    /// <summary>
    /// Get item property
    /// </summary>
    public String pItemProperty
    {
      get
      {
        if (pIsAbr)
        {
          return "ABR";
        }
        else if (pIsInsideSlash)
        {
          return "insede slash";
        }
        else if (pIsWordWithUpperInside)
        {
          return "word with up inside";
        }
        else if (pIsLetterDigit)
        {
          return "letter & digit";
        }
        else if (pIsIndex)
        {
          return "index";
        }
        else if (pIsDelim)
        {
          switch (pDelim)
          {
            case Delim.Bracket:
              return "{bracket}";
            case Delim.Comma:
              return "{comma}";
            case Delim.No:
              return "{No}";
            case Delim.Point:
              return "{point}";
            case Delim.WhiteSpase:
              return "{whitespase}";
            case Delim.Undefine:
            default:
              return "{delim undefined!}";
          }
        }
        else
          return string.Empty;
      }
    }
    /// <summary>
    /// This is word - letter only!
    /// </summary>
    public bool pIsWord {get; set;}
    /// <summary>
    ///  This is number - digital only!
    /// </summary>
    public bool pIsDigit {get; set;}
    /// <summary>
    /// This is mixed letters and digitals.
    /// </summary>
    public bool pIsLetterDigit {get; set;}
    /// <summary>
    /// Word is upper charcter inside word, not first!
    /// </summary>
    public bool pIsWordWithUpperInside;
    /// <summary>
    /// Word or number is inside slash character.
    /// </summary>
    public bool pIsInsideSlash;

    private bool _isAbr;
    /// <summary>
    /// This is abridgment
    /// </summary>
    public bool pIsAbr 
    {
      get { return _isAbr; }
      set
      {
        _isAbr = value;
        if (_isAbr)
        {
          // serach to the SOCRBASE table
        }
      }
    }
    /// <summary>
    /// Abridgment code from FIAS.SOCRBASE table
    /// </summary>
    public int pAbrCodeR { get; set; }

    private Delim _delim;

    public Delim pDelim
    {
      get { return _delim;  }
      set 
      { 
        _delim = value;
        pItem = null;
      }
    }

    public bool pIsDelim 
    {
      get { return _delim != Delim.Undefine; }
    }

    public bool pIsEmpty
    {
      get
      {
        return !(pIsDelim || pIsDigit || pIsLetterDigit || pIsWord);
      }
    }

    /// <summary>
    /// Is this item index?
    /// </summary>
    public bool pIsIndex
    {
      get { return pIsDigit && pItem.Length == 6; }
    }

    /// <summary>
    /// Is skip this word?
    /// </summary>
    public bool pIsSkipIt { get; set; }

    public paItem()
    {
      pItem = new StringBuilder();

      pIsWord = false;
      pIsDigit = false;
      pIsLetterDigit = false;
      pIsWordWithUpperInside = false;
      pIsInsideSlash = false;
      pIsSkipIt = false;
      _isAbr = false;
      pAbrCodeR = 0;

      _delim = Delim.Undefine;
    }

    public void AppendNextLetter(char aChar)
    {
      ///
      /// search flowing combination:
      /// OneTwo - this is propable two words, mistake!!!
      /// 
      if (pItem.Length > 0 && char.IsUpper(aChar) && !pIsWordWithUpperInside)
      {
        char pc = pItem[pItem.Length - 1];
        if (char.IsLetter(pc) && char.IsLower(pc))
          pIsWordWithUpperInside = true;
      }

      ///
      /// add character to the pItem
      ///
      pItem.Append(aChar);


      ///
      /// correct flags
      /// 
      if (pIsLetterDigit || pIsWord) 
      {
      }
      else if (pIsDigit)
      {
        pIsDigit = false;
        pIsWord = false;
        pIsLetterDigit = true;
      }
      else
      {
        pIsWord = true;
      }

      //OnPropertyChanged("pItemTitle");
    }

    public void AppendNextDigit(char aChar)
    {
      pItem.Append(aChar);
      if (pIsLetterDigit || pIsDigit) 
      {
      }
      else if (pIsWord)
      {
        pIsDigit = false;
        pIsWord = false;
        pIsLetterDigit = true;
      }
      else
      {
        pIsDigit = true;
      }
      //OnPropertyChanged("pItemTitle");
    }

    //public event PropertyChangedEventHandler PropertyChanged;
    //protected void OnPropertyChanged(string propertyName)
    //{
    //  if (PropertyChanged != null)
    //    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
    //}
  }

  public class ParAd
  {
    private String _src;
    private ArrayList _pai;

    private FIAS _fi;
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

    public ArrayList pArrPaItems
    {
      get { return _pai; }
    }

    public ParAd(FIAS aFIAS, Experience aExp)
    {
      _fi = aFIAS;
      _exp = aExp;
      _pai = new ArrayList();
    }

    private paItem _addDelimAndNextPAItem(paItem aPAI, paItem.Delim aDelim)
    {
      if (!aPAI.pIsEmpty)
      {
        _pai.Add(aPAI);
        aPAI = new paItem();
      }
      aPAI.pDelim = aDelim;
      _pai.Add(aPAI);
      return new paItem();
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

    public void Run()
    {
      StepOne_Characters();

      foreach (paItem pa in _pai)
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

      StepTwo_FIAS();
    }

    public void StepOne_Characters()
    {
      StringBuilder wsb = new StringBuilder(_src.Trim()); //.ToLower());

      wsb = _replaceLat2Cyr(wsb);

      paItem pa = new paItem();

      _pai.Clear();

      for (int ii = 0; ii < wsb.Length; ii++)
      {
        if (char.IsWhiteSpace(wsb[ii]) || wsb[ii] == ';' || wsb[ii] == ':')
        {
          if (!pa.pIsEmpty)
          { 
            _pai.Add(pa);
            pa = new paItem();
          }
          /// skip such combination:
          /// "г. Москва" - space skip
          /// "д. 10, кв. 11" -spaces skip
          /// "one      tow" - skip more then one space
          if (_pai.Count > 0 && ((paItem)_pai[_pai.Count-1]).pIsDelim)
          //if (pa.pIsDelim)
            continue;  // if previous is whitespace skip this

          pa.pDelim = paItem.Delim.WhiteSpase;
          _pai.Add(pa);
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
        _pai.Add(pa);
    }

    public void StepTwo_FIAS()
    {
      foreach (paItem pa in _pai)
      {
        if (pa.pIsSkipIt)
          continue;

        if (pa.pIsWord)
        {

        }
      }

    }
  }

}
