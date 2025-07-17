using TMPro;

namespace DefaultNamespace.Widgets
{
    public class TimeWidget : Widget
    {
        public TextMeshProUGUI timeText;
        public TextMeshProUGUI timeAddedText;
        
        private TimeManager _timeManager;
        
        private LTDescr _currentTween;
        private float _lastMinutesAdded;

        public void Awake()
        {
            _timeManager = FindFirstObjectByType<TimeManager>();
            _timeManager.TimeChanged += OnTimeChanged;
            _timeManager.MinutesAdded += OnMinutesAdded;
            timeAddedText.alpha = 0f;
        }

        private void OnTimeChanged(float hour, float minute)
        {
            timeText.text = _timeManager.timeString;
        }
        
        private void OnMinutesAdded(float minutes)
        {
            if (_currentTween is not null)
            {
                LeanTween.cancel(_currentTween.id);
                minutes += _lastMinutesAdded;
            }
            
            timeAddedText.text = $"+{minutes}m";
            timeAddedText.alpha = 1f;
            _currentTween = LeanTween.value(gameObject, 
                (float v) => { timeAddedText.alpha = v; }, 
                1f, 0f, 1f).setEase(LeanTweenType.easeOutCubic).setDelay(2f)
                .setOnComplete(() =>
                {
                    _lastMinutesAdded = 0f;
                    _currentTween = null;
                });
            _lastMinutesAdded = minutes;
        }
    }
}