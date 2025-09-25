using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoToScene : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private Button button;

    private void Awake()
    {
        if (button == null) return;
        button.onClick.AddListener(HandleOnButtonClicked);
    }

    private void HandleOnButtonClicked()
    {
        if (button == null) return;
        button.interactable = false;
        SimpleSceneManager.Instance.LoadSceneByName(sceneName);
    }
}
