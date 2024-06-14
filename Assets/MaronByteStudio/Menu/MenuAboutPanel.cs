using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MaronByteStudio.Menu
{
    public class MenuAboutPanel : MonoBehaviour
    {
        [SerializeField] Button BackButton;
        [SerializeField] MenuScene menu;

        void Start()
        {
            BackButton.onClick.AddListener(() => menu.OnBackButton());
        }

        private void OnEnable()
        {
            EventSystem.current.SetSelectedGameObject(BackButton.gameObject);
        }

        private void OnDestroy()
        {
            BackButton.onClick.RemoveAllListeners();
        }
    }
}
