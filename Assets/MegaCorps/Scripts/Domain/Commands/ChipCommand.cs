using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ChipCommand : ICommand {

    private int CHIP_MARKETING_COST = 1000;

    public ChipCommand() {
    }

    public int getCost() {
        return CHIP_MARKETING_COST;
    }
}
