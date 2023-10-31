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

        public static void PlayPlayerJoinedSound()
        {
            AudioFileReader audioFileReader = new AudioFileReader("SFX\\playerJoinedSound.wav");
            WaveOutEvent waveOut = new WaveOutEvent();
            waveOut.Init(audioFileReader);
            waveOut.Volume = 0.5f;
            waveOut.Play();
        }

        public static void PlayPlayerLeftSound()
        {
            AudioFileReader audioFileReader = new AudioFileReader("SFX\\playerLeftSound.wav");
            WaveOutEvent waveOut = new WaveOutEvent();
            waveOut.Init(audioFileReader);
            waveOut.Volume = 0.5f;
            waveOut.Play();
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
