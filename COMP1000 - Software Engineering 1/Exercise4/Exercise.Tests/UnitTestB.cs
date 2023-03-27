using System;
using System.Collections.Generic;
using Xunit;

[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace Exercise.Tests
{
    [TestCaseOrderer("XUnit.Project.Orderers.PriorityOrderer", "XUnit.Project")]
    public class UnitTestB
    {
        private Exercise.ProgramB prog;
        public UnitTestB()
        {
            prog = new ProgramB();
        }
        [Theory]
        [InlineData("files/file1.txt", 0)]
        [InlineData("file/file1.txt", -1)]
        [InlineData("files/files1.txt", 1)]
        public void Test1(string values, int result)
        {
            var outcome = prog.AccessFile(values);
            Assert.True(outcome == result, $"in ProgramB.OpenFile: When trying to open a file, you should have returned state: {result} but did return {outcome}.");
        }

        [Theory]
        [InlineData("files/file1.txt", 1, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. In lobortis, ligula at hendrerit facilisis, eros risus dapibus dui, et volutpat dui nibh vel nisi. Morbi gravida sapien ac odio tincidunt tristique. Praesent tristique libero tristique tincidunt varius. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. ")]
        [InlineData("files/file2.map", 3, "Duis vel sagittis elit. Pellentesque et viverra nibh. Proin sed lectus justo. Aliquam volutpat laoreet nisi a placerat. Sed nulla erat, volutpat in dictum ac, pulvinar non enim. Mauris finibus lacus fermentum facilisis bibendum. Mauris dui tortor, vehicula eu libero condimentum, sodales volutpat sem. Nulla facilisi.")]
        [InlineData("files/file2.map", 2, " ")]
        [InlineData("files/fil.map", 2, null)]
        [InlineData("filesa/file2.map", 5, null)]
        [InlineData("files/file2.map", 9, "Sed at maximus ipsum, sed faucibus risus. Aliquam ligula dui, semper in rhoncus vel, ornare a libero. Pellentesque sit amet felis ut libero aliquet tempor. Praesent lacinia metus in luctus vehicula. Donec massa quam, mattis vitae urna vel, blandit rhoncus nulla. Nunc volutpat libero sit amet risus consequat, eget fermentum justo bibendum. Donec at dignissim mauris, nec suscipit leo. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Curabitur et interdum est, porttitor tincidunt turpis. Nullam vulputate mauris in fringilla volutpat. Cras dapibus molestie libero. In eget bibendum augue, rhoncus pharetra nulla. Maecenas lorem dolor, aliquam eget suscipit ac, dignissim at lectus. ")]
        public void Test2(string values, int result, string line)
        {
            var outcome = prog.ReadFromFile(values, result);
            if (line is null)
            {
                Assert.True(outcome is null, $"in ProgramB.OpenFileAndReadLines: The files does not exist but you did not return null, but you return \"{outcome}\"");
            }
            else
            {
                Assert.True(outcome.Length == result, "in ProgramB.OpenFileAndReadLines: Not enough lines were read from the file.");
                Assert.True(outcome[result-1].Equals(line), $"in ProgramB.OpenFileAndReadLines: When trying to open and read a file: From the file, you should have returned {result } lines where line nr.{result - 1} is:\n \"{line}\" \nbut did return: \n\"{outcome[result - 1]}\".");
            }
        }
    }
}
