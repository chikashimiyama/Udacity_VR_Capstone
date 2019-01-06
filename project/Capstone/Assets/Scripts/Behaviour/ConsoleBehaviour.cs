using System;
using UnityEngine;

namespace DomainF
{
    public interface IConsoleBehaviour
    {
        IToggleButtonBehaviour ScaleGridToggleBehaviour { get; }
        IToggleButtonBehaviour DirectionGridToggleBehaviour { get; }
        IToggleButtonBehaviour EquatorToggleBehaviour { get; }

        event Action ConsoleEntered;
        event Action ConsoleExited;
    }

    public class ConsoleBehaviour : MonoBehaviour, IConsoleBehaviour
    {
        [SerializeField] private ToggleButtonBehaviour scaleGridToggleBehaviour;
        [SerializeField] private ToggleButtonBehaviour directionGridToggleBehaviour;
        [SerializeField] private ToggleButtonBehaviour equatorToggleBehaviour;

        public IToggleButtonBehaviour ScaleGridToggleBehaviour
        {
            get { return scaleGridToggleBehaviour; }
        }

        public IToggleButtonBehaviour DirectionGridToggleBehaviour
        {
            get { return directionGridToggleBehaviour; }
        }

        public IToggleButtonBehaviour EquatorToggleBehaviour
        {
            get { return equatorToggleBehaviour; }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (ConsoleEntered != null)
                ConsoleEntered.Invoke();
        }

        private void OnTriggerExit(Collider other)
        {
            if (ConsoleExited != null)
                ConsoleExited.Invoke();
        }

        public event Action ConsoleEntered;
        public event Action ConsoleExited;
    }
}