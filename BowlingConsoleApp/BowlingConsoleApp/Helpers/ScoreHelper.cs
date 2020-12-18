using BowlingConsoleApp.Game;
using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingConsoleApp.Helpers
{
    public static class ScoreHelper
    {
        public static void UpdateScores(List<ScoreTracker> scoreTrackers, Ball ball, Frame frame, Round round)
        {
            frame.Score += ball.PinsHit;            // Update the frame score with the amount of pins hit
            
            foreach (var tracker in scoreTrackers)  // For each of our score trackers that are out there
            {
                tracker.UpdateFrameScore(ball);     // Update their attached frame's score
            }

            scoreTrackers.RemoveAll(t => t.Counter <= 0); // Clear out any of the trackers that have finished
        }
    }
}
