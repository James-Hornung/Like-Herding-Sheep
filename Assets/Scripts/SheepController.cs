using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepController : MonoBehaviour
{

    public float visual_range;
    public float protected_range;

    public float seperation_factor;
    public float alignment_factor;
    public float cohesion_factor;

    public float minSpeed;
    public float maxSpeed;

    public float margin;
    public float turnFactor;

    private List<GameObject> flock;
    private Vector2 velocity;

    // Start is called before the first frame update
    void Start()
    {
        this.velocity = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
    }

    // Update is called once per frame
    void Update()
    {
        this.align();
        this.cohere();
        this.seperate();
        
        this.limitSpeed();
        this.returnToField();
        
        this.GetComponent<Transform>().position += new Vector3(this.velocity.x, 0, this.velocity.y) * Time.deltaTime;
        this.GetComponent<Transform>().LookAt(this.GetComponent<Transform>().position + new Vector3(this.velocity.x, 0, this.velocity.y));
    }

    private void seperate() {
        Vector2 avoid_dir = Vector2.zero;
        foreach(GameObject otherSheep in this.flock) {
            if (Vector3.Distance(this.GetComponent<Transform>().position, otherSheep.GetComponent<Transform>().position) <= this.protected_range) {
                avoid_dir += this.getPos() - otherSheep.GetComponent<SheepController>().getPos();
            }
        }
        this.velocity += avoid_dir * seperation_factor;
    }

    private void align() {
        Vector2 avg_dir = Vector2.zero;
        int count = 0;
        foreach(GameObject otherSheep in this.flock) {
            if (Vector3.Distance(this.GetComponent<Transform>().position, otherSheep.GetComponent<Transform>().position) <= this.visual_range) {
                avg_dir += otherSheep.GetComponent<SheepController>().getVelocity();
                count++;
            }
        }
        avg_dir /= count;
        this.velocity += (avg_dir - this.velocity) * alignment_factor;
    }

    private void cohere() {
        Vector2 avg_pos = Vector2.zero;
        int count = 0;
        foreach(GameObject otherSheep in this.flock) {
            if (Vector3.Distance(this.GetComponent<Transform>().position, otherSheep.GetComponent<Transform>().position) <= this.visual_range) {
                avg_pos += otherSheep.GetComponent<SheepController>().getPos();
                count++;
            }
        }

        avg_pos /= count;
        this.velocity += (avg_pos - this.getPos()) * this.cohesion_factor;
    }

    private void limitSpeed() {
        if (this.velocity.magnitude < this.minSpeed) {
            this.velocity = this.velocity.normalized * this.minSpeed;
        }
        Vector2.ClampMagnitude(this.velocity, this.maxSpeed);
    }

    private void returnToField() {
        Vector2 turnVector = Vector2.zero;
        if (this.getPos().x < -this.margin) {
            turnVector.x = this.turnFactor;
        }
        if (this.getPos().x > this.margin) {
            turnVector.x = -this.turnFactor;
        }
        if (this.getPos().y < -this.margin) {
            turnVector.y = this.turnFactor;
        }
        if (this.getPos().y > this.margin) {
            turnVector.y = -this.turnFactor;
        }
        this.velocity += turnVector;
    }

    public Vector2 getVelocity() {
        return this.velocity;
    }

    public Vector2 getPos() {
        return new Vector2(this.GetComponent<Transform>().position.x, this.GetComponent<Transform>().position.z);
    }

    public void setFlock(List<GameObject> flock) {
        this.flock = flock;
    }
}
