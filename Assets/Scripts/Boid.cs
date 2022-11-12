using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour {
    Vector3 size;
    Vector2 screenHalfSizeWorldUnits;


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
    //     // the position of the 'head' vertex of the boid relative to transform.position
    //     Vector3 headPos = transform.position + transform.right * size.x / 2 + transform.up * size.y / 2;
    //     Debug.DrawRay(
    //         start: headPos,
    //         dir: transform.up,
    //         color: Color.red
    //     );

    //     transform.Translate(Vector3.up * speed * Time.deltaTime, Space.Self);

    //     if (Mathf.Abs(transform.position.x) > screenHalfSizeWorldUnits.x) {
    //         transform.position = new Vector3(-1 * Mathf.Sign(transform.position.x) * screenHalfSizeWorldUnits.x, transform.position.y);
    //     }
    //     if (Mathf.Abs(transform.position.y) > screenHalfSizeWorldUnits.y) {
    //         transform.position = new Vector3(transform.position.x, -1 * Mathf.Sign(transform.position.y) * screenHalfSizeWorldUnits.y);
    //     }
    }

    public Vector3 GetPosition() {
        return transform.position;
    }

    public void ApplyForce(Vector3 force, float speed) {
        Vector3 velocity = transform.up * speed + force * Time.deltaTime;
        transform.up = velocity;
        transform.Translate(velocity * Time.deltaTime, Space.World);
        print(velocity);

        if (Mathf.Abs(transform.position.x) > screenHalfSizeWorldUnits.x) {
            transform.position = new Vector3(-1 * Mathf.Sign(transform.position.x) * screenHalfSizeWorldUnits.x, transform.position.y);
        }
        if (Mathf.Abs(transform.position.y) > screenHalfSizeWorldUnits.y) {
            transform.position = new Vector3(transform.position.x, -1 * Mathf.Sign(transform.position.y) * screenHalfSizeWorldUnits.y);
        }
    }

}
