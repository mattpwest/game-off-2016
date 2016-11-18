using System;
using System.Collections.Generic;

public class CityBlock {

    public Player owner { get; set; }
    public string name { get; set; }
    public int population { get; set; }
    public float chipAdoption { get; set; }
    public float taxRate { get; set; }
    public WealthLevel wealthLevel { get; set; }
    private int happiness; 

    public int x { get; set; }
    public int y { get; set; }

    public CityBlock() {
        this.x = -1;
        this.y = 1;
    }

    public int getIncome() {
        return 0;
    }

    public HappinessLevel getHappinessLevel() {
        return HappinessLevel.getForLevel(happiness);
    }

    public void setHappinessLevel(HappinessLevel level) {
        this.happiness = level.threshold;
    }
}

public interface ICityBlockBuilder {
    ICityBlockBuilder setName(string name);
    ICityBlockBuilder setPopulation(int population);
    ICityBlockBuilder setChipAdoption(float chipAdoption);
    ICityBlockBuilder setTaxRate(float taxRate);
    ICityBlockBuilder setWealthLevel(WealthLevel wealthLevel);
    ICityBlockBuilder setHappinessLevel(HappinessLevel happinessLevel);

    CityBlock getResult();
}

public class CityBlockBuilder : ICityBlockBuilder {

    private CityBlock block;

    public CityBlockBuilder() {
        block = new CityBlock();
    }

    public CityBlock getResult() {
        if (block.name == null) {
            block.name = CityBlockRandomGenerator.getRandomName();
        }
        return block;
    }

    public ICityBlockBuilder setName(string name) {
        block.name = name;
        return this;
    }

    public ICityBlockBuilder setChipAdoption(float chipAdoption) {
        block.chipAdoption = chipAdoption;
        return this;
    }

    public ICityBlockBuilder setHappinessLevel(HappinessLevel happinessLevel) {
        block.setHappinessLevel(happinessLevel);
        return this;
    }

    public ICityBlockBuilder setPopulation(int population) {
        block.population = population;
        return this;
    }

    public ICityBlockBuilder setTaxRate(float taxRate) {
        block.taxRate = taxRate;
        return this;
    }

    public ICityBlockBuilder setWealthLevel(WealthLevel wealthLevel) {
        block.wealthLevel = wealthLevel;
        return this;
    }
}

public class WealthLevel {
    public static WealthLevel POOR = new WealthLevel("Poor", 1);
    public static WealthLevel AVERAGE = new WealthLevel("Average", 3);
    public static WealthLevel RICH = new WealthLevel("Rich", 5);

    public string name { get; private set; }
    public int incomePerPerson { get; private set; }

    private WealthLevel(string name, int incomePerPerson) {
        this.name = name;
        this.incomePerPerson = incomePerPerson;
    }
}

public class HappinessLevel {
    public static HappinessLevel RIOTOUS = new HappinessLevel("Riotous", 0);
    public static HappinessLevel UNHAPPY = new HappinessLevel("Unhappy", 30);
    public static HappinessLevel CONTENT = new HappinessLevel("Content", 55);
    public static HappinessLevel HAPPY = new HappinessLevel("Happy", 80);
    public static HappinessLevel JOYOUS = new HappinessLevel("Joyous", 95);

    public string name { get; private set; }
    public int threshold { get; private set; }

    private HappinessLevel(string name, int threshold) {
        this.name = name;
        this.threshold = threshold;
    }

    internal static HappinessLevel getForLevel(int happiness) {
        if (happiness >= JOYOUS.threshold) {
            return JOYOUS;
        } else if (happiness >= HAPPY.threshold) {
            return HAPPY;
        } else if (happiness >= CONTENT.threshold) {
            return CONTENT;
        } else if (happiness >= UNHAPPY.threshold) {
            return UNHAPPY;
        } else {
            return RIOTOUS;
        }
    }
}

public class CityBlockRandomGenerator {

    private static List<string> names;

    static CityBlockRandomGenerator() {
        names.Add("Devil's Peak");
        names.Add("Belville");
        names.Add("Goodwood");
        names.Add("Clifton");
        names.Add("Constantia");
        names.Add("Delft");
        names.Add("Atlantis");
        names.Add("Ashcroft");
        names.Add("Avalon");
        names.Add("Beacon Hill");
        names.Add("Brighton");
        names.Add("Brooklyn");
        names.Add("Burwood");
        names.Add("Camden");
        names.Add("Cambridge");
        names.Add("Carlton");
        names.Add("Claymore");
        names.Add("Castle Cove");
        names.Add("Earlwood");
        names.Add("Eastwood");
        names.Add("Enfield");
        names.Add("Fairfield");
        names.Add("Forestville");
        names.Add("Glenfield");
        names.Add("Glenwood");
        names.Add("Gordon");
        names.Add("Grasmere");
        names.Add("Greenacre");
        names.Add("Greendale");
        names.Add("Greenwich");
        names.Add("Hillsdale");
        names.Add("Huntingwood");
        names.Add("Hurstville");
        names.Add("Kellyville");
        names.Add("Kensington");
        names.Add("Kings Park");
        names.Add("Kingsford");
        names.Add("Kingswood");
        names.Add("Kirkham");
        names.Add("Liberty Grove");
        names.Add("Leonay");
        names.Add("Newport");
        names.Add("Northmead");
        names.Add("Northwood");
        names.Add("Northbridge");
    }

    public static string getRandomName() {

    }
}