using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    PlayerController player;
    [SerializeField]
    Image staminaBar;
    [SerializeField]
    Image healthBar;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = player.health / player.maxHealth;
        staminaBar.fillAmount = player.stamina / player.maxStamina;
    }
}
