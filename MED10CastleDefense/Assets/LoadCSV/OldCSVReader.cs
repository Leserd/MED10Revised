using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;


    public static class OldCSVReader
    {
        private static readonly string SPLIT_RE = @";(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
        private static readonly string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
        private static readonly char[] TRIM_CHARS = { '\"' };

        public delegate void OnDataParsed(List<Dictionary<string, object>> dataRows);

        public static List<Dictionary<string, object>> Read(string data)
        {
            var list = new List<Dictionary<string, object>>();

            var lines = Regex.Split(data, LINE_SPLIT_RE);
            if (lines.Length <= 1)
                return list;

            var header = Regex.Split(lines[0], SPLIT_RE);
            for (var i = 1; i < lines.Length; i++)
            {

                var values = Regex.Split(lines[i], SPLIT_RE);
                if (values.Length == 0 || values[0] == "")
                    continue;

                var entry = new Dictionary<string, object>();
                for (var j = 0; j < header.Length && j < values.Length; j++)
                {
                    string value = values[j];
                    value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");

                    object finalvalue = value;
                    int n;
                    float f;
                    if (int.TryParse(value, out n))
                    {
                        finalvalue = n;
                    }
                    else if (float.TryParse(value, out f))
                    {
                        finalvalue = f;
                    }
                    entry[header[j]] = finalvalue;
                }
                list.Add(entry);
            }
            return list;
        }


        public static List<Dictionary<string, object>> Read(TextAsset textAsset, bool debug = false)
        {
            var list = new List<Dictionary<string, object>>();

            var lines = Regex.Split(textAsset.text, LINE_SPLIT_RE);
            if (lines.Length <= 1)
                return list;

            var header = Regex.Split(lines[0], SPLIT_RE);
            for (var i = 1; i < lines.Length; i++)
            {

                var values = Regex.Split(lines[i], SPLIT_RE);
                if (values.Length == 0 || values[0] == "")
                    continue;

                var entry = new Dictionary<string, object>();
                for (var j = 0; j < header.Length && j < values.Length; j++)
                {
                    string value = values[j];
                    value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");
                    object finalvalue = value;
                    int n;
                    float f;
                    if (int.TryParse(value, out n))
                    {
                        finalvalue = n;
                    }
                    else if (float.TryParse(value, out f))
                    {
                        finalvalue = f;
                    }
                    entry[header[j]] = finalvalue;
                }

                if(debug)
                    Debug.Log("Debug - CSVReader. Row " +i+ ". Keys: " + entry.Keys.Count + ". Values: " + entry.Values.Count);

                list.Add(entry);
            }

            if(debug)
                Debug.Log("Debug - CSVReader. Count: " + list.Count);

            return list;
        }
    }

