using System.Collections;
using System.Collections.Generic;
using Carrasco.Extensions;
using Carrasco.Pleaceables;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Carrasco.Core
{
    public class GameManager : MonoBehaviour
    {
        private BasePlaceable currPlaceable;

        public float Score;
        public float Life;
        public GameWave GameWave;
        public UnityEvent GameEndEvent;
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
            this.GameWave = FindObjectOfType<GameWave>();
            this.Score = 500;
            this.Life = 10;
            this.GameEndEvent.AddListener(() =>
            {
                Debug.Log("GameEnd");
                SceneManager.LoadScene("End");
            });

            DontDestroyOnLoad(this.gameObject);
        }

        // Update is called once per frame
        void Update()
        {
            this.input.Handle()?.Execute();

            if(this.Life <=0 && SceneManager.GetActiveScene().name != "End") {
                SceneManager.LoadScene("End");
            }
        }
    }
}
