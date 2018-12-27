using System;
using Magicolo;

namespace DomainF
{
    public interface IWaterfall
    {
        void Add(float[] spectrum);
    }

    public class Waterfall
    {
        private float[][] buffer_;
        private int currentIndex_;

        Waterfall(int fftSize = 1024, int numFrames = 128)
        {
            buffer_ = new float[fftSize][];
            for(var i = 0; i < numFrames; i++)
            {
                buffer_[i] = new float[fftSize];
            }
        }

        void Add(float[] spectrum)
        {
            Array.Copy(spectrum, buffer_[currentIndex_], spectrum.Length);
            ++currentIndex_;
            if(currentIndex_ >= buffer_.Length)
                currentIndex_ = 0;
                
        }
    }
}