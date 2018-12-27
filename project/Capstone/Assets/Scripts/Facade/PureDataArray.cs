using LibPDBinding;

namespace DomainF
{
    public interface IPureDataArrayFacade
    {
        void Update();
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

        public void Update()
        {
            LibPD.ReadArray(data_, name_, 0, data_.Length);
        }

        public float[] Get()
        {
            return data_;
        }
    }
}