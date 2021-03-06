﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
      Colon = 4,
      Semicolon = 5,
      Bracket = 6,
      No = 7
    }

    /// <summary>
    /// This is item, itself value of looking.
    /// </summary>
    public StringBuilder pItem { get; set; }

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
    public virtual String pItemProperty
    {
      get
      {
        if (pIsInsideSlash)
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
        else if (pIsWord)
        {
          return "word";
        }
        else if (pIsDigit)
        {
          return "digit";
        }
        else
          return string.Empty;
      }
    }

    /// <summary>
    /// This is word - letter only!
    /// </summary>
    public bool pIsWord { get; set; }
    /// <summary>
    ///  This is number - digital only!
    /// </summary>
    public bool pIsDigit { get; set; }
    /// <summary>
    /// This is mixed letters and digitals.
    /// </summary>
    public bool pIsLetterDigit { get; set; }
    /// <summary>
    /// Word is upper charcter inside word, not first!
    /// </summary>
    public bool pIsWordWithUpperInside;
    /// <summary>
    /// Word or number is inside slash character.
    /// </summary>
    public bool pIsInsideSlash;
    /// <summary>
    /// This is abbreviation.
    /// </summary>
    public bool pIsAbr;


    private Delim _delim;

    public Delim pDelim
    {
      get { return _delim; }
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
    /// Is skip this word?
    /// </summary>
    public bool pIsSkipIt { get; set; }


    private bool _is1ya;

    public bool pIs1ya
    {
      get { return _is1ya; }
    }


    public paItem()
    {
      pItem = new StringBuilder();

      pIsWord = false;
      pIsDigit = false;
      pIsLetterDigit = false;
      pIsWordWithUpperInside = false;
      pIsInsideSlash = false;
      pIsSkipIt = false;
      pIsAbr = false;
      _is1ya = false;
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

    /// <summary>
    /// Check is words as [number][-][letter]
    /// and transform words as:
    /// 1-ая ->   1-я
    /// 1ая  ->   1-я
    /// 1я   ->   1-я
    /// 11-ый -> 11-й
    /// 
    /// </summary>
    /// <returns></returns>
    public bool CheckForm_1aya()
    {
      if (!pIsLetterDigit)
        return false;

      StringBuilder sbTgt = new StringBuilder();
      int flag = 0;
      char last = ' ';
      int ii = 0;
      foreach (char cc in pItem.ToString().ToArray())
      {
        if (char.IsDigit(cc))
        {
          if (flag == 0)
            sbTgt.Append(cc);
          else
            return false;
        }
        else if (cc == '-') 
        {
          if (flag == 2)
            return false;

          flag = 1;
        }
        else if (char.IsLetter(cc))
        {
          flag = 2;
          last = cc;
          ii++;
        }
        else
        {
          return false;
        }
      }

      if (flag == 2 && ii < 4)
      {
        sbTgt.Append('-');
        sbTgt.Append(last);
        pItem = sbTgt;
        _is1ya = true;
        return true;
      }

      return false;
    }
  }
  
  //public class paItem2
  //{
  //  private paItem _pai;

  //  public paItem pItem
  //  {
  //    get { return _pai; }
  //  }

  //  /// <summary>
  //  /// This is reference to the next item in case composite title.
  //  /// More then one words. For examples:
  //  /// [Парковая 16-я]
  //  /// [Маршала Рыбалко]
  //  /// [Северная Осетия - Алания]    
  //  /// </summary>
  //  public paItem2 pNextItem;

  //  public paItem2(paItem aPAI) 
  //  {
  //    _pai = aPAI;
  //    pNextItem = null;
  //  }

  //  public override string ToString()
  //  {
  //    if (pNextItem != null)
  //      return _pai.ToString() + " " + pNextItem.ToString();
  //    else
  //      return _pai.ToString();
  //  }

  //}
}
