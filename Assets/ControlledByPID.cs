using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlledByPID : MonoBehaviour {

    private Rigidbody body;
    private PIDController xPID = new PIDController();
    private PIDController zPID = new PIDController();

    void Start () {
        body = GetComponent<Rigidbody>();
	}
	
	void UpdateFixed () {
        // Get x and z error
        var target = 0;
        var dx = body.position.x - target;
        var dz = body.position.z - target;

        // Get control
        var xControl = xPID.GetControl(dx);
        var zControl = zPID.GetControl(dz);

        // Apply control
        body.AddRelativeForce(xControl, 0, zControl, ForceMode.Impulse);
    }
}
