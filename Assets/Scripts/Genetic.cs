using UnityEngine;
using System.Collections;

/// <summary>
/// Creates new populations from other populations using a genetic algorithm.
/// </summary>
public class Genetic {

    public ArrayList genomeFamily = new ArrayList();
    public int currentGenome;
    public int genomeSize = 20;
	public int currentGeneration = 0;

	/// <summary>
	/// Initializes a new instance of the <see cref="Genetic"/> class. Creates a random population.
	/// </summary>
    public Genetic() {
        for (int i = 0; i < genomeSize; i++)
            genomeFamily.Add(new Graph());
        currentGenome = 0;
    }

	/// <summary>
	/// Sets the fitness of the current genome to i.
	/// </summary>
	/// <param name="i">The fitness.</param>
    public void setFitness(int i) {
		if (currentGenome < genomeSize)
        	((Graph)genomeFamily[currentGenome]).setFitness(i);
    }

	/// <summary>
	/// Creates a new population using the old population and their levels of fitness.
	/// 3 genomes are created using selection, 7 genomes are created using breeding, and 10 genomes are created using mutation.
	/// </summary>
    public void createNewPopulation() {

		// Initializes the new population.
        ArrayList newGenomeFamily = new ArrayList();
		currentGenome = 0;

		// Sorts the old population based on fitness.
        genomeFamily.Sort();

        // Selection procedure - selects the best 3 genomes and places them in the new population.
        for (int i = genomeSize - 3; i < genomeSize; i++)
            newGenomeFamily.Add(genomeFamily[i]);

        // Breeding procedure - selects the best 7 genomes and "breeds" them randomly with the best 5 genomes.
		// Breeding is done by swapping out graph nodes for other graph nodes in genomes randomly.
        for (int i = genomeSize - 9; i < genomeSize - 2; i++)
        {
			Graph newGraph = new Graph (((Graph)genomeFamily[i]).graph);
			for (int j = 0; j < ((Graph)genomeFamily[i]).graph.Count && j < ((Graph)genomeFamily[genomeSize - 1]).graph.Count; j++)
            {
				float rand = Random.Range (0f, 100.0f);
				if (rand < 40) // dont breed
					continue;
				int randIndex = genomeSize - 1; // breed with the best genome
                Vector3 n = new Vector3();
				if (rand > 65) // breed with the second best genome
					randIndex--;
				if (rand > 80) // breed with the third best genome
					randIndex--;
				if (rand > 90) // breed with the fourth best genome
					randIndex--;
				if (rand > 95) // breed with the fifth best genome
					randIndex--;
				
				n.x = ((Vector3)((Graph)genomeFamily [randIndex]).graph [Mathf.Min(((Graph)genomeFamily [randIndex]).nodeCount - 1, j)]).x;
				n.z = ((Vector3)((Graph)genomeFamily [randIndex]).graph [Mathf.Min(((Graph)genomeFamily [randIndex]).nodeCount - 1, j)]).z;
				n.y = GameObject.Find ("Car").transform.position.y;
                newGraph.graph[j] = n;

            }
            newGenomeFamily.Add(newGraph);
        }

        // Mutation procedure - selects the best 9 genomes and "mutates" them randomly.
		// Mutation is done by randomly randomizing and removing some graph nodes.
        for (int i = genomeSize - 9; i < genomeSize; i++)
        {
			Graph newGraph = new Graph (((Graph)genomeFamily[i]).graph);

			// Randomizes points.
			for (int j = ((Graph)genomeFamily[i]).lastBestNode - 1; j < ((Graph)genomeFamily[i]).graph.Count; j++)
            {
				if (j == -1) 
					continue;
				
                Vector3 n = new Vector3();
                n.x = UnityEngine.Random.Range(-8.0f, 8.5f);
                n.z = UnityEngine.Random.Range(3.0f, 19.0f);
                n.y = GameObject.Find("Car").transform.position.y;
                newGraph.graph[j] = n;
            }

			// Randomly randomizes graph points.
            if (Random.Range(0f, 100.0f) > 40) {
                while (true) {
					if (Random.Range(0f, 100.0f) > 70)
                        break;
                    Vector3 n = new Vector3();
                    n.x = UnityEngine.Random.Range(-8.0f, 8.5f);
                    n.z = UnityEngine.Random.Range(3.0f, 19.0f);
                    n.y = GameObject.Find("Car").transform.position.y;
					newGraph.graph.Add(n);
                }
				newGraph.nodeCount = newGraph.graph.Count;
            }

			// Randomly removes graph points.
            if (Random.Range(0f, 100.0f) > 40) {
                while (true) {
					if (Random.Range(0f, 100.0f) > 70 || newGraph.graph.Count < 10)
                        break;
					newGraph.graph.Remove(newGraph.graph.Count - 1);
                }
				newGraph.nodeCount = newGraph.graph.Count;
            }

			// Randomize graph points.
			for (int k = 0; k < 2; k++) {
				if (Random.Range (0f, 100.0f) > 60) {
					Vector3 n = new Vector3 ();
					n.x = UnityEngine.Random.Range (-8.0f, 8.5f);
					n.z = UnityEngine.Random.Range (3.0f, 19.0f);
					n.y = GameObject.Find ("Car").transform.position.y;
					newGraph.graph [Random.Range (0, newGraph.graph.Count - 1)] = n;
				}
			}

            newGenomeFamily.Add(newGraph);
        }

		// Creates one completely new genome.
		newGenomeFamily.Add(new Graph());

        genomeFamily = new ArrayList(newGenomeFamily);
        foreach (Graph g in genomeFamily) {
            setFitness(0);
            g.currentNode = 0;
        }

		currentGeneration++;
    }

}
