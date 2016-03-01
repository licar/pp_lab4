using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    class CDoctor
    {
        List<string> _patients;

        public CDoctor()
        {
            _patients = new List<string>();
        }

        public void AddPatient(string patient)
        {
            lock (_patients)
            {
                _patients.Add(patient);
            }
        }

        public void AcceptPatient()
        {
            while (true)
            {
                lock (_patients)
                {
                    if (_patients.Count != 0)
                    {
                        System.Threading.Thread.Sleep(2000);
                        string patient = "";
                        patient = _patients.First();
                        _patients.RemoveAt(0);
                        Console.WriteLine("Patient accepted on:" + patient);
                    }
                }
            }
        }
    }
}
