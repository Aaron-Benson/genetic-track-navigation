using UnityEngine;
using System;
using System.Collections;

/// <summary>
/// Graph class. Contains 8 to 30 ordered nodes representing a genome's path.
/// </summary>
public class Graph : IComparable {

    public int nodeCount;
    public int fitness;
    public ArrayList graph = new ArrayList();
    public int currentNode;
    public int lastBestNode;

	/// <summary>
	/// Initializes a new instance of the <see cref="Graph"/> class. Creates an ordered list of 8 to 30 nodes that are in random locations.
	/// </summary>
    public Graph() {
		
		// Determines the amount of nodes in the graph.
        nodeCount = UnityEngine.Random.Range(8, 30);

		// Generates random locations for nodes and adds them to the graph.
        for (int i = 0; i < nodeCount; i++) {
            Vector3 n = new Vector3();
            if (i != 0) {
                n.x = UnityEngine.Random.Range(-8.0f, 8.5f);
                n.z = UnityEngine.Random.Range(3.0f, 19.0f);
                n.y = GameObject.Find("Car").transform.position.y;
            } else {
                n.x = GameObject.Find("Car").transform.position.x;
                n.z = GameObject.Find("Car").transform.position.z;
                n.y = GameObject.Find("Car").transform.position.y;
            }
            graph.Add(n);
        }
        currentNode = 0;
    }

	/// <summary>
	/// Initializes a new instance of the <see cref="Graph"/> class using nodes.
	/// </summary>
	/// <param name="nodes">Nodes.</param>
    public Graph(ArrayList nodes) {
        nodeCount = nodes.Count;
        graph = new ArrayList(nodes);
        currentNode = 0;
    }

	/// <summary>
	/// Sets the fitness to i.
	/// </summary>
	/// <param name="i">The fitness.</param>
    public void setFitness(int i) {
        fitness = i;
    }

	/// <summary>
	/// Compares two graphs.
	/// </summary>
	/// <returns>Fitness of this compared to the fitness of other.</returns>
	/// <param name="obj">Other graph.</param>
    public int CompareTo(object obj) {
        if (obj == null) return 1;

        Graph xGraph = (Graph)obj;

        return this.fitness.CompareTo(xGraph.fitness);
    }
}
