using System.Collections.Generic;

public class City {

    private static City instance;

    public int width { get; private set; }
    public int height { get; private set; }
    public int maxPlayers { get; private set; }

    private List<List<CityBlock>> map;
    private List<Player> players;
    private List<Colour> playerColours;
    private int currentPlayer = -1;

	private City() {
        width = 0;
        height = 0;
        maxPlayers = 4;

        players = new List<Player>(maxPlayers);

        playerColours = new List<Colour>(maxPlayers);
        playerColours.Add(new Colour(1.0f, 0.0f, 0.0f));
        playerColours.Add(new Colour(0.0f, 1.0f, 0.0f));
        playerColours.Add(new Colour(0.0f, 0.0f, 1.0f));
        playerColours.Add(new Colour(0.7f, 0.0f, 0.7f));
    }

    public static City getInstance() {
        if (instance == null) {
            instance = new City();
        }

        return instance;
    }

    public void generateCity(int width, int height) {
        this.width = width;
        this.height = height;
        this.map = new List<List<CityBlock>>(height);

        for (int y = 0; y < height; y++) {
            List<CityBlock> row = new List<CityBlock>(width);
            this.map.Add(row);

            for (int x = 0; x < width; x++) {
                char column = (char) ('A' + x);
                CityBlock block = new CityBlock(column + "" + y, 10000);
                row.Add(block);
            }
        }
    }

    public Player addPlayer(string name) {
        if (this.players.Count >= maxPlayers) {
            return null;
        }

        Player player = new Player(name, playerColours[players.Count]);
        players.Add(player);

        return player;
    }

    public CityBlock getCityBlock(int x, int y) {
        if (x < 0 || x >= width || y < 0 || y >= height || map == null) {
            return null;
        }

        return this.map[y][x];
    }

    public Player getCurrentPlayer() {
        if (currentPlayer < 0 || currentPlayer > players.Count) {
            return null;
        }

        return players[currentPlayer];
    }

    private void startGame() {
        // Spawn players
        if (players.Count == 2) {
            getCityBlock(0, height / 2).owner = players[0];
            getCityBlock(width - 1, height / 2).owner = players[1];
        } else if (players.Count == 3) {
            getCityBlock(0, height - 1).owner = players[0];
            getCityBlock(width / 2, 0).owner = players[1];
            getCityBlock(width - 1, height - 1).owner = players[2];
        } else if (players.Count == 4) {
            getCityBlock(0, height - 1).owner = players[0];
            getCityBlock(0, 0).owner = players[1];
            getCityBlock(width - 1, 0).owner = players[2];
            getCityBlock(width - 1, height - 1).owner = players[3];
        }

        currentPlayer = 0;
    }

    public void startTurn() {
        if (currentPlayer < 0) {
            startGame();
        }
    }

    public void endTurn() {
        currentPlayer++;

        if (currentPlayer == players.Count) {
            currentPlayer = 0;

            endRound();
            startRound();
        }

        startTurn();
    }

    private void startRound() {
        for (int i = 0; i < players.Count; i++) {
            Player player = players[i];

            player.cash += calculateIncomeFromOwnedCityBlocks(player);
        }
    }

    private void endRound() {
    }

    private int calculateIncomeFromOwnedCityBlocks(Player player) {
        int total = 0;
        for (int y = 0; y < height; y++) {
            for (int x = 0; x < width; x++) {
                CityBlock block = getCityBlock(x, y);
                Player owner = block.owner;
                if (owner != null && owner.name == player.name) {
                    total += block.income;
                }
            }
        }

        return total;
    }
}
