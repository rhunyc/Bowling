using Bowling_Console_App.Game.Contracts;
using Bowling_Console_App.Game.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bowling_Console_App.Game
{
    public class Round
    {
        public List<Frame> Frames = new List<Frame>();
        public List<FrameScoreUpdater> FrameScoreUpdaters = new List<FrameScoreUpdater>();
        public int TotalScore = 0;

        // Initialize our round object, creating ten frames starting at 1, if it's frame 10, we make it a LastFrame object
        public Round()
        {
            for (int i = 1; i < 11; i++)
            {
                if (i < 10)
                    Frames.Add(new NormalFrame() { FrameNumber = i });
                else
                    Frames.Add(new LastFrame() { FrameNumber = i });
            }
        }

        // Our logic to play our round of bowling
        public void PlayRound()
        { 
            foreach (var frame in Frames)
            {
                UpdateAllFramesTotalScore();                            // At the start of every frame, we want to update our frame running totals
                OutputHelper.WipeAndPrintNewScoreBoard(Frames);         // Wipe and print a new score board
                while (frame.CanBePlayed())                             // Then, while a frame can be played
                {
                    FrameScoreUpdater updater = frame.Roll();           // Roll, and if it is a strike or spare, we will track it with a frame score updater
                    UpdateTrackedFrameScores(frame);                    // With our newest shot, we want to update any tracked frames with their next shot(s)

                    if (!(updater == null))                             // After we track for any existing shots, we want to add our new updater to be tracked for future shots
                        FrameScoreUpdaters.Add(updater);
                }
            }

            UpdateAllFramesTotalScore();                                // At the end of our round, update the frame totals one last time (no need to update frame scores, since the last frame doesn't track that)
            OutputHelper.WipeAndPrintNewScoreBoard(Frames);             // Reprint the final score board
            OutputHelper.PrintFinalScore(TotalScore);                   // Let the user know their final score (it shows on the board now too, but might as well)
        }

        // This function will update every frame total so we can know what the users total score per frame is
        public void UpdateAllFramesTotalScore()
        { 
            int runningTotal = 0;                                       // Setup our new running total
            foreach (var frame in Frames)                               // For each frame
            {
                if (!frame.CanUpdateFrameTotal())                       // If it's currently counting, we know we don't need to do anymore so we break
                    break;

                runningTotal = frame.UpdateFrameTotal(runningTotal);    // Otherwise, we will keep track of our running total
                TotalScore = runningTotal;                              // Update our total score to this new running total
            }

            if (TotalScore > 300)
            {
                throw new Exception("The score exceeded 300 somehow.");
            } else if (TotalScore < 0)
            {
                throw new Exception("The score went negative somehow.");
            }
        }

        // This is used to update our individual frames' scores
        public void UpdateTrackedFrameScores(Frame frame)
        {
            if (frame.FrameNumber == 1)                                   // We'll never do this on the first frame
                return;

            foreach(var frameScoreUpdater in FrameScoreUpdaters)                // For each framescoreupdater
            {
                frameScoreUpdater.UpdateFrameScore(frame.Shots.LastOrDefault()); // Add the last shot's points to the tracked frame's score
            }

            FrameScoreUpdaters.RemoveAll(f => f.CanDelete);                     // Delete any score trackers that are finished
        }
    }
}
