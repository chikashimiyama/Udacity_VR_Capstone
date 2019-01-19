using UnityEngine;
using UnityEngine.UI;

namespace DomainF
{
    public interface IIndicatorBehaviour
    {
        string FuncText { set; }
        string FreqText { set; }
        string AmpText { set; }
        string ResText { set; }
    }

    public class 
        IndicatorBehaviour : MonoBehaviour, IIndicatorBehaviour
    {
        [SerializeField] private Text freqText;
        [SerializeField] private Text ampText;
        [SerializeField] private Text funcText;
        [SerializeField] private Text resText;

        public string FuncText
        {
            set { funcText.text = value; }
        }

        public string FreqText
        {
            set { freqText.text = value; }
        }

        public string AmpText
        {
            set { ampText.text = value; }
        }

        public string ResText
        {
            set { resText.text = value; }
        }
        
    }
}