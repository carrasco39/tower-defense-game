using System.Collections;
using System.Collections.Generic;
using Carrasco.Core;
using Carrasco.Interfaces;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
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
            var layerMask = LayerMask.GetMask("Surface");
            var hits = Physics.RaycastAll(ray, 1000, layerMask);
            this.renderer.material = Resources.Load<Material>("NoPlaceMat");
            foreach (var hit in hits)
            {
                this.transform.position = hit.point + Vector3.up * .1f;
                this.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
                if (hit.transform.tag == this.SurfaceTag)
                {
                    this.renderer.material = Resources.Load<Material>("CanPlaceMat");
                }
                //TODO: REFACTOR AND REDO IT BETTER
            }
        }

        public virtual void PlacePleaceableObject()
        {
            print(this.renderer.material.name);
            if(this.renderer.material.name.Contains("CanPlaceMat")) {
                this.renderer.material = this.defaultMaterial;
                this.IsPlaced = true;
                GameManager.Instance.CurrPlaceable = null;
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
