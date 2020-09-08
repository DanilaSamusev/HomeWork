using PerformanceCounterHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoggingAndMonitoring.Infrastructure
{
    [PerformanceCounterCategoryAttribute
        ("LoggingAndMonitoring",
        System.Diagnostics.PerformanceCounterCategoryType.MultiInstance,
        "LoggingAndMonitoring")]
    public enum Counters
    {
        [PerformanceCounter
            ("Go to Home count",
            "Go to home",
            System.Diagnostics.PerformanceCounterType.NumberOfItems32)]
        GoToIndex,
    }
}
