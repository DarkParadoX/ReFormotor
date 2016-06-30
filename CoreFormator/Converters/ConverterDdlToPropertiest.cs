using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace CoreFormator
{
    public sealed class ConverterDdlToPropertiest : IConverter
    {
        public string Convert(string text)
        {
            text = new Regex(@"NOT NULL").Replace(text, "NOTNULL");
            text = new Regex(@"DECIMAL\(.+\)").Replace(text, "decimal");
            text = new Regex(@"MONEY\(.+\)").Replace(text, "decimal");
            text = new Regex(@"VARCHAR\(.+\)").Replace(text, "string");
            text = new Regex(@"CHAR\(.+\)").Replace(text, "string");
            text = new Regex(@"SERIAL").Replace(text, "int");
            text = new Regex(@"\s+DATETIME").Replace(text, " DateTime");
            text = new Regex(@"\s+DATE").Replace(text, " DateTime");
            text = new Regex(@"\s+INTEGER").Replace(text, " int");
            text = new Regex(@"\s+SMALLINT").Replace(text, " short");
            text = new Regex(@"default \S+").Replace(text, "");
            text = new Regex(@"YEAR\s+to\s+\S+").Replace(text, "");
            text = new Regex("\"").Replace(text, "");

            var strs = text.Split('\n', '\r');
            var res = new List<string>();

            for (int i = 0; i < strs.Count(); i++)
            {
                if (String.IsNullOrEmpty(strs[i].Trim()))
                {
                    continue;
                }
                string s = strs[i].Trim().TrimEnd(',');
                bool NOTNULL = s.Contains("NOTNULL");
                s = new Regex(@" NOTNULL").Replace(s, "");
                var st = s.Split(' ');
                if (st.Count(x => !string.IsNullOrWhiteSpace(x)) != 2)
                {
                    throw new FormatException("Неверный формат строки " + (i + 1));
                }
                res.Add("public " + st[1] + (NOTNULL || st[1] == "string" ? " " : "? ") + st[0] + " { get; set; }");
            }
            return String.Join(Environment.NewLine, res);
        }
    }
}