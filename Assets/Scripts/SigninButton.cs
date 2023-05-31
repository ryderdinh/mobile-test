using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

using Debug = UnityEngine.Debug;

public class SigninButton : MonoBehaviour
{
    public Button myButton;

    // Start is called before the first frame update
    public void Start()
    {
        //myButton = GetComponent<Button>();
        myButton.onClick.AddListener(HandleButtonClick);
    }

    // Update is called once per frame
    public void HandleButtonClick()
    {
        Debug.Log("Clicked!");
    }

}
