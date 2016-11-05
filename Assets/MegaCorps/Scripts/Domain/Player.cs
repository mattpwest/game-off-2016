using System.Collections;

public class Player {

    public string name { get; set; }
    public Colour colour { get; set; }
    public int cash { get; set; }

    public Player(string name, Colour colour) {
        this.name = name;
        this.colour = colour;
        this.cash = 0;
    }
}
