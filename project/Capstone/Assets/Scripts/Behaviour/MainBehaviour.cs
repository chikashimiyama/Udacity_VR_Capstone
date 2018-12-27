using UnityEngine;

namespace DomainF
{
    public class MainBehaviour : MonoBehaviour
    {
        [SerializeField] private IControllerBehaviour leftControllerBehaviour;
        [SerializeField] private IControllerBehaviour rightControllerBehaviour;

        private StateAssigner stateAssigner_ = new StateAssigner();
        private Controller leftController_;
        private Controller rightController_;
        
        void Start()
        {
            stateAssigner_ = new StateAssigner();

            leftController_ = new Controller(stateAssigner_, leftControllerBehaviour);
            rightController_ = new Controller(stateAssigner_, rightControllerBehaviour);
        }
    }
}