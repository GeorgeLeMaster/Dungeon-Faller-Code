using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class PlayerCollision : MonoBehaviour
{
    public int health;
    public int maxDistance;

    public float goldAmnt;
    public float maxArmour;
    public float currentArmour;
    public float maxAirDash;
    public float currentAirDash;

    public bool chestDash;

    private Rigidbody2D rb;

    public float bounceForce;
    public float airDashCooldown;

    public GameObject HPbar;
    public GameObject armourBar;
    public GameObject airDashBar;
    public GameObject playAgainButton;
    public GameObject coinText;
    public GameObject gameOverUI;

    public TextMeshProUGUI armourCounter;
    public TextMeshProUGUI distance;
    public TextMeshProUGUI maxDistanceText;

    public GameObject knightSystem;

    // Start is called before the first frame update
    void Start()
    {
        LoadPlayer();

        rb = gameObject.GetComponent<Rigidbody2D>();

        gameOverUI.SetActive(false);

        if (currentArmour >= 1)
        {
            armourBar.SetActive(true);
        }
        else
        {
            armourBar.SetActive(false);
        }

        if (maxAirDash >= 1 && health > 0 && airDashBar != null)
        {
            airDashBar.SetActive(true);
        }
        else
        {
            currentAirDash = 0;
            airDashBar.SetActive(false);
        }

        maxDistanceText.text = maxDistance + "M";


    }

    void Update()
    {
        if (health <= 0)
        {
            var gameMusic = Camera.main.GetComponent<AudioSource>();
            gameMusic.volume = 0;


        }

        if (airDashCooldown > 0)
        {
            airDashCooldown -= Time.deltaTime;
        }
        else
        {
            airDashCooldown = 0;
        }

        if (airDashCooldown >= 1f && knightSystem != null)
        {
            knightSystem.SetActive(true);
        }
        else
        {
            if (knightSystem != null)
            knightSystem.SetActive(false);

        }

        if (maxDistanceText != null)
        maxDistanceText.text = maxDistance + "M";

        // Determine max distance
        if (Mathf.RoundToInt(Vector3.Distance(gameObject.transform.position, new Vector3(0, 0, 0))) > maxDistance)
        {
            maxDistance = Mathf.RoundToInt(Vector3.Distance(gameObject.transform.position, new Vector3(0, 0, 0)));
        }

        // Set max distance text
        if (Mathf.RoundToInt(Vector3.Distance(gameObject.transform.position, new Vector3(0, 0, 0))) >= 0 && gameObject.transform.position.y <= 0 && distance != null)
        {
            distance.text = Mathf.RoundToInt(Vector3.Distance(gameObject.transform.position, new Vector3(0, 0, 0))) + "M";
        }

        // set coin text
        if (coinText != null)
        coinText.GetComponent<TextMeshProUGUI>().text = goldAmnt.ToString() + " GOLD";

        // set armour counter
        if (armourCounter != null)
        armourCounter.text = currentArmour + " ARMOUR";

        Debug.Log(Mathf.RoundToInt(Vector3.Distance(gameObject.transform.position, new Vector3(0, 0, 0))));

        HPbar.GetComponent<Image>().fillAmount = (health / 4f);

        if (maxArmour >= 1)
        {
            armourBar.GetComponent<Image>().fillAmount = (currentArmour / maxArmour);
        }

        if (maxAirDash >= 1 && airDashBar != null)
        {
            airDashBar.GetComponent<Image>().fillAmount = (currentAirDash / maxAirDash);
        }

        // Air dash
        if (health > 0)
        {
            if (Input.GetKeyDown(KeyCode.A) && Input.GetKey(KeyCode.Space) && currentAirDash >= 1 && airDashCooldown == 0)
            {
                rb.velocity = new Vector2(0, -1f);
                rb.AddForce(Vector3.left * 100000 * Time.deltaTime);
                currentAirDash -= 1;
                airDashCooldown = 1.25f;
                FindObjectOfType<AudioManager>().Play("AirDash");

            }
            if (Input.GetKeyDown(KeyCode.D) && Input.GetKey(KeyCode.Space) && currentAirDash >= 1 && airDashCooldown == 0)
            {
                rb.velocity = new Vector2(0, -1f);
                rb.AddForce(Vector3.right * 100000 * Time.deltaTime);
                currentAirDash -= 1;
                airDashCooldown = 1.25f;
                FindObjectOfType<AudioManager>().Play("AirDash");

            }

            if (Input.GetKeyDown(KeyCode.W) && Input.GetKey(KeyCode.Space) && currentAirDash >= 1 && airDashCooldown == 0)
            {
                rb.velocity = new Vector2(0, -1f);
                rb.AddForce(Vector3.up * 120000 * Time.deltaTime);
                currentAirDash -= 1;
                airDashCooldown = 1.25f;
                FindObjectOfType<AudioManager>().Play("AirDash");

            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        var dir = (gameObject.transform.position - collision.transform.position);

        if (collision.transform.CompareTag("Obsticle"))
        {
            rb.AddForce(dir.normalized * bounceForce);
            goldAmnt += Mathf.RoundToInt(1 * ((Vector3.Distance(gameObject.transform.position, new Vector3(0, 0, 0)))/100));

            FindObjectOfType<AudioManager>().Play("Player Impact");
        }

        if (collision.transform.gameObject.tag == "Enemy")
        {
            if (currentArmour > 0)
            {
                currentArmour -= 1;
            }
            else
            {
                if (health == 1)
                {
                    FindObjectOfType<AudioManager>().Play("RecordScratch");

                }

                health -= 1;

            }

            FindObjectOfType<AudioManager>().Play("Enemy Impact");
            rb.AddForce(dir.normalized * bounceForce);
            Destroy(collision.gameObject);
        }

        if (collision.transform.CompareTag("Wall"))
        {
            FindObjectOfType<AudioManager>().Play("Wall Impact");


            if (collision.transform.position.x > gameObject.transform.position.x)
            {
                rb.AddForce(Vector2.left * bounceForce / 2);
            }

            if (collision.transform.position.x < gameObject.transform.position.x)
            {
                rb.AddForce(Vector2.right * bounceForce / 2);
            }

        }

        if (collision.transform.CompareTag("Chest"))
        {
            if (maxAirDash > 0 && currentAirDash < maxAirDash && chestDash == true)
            {
                currentAirDash += 1;
            }

            goldAmnt += (Random.Range(5, 20) + (Mathf.RoundToInt(Vector3.Distance(gameObject.transform.position, new Vector3(0, 0, 0)))/10));
            Debug.Log(goldAmnt);
            rb.AddForce(dir.normalized * bounceForce);
            Destroy(collision.gameObject);

            FindObjectOfType<AudioManager>().Play("Chest Impact");
            FindObjectOfType<AudioManager>().Play("CoinsRattle");


        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("FinalTrigger"))
        {
            gameOverUI.SetActive(true);
            Destroy(collision.gameObject);

        }

        if (collision.transform.CompareTag("LowReward"))
        {
            goldAmnt += 100f;
            Destroy(collision.gameObject);
            FindObjectOfType<AudioManager>().Play("Low Reward");

        }

        if (collision.transform.CompareTag("MediumReward"))
        {
            goldAmnt += 200f;
            Destroy(collision.gameObject);
            FindObjectOfType<AudioManager>().Play("Medium Reward");

        }

        if (collision.transform.CompareTag("HighReward"))
        {
            goldAmnt += 1000f;
            Destroy(collision.gameObject);
            FindObjectOfType<AudioManager>().Play("High Reward");

        }
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        goldAmnt = data.gold;
        maxArmour = data.armour;
        currentArmour = maxArmour;
        maxDistance = data.longestDistance;
        maxAirDash = data.airDash;
        currentAirDash = maxAirDash;
        chestDash = data.chestDash;
    }

}
