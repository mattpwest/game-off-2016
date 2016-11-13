using System;
using System.Collections.Generic;

public class Squad {

    public event EventHandler<LocationChangedEventArgs> LocationChanged;

    public Player owner { get; set; }
    public string name { get; set; }
    public Command command { get; set; }
    public int x { get; private set; }
    public int y { get; private set; }
    public float speed { get; set; }

    private List<Agent> agents = new List<Agent>();

    public Squad(Player owner, string name, int numRandomAgents) {
        this.x = -1;
        this.y = -1;
        this.name = name;
        this.speed = 1.0f;

        this.owner = owner;

        for (int i = 0; i < numRandomAgents; i++) {
            Agent agent = new Agent();
            this.agents.Add(agent);
        }
    }

    public void setLocation(int x, int y) {
        LocationChangedEventArgs eventArgs = new LocationChangedEventArgs(this.x, this.y, x, y);

        this.x = x;
        this.y = y;

        if (LocationChanged != null) {
            EventArgs args = new EventArgs();
            LocationChanged(this, eventArgs);
        }
    }

    public int agentCount() {
        return agents.Count;
    }
}

public class LocationChangedEventArgs : EventArgs {
    public int oldX { get; set; }
    public int oldY { get; set; }
    public int newX { get; set; }
    public int newY { get; set; }

    public LocationChangedEventArgs(int oldX, int oldY, int newX, int newY) {
        this.oldX = oldX;
        this.oldY = oldY;
        this.newX = newX;
        this.newY = newY;
    }
}
