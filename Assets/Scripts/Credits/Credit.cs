using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Credit : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI role;
    [SerializeField] private TextMeshProUGUI personName;

    public void Initialize(string role, string personName) {
        this.role.text = role;
        this.personName.text = personName;
    }

}
