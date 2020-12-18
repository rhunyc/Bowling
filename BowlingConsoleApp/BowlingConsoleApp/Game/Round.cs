using System;
using System.Collections.Generic;
using BowlingConsoleApp.Helpers;

namespace BowlingConsoleApp.Game
{

    // A round is a round of bowling, so 1 round consists of 10 frames of bowling.
    public class Round
    {
        public List<Frame> Frames { get; } = new List<Frame>();
        public List<ScoreTracker> ScoreTrackers = new List<ScoreTracker>(); // ScoreTrackers is a collection of ScoreTracker objects that help manage strike / spare scoring rules

        // Build our frames, we need 10 of them, starting at 1.
        public Round()
        {
            for (int i = 1; i < 11; i++)
            {
                Frames.Add(new Frame(this, i));
            }
        }

        // This will start the round.
        public void PlayRound() {
            
            foreach (var frame in Frames) // What we do for each frame
            {
                Console.Clear();                                    // Wipe the board clean
                OutputHelper.PrintScoreBoard(Frames, frame);        // Create an updated scoreboard for the round
                var tracker = frame.PlayFrame();                    // Play the frame, will return a tracker if it was successful
                if (!(tracker is null))                             // If the frame had a spare or strike, we want to keep track of it
                {
                    ScoreTrackers.Add(tracker);
                }   
            }

            Console.Clear();                                        // At the end of our round, we want to show the final scoreboard, and print the final score
            OutputHelper.PrintScoreBoard(Frames);
            OutputHelper.PrintFinalScore(Frames);
            Console.ReadKey();
        }
    }
}
