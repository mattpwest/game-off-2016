using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class UIEmailButton : MonoBehaviour {

    public Transform messagesPanel;
    LTDescr flashingTween;
    Text buttonText;
    Image buttonImage;

	void Start () {
        UIState.getInstance().PlayerChanged += OnPlayerChanged;

        flashingTween = LeanTween.alpha(this.GetComponentInParent<RectTransform>(), 0.6f, 0.5f)
            .setEase(LeanTweenType.easeInOutBack)
            .setLoopType(LeanTweenType.pingPong)
            .pause();

        buttonText = this.GetComponentInChildren<Text>();
        buttonImage = this.GetComponent<Image>();
    }
    
    void Update () {
	
	}

    private void OnPlayerChanged(object sender, UIState.PlayerChangedEventArgs args) {
        stopTween();

        if (args.player != null) {
            List<String> messages = City.getInstance().getMessages(args.player);

            buttonText.text = "Email (" + messages.Count + ")";

            if (messages.Count > 0) {
                startTween();
            }
        }
    }

    public void OnClick() {
        stopTween();
        messagesPanel.gameObject.SetActive(true);
    }

    private void stopTween() {
        buttonImage.color = new Color(buttonImage.color.r, buttonImage.color.g, buttonImage.color.b, 1.0f);
        buttonText.color = new Color(buttonText.color.r, buttonText.color.g, buttonText.color.b, 1.0f);
        LeanTween.pause(flashingTween.id);
    }

    private void startTween() {
        LeanTween.resume(flashingTween.id);
    }
}
