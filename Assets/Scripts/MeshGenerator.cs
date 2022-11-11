using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour {

    public float width = 1;
    public float height = 1;
    public Material mat;
    private void Start() {
        Mesh mesh = new Mesh();
        Vector3[] vertices = new Vector3[3];
        vertices[0] = new Vector3(-width/2, -height/2);
        vertices[1] = new Vector3(width/2, -height/2);
        vertices[2] = new Vector3(0, height/2);

        mesh.vertices = vertices;
        mesh.triangles = new int[] {0, 1, 2};

        GetComponent<MeshRenderer>().material = mat;

        GetComponent<MeshFilter>().mesh = mesh;
    }
}
