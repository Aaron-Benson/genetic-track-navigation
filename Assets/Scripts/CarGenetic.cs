using UnityEngine;
using System.Collections;

/// <summary>
/// Top level genetic algorithm controller for the car. Contains the population that is currently being tested and calls to create new populations when complete. 
/// Contains the positioning and movement information for the car.
/// Handles collisions with walls and checkpoints.
/// </summary>
[RequireComponent (typeof (CheckpointManager))]
public class CarGenetic : MonoBehaviour
{

    public Genetic geneticNetwork;
    public float RotationSpeed = 3;
    public float MovementSpeed = 4.5f;
    Vector3 initialPosition;
    Quaternion initialRotation;

    // Use this for initialization
    void Start()
    {
        geneticNetwork = new Genetic();
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {

        // Creates a new population when all genomes have finished evaluation.
        if (geneticNetwork.currentGenome >= geneticNetwork.genomeSize)
            geneticNetwork.createNewPopulation();

        // Sets current graph.
        Graph currentGraph = (Graph)geneticNetwork.genomeFamily[geneticNetwork.currentGenome];

		// Draws a red line showing the current genome's graph.
		for (int i = 0; i < currentGraph.nodeCount; i++)
			Debug.DrawLine((Vector3)currentGraph.graph[i], (Vector3)currentGraph.graph[i == currentGraph.nodeCount - 1 ? 0 : i + 1], Color.red);

        // Makes sure that the current node is correct; goes to the next node if the car is too close to the current node
        if (Vector3.Distance((Vector3)currentGraph.graph[currentGraph.currentNode], transform.position) < 1) {
            currentGraph.currentNode++;
            if (currentGraph.currentNode >= currentGraph.nodeCount)
                currentGraph.currentNode = 0;
        }

        // Rotates toward the current node and moves forward.
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(((Vector3)currentGraph.graph[currentGraph.currentNode] - transform.position).normalized), Time.deltaTime * RotationSpeed);
        transform.position += transform.forward * Time.deltaTime * MovementSpeed;
    }

	/// <summary>
	/// Raises the trigger stay event.
	/// </summary>
    void OnTriggerStay(Collider other) {

		// When the car collides with wall, sets the genome's current fitness and moves to the next genome. Resets the car's position and rotation and the checkpoints.
        if (other.name.Equals("Wall")){
            geneticNetwork.setFitness(GetComponent<CheckpointManager>().currentFitness);
            ((Graph)geneticNetwork.genomeFamily[geneticNetwork.currentGenome]).lastBestNode = ((Graph)geneticNetwork.genomeFamily[geneticNetwork.currentGenome]).currentNode;
            ((Graph)geneticNetwork.genomeFamily[geneticNetwork.currentGenome]).currentNode = 0;
            geneticNetwork.currentGenome++;
			if (geneticNetwork.currentGenome >= geneticNetwork.genomeSize)
				geneticNetwork.createNewPopulation();
            transform.position = initialPosition;
            transform.rotation = initialRotation;
            GetComponent<CheckpointManager>().ResetManager();
        }

		// When colliding with a checkpoint, tells checkpoint manager that a checkpoint was hit.
        if (other.name.Length > 13 && other.name.Substring(0, 10).Equals("checkpoint"))
            GetComponent<CheckpointManager>().CheckpointHit(other.name);
    }
}