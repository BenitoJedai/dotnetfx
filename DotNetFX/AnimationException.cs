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

namespace DotNetFX {
    /// <summary>
    /// An exception that is fired when something goes wrong with animation.
    /// </summary>
    public class AnimationException : Exception {
        /// <summary>
        /// Constructs a new AnimationException.
        /// </summary>
        /// <param name="message">The exception message to display.</param>
        public AnimationException(string message) : base(message) { }
    }
}
