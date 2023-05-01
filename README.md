# Game Engines Final Asessment

By Adam Kaufman
D19127919
DT508

This is a 2.5D nature sim focused around the hunting/scavenging behaviours of prey and predators in a small environment. This project features two kinds of Boids; Prey and Predators, as well as terrain features, passively spawning food items, and a day/night cycle.

https://youtu.be/Tujlmv6r0co

## Instructions
Load any of the scenes and hit play. W,A,S, and D control camera movement. Spacebar to move the camera upwards, Left Shift move the camera down, move the mouse to look around, and hold Left Ctrl to freeze the camera rotation to assist clicking outside the window.
The other controls are detailed in the UI: use 1, 2, 3, 4, and 5 to select what to spawn. Press P to spawn it, and press L to delete things (You may need to aim at a specific part of some trees, and the hills and rock barrier around the edges of the map cannot be deleted). Press F to toggle between spawning Diurnal and Nocturnal Boids.

## How it Works
There are a number of basic Boid behaviours on each Boid - chase, flee, avoid, and wander. There is a different AI on each Boid type that controls certain elements of behaviour.
The Prey Boids (the blue ones) wander around looking for food (the greenish cubes), which spawns naturally over time. They also run from predators. Predator Boids (the red ones) chase the Prey. When a Predator gets close enough to a Prey it will damage the Prey. Prey can eat food to recover their health, but if a Prey's health reaches zero it dies and eventually decays.
Predators cannot detect prey unless they have line of sight and are close enough. If there isn't any food nearby for that respective Boid type, the Boid will wander.
By default, the day/night cycle is two minutes long - 60 seconds of day and 60 seconds of night. Once night falls, Diurnal boids will slow to a stop and nocturnal boids will become active, and vice-versa when day breaks.

After weighting and coding everything the emergent behaviour began to appear; I weighted the Predators so that they were very fast but changed directions slowly, and I weighted the Prey such that they were much more maneuverable. This allowed Prey boids to outwit Predator boids. Because of their fast speed and low maneuverability Predator boids would often collide with obstacles and become "stunned" for a few seconds as their avoidance behaviour slowly reversed course. This was completely unintentional but somewhat natural. In addition, the Predators would hang back from a Prey while another Predator attacked it. This was due to the avoid script not filtering out Boids of the same type. This gave a more natural impression of hanging back and waiting for an opportunity to move in. Prey boids exhibit a similar behaviour, but they all fall back from their food target giving an impression closer to fighting over the food. All of these behaaviour are "effortless action" as described by Alan Watts.
The player can direclty influence the scene by creating and deleting elements, thus forcing all boids to adapt to the changin environment.

The nature of the Predator boids being fast but slow to change directions made their hunting look a bit more natural as they picked up speed and sprinted to a Prey, grazing it and damaging the Prey but not killing it, mimicking the hunting behaviour of some real-world animals.

### There are a number of scenes available to run;

* "Base Scene", which contains one of each Boid and one of each terrain feature in a small area.
* "Dense Obstacles", which has a high concentration of terrain features.
* "King Hill", which features a large hill modifing most of the map.
* "Mass Scene", which is a relatively open area with a large amount of Boids.
* "Predator Mob", pitting a large amount of predators against a single prey.
* "Prey Mob", which is the opposite of the above; a large amount of prey with a single predator.
* "Threatless", which contains a large amount of prey but no predators.
* "Sandbox", which is entirely blank to be customized as the user sees fit.
* "Omega Scene", which is a very large scene with varied terrain features and groups of boids of each type and active time.

## Asset List
### Scripts (.CS files)
* Arrive - original script, however while kept in the project it is not used and not functional
* Avoid - transcribed and modified from reference
* Boid - transcribed and modified from reference
* BoidBehavior - transcribed from reference
* CameraController - original script
* Chase - transcribed and modified from reference
* CopiedAvoid - copied from reference (see in-code disclaimer)
* DayNightCycle - original script
* Flee - transcribed and modified from reference
* FoodSpawning - original script
* PredatorAI - original script
* PreyAI - original script
* Wander - transcribed and modified from reference

### Other Assets
* All in-game models are original assemblies of Unity prefab shapes
* The UI sprites are original creations made in [Piskel](https://www.piskelapp.com/)

All code was referenced from [this repository](https://github.com/skooter500/GE2-2023). All code was modified and extensively commented to ensure understanding.

As for designing the project itself, I took what I call a "three-coat" approach. First I went through programming all the basic functionality and making sure everything worked, then I went back over everything and added polish and new features, such as the smart/dumb toggle for the chase behaviour. Finally, I went back over everything a third time to ensure it was all as polished as possible and seeing if there was anything quick I could add in the remaining time - usually prettier models. This was also the phase in which I began to craft the default scenarios for the project.
