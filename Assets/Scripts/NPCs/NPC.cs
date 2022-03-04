using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class NPC : MonoBehaviour
{

    private BoxCollider speakingArea;

    private void Awake()
    {
        speakingArea = GetComponent<BoxCollider>();
        speakingArea.isTrigger = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Speak()
    {

        speakingArea.enabled = false;
    }
}
