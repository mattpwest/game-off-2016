using System.Collections.Generic;

public class City {

    private const int NUM_AGENTS_IN_STARTING_SQUAD = 2;

    private static City instance;

    public int width { get; private set; }
    public int height { get; private set; }
    public int maxPlayers { get; private set; }
    public SquadManager squadManager { get; private set; }
    
    private List<List<CityBlock>> map;
    private List<Player> players;
    private List<Colour> playerColours;
    private int currentPlayer = -1;

	private City() {
        width = 0;
        height = 0;
        maxPlayers = 4;
        this.squadManager = new SquadManager();

        players = new List<Player>(maxPlayers);

        playerColours = new List<Colour>(maxPlayers);
        playerColours.Add(new Colour(1.0f, 0.0f, 0.0f));
        playerColours.Add(new Colour(0.0f, 0.0f, 1.0f));
        playerColours.Add(new Colour(0.0f, 1.0f, 0.0f));
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
                CityBlock block = new CityBlock(column + "" + (y + 1), 10000);
                block.x = x;
                block.y = y;
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
            spawnPlayer(0, height / 2, players[0]);
            spawnPlayer(width - 1, height / 2, players[1]);
        } else if (players.Count == 3) {
            spawnPlayer(0, height - 1, players[0]);
            spawnPlayer(width / 2, 0, players[1]);
            spawnPlayer(width - 1, height - 1, players[2]);
        } else if (players.Count == 4) {
            spawnPlayer(0, height - 1, players[0]);
            spawnPlayer(0, 0, players[1]);
            spawnPlayer(width - 1, 0, players[2]);
            spawnPlayer(width - 1, height - 1, players[3]);
        }

        currentPlayer = 0;
    }

    public void spawnPlayer(int x, int y, Player owner) {
        CityBlock block = getCityBlock(x, y);
        block.owner = owner;

        Squad squad = squadManager.createSquad(owner, NUM_AGENTS_IN_STARTING_SQUAD);
        squad.setLocation(x, y);
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

    public HashSet<CityBlock> getBlocksWherePlayerPresent(Player player) {
        HashSet<CityBlock> result = new HashSet<CityBlock>();
        List<Squad> squads = squadManager.getSquads(player);

        for (int i = 0; i < squads.Count; i++) {
            Squad squad = squads[i];
            result.Add(getCityBlock(squad.x, squad.y));
        }

        return result;
    }

    public List<Squad> getCurrentPlayerSquads() {
        return squadManager.getSquads(getCurrentPlayer());
    }

    public List<Squad> getCurrentPlayerSquads(CityBlock cityBlock) {
        return squadManager.getSquads(cityBlock, getCurrentPlayer());
    }
}
