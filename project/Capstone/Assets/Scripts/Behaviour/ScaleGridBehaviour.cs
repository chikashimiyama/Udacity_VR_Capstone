using System;
using UnityEngine;

namespace DomainF
{
    public class ScaleGridBehaviour : MonoBehaviour, IVisible
    {
        enum KeyColor
        {
            White,
            Black
        }

        [SerializeField] private GameObject circlePrefab;
        private const int NumPitches = 24;
        private const float Step = 180f / NumPitches;
        private const float MaxDist = 35f;
        private const float VStep = MaxDist * 2 / NumPitches;

        private KeyColor[] KeyColors = new KeyColor[]
        {
            KeyColor.White, // A 
            KeyColor.Black, // A#
            KeyColor.White, // B
            KeyColor.White, // C
            KeyColor.Black, // C#
            KeyColor.White, // D
            KeyColor.Black, // D#
            KeyColor.White, // E
            KeyColor.White, // F
            KeyColor.Black, // F#
            KeyColor.White, // G
            KeyColor.Black // G#
        };

        private string[] NoteNames = new string[]
        {
            "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B"
        };

        private void Start()
        {
            var scl = Step;
            var yPos = -MaxDist + VStep;
            for (var i = 1; i < NumPitches; i++)
            {
                var scaleCircle = Instantiate(circlePrefab);
                scaleCircle.transform.parent = gameObject.transform;
                IScaleCircleBehaviour scaleCircleBehaviour = scaleCircle.GetComponent<ScaleCircleBehaviour>();

                var color = KeyColors[i % 12] == KeyColor.White
                        ? new Color(1f, 1f, 1f, 0.5f)
                        : new Color(0.5f, 0.5f, 0.5f, 0.5f);

                scaleCircleBehaviour.SetColor(color);

                var scale = (float) Math.Sin(Mathf.Deg2Rad * scl) * MaxDist;
                scaleCircleBehaviour.SetScale(scale);
                scaleCircleBehaviour.SetHeight(yPos); // label should be above the lines
                scl += Step;
                yPos += VStep;
                scaleCircleBehaviour.SetLabel(AbsoluteNoteName(i + 57)); // 59 = midi note offset
            }
        }

        public bool State
        {
            set { gameObject.SetActive(value); }
        }

        private string AbsoluteNoteName(int midiNoteNumber)
        {
            var noteName = NoteNames[midiNoteNumber % 12];
            var octave = midiNoteNumber / 12 - 1;
            return noteName + octave;
        }
    }
}