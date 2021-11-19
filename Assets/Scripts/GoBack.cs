using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.XR.Cardboard;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GoBack : MonoBehaviour, IPointerClickHandler 
{
    public void OnPointerClick(PointerEventData eventData)
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Start is called before the first frame update
    void Start()
    {
        // Api.MinTriggerHeldPressedTime = 2;
        Debug.Log("MinTriggerHeldPressedTime" + Api.MinTriggerHeldPressedTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (Api.IsTriggerHeldPressed)
        {
            SceneManager.LoadScene("MainMenu");
        }
        //Debug.Log("IsHeldPressed: " + Api.IsTriggerHeldPressed);
        //Debug.Log("IsPressed: " + Api.IsTriggerPressed);
    }
}
