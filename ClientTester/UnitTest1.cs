using Client;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.ComponentModel.Design;

namespace DataTest
{
    [TestClass]
    public class UnitTest1
    {
        byte command;
        Int16 input1, input2, input3, input4, input5;
        string text2 = "";
        byte[] expectedArrCorrect, expectedArrIncorrect, resultArr;
        [TestMethod]
        public void TestClearDisplayCorrect()
        {
            command = 1;
            byte[] RGB = new byte[] { 255, 0, 0 };
            expectedArrCorrect = new byte[] { 1, 255, 0, 0 };
            resultArr = ClientProgram.ClearDisplay(command, RGB);

            CollectionAssert.AreEqual(expectedArrCorrect, resultArr);
        }
        [TestMethod]
        public void TestClearDisplayIncorrect()
        {
            command = 1;
            byte[] RGB = new byte[] { 255, 0, 0 };
            expectedArrIncorrect = new byte[] { command, 0, 250, 0, 1 };
            resultArr = ClientProgram.ClearDisplay(command, RGB);
            CollectionAssert.AreNotEqual(expectedArrIncorrect, resultArr);
        }

        [TestMethod]
        public void Test3VarConverterCorrect()
        {
            command = 2;
            input1 = 5;
            input2 = 10;
            byte[] RGB = new byte[] { 255, 255, 255 };
            expectedArrCorrect = new byte[] { command, 5, 0, 10, 0, 255, 255, 255 };

            resultArr = ClientProgram.ThreeVarsConverter(command, input1, input2, RGB);
            CollectionAssert.AreEqual(expectedArrCorrect, resultArr);
        }

        [TestMethod]
        public void Test3VarConverterIncorrect()
        {
            command = 2;
            input1 = 5;
            input2 = 10;
            byte[] RGB = new byte[] { 255, 255, 255 };
            expectedArrIncorrect = new byte[] { command, 5, 0, 10, 0, 255, 255, 105 };

            resultArr = ClientProgram.ThreeVarsConverter(command, input1, input2, RGB);
            CollectionAssert.AreNotEqual(expectedArrIncorrect, resultArr);
        }

        [TestMethod]
        public void Test5VarConverterCorrect()
        {
            command = 2;
            input1 = 5;
            input2 = 10;
            input3 = 25;
            input4 = 13;
            byte[] RGB = new byte[] { 255, 0, 0 };
            expectedArrCorrect = new byte[] { command, 5, 0, 10, 0, 25, 0, 13, 0, 255, 0, 0 };

            resultArr = ClientProgram.FiveVarsConverter(command, input1, input2, input3, input4, RGB);
            CollectionAssert.AreEqual(expectedArrCorrect, resultArr);
        }

        [TestMethod]
        public void Test5VarConverterIncorrect()
        {
            command = 2;
            input1 = 5;
            input2 = 10;
            input3 = 25;
            input4 = 13;
            byte[] RGB = new byte[] { 255, 0, 0 };
            expectedArrIncorrect = new byte[] { command, 5, 0, 10, 0, 25, 15, 13, 0, 255, 0, 1 };

            resultArr = ClientProgram.FiveVarsConverter(command, input1, input2, input3, input4, RGB);
            CollectionAssert.AreNotEqual(expectedArrIncorrect, resultArr);
        }

        [TestMethod]
        public void TestCircleConverterCorrect()
        {
            command = 2;
            input1 = 5;
            input2 = 10;
            input3 = 25;
            byte[] RGB = new byte[] { 0, 0, 0 };
            expectedArrIncorrect = new byte[] { command, 5, 0, 10, 0, 25, 0, 0, 0, 0 };

            resultArr = ClientProgram.CircleConverter(command, input1, input2, input3, RGB);
            CollectionAssert.AreEqual(expectedArrIncorrect, resultArr);
        }


        [TestMethod]
        public void TestCircleConverterIncorrect()
        {
            command = 2;
            input1 = 5;
            input2 = 10;
            input3 = 25;
            byte[] RGB = new byte[] { 0, 0, 0 };
            expectedArrIncorrect = new byte[] { command, 5, 0, 10, 0, 25, 15, 0, 0, 1 };

            resultArr = ClientProgram.CircleConverter(command, input1, input2, input3, RGB);
            CollectionAssert.AreNotEqual(expectedArrIncorrect, resultArr);
        }

        [TestMethod]
        public void TestTextConverterCorrect()
        {
            command = 2;
            input1 = 5;
            input2 = 10;
            byte[] RGB = new byte[] { 0, 0, 0 };
            input3 = 25;
            text2 = "Hello";
            expectedArrCorrect = new byte[] { 2, 5, 0, 10, 0, 0, 0, 0, 25, 0, 5, 0, 72, 101, 108, 108, 111 };

            resultArr = ClientProgram.TextConverter(command, input1, input2, input3, text2, RGB);
            CollectionAssert.AreEqual(expectedArrCorrect, resultArr);
        }

        [TestMethod]
        public void TestTextConverterIncorrect()
        {
            command = 2;
            input1 = 5;
            input2 = 10;
            byte[] RGB = new byte[] { 0, 0, 0 };
            input3 = 25;
            text2 = "Hello";
            expectedArrIncorrect = new byte[] { 3, 5, 0, 10, 0, 0, 0, 1, 25, 5, 111, 108, 108, 101, 72 };

            resultArr = ClientProgram.TextConverter(command, input1, input2, input3, text2, RGB);
            CollectionAssert.AreNotEqual(expectedArrIncorrect, resultArr);
        }

        [TestMethod]
        public void TestRoundedRectangleConverterCorrect()
        {
            command = 2;
            input1 = 5;
            input2 = 10;
            input3 = 25;
            input4 = 133;
            input5 = 15;
            byte[] RGB = new byte[] { 0, 0, 0 };
            expectedArrCorrect = new byte[] { 2, 5, 0, 10, 0, 25, 0, 133, 0, 15, 0, 0, 0, 0 };

            resultArr = ClientProgram.RoundedRectangleConverter(command, input1, input2, input3, input4, input5, RGB);
            CollectionAssert.AreEqual(expectedArrCorrect, resultArr);
        }


        [TestMethod]
        public void TestRoundedRectangleConverterIncorrect()
        {
            command = 2;
            input1 = 5;
            input2 = 10;
            input3 = 25;
            input4 = 133;
            input5 = 15;
            byte[] RGB = new byte[] { 0, 0, 0 };
            expectedArrIncorrect = new byte[] { 3, 5, 0, 10, 0, 25, 0, 133, 0, 15, 0, 0, 0, 1 };

            resultArr = ClientProgram.RoundedRectangleConverter(command, input1, input2, input3, input4, input5, RGB);
            CollectionAssert.AreNotEqual(expectedArrIncorrect, resultArr);
        }





    }
}