using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;

namespace CSVHelper
{
    public class CSV
    {

        public static List<T> Read<T>(string path, bool header) where T : class, new()
        {
            string parentPath = GatParentPath(path);
            if (!CheckPathIsValid(parentPath)) return null;

            List<T> list = new List<T>();



            using (StreamReader sr = new StreamReader(path))
            {


                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] data = line.Split(',');
                    T t = new T();
                    Type type = t.GetType();
                    var props = type.GetProperties();
                    // 以 model property 為主
                    for (int i = 0; i < props.Length; i++)
                    {
                        if (header)
                        {
                            int index = CSVHeaderManger.FindIndex(props[i]);
                            if (index != -1)
                            {
                                props[i].SetValue(t, data[index]);
                            }
                            continue;
                        }

                        props[i].SetValue(t, data[i]);
                    }

                    list.Add(t);
                }
            }

            return list;
        }

        public static void Write<T>(string path, List<T> data, bool append, bool header)
        {
            string parentPath = GatParentPath(path);
            if (!CheckPathIsValid(parentPath))
            {
                Directory.CreateDirectory(parentPath);
            }

            using (StreamWriter sw = new StreamWriter(path, append))
            {
                if (header)
                {
                    sw.WriteLine(GetObjectVaule(data[0], true));
                }

                foreach (T t in data)
                {
                    sw.WriteLine(GetObjectVaule(t, false));
                }
            }
        }

        private static string GatParentPath(string path)
        {
            string[] array = path.Split('\\');
            string newPath = "";
            for (int i = 0; i < array.Length - 1; i++)
            {
                newPath += array[i] + '\\';
            }
            newPath.TrimEnd('\\');

            return newPath;
        }

        private static bool CheckPathIsValid(string path)
        {
            return Directory.Exists(path);
        }

        private static string GetObjectVaule(Object obj, bool header)
        {
            Type type = obj.GetType();
            var props = type.GetProperties();
            string data = "";
            foreach (var prop in props)
            {
                if (header)
                {
                    var displayName = prop.GetCustomAttribute<DisplayNameAttribute>();
                    data += displayName != null ? displayName.DisplayName + "," : prop.Name + ",";
                }
                else
                {

                    data += prop.GetValue(obj).ToString() + ",";
                }
            }
            data = data.TrimEnd(',');
            return data;
        }
    }
}
