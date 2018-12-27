using UnityEngine;

namespace DomainF
{
    public class MainBehaviour : MonoBehaviour
    {
        [SerializeField] private ControllerBehaviour leftControllerBehaviour;
        [SerializeField] private ControllerBehaviour rightControllerBehaviour;

        private StateAssigner stateAssigner_;
        private Controller leftController_;
        private Controller rightController_;
        
        void Start()
        {
            stateAssigner_ = new StateAssigner(new PureDataFacade());

            leftController_ = new Controller(stateAssigner_, leftControllerBehaviour);
            rightController_ = new Controller(stateAssigner_, rightControllerBehaviour);
        }
    }
}