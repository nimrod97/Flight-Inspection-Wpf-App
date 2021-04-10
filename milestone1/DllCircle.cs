using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace milestone1
{
    interface DllCircle
    {
        [DllImport("CircleAnomalyDetectorDll.dll")]
        public static extern IntPtr getFeature1(IntPtr a, int index);
        [DllImport("CircleAnomalyDetectorDll.dll")]
        public static extern IntPtr getFeature2(IntPtr a, int index);

        [DllImport("CircleAnomalyDetectorDll.dll")]
        public static extern double getCorrelationValue(IntPtr a, int index);

        [DllImport("CircleAnomalyDetectorDll.dll")]
        public static extern int getTimeStep(IntPtr a, int index);
        [DllImport("CircleAnomalyDetectorDll.dll")]
        public static extern IntPtr CreateCircleAnomalyDetector();
        [DllImport("CircleAnomalyDetectorDll.dll")]
        public static extern void CircleLearnNormal(IntPtr a, string CSVfileName, string[] properties, int size);
        [DllImport("CircleAnomalyDetectorDll.dll")]
        public static extern void CircleDetect(IntPtr a, string CSVfileName, string[] properties, int size);

        [DllImport("CircleAnomalyDetectorDll.dll")]
        public static extern int CircleVectorAnomalyReportSize(IntPtr a);
        [DllImport("CircleAnomalyDetectorDll.dll")]
        public static extern int CircleVectorCorrelatedFeaturesSize(IntPtr a);

    }
}
