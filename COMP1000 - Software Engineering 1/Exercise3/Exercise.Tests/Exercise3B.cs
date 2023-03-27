using System;
using Xunit;

namespace Exercise.Tests
{
    public class Exercise3B
    {
        private Exercise.Program2 prog;
        public Exercise3B()
        {
            prog = new Program2();
        }

        [Theory]
        [InlineData("1 + 2", " ", 3)]
        [InlineData("1 * 2", " ", 2)]
        [InlineData("1 * 2 * 3 * 4", " ", 24)]
        [InlineData("1 + 2 + 10 * -2 + 5 + 2 + -1 + 15", " ", 4)]
        [InlineData("2x*x2", "x", 4)]
        [InlineData("1 * 2 + 10 * -2 + 5 + 2 + -1 / 15", " ", -11)]
        [InlineData("1 * 21 + 10 * -2 + 5 + 2 + -12 / 15", " ", 8)]
        [InlineData("1 + 2 + 10 * -2 + 5 / 2 + -2 + 15", " ", -2)]
        [InlineData("1 + 2 + 10 * -2 + 5 / 2 + -1 + 15 * -2 + 5 / 2 + -1 + 15 * -2 + 5 / 2 + -1 + 15", " ", -59)]
        [InlineData("1 + 2 + 10 * -2 + 5 / 2 + -1 + 15 * -2 + 5 / 2 + -1 + 15 * -2 + 5 / 2 * 0 + -1 + 15", " ", -61)]
        public void Test8(string a, string b, int c)
        {
            var value = prog.PerformNonNaiveCalculation(a, b);
            Assert.True(value == c, $"For Program1.PerformAdvancedCalculation:\n You returned {value} but should have returned {c}");
        }
    }
}
