using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LerpAlpha : MonoBehaviour {

	public float speed = 2.5f;
	
	private float intendedAlpha;
	private Graphic image;
	
	public float IntendedAlpha {
		get { return intendedAlpha; }
		set { intendedAlpha = value; }
	}

	void Awake () {
		image = GetComponent<Graphic>();
		intendedAlpha = image.color.a;
	}
	
	void Update() {
		Color current = GetColor ();
		Color c = new Color(current.r, current.g, current.b, Mathf.Lerp(current.a, intendedAlpha, Time.deltaTime * speed));
		SetColor (c);
	}

	public void SetAlpha(float value) {
		Color col = image.color;
		col.a = value;
		image.color = col;
	}

	public Color GetColor() {
		return image.color;
	}

	public void SetColor (Color color) {
		image.color = color;
	}
}
