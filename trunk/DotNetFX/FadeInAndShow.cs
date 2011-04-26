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
    /// Sets a control to be visible and then fades it in from transparent to opaque.
    /// </summary>
    public class FadeInAndShow : FadeIn {
        /// <summary>
        /// Constructs an animation that sets a control to be visible and then fades it in from
        /// transparent to opaque.
        /// </summary>
        /// <param name="control">The Windows Forms control to be used in the animation.</param>
        /// <param name="duration">Length of animation in milliseconds.</param>
        /// <param name="accelFunc">Acceleration function, returns 0-1 for inputs 0-1.</param>
        public FadeInAndShow(Control control, int duration, Func<double, double> accelFunc)
            : base(control, duration, accelFunc) {
        }

        /// <inheritdoc/>
        protected override void OnBegin() {
            this.Show();
            base.OnBegin();
        }
    }
}
