using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    class CTherapist
    {
        private List<string> _patients;

        public const string SURGEON = "surgeon";
        public const string THERAPIST = "therapist";
        public const string DENTIST = "dentist";

        private CDoctor _therapist;
        private CDoctor _surgeon;
        private CDoctor _dentist;

        public CTherapist(CDoctor therapist,
            CDoctor surgeon,
            CDoctor dentist)
        {
            _therapist = therapist;
            _dentist = dentist;
            _surgeon = surgeon;

            _patients = new List<string>();
        }

        public int GetPatientCount()
        {
            return _patients.Count;
        }

        public void AddPatient(string patient)
        {
            lock (_patients)
            {
                _patients.Add(patient);
            }
        }

        public void SendToTreatingDoctor()
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
                        AddToDoctorQueue(patient);
                    }
                }
            }
        }

        private void AddToDoctorQueue(string patient)
        {
            switch (patient)
            {
                case DENTIST:
                    _dentist.AddPatient(patient);
                    break;
                case THERAPIST:
                    _therapist.AddPatient(patient);
                    break;
                case SURGEON:
                    _surgeon.AddPatient(patient);
                    break;
            }
            Console.WriteLine("Patient sent to:" + patient);
        }
    }
}
