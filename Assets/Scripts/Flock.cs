using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour {
    Boid[] boids;
    Vector2 screenHalfSize;

    // boid params
    public float speed = 10;
    [Range(0f, 100f)]
    public float sepFactor = 1;
    [Range(0f, 100f)]
    public float aliFactor = 1/8;
    [Range(0f, 100f)]
    public float cohFactor = 1/100;
    public float viewAngle = 270f;
    [Range(0f, 10f)]
    public float viewRadius = 2f;

    private void Start() {
        boids = Object.FindObjectsOfType<Boid>();

        screenHalfSize = new (
            x: Camera.main.aspect * Camera.main.orthographicSize,
            y: Camera.main.orthographicSize
        );

    }

    private void Update() {
        MoveBoids();
    }

    private void MoveBoids() {
        // forces from seperation, alignment and cohesion
        Vector3 sepForce, aliForce, cohForce, totForce;

        foreach (Boid boid in boids) {

            if (!boid.isActive)
                continue;

            DrawLines(boid);

            totForce = Vector3.zero;

            sepForce = BoidSeparation(boid);
            aliForce = BoidAlignment(boid);
            cohForce = BoidCohesion(boid);


            totForce = sepFactor * sepForce + aliFactor * aliForce + cohFactor * cohForce;
            boid.ApplyForce(totForce, speed);

            print(totForce);

            if (boid.drawRays) {
                Vector3 pos = boid.GetPosition();
                Debug.DrawRay(pos, totForce, Color.red);
            }
        }
    }

    private void DrawLines(Boid boid) {
        // return immediately if draw rays is off
        if (!boid.drawRays)
            return;

        Vector3 pos = boid.GetPosition();
        Vector3 neighbourPos;
        float distance;

        foreach (Boid neighbour in boids) {
            // relative position of the neigbour
            neighbourPos = neighbour.GetPosition() - pos;
            distance = neighbourPos.magnitude;

            // if neighbour is the boid, continue to next neighbour
            if ((!neighbour.Equals(boid)) && (distance < viewRadius)){
                Debug.DrawRay(pos, neighbourPos);
            }
        }

    }

    // finds the separation force
    private Vector3 BoidSeparation(Boid boid) {
        // the force vector accounting for boid separation
        Vector3 force = Vector3.zero;
        Vector3 pos = boid.GetPosition();

        Vector3 neighbourPos;
        float distance;

        foreach (Boid neighbour in boids) {
            // relative position of the neigbour
            neighbourPos = neighbour.GetPosition() - pos;
            distance = neighbourPos.magnitude;

            // if neighbour is the boid, continue to next neighbour
            if ((!neighbour.Equals(boid)) && (distance < viewRadius)){
                // move away from neighbour
                force -= neighbourPos.normalized;
            }
        }

        // separation from walls
        // top wall
        if (Mathf.Abs((screenHalfSize.y - pos.y)) < viewRadius) {
            force -= Vector3.up;
        }
        // bottom wall
        if (Mathf.Abs((-screenHalfSize.y - pos.y)) < viewRadius) {
            force -= Vector3.down;
        }
        // right wall
        if (Mathf.Abs((screenHalfSize.x - pos.x)) < viewRadius) {
            force -= Vector3.right;
        }
        // left wall
        if (Mathf.Abs((-screenHalfSize.x - pos.x)) < viewRadius) {
            force -= Vector3.left;
        }

        return force;
    }

    private Vector3 BoidAlignment(Boid boid) {
        Vector3 perceivedVel = Vector3.zero;
        Vector3 pos = boid.GetPosition();

        Vector3 neighbourPos;
        float distance;

        foreach (Boid neighbour in boids) {
            // relative position of the neighbour
            neighbourPos = neighbour.GetPosition() - pos;
            distance = neighbourPos.magnitude;

            // if neighbour is not the current boid, and neighbour is within
            // view radius, update alignment velocity
            if (!neighbour.Equals(boid) && (distance < viewRadius)) {
                perceivedVel += neighbour.GetVelocity();
            }
        }

        // average the perceived velocity
        perceivedVel = perceivedVel / (boids.Length + 1);

        // get the force
        Vector3 force = perceivedVel - boid.GetVelocity();

        return force;
    }

    private Vector3 BoidCohesion(Boid boid) {

        // include the current boid
        float neighbourCount = 1;
        Vector3 pos = boid.GetPosition();
        Vector3 centrePos = Vector3.zero;

        Vector3 neighbourPos;
        float distance;

        foreach (Boid neighbour in boids) {
            // relative position of the neighbour
            neighbourPos = neighbour.GetPosition() - pos;
            distance = neighbourPos.magnitude;

            // if neighbour is not the current boid, and neighbour is within
            // view radius, update central position
            if (!neighbour.Equals(boid) && (distance < viewRadius)) {
                centrePos += neighbourPos;
                neighbourCount += 1;
            }

            // get 'centre of mass'
            centrePos = centrePos / neighbourCount;
        }

        Vector3 force = centrePos;

        return force;
    }

}
