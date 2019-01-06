using System;
using UnityEngine;

namespace DomainF
{
    public class MathUtility
    {
        private const float MinDist = 5f;
        private const float MaxDist = 30f;
        private const float Divider = 30f;
        private const float Range = 60f;
        
        public static float DistanceToAmp(float distance)
        {
            if(distance < MinDist || distance > MaxDist)
                throw new ArgumentOutOfRangeException();
            
            return (MaxDist - (distance - MinDist)) / Divider;
        }
        
        public static float LimitDistance(float distance)
        {
            if (distance < MinDist)
                distance = MinDist;
            else if (distance > MaxDist)
                distance = MaxDist;
            return distance;
        }

        public static float EulerAngleToLinear(float angle)
        {
            if (angle >= 180f)
                angle = -(360f - angle);
            angle = 180 - (angle + 90);
            return angle / 180.0f;
        }

        public static float MidiToFrequency(float midiNote)
        {
            var exp = (midiNote - 69f) / 12f;
            return (float)Math.Pow(2.0, exp) * 440f;
        }

        public static float LimitKnobAngle(float input)
        {
            float output;
            if (input > 180)
            {
                if (input < 260)
                    output = 260 - input;
                else
                    output = 0;
            }
            else
            {
                if (input > 100)
                    output = 100 - input;
                else
                    output = 0;
            }

            return output;
        }
    }
}