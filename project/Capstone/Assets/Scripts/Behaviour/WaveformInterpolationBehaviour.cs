using UnityEngine;

namespace DomainFo
{
	public interface IWaveformInterpolationBehaviour
	{
		float Angle { set; }
	}

	public class WaveformInterpolationBehaviour : MonoBehaviour, IWaveformInterpolationBehaviour
	{
		
		public float Angle
		{
			set
			{
				var euler = transform.eulerAngles;
				euler.z = value;
				transform.eulerAngles = euler;
			}
		}
	}


}
