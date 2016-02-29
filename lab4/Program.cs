using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using lab4;

namespace lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            CHospital hospital = new CHospital();
        }
    }

    class CHospital
    {
        private List<string> firstTherapistPatientsQueue;
        private List<string> secondTherapistPatientsQueue;
        private List<string> dentistQueue;
        private List<string> surgeonQueue;
        private List<string> therapistQueue;

        private const int COUNT_DOCTORS = 3;

        private Thread firstTherapistThread;
        private Thread secondTherapistThread;
        private Thread dentistThread;
        private Thread therapistThread;
        private Thread surgeonThread;

        private const string SURGEON = "surgeon";
        private const string THERAPIST = "therapist";
        private const string DENTIST = "dentist";

        private int number = 0;

        public CHospital()
        {
            firstTherapistPatientsQueue = new List<string>();
            secondTherapistPatientsQueue = new List<string>();
            dentistQueue = new List<string>();
            surgeonQueue = new List<string>();
            therapistQueue = new List<string>(); 

            DefineTherapist();

        }
        private void DefineTherapist()
        {
            firstTherapistThread = new Thread(SendToTreatingDoctorFromFirstTherapist);
            secondTherapistThread = new Thread(SendToTreatingDoctorFromSecondTherapist);

            dentistThread = new Thread(ReceptionAtDentist);
            therapistThread = new Thread(ReceptionAtTherapist);
            surgeonThread = new Thread(ReceptionAtSurgeon);

            firstTherapistThread.Start();
            secondTherapistThread.Start();
            dentistThread.Start();
            therapistThread.Start();
            surgeonThread.Start();

            while (true)
            {
                string patient = GetRequireDoctor();
                if (firstTherapistPatientsQueue.Count <= secondTherapistPatientsQueue.Count)
                {
                    firstTherapistPatientsQueue.Add(patient);
                }
                else
                {
                    secondTherapistPatientsQueue.Add(patient);
                }
                System.Threading.Thread.Sleep(50);
            }
        }

        private int GetNextNumber()
        {
            if (number == 2)
            {
                number = 0;
            }
            else
            {
                ++number;
            }
            return number;
        }

        private string GetRequireDoctor()
        {
            //Random random = new Random();
            //int requireDoctorNoo = random.Next(COUNT_DOCTORS);
            int requireDoctorNoo = GetNextNumber();
            string requireDoctor = "";

            switch (requireDoctorNoo)
            {
                case 0:
                    requireDoctor = DENTIST;
                    break;
                case 1:
                    requireDoctor = THERAPIST;
                    break;
                case 2:
                    requireDoctor = SURGEON;
                    break;
            }

            return requireDoctor;
        }

        private void SendToTreatingDoctorFromFirstTherapist()
        {
            if (firstTherapistPatientsQueue.Count != 0)
            {
                System.Threading.Thread.Sleep(100);
                string patient = firstTherapistPatientsQueue.First();
                firstTherapistPatientsQueue.RemoveAt(0);
                AddToDoctorQueue(ref patient);
            }
        }

        private void SendToTreatingDoctorFromSecondTherapist()
        {
            while (true)
            {
                if (secondTherapistPatientsQueue.Count != 0)
                {
                    System.Threading.Thread.Sleep(100);
                    string patient = secondTherapistPatientsQueue.First();
                    secondTherapistPatientsQueue.RemoveAt(0);
                    AddToDoctorQueue(ref patient);
                }
            }
        }

        private void AddToDoctorQueue(ref string patient)
        {
            lock (this)
            {
                switch (patient)
                {
                    case DENTIST:
                        dentistQueue.Add(patient);
                        break;
                    case THERAPIST:
                        therapistQueue.Add(patient);
                        break;
                    case SURGEON:
                        surgeonQueue.Add(patient);
                        break;
                }
            }
        }

        private void ReceptionAtDentist()
        {
            while (true)
            {
                if (dentistQueue.Count != 0)
                {
                    System.Threading.Thread.Sleep(100);
                    string dentist = dentistQueue.First();
                    dentistQueue.RemoveAt(0);
                    Console.Write(dentist + '\n');
                }
            }
        }

        private void ReceptionAtTherapist()
        {
            while (true)
            {
                if (therapistQueue.Count != 0)
                {
                    System.Threading.Thread.Sleep(100);
                    string therapist = therapistQueue.First();
                    therapistQueue.RemoveAt(0);
                    Console.Write(therapist + '\n');
                }
            }
        }

        private void ReceptionAtSurgeon()
        {
            while (true)
            {
                if (surgeonQueue.Count != 0)
                {
                    System.Threading.Thread.Sleep(100);
                    string surgeon = surgeonQueue.First();
                    surgeonQueue.RemoveAt(0);
                    Console.Write(surgeon + '\n');
                }
            }
        }
    }
}
