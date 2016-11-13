using System;
using UnityEngine;

public class UICityBlock : MonoBehaviour {

    private UIState uiState;

    private Transform selector;
    private Transform squadIndicator;
    private Transform moveIndicator;
    private SpriteRenderer spriteRenderer;

    private bool showSelector = false;
    private bool showMoveSelector = false;
    private bool showSquadIndicator = false;
    private Color squadColor = Color.white;

    public CityBlock cityBlock { get; set; }

	void Start () {
        this.uiState = UIState.getInstance();
        
        this.selector = transform.FindChild("TileSelector");
        this.squadIndicator = transform.FindChild("SquadIndicator");
        this.moveIndicator = transform.FindChild("MoveSelector");

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

        if (this.moveIndicator.gameObject.activeSelf != this.showMoveSelector) {
            this.moveIndicator.gameObject.SetActive(this.showMoveSelector);
        }

        if (this.showMoveSelector && uiState.getActiveSquad() != null) {
            Squad squad = uiState.getActiveSquad();
            if (squad.command != null && squad.command.GetType() == typeof(MoveCommand)) {
                MoveCommand cmd = (MoveCommand) squad.command;
                if (cmd.x == cityBlock.x && cmd.y == cityBlock.y) {
                    this.moveIndicator.GetComponent<TweenRotation>().enabled = true;
                } else {
                    this.moveIndicator.GetComponent<TweenRotation>().enabled = false;
                    this.moveIndicator.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
                }
            } else {
                this.moveIndicator.GetComponent<TweenRotation>().enabled = false;
                this.moveIndicator.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
            }
        }
    }

    void OnMouseOver() {
        if (Input.GetMouseButtonDown(0)) {
            uiState.setActiveBlock(this.cityBlock);
        } else if (Input.GetMouseButtonDown(1)) {
            if (City.getInstance().issueMoveOrder(uiState.getActiveSquad(), this.cityBlock)) {
                Debug.Log("Movement order issued!");
            } else {
                Debug.Log("Movement order failed!");
            }
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

    internal void setShowMoveIndicator(bool show) {
        this.showMoveSelector = show;
    }
}
