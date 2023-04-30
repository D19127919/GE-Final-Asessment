# Game Engines Final Asessment

My first step in designing this project was to get the behaviors working. My goal was to simulate predators chasing prey, so I began with ("dumb") pursuit and flee functions. Boids of the same type (I.E. Either predator or prey) will avoid each other while prey flees from predators. All Boids avoid the obstacles that were placed in the test scene.

After ensuring that the behaviors worked I adjusted the pursuit behavior to have a toggle for whether or not the boid should predict the target's movement (the "Is Smart" bool). The idea was to have some boids pursue the target's intended position and others only be able to pursue the target's current position. Then I implemented the state machine for the predators.

After crafting most of the base behaviours and scenarios I began to wonder what I could do to make this unique. I added a hunger system that made the Prey boids prioritize seeking out food more. I also added in a day-night cycle and behaviour for sleeping creatures.

The system was designed in 2.5D to simulate ground-based creatures and the limitations they face during a hunt. This is further reflected in the Line Of Sight (LOS) requirements for finding Prey. The idea was to make a more realistic simulation where predators could potentially not find any prey.

Once all the fundamental behaviours were implemented I took some time to think about what could be added to make the simulation unique. I settled on adding certain items that the Prey boids would seek out while not being chased and implemented a system that spawned them randomly over time. I also added proper death behaviour for both boid types.

I decided that the player had very little input after the scene started, so I gave them the ability to spawn and delete both kinds of Boids and create and remove obstacles. This allowed them to manipulate the scenarios while they were running and force the Boids to adapt.

Here's where the emergent behaviour began to appear; I weighted the Predators so that they were very fast but changed directions slowly, and I weighted the Prey such that they were much more maneuverable. This allowed Prey boids to outwit Predator boids. Because of their fast speed and low maneuverability Predator boids would often collide with obstacles and become "stunned" for a few seconds as their avoidance behaviour slowly reversed course. This was completely unintentional but somewhat natural. In addition, the Predators would hang back from a Prey while another Predator attacked it. This was due to the avoid script not filtering out Boids of the same type. This gave a more natural impression of hanging back and waiting for an opportunity to move in. Prey boids exhibit a similar behaviour, but they all fall back from their food target giving an impression closer to fighting over the food. All of these behaaviour are "effortless action" as described by Alan Watts.

The nature of the Predator boids being fast but slow to change directions made their hunting look a bit more natural as they picked up speed and sprinted to a Prey, grazing it and damaging the Prey but not killing it, mimicking the hunting behaviour of some real-world animals.

As for designing the project itself, I took what I call a "three-coat" approach. First I went through programming all the basic functionality and making sure everything worked, then I went back over everything and added polish and new features, such as the smart/dumb toggle for the chase behaviour. Finally, I went back over everything a third time to ensure it was all as polished as possible and seeing if there was anything quick I could add in the remaining time - usually prettier models. This was also the phase in which I began to craft the default scenarios for the project.
