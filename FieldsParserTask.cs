using System;
using System.Text;
using System.Collections.Generic;
using NUnit.Framework;

namespace TableParser {
    [TestFixture]
    public class FieldParserTaskTests {
        public static void Test(string input, string[] expectedResult) {
            var actualResult = FieldsParserTask.ParseLine(input);
            Assert.AreEqual(expectedResult.Length, actualResult.Count);
            for (int i = 0; i < expectedResult.Length; ++i) {
                Assert.AreEqual(expectedResult[i], actualResult[i].Value);
            }
        }

// Пустые поля
[TestCase("", new string[0])]
[TestCase("''", new[] { "" })]
[TestCase("' '", new[] { " " })]
        
// Простые поля
[TestCase("text", new[] { "text" })]
[TestCase("hello world", new[] { "hello", "world" })]
[TestCase("Lorem Ipsum is simply", new[] { "Lorem", "Ipsum", "is", "simply"})]
[TestCase("Long    space     is  long", new[] { "Long", "space", "is", "long" })]

// Числа
[TestCase("100", new[] { "100" })]
[TestCase("100_000_000", new[] { "100_000_000" })]
[TestCase("0xFF00", new[] { "0xFF00" })]

// Спецсимволы
[TestCase("more - or - less", new[] { "more", "-", "or", "-", "less" })]
[TestCase("word in 'quotes'", new[] { "word", "in", "quotes" })]
[TestCase("\"escape\" symbols", new[] { "escape", "symbols" })]
[TestCase("\"'escape symbols'\"", new[] { "'escape symbols'" })]
[TestCase("'\"escape symbols\"'", new[] { "\"escape symbols\"" })]
[TestCase("'\"escape symbols\"'", new[] { "\"escape symbols\"" })]
[TestCase("\'escaped ", new[] { "escaped " })]
[TestCase("\"\\\\\"", new[] { "\\" })]
[TestCase("\"\'escaped\'\" strings", new[] { "'escaped'", "strings" })]
[TestCase("\'\"\'", new[] { "\"" })]

// символ @ и @"буквальные" строки
[TestCase(@"'\\'", new[] { @"\" })]
[TestCase(@"'\'", new[] { @"'" })]
[TestCase(@"'literal strings'", new[] { @"literal strings" })]
[TestCase(@"!'literal strings'", new[] { @"!", "literal strings" })]
[TestCase(@"'literal strings'  !", new[] { @"literal strings", "!" })]
[TestCase(@"   'literal strings' ", new[] { @"literal strings" })]
[TestCase(@"""\""literal""", new[] { @"""literal" })]
        
public static void RunTests(string input, string[] expectedOutput) {
    Test(input, expectedOutput);
}

    }

    public class FieldsParserTask {
        // При решении этой задаче постарайтесь избежать создания методов, длиннее 10 строк.
        // Подумайте как можно использовать ReadQuotedField и Token в этой задаче.
        public static List<Token> ParseLine(string line) {
            return new List<Token> { ReadQuotedField(line, 0) }; // сокращенный синтаксис для инициализации коллекции.
        }

        private static Token ReadField(string line, int startIndex) {
            return new Token(line, 0, line.Length);
        }

        public static Token ReadQuotedField(string line, int startIndex) {
            return QuotedFieldTask.ReadQuotedField(line, startIndex);
        }
    }
}