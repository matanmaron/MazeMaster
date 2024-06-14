using UnityEngine;
using UnityEngine.UI;

namespace MaronByteStudio
{
    public class Logo : MonoBehaviour
    {
        [SerializeField] Button LogoButton;
        void Start()
        {
            LogoButton.onClick.AddListener(OnLogoButton);
        }

        private void OnLogoButton()
        {
            Application.OpenURL("https://matanmaron.wixsite.com/home/maronbytestudio");
        }

        private void OnDestroy()
        {
            LogoButton.onClick.RemoveAllListeners();
        }
    }
}
