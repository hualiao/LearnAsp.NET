using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Hadoop.Avro;
using System.IO;

namespace CSharp.Serializer
{
    internal class AvroSerializer : ISerDeser
    {
        private readonly IAvroSerializer<Person> _serializer = Microsoft.Hadoop.Avro.AvroSerializer.Create<Person>();

        public AvroSerializer(Type type) { }

        #region ISerDeser Member

        public string Serialize<T>(object person)
        {
            using (var ms = new MemoryStream())
            {
                _serializer.Serialize(ms, (Person)person);
                ms.Flush();
                ms.Position = 0;
                return Convert.ToBase64String(ms.ToArray());
            }
        }

        public T Deserialize<T>(string serialized)
        {
            var b = Convert.FromBase64String(serialized);
            using (var strem = new MemoryStream(b))
            {
                strem.Seek(0, SeekOrigin.Begin);
                return (T)((object)_serializer.Deserialize(strem));
            }
        }
        #endregion
    }
}
