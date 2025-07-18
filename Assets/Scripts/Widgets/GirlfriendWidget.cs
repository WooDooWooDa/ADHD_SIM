using System;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Widgets
{
    public class GirlfriendWidget : Widget
    {
        public Transform startTransform; 
        public Button startButton;

        public void Awake()
        {
            startButton.onClick.AddListener(StartGame);
        }

        private void StartGame()
        {
            var gf = FindFirstObjectByType<Girlfriend>();
            gf.StartCoroutine(gf.StartDay());
            Hide();
        }
    }
}