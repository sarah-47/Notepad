using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotePad
{
    public partial class Form1 : Form
    {
        string filePath = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Title = "Open";
            fd.Filter = "Text Document(*.txt)|*.txt|All Files(*.*)|*.*";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                using (StreamReader sr = new StreamReader(fd.FileName))
                {
                    filePath = fd.FileName;
                    Task<string> text = sr.ReadToEndAsync();
                    richTextBox1.Text = text.Result;

                }
            }

            //richTextBox1.LoadFile(fd.FileName, RichTextBoxStreamType.PlainText);
            // this.Text = fd.FileName;

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                SaveFileDialog fd = new SaveFileDialog();
                fd.Title = "Save";
                fd.Filter = "Text Document(*.txt)|*.txt|All Files(*.*)|*.*";
                this.Text = fd.FileName;

                if (fd.ShowDialog() == DialogResult.OK)
                {
                    using (StreamWriter sw = new StreamWriter(fd.FileName))
                    {
                        sw.WriteLineAsync(richTextBox1.Text);
                    }
                }
            }
            // richTextBox1.SaveFile(fd.FileName, RichTextBoxStreamType.PlainText);
            else
            {
                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    sw.WriteLineAsync(richTextBox1.Text);

                }
            }
        }   
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog fd = new SaveFileDialog();
            fd.Title = "Save As";
            fd.Filter = "Text Document(*.txt)|*.txt|All Files(*.*)|*.*";
            fd.FileName = filePath;
            this.Text = fd.FileName;

            if (fd.ShowDialog() == DialogResult.OK)
                richTextBox1.SaveFile(fd.FileName, RichTextBoxStreamType.PlainText);
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Close();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Redo();
        }

        private void selectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }

        private void timeDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text +=" "+ System.DateTime.Now.ToString();
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fd=new FontDialog();
            if(fd.ShowDialog() == DialogResult.OK)
                richTextBox1.Font = fd.Font;
        }
        /*
        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog fd = new ColorDialog();
            if (fd.ShowDialog() == DialogResult.OK)
                richTextBox1.ForeColor = fd.Color;
        }

        private void backColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog fd = new ColorDialog();
            if (fd.ShowDialog() == DialogResult.OK)
                richTextBox1.BackColor = fd.Color;
        }
        */
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Are You Sure You Want To Exit?",
                      "Colse Window",
                       MessageBoxButtons.YesNo,
                       MessageBoxIcon.Information) == DialogResult.No)
                       e.Cancel = true;
            }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

        }
    }
}
