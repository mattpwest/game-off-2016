using UnityEngine;
using System.Collections;

public class CityBlock : MonoBehaviour {

    public Transform owner = null;
    public string name = "Default";
    public int income = 100000;

    private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
        this.spriteRenderer = GetComponentInParent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (this.owner != null) {
            Player player = this.owner.GetComponent<Player>();
            spriteRenderer.color = player.color;
        }
	}


}
