using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : Character
{
    [Header ("Player Stats")]
    [SerializeField]
    public float maxHealth = 100;
    [SerializeField]
    public float health;
    [SerializeField]
    public float maxStamina;
    [SerializeField]
    public float stamina;
    [SerializeField]
    float moveSpeed;


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

    [SerializeField]
    float timePerAttack;

    float timer;

    float distToGround;
    float fallDamage;

    private CharacterController controller;
    private Camera camera;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;

    bool isRunning = false;
    bool isMoving;
    bool canRun = true;
    bool isBlocking = false;

    private AudioSource audio;
    [SerializeField]
    AudioClip[] clips;

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
        audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (health <= 0)
        {
            anim.ResetTrigger("Die");
            anim.SetTrigger("Die");
        }
        else
        {
            timer += Time.deltaTime;
            groundedPlayer = IsGrounded();
            Gravity();
            Movement();
            if (Input.GetButtonDown("Jump") && groundedPlayer)
            {
                Jump();
            }

            // change current equiped item
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (currentItem == equipableItems[1])
                    equipableItems[1].Sheath();
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
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (timer > timePerAttack)
                {
                    if (currentItem != null)
                        currentItem.Attack();
                    timer = 0;
                }

            }
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

        //Block while sword drawn
        if(currentItem == equipableItems[1])
        {
            if (Input.GetKey(KeyCode.Mouse1))
            {
                isBlocking = true;
            }
            else isBlocking = false;
        }
        controller.Move(move.normalized * Time.deltaTime * moveSpeed);
        anim.SetBool("Idle", !isMoving);
        anim.SetFloat("Speed", (int)(Input.GetAxis("Vertical") * moveSpeed));
        anim.SetFloat("Strafe", Input.GetAxis("Horizontal"));
        anim.SetBool("Grounded", groundedPlayer);
        anim.SetBool("IsBlocking", isBlocking);

    }

    void Jump()
    {
        if (stamina > jumpCost)
        {
            anim.ResetTrigger("Jump");
            anim.SetTrigger("Jump");
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            stamina -= jumpCost;
            audio.clip = clips[4];
            audio.Play();
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
            stamina -= attackCost;
            audio.clip = clips[5];
            audio.Play();
        }
    }

    public override void TakeHit(float damage)
    {
        if(isBlocking)
        {
            anim.ResetTrigger("TakeHit");
            anim.SetTrigger("TakeHit");
            audio.clip = clips[1];
            audio.Play();
        }
        else
        {
            health -= damage;
            audio.clip = clips[3];
            audio.Play();
        }
    }
}
