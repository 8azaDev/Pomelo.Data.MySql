using System.Runtime.Remoting.Messaging;
using Newtonsoft.Json.Linq;
using static Newtonsoft.Json.JsonConvert;

namespace System
{
    public class JsonObject<T> : IEquatable<JsonObject<T>>, IEquatable<JsonObject>, IEquatable<string>
        where T : class
    {
        private string _originalValue { get; set; }

        public JsonObject() { }

        public JsonObject(T instance)
        {
            Object = instance;
            _originalValue = Json;
        }

        public JsonObject(string json)
        {
            Json = json;
            _originalValue = Json;
        }

        public T Object { get; set; }

        public string Json
        {
            get
            {
                if (Object != null)
                    return SerializeObject(Object);
                else
                    return string.Empty;
            }
            set
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(value))
                        Object = default(T);
                    else
                        Object = DeserializeObject<T>(value);
                }
                catch
                {
                    Object = null;
                }
            }
        }

        public override string ToString()
        {
            return Json;
        }

        public override bool Equals(object obj)
        {
            if (obj == null && Json == Null)
                return true;

            if (obj.GetType().Name == "String")
            {
                var objString = obj as string;
                if (objString == NaN && Json == NaN)
                    return false;

                return Equals(objString);
            }

            try
            {
                dynamic o = obj;
                return Equals((string)o.Json);
            }
            catch
            {
                return base.Equals(obj);
            }
        }

        public bool Equals(JsonObject<T> other)
        {
            if (other == null && Json == Null)
                return true;

            return Equals(other.Json);
        }

        public bool Equals(JsonObject other)
        {
            if (other == null && Json == Null)
                return true;

            return Equals(other.Json);
        }

        public bool Equals(string other)
        {
            if (other == NaN && Json == NaN)
                return false;

            if (string.IsNullOrWhiteSpace(other) ||
               string.CompareOrdinal(other, Undefined) == 0 ||
               string.CompareOrdinal(other, True) == 0 ||
               string.CompareOrdinal(other, False) == 0 ||
               string.CompareOrdinal(other, NegativeInfinity) == 0 ||
               string.CompareOrdinal(other, PositiveInfinity) == 0)
                return string.CompareOrdinal(other, Json) == 0;

            if (!IsSameType(Json, other))
                return false;

            try
            {
                if (IsObject(Json))
                {
                    var o1 = JObject.Parse(Json);
                    var o2 = JObject.Parse(other);
                    return JToken.DeepEquals(o1, o2);
                }
                else
                {
                    var a1 = JArray.Parse(Json);
                    var a2 = JArray.Parse(other);
                    return JToken.DeepEquals(a1, a2);
                }
            }
            catch
            {
                return false;
            }

        }

        public static implicit operator JsonObject<T>(string json)
        {
            return new JsonObject<T>(json);
        }

        public static implicit operator JsonObject<T>(T obj)
        {
            return new JsonObject<T>(obj);
        }

        public static implicit operator JsonObject<T>(JsonObject<object> obj)
        {
            return new JsonObject<T>(obj.Json);
        }

        private static bool IsObject(string json)
        {

            if (string.IsNullOrWhiteSpace(json) ||
                string.CompareOrdinal(json, NaN) == 0 ||
                string.CompareOrdinal(json, Undefined) == 0 ||
                string.CompareOrdinal(json, True) == 0 ||
                string.CompareOrdinal(json, False) == 0 ||
                string.CompareOrdinal(json, NegativeInfinity) == 0 ||
                string.CompareOrdinal(json, PositiveInfinity) == 0)
                return false;

            if (string.CompareOrdinal(json, Null) == 0)
                return true;

            try
            {
                JObject.Parse(json);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static bool IsSameType(string json1, string json2)
        {
            if (IsObject(json1) && IsObject(json2) || !IsObject(json1) && !IsObject(json2))
                return true;
            return false;
        }

        public static bool operator ==(JsonObject<T> a, JsonObject<T> b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(JsonObject<T> a, JsonObject<T> b)
        {
            return !a.Equals(b);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
