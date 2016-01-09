﻿using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

    private Animator animator;
    private CanvasGroup canvasGroup;

    public bool IsOpen
    {
        get { return animator.GetBool("IsOpen"); }
        set { animator.SetBool("IsOpen", value); }
    }

    public void Awake()
    {
        animator = GetComponent<Animator>();
        canvasGroup = GetComponent<CanvasGroup>();

        // set it that menu will appear in the center of the screen
        var rect = GetComponent<RectTransform>();
        rect.offsetMax = rect.offsetMin = new Vector2(0, 0);
    }

    public void Update()
    {
        // if its not open then disable interactibleness of stuff, else enable it
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Open"))
        {
            canvasGroup.blocksRaycasts = canvasGroup.interactable = false;
        }
        else
        {
            canvasGroup.blocksRaycasts = canvasGroup.interactable = true;
        }
    }
}
