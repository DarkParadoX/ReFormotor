using System;
using System.Collections.Generic;

namespace CoreFormator
{
    public sealed class RegexTemplates
    {
        /// <summary> Символ </summary>
        public string Symbol;
        /// <summary> Значение </summary>
        public string Value;
        /// <summary> Пример </summary>
        public string Example;
        /// <summary> Соответствие </summary>
        public string Compliant;
        /// <summary> Типы выражений </summary>
        public RegexTypeEnum RegexType;

        private RegexTemplates(string symbol, string value, string example, string compliant, RegexTypeEnum type)
        {
            Symbol = symbol;
            Value = value;
            Example = example;
            Compliant = compliant;
            RegexType = type;
        }

        public override string ToString()
        {
            return Symbol;
        }

        private static readonly List<RegexTemplates> Templates;

        static RegexTemplates()
        {
            Templates = new List<RegexTemplates>
                        {
                            new RegexTemplates(@"[...]", @"Любой из символов, указанных в скобках", @"[a-z]", @"В исходной строке может быть любой символ английского алфавита в нижнем регистре", RegexTypeEnum.Symbol),
                            new RegexTemplates(@"[^...]", @"Любой из символов, не указанных в скобках", @"[^0-9]", @"В исходной строке может быть любой символ кроме цифр", RegexTypeEnum.Symbol),
                            new RegexTemplates(@",", @"Любой символ, кроме перевода строки или другого разделителя Unicode-строки", @"", @"", RegexTypeEnum.Symbol),
                            new RegexTemplates(@"\w", @"Любой текстовый символ, не являющийся пробелом, символом табуляции и т.п.", @"", @"", RegexTypeEnum.Symbol),
                            new RegexTemplates(@"\W", @"Любой символ, не являющийся текстовым символом", @"", @"", RegexTypeEnum.Symbol),
                            new RegexTemplates(@"\s", @"Любой пробельный символ из набора Unicode", @"", @"", RegexTypeEnum.Symbol),
                            new RegexTemplates(@"\S", @"Любой непробельный символ из набора Unicode. Обратите внимание, что символы \w и \S - это не одно и то же", @"", @"", RegexTypeEnum.Symbol),
                            new RegexTemplates(@"\d", @"Любые ASCII-цифры. Эквивалентно [0-9]", @"", @"", RegexTypeEnum.Symbol),
                            new RegexTemplates(@"\D", @"Любой символ, отличный от ASCII-цифр. Эквивалентно [^0-9]", @"", @"", RegexTypeEnum.Symbol),

                            new RegexTemplates(@"{n,m}", @"Соответствует предшествующему шаблону, повторенному не менее n и не более m раз", "s{2,4}", "\"Press\", \"ssl\", \"progressss\"", RegexTypeEnum.SymbolRepetition),
                            new RegexTemplates(@"{n,}", @"Соответствует предшествующему шаблону, повторенному n или более раз", "s{1,}", "\"ssl\"", RegexTypeEnum.SymbolRepetition),
                            new RegexTemplates(@"{n}", @"Соответствует в точности n экземплярам предшествующего шаблона", "s{2}", "\"Press\", \"ssl\", но не \"progressss\"", RegexTypeEnum.SymbolRepetition),
                            new RegexTemplates(@"?", @"Соответствует нулю или одному экземпляру предшествующего шаблона; предшествующий шаблон является необязательным", "Эквивалентно {0,1}", "", RegexTypeEnum.SymbolRepetition),
                            new RegexTemplates(@"+", @"Соответствует одному или более экземплярам предшествующего шаблона", "Эквивалентно {1,}", "", RegexTypeEnum.SymbolRepetition),
                            new RegexTemplates(@"*", @"Соответствует нулю или более экземплярам предшествующего шаблона", "Эквивалентно {0,}", "", RegexTypeEnum.SymbolRepetition),

                            new RegexTemplates(@"|", @"Соответствует либо подвыражению слева, либо подвыражению справа (аналог логической операции ИЛИ).", "", "", RegexTypeEnum.CharactersRegularExpressionsSelection),
                            new RegexTemplates(@"(...)", @"Группировка. Группирует элементы в единое целое, которое может использоваться с символами *, +, ?, | и т.п. Также запоминает символы, соответствующие этой группе для использования в последующих ссылках.", "", "", RegexTypeEnum.CharactersRegularExpressionsSelection),
                            new RegexTemplates(@"(?:...)", @"Только группировка. Группирует элементы в единое целое, но не запоминает символы, соответствующие этой группе.", "", "", RegexTypeEnum.CharactersRegularExpressionsSelection),

                            new RegexTemplates(@"^", @"Соответствует началу строкового выражения или началу строки при многострочном поиске.", "^Hello", "\"Hello, world\", но не \"Ok, Hello world\" т.к. в этой строке слово \"Hello\" находится не в начале", RegexTypeEnum.AnchorRegularExpressionCharacters),
                            new RegexTemplates(@"$", @"Соответствует концу строкового выражения или концу строки при многострочном поиске.", "Hello$", "\"World, Hello\"", RegexTypeEnum.AnchorRegularExpressionCharacters),
                            new RegexTemplates(@"\b", @"Соответствует границе слова, т.е. соответствует позиции между символом \w и символом \W или между символом \w и началом или концом строки.", @"\b(my)\b", "В строке \"Hello my world\" выберет слово \"my\"", RegexTypeEnum.AnchorRegularExpressionCharacters),
                            new RegexTemplates(@"\B", @"Соответствует позиции, не являющейся границей слов.", @"\B(ld)\b", "Соответствие найдется в слове \"World\", но не в слове \"ld\"", RegexTypeEnum.AnchorRegularExpressionCharacters)
                        };

        }

        public static List<RegexTemplates> GetTemplateses()
        {
            return Templates;
        }

        public enum RegexTypeEnum
        {
            /// <summary> Классы символов </summary>
            Symbol,
            /// <summary> Символы повторения </summary>
            SymbolRepetition,
            /// <summary> Символы регулярных выражений выбора </summary>
            CharactersRegularExpressionsSelection,
            /// <summary> Якорные символы регулярных выражений </summary>
            AnchorRegularExpressionCharacters
        }
    }
}