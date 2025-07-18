using TMPro;

namespace DefaultNamespace.Widgets
{
    public class WinStateWidget : Widget
    {
        public TextMeshProUGUI winStateText;

        protected override void Start()
        {
            base.Start();
            _isVisible = false;
            gameObject.SetActive(false);
        }
    }
}