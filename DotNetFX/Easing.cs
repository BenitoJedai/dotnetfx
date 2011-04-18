using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetFX {
    /// <summary>
    /// Easing functions for animations.
    /// </summary>
    public static class Easing {

        /// <summary>
        /// Ease in - start slow and speed up.
        /// </summary>
        /// <param name="t">Input between 0 and 1.</param>
        /// <returns>Output between 0 and 1.</returns>
        public static double EaseIn(double t) {
            return t * t * t;
        }

        /// <summary>
        /// Ease out - start fast and slow to a stop.
        /// </summary>
        /// <param name="t">Input between 0 and 1.</param>
        /// <returns>Output between 0 and 1.</returns>
        public static double EaseOut(double t) {
            return 1 - Math.Pow(1 - t, 3);
        }

        /// <summary>
        /// Ease in and out - start slow, speed up, then slow down.
        /// </summary>
        /// <param name="t">Input between 0 and 1.</param>
        /// <returns>Output between 0 and 1.</returns>
        public static double EaseInAndOut(double t) {
            return 3 * t * t - 2 * t * t * t;
        }
    }
}
