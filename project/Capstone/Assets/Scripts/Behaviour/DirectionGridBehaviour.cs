using UnityEngine;

namespace DomainF
{
	public class DirectionGridBehaviour : MonoBehaviour, IShowable {

		[SerializeField] private GameObject circlePrefab;
		private const int NumCircles = 4;
		private const float MaxDist = 35f;
		private const float AStep = 180f / NumCircles;

		private void Start ()
		{
			for (var i = 0; i < NumCircles; i++)
			{
				var circle = Instantiate(circlePrefab);
				circle.transform.parent = gameObject.transform;
				circle.transform.localScale = new Vector3(MaxDist, 1f, MaxDist);
				circle.transform.Rotate(new Vector3(90f, i * AStep, 0f));
			}
		}

		public bool Visible
		{
			set
			{
				gameObject.SetActive(value);
			}
		}
	}
}

