using UnityEditor;
using UnityEngine;

namespace Gilzoide.TweenJobs.Editor
{
    [CustomEditor(typeof(ATweenComponent), true)]
    public class ATweenComponentEditor : UnityEditor.Editor
    {
        private dynamic _initialValue;
        private bool _isPlaying;
        private double _editorTime;

        private bool IsPaused
        {
            get
            {
                if (_isPlaying)
                {
                    return false;
                }

                dynamic tweener = GetTweener();
                return tweener.Time > 0 && tweener.Time < tweener.TotalDuration;
            }
        }

        private void OnDisable()
        {
            Stop();
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            using (new EditorGUILayout.HorizontalScope())
            {
                if (_isPlaying)
                {
                    if (GUILayout.Button("Pause"))
                    {
                        Pause();
                    }
                    if (GUILayout.Button("Stop"))
                    {
                        Stop();
                    }
                }
                else
                {
                    if (GUILayout.Button(IsPaused ? "Resume" : "Play"))
                    {
                        Play();
                    }
                    if (GUILayout.Button("Reset"))
                    {
                        Stop();
                    }
                }

                dynamic tweener = GetTweener();
                if (_isPlaying && tweener.Duration > 0)
                {
                    double now = EditorApplication.timeSinceStartup;
                    tweener.Time += now - _editorTime;
                    _editorTime = now;
                }

                float time = tweener.Time;
                float newTime = EditorGUILayout.Slider(time, 0, tweener.TotalDuration);
                if (_isPlaying || time != newTime)
                {
                    if (_initialValue == null)
                    {
                        _initialValue = GetTweener().Value;
                    }
                    tweener.Time = newTime;
                    dynamic jobData = tweener.InitialJobData;
                    jobData.Execute();
                    tweener.SyncJobData(jobData);
                    tweener.Time = newTime;
                    EditorUtility.SetDirty(target);

                    if (_isPlaying && jobData.IsComplete)
                    {
                        Pause();
                    }
                }
            }
        }

        public override bool RequiresConstantRepaint()
        {
            return _isPlaying;
        }

        private void Play()
        {
            if (!IsPaused)
            {
                Stop();
                _initialValue = GetTweener().Value;
            }
            _isPlaying = true;
            _editorTime = EditorApplication.timeSinceStartup;
        }

        private void Pause()
        {
            _isPlaying = false;
        }

        private void Stop()
        {
            _isPlaying = false;
            if (_initialValue != null)
            {
                GetTweener().Value = _initialValue;
                _initialValue = null;
            }
            GetTweener().Time = 0;
        }

        private dynamic GetTweener()
        {
            return ((ATweenComponent) target).GetTweener();
        }
    }
}
