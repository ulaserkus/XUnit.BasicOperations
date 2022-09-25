using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyUnitTest.APP;
using Xunit;

namespace UdemyUnitTest.TEST
{
    public class CalculatorTest
    {
        public Calculator calculator { get; set; }
        public Mock<ICalculatorService> myMock { get; set; }

        public CalculatorTest()
        {
            myMock = new Mock<ICalculatorService>();
            calculator = new Calculator(myMock.Object);
        }

        [Fact]//test method attribute with no parameter
        public void AddTest()
        {
            ////Arrange(initial state)
            //int a = 5;
            //int b = 20;
            //var calculator = new Calculator();

            ////Act(method run state)
            //var total = calculator.add(a, b);

            ////Assert(accurancy)
            //Assert.Equal<int>(25, total);
            var names = new string[] { "ulaş" };

            Assert.Contains(names, x => x.Contains("ulaş"));
            Assert.Contains("Ulaş", "Ulaş Erkuş");
            Assert.DoesNotContain("hasan", "Ulaş Erkuş");
            Assert.True(1 == 1);//expected value true
            Assert.False(0 == 2);//expected value is false
            Assert.Matches("^dog", "dog");//expected value regex
            Assert.DoesNotMatch("^dog", "s");//expected value regex
            Assert.StartsWith("test", "test string");//first string control
            Assert.EndsWith("string", "test string");//last string control

            int[] emptyArray = new int[] { };
            int[] notEmptyArray = new int[] { 1, 2, 3, 4 };
            Assert.Empty(emptyArray); //is empty return true
            Assert.NotEmpty(notEmptyArray);//is empty return false

            Assert.InRange(10, 2, 20); // return true
            Assert.NotInRange(1, 2, 20);//return true

            Assert.Single(new List<string>() { "Ulaş" });//array has one element return true

            Assert.IsType<string>("string type");//return true if type is correct
            Assert.IsNotType<string>(1);//return true if type is not correct

            Assert.IsAssignableFrom<IEnumerable<string>>(new List<string>());//assign reference return true

            Assert.NotNull(notEmptyArray);
            Assert.Null(null);

            Assert.Equal<int>(2, 2);
            Assert.NotEqual<int>(2, 3);

        }

        [Theory]
        [InlineData(1, 2, 3)]
        [InlineData(10, 2, 12)]
        public void AddTest2(int a, int b, int expectedTotal)
        {

            myMock.Setup(x => x.add(a, b)).Returns(expectedTotal);

            var actualData = calculator.add(a, b);

            Assert.Equal(expectedTotal, actualData);

            myMock.Verify(x => x.add(a, b), Times.Once);

        }


        //[MethodName_StateUnderTest_ExpectedBehavior]
        [Theory]
        [InlineData(1, 1, 1)]
        [InlineData(2, 2, 4)]
        public void Multiply_SimpleValues_ReturnsMultipleValues(int a, int b, int expectedValue)
        {
            int actualMultip=0;
            myMock.Setup(x => x.multiply(It.IsAny<int>(), It.IsAny<int>())).Callback<int, int>((x, y) => actualMultip = x * y);

            calculator.multiply(a, b);

            Assert.Equal(expectedValue, actualMultip);

            myMock.Verify(x => x.multiply(a, b), Times.Once);

        }


        [Theory]
        [InlineData(0, 1)]
        public void Multiply_ZeroValue_ReturnsException(int a, int b)
        {
            myMock.Setup(x => x.multiply(a, b)).Throws(new Exception("a=0 olamaz"));

            Exception ex = Assert.Throws<Exception>(() => calculator.multiply(a, b));

            Assert.Equal("a=0 olamaz", ex.Message);
        }
    }
}
