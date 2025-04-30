using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KayakController : MonoBehaviour {

    public float speed = 5.0f;

    public float friction = 10;
    public float torque = 10;

    private static KayakController _singleton;
    Vector3 dir;
    Quaternion lookRot;

    [SerializeField]
    Rigidbody _rb;

    Quaternion _initPos;

    public static KayakController Singleton
    {
        get
        {
            if (_singleton==null)
            {
                _singleton = FindObjectOfType<KayakController>();
            }
            return _singleton;
        }
    }

    public float GetAngle()
    {
        return Quaternion.Angle(_initPos, transform.rotation);
    }

    public Vector3 GetSpeed()
    {
        return _rb.velocity;
    }

    private void Start()
    {
        _initPos = transform.rotation;
        _rb.inertiaTensorRotation = Quaternion.identity;
    }

    void FixedUpdate()
    {
        //desaceleracion
        _rb.velocity = Vector3.Lerp(_rb.velocity, Vector3.zero, Time.deltaTime * friction);
    }

    public void AddThrust(Vector3 thrust, float rotative, int side)
    {
        thrust.y = 0;

        if (_rb.velocity.magnitude < 4)
        {
            _rb.AddForce(speed * (thrust), ForceMode.Acceleration);
        }

        //Vector3 torq = new Vector3(0, thrust.magnitude * side * (torque - _rb.velocity.magnitude),0);
        //Vector3 torq = new Vector3(0, ((_rb.velocity - thrust).magnitude * side * torque),0);
        //Vector3 torq = new Vector3(0, thrust.magnitude * side * (torque - _rb.velocity.magnitude), 0);
        //_rb.AddRelativeTorque(torq, ForceMode.Acceleration);
        Vector3 vector = new Vector3(0f, (_rb.velocity - thrust).magnitude * rotative * (float)side * torque, 0f);
        _rb.AddRelativeTorque(-vector, ForceMode.Acceleration);
    }
}