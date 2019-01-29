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
        public float Cost;
        public bool IsPlaced;
        public bool IsConfirmPlacing;
        public Canvas canvas;
        public new Collider collider;
        protected new MeshRenderer renderer;


        private Material defaultMaterial;
        private Outline outline;

        public virtual void Start()
        {
            this.renderer = GetComponentInChildren<MeshRenderer>();
            this.collider = GetComponentInChildren<Collider>();
            this.canvas = GetComponentInChildren<Canvas>();
            this.outline = GetComponentInChildren<Outline>();
            this.defaultMaterial = this.renderer.material;
            this.collider.enabled = false;
            this.canvas.enabled = false;
            this.outline.enabled = false;
        }

        public virtual void MovePlaceableObject()
        {
            this.outline.OutlineColor = new Color32(255, 0, 0, 255);
            this.outline.enabled = true;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var layerMask = LayerMask.GetMask("Surface");
            var hits = Physics.RaycastAll(ray, 1000, layerMask);
            this.renderer.material = Resources.Load<Material>("NoPlaceMat");
            foreach (var hit in hits)
            {
                print(Camera.main.transform.rotation);
                this.transform.position = hit.point + Vector3.up * .01f;
                this.transform.rotation = Quaternion.Euler(Vector3.up * Camera.main.transform.rotation.eulerAngles.y);
                
                if (hit.transform.tag == this.SurfaceTag)
                {
                    this.renderer.material = Resources.Load<Material>("CanPlaceMat");
                    this.outline.OutlineColor = new Color32(0, 255, 255, 255);
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

        public virtual void OnPlacedSelected()
        {
            if (GameManager.Instance.SelectedPlacedPlaceable == this && this.IsPlaced)
            {
                this.canvas.enabled = true;
                this.outline.enabled = true;
                this.outline.OutlineColor = new Color32(255, 255, 0, 255);
            }
        }

        public virtual void OnPlacedDeselected()
        {
            this.canvas.enabled = false;
            this.outline.enabled = false;
            GameManager.Instance.SelectedPlacedPlaceable = null;
        }
        public virtual void ConfirmPlace()
        {
            GameManager.Instance.Score -= this.Cost;
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
        public virtual void RemovePlaced()
        {
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
