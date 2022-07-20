using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class SinCos: Algorithm
    {
        public string type { get; set; }
        public float A { get; set; }
        public float PhaseShift { get; set; }
        public float AnalogFrequency { get; set; }
        public float SamplingFrequency { get; set; }
        public List<float> samples { get; set; }
        public override void Run()
        {
            samples = new List<float>();
            if (type == "sin"|| type == "Sin" || type == "SIN")
            {
                for (int i = 0; i < (int)SamplingFrequency; i++)
                {
                    samples.Add((float)(A * Math.Sin((2 * Math.PI * (AnalogFrequency / SamplingFrequency) * i) + PhaseShift)));
                }
            }
            else if (type == "cos"|| type == "Cos"|| type == "COS")
            {
                for (int i = 0; i < (int)SamplingFrequency; i++)
                {
                    samples.Add((float)(A * Math.Cos((2 * Math.PI * (AnalogFrequency / SamplingFrequency) * i) + PhaseShift)));
                }
            }
            else
            {
                Console.WriteLine("index error");
            }
            // throw new NotImplementedException();
        }
    }
}
