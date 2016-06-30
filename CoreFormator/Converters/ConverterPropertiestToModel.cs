using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace CoreFormator
{
    public class ConverterPropertiestToModel : IConverter
    {
        public string Convert(string text)
        {
            var strs = text.Split('\n', '\r').Where(x => !String.IsNullOrWhiteSpace(x));
            var res = new List<string>();
            foreach (var str in strs)
            {
                var values = str.Split(' ');
                if (values[1] == "DateTime" || values[1] == "DateTime?")
                {
                    res.Add(String.Format("{{name: '{0}', type: 'date', dateFormat: 'Y-m-dTH:i:s'}}", values[2]));

                }
                else
                    res.Add(String.Format("'{0}'", values[2]));
            }
            return String.Join("," + Environment.NewLine, res);
        }
    }
}