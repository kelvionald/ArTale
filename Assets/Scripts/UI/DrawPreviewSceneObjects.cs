using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawPreviewSceneObjects : MonoBehaviour
{
    public GameObject ObjectsForScene;
    public GameObject ContentScroll;
    public GameObject ContentScrollItem;

    void Start()
    {
        RenderObjectsPreview();
    }

    public void ClearObjectsForScene()
    {
        Debug.Log("ClearObjectsForScene");
        foreach (Transform child in ObjectsForScene.transform)
        {
            Destroy(child.gameObject);
        }
        Destroy(ObjectsForScene);
        ObjectsForScene = new GameObject();
    }

    public void len()
    {
        int c = 0;
        foreach (Transform child in ObjectsForScene.transform)
        {
            c++;
        }
        Debug.Log("c = " + c);
    }

    public void RenderObjectsPreview()
    {
        int i = 0;
        float imageHeight = 58;

        foreach (Transform child2 in ContentScroll.transform)
        {
            Destroy(child2.gameObject);
        }

        foreach (Transform child in ObjectsForScene.transform)
        {
            if (null == child || child.gameObject == null)
            {
                continue;
            }

            Debug.Log("child");
            Debug.Log(child);
            Debug.Log(child.gameObject);

            Texture2D img = RuntimePreviewGenerator.GenerateModelPreview(child);
            GameObject obj = Instantiate(ContentScrollItem, ContentScroll.transform);
            obj.transform.localPosition = changeY(obj.transform.localPosition, obj.transform.localPosition.y - i * imageHeight);
            obj.GetComponent<RawImage>().texture = img;
            obj.GetComponent<PreviewSceneObject>().sceneObject = child.gameObject;
            //Destroy(obj);
            child.gameObject.SetActive(false);
            i++;
        }

        Debug.Log("I = " + i);
    }

    Vector3 changeY(Vector3 position, float changeY)
    {
        return new Vector3(position.x, changeY, position.z);
    }
}
