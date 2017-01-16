using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MvvX.Plugins.CouchBaseLite.Managers
{
    public class PlatformNewtonsoftJsonSerializer : IJsonSerializer
    {

        public static IJsonSerializer Default
        {
            get;
            private set;
        }

        static PlatformNewtonsoftJsonSerializer()
        {
            Default = new PlatformNewtonsoftJsonSerializer();
        }

        #region Constants

        private static JsonSerializerSettings settings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        private const string TAG = "NewtonsoftJsonSerializer";

        #endregion

        #region Variables

        private JsonTextReader _textReader;


        public JsonToken CurrentToken
        {
            get
            {
                return _textReader == null ? JsonToken.None : (JsonToken)_textReader.TokenType;
            }
        }

        #endregion

        #region IJsonSerializer

        public string SerializeObject(object obj, bool pretty)
        {
            return JsonConvert.SerializeObject(obj, pretty ? Formatting.Indented : Formatting.None, settings);
        }

        public T DeserializeObject<T>(string json)
        {
            T item = JsonConvert.DeserializeObject<T>(json, settings);
            return item;
        }

        public T Deserialize<T>(Stream json)
        {
            using (var sr = new StreamReader(json))
            using (var jsonReader = new JsonTextReader(sr))
            {
                var serializer = JsonSerializer.Create(settings);
                T item = serializer.Deserialize<T>(jsonReader);
                return item;
            }
        }

        public void StartIncrementalParse(Stream json)
        {
            if (_textReader != null)
            {
                ((IDisposable)_textReader).Dispose();
            }

            _textReader = new JsonTextReader(new StreamReader(json));
        }

        public bool Read()
        {
            return _textReader != null && _textReader.Read();
        }

        public T DeserializeNextObject<T>()
        {
            if (_textReader == null)
            {
                return default(T);
            }

            return JToken.ReadFrom(_textReader).ToObject<T>();
        }

        public IDictionary<K, V> ConvertToDictionary<K, V>(object obj)
        {
            if (obj == null)
            {
                return null;
            }

            var jObj = obj as JObject;
            return jObj == null ? null : jObj.ToObject<IDictionary<K, V>>();
        }

        public IList<T> ConvertToList<T>(object obj)
        {
            if (obj == null)
            {
                return null;
            }

            var jObj = obj as JArray;
            return jObj == null ? null : jObj.Select(x => x.ToObject<T>()).ToList();
        }

        public IJsonSerializer DeepClone()
        {
            return new PlatformNewtonsoftJsonSerializer();
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            if (_textReader != null)
            {
                ((IDisposable)_textReader).Dispose();
            }
        }

        #endregion

    }
}