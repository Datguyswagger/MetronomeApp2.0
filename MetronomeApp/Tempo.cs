using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;
using PrecisionTiming;
using TimerSink;

namespace MetronomeApp
{
    internal class Tempo
    {
        // I need to make a Timing class and need to figure out how they work.
        // Stopwatch baseTime = new Stopwatch();
        // DispatcherTimer metronomeTime = new DispatcherTimer();
        PrecisionTimer precisionTimer = new PrecisionTimer();
        PrecisionTimer downbeatTimer = new PrecisionTimer();
        // TimingSink timer = new TimingSink();
        System.Media.SoundPlayer downbeatSound = new System.Media.SoundPlayer(@"C:\Users\josep\Downloads\MetronomeApp-7ded28f6b9326be4a94f38a064534d3b837165b9\MetronomeApp-7ded28f6b9326be4a94f38a064534d3b837165b9\MetronomeApp\Resources\Voices\Metronomes\Synth_Square_E_hi.wav");
        System.Media.SoundPlayer baseSound = new System.Media.SoundPlayer(@"C:\Users\josep\Downloads\MetronomeApp-7ded28f6b9326be4a94f38a064534d3b837165b9\MetronomeApp-7ded28f6b9326be4a94f38a064534d3b837165b9\MetronomeApp\Resources\Voices\Metronomes\Synth_Square_E_lo.wav");
        private bool metStart;
        private bool running;
        private double metTempo;
        private int metTempoCount;
        private double metDownBeat;
        private double metDownBeatCalc;
        private int counter = 1;

        public bool MetStart
        {
            get => metStart;
            set => metStart = value;
        }

        public double MetTempo
        {
            get => metTempo;
            set => metTempo = value;
        }
        public double MetDownBeat
        {
            get => metDownBeat;
            set => metDownBeat = value;
        }
        public Tempo(double tempo, double downbeat) 
        {
            metTempo = tempo;
            metDownBeat = downbeat;
            metStart = false;
            running = false;
            downbeatCalc(metTempo, metDownBeat);
        }
        public double downbeatCalc(double metTempo, double metDownBeat)
        {
            double metdownbeatCalc = metTempo * metDownBeat;
            return metdownbeatCalc;
        }

        public void startTime(bool what)
        {
            if (what == false)
            {
                // MessageBox.Show("Start");
                // running = true;
                // metronomeTime.Interval = TimeSpan.FromMilliseconds(tempo);
                // metronomeTime.Tick += new EventHandler(sound_test);
                // metronomeTime.Start();
                //metronomeTime.IsEnabled = true;
                //precisionTimer.SetPeriod(500);
                //precisionTimer.Tick += new EventHandler(sound_test);

                precisionTimer.SetInterval(baseSound.PlaySync, (int)metTempo, true);
                //downbeatTimer.SetInterval(downbeatSound.PlaySync, (int)metTempo, true);  
                precisionTimer.Start();
                //downbeatTimer.Start();

                // MessageBox.Show(precisionTimer.IsRunning().ToString());

                // I need to make a loop that stops when the timer is stopped.
                // I know that I need to make the timer period equal to the tempo
                // I also know that I need to make the player switch sounds every fourth time and then switch back.
                


            }

            if (what == true)
            {
                // MessageBox.Show("Stop");
                // MessageBox.Show(precisionTimer.IsRunning().ToString());
                precisionTimer.SetInterval(null, 0, false);
                precisionTimer.Stop();
                precisionTimer.Dispose();
                precisionTimer = null;
                //downbeatTimer.SetInterval(null, 0, false);
                //downbeatTimer.Stop();
                //downbeatTimer.Dispose();
                //downbeatTimer = null;
                baseSound.Stop();
                baseSound.Dispose();
                baseSound = null;
                //downbeatSound.Stop();
                //downbeatSound.Dispose();
                //downbeatSound = null;
                

                // metronomeTime.Stop();
                // metronomeTime.IsEnabled = false;
                // metronomeTime.Tick -= sound_test;
                // running = false;
            }
        }
        public void soundFourFour(object sender, EventArgs e,)
        {
            // Starts the sound of the metronome

            
            System.Media.SoundPlayer downbeatSound = new System.Media.SoundPlayer(@"C:\Users\josep\Downloads\MetronomeApp-7ded28f6b9326be4a94f38a064534d3b837165b9\MetronomeApp-7ded28f6b9326be4a94f38a064534d3b837165b9\MetronomeApp\Resources\Voices\Metronomes\Synth_Square_E_hi.wav");
            System.Media.SoundPlayer baseSound = new System.Media.SoundPlayer(@"C:\Users\josep\Downloads\MetronomeApp-7ded28f6b9326be4a94f38a064534d3b837165b9\MetronomeApp-7ded28f6b9326be4a94f38a064534d3b837165b9\MetronomeApp\Resources\Voices\Metronomes\Synth_Square_E_lo.wav");
            
            if (what == true)
            {
                downbeatSound.Stop();
                baseSound.Play();
            }
            else if (what == false)
            {
                baseSound.Stop();
                downbeatSound.Play();

            }

        }

       /* public async void soundTestFour()
        {
            precisionTimer.SetInterval(baseSound.PlaySync, (int)metTempo, true);
            downbeatTimer.SetInterval(downbeatSound.PlaySync, (int)metTempo, true);  
            await precisionTimer.Start();
            await downbeatTimer.Start();
        }
       */
       public void downbeatLoop(bool what)
        {
            precisionTimer.SetPeriod((int)metTempo);
           // precisionTimer.Tick += new EventHandler(soundFourFour);
            precisionTimer.Start();
            do
            {
                if (counter % 4 == 0)
                {
                   // precisionTimer.Tick -= new EventHandler(soundFourFour);
                }
            } while (what == true);
        }
    }
}
