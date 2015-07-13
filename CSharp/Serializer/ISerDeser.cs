using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp.Serializer
{
    // the implement serialize class need support Generic
    interface ISerDeser
    {
        string Serialize<T>(object obj);
        T Deserialize<T>(string serialized);
    }
}
