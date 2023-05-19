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
    public long monkeTimeLimit = 30000;

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
    // Start is called before the first frame update
    void Start()
    {
        Component[] components = deathPanelText.GetComponents(typeof(Component));
        foreach (Component component in components)
        {
            Debug.Log(component.ToString());
        }
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
                }
            }
        }

        // if death panel is active, fade it in by changing the alpha value of the panel
        if (deathPanel.activeSelf)
        {
            deathPanel.GetComponent<Image>().color = new Color(0, 0, 0, Mathf.Clamp((System.DateTime.Now.Ticks / System.TimeSpan.TicksPerMillisecond - deathTime) / (float)deathPanelAnimationTime, 0f, 1f));
            // get the DeathText component by name and change the alpha value of the text
            deathPanelText.GetComponent<TextMeshProUGUI>().color = new Color(1, 0, 0, Mathf.Clamp((System.DateTime.Now.Ticks / System.TimeSpan.TicksPerMillisecond - deathTime) / (float)deathPanelAnimationTime, 0f, 1f));
        }
    }

    // Check if the player has collided with a banana
    void OnTriggerEnter(Collider other)
    {
        if ((pickupLayer & 1 << other.gameObject.layer) != 0)
        {
            // if the player has collided with a banana, destroy the banana and add to the score
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
