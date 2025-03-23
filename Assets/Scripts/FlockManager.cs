using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour
{
    public GameObject sheepPrefab;
    public int flockSize;
    public float spawnRadius;

    private List<GameObject> flock;

    // Start is called before the first frame update
    void Start()
    {
        this.flock = new List<GameObject>();
        for (int i = 0; i < this.flockSize; i++) {
            GameObject sheep = Instantiate(this.sheepPrefab);
            sheep.GetComponent<Transform>().position = new Vector3(Random.Range(-this.spawnRadius, this.spawnRadius), 0.5f, Random.Range(-this.spawnRadius, this.spawnRadius));
            this.flock.Add(sheep);
        }

        foreach(GameObject sheep in this.flock) {
            sheep.SendMessage("setFlockList", this.flock);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
