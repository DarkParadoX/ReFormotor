using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoreFormator
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            rtbChanges.Text = About.GetChandes();
        }
        
        private string GetConvertedText(string originalText, IConverter converter)
        {
            try
            {
                return converter.Convert(originalText);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }

        private void btnDdlToProp_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = GetConvertedText(richTextBox1.Text, new ConverterDdlToPropertiest());
        }
        private void btnPropToModel_Click(object sender, EventArgs e)
        {
            richTextBox3.Text = GetConvertedText(richTextBox2.Text, new ConverterPropertiestToModel());
        }

        private Stack<string> sqlRequestsStack = new Stack<string>();
        private void button2_Click(object sender, EventArgs e)
        {
            sqlRequestsStack.Push(tbSqlText.Text);
            tbSqlText.Text = GetConvertedText(tbSqlText.Text, new ConverterSqlToString(checkBox1.Checked, tbVarName.Text, tbReplace.Text)); 
        }
        private void tbRegexReplaseMask_TextChanged(object sender, EventArgs e)
        {
            rtbRegexReplace.Text = GetConvertedText(rtbRegexOrigin.Text, new ConverterRegex(tbRegexMask.Text, tbRegexReplaseMask.Text));
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //Regex regex;
            //try
            //{
            //    regex = new Regex(tbRegexMask.Text, RegexOptions.Multiline);
            //}
            //catch (Exception)
            //{
            //    return;
            //}
            rtbRegexOrigin.SelectAll();
            rtbRegexOrigin.SelectionBackColor = Color.White;
            rtbRegexOrigin.SelectionColor = ColorConverter.GetColorByGroup();

            SelectText(rtbRegexOrigin, tbRegexMask.Text);

            //var originText = rtbRegexOrigin.Text;
            //var matchCollection = regex.Matches(originText);
            //foreach (Match match in matchCollection)
            //{
            //    for (int i = 0; i < match.Groups.Count; i++)
            //    {
            //        rtbRegexOrigin.Select(match.Groups[i].Index, match.Groups[i].Length);
            //        rtbRegexOrigin.SelectionBackColor = ColorConverter.GetBackColorByGroup(i);
            //        rtbRegexOrigin.SelectionColor = ColorConverter.GetColorByGroup(i);
            //    }
            //}
            tbRegexReplaseMask_TextChanged(null, null);
        }

        private void richTextBox4_TextChanged(object sender, EventArgs e)
        {
            SqlColore();
        }

        private void SqlColore()
        {
            tbSqlText.SelectAll();
            tbSqlText.SelectionBackColor = Color.White;
            tbSqlText.SelectionColor = ColorConverter.GetColorByGroup();

            SelectText(tbSqlText, @"\{\d\}", (x => x.Trim('{', '}')));

            //richTextBox4.SelectionStart = cursor;
        }

        private void SelectText(RichTextBox textBox, string template)
        {
            SelectText(textBox, template, null);
        }

        private void SelectText(RichTextBox textBox, string template, Func<string,string> action)
        {
            Regex regex;
            try
            {
                regex = new Regex(tbRegexMask.Text, RegexOptions.Multiline);
            }
            catch (Exception)
            {
                return;
            }

            var cursor = textBox.SelectionStart;

            var originText = textBox.Text;
            var matchCollection = regex.Matches(originText);
            foreach (Match match in matchCollection)
            {
                //int p;
                //if (action == null)
                //    p = Convert.ToInt32();
                //else
                //    p = Convert.ToInt32(action(match.Value));
                for (int i = 0; i < match.Groups.Count; i++)
                {
                    textBox.Select(match.Groups[i].Index, match.Groups[i].Length);
                    textBox.SelectionBackColor = ColorConverter.GetBackColorByGroup(i);
                    textBox.SelectionColor = ColorConverter.GetColorByGroup(i);
                }
            }
            textBox.Select(cursor, 0);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (sqlRequestsStack.Any())
            {
                tbSqlText.Text = sqlRequestsStack.Pop();
            }
        }



        private Stack<Tuple<string, string, string>> regexStack = new Stack<Tuple<string, string, string>>();
        private void CopyRegex_Click(object sender, EventArgs e)
        {
            Tuple<string, string, string> stack = new Tuple<string, string, string>(rtbRegexOrigin.Text,tbRegexMask.Text,tbRegexReplaseMask.Text);
            regexStack.Push(stack);

            rtbRegexOrigin.Text = rtbRegexReplace.Text;
        }

        private void UndoRegex_Click(object sender, EventArgs e)
        {
            if (regexStack.Count == 0)
                return;
            Tuple<string, string, string> stack = regexStack.Pop();
            rtbRegexOrigin.Text = stack.Item1;
            tbRegexMask.Text = stack.Item2;
            tbRegexReplaseMask.Text = stack.Item3;
        }

        private void обновленияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About.CheckUpdate();
        }

        private void btnSqlShorRusChars_Click(object sender, EventArgs e)
        {
            SelectText(tbSqlText, @"[а-яА-Я]");
        }
    }
}
