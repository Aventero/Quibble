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
    private InputManager inputManager;
    private IEnumerator attackCoroutine;
    private IEnumerator cooldown;
    private bool onCooldown = false;
    private GameObject spawnedSword;
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
            radius = PlayerStats.Instance.Range / 2.0f + SwordDistance;
            SwordCollider.size = new Vector2(1f, radius * 15f);
            spawnedSword.transform.localScale = new Vector3(spawnedSword.transform.localScale.x * PlayerStats.Instance.Range, spawnedSword.transform.localScale.y, spawnedSword.transform.localScale.z);
            TrailRenderer trailRenderer = spawnedSword.GetComponentInChildren<TrailRenderer>();
            trailRenderer.widthMultiplier = PlayerStats.Instance.Range;

            // Attack From Right TO left when moving Left
            if (StateManager.MoveDirection == -1)
            {
                attackCoroutine = AttackRightCoroutine(SwingSpeed, spawnedSword, Player.eulerAngles.z, Player.eulerAngles.z + PlayerStats.Instance.Angle);
                SwordCollider.offset = new Vector2(0f, radius * 10f * 0.3f); // Shift backwards
            }
            else
            {
                attackCoroutine = AttackLeftCoroutine(SwingSpeed, spawnedSword, Player.eulerAngles.z + 180f, Player.eulerAngles.z + 180f - PlayerStats.Instance.Angle);
                SwordCollider.offset = new Vector2(0f, -radius * 10f * 0.3f); // Shift forward
            }
            StartCoroutine(attackCoroutine);
        }
    }

    private IEnumerator AttackRightCoroutine(float swingSpeed, GameObject sword, float startAngle, float endAngle)
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

    private IEnumerator AttackLeftCoroutine(float swingSpeed, GameObject sword, float startAngle, float endAngle)
    {
        for (float i = startAngle; i >= endAngle; i -= swingSpeed * Time.deltaTime)
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
}
    