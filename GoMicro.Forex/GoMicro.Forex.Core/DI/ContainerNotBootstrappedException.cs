using System;
using System.Runtime.Serialization;

namespace GoMicro.Forex.DI
{
    [Serializable]
    public class ContainerNotBootstrappedException : Exception
    {
        public ContainerNotBootstrappedException() { }
        protected ContainerNotBootstrappedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
}
