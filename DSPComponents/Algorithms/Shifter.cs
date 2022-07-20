using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Shifter : Algorithm
    {
        public Signal InputSignal { get; set; }
        public int ShiftingValue { get; set; }
        public Signal OutputShiftedSignal { get; set; }

        public override void Run()
        {
            List<float> sampleResult = new List<float>();
            List<int> sampleIndex = new List<int>();
            int k = ShiftingValue;

            for (int i = 0; i < InputSignal.Samples.Count; i++) 
            {
                sampleIndex.Add(InputSignal.SamplesIndices[i] - ShiftingValue);
                sampleResult.Add(InputSignal.Samples[i]);
                Console.WriteLine(InputSignal.SamplesIndices[i] + "  --->  " + InputSignal.Samples[i]);
            }
            OutputShiftedSignal = new Signal(sampleResult, sampleIndex, false);
            // throw new NotImplementedException();
        }
    }
}
