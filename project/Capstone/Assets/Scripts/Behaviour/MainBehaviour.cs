using UnityEngine;

namespace DomainF
{
    public class MainBehaviour : MonoBehaviour
    {
        [SerializeField] private IndicatorBehaviour leftIndicatorBehaviour;
        [SerializeField] private ControllerBehaviour leftControllerBehaviour;

        [SerializeField] private IndicatorBehaviour rightIndicatorBehaviour;
        [SerializeField] private ControllerBehaviour rightControllerBehaviour;

        [SerializeField] private ConsoleBehaviour consoleBehaviour;
        [SerializeField] private ScaleGridBehaviour scaleGridBehavior;
        [SerializeField] private DirectionGridBehaviour directionGridBehaviour;
        
        private StateAssigner stateAssigner_;
        private Controller leftController_;
        private Controller rightController_;
        private Console console_;

        private void Start()
        {
            
            stateAssigner_ = new StateAssigner(new PureDataFacade());

            leftController_ = new Controller(stateAssigner_, leftControllerBehaviour, leftIndicatorBehaviour);
            rightController_ = new Controller(stateAssigner_, rightControllerBehaviour, rightIndicatorBehaviour);
            
            console_ = new Console(consoleBehaviour, scaleGridBehavior, directionGridBehaviour);
        }
        
        
    }
}