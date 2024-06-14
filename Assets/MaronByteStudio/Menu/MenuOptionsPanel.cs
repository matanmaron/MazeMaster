using MaronByteStudio.MazeMaster;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MaronByteStudio.Menu
{
    public class MenuOptionsPanel : MonoBehaviour
    {
        [SerializeField] Button InvertMouseButton;
        [SerializeField] TextMeshProUGUI InvertMouseText;
        [SerializeField] Button SoundButton;
        [SerializeField] Button MusicButton;
        [SerializeField] Button BackButton;
        [SerializeField] MenuScene menu;
        TextMeshProUGUI SoundText;
        TextMeshProUGUI MusicText;

        void Start()
        {
            SoundText = SoundButton.GetComponentInChildren<TextMeshProUGUI>();
            MusicText = MusicButton.GetComponentInChildren<TextMeshProUGUI>();

            SoundButton.onClick.AddListener(OnSoundButton);
            MusicButton.onClick.AddListener(OnMusicButton);
            BackButton.onClick.AddListener(() => menu.OnBackButton());
            InvertMouseButton.onClick.AddListener(OnInvertMouse);
            InitButtons();
        }

        private void OnEnable()
        {
            EventSystem.current.SetSelectedGameObject(InvertMouseButton.gameObject);
        }
        
        private void InitButtons()
        {
            string invertMouse = Settings.Invert ? "On" : "Off";
            InvertMouseText.text = $"Invert Mouse {invertMouse}";
            string sound = Settings.MuteSFX ? "Off" : "On";
            SoundText.text = $"Sound {sound}";
            string music = Settings.MuteMusic ? "Off" : "On";
            MusicText.text = $"Music {music}";
        }

        private void OnMusicButton()
        {
            Settings.MuteMusic = !Settings.MuteMusic;
            string music = Settings.MuteMusic ? "Off" : "On";
            MusicText.text = $"Music is {music}";
            AudioManager.Instance.Refresh();
        }

        private void OnInvertMouse()
        {
            Settings.Invert = !Settings.Invert;
            string invertMouse = Settings.Invert ? "On" : "Off";
            InvertMouseText.text = $"Invert Mouse {invertMouse}";
        }
        private void OnSoundButton()
        {
            Settings.MuteSFX = !Settings.MuteSFX;
            string sound = Settings.MuteSFX ? "Off" : "On";
            SoundText.text = $"Sound is {sound}";
        }

        private void OnDestroy()
        {
            SoundButton.onClick.RemoveAllListeners();
            MusicButton.onClick.RemoveAllListeners();
            BackButton.onClick.RemoveAllListeners();
        }
    }
}
