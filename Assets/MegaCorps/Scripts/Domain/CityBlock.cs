using UnityEngine;
using System.Collections;

public class CityBlock {

    public Player owner { get; set; }
    public string name { get; set; }
    public int income { get; set; }

    public CityBlock(string name, int income) {
        this.name = name;
        this.income = income;
    }
}
