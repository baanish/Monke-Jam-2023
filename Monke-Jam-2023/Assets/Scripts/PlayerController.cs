using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public GameObject healthBarImage;

    [SerializeField]
    public LayerMask pickupLayer;

    [SerializeField]
    public long monkeTimeLimit = 60000;

    [SerializeField]
    public long monkeTimeStart = 0;

    [SerializeField]
    public long monkeTimeCurrent = 0;

    [SerializeField]
    public bool monkeStarted = false;

    [SerializeField]
    public bool monkeEnded = false;

    [SerializeField]
    public GameObject deathPanel;

    [SerializeField]
    public GameObject deathPanelText;

    [SerializeField]
    public long deathTime = 0;

    [SerializeField]
    public long deathPanelAnimationTime = 1000;

    [SerializeField]
    public int score = 0;

    [SerializeField]
    public GameObject scoreText;

    [SerializeField]
    public LayerMask enemyLayer;

    [SerializeField]
    public LayerMask personLayer;

    [SerializeField]
    public List<GameObject> punchPlanes;

    [SerializeField]
    public float lastPunchTime = 0f;

    [SerializeField]
    public float punchCooldown = 0.2f;

    [SerializeField]
    Animator monkeAnim;

    [SerializeField]
    public GameObject deathScoreText;

    [SerializeField]
    public GameObject BGM;

    [SerializeField]
    public GameObject deathBGM;

    [SerializeField]
    public GameObject eatSound;

    [SerializeField]
    public GameObject lightPunchSound;

    [SerializeField]
    public GameObject heavyPunchSound;


    void Start()
    {
        healthBarImage.GetComponent<Image>().fillAmount = 1f;
        if (!monkeStarted)
        {
            monkeStarted = true;
        }
        monkeTimeStart = System.DateTime.Now.Ticks / System.TimeSpan.TicksPerMillisecond;
    }


    // Update is called once per frame
    void Update()
    {
        // Check if the player has started the monke
        if (monkeStarted) {
            // decrement the health bar over time
            monkeTimeCurrent = System.DateTime.Now.Ticks / System.TimeSpan.TicksPerMillisecond;
            healthBarImage.GetComponent<Image>().fillAmount = 1f - Mathf.Clamp((monkeTimeCurrent - monkeTimeStart) / (float)monkeTimeLimit, 0f, 1f);
            if (healthBarImage.GetComponent<Image>().fillAmount <= 0f)
            {
                // if the health bar is empty, end the monke
                monkeEnded = true;
                if (!deathPanel.activeSelf) {
                    deathPanel.SetActive(true);
                    deathTime = System.DateTime.Now.Ticks / System.TimeSpan.TicksPerMillisecond;
                    deathScoreText.GetComponent<TextMeshProUGUI>().text = "Score: " + score;
                    //deactivate the BGM, activate the deathBGM
                    BGM.SetActive(false);
                    deathBGM.SetActive(true);
                }
            }
        }

        // if death panel is active, fade it in by changing the alpha value of the panel
        if (deathPanel.activeSelf)
        {
            deathPanel.GetComponent<Image>().color = new Color(0, 0, 0, Mathf.Clamp((System.DateTime.Now.Ticks / System.TimeSpan.TicksPerMillisecond - deathTime) / (float)deathPanelAnimationTime, 0f, 1f));
            // get the DeathText component by name and change the alpha value of the text
            deathPanelText.GetComponent<TextMeshProUGUI>().color = new Color(1, 0, 0, Mathf.Clamp((System.DateTime.Now.Ticks / System.TimeSpan.TicksPerMillisecond - deathTime) / (float)deathPanelAnimationTime, 0f, 1f));
            // get the DeathScoreText component by name and change the alpha value of the text
            deathScoreText.GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, Mathf.Clamp((System.DateTime.Now.Ticks / System.TimeSpan.TicksPerMillisecond - deathTime) / (float)deathPanelAnimationTime, 0f, 1f));
        }

        // light attack (left mouse button), E
        if (Input.GetKey(KeyCode.E) || Input.GetMouseButton(0)) {
            // set light attack plane to active
            punchPlanes[0].SetActive(true);
            monkeAnim.SetBool("walk", false);
            monkeAnim.SetBool("jump", false);
            monkeAnim.SetTrigger("punch1");
            lightPunchSound.GetComponent<AudioSource>().Play();
            Vector3 punchPlaneScale = new Vector3(transform.localScale.x * punchPlanes[0].transform.localScale.x*5, transform.localScale.y * punchPlanes[0].transform.localScale.y, transform.localScale.z * punchPlanes[0].transform.localScale.z * 10);
            Collider [] enemyHits = Physics.OverlapBox(punchPlanes[0].transform.position, punchPlaneScale, punchPlanes[0].transform.rotation, enemyLayer);
            Collider[] personHits = Physics.OverlapBox(punchPlanes[0].transform.position, punchPlaneScale, punchPlanes[0].transform.rotation, personLayer);

            foreach (Collider enemyHit in enemyHits)
            {
                // if cooldown is over, add score
                if (Time.time - lastPunchTime >= punchCooldown)
                {
                    score += 100;
                    scoreText.GetComponent<TextMeshProUGUI>().text = "Score: " + score;
                    lastPunchTime = Time.time;
                }
            }

            foreach (Collider personHit in personHits)
            {
                // if cooldown is over, add score
                if (Time.time - lastPunchTime >= punchCooldown)
                {
                    score += 200;
                    scoreText.GetComponent<TextMeshProUGUI>().text = "Score: " + score;
                    lastPunchTime = Time.time;
                }
            }
        }
        else
        {
            // set light attack plane to inactive
            punchPlanes[0].SetActive(false);
        }

        // heavy attack (right mouse button), Q
        if (Input.GetKey(KeyCode.Q) || Input.GetMouseButton(1))
        {
            
            // set heavy attack plane to active
            punchPlanes[1].SetActive(true);
            monkeAnim.SetBool("walk", false);
            monkeAnim.SetBool("jump", false);
            monkeAnim.SetTrigger("punch2");
            heavyPunchSound.GetComponent<AudioSource>().Play();
            Vector3 punchPlaneScale = new Vector3(transform.localScale.x * punchPlanes[1].transform.localScale.x*5, transform.localScale.y * punchPlanes[1].transform.localScale.y, transform.localScale.z * punchPlanes[1].transform.localScale.z*10);
            Collider[] enemyHits = Physics.OverlapBox(punchPlanes[1].transform.position, punchPlaneScale/2, punchPlanes[1].transform.rotation, enemyLayer);
            Collider[] personHits = Physics.OverlapBox(punchPlanes[1].transform.position, punchPlaneScale/2, punchPlanes[1].transform.rotation, personLayer);

            foreach (Collider enemyHit in enemyHits)
            {
                // if cooldown is over, add score
                if (Time.time - lastPunchTime >= punchCooldown)
                {
                    score += 100;
                    scoreText.GetComponent<TextMeshProUGUI>().text = "Score: " + score;
                    lastPunchTime = Time.time;
                }
            }

            foreach (Collider personHit in personHits)
            {
                // if cooldown is over, add score
                if (Time.time - lastPunchTime >= punchCooldown)
                {
                    score += 200;
                    scoreText.GetComponent<TextMeshProUGUI>().text = "Score: " + score;
                    lastPunchTime = Time.time;
                }
            }
        }
        else
        {
            // set heavy attack plane to inactive
            punchPlanes[1].SetActive(false);
        }

        //press escape to quit
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    // Check if the player has collided with a banana
    void OnTriggerEnter(Collider other)
    {
        if ((pickupLayer & 1 << other.gameObject.layer) != 0)
        {
            // if the player has collided with a banana, destroy the banana and add to the score
            monkeAnim.SetTrigger("eat");
            eatSound.GetComponent<AudioSource>().Play();
            Destroy(other.gameObject);
            healthBarImage.GetComponent<Image>().fillAmount = 1f;
            if (!monkeStarted)
            {
                monkeStarted = true;
            }
            monkeTimeStart = System.DateTime.Now.Ticks / System.TimeSpan.TicksPerMillisecond;
        }
    }
}
