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
using System.Windows.Forms;

namespace DotNetFX {
    /// <summary>
    /// An animation object that resizes a control between two heights.
    /// </summary>
    public class ResizeHeight : PredefinedEffect {
        /// <summary>
        /// Creates an animation object that will resize an element between two heights.
        /// </summary>
        /// <param name="control">Windows Forms control to be used in the animation.</param>
        /// <param name="start">Start height.</param>
        /// <param name="end">End height.</param>
        /// <param name="duration">Length of animation in milliseconds.</param>
        /// <param name="accelFunc">Acceleration function, returns 0-1 for inputs 0-1.</param>
        public ResizeHeight(Control control, double start, double end, int duration, Func<double, double> accelFunc)
            : base(control, new double[] { start }, new double[] { end }, duration, accelFunc) {
        }

        /// <summary>
        /// Animation event handler that will resize an element by setting its height.
        /// </summary>
        protected override void UpdateStyleInternal() {
            m_Control.Height = (int)Math.Round(m_Current[0]);
        }
    }
}
