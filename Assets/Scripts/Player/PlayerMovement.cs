using UnityEngine;
using static Wabubby.Extensions;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField, Range(0f, 30f)] public float speed = 6;

    bool stopControl = false;
    Rigidbody2D physics;
    PlayerInputCoordinator input;

     //Shield-related
    public GameObject shieldObject;
    private bool shieldActive = false;
    private float shieldTimer = 0f;
    [SerializeField] private float shieldDuration = 5f;

    void Start()
    {
        input = transform.root.GetComponent<PlayerInputCoordinator>();
        physics = GetComponent<Rigidbody2D>();
    }

    protected void OnDisable()
    {
        stopControl = false;
    }

    void Update()
    {
        physics.velocity = speed * input.Movement;

        
        if (shieldActive)
        {
            shieldTimer -= Time.deltaTime;
            GetComponent<PlayerHealth>().canDamage = false;
            if (shieldTimer <= 0)
            {
                DeactivateShield();
            }
        }
    }

    
    public void ActivateShield()
    {
        shieldActive = true;
        shieldObject.SetActive(true);
        shieldTimer = shieldDuration;
        Debug.Log("Shield Activated!");
       
    }

    private void DeactivateShield()
    {
        shieldActive = false;
        shieldObject.SetActive(false);
        GetComponent<PlayerHealth>().canDamage = true;
        Debug.Log("Shield Deactivated!");
       
    }

    
    public bool IsShieldActive()
    {
        return shieldActive;
    }
}