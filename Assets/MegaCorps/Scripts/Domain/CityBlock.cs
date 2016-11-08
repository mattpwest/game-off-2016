using System;
using System.Collections.Generic;

public class CityBlock {

    public Player owner { get; set; }
    public string name { get; set; }
    public int income { get; set; }
    public int x { get; set; }
    public int y { get; set; }

    public CityBlock(string name, int income) {
        this.name = name;
        this.income = income;

        this.x = -1;
        this.y = 1;
    }
}
