using System;
using UnityEngine;

namespace DomainF
{

	public class ScaleGridBehaviour : MonoBehaviour, IVisible {

		[SerializeField] private GameObject circlePrefab;
		private const int NumPitches = 24;
		private const float Step = 180f / NumPitches;
		private const float MaxDist = 35f;
		private const float VStep = MaxDist * 2 / NumPitches;

		private void Start ()
		{
			var scl = Step;
			var yPos = -MaxDist + VStep;
			for (var i = 1; i < NumPitches; i++)
			{
				var circle = Instantiate(circlePrefab);
				circle.transform.parent = gameObject.transform;

				var scale = (float)Math.Sin(Mathf.Deg2Rad *scl ) * MaxDist;
				circle.transform.localScale = new Vector3(scale, 1f, scale);
				circle.transform.position = new Vector3(0, yPos, 0);
				scl += Step;
				yPos += VStep;
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

