using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

[assembly: AssemblyVersion("0.1.5.4")]

namespace CoreFormator
{
    class About
    {
        public static string GetChandes()
        {
            return
@"
[0.1.5.4] --- (20.04.2016)
    + В Regex добавил кнопку ""Скопировать"" которая копирует данные из правого окна в левое
    + В Regex добавил кнопку ""Отмена"" для отмены копирования
    + Добавил кнопку проверки обновлений

[0.1.5.2] --- (14.04.2016)
    * Улучшение кода SQL

[0.1.5.0] --- (14.04.2016)
    + SQL теперь отделяет запросы, разделенные символом ';'
    + На вкладку SQL добавлены 2 текстовые области
        Первая нужна для имени переменной которой будет присвоено конечное значение строки
        Вторая нужна для замены выражений типа {0} на строки из данной области
    + На вкладке SQL в основной области, цветом вылеляются области типа {0}

[0.1.4.0] --- (22.03.2016)
    + Добавлена вкладка с регулярными вырожениями
    + Добавлено автоматическое обновление версии";
        }

        public static void CheckUpdate()
        {
            var localCoreAsemblyPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\CoreFormator.dll";
            var lanCoreAsemblyPath = @"\\Tsr\d\CoreFormator.dll";

            string oldVersion = FileVersionInfo.GetVersionInfo(localCoreAsemblyPath).FileVersion;
            string newVersion = FileVersionInfo.GetVersionInfo(lanCoreAsemblyPath).FileVersion;
            if (newVersion != oldVersion)
            {
                MessageBox.Show("Обнаружена новая версия. Перезапустите программу", "Проверка обновлений", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Обновлений нет", "Проверка обновлений", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
