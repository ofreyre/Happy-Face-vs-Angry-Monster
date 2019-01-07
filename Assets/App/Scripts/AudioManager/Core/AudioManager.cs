using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace AudioManagement
{

    [Serializable]
    public class ClipData
    {
        public AudioClip m_clip;
        public int m_maxAtOnce;
        public bool m_isConsumable;
        [HideInInspector]
        public string m_key;
        int m_clipsPlaying;

        public ClipData GetClip()
        {
            if (m_clipsPlaying < m_maxAtOnce)
            {
                m_clipsPlaying++;
                return this;
            }
            return null;
        }

        public bool ReturnClip()
        {
            if (!m_isConsumable)
            {
                m_clipsPlaying--;
                return false;
            }
            else if(m_clipsPlaying == m_maxAtOnce) {
                return true;
            }
            return false;
        }

        public bool IsFull
        {
            get
            {
                return m_clipsPlaying >= m_maxAtOnce;
            }
        }
    }

    [Serializable]
    public class DictionaryClipData : SerializableDictionary<string, ClipData> { }
    
    public class AudioManager: MonoBehaviour
    {
        public AudioBank[] m_bank;
        public DictionaryClipData m_clips;
        ClipPlayer[] m_playerFree;
        int m_playerFreeI;
        bool m_audioOn;

        public static AudioManager instance;

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else {
                Destroy(gameObject);
                return;
            }

            m_audioOn = DBmanager.AudioOn;

            int n;
            if (m_bank != null) {
                n = m_bank.Length;

                DictionaryClipData clips;
                ClipData clipData;
                for (int i = 0; i < n; i++)
                {
                    clips = m_bank[i].m_clips;
                    foreach (KeyValuePair<string, ClipData> kp in clips)
                    {
                        if (m_clips.ContainsKey(kp.Key))
                        {
                            clipData = m_clips[kp.Key];
                            clipData.m_maxAtOnce += kp.Value.m_maxAtOnce;
                            m_clips[kp.Key] = clipData;
                        }
                        else
                        {
                            m_clips[kp.Key] = kp.Value;
                        }
                    }
                }
            }

            n = PlayersCount;
            m_playerFree = new ClipPlayer[PlayersCount];
            ClipPlayer player;
            m_playerFreeI = 0;
            foreach (KeyValuePair<string, ClipData> kp in m_clips)
            {
                n = kp.Value.m_maxAtOnce;
                kp.Value.m_key = kp.Key;
                for (int i = 0; i < n; i++)
                {
                    player = gameObject.AddComponent<ClipPlayer>();
                    player.m_manager = this;
                    m_playerFree[m_playerFreeI] = player;
                    m_playerFreeI++;
                }
            }
        }

        public int ClipsCount
        {
            get
            {
                return m_clips.Count;
            }
        }

        public int PlayersCount
        {
            get
            {
                int count = 0;
                foreach (KeyValuePair<string, ClipData> kp in m_clips)
                {
                    count += kp.Value.m_maxAtOnce;
                }
                return count;
            }
        }

        public ClipPlayer Play(string clipName, bool loop = false)
        {
            if (!m_audioOn) return null;
            if (m_playerFreeI < 1) {
                return null;
            }
            ClipData clip = m_clips[clipName];
            if (clip.IsFull)
            {
                return null;
            }
            m_playerFreeI--;
            ClipPlayer player = m_playerFree[m_playerFreeI];
            player.Play(clip.GetClip(), loop);
            return player;
        }

        public void Return(ClipPlayer player)
        {
            bool consume = player.m_clip.ReturnClip();
            if (consume) {
                Remove(player.m_clip.m_key);
            }
            m_playerFree[m_playerFreeI] = player;
            m_playerFreeI++;
        }

        public void Remove(string key)
        {
            m_clips.Remove(key);
        }
    }
}
