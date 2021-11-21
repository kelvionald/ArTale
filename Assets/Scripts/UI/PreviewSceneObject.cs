using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreviewSceneObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(OnMouseUp);
    }

    void OnMouseUp()
    {
        Debug.Log("Drag ended!");
    }
}
