# Game Engines Final Asessment

My first step in designing this project was to get the behaviors working. My goal was to simulate predators chasing prey, so I began with simple pursuit and flee functions. After ensuring those worked I implemented an avoidance behavior similar to flee, but more closely geared towards not bumping into things rather than running away from them. Boids of the same type (I.E. Either predator or prey) will avoid each other while prey flees from predators. All Boids avoid the obstacles that were placed in the test scene.

After ensuring that the behaviors worked and the variables and weights were adjusted to the proper values I added the code and logic for a predator catching prey, as well as a new AI state that the predator Boid would enter for a few seconds after catching a prey Boid.

The system was designed in 2.5D to simulate ground-based creatures and the limitations they face during a hunt.
