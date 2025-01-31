using GestionDeTareas.Core.Domain.Models;

namespace GestionDeTareas.Core.Application.Helper
{
    public class TaskHelper
    {
        delegate bool ValidateTask(TaskItem task);

        public bool Validate(TaskItem task)
        {
            if (task == null) 
                return false;

            ValidateTask validate = task =>
                        !string.IsNullOrWhiteSpace(task.Description);


            return validate(task);
        }

        public void SendNotification(TaskItem message)
        { 
            Action<TaskItem> notifyCreation = task => 
                  Console.WriteLine($"Task created: {task.Description}");

            notifyCreation(message);
        }

        public int CalculateDaysLeft (TaskItem taskItem)
        {
            Func<TaskItem, int> calculateDaysLeft = task =>
            {
                DateTime taskDateTime = task.DuaDate.ToDateTime(TimeOnly.MinValue);
                return (taskDateTime - DateTime.Now).Days;
            };

            return calculateDaysLeft(taskItem);

        }

    }
}
