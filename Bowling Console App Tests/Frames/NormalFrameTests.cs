using Bowling_Console_App.Game;
using Bowling_Console_App.Game.Contracts;
using Bowling_Console_App.Game.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Bowling_Console_App_Tests.Contracts
{
    public class NormalFrameTests
    {
        public static IEnumerable<object[]> UpdateFrameTotalData()
        {
            NormalFrame frame = new NormalFrame() { FrameScore = 10 };

            yield return new object[]
            {
                100,
                110,
                frame
            };

            yield return new object[]
            {
                5,
                15,
                frame
            };

            yield return new object[]
            {
                -5,
                5,
                frame
            };
        }

        [Theory]
        [MemberData(nameof(UpdateFrameTotalData))]
        public void UpdateFrameTotalTests(int amount, int expected, NormalFrame frame)
        {
            //Arrange


            //Act
            var actual = frame.UpdateFrameTotal(amount);

            //Assert
            Assert.Equal(expected, actual);
        }

        public static IEnumerable<object[]> GetBallsRolledData()
        {
            yield return new object[]
            {
                new NormalFrame() { Shots = new List<int> { 0 } },
                1
            };

            yield return new object[]
            {
                new NormalFrame() { Shots = new List<int> { 0, 2 } },
                2
            };
            yield return new object[]
            {
                new NormalFrame() { Shots = new List<int> {  } },
                0
            };
        }

        [Theory]
        [MemberData(nameof(GetBallsRolledData))]
        public void GetBallsRolledTests(Frame frame, int expected)
        {
            // Arrange

            // Act
            var actual = frame.GetBallsRolled();

            // Assert
            Assert.Equal(expected, actual);
        }

        public static IEnumerable<object[]> AddPointsToFrameScoreData()
        {
            NormalFrame frame = new NormalFrame() { FrameScore = 5 };

            yield return new object[]
            {
                5,
                10,
                frame
            };

            yield return new object[]
            {
                0,
                10,
                frame
            };
        }

        [Theory]
        [MemberData(nameof(AddPointsToFrameScoreData))]
        public void AddPointsToFrameScore(int amount, int expected, NormalFrame frame)
        {
            // Arrange

            // Act
            frame.AddPointsToFrameScore(amount);
            int actual = frame.FrameScore;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AddNegativePointsToFrameScoreError()
        {
            // Arrange
            NormalFrame frame = new NormalFrame() { FrameScore = 0};

            // Act
            var ex = Assert.Throws<Exception>(() => frame.AddPointsToFrameScore(-1));
            // Assert

            Assert.Equal("The amount entered cannot go below 0", ex.Message);
        }

        [Fact]
        public void AddTooManyPointsToFrameScoreError()
        {
            // Arrange
            NormalFrame frame = new NormalFrame() { FrameScore = 0 };

            // Act
            var ex = Assert.Throws<Exception>(() => frame.AddPointsToFrameScore(11));
            // Assert

            Assert.Equal("The amount entered cannot be greater than 10", ex.Message);
        }

        [Fact]
        public void AddPointsToFrameScoreExceed30Error()
        {
            // Arrange
            NormalFrame frame = new NormalFrame() { FrameScore = 30 };

            // Act
            var ex = Assert.Throws<Exception>(() => frame.AddPointsToFrameScore(1));
            // Assert

            Assert.Equal("The score for a single frame cannot exceed 30", ex.Message);
        }

        [Fact]
        public void AddPointsToFrameScoreBelow0Error()
        {
            // Arrange
            NormalFrame frame = new NormalFrame() { FrameScore = -1 };

            // Act
            var ex = Assert.Throws<Exception>(() => frame.AddPointsToFrameScore(0));
            // Assert

            Assert.Equal("The score for a single frame cannot go below 0", ex.Message);
        }

        public static IEnumerable<object[]> CanUpdateFrameTotalData()
        {
            yield return new object[] {
                new NormalFrame() { StillCounting = false, FrameScore = 10, Shots = new List<int> { 10 } },
                false
            };
            yield return new object[] {
                new NormalFrame() { StillCounting = false, FrameScore = 10, Shots = new List<int> { 0, 10 } },
                false
            };
            yield return new object[] {
                new NormalFrame() { StillCounting = false, FrameScore = 3, Shots = new List<int> { 0, 3 } },
                false
            };
            yield return new object[] {
                new NormalFrame() { StillCounting = false, FrameScore = 0, Shots = new List<int> { 0 } },
                true
            };
            yield return new object[] {
                new NormalFrame() { StillCounting = false, FrameScore = 0, Shots = new List<int> { 0} },
                true
            };
        }

        [Theory]
        [MemberData(nameof(CanUpdateFrameTotalData))]
        public void CanUpdateFrameTotalTests(NormalFrame frame, bool expected)
        {
            // Arrange

            // Act
            var actual = frame.CanBePlayed();

            // Assert
            Assert.Equal(expected, actual);
        }

        public static IEnumerable<object[]> CanBePlayedData()
        {
            yield return new object[] {
                new NormalFrame() { StillCounting = false, FrameScore = 10, Shots = new List<int> { 10 } },
                false
            };
            yield return new object[] {
                new NormalFrame() { StillCounting = false, FrameScore = 10, Shots = new List<int> { 0, 10 } },
                false
            };
            yield return new object[] {
                new NormalFrame() { StillCounting = false, FrameScore = 3, Shots = new List<int> { 0, 3 } },
                false
            };
            yield return new object[] {
                new NormalFrame() { StillCounting = false, FrameScore = 0, Shots = new List<int> { 0 } },
                true
            };
            yield return new object[] {
                new NormalFrame() { StillCounting = false, FrameScore = 0, Shots = new List<int> { 0} },
                true
            };
        }

        [Theory]
        [MemberData(nameof(CanBePlayedData))]
        public void CanBePlayedTests(NormalFrame frame, bool expected)
        {
            // Arrange

            // Act
            var actual = frame.CanBePlayed();

            // Assert
            Assert.Equal(expected, actual);
        }

        public static IEnumerable<object[]> ValidatePointsData()
        {
            yield return new object[]
            {
                new NormalFrame() { Shots = new List<int> {  }},
                -10,
                false
            };
            yield return new object[]
            {
                new NormalFrame() { Shots = new List<int> {  }},
                10,
                true
            };
            yield return new object[]
            {
                new NormalFrame() { Shots = new List<int> {  }},
                11,
                false
            };
            yield return new object[]
            {
                new NormalFrame() { Shots = new List<int> { 0 }},
                5,
                true
            };
            yield return new object[]
            {
                new NormalFrame() { Shots = new List<int> { 0 }},
                10,
                true
            };
            yield return new object[]
            {
                new NormalFrame() { Shots = new List<int> { 4 }},
                10,
                false
            };
            yield return new object[]
            {
                new NormalFrame() { Shots = new List<int> { 9 }},
                1,
                true
            };
            yield return new object[]
            {
                new NormalFrame() { Shots = new List<int> { 9 }},
                -1,
                false
            };
            yield return new object[]
            {
                new NormalFrame() { Shots = new List<int> { 1, 1, 1 }},
                5,
                false
            };
        }

        [Theory]
        [MemberData(nameof(ValidatePointsData))]
        public void ValidatePointsTests(NormalFrame frame, int input, bool expected)
        {
            // Arrange

            // Act
            var actual = frame.ValidatePoints(input);

            // Assert
            Assert.Equal(expected, actual);
        }

        public static IEnumerable<object[]> CreateFrameScoreUpdaterAndTrackFrameData()
        {
            yield return new object[]
            {
                new NormalFrame(){ FrameScore = 10, Shots = new List<int> { 0 } },
                new FrameScoreUpdater() { RollCounter = 2}
            };
            yield return new object[]
            {
                new NormalFrame(){ FrameScore = 10, Shots = new List<int> { 0, 0 } },
                new FrameScoreUpdater() { RollCounter = 1}
            };
            yield return new object[]
            {
                new NormalFrame(){ FrameScore = 1, Shots = new List<int> { 0 } },
                null
            };
            yield return new object[]
            {
                new NormalFrame(){ FrameScore = 1, Shots = new List<int> {  } },
                null
            };
        }

        #nullable enable
        [Theory]
        [MemberData(nameof(CreateFrameScoreUpdaterAndTrackFrameData))]
        public void UpdateFrameTypeTests(NormalFrame frame, FrameScoreUpdater? expected) 
        {
            // Arrange

            // Act
            var actual = frame.CreateFrameScoreUpdaterAndTrackFrame();

            //Assert
            Assert.Equal(expected, actual);
        }

        public static IEnumerable<object[]> GetPreviousShotData()
        {
            NormalFrame frame = new NormalFrame() { Shots = new List<int> { 3, 2 } };

            yield return new object[]
            {
                frame,
                0,
                0
            };
            yield return new object[]
            {
                frame,
                1,
                3
            };
            yield return new object[]
            {
                frame,
                2,
                0
            };
        }

        [Theory]
        [MemberData(nameof(GetPreviousShotData))]
        public void GetPreviousShotTests(NormalFrame frame, int index, int expected)
        {
            //Arrange

            //Act
            int actual = frame.GetPreviousShot(index);

            //Assert
            Assert.Equal(expected, actual);
        }
    }
}
