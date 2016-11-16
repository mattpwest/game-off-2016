using System.Collections.Generic;
using System;

public class City {

    private const int NUM_AGENTS_IN_STARTING_SQUAD = 2;

    private static City instance;

    public int width { get; private set; }
    public int height { get; private set; }
    public int maxPlayers { get; private set; }
    public IEliminationStrategy eliminationStrategy { get; private set; }
    public IVictoryStrategy victoryStrategy { get; private set; }
    public SquadManager squadManager { get; private set; }
    
    private List<List<CityBlock>> map;
    private List<Player> players;
    private List<Colour> playerColours;
    private Dictionary<Player, List<String>> messages = new Dictionary<Player, List<string>>();
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

    public void generateCity(int width, int height, IEliminationStrategy eliminationStrategy, IVictoryStrategy victoryStrategy) {
        this.width = width;
        this.height = height;
        this.eliminationStrategy = eliminationStrategy;
        this.victoryStrategy = victoryStrategy;
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

        messages.Add(player, new List<String>());
        messages[player].Add("Welcome to the city!");
        
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

    public List<Player> getPlayers() {
        return this.players;
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

        Squad squad1 = squadManager.createSquad(owner, NUM_AGENTS_IN_STARTING_SQUAD);
        squad1.setLocation(x, y);

        Squad squad2 = squadManager.createSquad(owner, NUM_AGENTS_IN_STARTING_SQUAD);
        squad2.setLocation(x, y);
    }

    public void startTurn() {
        if (currentPlayer < 0) {
            startGame();
        }
    }

    public void endTurn() {
        while (currentPlayer < players.Count) {
            currentPlayer++;

            if (currentPlayer == players.Count) {
                currentPlayer = 0;

                endRound();
                startRound();
            }

            if (!players[currentPlayer].eliminated) break;
        }
        
        startTurn();
    }

    private void startRound() {
        eliminatePlayers();
        checkForWinners();
        addPlayerIncome();
    }
    
    private void eliminatePlayers() {
        foreach (Player player in players) {
            if (player.eliminated) continue; // Players should only be eliminated once

            EliminationOutcome outcome = eliminationStrategy.checkForAndPerformElimination(player);

            if (outcome.eliminated) {
                messages[player].Add("You have been eliminated " + outcome.reason);

                foreach (Player otherPlayer in players) {
                    if (otherPlayer != player) {
                        messages[otherPlayer].Add(player.name + " has been elminated " + outcome.reason);
                    }
                }
            }
        }
    }

    private void checkForWinners() {
        foreach (Player player in players) {
            VictoryOutcome outcome = victoryStrategy.checkForVictory(player);

            if (outcome.victory) {
                messages[player].Add("You have won by " + outcome.reason);

                foreach (Player otherPlayer in players) {
                    if (otherPlayer != player) {
                        messages[otherPlayer].Add(player.name + " won by " + outcome.reason);
                        otherPlayer.eliminated = true;
                    }
                }
            }
        }
    }

    private void addPlayerIncome() {
        foreach (Player player in players) {
            player.cash += calculateIncomeFromOwnedCityBlocks(player);
        }
    }

    private void endRound() {
        foreach (Player player in players) {
            messages[player].Clear();

            List<Squad> squads = squadManager.getSquads(player);
            foreach (Squad squad in squads) {
                if (squad.command == null) continue;

                if (squad.command.GetType() == typeof(MoveCommand)) {
                    MoveCommand cmd = (MoveCommand)squad.command;
                    squad.setLocation(cmd.x, cmd.y);
                    squad.command = null;
                } else if (squad.command.GetType() == typeof(ControlCommand)) {
                    CityBlock block = getCityBlock(squad.x, squad.y);
                    block.owner = squad.owner;
                    squad.command = null;
                }
            }
        }
        
    }

    private int calculateIncomeFromOwnedCityBlocks(Player player) {
        int total = 0;
        foreach (CityBlock block in getOwnedCityBlocks(player)) {
            total += block.income;
        }
        return total;
    }

    public List<CityBlock> getOwnedCityBlocks(Player player) {
        List<CityBlock> result = new List<CityBlock>();
        for (int y = 0; y < height; y++) {
            for (int x = 0; x < width; x++) {
                CityBlock block = getCityBlock(x, y);
                Player owner = block.owner;
                if (owner != null && owner == player) {
                    result.Add(block);
                }
            }
        }
        return result;
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

    public List<CityBlock> getSquadMoveTargets(Squad squad) {
        int maxDistance = (int) squad.speed;
        int minX = squad.x - maxDistance;
        int maxX = squad.x + maxDistance;
        int minY = squad.y - maxDistance;
        int maxY = squad.y + maxDistance;

        List<CityBlock> result = new List<CityBlock>();

        for (int x = minX; x <= maxX; x++) {
            for (int y = minY; y <= maxY; y++) {
                if (x == squad.x && y == squad.y) continue;

                CityBlock block = getCityBlock(x, y);
                if (block != null && calculateDistance(squad.x, squad.y, block.x, block.y) <= squad.speed) {
                    result.Add(block);
                }
            }
        }

        return result;
    }

    public bool issueMoveOrder(Squad squad, CityBlock cityBlock) {
        if (squad == null || cityBlock == null) return false;

        if (calculateDistance(squad.x, squad.y, cityBlock.x, cityBlock.y) <= squad.speed) {
            squad.command = new MoveCommand(cityBlock.x, cityBlock.y);
            return true;
        } else {
            return false;
        }
    }

    public bool issueControlOrder(Squad squad) {
        if (squad == null) return false;

        CityBlock block = getCityBlock(squad.x, squad.y);
        if (block.owner == squad.owner) return false;

        squad.command = new ControlCommand();
        return true;
    }

    private double calculateDistance(int x0, int y0, int x1, int y1) {
        int dx = x1 - x0;
        int dy = y1 - y0;
        return Math.Sqrt(dx * dx + dy * dy);
    }

    public List<String> getMessages(Player player) {
        return this.messages[player];
    }
}
