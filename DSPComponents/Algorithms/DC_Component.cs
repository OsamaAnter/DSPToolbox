using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSPAlgorithms.Algorithms
{
    public class DC_Component: Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal OutputSignal { get; set; }

        public override void Run()
        {
            List<float> res = new List<float>();
            float sum = InputSignal.Samples.Sum();
            int N = InputSignal.Samples.Count;
            float avr = (sum / N);

            for (int i = 0; i < N; i++)
            {
                res.Add(InputSignal.Samples[i] - avr);
            }

            OutputSignal = new Signal(res, false);
            //throw new NotImplementedException();
        }
    }
}
