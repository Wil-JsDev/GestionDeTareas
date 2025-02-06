using GestionDeTareas.Core.Application.Interfaces.Factories;

namespace GestionDeTareas.Core.Application.Factories.HighPriority
{
    //Concret Product
    public class HighPriorityTask : IHighPriority
    {
        private string _description;

        public void SetDescription(string description)
        {
            _description = description;
        }

        public string GetDescription() => _description;

    }
}
