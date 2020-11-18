using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    Camera mainCamera;

    MaterialPropertyBlock matBlock;

    MeshRenderer meshRenderer;

    BasicEnemyFunctions basicEnemyFunctions;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        matBlock = new MaterialPropertyBlock();

        basicEnemyFunctions = GetComponentInParent<BasicEnemyFunctions>();
    }

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        

        if(basicEnemyFunctions.currentHealth < basicEnemyFunctions.maxHealth)
        {
            meshRenderer.enabled = true;
            AlignCamera();
            UpdateParams();
        } else
        {
            meshRenderer.enabled = false;
        }


    }


    private void UpdateParams()
    {
        meshRenderer.GetPropertyBlock(matBlock);
        matBlock.SetFloat("_Fill", basicEnemyFunctions.currentHealth / (float)basicEnemyFunctions.maxHealth);
        meshRenderer.SetPropertyBlock(matBlock);
    }

    private void AlignCamera()
    {
        if(mainCamera != null)
        {
            var camXform = mainCamera.transform;
            var forward = transform.position - camXform.position;
            forward.Normalize();
            var up = Vector3.Cross(forward, camXform.right);
            transform.rotation = Quaternion.LookRotation(forward, up);
        }
    }


}
