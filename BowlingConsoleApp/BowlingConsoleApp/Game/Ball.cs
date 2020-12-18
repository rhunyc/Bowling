using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingConsoleApp.Game
{
    // Our Ball class is used to track scores - The InputHelper class takes care of any illegal inputs, so there is no need to check here.
    public class Ball
    {
        public int PinsHit { get;  set; }                               // The amount of pins this ball has hit
        public bool IsStrike { get; set; } = false;
        public bool IsSpare { get; set; } = false;

        // Used to set how many pins the ball hit, as well as track whether or not it was a spare / strike
        public void EnterBall(int input, Ball previousBall = null) {
            if (previousBall is null)                                   // If no previous ball, we are only concerned with strikes
            {
                if (input == 10)                                        // If the ball is 10, it's a strike
                {
                    IsStrike = true;
                }
            } 
            else
            {
                if (input + previousBall.PinsHit == 10)                 // If the previous ball and the current input's total is 10, we have a spare
                {
                    IsSpare = true;
                }
            }
            PinsHit = input;                                            // Set our pins hit
        }
    }
}
