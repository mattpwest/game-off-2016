using UnityEngine;

public class UICityBlock : MonoBehaviour {

    private UIState uiState;

    private Transform selector;
    private Transform squadIndicator;
    private SpriteRenderer spriteRenderer;

    private bool showSelector = false;
    private bool showSquadIndicator = false;
    private Color squadColor = Color.white;

    public CityBlock cityBlock { get; set; }

	void Start () {
        this.uiState = UIState.getInstance();
        
        this.selector = transform.FindChild("TileSelector");
        this.squadIndicator = transform.FindChild("SquadIndicator");

        this.spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	void Update () {
        if (cityBlock != null) {
            if (cityBlock.owner == null) {
                this.spriteRenderer.color = Color.white;
            } else {
                Color newColor = ColorConverter.convert(cityBlock.owner.colour);
                newColor = newColor * 0.75f;
                newColor.a = 1.0f;
                this.spriteRenderer.color = newColor;
            }

            SpriteRenderer theRenderer = squadIndicator.GetComponent<SpriteRenderer>();
            theRenderer.material.SetColor("_Color1out", squadColor);
        }

        if (this.selector.gameObject.activeSelf != this.showSelector) {
            this.selector.gameObject.SetActive(this.showSelector);
        }

        if (this.squadIndicator.gameObject.activeSelf != this.showSquadIndicator) {
            this.squadIndicator.gameObject.SetActive(this.showSquadIndicator);
        }
    }

    void OnMouseDown() {
        if (Input.GetMouseButtonDown(0)) {
            uiState.setActiveBlock(this.cityBlock);
        } else if (Input.GetMouseButtonDown(1)) {
            Debug.Log("Right click on : " + cityBlock.name);
        }
    }

    public void setShowSelector(bool show) {
        this.showSelector = show;
    }

    public void setShowSquadIndicator(bool show) {
        this.showSquadIndicator = show;
    }

    public void setSquadIndicatorColour(Color color) {
        this.squadColor = color;
    }
}
