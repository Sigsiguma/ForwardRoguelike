using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace utility.sound {
    public static class SoundSystem {

        private static Dictionary<int, List<SoundParam>> sound_play_list_ = null;
        private static MonoBehaviour mono_behaviour_ = null;

        public static bool Init() {
            if (sound_play_list_ == null) {
                mono_behaviour_ = SoundResources.mono_behaviour_;
                sound_play_list_ = new Dictionary<int, List<SoundParam>>();
                AddCategory((int)SoundSettings.SoundCategory.BGM);
                AddCategory((int)SoundSettings.SoundCategory.SE);

                Debug.Log("<color=green> SoundSystem is initialized!!! </color>");
                return true;
            }

            Debug.LogWarning("SoundSystem has been initialized!!!");
            return false;
        }

        public static void Release() {
            sound_play_list_.Clear();
            sound_play_list_ = null;
            mono_behaviour_ = null;
        }

        public static void Play(SoundParam param) {

            Debug.Assert(param != null, "Param is null!!!!!");

            SetSoundResource(param);

            if (param.is_oneshot_) {
                param.source_.PlayOneShot(param.clip_, param.volume_);
                mono_behaviour_.StartCoroutine(StopDetect(param));
            } else {
                param.source_.Play();
            }
        }

        public static void Stop(string file_name, int category_id, float fade_time = 0.0f) {

            List<SoundParam> category_play_list = sound_play_list_[category_id];

            Debug.Assert(category_play_list != null, "The category doesn't exist !!!!");

            SoundParam param = category_play_list.Find((par) => { return par.file_name_ == file_name; });

            Debug.Assert(param != null, "The filename doesn't exist !!!!");

            mono_behaviour_.StartCoroutine(FadeOut(param, fade_time,
                                                     () => {
                                                         category_play_list.Remove(param);
                                                         if (param.onFinish != null) {
                                                             param.onFinish();
                                                         }
                                                     }));
        }


        public static void StopCategoryAll(int category_id) {

            List<SoundParam> category_play_list = sound_play_list_[category_id];

            Debug.Assert(category_play_list != null, "The category doesn't exist !!!!");

            foreach (SoundParam param in category_play_list) {
                Stop(param);
            }

            category_play_list.Clear();
        }

        public static void Pause(string file_name, int category_id) {

            SoundParam param = GetSoundParam(file_name, category_id);

            if (param.source_.isPlaying && !param.is_pause_) {
                param.source_.Pause();
                param.is_pause_ = true;
            } else {
                Debug.LogWarning("Can't pause !!!!");
            }
        }

        public static void UnPause(string file_name, int category_id) {

            SoundParam param = GetSoundParam(file_name, category_id);

            if (param.source_.isPlaying || param.is_pause_) {
                param.source_.UnPause();
                param.is_pause_ = false;
            } else {
                Debug.LogWarning("Can't unpause!!!");
            }
        }

        public static void PauseCategoryAll(int category_id) {
            foreach (SoundParam param in sound_play_list_[category_id]) {
                param.source_.Pause();
                param.is_pause_ = true;
            }
        }

        public static void UnPauseCategoryAll(int category_id) {
            foreach (SoundParam param in sound_play_list_[category_id]) {
                param.source_.UnPause();
                param.is_pause_ = false;
            }
        }

        public static void MasterVolumeSet(float volume) {

            if (volume >= 0.0f && volume <= 1.0f) {
                AudioListener.volume = volume;
            } else {
                Debug.LogWarning("Please input in the range from 0 to 1!!!");
            }
        }

        public static float MasterVolumeGet() {
            return AudioListener.volume;
        }

        public static bool BGMContain(string file_name) {
            List<SoundParam> bgm_list = sound_play_list_[(int)SoundSettings.SoundCategory.BGM];

            Debug.Assert(bgm_list != null, "The bgmlist doesn't exist!!!");

            foreach (SoundParam param in bgm_list) {

                if (param.file_name_ == file_name) {
                    return true;
                }
            }

            return false;
        }

        private static void Stop(SoundParam param) {
            param.source_.Stop();

            if (param.onFinish != null) {
                param.onFinish();
            }

            param.source_.clip = null;
        }

        private static SoundParam GetSoundParam(string file_name, int category_id) {

            List<SoundParam> category_play_list = sound_play_list_[category_id];

            Debug.Assert(category_play_list != null, "The category doesn't exist!!!");

            SoundParam param = category_play_list.Find((par) => { return par.file_name_ == file_name; });

            Debug.Assert(param != null, "The filename doesn't exist !!!!");

            return param;
        }

        private static IEnumerator StopDetect(SoundParam param) {

            Debug.Assert(param != null, "param is null!!!!!");

            while (param.source_.isPlaying || param.is_pause_) {
                yield return null;
            }

            List<SoundParam> category_play_list = sound_play_list_[param.category_id_];
            param.source_.clip = null;

            if (param.onFinish != null) {
                param.onFinish();
            }

            category_play_list.Remove(param);
        }

        private static IEnumerator FadeOut(SoundParam param, float fade_time, System.Action onFinish = null) {

            if (fade_time > 0.0f) {
                float frame_count_ = param.source_.volume / (fade_time * SoundSettings.frame_rate_);

                while (param.source_.volume > 0.0f) {
                    param.source_.volume -= frame_count_;
                    yield return null;
                }
            }

            param.source_.Stop();
            param.source_.clip = null;

            if (onFinish != null) {
                onFinish();
            }
        }

        private static void SetSoundResource(SoundParam param) {
            param.source_ = SoundResources.GetAudioSource(param.category_id_);

            param.source_.clip = param.clip_;
            param.source_.volume = param.volume_;
            param.source_.loop = param.loop_;

            sound_play_list_[param.category_id_].Add(param);
        }


        private static void AddCategory(int category_id) {
            if (!sound_play_list_.ContainsKey(category_id)) {
                sound_play_list_.Add(category_id, new List<SoundParam>());
            } else {
                Debug.LogWarning("The key already exist!!");
            }
        }

    }
}

