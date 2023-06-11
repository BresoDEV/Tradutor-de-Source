using System.Text.RegularExpressions;
using static System.Windows.Forms.LinkLabel;

namespace Obf
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Breso.Temas.Dark(this);
        }





        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if(ofd.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text= ofd.FileName;
                if(ofd.FileName.Contains(".cpp"))
                    textBox2.Text= ofd.FileName.Replace(".cpp","2.cpp");
                if (ofd.FileName.Contains(".h"))
                    textBox2.Text = ofd.FileName.Replace(".h", "2.h");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Cursor= Cursors.WaitCursor;
            string tn = "";
            using(StreamReader sr = new StreamReader(textBox1.Text))
            {
                string linha;
                while((linha= sr.ReadLine()) != null)
                {
                    if (StringValida(linha))
                    {
                        string ls = Regex.Replace(linha, "\"(.*?)\"", "~Traducao::" + RemoverEspacosUnderline(RetornarTexto(linha)) + "~");

                        tn += ls.Replace("~", "") + Environment.NewLine;
                    }
                    else
                    {
                        tn += linha + Environment.NewLine;
                    }
                }
            }
            File.WriteAllText(textBox2.Text, tn);
            Cursor = Cursors.Default;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            textBox3.Text = "";
            string tn = "";
            using (StreamReader sr = new StreamReader(textBox1.Text))
            {
                string linha;
                while ((linha = sr.ReadLine()) != null)
                {
                    if (StringValida(linha))
                    {
                        string ls = Regex.Replace(linha, "\"(.*?)\"", "~const char* "+ RemoverEspacosUnderline(RetornarTexto(linha)) + " = \"" + RetornarTexto(linha) + "\";~");

                        string[] s2 = ls.Split('~');
                        textBox3.AppendText(s2[1] + Environment.NewLine);

                        tn += ls + Environment.NewLine;
                    }
                }
            }
            Cursor = Cursors.Default;
        }

        static bool StringValida(string linha)
        {
            if (linha.Contains('"') && linha.Contains('#') == false && linha.Contains('/') == false)
            {
                if(!linha.Contains("\"\""))
                    return true; 
                return false;
            }
            return false;
        }

        static string RetornarTexto(string linha)
        {
            string[] s = linha.Split('"');
            return s[1];
        }

        static string RemoverEspacos(string linha)
        {
           return linha.Replace(" ", "");
        }

        static string RemoverEspacosUnderline(string linha)
        {
            return linha.Replace(" ", "_");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            string tn = "";
            using (StreamReader sr = new StreamReader(textBox1.Text))
            {
                textBox3.Text = "";
                string linha;
                while ((linha = sr.ReadLine()) != null)
                {
                    if (StringValida(linha))
                    {
                        string ls = Regex.Replace(linha, "\"(.*?)\"", "~" + RemoverEspacosUnderline(RetornarTexto(linha)) + "=" + RetornarTexto(linha) + "~");

                        string[] s2 = ls.Split('~');
                        textBox3.AppendText(s2[1] + Environment.NewLine);

                        tn += ls + Environment.NewLine;
                    }
                }
            }
            Cursor = Cursors.Default;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}