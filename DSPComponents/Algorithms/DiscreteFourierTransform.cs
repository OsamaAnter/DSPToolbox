using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;
using System.IO;
namespace DSPAlgorithms.Algorithms
{

    public class DiscreteFourierTransform : Algorithm
    {
        public Signal InputTimeDomainSignal { get; set; }
        public float InputSamplingFrequency { get; set; }
        public Signal OutputFreqDomainSignal { get; set; }


        public override void Run()
        {
            // samples count
            int N = InputTimeDomainSignal.Samples.Count;

            double sumtermCom = 0, sumtermReal = 0;

            List<float> amplitude = new List<float>();
            List<float> phase = new List<float>();
            List<float> frequency = new List<float>();

            //first frequancy domain term
            double sigma = (2 * Math.PI * InputSamplingFrequency) / N;

            //intialate file
            StreamWriter File = new StreamWriter("H://New Text Document.txt");
            File.WriteLine("amplitude  phase");

            for (int k = 0; k < N; k++) // k  const. term in samples
            {
                sumtermCom = 0;
                sumtermReal = 0;
                for (int n = 0; n < N; n++) //n  samples
                {
                    if (InputTimeDomainSignal.Samples[n] == 0)
                    {
                        continue;
                    }
                    else
                    {
                        double ePower = -(n * 2 * k * Math.PI) / N;
                        sumtermReal += (Math.Cos(ePower) * InputTimeDomainSignal.Samples[n]);
                        sumtermCom += (Math.Sin(ePower) * InputTimeDomainSignal.Samples[n]);
                    }
                }

                //amplitude 
                amplitude.Add((float)Math.Sqrt((Math.Pow(sumtermReal, 2) + Math.Pow(sumtermCom, 2))));

                //phase 
                phase.Add((float)Math.Atan2(sumtermCom, sumtermReal));

                //Add to file ( amplitude   phase )
                File.WriteLine((float)Math.Sqrt((Math.Pow(sumtermReal, 2) + Math.Pow(sumtermCom, 2))) + "  " + (float)Math.Atan2(sumtermCom, sumtermReal));

                //frequancy domain
                frequency.Add((float)(sigma * (k + 1)));
            }
            //close file
            File.Close();

            //return output
            OutputFreqDomainSignal = new Signal(false, frequency, amplitude, phase);
        }
    }
}
