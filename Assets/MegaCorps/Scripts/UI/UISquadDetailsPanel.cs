using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class UISquadDetailsPanel : MonoBehaviour {

    private UIState uiState;

    private Text squadName;
    private Text orders;
    private Text agents;

    void Start() {
        this.uiState = UIState.getInstance();
        UIState.getInstance().SquadChanged += OnSquadChanged;

        this.squadName = transform.FindChild("SquadName").GetComponent<Text>();
        this.orders = transform.FindChild("Orders").GetComponent<Text>();
        this.agents = transform.FindChild("Agents").GetComponent<Text>();

        this.gameObject.SetActive(false);
    }

    void Update() {
        if (uiState.getActiveSquad() != null) {
            this.updateSquadOrders(uiState.getActiveSquad());
        }
    }

    private void OnSquadChanged(object sender, UIState.SquadChangedEventArgs args) {
        if (args.squad != null) {
            this.gameObject.SetActive(true);

            Squad squad = args.squad;
            this.squadName.text = squad.name;
            this.agents.text = "" + squad.agentCount();
            this.updateSquadOrders(squad);
        } else {
            this.gameObject.SetActive(false);
        }
    }

    private void updateSquadOrders(Squad squad) {
        if (squad.command != null) {
            if (squad.command.GetType() == typeof(MoveCommand)) {
                this.orders.text = "Moving";
            } else if (squad.command.GetType() == typeof(ControlCommand)) {
                this.orders.text = "Hacking";
            } else if (squad.command.GetType() == typeof(ChipCommand)) {
                this.orders.text = "CHIP";
            } else {
                this.orders.text = "None";
            }
        } else {
            this.orders.text = "None";
        }
    }
    
    public void OnControlClicked() {
        City.getInstance().issueControlOrder(uiState.getActiveSquad());
    }

    public void OnChipClicked() {
        City.getInstance().issueChipOrder(uiState.getActiveSquad());
    }
}
