using UnityEngine;

namespace DomainF
{
    public interface IConsoleBehaviour
    {
        IToggleButtonBehaviour ScaleGridToggleBehaviour { get; }
    }

    public class ConsoleBehaviour : MonoBehaviour, IConsoleBehaviour
    {
        [SerializeField] private ToggleButtonBehaviour scaleGridToggleBehaviour;

        public IToggleButtonBehaviour ScaleGridToggleBehaviour
        {
            get { return scaleGridToggleBehaviour; }
        }
    }
}