using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using SHA3;

namespace SHA_3
{
    public partial class Calculate : Form
    {
        string hash = "";
        string file = "";

        public Calculate()
        {
            InitializeComponent();
        }

        public Calculate(string f, int bits)
        {
            byte[] data = File.ReadAllBytes(f);
            SHA3Managed sha = new SHA3Managed(bits);
            file = f;
            sha.Initialize();
            byte[] h = sha.ComputeHash(data);
            hash = BitConverter.ToString(h).Replace("-","").ToLower();
            InitializeComponent();
            textBox1.Text = hash;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = Path.GetFileNameWithoutExtension(file) + "_Digest.txt";
            DialogResult res = saveFileDialog1.ShowDialog();
            if(res == DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog1.FileName, "SHA-3\r\n\r\n" + hash + " \t\t " + Path.GetFileName(file));
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(hash);
        }
    }
}
