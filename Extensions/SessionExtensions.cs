using Newtonsoft.Json;
using System;
using System.IO;

namespace INFT3050.Extensions
{
    public static class SessionExtensions
    {
        private static readonly string SessionFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SessionData");

        static SessionExtensions()
        {
            if (!Directory.Exists(SessionFolderPath))
            {
                Directory.CreateDirectory(SessionFolderPath);
            }
        }

        public static void SetObjectAsJsonWithExpiry(this ISession session, string key, object value, TimeSpan expiry)
        {
            var expiringObject = new ExpiringObject
            {
                Value = value,
                ExpiryDate = DateTime.UtcNow.Add(expiry)
            };

            var sessionData = JsonConvert.SerializeObject(expiringObject);
            session.SetString(key, sessionData);

            SaveToFile(key, sessionData);
        }

        public static T GetObjectFromJsonWithExpiry<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            if (value == null)
            {
                value = LoadFromFile(key);
                if (value != null)
                {
                    session.SetString(key, value);
                }
            }

            if (value == null)
            {
                return default(T);
            }

            var expiringObject = JsonConvert.DeserializeObject<ExpiringObject>(value);
            if (DateTime.UtcNow > expiringObject.ExpiryDate)
            {
                session.Remove(key);
                DeleteFile(key);
                return default(T);
            }

            return JsonConvert.DeserializeObject<T>(expiringObject.Value.ToString());
        }

        private static void SaveToFile(string key, string data)
        {
            var filePath = GetFilePath(key);
            File.WriteAllText(filePath, data);
        }

        private static string LoadFromFile(string key)
        {
            var filePath = GetFilePath(key);
            if (File.Exists(filePath))
            {
                return File.ReadAllText(filePath);
            }
            return null;
        }

        private static void DeleteFile(string key)
        {
            var filePath = GetFilePath(key);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        private static string GetFilePath(string key)
        {
            return Path.Combine(SessionFolderPath, $"{key}.json");
        }

        private class ExpiringObject
        {
            public object Value { get; set; }
            public DateTime ExpiryDate { get; set; }
        }
    }
}
