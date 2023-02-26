using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiCar : MonoBehaviour
{
    public Rigidbody rigidbody;
    public float carspeed;
    public float speed;
    public Transform wheel1;
    public Transform wheel2;
    public Transform wheel3;
    public Transform wheel4;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //rigidbody.velocity = transform.forward * carspeed;
        transform.Translate(Vector3.forward * carspeed);
        wheel1.Rotate(Vector3.left * Time.deltaTime * speed, Space.World);
        wheel2.Rotate(Vector3.left * Time.deltaTime * speed, Space.World);
        wheel3.Rotate(Vector3.left * Time.deltaTime * speed, Space.World);
        wheel4.Rotate(Vector3.left * Time.deltaTime * speed, Space.World);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Border")
        {
            SpawnItAgain();
        }
    }

    private void SpawnItAgain()
    {
        int point = Random.Range(0, 5);
        transform.SetPositionAndRotation(AiCarSpawnManager.spawnPoints[point].position, AiCarSpawnManager.spawnPoints[point].rotation);
    }
}
