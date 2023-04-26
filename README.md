# Game Engines Final Asessment

My first step in designing this project was to get the behaviors working. My goal was to simulate predators chasing prey, so I began with ("dumb") pursuit and flee functions. After ensuring those worked I implemented an avoidance behavior similar to flee, but more closely geared towards not bumping into things rather than running away from them. Boids of the same type (I.E. Either predator or prey) will avoid each other while prey flees from predators. All Boids avoid the obstacles that were placed in the test scene.

After ensuring that the behaviors worked I adjusted the pursuit behavior to have a toggle for whether or not the boid should predict the target's movement (the "Is Smart" bool). The idea was to have some boids pursue the target's intended position and others only be able to pursue the target's current position. Then I implemented the state machine for the predators.

The system was designed in 2.5D to simulate ground-based creatures and the limitations they face during a hunt. This is further reflected in the Line Of Sight (LOS) requirements for finding Prey. The idea was to make a more realistic simulation where predators could potentially not find any prey.

As for designing the project itself, I took what I call a "three-coat" approach. First I went through programming all the basic functionality and making sure everything worked, then I went back over everything and added polish and new features, such as the smart/dumb toggle for the chase behaviour. Finally, I went back over everything a third time to ensure it was all as polished as possible and seeing if there was anything quick I could add in the remaining time.
