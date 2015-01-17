using UnityEngine;
using System.Collections;

/* A path between nodes */
public class Pathway : MonoBehaviour {

	public Texture tex;

	public Vector3 begin;
	public Vector3 end;

	public void setPath(Vector3 begin_, Vector3 end_) {

		begin = begin_;
		end = end_;

		Vector3[] verts = new Vector3[4];
		Vector2[] texCoords = new Vector2[4];
		int[] newTris = new int[6];


		verts [0] = begin;
		verts [1] = begin + (new Vector3 (.1f, 0.0f, 0.1f));
		verts [2] = end;
		verts [3] = end + (new Vector3 (.1f, 0.0f, 0.1f));

		texCoords[0] = new Vector2 (0, 0);
		texCoords[1] = new Vector2 (1, 0);
		texCoords[2] = new Vector2 (1, 1);
		texCoords[3] = new Vector2 (0, 1);

		newTris [0] = 0;
		newTris [1] = 3;
		newTris [2] = 2;

		newTris [3] = 0;
		newTris [4] = 1;
		newTris [5] = 3;


		Mesh mesh = new Mesh ();
		GetComponent<MeshFilter> ().mesh = mesh;
		mesh.vertices = verts;
		mesh.uv = texCoords;
		mesh.triangles = newTris;
	}
	// Use this for initialization
	void Start () {

	
	}
	
	// Update is called once per frame
	void Update () {

	}
}
