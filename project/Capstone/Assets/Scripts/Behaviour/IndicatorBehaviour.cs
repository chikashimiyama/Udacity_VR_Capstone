using UnityEngine;
using UnityEngine.UI;

namespace DomainF
{
    public interface IIndicatorBehaviour
    {
        string FreqText { set; }
        string AmpText { set; }
    }
    
    public class IndicatorBehaviour : MonoBehaviour, IIndicatorBehaviour
    {
        [SerializeField] private Text freqText;
        [SerializeField] private Text ampText;

        public string FreqText
        {
            set { freqText.text = value; }
        }

        public string AmpText
        {
            set { ampText.text = value; }
        }
    }
}