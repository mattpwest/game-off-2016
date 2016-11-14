using System;
using System.Collections.Generic;

public class Player {

    public string name { get; set; }
    public Colour colour { get; set; }
    public int cash { get; set; }
    public bool eliminated { get; set; }

    public Player(string name, Colour colour) {
        this.name = name;
        this.colour = colour;
        this.cash = 0;
        this.eliminated = false;
    }
}
