using Behaviour;
using UnityEngine;

namespace DomainF
{
	public class DirectionGridBehaviour : MonoBehaviour, IVisible {

		[SerializeField] private GameObject circlePrefab;
		private const int NumCircles = 4;
		private const float AStep = 180f / NumCircles;
		private const float MinFreq = 100f;
		private const float MaxFreq = 3100f;
		private const float Range = MaxFreq - MinFreq;
		private const float Partial = Range / NumCircles;
		private void Start ()
		{
			for (var i = 0; i < NumCircles; i++)
			{
				var circle = Instantiate(circlePrefab);
				circle.transform.parent = gameObject.transform;
				var directionCircleBehaviour = circle.GetComponent<IDirectionCircleBehaviour>();
				directionCircleBehaviour.SetRotate(i * AStep);
				var minFreqStr = (MinFreq + i * Partial).ToString() + "Hz.";
				var maxFreqStr = (MaxFreq - i * Partial).ToString() + "Hz.";
				directionCircleBehaviour.SetLabels(minFreqStr, maxFreqStr);
			}
		}

		public bool State
		{
			set
			{
				gameObject.SetActive(value);
			}
		}
	}
}

