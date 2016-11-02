using UnityEngine;
using System.Collections;

public class TurnManager : MonoBehaviour {

    public Transform[] players;
    public UILabel playerLabel = null;
    public UILabel cashLabel = null;

    private CityBlock[] cityBlocks;
    private int currentPlayer = 0;

    // Use this for initialization
    void Start () {
        startTurn();
	}

    void Awake() {
        cityBlocks = Object.FindObjectsOfType<CityBlock>();
        Debug.Log("Found " + cityBlocks.Length + " CityBlocks");
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    private Player getPlayer(CityBlock cityBlock) {
        return cityBlock.owner == null ? null : cityBlock.owner.GetComponent<Player>();
    }

    private Player getPlayer(int index) {
        return players[index].GetComponent<Player>();
    }

    private Player getCurrentPlayer() {
        return players[currentPlayer].GetComponent<Player>();
    }

    public void startTurn() {
        Player player = getCurrentPlayer();
        playerLabel.text = player.name;
        playerLabel.color = player.color;
        cashLabel.text = "$ " + player.cash;
    }

    public void endTurn() {
        currentPlayer++;
        if (currentPlayer == players.Length) {
            currentPlayer = 0;

            endRound();
            startRound();
        }

        startTurn();
    }

    public void startRound() {
        for (int i = 0; i < players.Length; i++) {
            Player player = getPlayer(i);

            player.cash += calculateIncomeFromOwnedCityBlocks(player);
        }
    }

    public void endRound() {
    }

    private int calculateIncomeFromOwnedCityBlocks(Player player) {
        int total = 0;
        for (int i = 0; i < cityBlocks.Length; i++) {
            Player owner = getPlayer(cityBlocks[i]);
            if (owner != null && owner.name == player.name) {
                total += cityBlocks[i].income;
            }
        }

        Debug.Log("Income for " + player.name + " is " + total);
        return total;
    }
}
