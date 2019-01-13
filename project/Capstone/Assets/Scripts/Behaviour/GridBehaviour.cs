using UnityEngine;
using UnityEngine.Serialization;

namespace DomainF
{
    public interface IGridBehaviour
    {
        IVisible ScaleCircleBehaviour { get; }      
        IVisible DirectionCircleBehaviour { get; }      
        IVisible FFTCircleBehaviour { get; }      
    }
    
    public class GridBehaviour : MonoBehaviour, IGridBehaviour
    {
        [SerializeField] private ScaleGridBehaviour ScaleGridBehaviour;
        [SerializeField] private DirectionGridBehaviour DirectionGridBehaviour;
        [SerializeField] private FFTRippleBehaviour fftRippleBehaviour_;


        public IVisible ScaleCircleBehaviour
        {
            get { return ScaleGridBehaviour; }
            
        }
        public IVisible DirectionCircleBehaviour
        {
            get { return DirectionGridBehaviour; }
        }
        
        public IVisible FFTCircleBehaviour
        {
            get { return fftRippleBehaviour_; }
        }
    }
}