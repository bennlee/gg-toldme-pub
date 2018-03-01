using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class VertexColor : MonoBehaviour {

	public List<Color> vertexColors = new List<Color>();
	private List<Color> tempVertexColors = new List<Color>();
	public bool refreshVertexColors = false;

	// Use this for initialization
	void Start () {
		//get the vertex colors
		GetVertexColors();
	}

	private void GetVertexColors() {
		vertexColors.Clear ();
		Mesh mesh = GetComponent<MeshFilter> ().mesh;
		//Vector3[] vertices = mesh.vertices;
		Color[] colors = mesh.colors;
		int i = 0; 
		for (i=0; i < colors.Length; i++) {
			if (ListContainsColor (colors [i]) == false) {
				vertexColors.Add (colors [i]);
			}
		}
		CopyList ();
	}

	private bool ListContainsColor(Color color) {
		return vertexColors.Contains (color);
	}

	void Update() {
		if (refreshVertexColors) {
			GetVertexColors ();
			refreshVertexColors = false;
		}
		UpdateVertexColors ();
	}

	private void CopyList() {
		tempVertexColors.Clear ();
		for (int i = 0; i < vertexColors.Count; i++) {
			tempVertexColors.Add (vertexColors [i]);
		}
	}
	
	public void UpdateVertexColors() {
		if (colorsModified()) {
			Debug.Log ("Modify colors");
			Mesh mesh = GetComponent<MeshFilter> ().mesh;
			Color[] colors = new Color[mesh.vertices.Length];
			for (int i = 0; i < colors.Length; i++) {
				colors [i] = vertexColors [tempVertexColors.IndexOf (mesh.colors [i])];
			}
			mesh.colors = colors;
			CopyList ();
		}
	}

	private bool colorsModified() {
		if (tempVertexColors.Count > 0) {
			for (int i = 0; i < vertexColors.Count; i++) {
				if (vertexColors [i].r != tempVertexColors [i].r || vertexColors [i].g != tempVertexColors [i].g || vertexColors [i].b != tempVertexColors [i].b) {
					//Debug.Log ("gg");
					return true;
				}
			}
		}
		return false;
	}
}
