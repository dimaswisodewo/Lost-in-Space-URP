using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    public Rigidbody2D rb;
    public VariableJoystick variableJoystick;
    public GameObject particle;
    public int health = 3;
    public bool isAlive = true;
    private bool isInvincible;
    private bool dragging;
    private int bulletCount;
    [SerializeField] private float rotateSpeed = 90f;
    [SerializeField] private Transform gunPos;
    [SerializeField] private float shootSpeed = 400f;
    [SerializeField] private float blastSpeed = 10f;
    [SerializeField] private SpriteRenderer renderer;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        bulletCount = ObjectPooler.Instance.amountBulletToPool;
        UIManager.Instance.SetBulletText("x " + bulletCount);
    }

    private void FixedUpdate()
    {
        JoystickControl();
    }

    private void JoystickControl()
    {
        Vector3 direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
        rb.AddTorque(-direction.x * rotateSpeed * Time.fixedDeltaTime, ForceMode2D.Force);
    }

    private void Shoot()
    {
        // Get bullet from pool
        GameObject bullet = ObjectPooler.Instance.GetPooledBullet();
        bullet.SetActive(true);

        // Set bullet position to gun position
        bullet.transform.position = gunPos.position;

        // Add force to bullet
        Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();
        rbBullet.AddForce(-gunPos.transform.right * shootSpeed);
    }

    // Add force to player when shooting
    private void PlayerBlast()
    {
        rb.AddForce(transform.right * blastSpeed, ForceMode2D.Impulse);
    }

    public void AddHealth()
    {
        health += 1;
        UIManager.Instance.SetActiveHeart(health-1, true);
    }

    private void MinHealth()
    {
        health -= 1;
        UIManager.Instance.SetActiveHeart(health, false);
    }

    // Shoot Button event
    public void OnShootButtonClicked()
    {
        if (bulletCount > 0)
        {
            Shoot();
            PlayerBlast();
            bulletCount -= 1;
            UIManager.Instance.SetBulletText("x " + bulletCount);
            SoundManager.Instance.PlayShoot();
        }
    }

    public void ChangePlayerSpriteColor(COLOR colorEnum)
    {
        switch (colorEnum)
        {
            case COLOR.RED:
                renderer.color = new Color32(255, 102, 102, 255);
                break;

            case COLOR.WHITE:
                renderer.color = new Color32(255, 255, 255, 255);
                break;
        }
    }

    // make player invincible for 1 second after hurt
    private IEnumerator PlayerHurtToleration()
    {
        isInvincible = true;
        ChangePlayerSpriteColor(COLOR.RED);

        yield return new WaitForSeconds(1f);

        if (isAlive)
        {
            isInvincible = false;
            ChangePlayerSpriteColor(COLOR.WHITE);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // health will decrease if player collide with bullet or spike/increase if collide with heart
        if (health > 0 && !isInvincible)
        {
            if (collision.gameObject.tag == "Bullet" || collision.gameObject.tag == "Spike")
            {
                Vector3 particlePos = transform.position;
                particlePos.z = -10;

                Instantiate(particle, particlePos, Quaternion.identity);
                MinHealth();
                StartCoroutine(PlayerHurtToleration());
                SoundManager.Instance.PlayHurt();
            }
        }
    }

}

public enum COLOR
{
    WHITE,
    RED
}
