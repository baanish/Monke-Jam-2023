using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> blocks;

    [SerializeField]
    public List<GameObject> blockPrefabs;

    [SerializeField]
    public int blockCount = 5;

    [SerializeField]
    public float blockLength = 150f;

    [SerializeField]
    public GameObject player;

    [SerializeField]
    public int deletedBlock = 0;
    // Start is called before the first frame update
    void Start()
    {

        // set a random seed for the random block spawning
        Random.InitState((int)System.DateTime.Now.Ticks);

        // spawn the initial blockCount blocks. Starting at -60, 0, 0
        for (int i = 0; i < blockCount; i++)
        {
            GameObject block = Instantiate(blockPrefabs[Random.Range(0, blockPrefabs.Count)], new Vector3(-60f + (blockLength * i), 0f, 0f), Quaternion.identity);
            blocks.Add(block);
        }

    }

    // Update is called once per frame
    void Update()
    {
        // check which block the player is on
        int playerBlock = (int)Mathf.Floor(player.transform.position.x / blockLength);

        // if the player is on the 2rd block from the end, spawn a new block, and remove the first block
        if (playerBlock == (deletedBlock + blocks.Count - 2))
        {
            GameObject block = Instantiate(blockPrefabs[Random.Range(0, blockPrefabs.Count)], new Vector3(-60f + (blockLength * (deletedBlock + blocks.Count)), 0f, 0f), Quaternion.identity);
            blocks.Add(block);
            Destroy(blocks[0]);
            blocks.RemoveAt(0);
            deletedBlock++;
        }
    }
}
