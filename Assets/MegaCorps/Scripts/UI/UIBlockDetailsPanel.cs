using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class UIBlockDetailsPanel : MonoBehaviour {

    private UIState uiState;
    private Text name;
    private Text population;
    private Text chipped;
    private Text taxRate;
    private Text wealthLevel;
    private Text happiness;

    void Start () {
        uiState = UIState.getInstance();
        uiState.BlockChanged += OnBlockChanged;

        this.name = transform.FindChild("Name").GetComponent<Text>();
        this.population = transform.FindChild("Population").GetComponent<Text>();
        this.chipped = transform.FindChild("Chipped").GetComponent<Text>();
        this.taxRate = transform.FindChild("TaxRate").GetComponent<Text>();
        this.wealthLevel = transform.FindChild("WealthLevel").GetComponent<Text>();
        this.happiness = transform.FindChild("Happiness").GetComponent<Text>();

        this.gameObject.SetActive(false);
    }

    void Update () {
	}

    private void OnBlockChanged(object sender, UIState.BlockChangedEventArgs args) {
        if (args.block == null) {
            this.gameObject.SetActive(false);
        } else {
            this.gameObject.SetActive(true);

            CityBlock block = args.block;
            name.text = block.name;
            population.text = "" + block.population;
            wealthLevel.text = block.wealthLevel.name;
            chipped.text = block.chipAdoption * 100 + " %";
            happiness.text = block.getHappiness().name;
            taxRate.text = block.taxRate * 100 + " %";
        }
    }

}
