using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header ("Player Stats")]
    [SerializeField]
    float maxHealth = 100;
    [SerializeField]
    float health;
    [SerializeField]
    float maxStamina;
    [SerializeField]
    float stamina;
    [SerializeField]
    float moveSpeed;

    [SerializeField]
    EquipableItem[] equipableItems;
    public EquipableItem currentItem;
    public Transform itemHandTransform;

    [Header("Value use rates")]
    [SerializeField]
    float staminaUseRate = 10;
    [SerializeField]
    float staminaRegenRate = 10;
    [SerializeField]
    float maxWalkSpeed = 5;
    [SerializeField]
    float maxRunSpeed = 10;
    [SerializeField]
    float acceleration = 0.1f;
    [SerializeField]
    float safeFallLimit = 10;
    [SerializeField]
    float jumpCost;
    [SerializeField]
    float attackCost;
    [SerializeField]
    float blockCost;


    float distToGround;
    float fallDamage;

    private CharacterController controller;
    private Camera camera;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;

    [Header("UI elements")]
    public Image healthBar;
    public Image staminaBar;

    public Animator anim;
    

    private void Awake()
    {
        GameObject.DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        health = maxHealth;
        stamina = maxStamina;;
        controller = GetComponent<CharacterController>();
        camera = FindObjectOfType<Camera>();
        distToGround = controller.bounds.extents.y;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Debug.Log(currentItem);
        //healthBar.fillAmount = health / maxHealth;
        //staminaBar.fillAmount = stamina / maxStamina;
        groundedPlayer = IsGrounded();
        Gravity();
        Movement();
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            Jump();
        }

        // change current equiped item
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (currentItem == equipableItems[1])
                equipableItems[1].Sheath();
            else if (currentItem != equipableItems[0])
                equipableItems[0].Draw();

        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (currentItem == equipableItems[0])
                equipableItems[0].Sheath();
            equipableItems[1].Draw();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (currentItem == equipableItems[0])
                equipableItems[0].Sheath();
            equipableItems[1].Sheath();
        }
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(currentItem != null)
                currentItem.Attack();
        }

        anim.SetBool("HasEquiped", currentItem != null);
        if (health <= 0)
        {
            anim.ResetTrigger("Die");
            anim.SetTrigger("Die");
        }
    }

    void EquipGun()
    {
        currentItem = equipableItems[0];
        currentItem.Equip();
    }
    void EquipSword()
    {
        currentItem = equipableItems[1];
        currentItem.Equip();
    }

    void UnequipGun()
    {
        currentItem = equipableItems[0];
        currentItem.Unequip();
    }
    void UnequipSword()
    {
        currentItem = equipableItems[1];
        currentItem.Unequip();
    }

    void Movement()
    {
        bool isRunning = false;
        bool isMoving;
        bool canRun = true;
        if (stamina <= 0)
        {
            canRun = false;
        }
        else if (stamina > 20)
        {
            canRun = true;
        }

        if (Input.GetKey(KeyCode.LeftShift) & canRun)
        {
            isRunning = true;
            if (stamina > 0)
            {
                stamina -= staminaUseRate * Time.deltaTime;
            }
        }
        else
        {
            isRunning = false;
            if (stamina < maxStamina)
            {
                stamina += staminaRegenRate * Time.deltaTime;
            }
        }

        groundedPlayer = IsGrounded();

        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        transform.rotation = Quaternion.Euler(0f, camera.transform.eulerAngles.y, 0f);
        Vector3 move = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");

        if (move.x != 0 | move.z != 0)
            isMoving = true;
        else isMoving = false;

        if (isRunning)
        {
            if (moveSpeed < maxRunSpeed)
                moveSpeed += acceleration;
        }
        else if (isMoving)
        {
            if (moveSpeed < maxWalkSpeed)
                moveSpeed += acceleration;
            else if (moveSpeed > maxWalkSpeed)
                moveSpeed -= acceleration;
        }
        else if (!isMoving)
        {
            if (moveSpeed > 0)
                moveSpeed -= acceleration;
        }

        controller.Move(move.normalized * Time.deltaTime * moveSpeed);
        anim.SetFloat("Speed", moveSpeed);
        anim.SetBool("Grounded", groundedPlayer);

    }

    void Jump()
    {
        if (stamina > 20)
        {
            anim.ResetTrigger("Jump");
            anim.SetTrigger("Jump");
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            stamina -= 20;
        }
    }

    void Gravity()
    {
        //player falls with gravity
        playerVelocity.y += gravityValue * Time.deltaTime;

        if (playerVelocity.y < -1)
            anim.SetBool("FreeFall", true);
        else
            anim.SetBool("FreeFall", false);
        controller.Move(playerVelocity * Time.deltaTime);

        // if player falls to fast take damage
        if (playerVelocity.y < -safeFallLimit)
        {
            fallDamage = playerVelocity.y;
        }
        if (fallDamage < 0 & groundedPlayer)
        {
            health += fallDamage;
            fallDamage = 0;
        }
    }
    bool IsGrounded() 
    {
        return Physics.Raycast(transform.position, -Vector3.up,out RaycastHit hit, distToGround + 0.1f);
    }

    void Attack()
    {
        if(stamina > attackCost)
        {
            currentItem.Attack();
        }
    }

    void Block()
    {
        if(stamina > blockCost)
        {
            //do stuff
        }
    }
    void TakeHit()
    {

    }
}
