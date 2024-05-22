using System.Windows.Media;
using TwoPlayerGames;

namespace _2PlayerGames.Core
{
    public class InGameService
    {
        public MediaPlayer MediaPlayer;

        public void Play(Sounds sound)
        {
            string filename = sound.Path;

            MediaPlayer = new MediaPlayer();
            MediaPlayer.Open(new Uri(filename));
            MediaPlayer.Play();
        }

        public void SetVolume(float volume)
        {
            MediaPlayer.Volume = volume;
        }
    }
}


