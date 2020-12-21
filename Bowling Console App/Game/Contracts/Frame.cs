using Bowling_Console_App.Game.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bowling_Console_App.Game.Contracts
{
#nullable enable
    public abstract class Frame
    {
        // Properties
        public int FrameNumber { get; set; }
        public int FrameScore { get; set; }
        public int FrameTotal { get; set; }
        public bool StillCounting { get; set; } = false;
        public List<int> Shots { get; set; } = new List<int>();
        
        //Abstract Properties
        public abstract int PossibleRolls { get; set; }

        //Abstract Functions
        public abstract FrameScoreUpdater? Roll();
        public abstract bool ValidatePoints(int input);
        public abstract int GetPreviousShot(int indexOfCurrentShot);
        public abstract bool CanBePlayed();

        // Defined Functions
        // Gets total score for the frame (running total including previous scores of frames)
        public int UpdateFrameTotal(int amount)
        {
            FrameTotal = FrameScore + amount;
            return FrameTotal;
        }

        // Get the number of balls rolled
        public int GetBallsRolled() {
            return Shots.Count();
        }

        // Adds the number of points to the frames score, the amount should be validated, but errors just in case
        public void AddPointsToFrameScore(int amount)
        {
            if (amount < 0)
                throw new Exception("The amount entered cannot go below 0");
            else if (amount > 10)
                throw new Exception("The amount entered cannot be greater than 10");

            FrameScore += amount;

            if (FrameScore > 30)
                throw new Exception("The score for a single frame cannot exceed 30");
            else if (FrameScore < 0)
                throw new Exception("The score for a single frame cannot go below 0");
        }

        // Lets the round know whether or not this frame is able to be counted for the running total
        public bool CanUpdateFrameTotal()
        {
            if (this is NormalFrame && CanBePlayed())
                return false;
            else if (StillCounting)
                return false;
            else if (this is LastFrame && CanBePlayed())
                return false;

            return true;
        }
    }
}
