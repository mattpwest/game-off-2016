using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public Transform cityBlockView;

    public UILabel playerLabel = null;
    public UILabel cashLabel = null;

    public UILabel cityBlockLabel = null;
    public UILabel cityBlockIncomeLabel = null;
    public UILabel cityBlockIncomeValueLabel = null;

    private City city;
    private CityBlockView selectedBlock;
    private Dictionary<CityBlock, CityBlockView> blockViewIndex = new Dictionary<CityBlock, CityBlockView>();
    private List<CityBlockView> blockViews = new List<CityBlockView>();

    void Start () {
        setSelectedCityBlock(null);

        initGame();
        initViews();
        updateUiForCurrentPlayer();
    }

    private void initGame() {
        city = City.getInstance();

        city.generateCity(5, 5);
        city.addPlayer("Alpha");
        city.addPlayer("Beta");
        city.addPlayer("Charlie");
        city.addPlayer("Delta");

        city.startTurn();
    }

    private void initViews() {
        float cornerX = -city.width;
        float cornerY = -city.height;
        float tileSize = 2.0f;

        for (int y = 0; y < city.height; y++) {
            for (int x = 0; x < city.width; x++) {
                Vector3 pos = new Vector3(cornerX + x * tileSize, cornerY + y * tileSize);
                Transform view = (Transform) Instantiate(cityBlockView, pos, Quaternion.identity);
                CityBlockView newView = view.GetComponent<CityBlockView>();
                newView.cityBlock = city.getCityBlock(x, y);
                blockViewIndex.Add(newView.cityBlock, newView);
                blockViews.Add(newView);
            }
        }
    }

    private void updateUiForCurrentPlayer() {
        displayPlayerNameAndCash();

        resetSquadDisplayOnMap();
        displayFriendlySquadsOnMap();
        displayEnemySquadsOnMap();
    }

    private void displayPlayerNameAndCash() {
        Player player = city.getCurrentPlayer();

        playerLabel.text = player.name;
        playerLabel.color = ColorConverter.convert(player.colour);

        cashLabel.text = "$ " + player.cash;
    }

    private void resetSquadDisplayOnMap() {
        for (int i = 0; i < blockViews.Count; i++) {
            blockViews[i].setShowSquadIndicator(false);
        }
    }

    private void displayFriendlySquadsOnMap() {
        Player player = city.getCurrentPlayer();
        List<Squad> squads = city.getCurrentPlayerSquads();
        for (int i = 0; i < squads.Count; i++) {
            Squad squad = squads[i];
            CityBlock block = city.getCityBlock(squad.x, squad.y);
            blockViewIndex[block].setShowSquadIndicator(true);
            blockViewIndex[block].setSquadIndicatorColour(ColorConverter.convert(player.colour));
        }
    }

    private void displayEnemySquadsOnMap() {

    }

    public void endTurn() {
        city.endTurn();
        updateUiForCurrentPlayer();
    }

    public void setSelectedCityBlock(CityBlockView cityBlockView) {
        if (this.selectedBlock != null) {
            this.selectedBlock.setShowSelector(false);
        }

        this.selectedBlock = cityBlockView;

        if (this.selectedBlock == null) {
            this.cityBlockLabel.gameObject.SetActive(false);
            this.cityBlockIncomeLabel.gameObject.SetActive(false);
            this.cityBlockIncomeValueLabel.gameObject.SetActive(false);
        } else {
            this.selectedBlock.setShowSelector(true);

            CityBlock block = this.selectedBlock.cityBlock;
            this.cityBlockLabel.text = block.name;
            this.cityBlockLabel.gameObject.SetActive(true);
            if (block.owner != null) {
                this.cityBlockLabel.color = ColorConverter.convert(block.owner.colour);
            } else {
                this.cityBlockLabel.color = Color.white;
            }

            this.cityBlockIncomeLabel.gameObject.SetActive(true);

            this.cityBlockIncomeValueLabel.text = "$ " + block.income;
            this.cityBlockIncomeValueLabel.gameObject.SetActive(true);
        }
    }
	
	void Update () {
	
	}
}
