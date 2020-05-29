using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alinhamentos
{
    public class Align
    {
        /// <summary>
        /// Variaveis da instancia
        /// </summary>
        private static Align instance = null;

        private static readonly object pLock = new object();

        /// <summary>
        /// Gera a instancia
        /// </summary>
        public static Align Instance
        {
            get
            {
                lock (pLock)
                {
                    if (instance is null)
                    {
                        instance = new Align();
                    }

                    return instance;
                }
            }
        }

        public Align()
        {
            instance = this;
        }


        public readonly int[,] pam250 =
        {
            //0  1  2  3  4  5  6  7  8  9  10  11 12 13 14 15 16 17 18 19
            //A  C  D  E  F  G  H  I  K  L  M   N  P  Q  R  S  T  V  W  Y
            {2, -2, 0, 0, -3, 1, -1, -1, -1, -2, -1, 0, 1, 0, -2, 1, 1, 0, -6, -3},
            {-2, 12, -5, -5, -4, -3, -3, -2, -5, -6, -5, -4, -3, -5, -4, 0, -2, -2, -8, 0},
            {0, -5, 4, 3, -6, 1, 1, -2, 0, -4, -3, 2, -1, 2, -1, 0, 0, -2, -7, -4},
            {0, -5, 3, 4, -5, 0, 1, -2, 0, -3, -2, 1, -1, 2, -1, 0, 0, -2, -7, -4},
            {-3, -4, -6, -5, 9, -5, -2, 1, -5, 2, 0, -3, -5, -5, -4, -3, -3, -1, 0, 7},
            {1, -3, 1, 0, -5, 5, -2, -3, -2, -4, -3, 0, 0, -1, -3, 1, 0, -1, -7, -5},
            {-1, -3, 1, 1, -2, -2, 6, -2, 0, -2, -2, 2, 0, 3, 2, -1, -1, -2, -3, 0},
            {-1, -2, -2, -2, 1, -3, -2, 5, -2, 2, 2, -2, -2, -2, -2, -1, 0, 4, -5, -1},
            {-1, -5, 0, 0, -5, -2, 0, -2, 5, -3, 0, 1, -1, 1, 3, 0, 0, -2, -3, -4},
            {-2, -6, -4, -3, 2, -4, -2, 2, -3, 6, 4, -3, -3, -2, -3, -3, -2, 2, -2, -1},
            {-1, -5, -3, -2, 0, -3, -2, 2, 0, 4, 6, -2, -2, -1, 0, -2, -1, 2, -4, -2},
            {0, -4, 2, 1, -3, 0, 2, -2, 1, -3, -2, 2, 0, 1, 0, 1, 0, -2, -4, -2},
            {1, -3, -1, -1, -5, 0, 0, -2, -1, -3, -2, 0, 6, 0, 0, 1, 0, -1, -6, -5},
            {0, -5, 2, 2, -5, -1, 3, -2, 1, -2, -1, 1, 0, 4, 1, -1, -1, -2, -5, -4},
            {-2, -4, -1, -1, -4, -3, 2, -2, 3, -3, 0, 0, 0, 1, 6, 0, -1, -2, 2, -4},
            {1, 0, 0, 0, -3, 1, -1, -1, 0, -3, -2, 1, 1, -1, 0, 2, 1, -1, -2, -3},
            {1, -2, 0, 0, -3, 0, -1, 0, 0, -2, -1, 0, 0, -1, -1, 1, 3, 0, -5, -3},
            {0, -2, -2, -2, -1, -1, -2, 4, -2, 2, 2, -2, -1, -2, -2, -1, 0, 4, -6, -2},
            {-6, -8, -7, -7, 0, -7, -3, -5, -3, -2, -4, -4, -6, -5, 2, -2, -5, -6, 17, 0},
            {-3, 0, -4, -4, 7, -5, 0, -1, -4, -1, -2, -2, -5, -4, -4, -3, -3, -2, 0, 10}
        };

        public readonly int[,] blosum62 =
        {
            //0  1  2  3  4  5  6  7  8  9  10  11 12 13 14 15 16 17 18 19
            //A  C  D  E  F  G  H  I  K  L  M   N  P  Q  R  S  T  V  W  Y
            {4, 0, -2, -1, -2, 0, -2, -1, -1, -1, -1, -2, -1, -1, -1, 1, 0, 0, -3, -2},
            {0, 9, -3, -4, -2, -3, -3, -1, -3, -1, -1, -3, -3, -3, -3, -1, -1, -1, -2, -2},
            {-2, -3, 6, 2, -3, -1, -1, -3, -1, -4, -3, 1, -1, 0, -2, 0, -1, -3, -4, -3},
            {-1, -4, 2, 5, -3, -2, 0, -3, 1, -3, -2, 0, -1, 2, 0, 0, -1, -2, -3, -2},
            {-2, -2, -3, -3, 6, -3, -1, 0, -3, 0, 0, -3, -4, -3, -3, -2, -2, -1, 1, 3},
            {0, -3, -1, -2, -3, 6, -2, -4, -2, -4, -3, 0, -2, -2, -2, 0, -2, -3, -2, -3},
            {-2, -3, -1, 0, -1, -2, 8, -3, -1, -3, -2, 1, -2, 0, 0, -1, -2, -3, -2, 2},
            {-1, -1, -3, -3, 0, -4, -3, 4, -3, 2, 1, -3, -3, -3, -3, -2, -1, 3, -3, -1},
            {-1, -3, -1, 1, -3, -2, -1, -3, 5, -2, -1, 0, -1, 1, 2, 0, -1, -2, -3, -2},
            {-1, -1, -4, -3, 0, -4, -3, 2, -2, 4, 2, -3, -3, -2, -2, -2, -1, 1, -2, -1},
            {-1, -1, -3, -2, 0, -3, -2, 1, -1, 2, 5, -2, -2, 0, -1, -1, -1, 1, -1, -1},
            {-2, -3, 1, 0, -3, 0, 1, -3, 0, -3, -2, 6, -2, 0, 0, 1, 0, -3, -4, -2},
            {-1, -3, -1, -1, -4, -2, -2, -3, -1, -3, -2, -2, 7, -1, -2, -1, -1, -2, -4, -3},
            {-1, -3, 0, 2, -3, -2, 0, -3, 1, -2, 0, 0, -1, 5, 1, 0, -1, -2, -2, -1},
            {-1, -3, -2, 0, -3, -2, 0, -3, 2, -2, -1, 0, -2, 1, 5, -1, -1, -3, -3, -2},
            {1, -1, 0, 0, -2, 0, -1, -2, 0, -2, -1, 1, -1, 0, -1, 4, 1, -2, -3, -2},
            {0, -1, -1, -1, -2, -2, -2, -1, -1, -1, -1, 0, -1, -1, -1, 1, 5, 0, -2, -2},
            {0, -1, -3, -2, -1, -3, -3, 3, -2, 1, 1, -3, -2, -2, -3, -2, 0, 4, -3, -1},
            {-3, -2, -4, -3, 1, -2, -2, -3, -3, -2, -1, -4, -4, -2, -3, -3, -2, -3, 11, 2},
            {-2, -2, -3, -2, 3, -3, 2, -1, -2, -1, -1, -2, -3, -1, -2, -2, -2, -1, 2, 7},
        };

        public int[,] analizeMatrix;

        public List<char> AnalizeFasta(string FastaString)
        {
            List<char> amino = new List<char>();
            foreach (char c in FastaString)
            {
                if (!amino.Contains(c))
                {
                    amino.Add(c);
                }
            }

            return amino;
        }

        public void AlignLocalFastas()
        {
            if (Form1.Instance.rt1.Text == "" || Form1.Instance.rt2.Text == "")
            {
                Form1.Instance.MyException(
                    "Ambas as sequências devem ser carregadas para alinhar, por favor carregue-as", "Atenção",
                    MessageBoxIcon.Asterisk);
                return;
            }

            analizeMatrix = new int[Form1.Instance.rt1.Text.Length, Form1.Instance.rt2.Text.Length];
            Form1.Instance.MyException("A matriz de pontuação será gerada, isso pode demorar", "Atenção",
                MessageBoxIcon.Asterisk);
            for (var i = 0; i < analizeMatrix.GetLength(0); i++)
            {
                analizeMatrix[i, 0] = 0;
            }

            for (var i = 0; i < analizeMatrix.GetLength(1); i++)
            {
                analizeMatrix[0, i] = 0;
            }

            for (var j = 1; j < analizeMatrix.GetLength(1); j++)
            {
                for (var i = 1; i < analizeMatrix.GetLength(0); i++)
                {
                    analizeMatrix[i, j] = CalculateMax(i, j, 5);
                }
            }

            Form1.Instance.MyException("A matriz de pontuação foi gerada", "Finalizado", MessageBoxIcon.Asterisk);
            var pos = FindMaxValue();
            Form1.Instance.rt3.AppendText("Maior valor da matriz na posição: " + pos[0] + "," + pos[1] +
                                          Environment.NewLine);
            int m = pos[0], n = pos[1];
            var finished = true;
            string align1 = "", align2 = "";
            Form1.Instance.MyException(
                "O processo de busca do melhor alinhamento local será iniciado, isso pode demorar", "Atenção",
                MessageBoxIcon.Asterisk);
            var pontAlig = 0;
            align1 += Form1.Instance.rt1.Text[m];
            align2 += Form1.Instance.rt2.Text[n];
            pontAlig += analizeMatrix[m, n];
            while (finished)
            {
                if (analizeMatrix[m - 1, n - 1] > 0)
                {
                    align1 += Form1.Instance.rt1.Text[m - 1];
                    align2 += Form1.Instance.rt2.Text[n - 1];
                    pontAlig += analizeMatrix[m - 1, n - 1];
                    m--;
                    n--;
                }
                else if (analizeMatrix[m - 1, n - 1] == 0)
                {
                    align1 += Form1.Instance.rt1.Text[m - 1];
                    align2 += Form1.Instance.rt2.Text[n - 1];
                    pontAlig += analizeMatrix[m - 1, n - 1];
                    m--;
                    n--;
                    finished = false;
                }
            }

            Form1.Instance.rt3.AppendText("Sequencia 1:" + Reverse(align1) + Environment.NewLine);
            Form1.Instance.rt3.AppendText("Sequencia 2:" + Reverse(align2) + Environment.NewLine);
            Form1.Instance.rt3.AppendText("Pontuação do alinhamento:" + pontAlig + Environment.NewLine);
            ShowPontM();
        }

        public void AlignGlobalFastas()
        {
            if (Form1.Instance.rt1.Text == "" || Form1.Instance.rt2.Text == "")
            {
                Form1.Instance.MyException(
                    "Ambas as sequências devem ser carregadas para alinhar, por favor carregue-as", "Atenção",
                    MessageBoxIcon.Asterisk);
                return;
            }
            analizeMatrix = new int[Form1.Instance.rt1.Text.Length, Form1.Instance.rt2.Text.Length];
            Form1.Instance.MyException("A matriz de pontuação será gerada, isso pode demorar", "Atenção",
                MessageBoxIcon.Asterisk);
            for (var i = 0; i < analizeMatrix.GetLength(0); i++)
            {
                analizeMatrix[i, 0] = (-5) * i;
            }

            for (var i = 0; i < analizeMatrix.GetLength(1); i++)
            {
                analizeMatrix[0, i] = (-5) * i;
            }

            for (var j = 1; j < analizeMatrix.GetLength(1); j++)
            {
                for (var i = 1; i < analizeMatrix.GetLength(0); i++)
                {
                    analizeMatrix[i, j] = CalculateMaxGlobal(i, j, 5);
                }
            }

            Form1.Instance.MyException("A matriz de pontuação foi gerada", "Finalizado", MessageBoxIcon.Asterisk);
            ShowPontM();
            string align1 = "", align2 = "";
            Form1.Instance.MyException(
                "O processo de busca do melhor alinhamento global será iniciado, isso pode demorar", "Atenção",
                MessageBoxIcon.Asterisk);
            var pontAlig = 0;
            int m = analizeMatrix.GetLength(0) - 1, n = analizeMatrix.GetLength(1) - 1;
            align1 += Form1.Instance.rt1.Text[m];
            align2 += Form1.Instance.rt2.Text[n];
            while (m > 0 && n > 0)
            {
                var temp1 = Math.Max(analizeMatrix[m - 1, n - 1], analizeMatrix[m, n - 1]);
                var temp2 = Math.Max(temp1, analizeMatrix[m - 1, n]);
                if (analizeMatrix[m - 1, n - 1] == temp2)
                {
                    align1 += Form1.Instance.rt1.Text[m - 1];
                    align2 += Form1.Instance.rt2.Text[n - 1];
                    pontAlig += analizeMatrix[m - 1, n - 1];
                    m--;
                    n--;
                }
                else if (analizeMatrix[m, n - 1] == temp2)
                {
                    align1 += "-";
                    align2 += Form1.Instance.rt2.Text[n - 1];
                    pontAlig += analizeMatrix[m, n - 1];
                    n--;
                }
                else if (analizeMatrix[m - 1, n] == temp2)
                {
                    align1 += Form1.Instance.rt1.Text[m - 1];
                    align2 += "-";
                    pontAlig += analizeMatrix[m - 1, n];
                    m--;
                }
            }

            Form1.Instance.rt3.AppendText("Sequencia 1:" + Reverse(align1) + Environment.NewLine);
            Form1.Instance.rt3.AppendText("Sequencia 2:" + Reverse(align2) + Environment.NewLine);
            Form1.Instance.rt3.AppendText("Pontuação do alinhamento:" + pontAlig + Environment.NewLine);
        }

        public int CalculateMax(int i, int j, int penalty)
        {
            var diagonal = analizeMatrix[i - 1, j - 1] +
                           VerifyMatch(Form1.Instance.rt1.Text[i], Form1.Instance.rt2.Text[j]);
            var left = analizeMatrix[i - 1, j] - penalty;
            var up = analizeMatrix[i, j - 1] - penalty;

            var temp1 = Math.Max(diagonal, up);
            var temp2 = Math.Max(temp1, left);
            return Math.Max(0, temp2);
        }

        public int CalculateMaxGlobal(int i, int j, int penalty)
        {
            var diagonal = analizeMatrix[i - 1, j - 1] +
                           VerifyMatch(Form1.Instance.rt1.Text[i], Form1.Instance.rt2.Text[j]);
            var left = analizeMatrix[i - 1, j] - penalty;
            var up = analizeMatrix[i, j - 1] - penalty;

            var temp1 = Math.Max(diagonal, up);
            return Math.Max(temp1, left);
        }

        public int VerifyMatch(char n1, char n2)
        {
            return pam250[ChangeToValue(n1), ChangeToValue(n2)];
        }

        public int ChangeToValue(char n)
        {
            var result = 0;
            switch (n)
            {
                case 'A':
                case 'a':
                    result = 0;
                    break;
                case 'C':
                case 'c':
                    result = 1;
                    break;
                case 'D':
                case 'd':
                    result = 2;
                    break;
                case 'E':
                case 'e':
                    result = 3;
                    break;
                case 'F':
                case 'f':
                    result = 4;
                    break;
                case 'G':
                case 'g':
                    result = 5;
                    break;
                case 'H':
                    result = 6;
                    break;
                case 'I':
                    result = 7;
                    break;
                case 'K':
                    result = 8;
                    break;
                case 'L':
                    result = 9;
                    break;
                case 'M':
                    result = 10;
                    break;
                case 'N':
                    result = 11;
                    break;
                case 'P':
                    result = 12;
                    break;
                case 'Q':
                    result = 13;
                    break;
                case 'R':
                    result = 14;
                    break;
                case 'S':
                    result = 15;
                    break;
                case 'T':
                case 't':
                    result = 16;
                    break;
                case 'V':
                    result = 17;
                    break;
                case 'W':
                    result = 18;
                    break;
                case 'Y':
                    result = 19;
                    break;
            }

            return result;
        }

        public int[] FindMaxValue()
        {
            int temp, x, y;
            x = 0;
            y = 0;
            temp = 0;
            for (var j = 1; j < analizeMatrix.GetLength(1); j++)
            {
                for (var i = 1; i < analizeMatrix.GetLength(0); i++)
                {
                    if (analizeMatrix[i, j] <= temp) continue;
                    temp = analizeMatrix[i, j];
                    x = i;
                    y = j;
                }
            }

            var r = new[] {x, y};
            return r;
        }

        public string Reverse(string text)
        {
            if (text == null) return null;

            // this was posted by petebob as well 
            var array = text.ToCharArray();
            Array.Reverse(array);
            return new string(array);
        }

        public void ShowPontM()
        {
            Form1.Instance.dtView.Columns.Clear();
            Form1.Instance.dtView.Rows.Clear();
            Form1.Instance.dtView.ColumnCount = analizeMatrix.GetLength(1);
            for (var i = 0; i < analizeMatrix.GetLength(0); i++) // array rows
            {
                var row = new string[analizeMatrix.GetLength(1)];

                for (var j = 0; j < analizeMatrix.GetLength(1); j++)
                {
                    row[j] = analizeMatrix[i, j].ToString();
                }

                Form1.Instance.dtView.Rows.Add(row);
            }
        }
    }
}