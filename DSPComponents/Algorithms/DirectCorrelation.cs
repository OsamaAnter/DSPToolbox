//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using DSPAlgorithms.DataStructures;

//namespace DSPAlgorithms.Algorithms
//{
//    public class DirectCorrelation : Algorithm
//    {
//        public Signal InputSignal1 { get; set; }
//        public Signal InputSignal2 { get; set; }
//        public List<float> OutputNonNormalizedCorrelation { get; set; }
//        public List<float> OutputNormalizedCorrelation { get; set; }

//        public override void Run()
//        {
//            #region initializations
//            OutputNonNormalizedCorrelation = new List<float>();
//            OutputNormalizedCorrelation = new List<float>();
//            #endregion

//            #region auto-correlation
//            if (InputSignal2 == null) //auto-correlation
//            {
//                #region initializations
//                List<float> auto_correlation = new List<float>();
//                List<double> signal1_samples = new List<double>();
//                List<double> signal1_samples_copy = new List<double>();

//                for (int i = 0; i < InputSignal1.Samples.Count; i++)
//                {
//                    signal1_samples.Add(InputSignal1.Samples[i]);
//                    signal1_samples_copy.Add(InputSignal1.Samples[i]);
//                }
//                #endregion

//                #region normalization summation
//                double normalization_summation = 0, signal_samples_summation = 0, signal_samples_copy_summation = 0;
//                for (int i = 0; i < signal1_samples.Count; i++)
//                {
//                    signal_samples_summation += signal1_samples[i] * signal1_samples[i];
//                    signal_samples_copy_summation += signal1_samples_copy[i] * signal1_samples_copy[i];
//                }
//                normalization_summation = Math.Sqrt(signal_samples_summation * signal_samples_copy_summation) / signal1_samples.Count;
//                #endregion

//                #region non-periodic
//                if (InputSignal1.Periodic != true)
//                {
//                    for (int i = 0; i < signal1_samples_copy.Count; i++)
//                    {
//                        double sum = 0;
//                        if (i != 0) // so shift 
//                        {
//                            double first_element = 0;
//                            for (int j = 0; j < signal1_samples_copy.Count - 1; j++)
//                            {
//                                signal1_samples_copy[j] = signal1_samples_copy[j + 1];
//                                sum += signal1_samples_copy[j] * signal1_samples[j];
//                            }
//                            signal1_samples_copy[signal1_samples_copy.Count - 1] = first_element;
//                            sum += signal1_samples_copy[signal1_samples_copy.Count - 1] * signal1_samples[signal1_samples.Count - 1];
//                        }
//                        else
//                        {
//                            for (int j = 0; j < signal1_samples_copy.Count; j++)
//                                sum += signal1_samples_copy[j] * signal1_samples_copy[j];
//                        }
//                        auto_correlation.Add((float)sum / signal1_samples_copy.Count);
//                    }
//                }
//                #endregion

//                #region periodic
//                else
//                {
//                    for (int i = 0; i < signal1_samples_copy.Count; i++)
//                    {
//                        double sum = 0;
//                        if (i != 0) // so shift 
//                        {
//                            double first_element = signal1_samples_copy[0];
//                            for (int j = 0; j < signal1_samples_copy.Count - 1; j++)
//                            {
//                                signal1_samples_copy[j] = signal1_samples_copy[j + 1];
//                                sum += signal1_samples_copy[j] * signal1_samples[j];
//                            }
//                            signal1_samples_copy[signal1_samples_copy.Count - 1] = first_element;
//                            sum += signal1_samples_copy[signal1_samples_copy.Count - 1] * signal1_samples[signal1_samples.Count - 1];
//                        }
//                        else
//                        {
//                            for (int j = 0; j < signal1_samples_copy.Count; j++)
//                                sum += signal1_samples_copy[j] * signal1_samples_copy[j];
//                        }
//                        auto_correlation.Add((float)sum / signal1_samples_copy.Count);
//                    }

//                }
//                #endregion

//                #region output
//                OutputNonNormalizedCorrelation = auto_correlation;

//                for (int i = 0; i < OutputNonNormalizedCorrelation.Count; i++)
//                    OutputNormalizedCorrelation.Add((float)(OutputNonNormalizedCorrelation[i] / normalization_summation));
//                #endregion
//            }
//            #endregion*/

//            #region cross-correlation
//            else // cross-correlation
//            {
//                #region initializations
//                List<float> cross_correlation = new List<float>();
//                List<double> signal1_samples = new List<double>();
//                List<double> signal2_samples = new List<double>();
//                for (int i = 0; i < InputSignal1.Samples.Count; i++)
//                    signal1_samples.Add(InputSignal1.Samples[i]);
//                for (int i = 0; i < InputSignal2.Samples.Count; i++)
//                    signal2_samples.Add(InputSignal2.Samples[i]);
//                if (signal1_samples != signal2_samples)
//                {
//                    int cross_length = signal1_samples.Count + signal2_samples.Count - 1;
//                    for (int i = signal1_samples.Count; i < cross_length; i++)
//                        signal1_samples.Add(0);
//                    for (int i = signal2_samples.Count; i < cross_length; i++)
//                        signal2_samples.Add(0);
//                }
//                #endregion

//                #region normalization summation
//                double normalization_summation = 0, signal1_samples_summation = 0, signal2_samples_summation = 0;
//                for (int i = 0; i < signal1_samples.Count; i++)
//                {
//                    signal1_samples_summation += signal1_samples[i] * signal1_samples[i];
//                    signal2_samples_summation += signal2_samples[i] * signal2_samples[i];
//                }
//                normalization_summation = Math.Sqrt(signal1_samples_summation * signal2_samples_summation) / signal1_samples.Count;
//                #endregion

//                #region non-periodic
//                if (InputSignal1.Periodic != true)
//                {
//                    for (int i = 0; i < signal2_samples.Count; i++)
//                    {
//                        double sum = 0;
//                        if (i != 0) // so shift 
//                        {
//                            double first_element = 0;
//                            for (int j = 0; j < signal2_samples.Count - 1; j++)
//                            {
//                                signal2_samples[j] = signal2_samples[j + 1];
//                                sum += signal2_samples[j] * signal1_samples[j];
//                            }
//                            signal2_samples[signal2_samples.Count - 1] = first_element;
//                            sum += signal2_samples[signal2_samples.Count - 1] * signal1_samples[signal1_samples.Count - 1];
//                        }
//                        else
//                        {
//                            for (int j = 0; j < signal2_samples.Count; j++)
//                                sum += signal1_samples[j] * signal2_samples[j];
//                        }
//                        cross_correlation.Add((float)sum / signal2_samples.Count);
//                    }
//                }
//                #endregion

//                #region periodic
//                else
//                {
//                    for (int i = 0; i < signal2_samples.Count; i++)
//                    {
//                        double sum = 0;
//                        if (i != 0) // so shift 
//                        {
//                            double first_element = signal2_samples[0];
//                            for (int j = 0; j < signal2_samples.Count - 1; j++)
//                            {
//                                signal2_samples[j] = signal2_samples[j + 1];
//                                sum += signal2_samples[j] * signal1_samples[j];
//                            }
//                            signal2_samples[signal2_samples.Count - 1] = first_element;
//                            sum += signal2_samples[signal2_samples.Count - 1] * signal1_samples[signal1_samples.Count - 1];
//                        }
//                        else
//                        {
//                            for (int j = 0; j < signal2_samples.Count; j++)
//                                sum += signal1_samples[j] * signal2_samples[j];
//                        }
//                        cross_correlation.Add((float)sum / signal2_samples.Count);
//                    }
//                }
//                #endregion

//                #region output
//                OutputNonNormalizedCorrelation = cross_correlation;

//                for (int i = 0; i < OutputNonNormalizedCorrelation.Count; i++)
//                    OutputNormalizedCorrelation.Add((float)(OutputNonNormalizedCorrelation[i] / normalization_summation));
//                #endregion
//            }
//            #endregion

//            //throw new NotImplementedException();
//        }
//    }
//}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class DirectCorrelation : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public List<float> OutputNonNormalizedCorrelation { get; set; }
        public List<float> OutputNormalizedCorrelation { get; set; }

        public override void Run()
        {

            OutputNonNormalizedCorrelation = new List<float>();
            OutputNormalizedCorrelation = new List<float>();
            List<float> copy1 = new List<float>();
            List<float> copy2 = new List<float>();
            int N = InputSignal1.Samples.Count;

            for (int i = 0; i < N; i++)
                copy1.Add(InputSignal1.Samples[i]);
            int ishift = 0;
            //auto-correlation
            if (InputSignal2 == null)
            {
                double normal1 = 0, one = 0, two = 0;
                for (int i = 0; i < N; i++)
                {
                    // signal_samples_summation += signal1_samples[i] * signal1_samples[i];
                    // signal_samples_copy_summation += signal1_samples_copy[i] * signal1_samples_copy[i];
                    one += Math.Pow(copy1[i], 2);
                    two += Math.Pow(copy1[i], 2);

                }
                normal1 = Math.Sqrt(one * two) / N;

                if (InputSignal1.Periodic == true)
                {
                    for (int i = 0; i < N; i++)
                    {
                        double s = 0;
                        ishift = i;
                        for (int j = 0; j < N; j++)
                        {
                            s += copy1[ishift++] * copy1[j];
                            ishift %= N;
                        }
                        float tmp = (float)s / N;
                        OutputNonNormalizedCorrelation.Add(tmp);
                        OutputNormalizedCorrelation.Add((float)(tmp / normal1));
                    }
                }
                else
                {
                    for (int i = 0; i < N; i++)
                    {
                        double s = 0;
                        if (i != 0)
                        {
                            for (int j = 0; j < N - 1; j++)
                            {
                                copy1[j] = copy1[j + 1];
                                s += copy1[j] * InputSignal1.Samples[j];
                            }
                            copy1[N - 1] = 0;
                        }
                        else
                        {
                            for (int j = 0; j < N; j++)
                            {
                                s += InputSignal1.Samples[j] * InputSignal1.Samples[j];
                            }
                        }
                        float tmp = (float)s / N;
                        OutputNonNormalizedCorrelation.Add(tmp);
                        OutputNormalizedCorrelation.Add((float)(tmp / normal1));
                    }
                }
            }
            else// cross-correlation
            {
                int NN = InputSignal2.Samples.Count;
                List<float> copy11 = new List<float>();
                List<float> copy22 = new List<float>();
                for (int i = 0; i < N; i++)
                    copy11.Add(InputSignal1.Samples[i]);
                for (int i = 0; i < NN; i++)
                    copy22.Add(InputSignal2.Samples[i]);
                int T = NN + N - 1;
                while (copy11.Count < T)
                {
                    copy11.Add(0);
                }
                while (copy22.Count < T)
                {
                    copy22.Add(0);
                }
                double normal2 = 0, one2 = 0, two2 = 0;
                for (int i = 0; i < T; i++)
                {
                    one2 += Math.Pow(copy11[i], 2);
                    two2 += Math.Pow(copy22[i], 2);

                }
                normal2 = (one2 * two2);
                normal2 = Math.Sqrt(normal2);
                normal2 /= T;
                if (InputSignal1.Periodic == true)
                {
                    for (int i = 0; i < T; i++)
                    {
                        double s = 0;
                        ishift = i;
                        for (int j = 0; j < T; j++)
                        {
                            s += copy22[ishift++] * copy11[j];
                            ishift %= (T);
                        }
                        float tmp = (float)s / T;
                        OutputNonNormalizedCorrelation.Add(tmp);
                        OutputNormalizedCorrelation.Add((float)(tmp / normal2));
                    }
                }
                else
                {
                    for (int i = 0; i < T; i++)
                    {
                        double s = 0;
                        if (i != 0)
                        {
                            for (int j = 0; j < T - 1; j++)
                            {
                                copy22[j] = copy22[j + 1];
                                s += copy22[j] * copy11[j];
                            }
                            copy22[T - 1] = 0;
                        }
                        else
                        {
                            for (int j = 0; j < T; j++)
                            {
                                s += copy22[j] * copy11[j];
                            }
                        }
                        float tmp = (float)s / T;
                        OutputNonNormalizedCorrelation.Add(tmp);
                        OutputNormalizedCorrelation.Add((float)(tmp / normal2));
                    }
                }

            }
        }
    }
}
