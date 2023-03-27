using System;
using System.Collections.Generic;
using Xunit;

namespace Exercise.Tests
{
    public class UnitTestA
    {
        private ProgramA prog;
        public UnitTestA()
        {
            prog = new ProgramA();
        }


        [Theory]
        [InlineData(new int[] { 6, 23, 18, 5, 21, 24, 14, 0, 22, }, new string[] { "pkum", "kvbcoc", "umbtclxk", "mmiw", "vxalu", "khwhhd", "ram", "wplsutamlooj", "jrd", })]
        [InlineData(new int[] { -2, -1, 14, 11, 15, -3, 16, 20, }, new string[] { "elqhjshcgfd", "hlrtbtgdqqjef", "hrn", "ktq", "bpjvjbvfyj", "ujitxfvksv", "xtps", "hreeool", })]
        [InlineData(new int[] { -7, -10, 3, -9, -6, 9, 11, -8, 0, 23, 6, -2, 24, }, new string[] { "hfy", "sbeafbae", "hlkqoiri", "myrwbqb", "seugdfyaq", "syliy", "ua", "eraoyrxomix", "oskgr", "cvbwjufxk", "qqafacjwj", "bdmyxd", "euytvhkqve", })]
        public void Test0(int[] a, string[] b)
        {

            var outcome = prog.BuildDictionary(a, b);
            Random rand = new Random();

            for (int i = 0; i < 3; i++)
            {
                var index = rand.Next(0, a.Length - 1);
                Assert.True(outcome.Keys.Count == a.Length, "A: CreateDictionary: Missing keys in dictionary");
                Assert.True(outcome.ContainsKey(a[index]), $"A: CreateDictionary: Missing key {a[index]} in dictionary");
                Assert.True(outcome[a[index]].Equals(b[index]), "A: CreateDictionary: Elements are not paired correctly");
            }

        }

        [Theory]
        [InlineData(new string[] { "32", "7", "15", "25", "12", "21", "4", "28", "8", "18", }, new string[] { "FBGHUPP", "AQSR", "CR", "URDY", "DLLTS", "YQTCEBS", "AD", "QFVXBWLQQNQ", "PMEPNMLSQ", "TNAUNDNAdfTUSXJMH", }, 0, 0, -1)]
        [InlineData(new string[] { "32", "7", "15", "25", "12", "21", "4", "28", "8", }, new string[] { "FBGHUPP", "AQSR", "CR", "URDY", "DLLTS", "YQzTCEBS", "AD", "QFVXBWLQQNQ", "PMEPNMLSQ", "TNAUNDNATUSXJMH", }, 1, 0, -1)]
        [InlineData(new string[] { "32", "-7", "15", "25", "12", "21", "4", "18", }, new string[] { "FBGHUPP", "AQSR", "CR", "UgRDY", "DLLTS", "YQTCEBS", "AD", "QFVXBWLQQNQ", "PMEPNMzSQ", "TNAUNDXATUSXJMH", }, 2, 0, -1)]
        [InlineData(new string[] { "32", "7", "15", "25", "12", "21", "4", "28", "8", "18", "37" }, new string[] { "FBGHUPP", "AQSR", "CR", "UsRDY", "DLLTS", "YQTCEBS", "AD", "QFVXBWLQQNQ", "PMEaPNMLSQ", "TNAUNDNATUSXJMH", }, 0, 1, -1)]
        [InlineData(new string[] { "32", "7", "15", "25", "12", "21", "4", "28", "18", "18" }, new string[] { "FBGHUPP", "AQSR", "CR", "UsRDY", "DLLTS", "YQTCEBS", "AD", "QFVXBWLQQNQ", "PMEaPNMLSQ", "TNAUNDNATUSXJMH", }, 0, 0, 9)]
        [InlineData(new string[] { "32", "32", "15", "25", "12", "21", "4", "28", "8", "18", "37" }, new string[] { "FBGHUPP", "AQSR", "CR", "UsRDY", "DLLTS", "YQTCEBS", "AD", "QFVXBWLQQNQ", "PMEaPNMLSQ", "TNAUNDNATUSXJMH", }, 0, 0, 1)]
        public void Test1(string[] a, string[] b, int c, int d, int e)
        {

            var outcome = prog.BuildDictionaryBeta(b, a);
            Random rand = new Random();

            for (int i = 0; i < 3; i++)
            {
                if (d == 0)
                {
                    var index = rand.Next(0, a.Length - 1);
                    if (e >= 0 && index == e)
                    {
                        index++;
                    }
                    else
                    {
                        Assert.True(outcome.Keys.Count == a.Length + c, "A: CreateDictionary2: Missing keys in dictionary");
                        Assert.True(outcome.ContainsKey(a[index]), $"A: CreateDictionary2: Missing key {a[index]} in dictionary");
                        Assert.True(outcome[a[index]].Equals(b[index]), "A: CreateDictionary2: Elements are not paired correctly");
                    }
                }

            }
            if (c > 0)
            {
                for (int i = b.Length - 1; i >= a.Length; i--)
                    Assert.True(outcome[$"{i}"] == b[i], $"A: CreateDictionary2: Missing key for value {b[i]} in dictionary");
            }
            if (d > 0)
            {
                for (int i = a.Length - 1; i >= b.Length; i--)
                    Assert.True(outcome[a[i]] == "missing", $"A: CreateDictionary2: Missing value for key {a[i]} in dictionary");
            }
            if (e > -1)
            {
                int value = 0;
                if (int.TryParse(a[e], out value))
                    Assert.True(outcome[$"{value + 1}"] == b[e], $"A: CreateDictionary2: Wrong fix for doubled up key {a[e]}");
            }




        }




        [Theory]
        [InlineData(new string[] { "-6", "bed 0", "-8", "bed 1", }, 0, -14, 48, 0.75)]
        [InlineData(new string[] { "-6", "bed 0", "-8", "bed 1", "-4", "head 2", "-7", "bed 3", "8", "head 4" }, 1, -25, 1344, 0.026785714285714284)]

        [InlineData(new string[] { "3", "head 0", "0", "head 1", "-7", "head 2", "-2", "head 3", "-8", "head 4", "-1", "head 5", "-10", "head 6", "-5", "head 7", "0", "head 8", "-7", "head 9", "-7", "head 10", "7", "head 11", "-4", "head 12", "9", "head 13", "6", "head 14", "6", "head 15", "8", "head 16", "-10", "head 17", "-3", "head 18", "5", "head 19", "1", "head 20", "-8", "head 21", "-4", "head 22", "-10", "head 23", "7", "head 24", "-2", "head 25", "-9", "head 26", "4", "head 27", "-9", "head 28", "7", "head 29", "9", "head 30", "0", "head 31", "-8", "head 32", "8", "head 33", "0", "head 34", "-5", "head 35", "8", "head 36", "-2", "head 37", "5", "head 38", "3", "head 39", "4", "head 40", "7", "head 41", "-10", "head 42", "-6", "head 43", "-4", "head 44", "-5", "head 45", "-1", "head 46", "-2", "head 47", "9", "head 48", "-3", "head 49", "-9", "head 50", "0", "head 51", "3", "head 52", "5", "head 53", "7", "head 54", "9", "head 55", "7", "head 56", "5", "head 57", "5", "head 58", "2", "head 59", "9", "head 60", "3", "head 61", "0", "head 62", "4", "head 63", "-3", "head 64", "2", "head 65", "9", "head 66", "-5", "head 67", "-2", "head 68", "-7", "head 69", "-4", "head 70", "5", "head 71", "-4", "head 72", "8", "head 73", "1", "head 74", "-7", "head 75", "7", "head 76", "-7", "head 77", "-4", "head 78", "0", "head 79", "0", "head 80", "7", "head 81", "-1", "head 82", "4", "head 83", "-5", "head 84", "2", "head 85", "-1", "head 86", "2", "head 87", "0", "head 88", "8", "head 89", "0", "head 90", "-9", "head 91", "-3", "head 92", "-2", "head 93", "-4", "head 94", "-3", "head 95", "-4", "head 96", "-5", "head 97", "-10", "head 98", "5", "head 99" }, 42, -251, 0, double.NaN)]
        [InlineData(new string[] { "-24", "cat 0 running", "-146", "cat 1 running", "-927", "cat 2 running", "-592", "little 3", "55", "cat 4 running", "-551", "cat 5 running", "-828", "cat 6 running", "-7", "little 7", "-17", "cat 8 running", "-230", "cat 9 running" }, 1, -3322, -2.4011664765924647E+19, -2.3988340900769659E-17)]
        public void Test2(string[] values, int neg, double sum, double mult, double div)
        {
            var dictionary = new Dictionary<string, double>();
            for (int i = 0; i < values.Length - 1; i += 2)
            {
                double number = 0;
                double.TryParse(values[i], out number);
                dictionary.Add(values[i + 1], number);
            }
            var count = dictionary.Count;
            var outcome = prog.ModifyDictionaryBeta(dictionary);
            Assert.True(outcome.ContainsKey("sum"), "Missing key for the sum of the entries");
            Assert.True(outcome.ContainsKey("mult"), "Missing key for the product of the entries");
            Assert.True(outcome.ContainsKey("div"), "Missing key for the division result of the entries");
            Assert.True(outcome.Count == count + 3 - neg, "The positive elements were not removed");
            try
            {
                var vsum = Math.Round(outcome["sum"], 10);
                var vmult = Math.Round(outcome["mult"], 10);
                var vdiv = Math.Round(outcome["div"], 10);
                Assert.True(vsum == Math.Round(sum, 10), $"You should have returned sum: {sum}, but did return sum: {vsum}.");
                Assert.True(vmult == Math.Round(mult, 10), $"You should have returned mult: {mult} but did return mult: {vmult}.");
                if (!double.IsNaN(div))
                    Assert.True(vdiv == Math.Round(div, 10), $"You should have returned div: {div} but did return div: {vdiv}.");
                else
                    Assert.True(double.IsNaN(vdiv), $"You should have returned div: {div} but did return div: {vdiv}.");
            }
            catch (KeyNotFoundException ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
        }

        [Theory]
        [InlineData(new string[] { "-1", "cat 0", "-4", "lille 1", "3", "cat 2", "-4", "cat 3", "2", "lille 4", }, 2, -2, 12, "lille")]
        [InlineData(new string[] { "4", "cat 0", "2", "cat 1", "1", "cat 2", "-1", "cat 3", "4", "cat 4" }, 0, 10, -32, "mouse")]
        [InlineData(new string[] { "7", "lille 0", "-7", "lille 1", "0", "lille 2", "-7", "lille 3", "3", "lille 4" }, 5, 0, 0, "lille")]
        [InlineData(new string[] { "-5", "cat 0", "-4", "cat 1 mouse", "2", "cat 2 mouse", "-5", "cat 3", "3", "cat 4", "-3", "cat 5", "1", "cat 6", "2", "cat 7 mouse", "-1", "cat 8", "0", "cat 9 mouse", "-4", "cat 10 mouse", "1", "cat 11", "4", "cat 12 mouse", "-1", "cat 13", "-3", "cat 14 mouse", "-3", "cat 15", "4", "cat 16", "0", "cat 17 mouse", "3", "cat 18 mouse", "-2", "cat 19", "2", "cat 20", "-2", "cat 21", "-4", "cat 22", "4", "cat 23", "1", "cat 24", "-4", "cat 25 mouse", "2", "cat 26 mouse", "-4", "cat 27", "0", "cat 28", "0", "cat 29 mouse", "-2", "cat 30 mouse", "-5", "cat 31", "3", "cat 32", "-2", "cat 33", "1", "cat 34 mouse", "4", "cat 35", "4", "cat 36", "-5", "cat 37 mouse", "-2", "cat 38", "4", "cat 39 mouse", "-5", "cat 40", "-1", "cat 41", "-2", "cat 42 mouse", "0", "cat 43", "4", "cat 44", "-1", "cat 45 mouse", "-2", "cat 46 mouse", "-5", "cat 47 mouse", "-3", "cat 48", "0", "cat 49", "2", "cat 50", "1", "cat 51 mouse", "-3", "cat 52 mouse", "0", "cat 53 mouse", "0", "cat 54", "3", "cat 55", "-4", "cat 56 mouse", "2", "cat 57", "0", "cat 58 mouse", "4", "cat 59", "0", "cat 60", "0", "cat 61", "-2", "cat 62", "-5", "cat 63", "-4", "cat 64", "-1", "cat 65", "1", "cat 66 mouse", "-4", "cat 67 mouse", "-1", "cat 68 mouse", "3", "cat 69 mouse", "-5", "cat 70 mouse", "3", "cat 71 mouse", "-1", "cat 72", "1", "cat 73 mouse", "3", "cat 74 mouse", "-3", "cat 75", "-4", "cat 76", "1", "cat 77 mouse", "4", "cat 78", "0", "cat 79", "1", "cat 80", "-1", "cat 81", "4", "cat 82 mouse", "0", "cat 83", "-1", "cat 84", "2", "cat 85", "-4", "cat 86", "-5", "cat 87 mouse", "-3", "cat 88 mouse", "4", "cat 89", "-1", "cat 90", "-1", "cat 91", "-5", "cat 92", "-4", "cat 93", "-3", "cat 94", "0", "cat 95", "4", "cat 96 mouse", "-5", "cat 97 mouse", "0", "cat 98 mouse", "1", "cat 99 mouse" }, 41, -35, 0, "mouse")]
        public void Test3(string[] values, int neg, double sum, double mult, string remove)
        {
            var dictionary = new Dictionary<string, int>();
            for (int i = 0; i < values.Length - 1; i += 2)
            {
                int number = 0;
                int.TryParse(values[i], out number);
                dictionary.Add(values[i + 1], number);
            }
            var count = dictionary.Count;
            prog.ModifyDictionary(dictionary, remove);
            Assert.True(dictionary.ContainsKey("sum"), "Missing key for the sum of the entries");
            Assert.True(dictionary.ContainsKey("mult"), "Missing key for the product of the entries");
            Assert.True(dictionary.Count == count + 2 - neg, "The selected elements were not removed");
            try
            {
                var vsum = dictionary["sum"];
                var vmult = dictionary["mult"];
                Assert.True(vsum == sum, $"You should have returned sum: {sum}, but did return sum: {vsum}.");
                Assert.True(vmult == mult, $"You should have returned mult: {mult} but did return mult: {vmult}.");
            }
            catch (KeyNotFoundException ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
        }

        [Theory]
        [InlineData(new string[] { "-1", "rupt cor0", "-4", "corrupt1", "3", "cat 2", "-4", "corupt 3", "2", " 4 cor ru pt4", }, 3, 0, 24, "corrupt")]
        [InlineData(new string[] { "4", "cat 0hous", "2", "cat 1 mouse", "1", "hat 2 ous", "-1", "cat 3", "4", "hou cat 4e", "4", "houecat 4e" }, 4, 14, -128, "house")]
        [InlineData(new string[] { "7", "Lli belle 0", "-7", "lAiBlClDe 1", "0", "lie downlle 2", "-7", "lilL le 3", "3", "l ill e 4", "3", "lile 4" }, 4, -1, 0, "lille")]
        public void Test4(string[] values, int neg, double sum, double mult, string remove)
        {
            var dictionary = new Dictionary<string, double>();
            for (int i = 0; i < values.Length - 1; i += 2)
            {
                double number = 0;
                double.TryParse(values[i], out number);
                dictionary.Add(values[i + 1], number);
            }


            List<double> results = prog.ModifyDictionaryGamma(dictionary, remove);
            var count = results.Count;
            Assert.True(results[results.Count-2] == sum, "Sum calculation is wrong in the list");
            Assert.True(results[results.Count - 1] == mult, "The product is wrong in the list");
            Assert.True(values.Length/2 == count + 2 - neg, "The selected elements were not removed from the new list");

        }


    }
}
