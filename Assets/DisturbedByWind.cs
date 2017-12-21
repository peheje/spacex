using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisturbedByWind : MonoBehaviour {
    public float MaxWindSpeed = 1.0f;
    private Rigidbody body;

    void Start () {
        body = GetComponent<Rigidbody>();
    }

    void OnValidate()
    {
        MaxWindSpeed = Mathf.Clamp(MaxWindSpeed, 0.0f, 1.0f);
    }

    void FixedUpdate () {
        float xNoise = Mathf.Clamp(Random.value, -MaxWindSpeed, MaxWindSpeed);
        float yNoise = Mathf.Clamp(Random.value, -MaxWindSpeed, MaxWindSpeed);
        float zNoise = Mathf.Clamp(Random.value, -MaxWindSpeed, MaxWindSpeed);
        body.AddRelativeTorque(0, 0, zNoise, ForceMode.Force);
    }
}
