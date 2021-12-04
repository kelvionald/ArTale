using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScene : MonoBehaviour
{
    public GameObject Scene;
    public TaleManager taleManager;
    public bool IsCurrent = false;
    public int SceneId = 0;

    private Color ColorSelectedScene;
    private Color ColorUnselecredScene;
    private Color ColorCurrentScene;

    public int LastSceneNumber;

    void Start()
    {
        ColorUtility.TryParseHtmlString("#92FF93", out ColorCurrentScene);
        ColorUtility.TryParseHtmlString("#8AB6FF", out ColorSelectedScene);
        ColorUtility.TryParseHtmlString("#FFFFFF", out ColorUnselecredScene);

        GetComponent<Button>().onClick.AddListener(OnClick);

        toggleSelection(false);
    }

    private void OnClick()
    {
        taleManager.SelectSceneBtn(this, Scene);
    }

    public void Init(GameObject SceneParent, TaleManager taleManager, bool IsCurrent, int SceneId)
    {
        this.Scene = SceneParent;
        this.taleManager = taleManager;
        this.IsCurrent = IsCurrent;
        this.SceneId = SceneId;

        toggleSelection(false);
    }

    internal void toggleSelection(bool toggle)
    {
        if (toggle)
        {
            GetComponent<Image>().color = ColorSelectedScene;
        }
        else
        {
            GetComponent<Image>().color = IsCurrent ? ColorCurrentScene : ColorUnselecredScene;
        }
    }
}
