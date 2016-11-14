using UnityEngine;
using System.Collections;

public class UIMain : MonoBehaviour {

    private UIState uiState;
    private float delayUntilStart = 0.1f;

	void Start () {
    }

	void Update () {
        if (delayUntilStart > 0) {
            delayUntilStart -= Time.deltaTime;
        } else if (this.uiState == null) {
            this.uiState = UIState.getInstance();

            this.startGame();
        }
    }

    public void startGame() {
        City city = City.getInstance();

        city.generateCity(5, 5, new NoCityBlocksEliminationStrategy());
        city.addPlayer("Alpha");
        city.addPlayer("Beta");
        city.addPlayer("Charlie");
        city.addPlayer("Delta");

        city.startTurn();

        this.uiState.setActiveCity(city);
    }

    public void endTurn() {
        City city = City.getInstance();

        city.endTurn();

        uiState.setActiveBlock(null);
        uiState.setActivePlayer(city.getCurrentPlayer());
    }
}
