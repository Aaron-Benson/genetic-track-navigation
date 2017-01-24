using UnityEngine;
using System.Collections;

/// <summary>
/// Allows the user to increase the time step for the car's movement.
/// 0 is normal speed, 1 is 2x speed, and 2 is 4x speed.
/// </summary>
public class manualsteer : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            Time.timeScale = 2;
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            Time.timeScale = 4;
		else if (Input.GetKeyDown(KeyCode.Alpha0))
			Time.timeScale = 1;
    }
}
