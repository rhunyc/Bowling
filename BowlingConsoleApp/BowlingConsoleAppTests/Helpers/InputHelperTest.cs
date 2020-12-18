using System;
using System.Collections.Generic;
using BowlingConsoleApp.Helpers;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using BowlingConsoleApp.Game;

namespace BowlingConsoleAppTests.Helpers
{
    public class InputHelperTest
    {
        public static IEnumerable<object[]> CheckIfInputIsIntegerData()
        {
            yield return new object[]
            {
                null,
                -1
            };
            yield return new object[]
            {
                "",
                -1
            };
            yield return new object[]
            {
                "f",
                -1
            };
            yield return new object[]
            {
                "2",
                2
            };
            yield return new object[]
            {
                "0",
                0
            };
        }

        [Theory]
        [MemberData(nameof(CheckIfInputIsIntegerData))]
        public void CheckIfInputIsInteger_Tests(string input, int expected)
        {
            // Arrange

            // Act
            var actual = InputHelper.CheckIfInputIsInteger(input);

            // Assert
            Assert.Equal(expected, actual);
        }

        public static IEnumerable<object[]> VerifyPinTotalAmountData()
        {
            yield return new object[]
            {
                0,
                null,
                true
            };
            yield return new object[]
            {
                -1,
                null,
                false
            };
            yield return new object[]
            {
                11,
                null,
                false
            };
            yield return new object[]
            {
                10,
                null,
                true
            };
            yield return new object[]
            {
                null,
                null,
                false
            };
            yield return new object[]
            {
                0,
                new Ball() { PinsHit = 9},
                true
            };
            yield return new object[]
            {
                -1,
                new Ball() { PinsHit = 10},
                false
            };
            yield return new object[]
            {
                1,
                new Ball() { PinsHit = 10},
                false
            };
            yield return new object[]
            {
                9,
                new Ball() { PinsHit = 1},
                true
            };
            yield return new object[]
            {
                null,
                new Ball() { PinsHit = 9},
                false
            };
        }

        [Theory]
        [MemberData(nameof(VerifyPinTotalAmountData))]
        public void VerifyPinTotalAmount_Tests(int? input, Ball previousBall, bool expected)
        {
            // Arrange

            // Act
            var actual = InputHelper.VerifyPinTotalAmount(input, previousBall);

            // Assert
            Assert.Equal(expected, actual);
        }


    }
}
