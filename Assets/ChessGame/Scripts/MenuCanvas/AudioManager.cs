using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
namespace ChessGame.Scripts.MenuCanvas {
    public class AudioManager : MonoBehaviour {
        [SerializeField] private AudioClip[] standardMouseClicksClips;
        [SerializeField] private AudioListener audioListener;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private Button[] allButtons;
        private bool _audioMuted = false;
        [SerializeField] private AudioClip[] chessPickUpChessmanClips;
        [SerializeField] private AudioClip[] chessDropDownChessmanClips;
        [SerializeField] private AudioClip[] chessGameOverClips;

        private void Reset() {
            audioListener = Camera.main.GetComponent<AudioListener>();
            audioSource = FindFirstObjectByType<AudioSource>();
            allButtons = FindObjectsOfType<Button>(true);
        }

        private void Start() {
            foreach (Button button in allButtons) {
                button.onClick.AddListener(PlayRandomMouseClick);
                if (button.name.Contains("Mute")) {
                    button.onClick.AddListener(ToggleSound);
                }
            }
        }

        public void ToggleSound() {
            _audioMuted = !_audioMuted;
            AudioListener.volume = _audioMuted ? 0f : 1f;
        }

        public void PlayRandomMouseClick() {
            int index = Random.Range(0, standardMouseClicksClips.Length);
            PlayAudioClip(standardMouseClicksClips[index]);
        }

        public void PlayAudioClip(AudioClip audioClip) {
            audioSource.PlayOneShot(audioClip);
        }

        public void PlayRandomChessmanPickUp() {
            int index = Random.Range(0, chessPickUpChessmanClips.Length);
            PlayAudioClip(chessPickUpChessmanClips[index]);
        }

        public void PlayRandomChessmanDropDown() {
            int index = Random.Range(0, chessDropDownChessmanClips.Length);
            PlayAudioClip(chessDropDownChessmanClips[index]);
        }

        public void PlayRandomChessGameOver() {
            int index = Random.Range(0, chessGameOverClips.Length);
            PlayAudioClip(chessGameOverClips[index]);
        }
    }
}