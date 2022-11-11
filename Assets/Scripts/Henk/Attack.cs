using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Transform Player;
    public GameObject Sword;
    public float CooldownTime = 1.0f;
    public float SwingSpeed = 5.0f;
    public float radius = 1.0f;
    public float Angle = 720.0f;
    private InputManager inputManager;
    private IEnumerator attackCoroutine;
    private IEnumerator cooldown;
    private bool onCooldown = false;
    private GameObject spawnedSword;

    // Start is called before the first frame update
    void Start()
    {
        inputManager = GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inputManager.Attack && !onCooldown)
        {
            onCooldown = true;
            cooldown = Cooldown(CooldownTime);
            StartCoroutine(cooldown);

            spawnedSword = Instantiate(Sword, Player.position, Quaternion.identity);
            attackCoroutine = AttackCoroutine(SwingSpeed, spawnedSword, Player.eulerAngles.z, Player.eulerAngles.z + Angle);
            StartCoroutine(attackCoroutine);
        }
    }

    private IEnumerator AttackCoroutine(float swingSpeed, GameObject sword, float startAngle, float endAngle)
    {
        for (float i = startAngle; i <= endAngle; i += swingSpeed)
        {
            //sword.transform.RotateAround(Player.transform.position, Vector3.forward, 1 * swingSpeed);   // Per frame, "angle" amount of rotation
            // JUST USE LOOK AT. 
            // Sword has to rotate in the player direction UFF
            sword.transform.position = new Vector2(Player.position.x + radius * Mathf.Cos(Mathf.Deg2Rad * i), Player.position.y + radius * Mathf.Sin(Mathf.Deg2Rad * i));
            yield return new WaitForEndOfFrame();
        }
        Destroy(spawnedSword);
        yield return new WaitForEndOfFrame();
    }

    private IEnumerator Cooldown(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        onCooldown = false;
    }
}
