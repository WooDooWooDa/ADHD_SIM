using System;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Widgets
{
    public class PauseWidget : Widget
    {
        public PlayerController player;
        public Button resumeButton;

        public void Awake()
        {
            resumeButton.onClick.AddListener(Hide);
        }

        protected override void Start()
        {
            base.Start();
            Deactivate();
        }

        protected override void InAnimation() { }

        protected override void OutAnimation() { }

        public override void Show()
        {
            if (!FindFirstObjectByType<Girlfriend>().dayIsActive) return;
            
            base.Show();
            player.canMove = false;
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
            Deactivate();
        }
    }
}