using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;

namespace Alinhamentos
{
    public partial class Form1 : Form
    {
        public OpenFileDialog FileOpen;
        public RichTextBox rt1, rt2, rt3;
        public DataGridView dtView;

        /// <summary>
        /// Variaveis da instancia
        /// </summary>
        private static Form1 instance = null;
        private static readonly object pLock = new object();
        /// <summary>
        /// Gera a instancia
        /// </summary>
        public static Form1 Instance
        {
            get
            {
                lock (pLock)
                {
                    return instance ?? (instance = new Form1());
                }
            }
        }

        public Form1()
        {
            try
            {
                instance = this;
                InitializeComponent();
                FileOpen = openFileDialog1;
                rt1 = richTextBox1;
                rt2 = richTextBox2;
                rt3 = richTextBox3;
                dtView = dataGridView1;
            }
            catch (Exception e)
            {
                MyException(e.Message, "Ocorreu um erro",MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (FileOpen.ShowDialog() == DialogResult.OK)
            {
                string fasta1 = System.IO.File.ReadAllText(FileOpen.FileName);
                if (SelectFastaDialog())
                {
                    rt2.Text = Regex.Replace(fasta1, @"\t|\n|\r", "");
                }
                else
                {
                    rt1.Text = Regex.Replace(fasta1, @"\t|\n|\r", "");
                }
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Align.Instance.AlignLocalFastas();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Align.Instance.AlignGlobalFastas();
        }

        private static bool SelectFastaDialog()
        {
            var select = new Form();
            var selection = false;
            select.Width = 400;
            select.Height = 100;
            select.Text = "Selecione a sequencia para assinalar.";
            var firstOption = new Button(){Text= "Sequência 1", Left = 30, Width = 100, Top = 20};
            firstOption.Click += (sender, e) =>
            {
                selection = false;
                select.Close();
            };
            select.Controls.Add(firstOption);
            var secondOption = new Button() { Text = "Sequência 2", Left = 130, Width = 100, Top = 20 };
            secondOption.Click += (sender, e) =>
            {
                selection = true;
                select.Close();
            };
            select.Controls.Add(secondOption);
            select.ShowDialog();
            return selection;
        }
        
        public void MyException(string message, string caption, MessageBoxIcon icon)
        {
            string text = message;
            MessageBoxButtons button = MessageBoxButtons.OK;
            MessageBox.Show(text, caption, button, icon);
        }
    }
}
