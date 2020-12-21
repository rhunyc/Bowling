using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Bowling_Console_App.Game.Helpers
{

    // I suppressed the warning for the hash not being overriden - just keeping things simple
#pragma warning disable CS0659 
    public class FrameScoreUpdater
   {
        public NormalFrame Frame { get; set; }
        public int RollCounter { get; set; }
        public bool CanDelete { get; set; } = false;

        // Updates the frame's individual score if it was a strike or spare
        public void UpdateFrameScore(int score)
        {
            Frame.AddPointsToFrameScore(score);     // Adds the points from the current shot to the tracked frame's score

            RollCounter--;                          // Decrements how many rolls it wants to keep track of

            if (RollCounter <= 0)                   // If the rollcounter is at or below 0 we then say the frame is not counting anymore and we can delete this tracker
            {
                Frame.StillCounting = false;
                CanDelete = true;
            }
        }

        public override bool Equals(object obj)
        {
            if (!(obj is FrameScoreUpdater))
                return false;

            var other = obj as FrameScoreUpdater;

            return (other.RollCounter == RollCounter); // Just want to make sure the roll counters match up, if I had more time I would do a Normal Frame comparrison
        }
    }
}
