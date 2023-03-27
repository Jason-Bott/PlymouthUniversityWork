using System;
using System.Collections.Generic;
using Xunit;

namespace Exercise.Tests
{
    public class Exercise3A
    {
        private Exercise.Program1 prog;
        public Exercise3A()
        {
            prog = new Program1();
        }
        [Theory]
        [InlineData("two words", " ", 1, 2,'r')]
        public void Test1(string a, string b, int c, int d, char e)
        {
            var value = prog.ExtractCharacter(a, b, c, d);
            Assert.True(value == e, $"For Program1.ReturnLetter:\n You returned {value} but should have returned {e}");
        }

        [Theory]
        [InlineData(new float[] { 1.0f }, 1.0f)]
        [InlineData(new float[] { 1, 4, 3, -4, 1 }, 5)]
        [InlineData(new float[] { 6, 2, 9, 5, -8, 4, -9, -1, 2, -7 }, 3)]
        [InlineData(new float[] { 2, 8, -3, -9, 1, 4, 5, 8, 4, -1, 5, 8, -2, -8.115f, -4, -2, -9, -9, -8, -7, 3, 4, -6, -9, 8, 7, -3, -6, -5, 2, -6, 3, 9, -5, -6, 5, 6, 3, -9, 6, 1, 2, 3, 6, 9, 4, 8, 2, 9, 3 }, 30.885)]
        [InlineData(new float[] { -1, 2, -1, -4, 1.2f, -3, 2, 3, -1, 2, -4, 3, 2, -1, -2, 4, 3, -4, 3, -3, -2, -4, 1, -4, 3, 2, -3, 2, 2, -3, 3, 3, 3, -4, 2, -3, -2, 4, -4, 1, -2, 1, -3, -3, 1, 1, -2, 2, -1, -4 }, -11.8)]
        public void Test2(float[] a, float b)
        {
            var value = prog.CalculateSumOfElements(new List<float>(a));
            value = (int)(value * 1000) / 1000.0f;
            Assert.True(value.Equals(b), $"For Program1.CalListSum:\n You returned {value} but should have returned {b}");
        }
        

        [Theory]
        [InlineData(new string[] { "-1", "2", "3" }, 1)]
        [InlineData(new string[] { "3", "3", "0", "-3", "-3", "2", "1", "0", "-4", "-5", "1", "-4", "-3", "1", "1", "-5", "3", "-4", "-2", "-1" }, 10)]
        [InlineData(new string[] { "3", "3", "a", "-3", "four", "1", "2", "1", "-4", "-1", }, 3)]
        [InlineData(new string[] { "-4", "-5", "-5", "-3", "4a", "-4", "three houses", "-1", "-4", "-4" }, 8)]
        [InlineData(new string[] { "-1,-17", "-24", "-23", "-30,-3", "-32", "-22", "4", "-33" }, 5)]
        public void Test3(string[] a, int b)
        {
            var value = prog.CountNegativeNumbers(new List<string>(a));

            Assert.True(value == b, $"For Program1.AddNegNumbers:\n You returned {value} but should have returned {b}");
        }

        [Theory]
        [InlineData(new string[] { "-2", "-10", "- 8" }, 0)]
        [InlineData(new string[] { "O", "9", "4", "7", "-7", }, -7)]
        [InlineData(new string[] { "-9", "2", "-5", "-9", "7", "-5", "-2", "-7", "-2", "-9", }, -17)]
        [InlineData(new string[] { "-9", "2 a", "- 5", "-9", "7", "-5", "-6", "- 7", "-2", "-9 -", }, -11)]
        [InlineData(new string[] { "8 house 3", " boom -3", "-8,0", " -8", "-5", "- -10", "2", "-9 9", "4", "-5", "-2,3,4", "-9,-3", "-4", "3", "- - - - 3", }, -14)]
        [InlineData(new string[] { "1O", "-8","A 11","9", "4", "7", "-7", }, -7)]
        [InlineData(new string[] { "-9", "2", "-5", "-9", "7", "-9", "2", "-5", "-9", "7"," S", "-5", "-2", "-7", "-2", "-9", }, -22)]
        [InlineData(new string[] { "A-9", "2 a", "- 5", "-9", "7", "-9", "2", "-5", "-9", "7", "-5", "-6", "- 7", "-2", "-9 -", }, -16)]
        [InlineData(new string[] { "a house 3", " boom -3", "-8,0", " -8", "-5", "- -10", "2", "-9 9", "-4", "-5", "-2,3,4", "-9,-3", "-4", "3", "- - - - 3", }, -18)]
        public void Test4(string[] a, int b)
        {
            var value = prog.AddNegNumbersFromFixedInterval(new List<string>(a));

            Assert.True(value == b, $"For Program1.AddNegNumbersFromFixedInterval:\n You returned {value} but should have returned {b}");
        }

        [Theory]
        [InlineData(new int[] { 1,2,3 }, 2, 1, 2)]
        [InlineData(new int[] { 1, 2, 3, 4, 5, 6, 7 }, 5, 1, 3)]
        [InlineData(new int[] { 1, 5, 3 }, 0, 1, 2)]
        [InlineData(new int[] { -1, 2, -3, 4, 5, 6, -7 }, 1, -2, 3)]
        [InlineData(new int[] { 1, 5, 3, 100, -30, 2, 2, 3, -10, 70 }, 46, -31, 90)]
        public void Test5(int[] a, float b, int c, int d)
        {
            var value = prog.SumValuesWithinInterval(new List<int>(a),c,d);
            
            Assert.True((char)value == b, $"For Program1.CalSumBetweenValues:\n You returned {value} but should have returned {b}");
        }



        [Theory]
        [InlineData("1 + 2", " ", 3)]
        [InlineData("1 - 3", " ", -2)]
        [InlineData("1 + 2 - 3", " ", 0)]
        [InlineData("1 + 2 + 10 - 2 + 5 + 2 + -1 + 15", " ", 32)]
        [InlineData("1x+x2", "x", 3)]
        [InlineData("1sSs+sSs2sSs+sSs10sSs-sSs2sSs+sSs5sSs +sSs2sSs+sSs -1sSs+sSs 15","sSs", 32)]
        [InlineData("1 + 20 + -10 - -2 + 5000 + 2 + -1000000 + -15", " ", -995000)]
        public void Test6(string a, string b, int c)
        {
            var value = prog.PerformNaiveCalculation(a, b );
            Assert.True(value == c, $"For Program1.PerformCalculation:\n You returned {value} but should have returned {c}");
        }

        [Theory]
        [InlineData(new string[] { "5", "add1"}, "add", "",5)]
        [InlineData(new string[] { "5", "ignore1", "-8", "add2", "3", "ignore2", "3", "add1", "9", "  0" }, "add", "  " ,4)]
        [InlineData(new string[] { "16", "add0", "11", "add1", "4", "add2", "19", "add3", "5", "mad4", "1", "mad5", "11", "add6", "15", "add7", "17", "mad8", "10", "add9", "17", "add10", "0", "add11", "1", "mad12", "11", "gad13", "12", "add14", "18", "add15", "2", "add16", "17", "add17", "16", "mad18", "19", "gad19" },"mad", "gad" ,70)]
        [InlineData(new string[] { "-7", "ore234526", "-2", "correct49921", "-5", "correct95472", "-5", "correct91619", "-5", "correct75508", "-5", "morrect81071", "7", "correct93595", "-3", "correct50713", "3", "correct33333", "6", "correct81799" },"ore", "mor" ,-12)]
        [InlineData(new string[] { "19", "defaul97027", "13", "defaul88276", "12", "defaul71507", "-17", "defaul58673", "6", "defaul81262", "-3", "defaul2192", "1", "default0", "16", "default1", "7", "default2", "-16", "default3", "13", "tefault4", "5", "tefault5", "2", "defaul16539", "12", "tefault6", "-13", "defaul10704" }, "default","tefault",38)]
        public void Test7(string [] a, string b, string bb, int c)
        {
            var dictionary = new Dictionary<string, double>();
            for (int i = 0; i < a.Length - 1; i += 2)
            {
                double number = 0;
                double.TryParse(a[i], out number);
                dictionary.Add(a[i + 1], number);
            }
            var value = prog.ExtractAndCalculateValues(dictionary, b, bb);
            Assert.True(value == c, $"For Program1.ExtractAndCalculateValues:\n You returned {value} but should have returned {c}");
        }




    }
}
