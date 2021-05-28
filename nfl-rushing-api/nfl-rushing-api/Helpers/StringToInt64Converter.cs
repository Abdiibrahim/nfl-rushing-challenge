using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nfl_rushing_api.Helpers
{
    /// <summary>
    /// Converts json strings to int64 data types (int64 is default type for JsonConverter class)
    /// </summary>
    public class StringToInt64Converter : JsonConverter
    {
        /// <summary>
        /// Default CanConvert method
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns></returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(long);
        }

        /// <summary>
        /// Read json object and return strings as long
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Integer)
                return reader.Value;

            if (reader.TokenType == JsonToken.String)
            {
                long num;
                if (long.TryParse(((string)reader.Value).Replace(",", ""), out num))
                    return num;

                throw new JsonReaderException(string.Format("Expected integer, got {0}", reader.Value));
            }

            throw new JsonReaderException(string.Format("Unexpected token {0}", reader.TokenType));
        }

        /// <summary>
        /// Default WriteJson method
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(value);
        }
    }
}
