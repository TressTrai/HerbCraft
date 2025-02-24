using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;


public class EmailDesk : MonoBehaviour
{
    [SerializeField] private int recoveryDuration = 1;
    [SerializeField] private GameObject taskField;

    private TextMeshProUGUI titleField;
    private TextMeshProUGUI descriptionField;
    private PersonalTriggerZone triggerZone;
    private GameObject taskAvailableIcon;
    private Player player;

    private bool orderAvailable = true;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("PlayerBody").GetComponent<Player>();
        titleField = taskField.transform.Find("TitleText").gameObject.GetComponent<TextMeshProUGUI>();
        descriptionField = taskField.transform.Find("DescriptionText").gameObject.GetComponent<TextMeshProUGUI>();
        triggerZone = gameObject.GetComponent<PersonalTriggerZone>();
        taskAvailableIcon = transform.parent.transform.Find("TaskAvailable").gameObject;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && triggerZone.PlayerDetection)
        {
            if (orderAvailable && (player.currentTask == null || player.currentTask.title == ""))
            {
                print(1);
                ShowTask();
                SetOrderUnavailable();
                StartCountdown();
            }
            else
            {
                print("you have no orders. Please, wait");
            }
        }
    }

    private void SetOrderAvailable()
    {
        orderAvailable = true;
        taskAvailableIcon.SetActive(true);
    }

    private void SetOrderUnavailable()
    {
        orderAvailable = false;
        taskAvailableIcon.SetActive(false);
    }

    private void ShowTask()
    {
        Task randomTask = GetRandomTask();

        taskField.SetActive(true);
        titleField.text = randomTask.title;
        descriptionField.text = randomTask.description;

        print("plr");
        print(player.currentTask);
        player.currentTask = randomTask;
    }

    private Task GetRandomTask()
    {
        Task[] tasks = GetTasksText().tasksList;
        if (tasks == null)
        {
            print("no tasks");
            return null;
        }
        int randomIndex = UnityEngine.Random.Range(0, tasks.Length-1);

        return tasks[randomIndex];
    }

    private Tasks GetTasksText()
    {
        string jsonString = GetJSONString();

        return Tasks.CreateFromJSON(jsonString);
    }

    private string GetJSONString()
    {
        string pathToJSON = "/Texts/TasksAncient.json";
        string content = File.ReadAllText(Application.streamingAssetsPath + pathToJSON).ToString();


        return content;
    }

    private void StartCountdown()
    {
        void timerCallback()
        {
            SetOrderAvailable();
        }
        StartCoroutine(TimerCountdown.StartCountdown(timerCallback, recoveryDuration*60));
    }
}

public delegate void TimerCallback();

[System.Serializable]
public class Task
{
    public string title;
    public string description;
    public string objectName;
    public int cost;
}

[System.Serializable]
public class Tasks
{
    public Task[] tasksList;

    public static Tasks CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<Tasks>(jsonString);
    }
}