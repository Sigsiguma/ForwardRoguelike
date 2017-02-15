using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace utility.sound {
    public static class SoundResources {

        private static GameObject sound_manager_ = null;
        private static List<AudioSource> bgm_sources_ = null;
        private static List<AudioSource> se_sources_ = null;
        public static MonoBehaviour mono_behaviour_ { get; private set; }

        public static void Init() {
            var obj = GameObject.Find("SoundManager");

            if (obj == null) {
                bgm_sources_ = new List<AudioSource>();
                se_sources_ = new List<AudioSource>();

                sound_manager_ = new GameObject("SoundManager");
                mono_behaviour_ = sound_manager_.AddComponent<GetMonoBehaviour>().mono_behaviour_;
                Object.DontDestroyOnLoad(sound_manager_);

                for (int i = 0; i < SoundSettings.bgm_channel_; ++i) {
                    bgm_sources_.Add(sound_manager_.AddComponent<AudioSource>());
                }

                for (int i = 0; i < SoundSettings.se_channel_; ++i) {
                    se_sources_.Add(sound_manager_.AddComponent<AudioSource>());
                }

                Debug.Log("<color=green> SoundResources is initialized!!! </color>");

            } else {
                Debug.Log("SoundManager already exist!!");
            }
        }

        //TODO:キャッシュとかできたらいいのかもなあ…
        public static AudioSource GetAudioSource(int category_id_) {

            Debug.Assert(bgm_sources_ != null, "AudioSource doesn't initialized!!!!");
            Debug.Assert(se_sources_ != null, "AudioSource doesn't initialized!!!!");


            if (category_id_ == (int)SoundSettings.SoundCategory.BGM) {

                for (int i = 0; i < bgm_sources_.Count; ++i) {
                    if (bgm_sources_[i].clip == null) {
                        return bgm_sources_[i];
                    }
                }

                Debug.LogWarning("BGMAudioSource is not enough");
                return null;
            } else if (category_id_ == (int)SoundSettings.SoundCategory.SE) {

                for (int i = 0; i < se_sources_.Count; ++i) {
                    if (se_sources_[i].clip == null) {
                        return se_sources_[i];
                    }
                }

                Debug.LogWarning("SEAudioSource is not enough");
                return null;
            }

            Debug.LogError("The category doesn't exist!!!!");
            return null;
        }


    }
}

