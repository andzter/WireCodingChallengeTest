using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WireCodingChallenge.Lib;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void WriteOutputTest()
        {

            var words = "test";
            var article = "test my test.";

            var outStr = ProcessFile.GenerateOutPut(article, words);

            
            Assert.IsTrue(outStr.Equals(" a. test {2:1,1}\r\n"));
        }
    }
}
