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
    /// An animation object that resizes a control between two widths and heights.
    /// </summary>
    public class Resize : PredefinedEffect {
        /// <summary>
        /// Creates an animation object that will resize an element between two widths and heights.
        /// 
        /// Start and End should be 2 dimensional arrays.
        /// </summary>
        /// <param name="control">Windows Forms control to be used in the animation.</param>
        /// <param name="start">2D array for start width and height.</param>
        /// <param name="end">2D array for end width and height.</param>
        /// <param name="duration">Length of animation in milliseconds.</param>
        /// <param name="accelFunc">Acceleration function, returns 0-1 for inputs 0-1.</param>
        public Resize(Control control, double[] start, double[] end, int duration, Func<double, double> accelFunc)
            : base(control, start, end, duration, accelFunc) {
            if (start.Length != 2 || end.Length != 2) {
                throw new AnimationException("Start and end points must be 2D");
            }
        }

        /// <summary>
        /// Animation event handler that will resize a control by setting its width and height.
        /// </summary>
        protected override void UpdateStyle() {
            m_Control.Width = (int)Math.Round(m_Current[0]);
            m_Control.Height = (int)Math.Round(m_Current[1]);
        }
    }
}
