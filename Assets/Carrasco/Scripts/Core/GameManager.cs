using System.Collections;
using System.Collections.Generic;
using Carrasco.Extensions;
using Carrasco.Pleaceables;
using UnityEngine;
namespace Carrasco.Core
{
    public class GameManager : MonoBehaviour
    {
        public float Score;
        public float Life = 10;
        private BasePlaceable currPlaceable;

        public BasePlaceable CurrPlaceable
        {
            get
            {
                return currPlaceable;
            }
            set
            {
                currPlaceable = value;
            }
        }

        public BasePlaceable SelectedPlacedPlaceable { get; set; }

        private InputHandler input;
        private static GameManager _instance;

        public static GameManager Instance
        {
            get
            {
                if (!_instance)
                {
                    _instance = FindObjectOfType<GameManager>();
                }

                if (!_instance)
                {
                    _instance = new GameObject("GameManager").AddComponent<GameManager>();
                }

                return _instance;
            }
        }


        // Start is called before the first frame update
        void Start()
        {
            this.input = new InputHandler();
        }

        // Update is called once per frame
        void Update()
        {
            this.input.Handle()?.Execute();
        }
    }
}
