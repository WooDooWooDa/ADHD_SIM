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
                TaskPriority.VeryLow => 0.5f,
                TaskPriority.Low or TaskPriority.Medium or TaskPriority.High => 0.25f,
                TaskPriority.VeryHigh => 0.1f,
                _ => throw new ArgumentOutOfRangeException(nameof(priority), priority, null)
            };
        }
    }
}