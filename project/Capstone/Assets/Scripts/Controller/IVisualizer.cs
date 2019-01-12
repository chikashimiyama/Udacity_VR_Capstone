using System;

namespace DomainF
{
    public interface IVisualizerBehaviour
    {
        void Visualize(float[] data);
        event Action Updated;

    }
}