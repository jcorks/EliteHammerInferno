using UnityEngine;
using System.Collections;

/* A path between nodes */
public class Pathway : MonoBehaviour {

	public Texture tex;
	private Color cachedColor;

	public Vector3 begin;
	public Vector3 end;

	public void setPath(Vector3 begin_, Vector3 end_) {

		begin = begin_;
		end = end_;

		/*
		Vector3[] verts = new Vector3[4];
		Vector2[] texCoords = new Vector2[4];
		int[] newTris = new int[6];

		Vector3 offset = new Vector3 (.15f, 0.0f, 0.1f);
		float angle = Vector3.Angle (begin, end);
		if (begin.x > end.x && begin.z < end.z) {
			angle += 90f;
		} else if (begin.x > end.x && begin.z > end.z) {
			angle += 180f;
		} else if (begin.x < end.x && begin.z > end.z) {
			angle += 270f;
		}


		offset = Quaternion.Euler (0, angle, 0) *offset;

		verts [0] = begin;
		verts [1] = begin + offset;
		verts [2] = end;
		verts [3] = end + offset;

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
		*/
		GetComponent<LineRenderer> ().SetPosition (0, begin);
		GetComponent<LineRenderer> ().SetPosition (1, end);
	}

	public void setSelected(bool t) {
		if (t) {
			cachedColor = renderer.material.color;
			renderer.material.color = new Color (255, 255, 0, 255);
		} else {
			renderer.material.color = cachedColor;
		}	
	}

	// Use this for initialization
	void Start () {
		renderer.material.mainTexture = tex;

	
	}
	
	// Update is called once per frame
	void Update () {

	}
}
