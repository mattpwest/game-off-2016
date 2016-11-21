using System;
using System.Collections.Generic;

public class Agent {

    public Body body { get; private set; }

    public Agent() {
        body = new Body();
    }

    public int calculateBaseMaintenanceCost() {
        return 500;
    }

    public float getHealthPercentage() {
        return body.getHealthPercentage();
    }
}

public class Body {

    Dictionary<BodyPartType, int> health = new Dictionary<BodyPartType, int>();

    public Body() {
        health.Add(BodyPartType.HEAD, BodyPartType.HEAD.hp);
        health.Add(BodyPartType.TORSO, BodyPartType.TORSO.hp);
        health.Add(BodyPartType.LEFT_ARM, BodyPartType.LEFT_ARM.hp);
        health.Add(BodyPartType.RIGHT_ARM, BodyPartType.RIGHT_ARM.hp);
        health.Add(BodyPartType.LEFT_LEG, BodyPartType.LEFT_LEG.hp);
        health.Add(BodyPartType.RIGHT_LEG, BodyPartType.RIGHT_LEG.hp);
    }

    public int getHealth(BodyPartType bodyPartType) {
        return health[bodyPartType];
    }

    public int getMaxHealth(BodyPartType bodyPartType) {
        return bodyPartType.hp;
    }

    public float getHealthPercentage(BodyPartType bodyPartType) {
        return health[bodyPartType] / (float) bodyPartType.hp;
    }

    public float getHealthPercentage() {
        int hp = 0;
        int hpMax = 0;
        foreach (BodyPartType bodyPartType in health.Keys) {
            hp += health[bodyPartType];
            hpMax += bodyPartType.hp;
        }

        return hp / (float) hpMax;
    }

    public void damage(int dmg) {
        // TODO: Random damage assignment...
        // TODO: Events triggered in the event of destruction of body parts...
        // TODO: Possible overflow of damage to arms to torso...
    }
}

public class BodyPartType {

    private static int globalId = 0;

    // Head calculated to have a 12.5% chance of taking full damage from a pistol that does an exploding D2 of damage
    // Best layman's explanation I could find: http://www.insomnihack.com/?p=495
    public static BodyPartType HEAD = new BodyPartType("Head", 5);
    public static BodyPartType TORSO = new BodyPartType("Torso", 15);
    public static BodyPartType LEFT_ARM = new BodyPartType("Left Arm", 10);
    public static BodyPartType RIGHT_ARM = new BodyPartType("Right Arm", 10);
    public static BodyPartType LEFT_LEG = new BodyPartType("Left Leg", 10);
    public static BodyPartType RIGHT_LEG = new BodyPartType("Left Leg", 10);

    public int id { get; private set; }
    public string name { get; private set; }
    public int hp { get; private set; }

    private BodyPartType(string name, int hp) {
        this.id = ++globalId;
        this.name = name;
        this.hp = hp;
    }
}