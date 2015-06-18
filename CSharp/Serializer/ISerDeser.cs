using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp.Serializer
{
    interface ISerDeser
    {
        string Serialize<T>(object obj);
        T Deserialize<T>(string serialized);
    }
}
