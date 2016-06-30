using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace CoreFormator
{
    public class ConverterSqlToString : IConverter
    {
        private bool _newLine;
        private string _replace;
        private string _varName;

        public ConverterSqlToString(bool newLine, string varName, string replace)
        {
            _newLine = newLine;
            _varName = varName;
            _replace = replace;
        }
        public string Convert(string text)
        {
            var regex = new Regex(@"^\s*(-{2,})\s*$", RegexOptions.Multiline);
            var requests = regex.Replace(text, ";$1;");// Regex.Replace(text, , ";$1;");
            List<string> list = new List<string>();
            foreach (var item in requests.Split(';').ToList())
            {
                list.Add(ConverRequest(item));
            }
            return String.Join(Environment.NewLine, list);
        }

        private string ConverRequest(string text)
        {
            List<string> list = new List<string>();
            var strs = String.Format(text, _replace.Split('\r')).Split('\n', '\r').ToList();


            strs.RemoveAll(String.IsNullOrWhiteSpace);
            if (strs.Count == 0)
                return null;

            var minSpases = strs.Select(x => Regex.Replace(x, @"^( *).+", "$1").Length).ToList().Min();
            strs = strs.Select(x => x.Substring(minSpases).TrimEnd()).ToList();

            var max = strs.Max(x => x.Length) + (strs.Count() > 1 ? 3 : 0);

            foreach (string item in strs)
            {
                var res = String.Format("\"{0}\"", item.PadRight(max));
                list.Add(res);
            }

            if (!String.IsNullOrWhiteSpace(_varName))
            {
                list[0] = _varName + " = " + list[0];
                if (list.Count > 1)
                    for (int i = 1; i < list.Count; i++)
                    {
                        list[i] = new string(' ', _varName.Length + 3) + list[i];
                    }
            }
            var count = minSpases;
            return String.Join((_newLine ? " + Environment.NewLine" : "") + " +" + Environment.NewLine, list) + ";" + Environment.NewLine;
        }
    }
}