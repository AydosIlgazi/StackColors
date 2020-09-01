using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] string color;
    [SerializeField] float speed = 9f;
    GameObject user;
    float offset = 1f;
    Rigidbody rb;
    Vector3 offsetVector;
    bool isCollisionOcurred = false;
    // Start is called before the first frame update
    void Start()
    {
        user = GameObject.Find("User");
        rb = GetComponent<Rigidbody>();
        //rb.velocity = new Vector3(0, speed, 0);
        offsetVector = new Vector3(0f,0.5f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isCollisionOcurred)
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }
        else
        {
           
           transform.position = new Vector3(user.transform.position.x,transform.position.y,transform.position.z);
        }
        if (transform.position.y < 0)
        {
            Destroy();
        }

    }
    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("blok collision");

        if (gameObject.GetComponent<MeshRenderer>().sharedMaterial== collision.gameObject.GetComponent<MeshRenderer>().sharedMaterial)
        {
            collision.gameObject.GetComponent<UserScript>().addBlock(gameObject.GetComponent<Block>());
            isCollisionOcurred = true;
            Vector3 pos = new Vector3(collision.transform.position.x, collision.transform.position.y, collision.transform.position.z);
            transform.position = pos + offsetVector * collision.gameObject.GetComponent<UserScript>().getBlockNumber();
        }
        else
        {
            collision.gameObject.GetComponent<UserScript>().removeBlock();
            Destroy();
        }

    }

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("block trigger method");
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
