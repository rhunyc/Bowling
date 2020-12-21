using Bowling_Console_App.Game.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Bowling_Console_App_Tests.Helpers
{
    public class InputHelperTests
    {
        public static IEnumerable<object[]> InputBetween0and10Data()
        {
            yield return new object[]
            {
                0,
                true
            };
            yield return new object[]
            {
                10,
                true
            };
            yield return new object[]
            {
                -1,
                false
            };
            yield return new object[]
            {
                11,
                false
            };
        }

        [Theory]
        [MemberData(nameof(InputBetween0and10Data))]
        public void InputBetween0and10Tests(int input, bool expected)
        {
            // Arrange

            // Act
            var actual = InputHelper.InputBetween0and10(input);

            // Assert
            Assert.Equal(expected, actual);
        }

        public static IEnumerable<object[]> InputAndPreviousShotValidData()
        {
            yield return new object[]
            {
                1,
                9,
                true
            };
            yield return new object[]
            {
                2,
                9,
                false
            };
            yield return new object[]
            {
                -1,
                9,
                false
            };
            yield return new object[]
            {
                0,
                1,
                true
            };
        }

        [Theory]
        [MemberData(nameof(InputAndPreviousShotValidData))]
        public void InputAndPreviousShotValidTests(int input, int previousShot, bool expected)
        {
            // Arrange

            // Act
            var actual = InputHelper.InputAndPreviousShotValid(input, previousShot);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
