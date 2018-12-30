using LibPDBinding;

namespace DomainF
{
    public interface IPureDataArrayFacade
    {
        float[] Get();
    }

    public class PureDataArrayFacade : IPureDataArrayFacade
    {
        private readonly string name_;
        private readonly float[] data_;
        
        public PureDataArrayFacade(string name, int size)
        {
            name_ = name;
            data_ = new float[size];
        }

        public float[] Get()
        {
            LibPD.ReadArray(data_, name_, 0, data_.Length);
            return data_;
        }
    }
}