using System;

namespace DomainF
{
    public class MathUtility
    {
        private const float MinDist = 5f;
        private const float MaxDist = 30f;
        private const float Divider = 30f;
        
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

        public static float EulerAngleToFrequency(float angle)
        {
            if (angle > 180f)
                angle = -(360f - angle);
            angle = 180 - (angle + 90);
            return angle * 5 + 100;
        }
    }
}