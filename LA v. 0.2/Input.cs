using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;


namespace LA_Shtokal
{
    class Input_Data
    {
        protected  string code = "";

        protected  Dictionary<int, string> lexemTable = new Dictionary<int, string>();

        protected  Dictionary<string, Regex> symbolTable = new Dictionary<string, Regex>();
 
        protected int[,] automat_table;

        protected enum ChCl
        {
            letter,digit,separ_one, separ_logic, equare,space,other
        }

        protected Input_Data(string code_program):this()
        {
            code = code_program;
        }
        protected Input_Data()
        {
            code = "Program qq;";

            InitLexemTable();
            InitSymbolTable();

            automat_table = new int[,] {{ 1, 2, -3,  3,  4, -6, -7 },
                                        { 1, 1, -1, -1, -1, -1, -1 },
                                        {-7, 2, -2, -2, -2, -2, -2 },
                                        {-5,-5, -5, -5, -4, -5, -5 },
                                        {-5,-5, -5, -5, -4, -5, -5 }};
        }

        private  void InitLexemTable()
        {
            lexemTable.Add(1, "Program");
            lexemTable.Add(2, "var");
            lexemTable.Add(3, "begin");
            lexemTable.Add(4, "end");
            lexemTable.Add(5, "end.");
            lexemTable.Add(6, "integer");
            lexemTable.Add(7, "real");
            lexemTable.Add(8, "char");
            lexemTable.Add(9, "for");
            lexemTable.Add(10, "if");
            lexemTable.Add(11, "else");
            lexemTable.Add(12, "read");
            lexemTable.Add(13, "write");
            lexemTable.Add(14, "or");
            lexemTable.Add(15, "and");
            lexemTable.Add(16, ";");
            lexemTable.Add(17, ":");
            lexemTable.Add(18, ",");
            lexemTable.Add(19, "(");
            lexemTable.Add(20, ")");
            lexemTable.Add(21, "[");
            lexemTable.Add(22, "]");
            lexemTable.Add(23, "{");
            lexemTable.Add(24, "}");
            lexemTable.Add(25, "=");
            lexemTable.Add(26, "+");
            lexemTable.Add(27, "-");
            lexemTable.Add(28, "*");
            lexemTable.Add(29, ">");
            lexemTable.Add(30, "<");
            lexemTable.Add(31, ">=");
            lexemTable.Add(32, "<=");
            lexemTable.Add(33, "==");
            lexemTable.Add(34, "!=");
            lexemTable.Add(35, "!");
            lexemTable.Add(36, "^");
            lexemTable.Add(37, "ind");
            lexemTable.Add(38, "const");
        }
        private  void InitSymbolTable()
        {
            Regex letter = new Regex("[a-zA-Z]");
            Regex digit = new Regex("[0-9]");
            Regex separ_one = new Regex("[(|)|\\[|\\]|\\{|\\}|,|;|:|+|\\-|*|/|\\^|.]");
            Regex separ_logic = new Regex("[>|<|!]");
            Regex equare = new Regex("[=]");
            Regex space = new Regex(@"\s");

            symbolTable.Add("letter",letter);
            symbolTable.Add("digit", digit);
            symbolTable.Add("separ_one", separ_one);
            symbolTable.Add("separ_logic", separ_logic);
            symbolTable.Add("equare", equare);
            symbolTable.Add("space", space);

        }

        

    }
}