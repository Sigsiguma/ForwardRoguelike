using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace utility.fade {
    public static class Fade {

        private static MonoBehaviour mono_behaviour_ = null;
        private static Image fade_image_ = null;

        public static void Init(MonoBehaviour mono_behaviour, Image image) {
            Debug.Assert(mono_behaviour != null, "Monobehaviour is required!!!!");
            Debug.Assert(image != null, "Image is required!!!!");

            if (mono_behaviour_ == null && fade_image_ == null) {
                mono_behaviour_ = mono_behaviour;
                fade_image_ = image;
            } else {
                Debug.LogWarning("Fade has been initialized!!!!");
            }

        }

        public static void FadeOut(float fade_time, System.Action onFinish = null) {
            mono_behaviour_.StartCoroutine(FadeOutCoroutine(fade_time, onFinish));
        }

        public static void FadeIn(float fade_time, System.Action onFinish = null) {
            mono_behaviour_.StartCoroutine(FadeInCoroutine(fade_time, onFinish));
        }

        public static void FadeInOut(float fade_time, string scene_name, System.Action onFinish = null) {
            FadeIn(fade_time, () => {
                SceneManager.LoadScene(scene_name);
                FadeOut(fade_time, onFinish);
            });
        }

        private static IEnumerator FadeOutCoroutine(float fade_time, System.Action onFinish = null) {

            float end_time = Time.time + fade_time;

            var end_frame = new WaitForEndOfFrame();

            var color = fade_image_.color;


            while (Time.time < end_time) {
                color.a = (end_time - Time.time) / fade_time;
                fade_image_.color = color;
                yield return end_frame;
            }

            color.a = 0;
            fade_image_.color = color;

            if (onFinish != null) {
                onFinish();
            }

            fade_image_.enabled = false;
        }

        private static IEnumerator FadeInCoroutine(float fade_time, System.Action onFinish = null) {

            fade_image_.enabled = true;

            float end_time = Time.time + fade_time;

            var end_frame = new WaitForEndOfFrame();

            var color = fade_image_.color;

            while (Time.time < end_time) {
                color.a = 1 - ((end_time - Time.time) / fade_time);
                fade_image_.color = color;
                yield return end_frame;
            }

            color.a = 1;
            fade_image_.color = color;

            if (onFinish != null) {
                onFinish();
            }
        }
    }
}
