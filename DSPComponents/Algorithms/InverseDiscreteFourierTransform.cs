using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;
using System.Numerics;

namespace DSPAlgorithms.Algorithms
{
    public class InverseDiscreteFourierTransform : Algorithm
    {
        public Signal InputFreqDomainSignal { get; set; }
        public Signal OutputTimeDomainSignal { get; set; }

        public override void Run()
        {
            List<Complex> comp = new List<Complex>();
            List<Complex> result = new List<Complex>();
            
            //sample size number
            int N = InputFreqDomainSignal.FrequenciesAmplitudes.Count;

            //add amp & phase
            for (int n = 0; n < N; n++) 
            {
                comp.Add(Complex.FromPolarCoordinates(InputFreqDomainSignal.FrequenciesAmplitudes[n], InputFreqDomainSignal.FrequenciesPhaseShifts[n]));
            }

            for (int n = 0; n < N; n++)//n
            {
                result.Add(0);
                for (int k = 0; k < N; k++)//k
                {
                    double epower = (2 * k * Math.PI * n) / N;
                    result[n] += comp[k] * (Math.Cos(epower) + Complex.ImaginaryOne * Math.Sin(epower));
                }
                result[n] /= N;
            }
            for(int i = 0; i < result.Count; i++)
            {
                Console.WriteLine("real " + result[i].Imaginary);
            }
            List<float> placeHolder = new List<float>();

            for (int i = 0; i < N; i++)
            {
                placeHolder.Add((float)result[i].Real);
            }
            OutputTimeDomainSignal = new Signal(placeHolder, false);
            //List<s> lst = new List<s>();
            //for (int i = 0; i < InputFreqDomainSignal.FrequenciesAmplitudes.Count; i++)
            //{
            //    s c = new s();
            //    float aa = InputFreqDomainSignal.FrequenciesAmplitudes[i];
            //    float bb = InputFreqDomainSignal.FrequenciesPhaseShifts[i];
            //    c.x = (aa * (float)Math.Cos(bb));
            //    c.y = (float)Math.Ceiling((aa * (float)Math.Sin(bb)));
            //    lst.Add(c);
            //}
            //List<float> ans = new List<float>();
            //for (int k = 0; k < InputFreqDomainSignal.Frequencies.Count; k++)
            //{
            //    float x = 0;
            //    for (int n = 0; n < InputFreqDomainSignal.Frequencies.Count; n++)
            //    {
            //        float xx = (2 * (float)Math.PI * k * n) / InputFreqDomainSignal.Frequencies.Count;
            //        float real = (float)Math.Cos(xx);
            //        float imagine = (float)Math.Sin(xx);
            //        if (imagine != 0)
            //        {
            //            x -= (lst[n].y * imagine);
            //        }
            //        if (real != 0)
            //        {
            //            x += (lst[n].x * real);
            //        }
            //        if (real == 0 && 0 == imagine)
            //            x += lst[n].x;
            //    }
            //    x /= InputFreqDomainSignal.Frequencies.Count;
            //    ans.Add((float)Math.Round(x));

            //}
            //for (int i = 0; i < ans.Count; i++)
            //{
            //    Console.Write(ans[i]);
            //    Console.Write(" ");
            //}
            //OutputTimeDomainSignal = new Signal(ans, true);
            //----------------------------------------------------------------------------------
            //int N = InputFreqDomainSignal.Samples.Count;
            //double sumtermCom = 0, sumtermReal = 0;

            //for (int n = 0; n < N; n++) // n  samples
            //{
            //    sumtermCom = 0;
            //    sumtermReal = 0;
            //    for (int k = 0; k < N; k++)
            //    {
            //        double ePower = (n * 2 * k * Math.PI) / N;
            //        sumtermReal += (Math.Cos(ePower) * InputFreqDomainSignal.Samples[n]) / N;
            //        sumtermCom += (Math.Sin(ePower) * InputFreqDomainSignal.Samples[n]) / N;
            //    }


        }

            //OutputTimeDomainSignal = new Signal();

            //throw new NotImplementedException();
        }
    
}
