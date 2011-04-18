using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace DotNetFX {
    public class Animation {
        public const int TIMEOUT = 15;

        private double[] m_Start;
        private double[] m_End;
        private int m_Duration;
        private Func<double, double> m_AccelFunc;

        private double[] m_Current;
        private AnimationState m_State;
        private double m_Progress;

        public Animation(double[] start, double[] end, int duration, Func<double, double> accelFunc) {
            if (start.Length != end.Length) {
                throw new Exception("Start and end arrays must be the same length.");
            }
            m_Start = start;
            m_End = end;
            m_Duration = duration;
            m_AccelFunc = accelFunc;

            m_Current = new double[m_Start.Length];
            m_State = AnimationState.Stopped;
            m_Progress = 0;
        }

        public enum EventType {
            Play,
            Begin,
            Resume,
            End,
            Stop,
            Finish,
            Pause,
            Animate,
            Destroy
        }

        public enum AnimationState {
            Stopped,
            Paused,
            Playing
        }

        public AnimationState State {
            get { return m_State; }
        }

        public double[] Coordinates {
            get { return m_Current; }
        }

        public double Progress {
            get { return m_Progress; }
        }

        public bool Play() {
            return Play(false);
        }

        public bool Play(bool restart) {
            if (restart || m_State == AnimationState.Stopped) {
                m_Progress = 0;
                m_Current = m_Start;
            } else if (m_State == AnimationState.Playing) {
                return false;
            }

            if (m_Progress == 0) {
                OnBegin();
            }

            OnPlay();

            if (m_State == AnimationState.Paused) {
                OnResume();
            }

            m_State = AnimationState.Playing;

            // TODO: Start animation timer here

            return true;
        }

        public void Stop(bool gotoEnd) {
            // TODO: Stop animation timer here
            m_State = AnimationState.Stopped;

            if (gotoEnd) {
                m_Progress = 1;
            }

            UpdateCoords(m_Progress);

            OnStop();
            OnEnd();
        }

        public void Pause() {
            if (m_State == AnimationState.Playing) {
                // TODO: Pause animation timer here
                m_State = AnimationState.Paused;
                OnPause();
            }
        }

        private void Cycle() {
            // TODO: Replace the below line with a calculation based on timestamps
            m_Progress = 0;

            if (m_Progress >= 1) {
                m_Progress = 1;
            }

            if (m_AccelFunc == null) {
                UpdateCoords(m_Progress);
            } else {
                UpdateCoords(m_AccelFunc(m_Progress));
            }

            // Animation has finished.
            if (m_Progress == 1) {
                m_State = AnimationState.Stopped;
                // TODO: Stop timer here

                OnFinish();
                OnEnd();
            } else if (m_State == AnimationState.Playing) {
                OnAnimate();
            }
        }

        private void UpdateCoords(double t) {
            m_Current = new double[m_Start.Length];
            for (int i = 0; i < m_Start.Length; i++) {
                m_Current[i] = (m_End[i] - m_Start[i]) * t + m_Start[i];
            }
        }

        #region Events
        private void OnAnimate() {
            if (Animate != null) {
                Animate(this);
            }
        }

        private void OnBegin() {
            if (Begin != null) {
                Begin(this);
            }
        }

        private void OnEnd() {
            if (End != null) {
                End(this);
            }
        }

        private void OnFinish() {
            if (Finish != null) {
                Finish(this);
            }
        }

        private void OnPause() {
            if (PauseEvent != null) {
                PauseEvent(this);
            }
        }

        private void OnPlay() {
            if (PlayEvent != null) {
                PlayEvent(this);
            }
        }

        private void OnResume() {
            if (Resume != null) {
                Resume(this);
            }
        }

        private void OnStop() {
            if (StopEvent != null) {
                StopEvent(this);
            }
        }

        public event AnimationEvent Animate;
        public event AnimationEvent Begin;
        public event AnimationEvent End;
        public event AnimationEvent Finish;
        public event AnimationEvent PauseEvent;
        public event AnimationEvent PlayEvent;
        public event AnimationEvent Resume;
        public event AnimationEvent StopEvent;
        #endregion
    }

    public delegate void AnimationEvent(Animation animation);
}
