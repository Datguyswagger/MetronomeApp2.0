using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MetronomeApp.Tempo;

namespace MetronomeApp
{
    public partial class frmMetronome : Form
    {
        public frmMetronome()
        {
            InitializeComponent();
        }

        Tempo start;
        bool answer;
        CancellationTokenSource cTokenSource = new CancellationTokenSource();

        private void btnPlay_Click(object sender, EventArgs e)
        {
            // Checks to see the state of the app to see if the metronome is running or not.
            bpmValidation(txtBPM.Text);
            if (answer == true)
            {
                setBPM(txtBPM.Text);
                setState("Play");

            } 
        }
        private void btnStop_Click(object sender, EventArgs e)
        {
            setState("Pause");
            cTokenSource.Cancel();
        }

        private void setState(string appState)
        {
            // Sets the state of the app to see if the metronome needs to run or not.
            
            switch (appState)
            {
                case "Play":
                    start.startTime(false);
                    btnPlay.Enabled = false;
                    btnStop.Enabled = true;
                    txtBPM.Enabled = false;
                    break;
                case "Pause":
                    start.startTime(true);
                    btnPlay.Enabled = true;
                    btnStop.Enabled = false;
                    txtBPM.Enabled = true;
                    break;
                default:
                    break;



            }
        }
        private void setBPM(string bpm)
        {
            // converts the target bpm into milliseconds so we can add it to the tempo class

            double tempo = 60000 / Convert.ToDouble(bpm);
            double downbeat = 4;
            start = new Tempo(tempo, downbeat);
        }
        // This makes sure that the user can only put in a numberic value, nothing else!
        private void txtBPM_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        // This validates that the tempo inputted is a number between 30 and 280
        private void bpmValidation(string bpm)
        {
            double tempo = Convert.ToDouble(bpm);

            if (tempo < 30)
            {
                MessageBox.Show("Please enter a number between 30 and 250.", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtBPM.Text = string.Empty;
                answer = false;
                
            }
            else if (tempo > 250)
            {
                MessageBox.Show("Please enter a number between 30 and 250.", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtBPM.Text = string.Empty;
                answer = false;
                
            }
            else
            {
                answer = true;
                
            }
        }
    } 
}