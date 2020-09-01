using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserScript : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] float minX = 1.5f;
    [SerializeField] float maxX = 16.5f;
    [SerializeField] float screenWidthWithUnits = 18f;
    [SerializeField] string color;
    [SerializeField] Material[] materials;
    [SerializeField] List<Block> blocks;
    Rigidbody rb;
    List<string> colorSet;
    int numberOfBlocks=0;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        blocks = new List<Block>();
        int startingColor = Random.Range(0, 3);
        GetComponent<MeshRenderer>().material = materials[startingColor];

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
            block.GetComponent<MeshRenderer>().sharedMaterial = gameObject.GetComponent<MeshRenderer>().sharedMaterial;
        }
    }

}
