using System.Collections.Generic;

class LastManStandingVictoryStrategy : IVictoryStrategy {
    public VictoryOutcome checkForVictory(Player player) {
        City city = City.getInstance();

        if (city.getCurrentPlayer().eliminated) {
            return new VictoryOutcome(false, "eliminated");
        }

        List<Player> players = city.getPlayers();

        bool allEliminated = true;
        foreach (Player otherPlayer in players) {
            if (player == otherPlayer) continue;

            allEliminated = allEliminated && otherPlayer.eliminated;
        }

        if (allEliminated) {
            return new VictoryOutcome(true, "by eliminating all competing mega corporations in the city!");
        } else {
            return new VictoryOutcome(false, "not victorious!");
        }
    }
}