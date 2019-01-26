using System.Collections;
using System.Collections.Generic;
using Carrasco.Interfaces;
using UnityEngine;
using UnityEngine.Events;
namespace Carrasco.Pleaceables
{
    public abstract class BasePlaceable : MonoBehaviour, IPoolCallback
    {
        public GameObject Surface;
        public Material defaultMaterial;

        public virtual void MovePlaceableObject()
        {
            if (!this.Surface) return;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo) && hitInfo.transform.name == this.Surface.name)
            {
                this.transform.position = hitInfo.point;
                this.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
            }
        }

        public virtual void OnRecycleCallback()
        {
        }

        public void OnSpawnCallback()
        {
            
        }
    }
}
