using System.Collections;
using System.Collections.Generic;
using Carrasco.Core;
using Carrasco.Interfaces;
using UnityEngine;
using UnityEngine.Events;
namespace Carrasco.Pleaceables
{
    public abstract class BasePlaceable : MonoBehaviour, IPoolCallback
    {
        public string SurfaceTag;
        public Material defaultMaterial;
        public bool IsPlaced;
        private new MeshRenderer renderer;

        public virtual void Start()
        {
            this.renderer = GetComponent<MeshRenderer>();
            this.defaultMaterial = this.renderer.material;
        }

        public virtual void MovePlaceableObject()
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.transform.tag == this.SurfaceTag)
                {
                    this.renderer.material = Resources.Load<Material>("CanPlaceMat");
                    print("here");
                }
                else
                {
                    this.renderer.material = Resources.Load<Material>("NoCanPlaceMat");
                }
                this.transform.position = hitInfo.point;
                this.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
            }
        }

        public virtual void PlacePleaceableObject()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.transform.tag == this.SurfaceTag)
                {
                    this.transform.position = hitInfo.point;
                    this.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
                    this.IsPlaced = true;
                    this.renderer.material = this.defaultMaterial;
                    GameManager.Instance.CurrPlaceable = null;
                }
            }
        }

        public virtual void OnRecycleCallback()
        {
            this.renderer.material = this.defaultMaterial;
            this.IsPlaced = false;
        }

        public void OnSpawnCallback()
        {

        }
    }
}
