# Game Engines Final Asessment

My first step in designing this project was to get the behaviors working. My goal was to simulate predators chasing prey, so I began with ("dumb") pursuit and flee functions. After ensuring those worked I implemented an avoidance behavior similar to flee, but more closely geared towards not bumping into things rather than running away from them. Boids of the same type (I.E. Either predator or prey) will avoid each other while prey flees from predators. All Boids avoid the obstacles that were placed in the test scene.

After ensuring that the behaviors worked I adjusted the pursuit behavior to have a toggle for whether or not the boid should predict the target's movement (the "Is Smart" bool). The idea was to have some boids pursue the target's intended position and others only be able to pursue the target's current position. Then I implemented the state machine for the predators.

The system was designed in 2.5D to simulate ground-based creatures and the limitations they face during a hunt. This is further reflected in the Line Of Sight (LOS) requirements for finding Prey. The idea was to make a more realistic simulation where predators could potentially not find any prey.

Once all the fundamental behaviours were implemented I took some time to think about what could be added to make the simulation unique. I settled on adding different weapons to certain scenarios. Boids would attempt to grab these weapons and use them for an advantage, usually a larger attack range (predtors) or protection from attacks. I also added behaviour for harvesting killed prey and began to set up some scenarios.

I decided that the player had very little input after the scene started, so I gave them the ability to spawn and delete both kinds of Boids, create and remove obstacles, as well as place weapons. I also gave them the ability to pause the simulation. This allowed them to manipulate the scenarios while they were running and force the Boids to adapt.

Here's where the emergent behaviour began to appear; I weighted the Predators so that they were very fast but changed directions slowly, and I weighted the Prey such that they were much more maneuverable. This allowed Prey boids to outwit Predator boids. Because of their fast speed and low maneuverability Predator boids would often collide with obstacles and become "stunned" for a few seconds as their avoidance behaviour slowly reversed course. This was completely unintentional but somewhat natural.

As for designing the project itself, I took what I call a "three-coat" approach. First I went through programming all the basic functionality and making sure everything worked, then I went back over everything and added polish and new features, such as the smart/dumb toggle for the chase behaviour. Finally, I went back over everything a third time to ensure it was all as polished as possible and seeing if there was anything quick I could add in the remaining time.
