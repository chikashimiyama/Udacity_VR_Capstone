using DomainFo;
using UnityEngine;

namespace DomainF
{
    public class MainBehaviour : MonoBehaviour
    {
        [SerializeField] private ControllerBehaviour leftControllerBehaviour;
        [SerializeField] private ControllerBehaviour rightControllerBehaviour;


        [SerializeField] private ConsoleBehaviour consoleBehaviour;
        [SerializeField] private GridBehaviour gridBehaviour;

        private StateAssigner stateAssigner_;
        private Controller leftController_;
        private Controller rightController_;
        private Console console_;

        private void Start()
        {
            var puredataFacade = new PureDataFacade();
            stateAssigner_ = new StateAssigner(puredataFacade);
            leftController_ = new Controller(stateAssigner_, leftControllerBehaviour);
            rightController_ = new Controller(stateAssigner_, rightControllerBehaviour);
            console_ = new Console(consoleBehaviour, gridBehaviour, puredataFacade);
        }
    }
}