using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(TimeManager))]
    public class TimeManagerInspector : UnityEditor.Editor
    {
        private TimeManager _manager;

        private float _hours = 1;
        private float _minutes = 5;

        public void OnEnable()
        {
            _manager = FindFirstObjectByType<TimeManager>();
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            DrawDefaultInspector();
            
            GUILayout.Label("Current Time : " + _manager.hour.ToString("00") + ":" +  _manager.minute.ToString("00"));
            EditorGUILayout.BeginHorizontal();
            _hours = EditorGUILayout.FloatField("Hours", _hours);
            if (GUILayout.Button($"Add {_hours} Hour", EditorStyles.miniButton))
            {
                _manager.AddHours(_hours);
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            _minutes = EditorGUILayout.FloatField("Minutes", _minutes);
            if (GUILayout.Button($"Add {_minutes} minute", EditorStyles.miniButton))
            {
                _manager.AddMinutes(_minutes);
            }
            EditorGUILayout.EndHorizontal();
            Repaint();
        }
    }
}