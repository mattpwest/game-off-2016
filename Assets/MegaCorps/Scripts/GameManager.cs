using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public Transform cityBlockView;
    public UILabel playerLabel = null;
    public UILabel cashLabel = null;

    private City city;

    void Start () {
        initGame();
        initViews();
        updateUiForCurrentPlayer();
    }

    private void initGame() {
        city = City.getInstance();

        city.generateCity(7, 7);
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
                view.GetComponent<CityBlockView>().cityBlock = city.getCityBlock(x, y);
            }
        }
    }

    private void updateUiForCurrentPlayer() {
        Player player = city.getCurrentPlayer();

        playerLabel.text = player.name;
        playerLabel.color = ColorConverter.convert(player.colour);

        cashLabel.text = "$ " + player.cash;
    }

    public void endTurn() {
        city.endTurn();
        updateUiForCurrentPlayer();
    }
	
	void Update () {
	
	}
}
