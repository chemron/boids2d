using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour {
    Boid[] boids;

    // boid params
    public float speed = 10;
    [Range(0f, 100f)]
    public float sepFactor = 1;
    [Range(0f, 100f)]
    public float aliFactor = 1;
    [Range(0f, 100f)]
    public float cohFactor = 1;
    public float viewAngle = 270f;
    public float viewRadius = 2f;

    private void Start() {
        boids = Object.FindObjectsOfType<Boid>();
    }


    private void Update() {
        MoveBoids();
    }

    private void MoveBoids() {
        // forces from seperation, alignment and cohesion
        Vector3 sepForce, aliForce, cohForce, totForce;

        foreach (Boid boid in boids) {
            totForce = Vector3.zero;

            sepForce = BoidSeparation(boid);
            aliForce = BoidAlignment(boid);
            cohForce = BoidCohesion(boid);


            totForce = sepFactor * sepForce + aliFactor * aliForce + cohFactor * cohForce;

            boid.ApplyForce(totForce, speed);
        }
    }

    // finds the separation force
    private Vector3 BoidSeparation(Boid boid) {
        // position of current boid
        Vector3 pos = boid.GetComponent<Transform>().position;
        // the force vector accounting for boid separation
        Vector3 force = Vector3.zero;

        Vector3 neighbourPos;
        float distance;

        foreach (Boid neighbour in boids) {
            // relative position of the neigbour
            neighbourPos = neighbour.GetPosition() - boid.GetPosition();
            distance = neighbourPos.magnitude;

            // if neighbour is the boid, continue to next neighbour
            if ((!neighbour.Equals(boid)) && (distance < viewRadius)){
                force -= neighbourPos;
            }
        }

        print(force);
        return force;
    }

    private Vector3 BoidAlignment(Boid boid) {
        Vector3 force = Vector3.zero;
        return force;
    }

    private Vector3 BoidCohesion(Boid boid) {
        Vector3 force = Vector3.zero;
        return force;
    }

}
