using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlantsVsZombies.Counter
{
    /// <summary>
    /// Counter processor
    /// </summary>
    /// <param name="counter"></param>
    public delegate void EventOnCounterMeet(ICounter counter);

    /// <summary>
    /// Implements a countdown
    /// </summary>
    public interface ICounter
    {
        /// <summary>
        /// True if counter is evented
        /// </summary>
        bool IsMeet { get; }

        /// <summary>
        /// Start counter
        /// </summary>
        void Start();

        /// <summary>
        /// Reset counter
        /// </summary>
        void Reset();

        /// <summary>
        /// Pause counter
        /// </summary>
        void Pause();

        /// <summary>
        /// Stop counter
        /// </summary>
        void Stop();

        /// <summary>
        /// Event: Process when counter is met
        /// </summary>
        event EventOnCounterMeet OnMeet;
    }
}
