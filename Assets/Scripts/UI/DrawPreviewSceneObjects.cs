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

    public void RenderObjectsPreview()
    {
        int i = 0;
        float imageHeight = 58;

        foreach (Transform child2 in ContentScroll.transform)
        {
            Destroy(child2.gameObject);
        }

        //Previews.Clear();

        foreach (Transform child in ObjectsForScene.transform)
        {
            if (null == child)
                continue;

            Texture2D img = RuntimePreviewGenerator.GenerateModelPreview(child);
            GameObject obj = Instantiate(ContentScrollItem, ContentScroll.transform);
            obj.transform.localPosition = changeY(obj.transform.localPosition, obj.transform.localPosition.y - i * imageHeight);
            obj.GetComponent<RawImage>().texture = img;
            obj.GetComponent<PreviewSceneObject>().sceneObject = child.gameObject;
            //child.gameObject.SetActive(false); // test
            //Previews.Add(obj);
            i++;
        }
    }

    Vector3 changeY(Vector3 position, float changeY)
    {
        return new Vector3(position.x, changeY, position.z);
    }
}
