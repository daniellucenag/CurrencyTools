using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Api.Middlewares
{
    [ExcludeFromCodeCoverage]
    public class DynamicJsonConverter : JsonConverter<dynamic>
    {

        /// <summary>
        /// Function Read on Class Converter Json Dynamic
        /// </summary>
        /// <param name="reader">Utf8JsonReader</param>
        /// <param name="typeToConvert">Type</param>
        /// <param name="options">JsonSerializerOptions</param>
        public override dynamic Read(ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {

            if (reader.TokenType == JsonTokenType.True)
            {
                return true;
            }

            if (reader.TokenType == JsonTokenType.False)
            {
                return false;
            }

            if (reader.TokenType == JsonTokenType.Number)
            {
                if (reader.TryGetInt64(out long l))
                {
                    return l;
                }

                return reader.GetDouble();
            }

            if (reader.TokenType == JsonTokenType.String)
            {
                if (reader.TryGetDateTime(out DateTime datetime))
                {
                    return datetime;
                }

                return reader.GetString();
            }

            if (reader.TokenType == JsonTokenType.StartObject)
            {
                using JsonDocument documentV = JsonDocument.ParseValue(ref reader);
                return ReadObject(documentV.RootElement);
            }
            JsonDocument document = JsonDocument.ParseValue(ref reader);
            return document.RootElement.Clone();
        }

        private object ReadObject(JsonElement jsonElement)
        {
            IDictionary<string, object> expandoObject = new ExpandoObject();
            foreach (var obj in jsonElement.EnumerateObject())
            {
                var k = obj.Name;
                expandoObject[k] = ReadValue(obj.Value);
            }
            return expandoObject;
        }

#nullable enable
        private object? ReadValue(JsonElement jsonElement)
        {
            object? result;
            switch (jsonElement.ValueKind)
            {
                case JsonValueKind.Object:
                    result = ReadObject(jsonElement);
                    break;
                case JsonValueKind.Array:
                    result = ReadList(jsonElement);
                    break;
                case JsonValueKind.String:
                    if (DateTime.TryParse(jsonElement.ToString(), out DateTime date))
                    {
                        result = date;
                        break;
                    }
                    result = jsonElement.GetString();
                    break;
                case JsonValueKind.Number:
                    result = 0;
                    if (jsonElement.TryGetInt64(out long l))
                    {
                        result = l;
                    }
                    else if (jsonElement.TryGetDouble(out double d))
                    {
                        result = d;
                    }
                    break;
                case JsonValueKind.True:
                    result = true;
                    break;
                case JsonValueKind.False:
                    result = false;
                    break;
                case JsonValueKind.Undefined:
                case JsonValueKind.Null:
                    result = null;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(jsonElement));
            }
            return result;
        }

        private object? ReadList(JsonElement jsonElement)
        {
            IList<object?> list = new List<object?>();
            foreach (var item in jsonElement.EnumerateArray())
            {
                list.Add(ReadValue(item));
            }
            return list.Count == 0 ? null : list;
        }
#nullable restore

        /// <summary>
        /// Function Read on Class Converter Json Dynamic
        /// </summary>
        /// <param name="writer">Utf8JsonWriter</param>
        /// <param name="value">object</param>
        /// <param name="options">JsonSerializerOptions</param>
        public override void Write(Utf8JsonWriter writer,
            object value,
            JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
