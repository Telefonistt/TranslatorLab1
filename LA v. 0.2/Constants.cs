using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LA_Shtokal
{
    class Constants
    {
        static public string[] LexemTable { get; private set; } = {"Program",
                                                                   "program",
                                                                   "var",
                                                                   "begin",
                                                                   "end",
                                                                   "integer",
                                                                   "for",
                                                                   "if",
                                                                   "read",
                                                                   "write",
                                                                   "or",
                                                                   "and",
                                                                   ";",
                                                                   ":",
                                                                   ",",
                                                                   "(",
                                                                   ")",
                                                                   "{",
                                                                   "}",
                                                                   "=",
                                                                   "+",
                                                                   "-",
                                                                   "*",
                                                                   "/",
                                                                   "^",
                                                                   ">",
                                                                   "<",
                                                                   ">=",
                                                                   "<=",
                                                                   "==",
                                                                   "?",
                                                                   "idn",
                                                                   "const"
                                                                   };

        static public int[,] AutomatTable { get; private set; } = {{ 1, 2, -3,  3,  4, -6, -7 },
                                                                   { 1, 1, -1, -1, -1, -1, -1 },
                                                                   {-2, 2, -2, -2, -2, -2, -2 },
                                                                   {-5,-5, -5, -5, -4, -5, -5 },
                                                                   {-5,-5, -5, -5, -4, -5, -5 }};

        static public string[] SymbolClassTable { get; private set; } = {"[a-zA-Z]",
                                                                         "[0-9]",
                                                                         "[(|)|\\{|\\}|,|;|:|+|\\-|*|/|\\^|\\?]",
                                                                         "[>|<|\\!]",
                                                                         "[=]",
                                                                         @"\s"
                                                                         };
    }
}