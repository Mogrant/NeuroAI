using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SA3
{

    public partial class Form1 : Form
    {

        struct LearnEx
        {
            ArrayList Arr;
            ArrayList YCorrect;
            
            public LearnEx( ArrayList arr, ArrayList ycorrect)
            {
                this.Arr = arr;
                this.YCorrect = ycorrect;
            }

            public ArrayList LearnArray
            {
                get
                {
                    return this.Arr;
                }
                set
                {
                    this.Arr = value;
                }
            }

            public ArrayList LearnYCorrect
            {
                get
                {
                    return this.YCorrect;
                }
                set
                {
                    this.YCorrect = value;
                }
            }
        }
        
        List<LearnEx> learns ;

        Color defaultColor = Color.White;
        int Xcount = 10;
        List<InputNeuron> Xneurons = new List<InputNeuron>();
        //List<HideNeuron> Zneurons = new List<HideNeuron>();
        //List<OutputNeuron> Yneurons = new List<OutputNeuron>();
        //

        public Form1()
        {
            InitializeComponent();
        }

        private LearnEx AddLearnEx(string file, int resultY)
        {
            if (!System.IO.File.Exists("./" + file))
            {
                WriteLineConsole(Color.Red, "File : \"" + file + "\" is not exists!" );
            }
            System.Drawing.Image image;
            System.Drawing.Bitmap picture;
            int[,] picMatrix;
            try
            {
                image = Image.FromFile("./" + file);
                picture = new Bitmap(image);
                WriteLineConsole("bitmap created");
                picMatrix = new int[picture.Width, picture.Height];
                Color clr;
                for (int i = 0; i < picture.Width; i++)
                {
                    for (int j = 0; j < picture.Height; j++)
                    {
                        clr = picture.GetPixel(i, j);
                        if (clr.R > 0 && clr.G > 0 && clr.B > 0)
                        {
                            picMatrix[i, j] = 0;
                        }
                        else
                        {
                            picMatrix[i, j] = 1;
                        }
                    }
                }
            }
            catch
            {
                picMatrix = new int[0, 0];
                WriteLineConsole("Error load image");
            }

            ArrayList arr = new ArrayList();
            for (int i = 0; i < picMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < picMatrix.GetLength(1); j++)
                {
                    WriteConsole(" " + picMatrix[i, j]);
                    arr.Add(picMatrix[i, j]);

                }
                WriteLineConsole("");
            }
            ArrayList Ycorrect = new ArrayList();
            Ycorrect.Add(resultY);
            return new LearnEx(arr, Ycorrect);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < Xcount; i++)
            {
                Xneurons.Add(new InputNeuron());
            }
            learns = new List<LearnEx>();
            learns.Add(AddLearnEx("l1.png", 0));
            learns.Add(AddLearnEx("l2.png", 0));
            learns.Add(AddLearnEx("l3.png", 0));
        }

        private void WriteLineConsole(string str)
        {
            richTextBox1.AppendText(str + Environment.NewLine);
            richTextBox1.ScrollToCaret();
        }

        private void WriteConsole(string str)
        {
            richTextBox1.AppendText(str);
            richTextBox1.ScrollToCaret();
        }

        private void WriteLineConsole(Color clr, string str)
        {
            richTextBox1.SelectionColor = clr;
            richTextBox1.AppendText(str + Environment.NewLine);
            richTextBox1.ScrollToCaret();
            richTextBox1.SelectionColor = defaultColor;
        }

        private void WriteConsole(Color clr, string str)
        {
            richTextBox1.SelectionColor = clr;
            richTextBox1.AppendText(str);
            richTextBox1.ScrollToCaret();
            richTextBox1.SelectionColor = defaultColor;

        }

    }
}
