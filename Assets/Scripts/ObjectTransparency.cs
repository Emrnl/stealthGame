using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTransparency : MonoBehaviour
{

    GameObject Player;
    GameObject lastObject;
    Material[] myMaterials;


    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }


    void Update()
    {

        RaycastHit hit;

        Vector3 RayDir;
        RayDir = Player.transform.position - Camera.main.transform.position;
        if (Physics.Raycast(Camera.main.transform.position, RayDir, out hit))
        {
            if (hit.transform.tag == "Environment")
            {
                Debug.Log("Obj");

                lastObject = hit.transform.gameObject;
                myMaterials = lastObject.GetComponent<Renderer>().materials;
                foreach (Material mat in myMaterials)
                {
                    mat.SetFloat("_Mode", 2);
                    mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                    mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    mat.SetInt("_ZWrite", 0);
                    mat.DisableKeyword("_ALPHATEST_ON");
                    mat.EnableKeyword("_ALPHABLEND_ON");
                    mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    mat.renderQueue = 3000;
                    mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, 0.5f);
                }
            }
            else if (lastObject != null)
            {
                foreach (Material mat in myMaterials)
                {
                    mat.SetFloat("_Mode", 0);
                    mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                    mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                    mat.SetInt("_ZWrite", 1);
                    mat.DisableKeyword("_ALPHATEST_ON");
                    mat.DisableKeyword("_ALPHABLEND_ON");
                    mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    mat.renderQueue = -1;
                    mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, 1f);
                }
            }
        }

    }
}