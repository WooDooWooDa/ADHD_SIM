using System;
using System.Collections;
using DefaultNamespace.Widgets;
using UnityEngine;

namespace DefaultNamespace
{
    public class Girlfriend : MonoBehaviour
    {
        public float endOfDayHour;
        public float endOfDayMinutes;
        public float timeBeforeDayStarts = 5f;
        public AudioClip WinClip;
        public AudioClip LoseClip;
        public WinStateWidget winStateWidget;
        
        public bool dayIsActive;
        private TimeManager _timeManager;
        private ThoughtsWidget _thoughtsWidget;
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _timeManager = FindObjectOfType<TimeManager>();    
            _timeManager.SetTimeOfDay(8f, 30f);
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
            _thoughtsWidget = FindFirstObjectByType<ThoughtsWidget>(FindObjectsInactive.Include);
            _thoughtsWidget.ShowTextFor("Girlfriend : \nDon't forget to take out the trash before I come from work at 16h00", timeBeforeDayStarts + 1f);
            yield return new WaitForSeconds(timeBeforeDayStarts);
            FindFirstObjectByType<PlayerController>().canMove = true;
            FindFirstObjectByType<Noticer>().canDetect  = true;
            dayIsActive = true;
        }

        private void EndDay()
        {
            dayIsActive = false;
            _timeManager.timeIsTicking = false;
            FindFirstObjectByType<PlayerController>().canMove = false;
            FindFirstObjectByType<Noticer>().canDetect = false;
            FindFirstObjectByType<InteractWith>().detectionDistance = 0f;
            FindFirstObjectByType<TaskListWidget>(FindObjectsInactive.Include).Hide();
            var task = FindFirstObjectByType<OriginalTask>();
            if (task.taskState == TaskState.Done)
            {
                //Win
                winStateWidget.winStateText.text = "YOU WIN!!";
                _audioSource.clip = WinClip;
                _thoughtsWidget.ShowTextFor("Girlfriend : \nNice! Thank you my love, you remembered to take out the trash!", 100f);
                _audioSource.Play();
            }
            else
            {
                //Lose
                winStateWidget.winStateText.text = "YOU LOSE...";
                _audioSource.clip = LoseClip;
                _thoughtsWidget.ShowTextFor("Girlfriend : \nLove!! You forgot the only thing I asked you before leaving!", 100f);
                _audioSource.Play();
            }
            winStateWidget.Show();
        }
    }
}