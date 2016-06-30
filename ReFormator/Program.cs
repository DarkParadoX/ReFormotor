using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReFormator
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                var localCoreAsemblyPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\CoreFormator.dll";
                var lanCoreAsemblyPath = @"\\Tsr\d\CoreFormator.dll";

#if DEBUG
                File.Copy(@"..\..\..\CoreFormator\bin\Debug\CoreFormator.dll", localCoreAsemblyPath, true);
#else
                try
                {
                    File.Copy(@"..\..\..\CoreFormator\bin\Release\CoreFormator.dll", localCoreAsemblyPath, true);
                    File.Copy(localCoreAsemblyPath, lanCoreAsemblyPath, true);
                }
                catch (Exception)
                {

                }

                if (!File.Exists(localCoreAsemblyPath))
                {
                    File.Copy(lanCoreAsemblyPath, localCoreAsemblyPath, true);
                }
                else
                {
                    string oldVersion = FileVersionInfo.GetVersionInfo(localCoreAsemblyPath).FileVersion;
                    string newVersion = FileVersionInfo.GetVersionInfo(lanCoreAsemblyPath).FileVersion;
                    if (newVersion != oldVersion)
                    {
                        var result = MessageBox.Show("Обнаружена новая версия. Загрузить?", "Новая версия", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                        switch (result)
                        {
                            case DialogResult.Yes:
                                File.Copy(lanCoreAsemblyPath, localCoreAsemblyPath, true);
                                break;
                            case DialogResult.No:

                                break;
                            case DialogResult.Cancel:
                                Application.Exit();
                                return;
                        }
                    }
                }
#endif

                var assembly = Assembly.LoadFrom(localCoreAsemblyPath);
                var mainFormType = assembly.GetType("CoreFormator.MainForm");

                var mainForm = (Form) Activator.CreateInstance(mainFormType);
                mainForm.Text += " " + FileVersionInfo.GetVersionInfo(localCoreAsemblyPath).ProductVersion;
                Application.Run(mainForm);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Application.Exit();
            }
        }
    }
}
