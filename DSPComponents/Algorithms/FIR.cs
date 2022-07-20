using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;
using System.IO;

namespace DSPAlgorithms.Algorithms
{
    public class FIR : Algorithm
    {
        public Signal InputTimeDomainSignal { get; set; }
        public FILTER_TYPES InputFilterType { get; set; }
        public float InputFS { get; set; }//sampling_frequency
        public float? InputCutOffFrequency { get; set; }//pass_band_edge_frequency if == null then not low and not high
        public float? InputF1 { get; set; }//case FILTER_TYPES.BAND_STOP or FILTER_TYPES.BAND_PASS
        public float? InputF2 { get; set; }//case FILTER_TYPES.BAND_STOP or FILTER_TYPES.BAND_PASS
        public float InputStopBandAttenuation { get; set; } // stop_band
        public float InputTransitionBand { get; set; }//transition_width
        public Signal OutputHn { get; set; }
        public Signal OutputYn { get; set; }

        public override void Run()
        {

            //get numerator of transition width of window by stopband
            double numerator = 0;
            if (InputStopBandAttenuation <= 0)
            {
                Console.WriteLine("index error");
            }
            else if (InputStopBandAttenuation <= 21)//rectangular
            {
                numerator = 0.9;
            }
            else if (InputStopBandAttenuation <= 44)//hanning
            {
                numerator = 3.1;
            }
            else if (InputStopBandAttenuation <= 53)//hamming
            {
                numerator = 3.3;
            }
            else if (InputStopBandAttenuation <= 74)//blackman
            {
                numerator = 5.5;
            }
            else
            {
                Console.WriteLine("index error");
            }
            //upper value with normalization 
            int transition_width = (int)Math.Round(numerator / (InputTransitionBand / InputFS));//numerator / deltaf
            if (transition_width % 2 == 0)//if even
            {
                transition_width++;//make it odd
            }

            //declaration outputsignal 
            List<float> Samples = new List<float>();
            List<float> SignalFreq = new List<float>();
            List<float> FrequenciesAmp = new List<float>();
            List<float> FrequenciesPhaseShifts = new List<float>();
            bool Periodic = false;
            OutputHn = new Signal(Samples, Periodic, SignalFreq, FrequenciesAmp, FrequenciesPhaseShifts);
            OutputYn = new Signal(Samples, Periodic, SignalFreq, FrequenciesAmp, FrequenciesPhaseShifts);

            //h(n) = hD(n) * W(n) 
            double hD = 0, w = 0;

            //intial by 0 not null 
            for (int i = 0; i < transition_width; i++)
            {
                OutputHn.Samples.Add(0);
                OutputHn.SamplesIndices.Add(0);
            }

            //get half sampling beacouse it symetric about origin
            int half_samples = transition_width / 2;

            //calculate fir
            if (InputCutOffFrequency == null) // BAND_PASS or BAND_STOP
            {
                double F1Dash = ((double)InputF1 - (InputTransitionBand / 2)) / InputFS;//F1dach = Fc - deltaF / 2 & normalizer
                double F2Dash = ((double)InputF2 + (InputTransitionBand / 2)) / InputFS;//F2dach = Fc + deltaF / 2 & normalizer

                if (InputFilterType == DSPAlgorithms.DataStructures.FILTER_TYPES.BAND_PASS) //if type = BAND_PASS
                {
                    for (int i = 0; i <= half_samples; i++)
                    {
                        //calculate ideal impulse respnose
                        if (i == 0) // n = 0
                        {
                            hD = 2 * (F2Dash - F1Dash); // hD(0) = 2(F2-F1)
                        }
                        else
                        {
                            // hD(n) = 2F2*(Sin(nW2)/nW2)-2F1*(Sin(nW1)/nW1)
                            hD = 2 * F2Dash * Math.Sin(i * 2 * Math.PI * F2Dash) / (i * 2 * Math.PI * F2Dash) - (2 * F1Dash * Math.Sin(i * 2 * Math.PI * F1Dash)) / (i * 2 * Math.PI * F1Dash);
                        }

                        //get BAND_PASS window function by stopband
                        if (InputStopBandAttenuation <= 0)
                        {
                            Console.WriteLine("index error 1");
                        }
                        else if (InputStopBandAttenuation <= 21)
                        {
                            w = 1;
                        }
                        else if (InputStopBandAttenuation <= 44)
                        {
                            w = 0.5 + 0.5 * (Math.Cos(2 * Math.PI * i / transition_width));
                        }
                        else if (InputStopBandAttenuation <= 53)
                        {
                            w = 0.54 + 0.46 * (Math.Cos(2 * Math.PI * i / transition_width));
                        }
                        else if (InputStopBandAttenuation <= 74)
                        {
                            w = 0.42 + 0.5 * (Math.Cos(2 * Math.PI * i / (transition_width - 1))) + 0.08 * (Math.Cos(4 * Math.PI * i / (transition_width - 1)));
                        }
                        else
                        {
                            Console.WriteLine("index error 1");
                        }
                        //put values and indices in +ve & -ve
                        OutputHn.Samples[half_samples - i] = (float)(hD * w);
                        OutputHn.Samples[half_samples + i] = (float)(hD * w);
                        OutputHn.SamplesIndices[half_samples - i] = -i;
                        OutputHn.SamplesIndices[half_samples + i] = i;
                    }
                }
                else // BAND_STOP
                {
                    for (int n = 0; n <= half_samples; n++)
                    {
                        //calculate ideal impulse respnose
                        if (n == 0)
                        {
                            hD = 1 - 2 * (F2Dash - F1Dash);
                        }
                        else
                        {
                            hD = 2 * F1Dash * Math.Sin(n * 2 * Math.PI * F1Dash)/ (n * 2 * Math.PI * F1Dash)- (2 * F2Dash * Math.Sin(n * 2 * Math.PI * F2Dash)) / (n * 2 * Math.PI * F2Dash);
                        }

                        //get BAND_STOP window function by stopband
                        if (InputStopBandAttenuation <= 0)
                        {
                            Console.WriteLine("index error 2");
                        }
                        else if (InputStopBandAttenuation <= 21)
                        {
                            w = 1;
                        }
                        else if (InputStopBandAttenuation <= 44)
                        {
                            w = 0.5 + 0.5 * (Math.Cos(2 * Math.PI * n / transition_width));
                        }
                        else if (InputStopBandAttenuation <= 53)
                        {
                            w = 0.54 + 0.46 * (Math.Cos(2 * Math.PI * n / transition_width));
                        }
                        else if (InputStopBandAttenuation <= 74)
                        {
                            w = 0.42 + 0.5 * (Math.Cos(2 * Math.PI * n / (transition_width - 1))) + 0.08 * (Math.Cos(4 * Math.PI * n / (transition_width - 1)));
                        }
                        else
                        {
                            Console.WriteLine("index error 2");
                        }

                        //put values and indices in +ve & -ve
                        OutputHn.Samples[half_samples - n] = (float)(hD * w);
                        OutputHn.Samples[half_samples + n] = (float)(hD * w);
                        OutputHn.SamplesIndices[half_samples - n] = -n;
                        OutputHn.SamplesIndices[half_samples + n] = n;
                    }
                }
            }
            else if (InputCutOffFrequency != null)// lowPass or HighPass
            {
                //F1dach = Fc - deltaF / 2 & normalizer
                double f_dash = ((double)InputCutOffFrequency + (InputTransitionBand / 2)) / InputFS;

                if (InputFilterType == DSPAlgorithms.DataStructures.FILTER_TYPES.LOW) //lowPass
                {
                    for (int n = 0; n <= half_samples; n++)
                    {
                        //calculate ideal impulse respnose
                        if (n == 0)
                        {
                            hD = 2 * f_dash;
                        }
                        else
                        {
                            hD = 2 * f_dash * Math.Sin(n * 2 * Math.PI * f_dash) / (n * 2 * Math.PI * f_dash);
                        }

                        //get lowPass window function by stopband
                        if (InputStopBandAttenuation <= 0)
                        {
                            Console.WriteLine("index error 3");
                        }
                        else if (InputStopBandAttenuation <= 21)
                        {
                            w = 1;
                        }
                        else if (InputStopBandAttenuation <= 44)
                        {
                            w = .5 + .5 * (Math.Cos(2 * Math.PI * n / transition_width));
                        }
                        else if (InputStopBandAttenuation <= 53)
                        {
                            w = .54 + .46 * (Math.Cos(2 * Math.PI * n / transition_width));
                        }
                        else if (InputStopBandAttenuation <= 74)
                        {
                            w = .42 + .5 * (Math.Cos(2 * Math.PI * n / (transition_width - 1))) + .08 * (Math.Cos(4 * Math.PI * n / (transition_width - 1)));
                        }
                        else
                        {
                            Console.WriteLine("index error 3");
                        }

                        //put values and indices in +ve & -ve
                        OutputHn.Samples[transition_width / 2 - n] = (float)(hD * w);
                        OutputHn.Samples[transition_width / 2 + n] = (float)(hD * w);
                        OutputHn.SamplesIndices[transition_width / 2 - n] = -n;
                        OutputHn.SamplesIndices[transition_width / 2 + n] = n;
                    }
                }
                else // HighPass
                {
                    for (int n = 0; n <= half_samples; n++)
                    {
                        //calculate ideal impulse respnose
                        if (n == 0)
                        {
                            hD = 1 - (2 * f_dash);
                        }
                        else
                        {
                            hD = -1 * 2 * f_dash * Math.Sin(n * 2 * Math.PI * f_dash) / (n * 2 * Math.PI * f_dash);
                        }

                        //get highPass window function by stopband
                        if (InputStopBandAttenuation <= 0)
                        {
                            Console.WriteLine("index error 4");
                        }
                        else if (InputStopBandAttenuation <= 21)
                        {
                            w = 1;
                        }
                        else if (InputStopBandAttenuation <= 44)
                        {
                            w = 0.5 + 0.5 * (Math.Cos(2 * Math.PI * n / transition_width));
                        }
                        else if (InputStopBandAttenuation <= 53)
                        {
                            w = .54 + .46 * (Math.Cos(2 * Math.PI * n / transition_width));
                        }
                        else if (InputStopBandAttenuation <= 74)
                        {
                            w = .42 + .5 * (Math.Cos(2 * Math.PI * n / (transition_width - 1))) + .08 * (Math.Cos(4 * Math.PI * n / (transition_width - 1)));
                        }
                        else
                        {
                            Console.WriteLine("index error 4");
                        }
                        //put values and indices in +ve & -ve
                        OutputHn.Samples[transition_width / 2 - n] = (float)(hD * w);
                        OutputHn.Samples[transition_width / 2 + n] = (float)(hD * w);
                        OutputHn.SamplesIndices[transition_width / 2 - n] = -n;
                        OutputHn.SamplesIndices[transition_width / 2 + n] = n;
                    }
                }
            }

            StreamWriter fir = new StreamWriter("H://fir.txt");
            fir.WriteLine("index   value");

            for (int i = -half_samples; i < 0; i++)
            {
                fir.WriteLine(OutputHn.SamplesIndices[transition_width / 2 + i] + "   " + OutputHn.Samples[transition_width / 2 + i]);
            }
            for (int i = 0; i <= half_samples; i++)
            {
                fir.WriteLine(OutputHn.SamplesIndices[transition_width / 2 + i] + "   " + OutputHn.Samples[transition_width / 2 + i]);
            }
            fir.Close();

            //passing convolution 
            DirectConvolution convolution = new DirectConvolution();
            convolution.InputSignal1 = InputTimeDomainSignal;
            convolution.InputSignal2 = OutputHn;
            convolution.Run();
            OutputYn = convolution.OutputConvolvedSignal;
            OutputYn = convolution.Get_convolo();

        }
    }
}