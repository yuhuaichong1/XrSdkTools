using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test3 : MonoBehaviour
{
    public GameObject obj;
    public Button button;

    public GameObject obj2;
    public Button button2;
    void Start()
    {
        button.onClick.RemoveAllListeners();
        button2.onClick.RemoveAllListeners();
        button.onClick.AddListener(ButtonOnClick);
        button2.onClick.AddListener(ButtonOnClick2);
    }

    private void ButtonOnClick()
    {
        for (int i = 0; i < 50; i++)
        {
            Instantiate(obj, new Vector3(Random.Range(-18.5f, 5.5f), 0.38f, Random.Range(-38f, -8.8f)), Quaternion.identity);
        }
    }

    private void ButtonOnClick2()
    {
        for (int i = 0; i < 36; i++)
        {
            Instantiate(obj2, new Vector3(Random.Range(-18.5f, 5.5f), 0.38f, Random.Range(-38f, -8.8f)), Quaternion.identity);
        }
    }
}
