using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CommentaryManager : MonoBehaviour
{
    public static CommentaryManager instance { get; private set; }

    [SerializeField]
    private TextMeshProUGUI textCommentary;

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            TriggerCommentary("Attack!!!!!!!!!!!! Player01");
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            TriggerCommentary("Attack!!!!!!!!!!!! Player02");
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            TriggerCommentary("ohOHHOHOOHO!!!!!!!!!!!! Player01");
        }
    }

    public void TriggerCommentary(string commentary)
    {
        textCommentary.text = commentary;
    }
}
