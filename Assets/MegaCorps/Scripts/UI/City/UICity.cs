using UnityEngine;
using System.Collections.Generic;
using System;

public class UICity : MonoBehaviour {

    private UIState uiState;
    private City city;
    private Dictionary<CityBlock, UICityBlock> blockViewIndex = new Dictionary<CityBlock, UICityBlock>();
    private List<UICityBlock> blockViews = new List<UICityBlock>();

    public Transform cityBlockView;

	void Start () {
        this.uiState = UIState.getInstance();
        this.uiState.CityChanged += OnCityChanged;
        this.uiState.BlockChanged += OnBlockChanged;
        this.uiState.PlayerChanged += OnPlayerChanged;
	}

    void Update () {
	
	}

    private void OnCityChanged(object sender, UIState.CityChangedEventArgs e) {
        this.city = e.city;

        initViews();
    }

    private void initViews() {
        float cornerX = -this.city.width;
        float cornerY = -this.city.height;
        float tileSize = 2.0f;

        for (int y = 0; y < city.height; y++) {
            for (int x = 0; x < city.width; x++) {
                Vector3 pos = new Vector3(cornerX + x * tileSize, cornerY + y * tileSize);
                Transform view = (Transform)Instantiate(cityBlockView, pos, Quaternion.identity);
                UICityBlock newView = view.GetComponent<UICityBlock>();
                newView.cityBlock = city.getCityBlock(x, y);
                blockViewIndex.Add(newView.cityBlock, newView);
                blockViews.Add(newView);
            }
        }
    }

    private void OnBlockChanged(object source, UIState.BlockChangedEventArgs args) {
        if (args.previousBlock != null) {
            this.blockViewIndex[args.previousBlock].setShowSelector(false);
        }

        CityBlock block = args.block;

        if (block != null) {
            this.blockViewIndex[block].setShowSelector(true);
        }
    }

    private void OnPlayerChanged(object source, UIState.PlayerChangedEventArgs args) {
        if (args.previousPlayer != null) {
            hideSquads(args.previousPlayer);
        }

        if (args.player != null) {
            showSquads(args.player);
        }
    }

    private void hideSquads(Player player) {
        HashSet<CityBlock> blocks = city.getBlocksWherePlayerPresent(player);
        foreach (CityBlock block in blocks) {
            blockViewIndex[block].setShowSquadIndicator(false);
        }
    }

    private void showSquads(Player player) {
        Debug.Log("Showing squads for " + player.name);
        HashSet<CityBlock> blocks = city.getBlocksWherePlayerPresent(player);
        foreach (CityBlock block in blocks) {
            UICityBlock view = blockViewIndex[block];
            view.setSquadIndicatorColour(ColorConverter.convert(player.colour));
            view.setShowSquadIndicator(true);
        }
    }
}
