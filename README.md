# Genetic Algorithm Track Navigation

![alt tag](https://raw.githubusercontent.com/Aaron-Benson/genetic-track-navigation/master/demo.png)

A car must navigate a track that it knows nothing about; it simply follows a path. To begin with, these paths are randomly generated and 
the car will not make it very far before hitting a wall. However, after this group of random paths have been tested, a new group of paths are generated. 
The goal of the new group is to perform better than the previous group. It can do this by evaluating the performance of old paths. In this case, the method 
used to generate these new paths from previous paths is called a genetic algorithm. 

A genetic algorithm is inspired by 3 processes of natural selection: selection, crossover, and mutation. In order to model these processes, 
a way of evaluating how well a path did was necessary to create. Thus, checkpoints were added to the track. How well a path does is called fitness. The following 
are some vague definitions of the processes of natural selection:

1. Selection - genomes with a higher fitness will be more likely to move to the next generation.
2. Crossover (Breeding) - genomes with a higher fitness will have a better chance of getting their offspring into the next generation.
3. Mutation - genomes with higher fitnesses will produce some offspring with mutations.

Actually interpreting these processes into a computer program required some creativity and thought.

Interpretation of the processes of natural selection:

1. Selection - the best 3 genomes from the previous generation will move to the next generation
2. Crossover (Breeding) - the best 7 genomes from the previous generation will breed with the best 5 genomes from the previous generation. Here, breeding is done by randomly swapping path points with other path points.
3. Mutation - the best 10 genomes will "mutate" and will be placed into the next generation. Here, mutation is done by randomly selecting some path points to be randomized or removed.

The resulting program was a success. In only 5 populations, the genetic algorithm produced a result allowing the car to complete an entire lap. 

C# scripts can be found in the Assets folder of the repository.

## Running (Windows only, install if on another system)

1. Clone the repository locally
2. Execute either genetic-track-navigation-win32.exe or genetic-track-navigation-win64.exe according to your system

## Installation (Unity or Windows Executable)

1. Clone the repository locally
2. In Unity, select open project and navigate to the root folder of the repository (genetic-track-navigation)
3. Play and edit as you wish

## Controls

0 - Set time step to 1

1 - Set time step to 2

2 - Set time step to 4

R - Reset

Escape - Quit

## Credits

Aaron Benson