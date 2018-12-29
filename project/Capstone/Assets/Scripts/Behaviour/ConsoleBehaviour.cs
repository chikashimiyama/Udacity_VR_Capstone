using UnityEngine;

namespace DomainF
{
    public interface IConsoleBehaviour
    {
        IToggleButtonBehaviour ScaleGridToggleBehaviour { get; }
        IToggleButtonBehaviour DirectionGridToggleBehaviour { get; }
    }

    public class ConsoleBehaviour : MonoBehaviour, IConsoleBehaviour
    {
        [SerializeField] private ToggleButtonBehaviour scaleGridToggleBehaviour;
        [SerializeField] private ToggleButtonBehaviour directionGridToggleBehaviour;

        public IToggleButtonBehaviour ScaleGridToggleBehaviour
        {
            get { return scaleGridToggleBehaviour; }
        }

        public IToggleButtonBehaviour DirectionGridToggleBehaviour
        {
            get { return directionGridToggleBehaviour; }
        }
    }
}