//  Copyright 2011 Joey Scarr
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
using System.Windows.Forms;
using System.Reflection;
using System.Drawing;

namespace DotNetFX {
    /// <summary>
    /// An animation object that fades the opacity of a control between two limits.
    /// </summary>
    public class Fade : PredefinedEffect {
        private PropertyInfo m_OpacityProperty;

        /// <summary>
        /// Creates an animation object that fades the opacity of a control between two limits.
        /// 
        /// Start and End should be floats between 0 and 1.
        /// </summary>
        /// <param name="control">Windows Forms control to be used in the animation.</param>
        /// <param name="start">Start opacity.</param>
        /// <param name="end">End opacity.</param>
        /// <param name="duration">Length of animation in milliseconds.</param>
        /// <param name="accelFunc">Acceleration function, returns 0-1 for inputs 0-1.</param>
        public Fade(Control control, double start, double end, int duration, Func<double, double> accelFunc)
            : base(control, new double[] { start }, new double[] { end }, duration, accelFunc) {
            m_OpacityProperty = control.GetType().GetProperty("Opacity");
        }

        /// <summary>
        /// Animation event handler that will set the opacity of a control.
        /// </summary>
        protected override void UpdateStyle() {
            if (m_OpacityProperty != null) {
                m_OpacityProperty.SetValue(m_Control, m_Current[0], null);
            } else {
                m_Control.BackColor = Color.FromArgb((int)Math.Round(m_Current[0] * Byte.MaxValue), m_Control.BackColor);
            }
        }

        /// <summary>
        /// Animation event handler that will show the control.
        /// </summary>
        public void Show() {
            m_Control.Show();
        }

        /// <summary>
        /// Animation event handler that will hide the control.
        /// </summary>
        public void Hide() {
            m_Control.Hide();
        }
    }
}
