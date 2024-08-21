using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DamagebleCharacter : MonoBehaviour, IDamageale
{
    public GameObject healthText;
    public bool isInvincibleEnable = false;
    public float invincibilityTime = 0.25f;
    Animator animator;
    Rigidbody2D rb;
    bool isAlive = true;
    Collider2D physicsCollider;
    private float invincibleTimeElapsed = 0f;
    public float Health
    {
        set
        {
            if (value < health)
            {
                animator.SetTrigger("hit");
                RectTransform textTransform = Instantiate(healthText).GetComponent<RectTransform>();
                textTransform.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position);  
                Canvas canvas = GameObject.FindObjectOfType<Canvas>();
                textTransform.SetParent(canvas.transform);
            }
            health = value;
            if (health <= 0)
            {
                animator.SetBool("isAlive", false);
                Targetable = false;
                
                if (gameObject.CompareTag("Player"))
                {
                    LoadScene("FailScreen");
                }
                if (gameObject.CompareTag("Enemy"))
                {

                    StartCoroutine( OnObjectDestroyed());
                }
            }
        }
        get
        {
            return health;
        }
    }

    public bool Targetable { get  { return targetable; }
        set {
            targetable = false;
            physicsCollider.enabled = value;
        } }

    public bool Invincible
    {
        get
        {
            return invincible;
        }
        set
        {
            invincible = value;
            if(invincible == true)
            {
                invincibleTimeElapsed = 0f;
            }
        }
    }

    public float health = 3;
    bool targetable = true;
    public bool invincible = false;
    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isAlive", isAlive);
        rb = GetComponent<Rigidbody2D>();
        physicsCollider = GetComponent<Collider2D>();
    }



    public void OnHit(float damage, Vector2 knockback)
    {
        if (!isInvincibleEnable || !Invincible)
        {
            Health -= damage;
            rb.AddForce(knockback, ForceMode2D.Impulse);
            if (isInvincibleEnable)
            {
                Invincible = true;
            }
        }
    }

    public void OnHit(float damage)
    {
        if (!isInvincibleEnable || !Invincible)
        {
            Health -= damage;
        }
    }

    public IEnumerator OnObjectDestroyed()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
    private void FixedUpdate()
    {
        if (Invincible)
        {
            invincibleTimeElapsed += Time.deltaTime;
            if(invincibleTimeElapsed > invincibilityTime)
            {
                Invincible = false;
            }
        }
    }
    public void LoadScene(string sceneName)
    {
        ScreenManager.previousSceneName = SceneManager.GetActiveScene().name; // Lưu tên của scene hiện tại
        SceneManager.LoadScene(sceneName); // Load scene mới
    }
    public void ItemHealth()
    {
        Health += 1;
    }
}
