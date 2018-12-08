using UnityEngine;

namespace DomainF
{
    public class PointerBehaviour : MonoBehaviour
    {
        private LineRenderer laserPointer_;

        private void Start()
        {
            laserPointer_ = GetComponentInChildren<LineRenderer>();
        }

        private void Update()
        {
            laserPointer_.SetPosition(0, transform.position);
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 50))
            {
                Debug.Log("hit");
                laserPointer_.SetPosition(1, hit.point);
            }
        }
    }


}
