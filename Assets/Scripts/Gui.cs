using UnityEngine;
using System.Collections;

/// <summary>
/// Displays GUI describing the current status of the game and controls.
/// </summary>
public class Gui : MonoBehaviour {
	
	int maxFitness = 0;
	
	// Updates the maximum fitness.
	void Update () {
		if (GetComponent<CheckpointManager> ().currentFitness > maxFitness)
			maxFitness = GetComponent<CheckpointManager> ().currentFitness;
	}

	public void OnGUI() {
		int x = Screen.width/2 - 70;
		int y = Screen.height/2 - 100;
		GUI.Label (new Rect(x, y, 200, 20), "Current Generation: " + GetComponent<CarGenetic>().geneticNetwork.currentGeneration);
		GUI.Label (new Rect(x, y + 20, 200, 20), "Current Genome: " + GetComponent<CarGenetic>().geneticNetwork.currentGenome + " out of " + GetComponent<CarGenetic>().geneticNetwork.genomeSize);
		GUI.Label (new Rect(x, y + 40, 200, 20), "Current Fitness: " + GetComponent<CheckpointManager>().currentFitness);
		GUI.Label (new Rect(x, y + 60, 200, 20), "Max Fitness: " + maxFitness);

		x = 20;
		y = 20;
		GUI.Label (new Rect(x, y, 200, 20), "Controls:");
		GUI.Label (new Rect(x, y + 20, 200, 25), "0 - Set time step to 1");
		GUI.Label (new Rect(x, y + 40, 200, 25), "1 - Set time step to 2");
		GUI.Label (new Rect(x, y + 60, 200, 25), "2 - Set time step to 4");
		GUI.Label (new Rect(x, y + 80, 500, 25), "R - Reset");
		GUI.Label (new Rect(x, y + 100, 200, 25), "Escape - Quit");
	}
}
