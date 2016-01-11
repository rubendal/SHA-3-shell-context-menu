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
    public partial class Verify : Form
    {

        string hash = "";

        public Verify()
        {
            InitializeComponent();
        }

        public Verify(string f, int bits)
        {
            byte[] data = File.ReadAllBytes(f);
            SHA3Managed sha = new SHA3Managed(bits);
            sha.Initialize();
            byte[] h = sha.ComputeHash(data);
            hash = BitConverter.ToString(h).Replace("-", "").ToLower();
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text.ToLower().Trim() == hash)
            {
                MessageBox.Show("This hash is identical to the file's hash");
            }
            else
            {
                MessageBox.Show("This hash is different to the file's hash");
            }
        }
    }
}
