using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int currentObjects = 0;
    public int deadObjects = 0;
    public GameObject[] wave0Objects;
    public GameObject[] wave1Objects;
    public GameObject[] wave2Objects;
    public GameObject[] wave3Objects;
    public bool switchLock;
    private int wave = 0;
    private int maxWave = 4;

    public GameObject bossPanel;
    public Text currentWave;
    public GameObject waveAlert;
    public float alertVisibleFor = 1f;
    private float alertVisibleFrom;


    private void Start()
    {
        alertVisibleFrom = Time.time;
        bossPanel.SetActive(false);
        foreach (var wave1Object in wave0Objects)
        {
            wave1Object.SetActive(false);
        }

        foreach (var wave1Object in wave1Objects)
        {
            wave1Object.SetActive(false);
        }

        foreach (var wave2Object in wave2Objects)
        {
            wave2Object.SetActive(false);
        }

        foreach (var wave3Object in wave3Objects)
        {
            wave3Object.SetActive(false);
        }
    }

    public void Update()
    {
        currentWave.text = "Current wave: " + wave;

        if (Time.time - alertVisibleFrom < alertVisibleFor)
        {
            waveAlert.SetActive(true);
            waveAlert.GetComponent<Text>().text = "Wave " + (wave < 4 ? wave.ToString() : "BOSS");
        }
        else
        {
            waveAlert.SetActive(false);
        }

        if (wave < maxWave && currentObjects <= deadObjects && !switchLock)
        {
            switchLock = true;
            alertVisibleFrom = Time.time;
            wave++;
            currentObjects = 0;
            switch (wave)
            {
                case 1:
                    foreach (var wave0Object in wave0Objects)
                    {
                        wave0Object.SetActive(true);
                        currentObjects++;
                    }

                    break;
                case 2:
                    foreach (var wave1Object in wave1Objects)
                    {
                        wave1Object.SetActive(true);
                        currentObjects++;
                    }

                    break;
                case 3:
                    foreach (var wave2Object in wave2Objects)
                    {
                        wave2Object.SetActive(true);
                        currentObjects++;
                    }

                    break;
                case 4:
                    bossPanel.SetActive(true);
                    foreach (var wave3Object in wave3Objects)
                    {
                        wave3Object.SetActive(true);
                        currentObjects++;
                    }

                    break;
            }

            deadObjects = 0;
            switchLock = false;
        }
    }
}