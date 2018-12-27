using LibPDBinding;

namespace DomainF
{
    public interface IPureDataArray
    {
        void Update();
        float[] Get();
    }

    public class PureDataArray : IPureDataArray
    {
        private readonly string name_;
        private readonly float[] data_;
        
        public PureDataArray(string name, int size)
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