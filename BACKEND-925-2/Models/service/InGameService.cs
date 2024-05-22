using System;
using TwoPlayerGames.Domain.Auxiliary;
using TwoPlayerGames.Repository;
using System.Windows.Media;

namespace TwoPlayerGames.Service
{
    public class InGameService
    {
        public MediaPlayer MediaPlayer;
        StatsRepository StatsRepository;

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

        public PlayerStats GetStats(Player player)
        {
            return StatsRepository.GetProfileStatsForPlayer(player);
        }
    }
}


