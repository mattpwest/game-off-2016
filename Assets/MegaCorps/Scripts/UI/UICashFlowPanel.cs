using UnityEngine;
using UnityEngine.UI;

public class UICashFlowPanel : MonoBehaviour {

    private Text cashTotal;
    private Text cashChange;
    private Text cashIncomes;
    private Text cashExpenses;

	void Start () {
        this.cashTotal = transform.FindChild("CashTotal").GetComponent<Text>();
        this.cashChange = transform.FindChild("CashChange").GetComponent<Text>();
        this.cashIncomes = transform.FindChild("Incomes").GetComponent<Text>();
        this.cashExpenses = transform.FindChild("Expenses").GetComponent<Text>();

        UIState.getInstance().PlayerChanged += OnPlayerChanged;
    }

    private void OnPlayerChanged(object sender, UIState.PlayerChangedEventArgs e) {
        Player player = e.player;

        cashTotal.text = "$ " + player.cash.ToString();
        cashChange.text = "$ x";
        cashIncomes.text = "$ x";
        cashExpenses.text = "$ x";
    }

    void Update () {
	
	}
}
