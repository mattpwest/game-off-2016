using UnityEngine;
using System.Collections;

public class CityBlockView : MonoBehaviour {

    private SpriteRenderer renderer;
    public CityBlock cityBlock { get; set; }

	void Start () {
        renderer = GetComponent<SpriteRenderer>();
	}
	
	void Update () {
        if (cityBlock != null) {
            if (cityBlock.owner == null) {
                renderer.color = Color.white;
            } else {
                renderer.color = ColorConverter.convert(cityBlock.owner.colour);
            }
        }
	}
}
