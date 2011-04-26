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
    /// Abstract class that provides reusable functionality for predefined animations that
    /// manipulate a single Windows Forms control.
    /// </summary>
    public abstract class PredefinedEffect : Animation {
        /// <summary>
        /// Windows Forms control that will be used in the animation.
        /// </summary>
        protected Control m_Control;

        /// <summary>
        /// Constructs a PredefinedEffect.
        /// </summary>
        /// <param name="control">The Windows Forms control to be used in the animation.</param>
        /// <param name="start">Array of start co-ordinates.</param>
        /// <param name="end">Array of end co-ordinates.</param>
        /// <param name="duration">Length of animation in milliseconds.</param>
        /// <param name="accelFunc">Acceleration function, returns 0-1 for inputs 0-1.</param>
        public PredefinedEffect(Control control, double[] start, double[] end, int duration, Func<double, double> accelFunc)
            : base(start, end, duration, accelFunc) {
            m_Control = control;
        }

        /// <summary>
        /// Called to update the style of the element.
        /// </summary>
        protected abstract void UpdateStyle();

        /// <inheritdoc/>
        protected override void OnAnimate() {
            this.UpdateStyle();
            base.OnAnimate();
        }

        /// <inheritdoc/>
        protected override void OnEnd() {
            this.UpdateStyle();
            base.OnEnd();
        }

        /// <inheritdoc/>
        protected override void OnBegin() {
            this.UpdateStyle();
            base.OnBegin();
        }
    }
}
