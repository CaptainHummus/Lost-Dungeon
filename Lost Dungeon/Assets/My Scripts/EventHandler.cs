using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EventHandler : MonoBehaviour
{
    private Object[] allEvents;
    [SerializeField]
    private List<Event> commonEvents = new List<Event>();
    [SerializeField]
    private List<Event> uncommonEvents = new List<Event>();
    [SerializeField]
    private List<Event> rareEvents = new List<Event>();
    [SerializeField]
    private Event emptyEvent = null;
    [SerializeField]
    private Event deathEvent = null;


    [SerializeField]
    private IntVariable playerHealth = null;
    [SerializeField]
    private IntVariable playerGold = null;


    private TextMeshProUGUI eventText;
    private TextMeshProUGUI eventTitle;
    private GameObject continueButton;
    private GameObject option1Button;
    private GameObject option2Button;

    private GameObject player;



    private Event selectedEvent;

    private int randomInt;

    public static EventHandler instance = null;
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        eventText = GameObject.Find("EventText").GetComponent<TextMeshProUGUI>();
        eventTitle = GameObject.Find("EventTitle").GetComponent<TextMeshProUGUI>();
        continueButton = GameObject.Find("Continue");
        option1Button = GameObject.Find("Option 1");
        option2Button = GameObject.Find("Option 2");

        player = GameObject.FindGameObjectWithTag("Player");

        allEvents = Resources.LoadAll("Events", typeof(Event));
        foreach (Event tempEvent in allEvents)
        {
            switch (tempEvent.rarity)
            {
                case Rarity.Common:
                    commonEvents.Add(tempEvent);
                    break;
                case Rarity.Uncommon:
                    uncommonEvents.Add(tempEvent);
                    break;
                case Rarity.Rare:
                    rareEvents.Add(tempEvent);
                    break;
                case Rarity.Portal:
                    break;
                default:
                    break;
            }
        }
        
    }

    public void LoadRandomEvent()
    {
        randomInt = Random.Range(1, 11);
        if (randomInt <= 5)
        {
            selectedEvent = commonEvents[Random.Range(0, commonEvents.Count)];

        }
        else if (randomInt <= 8)
        {
            selectedEvent = uncommonEvents[Random.Range(0, uncommonEvents.Count)];
        }
        else if (randomInt > 8)
        {
            selectedEvent = rareEvents[Random.Range(0, rareEvents.Count)];
        }

        RunEvent(selectedEvent);
    }

    public void RunEvent(Event currentEvent)
    {
        eventTitle.text = currentEvent.eventName;
        eventText.text = currentEvent.eventDescription;

        if (!currentEvent.pauseMovement)
        {
            player.GetComponent<Player>().canMove = true;
        }
        else
        {
            player.GetComponent<Player>().canMove = false;
        }

        switch (currentEvent.numberOfOptions)
        {
            case 0:
                continueButton.SetActive(true);
                option1Button.SetActive(false);
                option2Button.SetActive(false);
                break;

            case 1:
                continueButton.SetActive(false);
                option1Button.SetActive(true);
                option2Button.SetActive(false);

                option1Button.GetComponentInChildren<TextMeshProUGUI>().text = currentEvent.option1.eventName;
                break;

            case 2:
                continueButton.SetActive(false);
                option1Button.SetActive(true);
                option2Button.SetActive(true);

                option1Button.GetComponentInChildren<TextMeshProUGUI>().text = currentEvent.option1.eventName;
                option2Button.GetComponentInChildren<TextMeshProUGUI>().text = currentEvent.option2.eventName;
                break;
            default:
                break;
        }

        if (currentEvent.chance != 0)
        {
            if (currentEvent.chance < Random.value)
            {
                option2Button.SetActive(false);
            }
            else
            {
                option1Button.SetActive(false);
            }
        }


    }

    public void Continue()
    {
        if (selectedEvent.healthCalculation != 0)
        {
            playerHealth.variable += selectedEvent.healthCalculation;
        }

        if (selectedEvent.goldGain != 0)
        {
            playerGold.variable += selectedEvent.goldGain;
        }

        if (selectedEvent.exitPortal)
        {
            GameManager.instance.LoadScene("Win Scene");
        }

        if (selectedEvent.deathEvent)
        {
            GameManager.instance.LoadScene("Lose Scene");
        }
        else if (playerHealth.variable < 1)
        {
            RunEvent(deathEvent);
            selectedEvent = deathEvent;
        }
        else
        {
            RunEvent(emptyEvent);
            selectedEvent = emptyEvent;
        }
    }

    public void Option1()
    {

        RunEvent(selectedEvent.option1);
        selectedEvent = selectedEvent.option1;

    }
    public void Option2()
    {
        RunEvent(selectedEvent.option2);
        selectedEvent = selectedEvent.option2;
    }


}
