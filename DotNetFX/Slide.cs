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

namespace DotNetFX {
    /// <summary>
    /// Creates an animation object that will slide an element from A to B.
    /// </summary>
    public class Slide : PredefinedEffect {
        /// <summary>
        /// Creates an animation object that will slide an element from A to B.
        /// 
        /// Start and End should be 2 dimensional arrays.
        /// </summary>
        /// <param name="control">Windows Forms control to be used in the animation.</param>
        /// <param name="start">2D array for start co-ordinates (x, y).</param>
        /// <param name="end">2D array for end co-ordinates (x, y).</param>
        /// <param name="duration">Length of animation in milliseconds.</param>
        /// <param name="accelFunc">Acceleration function, returns 0-1 for inputs 0-1.</param>
        public Slide(Control control, double[] start, double[] end, int duration, Func<double, double> accelFunc)
            : base(control, start, end, duration, accelFunc) {
            if (start.Length != 2 || end.Length != 2) {
                throw new AnimationException("Start and end points must be 2D");
            }
        }

        /// <inheritdoc/>
        protected override void UpdateStyle() {
            m_Control.Left = (int)Math.Round(m_Current[0]);
            m_Control.Top = (int)Math.Round(m_Current[1]);
        }
    }
}
