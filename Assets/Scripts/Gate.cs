using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] string color;
    [SerializeField] float speed = 7f;
    GameObject user;
    // Start is called before the first frame update
    void Start()
    {
        user = GameObject.Find("User");

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        if (transform.position.y < 0)
        {
            Destroy();
        }
    }

    void OnTriggerEnter(Collider collider)
    {
       // Debug.Log("gate Tigger enter");
       if(collider.name == "User")
        {
            collider.GetComponent<MeshRenderer>().sharedMaterial = gameObject.GetComponent<MeshRenderer>().sharedMaterial;
            collider.gameObject.GetComponent<UserScript>().UpdateBlockColors();
        }

        
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
