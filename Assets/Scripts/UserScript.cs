using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserScript : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] float minX = 0f;
    [SerializeField] float maxX = 18f;
    [SerializeField] float screenWidthWithUnits = 18f;
    [SerializeField] Material[] materials;
    [SerializeField] List<Block> blocks;
    Rigidbody rb;
    List<string> colorSet;
    [SerializeField] GlobalVars.Color color;
    int numberOfBlocks=0;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        blocks = new List<Block>();
        int startingColor = Random.Range(0, 3);
        GetComponent<MeshRenderer>().material = materials[startingColor];
        if(startingColor == 0)
        {
            color = GlobalVars.Color.red;
        }
        else if (startingColor == 1)
        {
            color = GlobalVars.Color.green;
        }
        else
        {
            color = GlobalVars.Color.blue;
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 userPos = new Vector2(Mathf.Clamp(transform.position.x, minX, maxX), transform.position.y);
        transform.position = userPos;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.MovePosition(transform.position + Vector3.left * Time.deltaTime * speed);
            //transform.position += Vector3.left * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.MovePosition(transform.position + Vector3.right * Time.deltaTime * speed);

            //transform.position += Vector3.right * speed * Time.deltaTime;
        }
    }
    public void addBlock(Block block)
    {
        Debug.Log("Add Blok");
        blocks.Add(block);
    }
    public void removeBlock()
    {
        Debug.Log("Remove Blok");
        var block = blocks[blocks.Count - 1];
        blocks.RemoveAt(blocks.Count - 1);
        block.GetComponent<Block>().Destroy();
    }
    public int getBlockNumber()
    {
        return blocks.Count;
    }

    public void UpdateBlockColors()
    {
        foreach( Block block in blocks)
        {
            block.SetColor(color,gameObject);
        }
    }
    public void SetColor(GlobalVars.Color color,GameObject gameObjectGate)
    {
        this.color = color;
        gameObject.GetComponent<MeshRenderer>().sharedMaterial = gameObjectGate.GetComponent<MeshRenderer>().sharedMaterial;
    }
    public GlobalVars.Color GetColor()
    {
        return color;
    }
 
}
