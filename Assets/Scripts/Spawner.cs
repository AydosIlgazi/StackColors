using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject[] blocks;
    [SerializeField] GameObject[] gates;
    [SerializeField] UserScript user;
    private float time = 0.0f;
    public float interpolationPeriod = 5f;
    int gateOrBlock = 0;
    Vector3 firstIndex;
    Vector3 secondIndex;
    Vector3 thirdIndex;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", 2.0f, 5.0f);
        firstIndex = new Vector3(3f, 28f);
        secondIndex = new Vector3(9f, 28f);
        thirdIndex = new Vector3(15f, 28f);
    }

    // Update is called once per frame
    void Update()
    {


    }
    void Spawn()
    {
        if (gateOrBlock < 5) //spawn block
        {
            gateOrBlock = Random.Range(0, 7);
            StartCoroutine(SpawnBlocks(Random.Range(6, 9)));
        }
        else //spawn gate
        {
            gateOrBlock = Random.Range(0, 7);
            StartCoroutine(SpawnGates());
            
        }
    }
    Block GetBlokWithUserMaterial()
    {
        var userColor = user.GetColor();
        Block block;
        if(userColor == blocks[0].GetComponent<Block>().GetColor())
        {
            block = blocks[0].GetComponent<Block>();
        }
        else if (userColor == blocks[1].GetComponent<Block>().GetColor())
        {
            block = blocks[1].GetComponent<Block>();
        }
        else
        {
            block = blocks[2].GetComponent<Block>();

        }
        return block;
    }
    IEnumerator SpawnBlocks(int numberOfBlocks)
    {
        int i = 0;
        int columnCount = Random.Range(0, 6);
        Block block = GetBlokWithUserMaterial();
        Block firstIndexBlock = blocks[Random.Range(0, 3)].GetComponent<Block>();
        Block secondIndexBlock = blocks[Random.Range(0, 3)].GetComponent<Block>();
        Block thirdIndexBlock = blocks[Random.Range(0, 3)].GetComponent<Block>();
        int oneColumnIndex = Random.Range(0, 3);
        int twoColumnIndex = Random.Range(0, 2);
        while (i < numberOfBlocks)
        {
            if(columnCount >= 3) //3 column blocks
            {
                if (columnCount==5)//instantiate first index
                {
                    Instantiate(block, firstIndex, Quaternion.identity);
                    Instantiate(secondIndexBlock, secondIndex, Quaternion.identity);
                    Instantiate(thirdIndexBlock, thirdIndex, Quaternion.identity);
                }
                else if (columnCount==4)//instantiate second index
                {
                    Instantiate(firstIndexBlock, firstIndex, Quaternion.identity);
                    Instantiate(block, secondIndex, Quaternion.identity);
                    Instantiate(thirdIndexBlock, thirdIndex, Quaternion.identity);
                }
                else//instantiate third index
                {
                    Instantiate(firstIndexBlock, firstIndex, Quaternion.identity);
                    Instantiate(secondIndexBlock, secondIndex, Quaternion.identity);
                    Instantiate(block, thirdIndex, Quaternion.identity);

                }
            }
            else if(columnCount >= 1) //2
            {
                if (oneColumnIndex == 0) // 1 and 2
                {
                    if (twoColumnIndex == 0)
                    {
                        Instantiate(block, firstIndex, Quaternion.identity);
                        Instantiate(secondIndexBlock, secondIndex, Quaternion.identity);

                    }
                    else
                    {
                        Instantiate(firstIndexBlock, firstIndex, Quaternion.identity);
                        Instantiate(block, secondIndex, Quaternion.identity);

                    }
                }
                else if (oneColumnIndex == 1)  //1 and 3
                {
                    if (twoColumnIndex == 0)
                    {
                        Instantiate(block, firstIndex, Quaternion.identity);
                        Instantiate(thirdIndexBlock, thirdIndex, Quaternion.identity);

                    }
                    else
                    {
                        Instantiate(firstIndexBlock, firstIndex, Quaternion.identity);
                        Instantiate(block, thirdIndex, Quaternion.identity);

                    }

                }
                else  //2 and 3

                {
                    if (twoColumnIndex == 0)
                    {
                        Instantiate(block, secondIndex, Quaternion.identity);
                        Instantiate(thirdIndexBlock, thirdIndex, Quaternion.identity);

                    }
                    else
                    {
                        Instantiate(secondIndexBlock, secondIndex, Quaternion.identity);
                        Instantiate(block, thirdIndex, Quaternion.identity);

                    }

                }
            }
            else //1
            {
                if(oneColumnIndex == 0)
                {
                    Instantiate(block, firstIndex, Quaternion.identity);
                }
                else if (oneColumnIndex == 1)
                {
                    Instantiate(block, secondIndex, Quaternion.identity);

                }
                else
                {
                    Instantiate(block, thirdIndex, Quaternion.identity);

                }
            }
            //do stuff
            yield return new WaitForSeconds(0.3f); //Wait 1 Frame
            //yield return new WaitForSeconds(1.0f) //wait 1 second per interval
            i++;
        }
    }

    IEnumerator SpawnGates()
    {
        Gate gate = gates[Random.Range(0, 3)].GetComponent<Gate>();
        Instantiate(gate, secondIndex, Quaternion.identity);
        yield return new WaitForSeconds(Random.Range(0f, 1.5f));
        SpawnBlocks(Random.Range(5, 7));
    }
}
