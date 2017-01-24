using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public Transform cityBlockView;

    public Text playerLabel = null;
    public Text cashLabel = null;

    public Text cityBlockLabel = null;
    public Text cityBlockIncomeLabel = null;
    public Text cityBlockIncomeValueLabel = null;
    public GameObject squadSelector;

    private City city;
    //private CityBlockView selectedBlock;
    //private Dictionary<CityBlock, CityBlockView> blockViewIndex = new Dictionary<CityBlock, CityBlockView>();
    //private List<CityBlockView> blockViews = new List<CityBlockView>();

    void Start () {
        //setSelectedCityBlock(null);

        initGame();
        initViews();
        updateUiForCurrentPlayer();
    }

    private void initGame() {
        city = City.getInstance();

        city.addPlayer("Alpha");
        city.addPlayer("Beta");
        city.addPlayer("Charlie");
        city.addPlayer("Delta");
        city.generateCity(5, 5, new ConcentricCityMapGenerator(), new NoCityBlocksEliminationStrategy(), new LastManStandingVictoryStrategy());

        city.startTurn();
    }

    private void initViews() {
        float cornerX = -city.width;
        float cornerY = -city.height;
        float tileSize = 2.0f;

        for (int y = 0; y < city.height; y++) {
            for (int x = 0; x < city.width; x++) {
                Vector3 pos = new Vector3(cornerX + x * tileSize, cornerY + y * tileSize);
                Instantiate(cityBlockView, pos, Quaternion.identity);
      //          CityBlockView newView = view.GetComponent<CityBlockView>();
      //          newView.cityBlock = city.getCityBlock(x, y);
      //          blockViewIndex.Add(newView.cityBlock, newView);
      //          blockViews.Add(newView);
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
        //for (int i = 0; i < blockViews.Count; i++) {
        //    blockViews[i].setShowSquadIndicator(false);
        //}
    }

    private void displayFriendlySquadsOnMap() {
        List<Squad> squads = city.getCurrentPlayerSquads();
        for (int i = 0; i < squads.Count; i++) {
            Squad squad = squads[i];
            city.getCityBlock(squad.x, squad.y);
            //blockViewIndex[block].setShowSquadIndicator(true);
            //blockViewIndex[block].setSquadIndicatorColour(ColorConverter.convert(squad.owner.colour));
        }
    }

    private void displayEnemySquadsOnMap() {

    }

    public void endTurn() {
        city.endTurn();
        updateUiForCurrentPlayer();
    }

    /*public void setSelectedCityBlock(CityBlockView cityBlockView) {
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

            showCityBlockDetails();
            showCityBlockSquads();
        }
    }*/
    
    private void showCityBlockDetails() {
        /*CityBlock block = this.selectedBlock.cityBlock;
        this.cityBlockLabel.text = block.name;
        this.cityBlockLabel.gameObject.SetActive(true);
        if (block.owner != null) {
            this.cityBlockLabel.color = ColorConverter.convert(block.owner.colour);
        } else {
            this.cityBlockLabel.color = Color.white;
        }

        this.cityBlockIncomeLabel.gameObject.SetActive(true);

        this.cityBlockIncomeValueLabel.text = "$ " + block.income;
        this.cityBlockIncomeValueLabel.gameObject.SetActive(true);*/
    }

    private void showCityBlockSquads() {
        /*CityBlock block = this.selectedBlock.cityBlock;
        if (block == null) {
            return;
        }

        List<Squad> squads = city.getCurrentPlayerSquads(block);
        if (squads.Count > 0) {
            Squad squad = squads[0];
            this.squadSelector.SetActive(true);

            for (int i = 4; i >= 1; i--) {
                GameObject agent = this.squadSelector.transform.FindChild("Agent" + i).gameObject;
                agent.SetActive(i <= squad.agentCount());

                UI2DSprite theRenderer = agent.GetComponent<UI2DSprite>();
                theRenderer.material.SetColor("_Color1out", ColorConverter.convert(squad.owner.colour));

                UILabel nameLabel = this.squadSelector.transform.FindChild("Name Label").GetComponent<UILabel>();
                nameLabel.text = squad.name;

                UILabel orderLabel = this.squadSelector.transform.FindChild("Order Label").GetComponent<UILabel>();
                if (squad.command == null) {
                    orderLabel.text = "None";
                }
            }
        } else {
            this.squadSelector.SetActive(false);
        }*/
    }

    void Update () {
	
	}
}
