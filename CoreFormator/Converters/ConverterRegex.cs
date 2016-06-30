using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace CoreFormator
{
    public class ConverterRegex : IConverter
    {
        private string _mask;
        private string _replace;

        public ConverterRegex(string mask, string replace)
        {
            _mask = mask;
            _replace = new Regex(@"\\(\d)").Replace(replace, "$$$1");
        }
        public string Convert(string originalText)
        {
            Regex regex;
            try
            {
                regex = new Regex(_mask, RegexOptions.Multiline);
            }
            catch (Exception)
            {
                return originalText;
            }

            return regex.Replace(originalText, _replace);
        }
    }
}