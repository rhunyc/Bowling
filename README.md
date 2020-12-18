# Bowling - OnSharp Challenge

Hello, here's my submission for the coding challenge with OnSharp.

I'm going to be the first to admit that there has to be a better way to do this than what I provided.

I bring this up, because half-way through coding it I realized I had kind of coded myself into a corner with some dependencies. I was trying really hard to get the total score to update as frames were calculated, but once I realized how difficult I had made it, I wouldn't have had enough time to start from scratch.

So I decided to just stick with how I had structured it and tried to make the best of it. I was able to get it to calculate the scores accurately and it will print the score for each frame, unfortunately it isn't a running total due to the previously mentioned issue.

Another thing I noticed was that when writing unit tests, the functions for most classes are not as simple and would require either breaking them apart or really indepth testing. So this is why there are unit tests lacking for most classes, unfortunately.

If I had more time this is what I would do:
-Rework the scoring logic completely, figure out a more efficient way of keeping track of both the frame and total scores
-Make sure that what I do can be easily tested, and incorporate more unit tests as a result

I appreciate the opportunity to work on this coding challenge and I would love to hear your honest feedback. It's been a really hectic week, but I tried my best!

Thank you.
