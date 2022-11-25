using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Transform Player;
    public GameObject Sword;
    public float SwingSpeed = 5.0f;
    public float SwordDistance = 0.25f;
    public float CooldownTime = 1.0f;
    private float radius = 1.0f;
    public float Angle = 720.0f;
    private InputManager inputManager;
    private IEnumerator attackCoroutine;
    private IEnumerator cooldown;
    private bool onCooldown = false;
    private GameObject spawnedSword;
    public float SwordScale = 1.0f;
    private BoxCollider2D SwordCollider;

    // Start is called before the first frame update
    void Start()
    {
        inputManager = GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (StateManager.IsDead || StateManager.InMenu)
            return;
        Attacking();

    }

    private void Attacking()
    {
        if (inputManager.Attack && !onCooldown && spawnedSword == null)
        {
            FindObjectOfType<AudioManager>().Play("Sword");
            onCooldown = true;
            cooldown = Cooldown(CooldownTime);
            StartCoroutine(cooldown);

            // Spawn sword
            spawnedSword = Instantiate(Sword, Player.position, Quaternion.identity);
            SwordCollider = spawnedSword.GetComponent<BoxCollider2D>();

            // Change Scaling
            radius = SwordScale / 2.0f + SwordDistance;
            SwordCollider.size = new Vector2(1f, radius * 15f);
            SwordCollider.offset = new Vector2(0f, radius * 10f * 0.3f); // Shift backwards
            spawnedSword.transform.localScale = new Vector3(spawnedSword.transform.localScale.x * SwordScale, spawnedSword.transform.localScale.y, spawnedSword.transform.localScale.z);
            TrailRenderer trailRenderer = spawnedSword.GetComponentInChildren<TrailRenderer>();
            trailRenderer.widthMultiplier = SwordScale;
            attackCoroutine = AttackCoroutine(SwingSpeed, spawnedSword, Player.eulerAngles.z, Player.eulerAngles.z + Angle);
            StartCoroutine(attackCoroutine);
        }
    }

    private IEnumerator AttackCoroutine(float swingSpeed, GameObject sword, float startAngle, float endAngle)
    {
        for (float i = startAngle; i <= endAngle; i += swingSpeed * Time.deltaTime)
        {
            Vector3 dirToSword = sword.transform.position - Player.position;
            float angle = Mathf.Atan2(dirToSword.y, dirToSword.x);
            sword.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Rad2Deg * angle));
            sword.transform.position = new Vector2(Player.position.x + radius * Mathf.Cos(Mathf.Deg2Rad * i), Player.position.y + radius * Mathf.Sin(Mathf.Deg2Rad * i));
            yield return new WaitForEndOfFrame();
        }
        Destroy(spawnedSword);
        spawnedSword = null;
        yield return new WaitForEndOfFrame();
    }

    private IEnumerator Cooldown(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        onCooldown = false;
    }

    public void UpgradeSwordLength(float newLength)
    {
        SwordScale = newLength;
    }

    public void UpgradeAttackAngle(float newAngle)
    {
        Angle = newAngle;
    }

    public void UpgradeCooldown(float newCooldown)
    {
        CooldownTime = newCooldown;
    }
}
