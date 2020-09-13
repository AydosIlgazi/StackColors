using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] float speed = 20f;
    GameObject user;
    float offset = 1f;
    Rigidbody rb;
    Vector3 offsetVector;
    bool isCollisionOcurred = false;
    [SerializeField] GlobalVars.Color color;
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
            transform.Translate(Vector3.back * speed * Time.deltaTime);
        }
        else
        {
           
           transform.position = new Vector3(user.transform.position.x,transform.position.y,transform.position.z);
        }
        if (transform.position.z< -20)
        {
            Destroy(gameObject);
        }

    }
    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("blok collision");

        if (color== collision.gameObject.GetComponent<UserScript>().GetColor())
        {
            collision.gameObject.GetComponent<UserScript>().addBlock(gameObject.GetComponent<Block>());
            isCollisionOcurred = true;
            Vector3 pos = new Vector3(collision.transform.position.x, collision.transform.position.y, collision.transform.position.z);
            pos = pos + offsetVector * collision.gameObject.GetComponent<UserScript>().getBlockNumber();
            float animationTime = collision.gameObject.GetComponent<UserScript>().getBlockNumber() * 0.1f;
            StartCoroutine(BlockStackAnimation(pos, animationTime,collision.gameObject));
        }
        else
        {
            if (collision.gameObject.name == "User")
            {
                collision.gameObject.GetComponent<UserScript>().removeBlock();
                Destroy(gameObject);
            }
            
        }

    }
    IEnumerator BlockStackAnimation(Vector3 target, float animationTime,GameObject user)
    {
        Vector3 current = gameObject.transform.position;

        float elapsedTime = 0;

        while (elapsedTime < animationTime)
        {
            target.x = user.transform.position.x;
            //rb.MovePosition(Vector3.Lerp(current, target, (elapsedTime / animationTime)));
            transform.position = Vector3.Lerp(current, target, (elapsedTime / animationTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }


        yield return null;

    }
    IEnumerator BlockDestroyAnimation(float animationTime)
    {
        Vector3 current = gameObject.transform.position;

        float elapsedTime = 0;
        Vector3 scale = gameObject.transform.localScale;
        while (elapsedTime < animationTime && scale.x>0 && scale.y>0 && scale.z>0)
        {
            gameObject.transform.localScale -= new Vector3(0.15f, 0.05f, 0.05f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }


        yield return null;
        Destroy(gameObject);
    }



    public void DestroyWithAnimation()
    {
        StartCoroutine(BlockDestroyAnimation(0.3f));
        
    }
    public void SetColor(GlobalVars.Color color,GameObject gameObjectUser)
    {
        this.color = color;
        gameObject.GetComponent<MeshRenderer>().sharedMaterial = gameObjectUser.GetComponent<MeshRenderer>().sharedMaterial;
    }
    public GlobalVars.Color GetColor()
    {
        return color;
    }
}
