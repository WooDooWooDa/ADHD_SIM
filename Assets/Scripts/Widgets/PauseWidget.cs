using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Widgets
{
    public class PauseWidget : Widget
    {
        public PlayerController player;
        public Button resumeButton;
        public Slider sentSlider;
        public TextMeshProUGUI sentModifierText;
        private Girlfriend _gf;

        public void Awake()
        {
            resumeButton.onClick.AddListener(Hide);
            sentSlider.onValueChanged.AddListener(ChangeSent);
            _gf = FindFirstObjectByType<Girlfriend>();
        }

        protected override void Start()
        {
            base.Start();
            Deactivate();
            ChangeSent(0.5f);
        }

        private void ChangeSent(float newSent)
        {
            player.lookSpeed = Mathf.Lerp(0.01f, 1f, newSent);
            var number = Mathf.Lerp(-9f, 9f, newSent);
            sentModifierText.text = number > 0 ? "+" + number.ToString("0") : number.ToString("0");
        }

        protected override void InAnimation() { }

        protected override void OutAnimation() { }

        public override void Show()
        {
            if (!_gf.dayIsActive) return;
            
            base.Show();
            player.canMove = false;
            _gf.musicSource.Pause();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
        }

        public override void Hide()
        {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            player.canMove = true;
            _gf.musicSource.Play();
            Deactivate();
        }
    }
}