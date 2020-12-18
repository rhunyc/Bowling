using BowlingConsoleApp.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingConsoleApp.Game
{
    #nullable enable
    public class Frame
    {
        public Round Round { get; }
        public int Score { get; set; }
        public Ball FirstBall { get; set; } = new Ball();
        public Ball? SecondBall { get; set; }
        public Ball? ThirdBall { get; set; }
        public int FrameNumber { get; set; }
        public bool DoneCounting { get; set; } = false;

        public Frame(Round round, int frameNumber)
        {
            Round = round;
            FrameNumber = frameNumber;
        }

        // This is what we use to play each frame of the game, based on the frame number and balls thrown, it will behave accordingly
        // Inputs: List<ScoreTracker> scoreTrackers - These are used to keep track of future balls
        public ScoreTracker? PlayFrame()
        {
            // FIRST BALL
            FirstBall.EnterBall(InputHelper.GetPinsHit("first"));                               // Put in how many pins the ball hit
            ScoreHelper.UpdateScores(Round.ScoreTrackers, FirstBall, this, Round);              // Update our scores

            if (FirstBall.IsStrike && FrameNumber != 10)                                        // If we get a strike and are not on frame ten
            {
                return new ScoreTracker(this, 2);                                               // Go to next frame and we must keep track of the next (2) balls.
            }

            // SECOND BALL
            SecondBall = new Ball();
            
            // Check the input for the second ball
            if (FrameNumber == 10 && FirstBall.IsStrike)                                        // If we're on the tenth frame and our first ball was a strike
            {
                SecondBall.EnterBall(InputHelper.GetPinsHit("second"));                         // We can get any amount 0-10
            }
            else 
            {
                SecondBall.EnterBall(InputHelper.GetPinsHit("second", FirstBall), FirstBall);   // Otherwise, we use our first ball to first check if the input is valid
            }                                                                                   // then we check if the ball is a spare or not
            ScoreHelper.UpdateScores(Round.ScoreTrackers, SecondBall, this, Round);             // Update our score

            // Determine how we move on and if we need to track anything
            if (FrameNumber != 10 && SecondBall.IsSpare)                                        // If not on frame 10 and it's a spare
            {
                return new ScoreTracker(this, 1);                                               // Let's keep track of this frame's score for 1 more ball
            } 
            else if (FrameNumber != 10 && !SecondBall.IsSpare)                                  // If we're not on frame ten and it's not a spare
            {
                return null;                                                                    // Go to next frame, not needing to keep track of anything.
            }
            else if (FirstBall.IsStrike || SecondBall.IsSpare)            
            {
                // THIRD BALL (10th Frame)
                ThirdBall = new Ball();
                Console.WriteLine("How many pins on third ball?: ");
                if (SecondBall.IsStrike || SecondBall.IsSpare)                                  // If the second ball is a strike or spare
                {
                    ThirdBall.EnterBall(InputHelper.GetPinsHit("third"));                       // We can just get the number, making sure it's 1-10 
                } else
                {
                    ThirdBall.EnterBall(InputHelper.GetPinsHit("third", SecondBall), SecondBall);  // Otherwise, we must check to make sure we don't go over 10 with the previous
                }
                ScoreHelper.UpdateScores(Round.ScoreTrackers, ThirdBall, this, Round);          // Update our scores
            }

            return null;
        }
    }
}
