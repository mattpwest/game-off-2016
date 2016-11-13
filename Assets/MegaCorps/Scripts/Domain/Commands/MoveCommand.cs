﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class MoveCommand : Command {
    public int x { get; private set; }
    public int y { get; private set; }

    public MoveCommand(int x, int y) {
        this.x = x;
        this.y = y;
    }
}
