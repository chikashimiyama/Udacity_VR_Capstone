using System;
using DomainF;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class UnitTestMathUtility
    {
        [TestCase(5f, 1f)]
        [TestCase(20f, 0.5f)]
        [TestCase(30f, 0.166666672f)]
        public void DistanceToAmp(float distance, float amp)
        {
            Assert.AreEqual(amp, MathUtility.DistanceToAmp(distance));
        }

        [Test]
        public void DistanceToAmp_out_of_range()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                MathUtility.DistanceToAmp(4.99999f);
            });
            
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                MathUtility.DistanceToAmp(30.00001f);
            });
        }

        [TestCase(4.99999f, 5f)]
        [TestCase(5f, 5f)]
        [TestCase(20f, 20f)]
        [TestCase(30f, 30f)]
        [TestCase(30.00001f, 30f)]
        public void LimitDistance(float input, float output)
        {
            Assert.AreEqual(output, MathUtility.LimitDistance(input));
        }

        [TestCase(0f, 0.5f)]
        [TestCase(180f, 1.5f)]
        public void EulerAngleToLinear(float input, float output)
        {
            Assert.AreEqual(output, MathUtility.EulerAngleToLinear(input));
        }
        
        [TestCase(57f, 220f)]
        [TestCase(69f, 440f)]
        [TestCase(81f, 880f)]
        public void MidiToFrequency(float midiNote, float frequency)
        {
            Assert.AreEqual(frequency, MathUtility.MidiToFrequency(midiNote));
        }

        [TestCase(60.001f, 60f)]
        [TestCase(60, 60f)]
        [TestCase(300f, 300f)]
        [TestCase(299.999f, 300f)]
        public void LimitKnobAngle(float input, float output)
        {
            Assert.AreEqual(output, MathUtility.LimitKnobAngle(input));
        }

    }
}