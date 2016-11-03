using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandAS.QueryLib;

namespace parad
{
  /// <summary>
  /// 
  /// </summary>
  public class paItem
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


    public paItem()
    {
      pItem = new StringBuilder();

      pIsWord = false;
      pIsDigit = false;
      pIsLetterDigit = false;
      pIsWordWithUpperInside = false;
      pIsInsideSlash = false;
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
    }
}

  public class ParAd
  {
    private String _src;
    private ArrayList _pai;

    private Performer _qs;

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

    public ParAd(Performer aQS)
    {
      _qs = aQS;
      _pai = new ArrayList();
    }

    private paItem _addDelimAndNextPAItem(paItem aPAI, paItem.Delim aDelim)
    {
      _pai.Add(aPAI);
      aPAI = new paItem();
      aPAI.pDelim = aDelim;
      _pai.Add(aPAI);
      return new paItem();
    }

    public void StepOne_Characters()
    {
      StringBuilder wsb = new StringBuilder(_src.Trim()); //.ToLower());

      #region Replace Lat -> Cyr
      ///          Lat  Cyr 
      wsb.Replace('a', 'а');
      wsb.Replace('A', 'А');
      wsb.Replace('x', 'х');
      wsb.Replace('X', 'Х');
      wsb.Replace('e', 'е');
      wsb.Replace('E', 'Е');
      wsb.Replace('c', 'с');
      wsb.Replace('C', 'С');
      wsb.Replace('t', 'т');
      wsb.Replace('T', 'Т');
      wsb.Replace('b', 'в');
      wsb.Replace('B', 'В');
      wsb.Replace('h', 'н');
      wsb.Replace('H', 'Н');
      wsb.Replace('m', 'м');
      wsb.Replace('M', 'М');
      wsb.Replace('k', 'к');
      wsb.Replace('K', 'К');
      wsb.Replace('o', 'о');
      wsb.Replace('O', 'О');
      wsb.Replace('p', 'р');
      wsb.Replace('P', 'Р');
      #endregion

      paItem pa = new paItem();

      for (int ii = 0; ii < wsb.Length; ii++)
      {
        if (char.IsWhiteSpace(wsb[ii]))
        {
          /// skip such combination:
          /// "г. Москва" - space skip
          /// "д. 10, кв. 11" -spaces skip
          /// "one      tow" - skip more then one space
          if (_pai.Count > 0 && ((paItem)_pai[_pai.Count-1]).pIsDelim)
            continue;  // if previous is whitespace skip this

          pa = _addDelimAndNextPAItem(pa, paItem.Delim.WhiteSpase);
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
        else if (wsb[ii] == '/')
        {
          pa.pItem.Append(wsb[ii]);
          pa.pIsInsideSlash = true;
        }
        else
        {

        }
      }
    }
  }

}
