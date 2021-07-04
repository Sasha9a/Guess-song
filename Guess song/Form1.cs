using System;
using System.Windows.Forms;

namespace Угадайка
{
    public partial class fMain : Form
    {
        fParam fp = new fParam();
        fGame fg = new fGame();
        public fMain()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnParam_Click(object sender, EventArgs e)
        {
            fp.ShowDialog();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            fg.ShowDialog();
        }

        private void fMain_Load(object sender, EventArgs e)
        {
            Victorina.ReadParam();
            Victorina.ReadMusic();
        }
    }
}
