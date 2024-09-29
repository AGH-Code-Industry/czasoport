using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class TeamData
{
    public string[] Lead;
    public string[] Team;
}

[System.Serializable]
public class CreatorsData
{
    public TeamData Programmers;
    public TeamData Designers;
    public TeamData Artists;
    public TeamData SoundDesign;
}

public class CreditsManager : MonoBehaviour {
    [SerializeField] private GameObject credit;
    [SerializeField] private GameObject role;
    [SerializeField] private Transform container;
    [SerializeField] private Button quitButton;

    private RectTransform rectTransform;
    private float moveSpeed = 100f;

    private void Start() {
        var peopleJson = Resources.Load<TextAsset>("Credits/Creators");
        var creatorsData = JsonUtility.FromJson<CreatorsData>(peopleJson.text);

        quitButton.onClick.AddListener(QuitGame);
        SpawnRole(creatorsData.Programmers, "Programmers");
        SpawnRole(creatorsData.Designers, "Designers");
        SpawnRole(creatorsData.Artists, "Artists");
        SpawnRole(creatorsData.SoundDesign, "Sound Designers");
        rectTransform = GetComponent<RectTransform>();
        MoveUp();
    }

    private void MoveUp() {
        float targetY = rectTransform.anchoredPosition.y + moveSpeed;

        LeanTween.moveY(rectTransform, targetY, 1f).setOnComplete(MoveUp);
    }

    private void SpawnRole(TeamData teamData, string roleName) {
        Instantiate(role, container).GetComponent<Role>().Initialize(roleName);
        foreach (var person in teamData.Lead) {
            Instantiate(credit, container).GetComponent<Credit>().Initialize("Lead", person);
        }
        foreach (var person in teamData.Team) {
            Instantiate(credit, container).GetComponent<Credit>().Initialize("Team", person);
        }
    }

    private void QuitGame() {
        UnityEngine.Application.Quit();
    }
}
