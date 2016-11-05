using UnityEngine;
using System.Collections;

public class CityBlockView : MonoBehaviour {

    private Transform selector;
    private GameManager gameManager;
    private SpriteRenderer renderer;
    public CityBlock cityBlock { get; set; }

	void Start () {
        this.selector = transform.GetChild(0);
        this.renderer = GetComponent<SpriteRenderer>();
        this.gameManager = Object.FindObjectOfType<GameManager>();
	}
	
	void Update () {
        if (cityBlock != null) {
            if (cityBlock.owner == null) {
                this.renderer.color = Color.white;
            } else {
                this.renderer.color = ColorConverter.convert(cityBlock.owner.colour);
            }
        }
    }

    void OnMouseDown() {
        if (Input.GetMouseButtonDown(0)) {
            gameManager.setSelectedCityBlock(this);
        } else if (Input.GetMouseButtonDown(1)) {
            Debug.Log("Right click on : " + cityBlock.name);
        }
    }

    public void showSelector(bool show) {
        if (show) {
            this.selector.gameObject.SetActive(true);
        } else {
            this.selector.gameObject.SetActive(false);
        }
    }
}
