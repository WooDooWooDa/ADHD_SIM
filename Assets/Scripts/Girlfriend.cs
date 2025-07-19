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
        public AudioSource musicSource;
        
        public bool dayIsActive;
        private TimeManager _timeManager;
        private ThoughtsWidget _thoughtsWidget;
        private AudioSource _audioSource;
        private OriginalTask _originalTask;

        private void Awake()
        {
            _originalTask = FindFirstObjectByType<OriginalTask>();
            _audioSource = GetComponent<AudioSource>();
            _timeManager = FindFirstObjectByType<TimeManager>();    
            _timeManager.SetTimeOfDay(8f, 30f);
            _timeManager.TimeChanged += TimeChanged;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        private void TimeChanged(float hours, float minutes)
        {
            if (!dayIsActive) return;
            
            if (Mathf.Approximately(hours, endOfDayHour) && Mathf.Approximately(minutes, endOfDayMinutes))
            {
                EndDay();
            }
        }

        public IEnumerator StartDay()
        {
            //Before
            musicSource.Play();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            _timeManager.timeIsTicking = true;
            _thoughtsWidget = FindFirstObjectByType<ThoughtsWidget>(FindObjectsInactive.Include);
            _thoughtsWidget.ShowTextFor("Partner :\nDon't forget to empty all trashcans before I come from work at 16h00", timeBeforeDayStarts + 1f);
            
            yield return new WaitForSeconds(timeBeforeDayStarts);
            
            //Day starting!
            FindFirstObjectByType<TaskListWidget>(FindObjectsInactive.Include).Show();
            FindFirstObjectByType<PlayerController>().canMove = true;
            FindFirstObjectByType<Noticer>().canDetect  = true;
            _originalTask.TryNotice();
            dayIsActive = true;
        }

        public void EndDay()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            dayIsActive = false;
            _timeManager.timeIsTicking = false;
            FindFirstObjectByType<PlayerController>().canMove = false;
            FindFirstObjectByType<Noticer>().canDetect = false;
            FindFirstObjectByType<InteractWith>().detectionDistance = 0f;
            FindFirstObjectByType<TaskListWidget>(FindObjectsInactive.Include).Hide();
            
            if (_originalTask.taskState == TaskState.Done)
            {
                //Win
                winStateWidget.winStateText.text = "YOU WIN!!";
                _audioSource.clip = WinClip;
                _thoughtsWidget.ShowTextFor("Partner : \nNice! Thank you my love, you remembered to take out the trash!", 100f);
                _audioSource.Play();
            }
            else
            {
                //Lose
                winStateWidget.winStateText.text = "YOU LOSE...";
                _audioSource.clip = LoseClip;
                _thoughtsWidget.ShowTextFor("Partner : \nLove!! You forgot the only thing I asked you before leaving!", 100f);
                _audioSource.Play();
            }
            winStateWidget.Show();
        }
    }
}