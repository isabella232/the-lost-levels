using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TexturedQuadWindows
{
    public partial class LevelGUI : Form
    {
        public LevelGUI()
        {
            InitializeComponent();
        }

        private void LevelGUI_Load(object sender, EventArgs e)
        {

        }
        public int model_index;
        public void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.model_index = comboBox1.SelectedIndex;

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
