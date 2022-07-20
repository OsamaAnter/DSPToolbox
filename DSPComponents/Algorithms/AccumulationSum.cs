using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;


namespace DSPAlgorithms.Algorithms
{
    public class AccumulationSum : Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal OutputSignal { get; set; }

        public override void Run()
        {
            List<float> sampleResult = new List<float>();

            for (int i = 0; i < InputSignal.Samples.Count ; i++)
            {
                float sym_y_n = 0;
                for (int j = 0; j <= i; j++)
                {
                    sym_y_n += InputSignal.Samples[j];
                }
                Console.WriteLine(sym_y_n);
                sampleResult.Add(sym_y_n);
            }
            OutputSignal = new Signal(sampleResult, false);
        }
    }
}
