using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Adder : Algorithm
    {
        public List<Signal> InputSignals { get; set; }
        public Signal OutputSignal { get; set; }

        public override void Run()
        {
            
            List <float> result = new List<float>();
            float sample = 0;

            for (int j = 0; j < InputSignals[0].Samples.Count; j++)
            {
                sample = InputSignals[0].Samples[j] + InputSignals[1].Samples[j];
                result.Add(sample);
            }
            
            OutputSignal = new Signal(result, false);
            //throw new NotImplementedException();
        }
    }
}