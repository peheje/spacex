using Assets;
using UnityEngine;

public class ControlledByPID : MonoBehaviour {

    private Rigidbody body;
    private PIDController xPID = new PIDController();
    private PIDController yPID = new PIDController();
    private PIDController zPID = new PIDController();

    public float kp = 0.0f;
    public float ki = 0.0f;
    public float kd = 0.0f;

    void OnValidate()
    {
        // Clamp public PID coefficients
        kp = Mathf.Clamp(kp, 0.0f, 100.0f);
        ki = Mathf.Clamp(ki, 0.0f, 100.0f);
        kd = Mathf.Clamp(kd, 0.0f, 100.0f);
    }

    void Start () {
        // UnityEditor.EditorWindow.FocusWindowIfItsOpen(typeof(UnityEditor.SceneView));

        body = GetComponent<Rigidbody>();
        Debug.Log("controlled by pid");
    }
	
	void FixedUpdate () {
        // Get x and z error
        var target = 0;
        var dx = body.rotation.x - target;
        var dy = body.rotation.y - target;
        var dz = body.rotation.z - target;

        // Get control
        var xControl = xPID.GetControl(dx, kp, ki, kd);
        var yControl = yPID.GetControl(dy, kp, ki, kd);
        var zControl = zPID.GetControl(dz, kp, ki, kd);

        // Limit control
        const float maxVal = 1.0f;
        xControl = Mathf.Clamp(xControl, -maxVal, maxVal);
        yControl = Mathf.Clamp(yControl, -maxVal, maxVal);
        zControl = Mathf.Clamp(zControl, -maxVal, maxVal);

        Debug.Log("force x,z: " + xControl + " " + zControl);

        // Apply control
        body.AddRelativeTorque(xControl, yControl, zControl, ForceMode.Force);
    }
}
