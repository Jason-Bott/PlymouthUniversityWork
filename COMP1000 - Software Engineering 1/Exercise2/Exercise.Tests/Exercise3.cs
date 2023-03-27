using System;
using Xunit;

namespace Exercise.Tests
{
    public class Exercise3
    {
        private Exercise.Program2 prog;
        public Exercise3()
        {
            prog = new Program2();
        }

        [Theory]
        [InlineData(1, 2, 1.0f, "1", 1.0f)]
        [InlineData(2, 2, 1.0f, "2", 1.0f)]
        [InlineData(20, 3, 2.0f, "1", 2.0f)]
        [InlineData(20, 3, 2.0f, "-1", 2.0f)]
        [InlineData(20, 3, 20000.0f, "1", 3.0f)]
        [InlineData(2, 3, 1.9f, "house", 1.9f)]
        [InlineData(2, 3, 2.1f, "house", 2.0f)]
        public void Test0(int a, int b, float c, string d, float e)
        {
            float value = float.MinValue;
            try
            {
                value = prog.ReturnSmallestNumber(a, b, c, d);
                value = (float)Math.Round(value, 1);
            }
            catch (Exception except)
            {
                Console.WriteLine(except.StackTrace);
                Assert.True(false, "You returned the wrong data format!");
            }
            e = (float)Math.Round(e, 1);
            Assert.True(value == e, $"You returned {value} but should have returned {e}");
        }

        [Theory]
        [InlineData("levelx23", 'x', 23)]
        [InlineData("1.1XdamageX2", 'X', 2)]
        [InlineData("13Ccakes", 'C', 13)]
        [InlineData("5 slimes 4", ' ', 5)]
        [InlineData("Your health is now-5_-4", '_', -4)]
        public void Test1(string a, char b, int c)
        {
            var value = prog.RetrieveNumberFromSubText(a, b);
            Assert.True(value == c, $"You returned {value} but should have returned {c}");
        }

        [Theory]
        [InlineData("hello  my friend", "  ", "my friend  hello")]
        [InlineData("hello My enemies", "y", " enemiesyhello M")]
        [InlineData("two cakes to go please", " ", "please go to cakes two")]
        [InlineData("fivexslimes", "x", "slimesxfive")]
        public void Test2(string a, string b, string c)
        {
            var value = prog.SplitShuffleAndReverseWords(a, b);
            Assert.True(value == c, $"You returned {value} but should have returned {c}");
        }

        [Theory]
        [InlineData("hello my friend", 2, 'l')]
        [InlineData("hello My enemies", 6, 'M')]
        [InlineData("two cakes to go please", 9, ' ')]
        [InlineData("fivexslimes", 0,'f')]
        [InlineData("hello my friend", -2, 'n')]
        [InlineData("fivexslimes", -1, 's')]
        public void Test3(string a, int b, char c)
        {
            var value = prog.GetValueAtPosition(a, b);
            Assert.True(value == c, $"You returned {value} but should have returned {c}");
        }

        [Theory]
        [InlineData("hello my friend", "HELLO MY FRIEND")]
        [InlineData("hello My enemies", "HELLO MY ENEMIES")]
        [InlineData("2 cakes to go please", "2 CAKES TO GO PLEASE")]
        public void Test4(string a, string b)
        {
            var value = prog.ChangeTheTextB(a);
            Assert.True(value == b, $"You returned {value} but should have returned {b}");
        }

        [Theory]
        [InlineData("hello my friend", "HELLO MY FRIEND")]
        [InlineData("HELLO MY ENEMIES", "hello my enemies")]
        [InlineData("2 cakes to GO please", "2 CAKES TO go PLEASE")]
        [InlineData("GOODBYE my FRIEND", "goodbye MY friend")]
        public void Test5(string a, string b)
        {
            var value = prog.ChangeTheTextA(a);
            Assert.True(value == b, $"You returned {value} but should have returned {b}");
        }

        [Theory]
        [InlineData("Hello my friend.", " ","Friend  my  hello.","  ")]
        [InlineData("Let'sxgoxtoxthexpark.", "x","Park_the_to_go_let's.","_")]
        [InlineData("One ale,please.", ",", "Please one ale.", " ")]
        [InlineData("One ale,please, if you do not mind.", ",", " if you do not mind hmm please hmm one ale.", " hmm ")]
        public void Test6(string a, string c, string b, string d)
        {
            var value = prog.ShuffelTheTextAdvanced(a,c,d);
            Assert.True(value == b, $"You returned {value} but should have returned {b}");
        }


    }
}
