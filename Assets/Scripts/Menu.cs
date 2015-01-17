﻿using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

    private Animator _animator;
    private CanvasGroup _canvasgroup;

    public bool isOpen
    {
        get { return _animator.GetBool("isOpen"); }
        set { _animator.SetBool("isOpen", value); }
    }

    public void Awake()
    {
        _animator = GetComponent<Animator>();
        _canvasgroup = GetComponent<CanvasGroup>();

        var rect = GetComponent<RectTransform>();
        rect.offsetMax = rect.offsetMin = new Vector2(0, 0);
    }

    public void Update()
    {
        if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("Open"))
        {
            _canvasgroup.blocksRaycasts = _canvasgroup.interactable = false;
        }
        else
        {
            _canvasgroup.blocksRaycasts = _canvasgroup.interactable = true;
        }
    }
}