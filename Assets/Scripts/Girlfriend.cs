using System;
using System.Collections;
using UnityEngine;

namespace DefaultNamespace
{
    public class Girlfriend : MonoBehaviour
    {
        public float endOfDayHour;
        public float endOfDayMinutes;
        public float timeBeforeDayStarts = 5f;
        
        public bool dayIsActive;
        private TimeManager _timeManager;

        private void Awake()
        {
            _timeManager = FindObjectOfType<TimeManager>();    
            _timeManager.SetTimeOfDay(8f, 0f);
            _timeManager.TimeChanged += TimeChanged;
            StartCoroutine(StartDay());
        }

        private void TimeChanged(float hours, float minutes)
        {
            if (!dayIsActive) return;
            
            if (Mathf.Approximately(hours, endOfDayHour) && Mathf.Approximately(minutes, endOfDayMinutes))
            {
                EndDay();
            }
        }

        private IEnumerator StartDay()
        {
            var widget = FindFirstObjectByType<ThoughtsWidget>(FindObjectsInactive.Include);
            widget.ShowTextFor("Girlfriend : \nDon't forget to take out the trash before I come from work at 16h00", timeBeforeDayStarts + 1f);
            yield return new WaitForSeconds(timeBeforeDayStarts);
            FindFirstObjectByType<PlayerController>().canMove = true;
            FindFirstObjectByType<Noticer>().canDetect  = true;
            dayIsActive = true;
        }

        private void EndDay()
        {
            dayIsActive = false;
            _timeManager.timeIsTicking = false;
            Debug.LogWarning("Ending Day");
        }
    }
}