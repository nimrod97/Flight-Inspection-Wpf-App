using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;


namespace milestone1
{
    interface DllSimple
    {
        [DllImport("SimpleAnomalyDetectorDll.dll")]
        public static extern IntPtr getFeature1(IntPtr a, int index);
        [DllImport("SimpleAnomalyDetectorDll.dll")]
        public static extern IntPtr getFeature2(IntPtr a, int index);

        [DllImport("SimpleAnomalyDetectorDll.dll")]
        public static extern double getCorrelationValue(IntPtr a, int index);
        [DllImport("SimpleAnomalyDetectorDll.dll")]
        public static extern double getThreshold(IntPtr a, int index);

        [DllImport("SimpleAnomalyDetectorDll.dll")]

        public static extern IntPtr getDescription(IntPtr a, int index);

        [DllImport("SimpleAnomalyDetectorDll.dll")]
        public static extern int getTimeStep(IntPtr a, int index);

        [DllImport("SimpleAnomalyDetectorDll.dll")]
        public static extern IntPtr CreateSimpleAnomalyDetector();

        [DllImport("SimpleAnomalyDetectorDll.dll")]
        public static extern void SimpleLearnAndDetect(IntPtr a, string learnFile, string testFile, string[] properties, int size);
        //[DllImport("SimpleAnomalyDetectorDll.dll")]

        //public static extern void SimpleDetect(IntPtr a, string CSVfileName, string[] properties, int size);
        [DllImport("SimpleAnomalyDetectorDll.dll")]
        public static extern int SimpleVectorAnomalyReportSize(IntPtr a);
        [DllImport("SimpleAnomalyDetectorDll.dll")]
        public static extern int SimpleVectorCorrelatedFeaturesSize(IntPtr a);

    }
}
