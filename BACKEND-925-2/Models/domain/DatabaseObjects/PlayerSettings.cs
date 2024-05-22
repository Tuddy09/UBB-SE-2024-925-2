namespace TwoPlayerGames.Domain.DatabaseObjects
{
    public class PlayerSettings
    {
        private Player2PlayerGame player;
        private string ip;
        private bool isSoundOn;
        private bool isMusicOn;
        private float soundVolume;
        private float musicVolume;

        public Player2PlayerGame Player { get => player; set => player = value; }
        public bool IsSoundOn { get => isSoundOn; set => isSoundOn = value; }
        public bool IsMusicOn { get => isMusicOn; set => isMusicOn = value; }
        public float SoundVolume { get => soundVolume; set => soundVolume = value; }
        public float MusicVolume { get => musicVolume; set => musicVolume = value; }
        public string Ip { get => ip; set => ip = value; }

        public PlayerSettings(Player2PlayerGame player, string ip, bool isSoundOn, bool isMusicOn, float soundVolume, float musicVolume)
        {
            this.player = player;
            this.ip = ip;
            this.isSoundOn = isSoundOn;
            this.isMusicOn = isMusicOn;
            this.soundVolume = soundVolume;
            this.musicVolume = musicVolume;
        }

        public PlayerSettings()
        {
            this.player = new Player2PlayerGame();
            this.ip = string.Empty;
            this.isSoundOn = true;
            this.isMusicOn = true;
            this.soundVolume = 1.0f;
            this.musicVolume = 1.0f;
        }
    }
}
