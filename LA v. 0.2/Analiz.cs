using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LA_Shtokal
{
    class Analiz : Input_Data
    {
        private bool new_idn = true;

        private int currentRow = 1;
        private int currentColumn = 1;
        private int currentSymb = 0;
        private string currentLexem = "";
        private int currentST = 0;

        public Error error { get; private set; }


        public List<RezultTable> rezultTable { get; private set; }= new List<RezultTable>();
        public List<IdentifiersTable> identifiersTable { get; private set; }= new List<IdentifiersTable>();
        public List<ConstTable> constTable { get; private set; }= new List<ConstTable>();


        public Analiz(string code):base(code)
        {
            if(code=="" ||code[0]!='P')
            {
                error = new Error(1, 1, "");
                return;
            }
            Move();
        }

        public Analiz():base()
        {
            Move();
        }

        private int Char_Type(char ch)
        {
            if (symbolTable["letter"].IsMatch(ch.ToString()))
            {
                return (int)ChCl.letter;
            }
            else if (symbolTable["digit"].IsMatch(ch.ToString()))
            {
                return (int)ChCl.digit;
            }
            else if (symbolTable["separ_one"].IsMatch(ch.ToString()))
            {
                return (int)ChCl.separ_one;
            }
            else if (symbolTable["separ_logic"].IsMatch(ch.ToString()))
            {
                return (int)ChCl.separ_logic;
            }
            else if (symbolTable["equare"].IsMatch(ch.ToString()))
            {
                return (int)ChCl.equare;
            }
            else if (symbolTable["space"].IsMatch(ch.ToString()))
            {
                return (int)ChCl.space;
            }
            else
            {
                return (int)ChCl.other;
            }
        }

        private void Move()
        {
            int CL;
            for (currentST = 0, currentSymb = 0; currentSymb != code.Length; currentSymb++)
            {
                CL = Char_Type(code[currentSymb]);
                currentST = automat_table[currentST, CL];

                if (currentST>=0)
                {
                    currentColumn++;
                    currentLexem += code[currentSymb];
                }
                else
                {
                    Actions();
                    currentST = 0;
                    currentLexem = "";
                }
                
            }
        }//головна функція руху по автомату

        private bool Actions()
        {
             switch (currentST)
             {
               case -1:
                    Action1();
                    break;
               case -2:
                    Action2();
                    break;
               case -3:
                    Action3();
                    break;
                case -4:
                    Action4();
                    break;
                case -5:
                    Action5();
                    break;
                case -6:
                    Action6();
                    break;
                case -7:
                    Action7();
                    break;
                default:
                    return false;
             }
                return true;
            }//фунція виклику підпрограм
        
        private void Action1()
        {
            int num_lexem = FindNumberLexem(lexemTable, currentLexem);//номер лексеми в таблиці лексем
            if (currentLexem == "begin") new_idn = false;
            if (num_lexem >= 0)
            {
                rezultTable.Add(new RezultTable(currentRow, currentLexem, num_lexem, 0));
            }
            else
            {
                int q= FindNumberIden(identifiersTable, currentLexem);
                if (q == -1)
                {
                    if (new_idn == false) error = new Error(currentRow, currentColumn, "Необ'явлений ідентифікатор");
                    identifiersTable.Add(new IdentifiersTable(currentLexem, identifiersTable.Count + 1));
                    rezultTable.Add(new RezultTable(currentRow, currentLexem, lexemTable.Count - 1, identifiersTable.Last().index));
                }
                else
                {
                    rezultTable.Add(new RezultTable(currentRow, currentLexem, lexemTable.Count - 1, q));
                }
            }
            currentSymb--;
           
        }

        private void Action2()
        {
            int q = FindNumberConst(constTable, currentLexem);
            if (q==-1)
            {
                constTable.Add(new ConstTable(currentLexem, constTable.Count() + 1));
                rezultTable.Add(new RezultTable(currentRow, currentLexem, lexemTable.Count, constTable.Count()));
            }
            else
            {
                rezultTable.Add(new RezultTable(currentRow, currentLexem, lexemTable.Count, q));
            }
            currentSymb--;
           
        }

        private void Action3()
        {
            currentLexem += code[currentSymb];
            int num_lexem = FindNumberLexem(lexemTable, currentLexem);
            rezultTable.Add(new RezultTable(currentRow, currentLexem, num_lexem, 0));
            currentColumn++;
          
        }

        private void Action4()
        {
            Action3();
        }

        private void Action5()
        {
            int num_lexem = FindNumberLexem(lexemTable, currentLexem);
            rezultTable.Add(new RezultTable(currentRow, currentLexem, num_lexem, 0));
            currentSymb--;
            
        }

        private void Action6()
        {
            currentLexem += code[currentSymb];
            if(currentLexem!="\r")
            {
                if (currentLexem == "\n")
                {
                    currentRow++;
                    currentColumn = 1;
                }
                else if (currentLexem == "\t")
                {
                    currentColumn += 4;
                }
                else currentColumn++;
            }     
        }

        private void Action7()
        {
            if(error==null)
            {
                error = new Error(currentRow, currentColumn, "Невідомий символ");
                currentColumn++;
            }
            
          
        }
    
         private int  FindNumberLexem(Dictionary<int,string> list,string currentLexem)
        {
            
            for (int i = 1; i <= list.Count; i++)
            {
                if (currentLexem == list[i])
                {
                    return i;
                }
            }
            return -1;
        }

         private int FindNumberIden(List<IdentifiersTable> list,string currentLexem)
        {
            
            for (int i = 0; i < identifiersTable.Count; i++)
            {
                if (currentLexem == identifiersTable[i].idn)
                {
                    return i + 1;
                }
            }
            return -1;
        }

        private int FindNumberConst(List<ConstTable> constTable,string currentLexem)
        {
            for (int i = 0; i < constTable.Count; i++)
            {
                if (currentLexem == constTable[i].constant)
                {
                    return i + 1;
                }
            }
            return -1;
        }

    }

     struct RezultTable
    {
        public int row { get; private set; }
        public string lexem { get; private set; }
        public int code { get; private set; }
        public int indexConst { get; private set; }
        public RezultTable(int row, string lexem, int code, int indexConst)
        {
            this.row = row;
            this.lexem = lexem;
            this.code = code;
            this.indexConst = indexConst;
        }
    }

     struct IdentifiersTable
    {
        public string idn { get; private set; }
        public int index { get; private set; }
        public IdentifiersTable(string idn, int index)
        {
            this.idn = idn;
            this.index = index;
        }
    }

     struct ConstTable
    {
        public string constant { get; private set; }
        public int index { get; private set; }
        public ConstTable(string constant,int index)
        {
            this.constant = constant;
            this.index = index;
           
        }
    }

     class Error
    {
        public int row { get; private set; }
        public int column { get; private set; }
        public string message { get; private set; }

        public Error(int row,int column,string message)
        {
            this.row = row;
            this.column = column;
            this.message = message;
        }
    }
}