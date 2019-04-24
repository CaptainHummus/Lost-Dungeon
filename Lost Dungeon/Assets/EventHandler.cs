using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    private Object[] allEvents;
    [SerializeField]
    private List<Event> commonEvents = new List<Event>();
    [SerializeField]
    private List<Event> uncommonEvents = new List<Event>();
    [SerializeField]
    private List<Event> rareEvents = new List<Event>();

    public static EventHandler instance = null;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void TriggerEvent()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        allEvents = Resources.LoadAll("Events", typeof(Event));
        foreach (Event currentEvent in allEvents)
        {
            switch (currentEvent.rarity)
            {
                case Rarity.Common:
                    commonEvents.Add(currentEvent);
                    break;
                case Rarity.Uncommon:
                    uncommonEvents.Add(currentEvent);
                    break;
                case Rarity.Rare:
                    rareEvents.Add(currentEvent);
                    break;
                case Rarity.Portal:
                    break;
                default:
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
