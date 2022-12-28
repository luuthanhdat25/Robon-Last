using UnityEngine;

namespace Audio
{
    public class AudioManager : RepeatMonobehaviour
    {
        private static AudioManager instance;
        public static AudioManager Instance { get => instance; }
        
        public AudioSource backgroundSound;
        public AudioSource dieSound;
        public AudioSource winGameSound;
        public AudioSource loseGameSound;
        public AudioSource completedLevel;
        public AudioSource warningSound;
        public AudioSource uiButtonSound;
        
        protected override void Awake()
        {
            AudioManager.instance = this;    
        }
    }
}