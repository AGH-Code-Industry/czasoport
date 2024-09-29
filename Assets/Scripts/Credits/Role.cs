using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Role : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI role;

    public void Initialize(string role) {
        this.role.text = role;
    }
}
