using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlledByPID : MonoBehaviour {

    private Rigidbody body;
    private PIDController xPID = new PIDController();
    private PIDController zPID = new PIDController();

    void Start () {
        UnityEditor.SceneView.FocusWindowIfItsOpen(typeof(UnityEditor.SceneView));


        body = GetComponent<Rigidbody>();
        Debug.Log("controlled by pid");
    }
	
	void FixedUpdate () {
        // Get x and z error
        var target = 0;
        var dx = body.rotation.x - target;
        var dz = body.rotation.z - target;

        // Get control
        var xControl = xPID.GetControl(dx);
        var zControl = zPID.GetControl(dz);

        Debug.Log("force x,z: " + xControl + " " + zControl);

        // Apply control
        body.AddRelativeTorque(xControl, 0, zControl, ForceMode.Impulse);
    }
}
