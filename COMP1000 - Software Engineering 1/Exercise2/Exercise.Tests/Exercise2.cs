using System;
using Xunit;

namespace Exercise.Tests
{
    public class Exercise2
    {
        private Exercise.Program1 prog;
        public Exercise2()
        {
            prog = new Program1();
        }
        [Theory]
        [InlineData('d', "word", 12)]
        [InlineData('a', "house", 0)]
        [InlineData('c', "jjja", 55)]
        [InlineData('1', "jjja", -55)]
        public void Test1(char a, string b, int c)
        {
            var value = prog.ReturnNumercial(b, a, c);
            Assert.True(value == c, $"You returned {value} but should have returned {c}");
        }

        [Theory]
        [InlineData('a', "word", 12)]
        [InlineData('d', "house", 0)]
        [InlineData('d', "jjja", 55)]
        [InlineData('1', "jjja", -55)]
        [InlineData('0', "0", 55)]
        [InlineData('3', "a", -15)]
        public void Test2(char a, string b, int c)
        {
            var value = prog.PicksLetter(b, a, c);
            Assert.True((char)value == a, $"You returned {value} but should have returned {a}");
        }

        [Theory]
        [InlineData("a", "word", "", "")]
        [InlineData("an", "word", "a", "a")]
        [InlineData("a long staff", "word", "other word", "word")]
        [InlineData("word", "word", "word", "word")]
        [InlineData("house", "a", "snake", "a")]
        [InlineData("hole", "shoes", "snake", "hole")]
        public void Test3(string a, string b, string c, string d)
        {
            var value = prog.RetrievesShorestText(a, b, c);
            Assert.True(value == d, $"You returned {value} but should have returned {d}");
        }

        [Theory]
        [InlineData(1, 1, "one", 2)]
        [InlineData(1, 2, "one", 3)]
        [InlineData(1, 2, "11", 3)]
        [InlineData(10, 2, "11", 12)]
        public void Test4(int a, int b, string c, int d)
        {
            var value = prog.CalculatSumA(a, b, c);
            Assert.True(value == d, $"You returned {value} but should have returned {d}");
        }

        [Theory]
        [InlineData(1, 1, 3, 'a', 1)]
        [InlineData(0.1f, 11.1f, 12.2f, '1', 1)]
        [InlineData(0.5f, .5f, 3.0f, '2', 2.0f)]
        [InlineData(-100.1f, .1f, -95.0f, 'T', 5)]
        public void Test5(float a, float b, float c, char d, int e)
        {
            var value = prog.CalculatSumB(a, d, e, b);
            value = (int)(value * 1000) / 1000.0f;
            Assert.True( value ==c , $"You returned {value} but should have returned {c}");
        }

        [Theory]
        [InlineData(new float[] { 1, 1 }, 2)]
        [InlineData(new float[] { 0.1f, 1, 0.0f }, 1.1f)]
        [InlineData(new float[]{-0.5f, 2,1,-1,100,-50,-50}, 1.5f)]
        [InlineData(new float[] { 0.0f }, 0.0f)]
        [InlineData(new float[] { -1.1f, 1, -0.1f },-0.2f)]
        public void Test6(float [] a, float c)
        {
            var value = prog.CalculatSum3(a);
            value = (int)(value * 1000) / 1000.0f;
            Assert.True(value == c, $"You returned {value} but should have returned {c}");
        }


        [Theory]
        [InlineData("Hello my friend",' ',"Hello")]
        [InlineData("Mein neues Spiel.", ' ', "Mein")]
        [InlineData("Kein schoener Tag als Heute.", ' ', "Kein")]
        [InlineData("Mein-neues-Spiel.", '-', "Mein")]
        public void Test7(string a, char b, string c)
        {
            var value = prog.ReturnFirstSubText(a, b);
            Assert.True(value == c, $"You returned {value} but should have returned {c}");
        }

        [Theory]
        [InlineData("Hello my friend", ' ', "friend",3)]
        [InlineData("Mein neues Spiel.", ' ', "Spiel.",3)]
        [InlineData("Kein schoener Tag_als Heute.", ' ', "schoener",2)]
        [InlineData("Mein-neues-Spiel.", '-', "Spiel.",3)]
        [InlineData("thebgamebgoesbonbasblongbas there is time", 'b', "the",1)]
        public void Test8(string a, char b, string c, int d)
        {
            var value = prog.ReturnThirdSubText(a, b, d);
            Assert.True(value == c, $"You returned {value} but should have returned {c}");
        }

        [Theory]
        [InlineData("Hello my friend", ' ', 2)]
        [InlineData("Mein neues Spiel.", ' ', 2)]
        [InlineData("Kein schoener Tag_als Heute.", ' ', 3)]
        [InlineData("Mein-neues-Spiel.", '-', 2)]
        [InlineData("thebgamebgoesbonbasblongbas there is time", 'b', 6)]
        public void Test9(string a, char b, int c)
        {
            var value = prog.CutCounter(a, b);
            Assert.True(value == c, $"You returned {value} but should have returned {c}");
        }
    }
}
