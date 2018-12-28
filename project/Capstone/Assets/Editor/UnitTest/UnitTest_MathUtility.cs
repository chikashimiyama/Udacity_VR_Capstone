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
    }
}