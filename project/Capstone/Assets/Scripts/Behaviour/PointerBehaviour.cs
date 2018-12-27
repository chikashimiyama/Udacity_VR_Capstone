using UnityEngine;

namespace DomainF
{
    public class PointerBehaviour : MonoBehaviour
    {
        [SerializeField] private GameObject targetSphere;

        private LineRenderer laserPointer_;
         
        private void Start()
        {
            laserPointer_ = GetComponentInChildren<LineRenderer>();
        }

        private void Update()
        {
            laserPointer_.SetPosition(0, transform.position);
            var endPoint =  transform.forward * 30;
            targetSphere.transform.position = endPoint;
            laserPointer_.SetPosition(1, endPoint);
        }

    }


}
