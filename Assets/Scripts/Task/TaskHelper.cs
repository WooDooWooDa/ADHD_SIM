using System;

namespace DefaultNamespace
{
    public class TaskHelper
    {
        public static bool IsNoticedYet(float value, TaskPriority priority)
        {
            return value >= GetPriorityNoticeThreshold(priority);
        }
        
        private static float GetPriorityNoticeThreshold(TaskPriority priority)
        {
            return priority switch
            {
                TaskPriority.Original => 0f,
                TaskPriority.VeryLow or TaskPriority.Low => 0.25f,
                TaskPriority.Medium or TaskPriority.High => 0.15f,
                TaskPriority.VeryHigh => 0.1f,
                _ => throw new ArgumentOutOfRangeException(nameof(priority), priority, null)
            };
        }
    }
}