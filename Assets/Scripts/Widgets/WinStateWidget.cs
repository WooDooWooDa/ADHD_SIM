using System;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DefaultNamespace.Widgets
{
    public class WinStateWidget : Widget
    {
        public Button restartButton;
        public TextMeshProUGUI winStateText;

        protected void Awake()
        {
            restartButton.onClick.AddListener(Restart);
        }

        protected override void Start()
        {
            base.Start();
            _isVisible = false;
            gameObject.SetActive(false);
        }

        private void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}