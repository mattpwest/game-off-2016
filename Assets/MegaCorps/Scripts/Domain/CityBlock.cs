using System;
using System.Collections.Generic;

public class CityBlock {

    public int x { get; set; }
    public int y { get; set; }
    public Player owner { get; set; }
    public string name { get; set; }
    public int population { get; set; }
    public float chipAdoption { get; set; }
    public float taxRate { get; set; }
    public WealthLevel wealthLevel { get; set; }

    private int happiness; 
    
    public CityBlock() {
        this.x = -1;
        this.y = 1;

        this.name = null;
        this.population = 0;
        this.chipAdoption = 0.0f;
        this.taxRate = 0.0f;
        this.wealthLevel = null;
        this.happiness = -1;
    }

    public int getIncome() {
        float baseIncome = taxRate * population * wealthLevel.incomePerPerson;
        float guaranteedIncome = baseIncome * chipAdoption; // * chipIncomeBonusPercentage; // (TODO: Corporation attributes feature)

        float nonChipTaxRate = taxRate * 0.5f * (1.0f - chipAdoption);
        float variableIncome = baseIncome * nonChipTaxRate; // * happinessBonusOrPenaltyPercentage; // TODO: Happiness systemm
        return (int) Math.Round(guaranteedIncome + variableIncome);
    }

    public HappinessLevel getHappiness() {
        if (happiness < 0) {
            return null;
        } else {
            return HappinessLevel.getForLevel(happiness);
        }
    }

    public void setHappiness(HappinessLevel level) {
        this.happiness = level.threshold;
    }

    public void setHappinessLevel(int level) {
        this.happiness = level;
    }

    public int getHappinessLevel() {
        return this.happiness;
    }
}

public interface ICityBlockBuilder {
    ICityBlockBuilder setName(string name);

    ICityBlockBuilder setPopulation(int population);
    ICityBlockBuilder setPopulationLow();
    ICityBlockBuilder setPopulationAverage();
    ICityBlockBuilder setPopulationHigh();


    ICityBlockBuilder setChipAdoption(float chipAdoption);
    ICityBlockBuilder setChipAdoptionLow();
    ICityBlockBuilder setChipAdoptionAverage();
    ICityBlockBuilder setChipAdoptionHigh();

    ICityBlockBuilder setTaxRate(float taxRate);

    ICityBlockBuilder setWealthLevel(WealthLevel wealthLevel);
    ICityBlockBuilder setWealthLevelPoor();
    ICityBlockBuilder setWealthLevelAverage();
    ICityBlockBuilder setWealthLevelRich();

    ICityBlockBuilder setHappiness(HappinessLevel happinessLevel);
    ICityBlockBuilder setHappinessLevel(int happiness);

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

        if (block.population == 0) {
            block.population = CityBlockRandomGenerator.getRandomPopulationAverage();
        }

        if (block.chipAdoption == 0.0f) {
            block.chipAdoption = CityBlockRandomGenerator.getRandomChipAdoptionLow();
        }

        if (block.taxRate == 0.0f) {
            block.taxRate = 0.1f;
        }

        if (block.wealthLevel == null) {
            block.wealthLevel = CityBlockRandomGenerator.getRandomWealthLevel();
        }

        if (block.getHappinessLevel() < 0) {
            block.setHappinessLevel(CityBlockRandomGenerator.getRandomHappiness());
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

    public ICityBlockBuilder setPopulation(int population) {
        block.population = population;
        return this;
    }

    public ICityBlockBuilder setPopulationLow() {
        block.population = CityBlockRandomGenerator.getRandomPopulationLow();
        return this;
    }

    public ICityBlockBuilder setPopulationAverage() {
        block.population = CityBlockRandomGenerator.getRandomPopulationAverage();
        return this;
    }

    public ICityBlockBuilder setPopulationHigh() {
        block.population = CityBlockRandomGenerator.getRandomPopulationHigh();
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

    public ICityBlockBuilder setHappiness(HappinessLevel happiness) {
        block.setHappiness(happiness);
        return this;
    }

    public ICityBlockBuilder setHappinessLevel(int happiness) {
        block.setHappinessLevel(happiness);
        return this;
    }

    public ICityBlockBuilder setChipAdoptionLow() {
        block.chipAdoption = CityBlockRandomGenerator.getRandomChipAdoptionLow();
        return this;
    }

    public ICityBlockBuilder setChipAdoptionAverage() {
        block.chipAdoption = CityBlockRandomGenerator.getRandomChipAdoptionAverage();
        return this;
    }

    public ICityBlockBuilder setChipAdoptionHigh() {
        block.chipAdoption = CityBlockRandomGenerator.getRandomChipAdoptionHigh();
        return this;
    }

    public ICityBlockBuilder setWealthLevelPoor() {
        block.wealthLevel = WealthLevel.POOR;
        return this;
    }

    public ICityBlockBuilder setWealthLevelAverage() {
        block.wealthLevel = WealthLevel.AVERAGE;
        return this;
    }

    public ICityBlockBuilder setWealthLevelRich() {
        block.wealthLevel = WealthLevel.RICH;
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

    private static Random rng = new Random();
    private static List<string> names = new List<string>();
    private static List<WealthLevel> wealthLevels = new List<WealthLevel>();

    private static int MIN_POP_THOUSANDS = 5;
    private static int MAX_POP_THOUSANDS = 49;

    private static int MIN_CHIP_ADOPTION = 0;
    private static int MAX_CHIP_ADOPTION = 10;

    static CityBlockRandomGenerator() {
        wealthLevels.Add(WealthLevel.POOR);
        wealthLevels.Add(WealthLevel.AVERAGE);
        wealthLevels.Add(WealthLevel.RICH);

        names.Add("Atlantis");
        names.Add("Ashcroft");
        names.Add("Avalon");
        names.Add("Belville");
        names.Add("Beacon Hill");
        names.Add("Brighton");
        names.Add("Brooklyn");
        names.Add("Burwood");
        names.Add("Bridgeview");
        names.Add("Chicago Ridge");
        names.Add("Chicago Heights");
        names.Add("Camden");
        names.Add("Cambridge");
        names.Add("Carlton");
        names.Add("Claymore");
        names.Add("Castle Cove");
        names.Add("Clifton");
        names.Add("Constantia");
        names.Add("Devil's Peak");
        names.Add("Delft");
        names.Add("Druid Hills");
        names.Add("Dolton");
        names.Add("Earlwood");
        names.Add("Eastwood");
        names.Add("Enfield");
        names.Add("Euclid");
        names.Add("Everett");
        names.Add("Elmwood Park");
        names.Add("Evergreen Park");
        names.Add("Fairfield");
        names.Add("Franklin Park");
        names.Add("Forestville");
        names.Add("Glenfield");
        names.Add("Glenwood");
        names.Add("Goodwood");
        names.Add("Gordon");
        names.Add("Grasmere");
        names.Add("Greenacre");
        names.Add("Greendale");
        names.Add("Greenwich");
        names.Add("Harvey");
        names.Add("Hillsdale");
        names.Add("Hometown");
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
        names.Add("Ludlow");
        names.Add("Malden");
        names.Add("Markham");
        names.Add("Milton");
        names.Add("Newport");
        names.Add("Niles");
        names.Add("Northmead");
        names.Add("Northwood");
        names.Add("Northbridge");
        names.Add("Oakhurst");
        names.Add("Oakville");
        names.Add("Oak Park");
        names.Add("Paddington");
        names.Add("Park Ridge");
        names.Add("Pikesville");
        names.Add("Queens Park");
        names.Add("Qeeunscliff");
        names.Add("Ramsgate");
        names.Add("Reading");
        names.Add("Richmond");
        names.Add("Riverdale");
        names.Add("Riverstone");
        names.Add("Riverview");
        names.Add("Riverwood");
        names.Add("Rockdale");
        names.Add("Rookwood");
        names.Add("Rosehill");
        names.Add("Roselands");
        names.Add("Riderwood");
        names.Add("Somerville");
        names.Add("Seaforth");
        names.Add("Seven Hills");
        names.Add("Silverwater");
        names.Add("Silverton");
        names.Add("Spring Farm");
        names.Add("St Andrews");
        names.Add("St Clair");
        names.Add("St Ives");
        names.Add("St Marys");
        names.Add("St Peters");
        names.Add("Summer Hill");
        names.Add("Summit");
        names.Add("Ultimo");
        names.Add("Villawood");
        names.Add("Vineyard");
        names.Add("Waterfall");
        names.Add("Waterloo");
        names.Add("Waverley");
        names.Add("Westmead");
        names.Add("Windsor");
        names.Add("Winthrop");
        names.Add("Woodcroft");
        names.Add("Wyoming");

        shuffle(names);
    }

    private static void shuffle(List<string> names) {
        for (int i = 0; i < names.Count; i++) {
            int idx = rng.Next(0, names.Count);
            String name = names[idx];
            names.RemoveAt(idx);
            names.Add(name);
        }
    }

    public static string getRandomName() {
        int idx = rng.Next(0, names.Count);
        string name = names[idx];
        names.RemoveAt(idx);
        return name;
    }

    public static int getRandomPopulation() {
        return rng.Next(MIN_POP_THOUSANDS, MAX_POP_THOUSANDS) * 1000 + rng.Next(0, 1000);
    }

    public static int getRandomPopulationLow() {
        int min = MIN_POP_THOUSANDS;
        int max = MIN_POP_THOUSANDS + getOneThirdOfPopulationRange();
        return rng.Next(min, max) * 1000 + rng.Next(0, 1000);
    }

    public static int getRandomPopulationAverage() {
        int min = MIN_POP_THOUSANDS + getOneThirdOfPopulationRange();
        int max = min + getOneThirdOfPopulationRange();
        return rng.Next(min, max) * 1000 + rng.Next(0, 1000);
    }

    public static int getRandomPopulationHigh() {
        int min = MAX_POP_THOUSANDS - getOneThirdOfPopulationRange();
        int max = MAX_POP_THOUSANDS;
        return rng.Next(min, max) * 1000 + rng.Next(0, 1000);
    }

    private static int getOneThirdOfPopulationRange() {
        return (MAX_POP_THOUSANDS - MIN_POP_THOUSANDS) / 3;
    }

    public static float getMaxChipAdoption() {
        return MAX_CHIP_ADOPTION * 0.1f;
    }

    public static float getRandomChipAdoptionLow() {
        int min = 0;
        int max = getOneThirdOfChipAdoptionRange();
        return rng.Next(min, max) * 0.1f;
    }

    public static float getRandomChipAdoptionAverage() {
        int min = getOneThirdOfChipAdoptionRange();
        int max = getOneThirdOfChipAdoptionRange() * 2;
        return rng.Next(min, max) * 0.1f;
    }

    public static float getRandomChipAdoptionHigh() {
        int min = getOneThirdOfChipAdoptionRange() * 2;
        int max = MAX_CHIP_ADOPTION;
        return rng.Next(min, max) * 0.1f;
    }

    private static int getOneThirdOfChipAdoptionRange() {
        return (MAX_CHIP_ADOPTION- MIN_CHIP_ADOPTION) / 3;
    }

    public static WealthLevel getRandomWealthLevel() {
        return wealthLevels[rng.Next(0, wealthLevels.Count)];
    }

    public static int getRandomHappiness() {
        return rng.Next(HappinessLevel.UNHAPPY.threshold + 10, HappinessLevel.HAPPY.threshold + 5);
    }
}