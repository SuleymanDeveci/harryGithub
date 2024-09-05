using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowFPS : MonoBehaviour
{

    float sayacTime;
    int fps;
    [SerializeField] TextMeshProUGUI fpsText;

    // Start is called before the first frame update
    void Start()
    {
        fps = 0;
        sayacTime = 0;
        //fpsText = GetComponent<TextMeshProUGUI>();  

    }

    // Update is called once per frame
    void Update()
    {
        if (sayacTime >= 0.25)
        {

            fpsText.text = (fps * 4).ToString();
            sayacTime = 0;
            fps = 0;
        }
        else
        {
            sayacTime += Time.deltaTime;
            fps++;
        }
    }
}
