using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface IEliminationStrategy {
    EliminationOutcome checkForAndPerformElimination(Player player);
}

public class EliminationOutcome {
    public bool eliminated { get; private set; }
    public string reason { get; private set; }

    public EliminationOutcome(bool eliminated, string reason) {
        this.eliminated = eliminated;
        this.reason = reason;
    }
}