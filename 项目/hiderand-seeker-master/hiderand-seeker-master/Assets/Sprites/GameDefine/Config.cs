﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Models
{
    class Config
    {
        public static bool MusicOn
        {
            get
            {
                return PlayerPrefs.GetInt("Music", 1) == 1;
            }

            internal set
            {
                PlayerPrefs.SetInt("Music", value ? 1 : 0);
                //SoundManager.Instance.MusicOn = value;
            }
        }
        public static bool SoundOn
        {
            get
            {
                return PlayerPrefs.GetInt("Sound", 1) == 1;
            }

            internal set
            {
                PlayerPrefs.SetInt("Sound", value ? 1 : 0);
                //SoundManager.Instance.SoundOn = value;
            }
        }
        public static int MusicVolume
        {
            get
            {
                return PlayerPrefs.GetInt("MusicVolume", 100);
            }

            internal set
            {
                PlayerPrefs.SetInt("MusicVolume", value);
                //SoundManager.Instance.MusicVolume = value;
            }
        }
        public static int SoundVolume
        {
            get
            {
                return PlayerPrefs.GetInt("SoundVolume", 100);
            }

            internal set
            {
                PlayerPrefs.SetInt("SoundVolume", value);
                //SoundManager.Instance.SoundVolume = value;
            }
        }
    }
}
