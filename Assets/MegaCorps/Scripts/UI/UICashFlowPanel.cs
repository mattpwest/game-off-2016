using UnityEngine;
using UnityEngine.UI;

public class UICashFlowPanel : MonoBehaviour {

    private UIState uiState;
    private Text cashTotal;
    private Text cashChange;
    private Text cashIncomes;
    private Text cashExpenses;

    private City city;
    private Player player;

	void Start () {
        uiState = UIState.getInstance();
        uiState.PlayerChanged += OnPlayerChanged;

        this.cashTotal = transform.FindChild("CashTotal").GetComponent<Text>();
        this.cashChange = transform.FindChild("CashChange").GetComponent<Text>();
        this.cashIncomes = transform.FindChild("Incomes").GetComponent<Text>();
        this.cashExpenses = transform.FindChild("Expenses").GetComponent<Text>();

        city = City.getInstance();
    }

    private void OnPlayerChanged(object sender, UIState.PlayerChangedEventArgs args) {
        player = args.player;

        cashTotal.text = "$ " + player.cash.ToString();
    }

    void Update () {
	    if (player != null) {
            int income = city.calculateIncomeFromOwnedCityBlocks(player);
            int expenses = city.calulateSquadMaintenanceCosts(player);

            cashChange.text = "$ " + (income - expenses);
            cashIncomes.text = "$ " + income;
            cashExpenses.text = "$ " + expenses;
        }
	}
}
