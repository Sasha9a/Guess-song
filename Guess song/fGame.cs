using System;
using System.Windows.Forms;
using System.Media;

namespace Угадайка
{
    public partial class fGame : Form
    {
        Random rnd = new Random();
        int MusicDuration = Victorina.musicDuration;
        bool[] players = new bool[2];
        public fGame()
        {
            InitializeComponent();
        }

        void MakeMusic()
        {
            if (Victorina.list.Count == 0) EndGame();
            else
            {
                MusicDuration = Victorina.musicDuration;
                int n = rnd.Next(0, Victorina.list.Count);
                WMP.URL = Victorina.list[n];
                Victorina.answer = WMP.URL;
                Victorina.list.RemoveAt(n);
                lblMelodyCount.Text = Victorina.list.Count.ToString();
                for (int i = 0; i != 2; i++) players[i] = false;
            }
        }

        void GamePause()
        {
            timer1.Stop();
            WMP.Ctlcontrols.pause();
        }

        void GamePlay()
        {
            timer1.Start();
            WMP.Ctlcontrols.play();
        }

        void EndGame()
        {
            timer1.Stop();
            WMP.Ctlcontrols.stop();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                if (progressBar1.Maximum == progressBar1.Value) return;
                timer1.Start();
                MakeMusic();
            }
            catch
            {
                MessageBox.Show("Вы не выбрали папку с композициями","Ошибка");
            }
        }

        private void fGame_FormClosed(object sender, FormClosedEventArgs e)
        {
            EndGame();
        }

        private void fGame_Load(object sender, EventArgs e)
        {
            lblMelodyCount.Text = Victorina.list.Count.ToString();
            progressBar1.Value = 0;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = Victorina.gameDuration;
            lblMusicDuration.Text = MusicDuration.ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Value++;
            MusicDuration--;
            lblMusicDuration.Text = MusicDuration.ToString();
            if(progressBar1.Value == progressBar1.Maximum)
            {
                EndGame();
                return;
            }
            if (MusicDuration == 0) MakeMusic();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            GamePause();
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            if (progressBar1.Maximum == progressBar1.Value) return;
            GamePlay();
        }

        private void fGame_KeyDown(object sender, KeyEventArgs e)
        {
            if (!timer1.Enabled) return;
            if(players[0] == false && e.KeyData == Keys.A)
            {
                GamePause();
                fMessage fm = new fMessage();
                fm.lblMessage.Text = "Игрок 1";
                SoundPlayer sp = new SoundPlayer("Resources\\1.wav");
                sp.PlaySync();
                players[0] = true;
                if (fm.ShowDialog() == DialogResult.Yes)
                {
                    lblCounter1.Text = Convert.ToString(Convert.ToInt32(lblCounter1.Text)+1);
                    MakeMusic();
                    progressBar1.Value = 0;
                }
                GamePlay();
            }
            if (players[1] == false && e.KeyData == Keys.P)
            {
                GamePause();
                fMessage fm = new fMessage();
                fm.lblMessage.Text = "Игрок 2";
                SoundPlayer sp = new SoundPlayer("Resources\\2.wav");
                sp.PlaySync();
                players[1] = true;
                if (fm.ShowDialog() == DialogResult.Yes)
                {
                    lblCounter2.Text = Convert.ToString(Convert.ToInt32(lblCounter2.Text) + 1);
                    MakeMusic();
                    progressBar1.Value = 0;
                }
                GamePlay();
            }
        }

        private void WMP_OpenStateChange(object sender, AxWMPLib._WMPOCXEvents_OpenStateChangeEvent e)
        {
            if (Victorina.randomStart)
                if (WMP.openState == WMPLib.WMPOpenState.wmposMediaOpen)
                    WMP.Ctlcontrols.currentPosition = rnd.Next(0, (int)WMP.currentMedia.duration / 2);
        }

        private void lblCounter1_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left) (sender as Label).Text = Convert.ToString(Convert.ToInt32((sender as Label).Text) + 1);
            if (e.Button == MouseButtons.Right) (sender as Label).Text = Convert.ToString(Convert.ToInt32((sender as Label).Text) - 1);
        }
    }
}
