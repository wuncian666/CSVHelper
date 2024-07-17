using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;

namespace CSVHelper
{
    internal class CSVHeaderManger
    {

        static Dictionary<string, int> csvMap = new Dictionary<string, int>();

        public static Dictionary<string, int> CreateHeaderMap(StreamReader sr)
        {
            string line = sr.ReadLine();
            string[] data = line.Split(',');

            for (int i = 0; i < data.Length; i++)
            {
                csvMap.Add(data[i], i);
            }
            return csvMap;
        }

        public static int FindIndex(PropertyInfo props)
        {
            if (csvMap.TryGetValue(props.Name, out int index))
                return index;

            var displayName = props.GetCustomAttribute<DisplayNameAttribute>();
            if (displayName == null)
                return -1;

            if (csvMap.TryGetValue(displayName.DisplayName, out index))
                return index;

            return -1;
        }
    }
}
