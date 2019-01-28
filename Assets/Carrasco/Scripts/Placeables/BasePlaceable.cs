using System.Collections;
using System.Collections.Generic;
using Carrasco.Core;
using Carrasco.Interfaces;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using Carrasco.Extensions;

namespace Carrasco.Pleaceables
{
    public abstract class BasePlaceable : MonoBehaviour, IPoolCallback
    {
        public string SurfaceTag;
        public Material defaultMaterial;
        public bool IsPlaced;
        public bool IsConfirmPlacing;
        public Canvas canvas;
        public new Collider collider;
        private new MeshRenderer renderer;
        private Outline outline;

        public virtual void Start()
        {
            this.renderer = GetComponent<MeshRenderer>();
            this.collider = GetComponent<Collider>();
            this.canvas = GetComponentInChildren<Canvas>();
            this.outline = GetComponent<Outline>();
            this.defaultMaterial = this.renderer.material;
            this.collider.enabled = false;
            this.canvas.enabled = false;
            this.outline.enabled = false;
        }

        public virtual void MovePlaceableObject()
        {
            this.outline.OutlineColor = new Color32(255, 0, 0,255);
            this.outline.enabled = true;
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
                    this.outline.OutlineColor = new Color32(0, 255, 255,255);
                }
                //TODO: REFACTOR AND REDO IT BETTER
            }
        }

        public virtual void PlacePleaceableObject()
        {
            //print(this.renderer && this.renderer.material.name);
            if (this.renderer && this.renderer.material.name.Contains("CanPlaceMat"))
            {
                this.IsConfirmPlacing = true;
                this.renderer.material = Resources.Load<Material>("PlaceHolderMat");
                this.canvas.enabled = true;
                this.outline.enabled = false;
            }
        }

        public virtual void OnPlacedSelected() {
            if(GameManager.Instance.SelectedPlacedPlaceable == this && this.IsPlaced) {
               this.canvas.enabled = true;
               this.outline.enabled = true;
            }
        }
        public virtual void OnPlacedDeselected() {
            this.canvas.enabled = false;
            this.outline.enabled = false;
            GameManager.Instance.SelectedPlacedPlaceable = null;
        }

        public virtual void ConfirmPlace()
        {
            this.IsConfirmPlacing = false;
            this.canvas.enabled = false;
            this.collider.enabled = true;
            this.IsPlaced = true;
            this.renderer.material = this.defaultMaterial;
            GameManager.Instance.CurrPlaceable = null;
            
        }
        public virtual void CancelPlace()
        {
            this.IsConfirmPlacing = false;
            this.canvas.enabled = false;
        }

        public virtual void RemovePlaced() {
            Debug.Log("removing");
            this.gameObject.Recycle(this);
            GameManager.Instance.SelectedPlacedPlaceable = null;
        }


        public virtual void OnRecycleCallback()
        {
            this.renderer.material = this.defaultMaterial;
            this.IsPlaced = false;
            this.collider.enabled = false;
            //this.canvas.SendMessage("ToggleCanvas",0,SendMessageOptions.DontRequireReceiver);
            this.canvas.enabled = false;
            this.outline.enabled = false;
        }

        public void OnSpawnCallback()
        {
        }


        //TODO: ADD OBJECT ROTATION COMMAND
    }
}
