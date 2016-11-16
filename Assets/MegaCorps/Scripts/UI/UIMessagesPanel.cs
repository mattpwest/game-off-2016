using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class UIMessagesPanel : MonoBehaviour {

    Text messageText;

	void Start () {
        messageText = transform.FindChild("Messages").GetComponent<Text>();
        UIState.getInstance().PlayerChanged += OnPlayerChanged;

        this.gameObject.SetActive(false);
    }
    
    void Update () {
	
	}

    public void OnClose() {
        gameObject.SetActive(false);
    }

    private void OnPlayerChanged(object sender, UIState.PlayerChangedEventArgs args) {
        if (args != null) {
            List<String> messages = City.getInstance().getMessages(args.player);

            String totalMessage = "";
            int i = 1;
            foreach (String msg in messages) {
                totalMessage += i + ". " + msg + "\n";
                i++;
            }

            messageText.text = totalMessage;
        }
    }
}
