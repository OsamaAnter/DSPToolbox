using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class DirectConvolution : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public Signal OutputConvolvedSignal { get; set; }

        /// <summary>
        /// Convolved InputSignal1 (considered as X) with InputSignal2 (considered as H)
        /// </summary>
        public override void Run()
        {
            List<float> conv = new List<float>();
            List<int> index = new List<int>();
            //throw new NotImplementedException();
            int len1 = InputSignal1.Samples.Count;
            int len2 = InputSignal2.Samples.Count;
            int n = len1 + len2 - 1;
            for (int i = 0; i < len1; i++)
            {
                for (int j = 0; j < len2; j++)
                {
                    int val = InputSignal1.SamplesIndices[i] + InputSignal2.SamplesIndices[j];
                    if (!index.Contains(val))
                    {
                        index.Add(val);
                    }
                }
            }
            int maximum = Math.Max(len1, len2);
            for (int i = 0; i < n; i++)
            {
                float a = 0;
                float b = 0;
                float v = 0;
                for (int j = 0; j <= i; j++)
                {
                    if (!(i - j >= len1))
                    {
                        a = InputSignal1.Samples[i - j];
                    }
                    else
                    {
                        a = 0;
                    }

                    if (!(j >= len2))
                    {
                        b = InputSignal2.Samples[j];
                    }
                    else
                    {
                        b = 0;
                    }


                    v += a * b;


                }
                conv.Add(v);

            }
            OutputConvolvedSignal = new Signal(conv, index, false);
        }

        //throw new NotImplementedException();

        public Signal Get_convolo()
        {
            //float v = 0;
            List<float> conv = new List<float>();
            List<int> index = new List<int>();
            //throw new NotImplementedException();
            int len1 = InputSignal1.Samples.Count;
            int len2 = InputSignal2.Samples.Count;
            int n = len1 + len2 - 1;
            for (int i = 0; i < len1; i++)
            {
                for (int j = 0; j < len2; j++)
                {
                    int val = InputSignal1.SamplesIndices[i] + InputSignal2.SamplesIndices[j];
                    if (!index.Contains(val))
                    {
                        index.Add(val);
                    }
                }
            }
            int maximum = Math.Max(len1, len2);
            for (int i = 0; i < n; i++)
            {
                float a = 0;
                float b = 0;
                float v = 0;
                for (int j = 0; j <= i; j++)
                {
                    if (!(i - j >= len1))
                    {
                        a = InputSignal1.Samples[i - j];
                    }
                    else
                    {
                        a = 0;
                    }

                    if (!(j >= len2))
                    {
                        b = InputSignal2.Samples[j];
                    }
                    else
                    {
                        b = 0;
                    }


                    v += a * b;


                }
                conv.Add(v);

            }
            return OutputConvolvedSignal = new Signal(conv, index, false);
        }

    }
}

