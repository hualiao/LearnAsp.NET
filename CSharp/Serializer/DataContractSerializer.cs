using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;

namespace CSharp.Serializer
{
    internal class DataContractSerializerSerializer : ISerDeser
    {
        private DataContractSerializer _serializer = null;

        public DataContractSerializerSerializer(Type t)
        {
            _serializer = new DataContractSerializer(t);
        }

        #region ISerDeser Members

        public string Serialize<T>(object person)
        {
            using (var stream = new MemoryStream())
            {
                // (T)person check person's type is  T or derived T
                _serializer.WriteObject(stream, (T)person);
                stream.Flush();
                stream.Position = 0;
                return Convert.ToBase64String(stream.ToArray());
            }
        }

        public T Deserialize<T>(string serialized)
        {
            var b = Convert.FromBase64String(serialized);
            using (var stream = new MemoryStream())
            {
                stream.Seek(0, SeekOrigin.Begin);
                return (T)_serializer.ReadObject(stream);
            }
        }

        #endregion
    }
}
