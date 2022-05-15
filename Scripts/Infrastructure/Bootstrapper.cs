using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure
{
    public class Bootstrapper : MonoBehaviour
    {
        private Game game;

        private void OnEnable()
        {
            game = new Game();

            DontDestroyOnLoad(this);
        }
    }
}