using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;

public static class ExtensionMethods
{

    public static void Equilength(this List<string> list)      {   int maxLen= list.Max( s=> s.Length );
                                                                    for(int i=0;i<list.Count;++i)
                                                                        list[i] += new string(' ',maxLen-(list[i].Length));                                             }

    public static string Print(this double x)                  {    return (x<0d ? "-":" ") + Math.Abs(x).ToString("00.00");                                            }


}
