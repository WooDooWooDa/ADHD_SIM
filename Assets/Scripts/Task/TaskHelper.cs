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
                TaskPriority.VeryLow => 1f,
                TaskPriority.Low or TaskPriority.Medium or TaskPriority.High or TaskPriority.VeryHigh => 1f,
                _ => throw new ArgumentOutOfRangeException(nameof(priority), priority, null)
            };
        }
    }
}