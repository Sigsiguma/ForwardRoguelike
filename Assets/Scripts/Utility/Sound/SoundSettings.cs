using UnityEngine;
using System.Collections;

namespace utility.sound {
    public static class SoundSettings {

        public const string base_path_ = "Audio/";

        public const string bgm_path_ = base_path_ + "BGM/";
        public const string se_path_ = base_path_ + "SE/";

        public const int bgm_channel_ = 3;
        public const int se_channel_ = 16;

        public const int frame_rate_ = 60;

        public enum SoundCategory {
            NONE,
            BGM,
            SE
        };
    }

    public class SoundParam {
        public AudioClip clip_;
        public AudioSource source_;
        public string file_name_;
        public bool loop_;
        public bool is_oneshot_;
        public bool is_pause_;
        public float volume_;
        public int category_id_;
        public System.Action onFinish;
    }
}