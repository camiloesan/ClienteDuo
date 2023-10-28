using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;

namespace ClienteDuo.Utilities
{
    public class MusicManager
    {
        private WaveOutEvent waveOut;
        private AudioFileReader audioFileReader;
        private bool isMusicEnabled = true;
        private float volume = 0.5f;

        public MusicManager(string musicFilePath)
        {
            audioFileReader = new AudioFileReader(musicFilePath);
            waveOut = new WaveOutEvent();
            waveOut.Init(audioFileReader);
        }

        public void ToggleMusic()
        {
            isMusicEnabled = !isMusicEnabled;

            if (isMusicEnabled)
            {
                PlayMusic();
            }
            else
            {
                StopMusic();
            }
        }

        public void PlayMusic()
        {
            if (isMusicEnabled)
            {
                waveOut.Volume = volume;
                waveOut.Play();
            }
        }

        public void StopMusic()
        {
            waveOut.Stop();
        }

        public float Volume
        {
            get { return volume; }
            set
            {
                if (value < 0) volume = 0f;
                else if (value > 1) volume = 1f;
                else volume = value;

                if (isMusicEnabled)
                {
                    waveOut.Volume = volume;
                }
            }
        }

        public bool IsMusicEnabled
        {
            get { return isMusicEnabled; }
        }
    }
}
