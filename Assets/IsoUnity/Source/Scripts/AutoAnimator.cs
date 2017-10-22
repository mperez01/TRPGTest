﻿using UnityEngine;
using System.Collections;

namespace IsoUnity
{
    public class AutoAnimator : MonoBehaviour
    {


        public int[] FrameSecuence = new int[0];
        public float FrameRate;

        public int Repeat = 0;
        public bool AutoDestroy = false;

        private int currentpos = 0;
        private int currentloop = 0;
        private float currenttime = 0f;

        private Decoration dec;

        private IGameEvent ev;

        // Use this for initialization
        void Start()
        {
            dec = this.GetComponent<Decoration>();
        }

        // Update is called once per frame
        void Update()
        {
            currenttime += Time.deltaTime;


            if (currenttime > this.FrameRate)
            {
                currenttime -= this.FrameRate;

                Debug.Log(dec.Tile);
                if (this.FrameSecuence.Length == 0)
                    if ((dec.Tile + 1) < (dec.IsoDec.nCols * dec.IsoDec.nRows))
                        dec.Tile++;
                    else
                    {
                        dec.Tile = 0;
                        if (this.AutoDestroy)
                            this.currentloop++;
                    }
                else
                {
                    if ((currentpos + 1) < this.FrameSecuence.Length)
                        currentpos++;
                    else
                    {
                        currentpos = 0;
                        if (this.AutoDestroy)
                            this.currentloop++;
                    }

                    dec.Tile = this.FrameSecuence[currentpos];
                }
                //dec.adaptate();

                if (this.AutoDestroy)
                    if (this.currentloop >= this.Repeat)
                    {
                        if (ev != null)
                            Game.main.eventFinished(ev);
                        GameObject.Destroy(this.gameObject);
                    }
            }
        }

        public void registerEvent(IGameEvent ev)
        {
            this.ev = ev;
        }
    }
}