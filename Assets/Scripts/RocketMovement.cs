using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMovement : MonoBehaviour
{

    [SerializeField] float thrustForce = 1f;
    [SerializeField] float rotationForce = 1f;

    Rigidbody rg;

    // Start is called before the first frame update
    void Start()
    {
        rg = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        processThrust();
        processHandling();
    }

    void processThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            // Debug.Log("thrusting " + Vector3.up * thrustForce * Time.deltaTime);
            rg.AddRelativeForce(Vector3.up * thrustForce * Time.deltaTime);
        }
    }

    void processHandling()
    {
        if (Input.GetKey(KeyCode.D)) processZRotation(true);
        if (Input.GetKey(KeyCode.A)) processZRotation(false);


    }

    void processZRotation(bool isRight)
    {
        rg.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationForce * Time.deltaTime * (isRight ? -1 : 1));
        rg.freezeRotation = false;
    }
}
