using TMPro;
using UnityEngine;

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
            timeAddedText.color = Color.white;
            var from = new Vector3(452.5f, -217.5f, 1f);
            timeAddedText.rectTransform.anchoredPosition = from;
            
            //Scale up tween
            LeanTween.scale(timeAddedText.rectTransform, Vector2.one * 3f, 0.35f).setLoopPingPong(1)
                .setOnComplete(() =>
                {
                    //move to corner tween
                    LeanTween.value(timeAddedText.gameObject, (Vector3 v) =>
                    {
                        timeAddedText.rectTransform.anchoredPosition = v;
                        timeAddedText.alpha = v.z;
                    }, from, new Vector3(95f, -12.5f, 0f), 0.25f).setDelay(0.5f);
                    /*.setOnComplete(() =>
                    {
                        timeAddedText.color = new Color(100f, 38f, 25f, 1f);
                        //Fade out tween
                        _currentTween = LeanTween.value(gameObject,
                            (float v) => { timeAddedText.alpha = v; },
                            1f, 0f, 1f).setEase(LeanTweenType.easeOutCubic).setDelay(3f)
                        .setOnComplete(() =>
                        {
                            _lastMinutesAdded = 0f;
                            _currentTween = null;
                        });
                    });*/
                });
            _lastMinutesAdded = minutes;
        }
    }
}