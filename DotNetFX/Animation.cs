﻿//  Copyright 2011 Joey Scarr
//
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//
//   http://www.apache.org/licenses/LICENSE-2.0
//
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.
//
//  This code is derivative of Google's Closure Animation Library, ported to C#
//  by Joey Scarr.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace DotNetFX {
    public class Animation {
        public const int TIMEOUT = 15;
        private static HashSet<Animation> s_ActiveAnimations = new HashSet<Animation>();
        private static Timer s_GlobalTimer = null;

        private static void StartGlobalTimer() {
            s_GlobalTimer = new Timer(TIMEOUT);
            s_GlobalTimer.Elapsed += CycleAnimations;
            s_GlobalTimer.Start();
        }

        private static void KillGlobalTimer() {
            if (s_GlobalTimer != null) {
                s_GlobalTimer.Stop();
                s_GlobalTimer.Dispose();
                s_GlobalTimer = null;
            }
        }

        private static void CycleAnimations(object sender, ElapsedEventArgs e) {
            DateTime now = DateTime.Now;

            foreach (Animation animation in s_ActiveAnimations) {
                animation.Cycle(now);
            }

            if (s_ActiveAnimations.Count == 0) {
                KillGlobalTimer();
            } else {
                StartGlobalTimer();
            }
        }

        private static void RegisterAnimation(Animation animation) {
            if (!s_ActiveAnimations.Contains(animation)) {
                s_ActiveAnimations.Add(animation);
            }

            // If the timer is not already started, start it now.
            if (!s_GlobalTimer.Enabled) {
                StartGlobalTimer();
            }
        }

        private static void UnregisterAnimation(Animation animation) {
            s_ActiveAnimations.Remove(animation);

            // If the global timer is running and we no longer have any active animations, we stop the timer.
            if (s_GlobalTimer != null && s_GlobalTimer.Enabled && s_ActiveAnimations.Count == 0) {
                KillGlobalTimer();
            }
        }

        private double[] m_Start;
        private double[] m_End;
        private int m_Duration;
        private Func<double, double> m_AccelFunc;

        private DateTime m_StartTime;
        private DateTime m_LastFrame;
        private int m_Fps;
        private DateTime m_EndTime;
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

            UnregisterAnimation(this);

            m_StartTime = DateTime.Now;

            if (m_State == AnimationState.Paused) {
                m_StartTime.AddMilliseconds(-m_Duration * m_Progress);
            }

            m_EndTime = m_StartTime + new TimeSpan(m_Duration * 10000);
            m_LastFrame = m_StartTime;

            if (m_Progress == 0) {
                OnBegin();
            }

            OnPlay();

            if (m_State == AnimationState.Paused) {
                OnResume();
            }

            m_State = AnimationState.Playing;

            RegisterAnimation(this);
            Cycle(m_StartTime);

            return true;
        }

        public void Stop(bool gotoEnd) {
            UnregisterAnimation(this);
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
                UnregisterAnimation(this);
                m_State = AnimationState.Paused;
                OnPause();
            }
        }

        private void Cycle(DateTime now) {
            m_Progress = (now - m_StartTime).TotalMilliseconds / (m_EndTime - m_StartTime).TotalMilliseconds;

            if (m_Progress >= 1) {
                m_Progress = 1;
            }

            m_Fps = (int)(1000d / (now - m_LastFrame).TotalMilliseconds);
            m_LastFrame = now;

            if (m_AccelFunc == null) {
                UpdateCoords(m_Progress);
            } else {
                UpdateCoords(m_AccelFunc(m_Progress));
            }

            // Animation has finished.
            if (m_Progress == 1) {
                m_State = AnimationState.Stopped;
                UnregisterAnimation(this);

                OnFinish();
                OnEnd();
            // Animation is still under way.
            } else if (m_State == AnimationState.Playing) {
                OnAnimate();
            }
        }

        private void UpdateCoords(double t) {
            if (t == 1) {
                m_Current = m_End;
            } else {
                m_Current = new double[m_Start.Length];
                for (int i = 0; i < m_Start.Length; i++) {
                    m_Current[i] = (m_End[i] - m_Start[i]) * t + m_Start[i];
                }
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