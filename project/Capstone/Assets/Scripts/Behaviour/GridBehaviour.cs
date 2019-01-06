using UnityEngine;

namespace DomainF
{
    public interface IGridBehaviour
    {
        IVisible ScaleCircleBehaviour { get; }      
        IVisible DirectionCircleBehaviour { get; }      
        IVisible EquatorCircleBehaviour { get; }      
    }
    
    public class GridBehaviour : MonoBehaviour, IGridBehaviour
    {
        [SerializeField] private ScaleGridBehaviour ScaleGridBehaviour;
        [SerializeField] private DirectionGridBehaviour DirectionGridBehaviour;
        [SerializeField] private EquatorBehaviour equatorBehaviour;


        public IVisible ScaleCircleBehaviour
        {
            get { return ScaleGridBehaviour; }
            
        }
        public IVisible DirectionCircleBehaviour
        {
            get { return DirectionGridBehaviour; }
        }
        
        public IVisible EquatorCircleBehaviour
        {
            get { return equatorBehaviour; }
        }
    }
}