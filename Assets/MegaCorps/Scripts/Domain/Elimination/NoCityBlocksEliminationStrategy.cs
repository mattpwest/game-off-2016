using System;

class NoCityBlocksEliminationStrategy : IEliminationStrategy {
    public EliminationOutcome checkForAndPerformElimination(Player player) {
        City city = City.getInstance();

        if (city.getOwnedCityBlocks(player).Count == 0) {
            player.eliminated = true;

            return new EliminationOutcome(true, "due to having lost control of all their sectors in the city.");
        } else {
            return new EliminationOutcome(false, "not eliminated");
        }
    }
}