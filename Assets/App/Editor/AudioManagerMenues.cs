using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace AudioManagement{
    public class AudioManagerMenues {

        [MenuItem("Assets/Create/AudioManager/New AudioBank")]
        [MenuItem("AudioManager/New AudioBank")]
        public static void AudioBank_new()
        {
            TasksMenu.CreateAsset<AudioBank>();
        }
    }
}
