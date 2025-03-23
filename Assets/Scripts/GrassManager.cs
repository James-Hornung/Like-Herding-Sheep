using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassManager : MonoBehaviour
{

    public GameObject grassPrefab;
    public int maxCount;
    public float spawnRadius;

    private List<Transform> grass;

    // Start is called before the first frame update
    void Start()
    {
        this.grass = new List<Transform>();

        for (int i = 0; i < this.maxCount; i++) {
            this.createNewGrass();
        }
    }

    private void createNewGrass() {
        Transform gras = Instantiate(this.grassPrefab).GetComponent<Transform>();
        gras.position = new Vector3(Random.Range(-this.spawnRadius, this.spawnRadius), 0, Random.Range(-this.spawnRadius, this.spawnRadius));
        this.grass.Add(gras);
    }

    public Transform getGrass() {
        Transform gras = this.grass[0];
        this.grass.RemoveAt(0);
        return gras;
    }

    public void grassDeleted() {
        this.createNewGrass();
    }
}
