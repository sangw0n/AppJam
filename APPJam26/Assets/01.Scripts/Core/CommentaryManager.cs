using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CommentaryManager : MonoSingleton<CommentaryManager>
{

    [SerializeField]
    private TextMeshProUGUI textCommentary;

    public void TriggerCommentary(string commentary)
    {
        textCommentary.text = commentary;
    }

    protected override void Init()
    {
        
    }
}
