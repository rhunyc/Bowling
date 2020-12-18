using BowlingConsoleApp.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BowlingConsoleApp.Helpers
{
    public class ScoreTracker
    {
        public Frame TrackedFrame { get; set; }                 // The frame this score tracker will add to
        public int Counter { get; set; }                        // How many balls we want to count for

        public ScoreTracker(Frame trackedFrame, int counter)
        {
            TrackedFrame = trackedFrame;                        
            Counter = counter;
        }

        public void UpdateFrameScore(Ball ball)                 // Based on the balls pin hits, we'll update the frame that wants 
        {                                                       // to know this information, then decrement how many future balls we care for.
            int scoreToAdd = ball.PinsHit;
            TrackedFrame.Score += scoreToAdd;
            Counter--;
        }
    }
}
