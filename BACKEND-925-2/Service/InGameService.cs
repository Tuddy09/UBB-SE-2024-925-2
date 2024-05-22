using System;
using TwoPlayerGames.Domain.Auxiliary;
using TwoPlayerGames.Repository;
using System.Windows.Media;
using TwoPlayerGames;

namespace BACKEND_925_2.Service
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

        public PlayerStats GetStats(Player2PlayerGame player)
        {
            return StatsRepository.GetProfileStatsForPlayer(player);
        }
    }
}


