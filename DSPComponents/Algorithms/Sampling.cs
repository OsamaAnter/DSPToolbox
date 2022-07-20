using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSPAlgorithms.Algorithms
{
    public class Sampling : Algorithm
    {
        public int L { get; set; } //upsampling factor
        public int M { get; set; } //downsampling factor
        public Signal InputSignal { get; set; }
        public Signal OutputSignal { get; set; }
        
        public override void Run()
        {
           int Ninput = InputSignal.Samples.Count;

            //values same read me file
            FIR fir = new FIR();
            fir.InputFilterType = DSPAlgorithms.DataStructures.FILTER_TYPES.LOW;
            fir.InputFS = 8000;
            fir.InputStopBandAttenuation = 50;
            fir.InputCutOffFrequency = 1500;
            fir.InputTransitionBand = 500;
            List<float> o = new List<float>();
            OutputSignal = new Signal(o, false);
            if (M==0 && L==0)
            {
                Console.WriteLine(" massege error M = 0 & L = 0");
                return ;
            }
            else if (M == 0 && L != 0) // up samplind && fir
            {
                List<float> result = new List<float>();
                for (int i = 0; i < Ninput; i++)
                {
                    result.Add(InputSignal.Samples[i]);
                    for (int j = 0; j < L - 1; j++)
                    {
                        result.Add(0); //up
                    }
                }
                fir.InputTimeDomainSignal = new Signal(result, false);
                fir.Run();
                for(int i = 0; i < 1165; i++)
                {
                    OutputSignal.Samples.Add(fir.OutputYn.Samples[i]);
                }
                //OutputSignal = /*fir.OutputYn*/;
            }
            else if (L == 0 && M !=0) // fir && down sampling
            {
                fir.InputTimeDomainSignal = new Signal(InputSignal.Samples, false);
                fir.Run();
                Signal signal = fir.OutputYn;
                List<float> result = new List<float>();
                Signal ss = new Signal(result, false);
                int Nsignal = signal.Samples.Count;
                for (int i = 0; i < Nsignal ; i++)
                {
                    ss.Samples.Add(signal.Samples[i]);
                    i += (M - 2);
                }
                OutputSignal = new Signal(ss.Samples, false);
            }
            else
            {
                List<float> result = new List<float>();
                for (int i = 0; i < Ninput; i++)
                {
                    result.Add(InputSignal.Samples[i]);
                    for (int j = 0; j < L - 1; j++)
                        result.Add(0);
                }

                fir.InputTimeDomainSignal = new Signal(result, false);
                fir.Run();

                Signal signal = fir.OutputYn;
                List<float> answer = new List<float>();
                Signal ss = new Signal(answer, false);

                for (int i = 0; i < signal.Samples.Count; i+=M)
                {
                    ss.Samples.Add(signal.Samples[i]);
                    //i += (M - 1);
                }
                OutputSignal = new Signal(ss.Samples, false);
            }
            //throw new NotImplementedException(); 
        }
    }
}
