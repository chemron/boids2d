using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour {
    Vector3 size;
    Vector2 screenHalfSizeWorldUnits;

    private Vector3 velocity;

    public bool isActive = true;
    public bool drawRays = true;

    // Start is called before the first frame update
    private void Start() {
        size = GetComponent<Renderer>().bounds.size;
        Vector2 halfPlayerSize = transform.localScale / 2f;

        screenHalfSizeWorldUnits = new (
            x: Camera.main.aspect * Camera.main.orthographicSize + halfPlayerSize.x,
            y: Camera.main.orthographicSize + halfPlayerSize.y);

    }

    // Update is called once per frame
    private void Update() {
    }

    public Vector3 GetPosition() {
        return transform.position;
    }

    public Vector3 GetVelocity() {
        if (!isActive) {
            return transform.up;
        }
        else return velocity;
    }

    public void ApplyForce(Vector3 force, float speed) {
        if (!isActive) {
            force = Vector3.zero;
        }

        velocity = transform.up + force * Time.deltaTime;
        transform.up = velocity;
        transform.Translate(transform.up * speed * Time.deltaTime, Space.World);

        if (Mathf.Abs(transform.position.x) > screenHalfSizeWorldUnits.x) {
            transform.position = new Vector3(-1 * Mathf.Sign(transform.position.x) * screenHalfSizeWorldUnits.x, transform.position.y);
        }
        if (Mathf.Abs(transform.position.y) > screenHalfSizeWorldUnits.y) {
            transform.position = new Vector3(transform.position.x, -1 * Mathf.Sign(transform.position.y) * screenHalfSizeWorldUnits.y);
        }
    }

}
