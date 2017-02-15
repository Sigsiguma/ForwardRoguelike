using UnityEngine;
using System.Collections;

namespace utility.sound {

    public abstract class SoundBase {

        protected string path_;
        protected int category_id_;

        public void Play(string file_name, bool loop, bool one_shot, System.Action onFinish = null, float volume = 1.0f) {

            Debug.Assert(!string.IsNullOrEmpty(file_name), "filename is empty!!!!");

            SoundParam param = new SoundParam();

            AudioClip clip = Resources.Load<AudioClip>(path_ + file_name);

            Debug.Assert(clip != null, file_name + " doesn't exist!!!!");

            param.clip_ = clip;
            param.file_name_ = file_name;
            param.loop_ = loop;
            param.is_oneshot_ = one_shot;
            param.volume_ = volume;
            param.category_id_ = category_id_;
            param.onFinish = onFinish;

            SoundSystem.Play(param);
        }

        public void Stop(string file_name, float fade_time) {

            Debug.Assert(!string.IsNullOrEmpty(file_name), "filename is empty!!!!");

            SoundSystem.Stop(file_name, category_id_, fade_time);
        }

        public void StopCategoryAll() {
            SoundSystem.StopCategoryAll(category_id_);
        }

        public void Pause(string file_name) {

            Debug.Assert(!string.IsNullOrEmpty(file_name), "filename is empty!!!!");

            SoundSystem.Pause(file_name, category_id_);
        }

        public void UnPause(string file_name) {

            Debug.Assert(!string.IsNullOrEmpty(file_name), "filename is empty!!!!");

            SoundSystem.UnPause(file_name, category_id_);
        }

        public void PauseCategoryAll() {
            SoundSystem.PauseCategoryAll(category_id_);
        }

        public void UnPauseCategoryAll() {
            SoundSystem.UnPauseCategoryAll(category_id_);
        }

        public void MasterVolumeSet(float volume) {
            SoundSystem.MasterVolumeSet(volume);
        }

        public float MasterVolumeGet() {
            return SoundSystem.MasterVolumeGet();
        }
    }

}