using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class UISquadsPanel : MonoBehaviour {

    private UIState uiState;
    private List<Squad> activeSquads;
    public Transform squadSelector;

	void Start () {
        uiState = UIState.getInstance();
        uiState.BlockChanged += OnBlockChanged;

        if (uiState.getActiveBlock() == null) {
            gameObject.SetActive(false);
        }
    }

    private void OnBlockChanged(object sender, UIState.BlockChangedEventArgs e) {
        if (e.block != null) {
            activeSquads = getActivePlayerSquadsInBlock();
            if (activeSquads.Count > 0) {
                activateSquadSelectButtons();

                this.gameObject.SetActive(true);
            } else {
                this.gameObject.SetActive(false);
            }
        } else {
            this.gameObject.SetActive(false);
        }
    }
    
    void Update () {	
	}

    private List<Squad> getActivePlayerSquadsInBlock() {
        return City.getInstance().getCurrentPlayerSquads(uiState.getActiveBlock());
    }

    private void activateSquadSelectButtons() {
        for (int i = 0; i < 5; i++) {
            Transform child = (Transform)transform.FindChild("Squad" + i);
            child.gameObject.SetActive(false);
        }

        for (int i = 0; i < activeSquads.Count; i++) {
            Squad squad = activeSquads[i];
            Transform child = (Transform) transform.FindChild("Squad" + i);
            if (child != null) {
                Text buttonLabel = child.GetComponentInChildren<Text>();
                buttonLabel.text = squad.name;
                child.gameObject.SetActive(true);
            }
        }
    }

    public void selectSquad0() {
        if (activeSquads.Count > 0) {
            Debug.Log("Activated squad 0");
            uiState.setActiveSquad(activeSquads[0]);
        } else {
            Debug.Log("Squad 0 failed to activate!");
        }
    }

    public void selectSquad1() {
        if (activeSquads.Count > 1) {
            uiState.setActiveSquad(activeSquads[1]);
        }
    }

    public void selectSquad2() {
        if (activeSquads.Count > 2) {
            uiState.setActiveSquad(activeSquads[2]);
        }
    }

    public void selectSquad3() {
        if (activeSquads.Count > 3) {
            uiState.setActiveSquad(activeSquads[3]);
        }
    }

    public void selectSquad4() {
        if (activeSquads.Count > 4) {
            uiState.setActiveSquad(activeSquads[4]);
        }
    }
}
