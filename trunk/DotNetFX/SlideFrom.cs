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
    /// Slides an element from its current position.
    /// </summary>
    public class SlideFrom : Slide {
        /// <summary>
        /// Constructs an animation object that slides an element from its current position.
        /// </summary>
        /// <param name="control">The Windows Forms control to be used in the animation.</param>
        /// <param name="end">2D array for end co-ordinates (X, Y).</param>
        /// <param name="duration">Length of animation in milliseconds.</param>
        /// <param name="accelFunc">Acceleration function, returns 0-1 for inputs 0-1.</param>
        public SlideFrom(Control control, double[] end, int duration, Func<double, double> accelFunc)
            : base(control, new double[] { control.Left, control.Top }, end, duration, accelFunc) {
        }

        /// <inheritdoc/>
        protected override void OnBegin() {
            m_Start = new double[] { m_Control.Left, m_Control.Top };
            base.OnBegin();
        }
    }
}
