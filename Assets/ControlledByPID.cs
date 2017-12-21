using Assets;
using UnityEngine;

public class ControlledByPID : MonoBehaviour {

    public GameObject floor;
    private Rigidbody body;
    private PIDController xPID = new PIDController();
    private PIDController yPID = new PIDController();
    private PIDController zPID = new PIDController();
    private PIDController wPID = new PIDController();

    public float kp = 5.0f;
    public float ki = 0.01f;
    public float kd = 150.0f;

    public float kpw = 0.0f;
    public float kiw = 0.0f;
    public float kdw = 0.0f;
    private float floorPosition;

    void OnValidate()
    {
        // Clamp public PID coefficients
        kp = Mathf.Max(kp, 0.0f);
        ki = Mathf.Max(ki, 0.0f);
        kd = Mathf.Max(kd, 0.0f);

        kpw = Mathf.Max(kpw, 0.0f);
        kiw = Mathf.Max(kiw, 0.0f);
        kdw = Mathf.Max(kdw, 0.0f);
    }

    void Start () {
        UnityEditor.EditorWindow.FocusWindowIfItsOpen(typeof(UnityEditor.SceneView));

        floorPosition = floor.transform.position.y;
        body = GetComponent<Rigidbody>();
        Debug.Log("controlled by pid");
    }
	
	void FixedUpdate () {
        // Get x and z error
        var target = 0;
        var dx = body.rotation.x - target;
        var dy = body.rotation.y - target;
        var dz = body.rotation.z - target;


        // var dw = body.position.y - floorPosition;
        var dw = body.velocity.y;

        // Get gimbal control
        var xControl = xPID.GetControl(dx, kp, ki, kd);
        var yControl = yPID.GetControl(dy, kp, ki, kd);
        var zControl = zPID.GetControl(dz, kp, ki, kd);

        // Get rocket control
        var wControl = wPID.GetControl(dw, kpw, kiw, kdw);

        // Limit control
        const float maxGimbal = 1.0f;
        xControl = Mathf.Clamp(xControl, -maxGimbal, maxGimbal);
        yControl = Mathf.Clamp(yControl, -maxGimbal, maxGimbal);
        zControl = Mathf.Clamp(zControl, -maxGimbal, maxGimbal);

        const float maxPower = 10000.0f;
        wControl = Mathf.Clamp(wControl, -maxPower, maxPower);

        // Apply control
        body.AddRelativeTorque(xControl, yControl, zControl, ForceMode.Force);
        body.AddForce(0, wControl, 0, ForceMode.Force);
    }
}
