using System.Collections.Generic;

public class SquadManager {
    private Dictionary<Player, List<Squad>> playerSquads = new Dictionary<Player, List<Squad>>();
    private Dictionary<CityBlock, List<Squad>> cityBlockSquads = new Dictionary<CityBlock, List<Squad>>();

    public Squad createSquad(Player player, int numAgents) {
        Squad squad = new Squad(player, numAgents);
        squad.LocationChanged += onSquadLocationChanged;

        if (!playerSquads.ContainsKey(player)) {
            playerSquads.Add(player, new List<Squad>());
        }

        playerSquads[player].Add(squad);

        return squad;
    }

    private void onSquadLocationChanged(object sender, LocationChangedEventArgs e) {
        Squad squad = (Squad) sender;
        CityBlock from = City.getInstance().getCityBlock(e.oldX, e.oldY);
        CityBlock to = City.getInstance().getCityBlock(e.newX, e.newY);

        if (from != null) {
            cityBlockSquads[from].Remove(squad);
        }

        if (to != null) {
            if (!cityBlockSquads.ContainsKey(to)) {
                cityBlockSquads.Add(to, new List<Squad>());
            }

            cityBlockSquads[to].Add(squad);
        }
    }

    public List<Squad> getSquads(CityBlock block) {
        if (cityBlockSquads.ContainsKey(block)) {
            return new List<Squad>(cityBlockSquads[block]);
        } else {
            return new List<Squad>();
        }
    }

    public List<Squad> getSquads(Player player) {
        if (playerSquads.ContainsKey(player)) {
            return new List<Squad>(playerSquads[player]);
        } else {
            return new List<Squad>();
        }
    }

    public List<Squad> getSquads(CityBlock block, Player player) {
        if (cityBlockSquads.ContainsKey(block)) {
            List<Squad> result = new List<Squad>(cityBlockSquads[block]);
            result.RemoveAll(squad => squad.owner != player);
            return result;
        } else {
            return new List<Squad>();
        }
    }
}