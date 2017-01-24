using UnityEngine;
using System.Collections;

/// <summary>
/// Manages checkpoint information and stores fitness of the car.
/// </summary>
public class CheckpointManager : MonoBehaviour {

    ArrayList checkpoints = new ArrayList();
    public Material defaultMaterial;
    public Material currentGoalMaterial;
    public int currentFitness = 0;
    public int currentGoal = 0;

    // Initializes checkpoints.
    void Start() {
        for (int i = 1; i <= 24; i++)
            checkpoints.Add(GameObject.Find("checkpoint (" + i + ")"));
	}

	/// <summary>
	/// Resets the checkpoints, setting them all back to their default material except for the first.
	/// Sets the goal to the first checkpoint.
	/// </summary>
    public void ResetCheckpoints()
    {
        currentGoal = 0;
        foreach (GameObject checkpoint in checkpoints)
        {
            checkpoint.GetComponent<MeshRenderer>().enabled = true;
            checkpoint.GetComponent<Renderer>().material = defaultMaterial;
        }
        ((GameObject)checkpoints[0]).GetComponent<Renderer>().material = currentGoalMaterial;
    }

	/// <summary>
	/// Resets the manager and checkpoints.
	/// </summary>
    public void ResetManager()
    {
        ResetCheckpoints();
        currentFitness = 0;
    }

	/// <summary>
	/// Handles the car's collision with checkpoints. Ignores if the goal is not the checkpoint. Otherwise, fitness is incremented and the goal is updated.
	/// </summary>
	/// <param name="name">Name of the checkpoint hit.</param>
    public void CheckpointHit(string name)
    {
        // Gets the checkpoint identity.
        int checkpointID;
        string number = name.Substring(12, 2);
        char c = number.Substring(1, 1).ToCharArray()[0];
        if (c.Equals('0') || c.Equals('1') || c.Equals('2') || c.Equals('3') || c.Equals('4') || c.Equals('5') || c.Equals('6') || c.Equals('7') || c.Equals('8') || c.Equals('9'))
            checkpointID = int.Parse(number);
        else
            checkpointID = int.Parse(number.Substring(0, 1));

        // Short circuits if checkpoint is not goal.
        if (checkpointID - 1 != currentGoal)
            return;

        // Otherwise, updates goal and increments fitness.
        currentFitness++;
        if (checkpointID >= checkpoints.Count)
            ResetCheckpoints();
        else {
            ((GameObject)checkpoints[checkpointID - 1]).GetComponent<MeshRenderer>().enabled = false;
            ((GameObject)checkpoints[checkpointID]).GetComponent<Renderer>().material = currentGoalMaterial;
            currentGoal = checkpointID;
        }
    }
}
