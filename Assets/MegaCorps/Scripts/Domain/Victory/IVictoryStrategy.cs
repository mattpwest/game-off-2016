using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface IVictoryStrategy {
    VictoryOutcome checkForVictory(Player player);
}

public class VictoryOutcome {
    public bool victory { get; private set; }
    public string reason { get; private set; }

    public VictoryOutcome(bool victory, string reason) {
        this.victory = victory;
        this.reason = reason;
    }
}