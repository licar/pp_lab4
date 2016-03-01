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
        private const int COUNT_DOCTORS = 3;

        private Thread firstTherapistThread;
        private Thread secondTherapistThread;
        private Thread dentistThread;
        private Thread therapistThread;
        private Thread surgeonThread;

        private int number = 0;

        public CHospital()
        {

            DefineTherapist();

        }
        private void DefineTherapist()
        {

            CDoctor therapist = new CDoctor();
            CDoctor surgeon = new CDoctor();
            CDoctor dentist = new CDoctor();

            CTherapist firstTherapist = new CTherapist(therapist, surgeon, dentist);
            CTherapist secondTherapist = new CTherapist(therapist, surgeon, dentist);

            firstTherapistThread = new Thread(firstTherapist.SendToTreatingDoctor);
            secondTherapistThread = new Thread(secondTherapist.SendToTreatingDoctor);

            dentistThread = new Thread(dentist.AcceptPatient);
            therapistThread = new Thread(surgeon.AcceptPatient);
            surgeonThread = new Thread(dentist.AcceptPatient);

            firstTherapistThread.Start();
            secondTherapistThread.Start();

            dentistThread.Start();
            therapistThread.Start();
            surgeonThread.Start();

            while (true)
            {
                string patient = GetRequireDoctor();
                Console.WriteLine("Came patient to doctor:" + patient);
                if (firstTherapist.GetPatientCount() <= secondTherapist.GetPatientCount())
                {
                    firstTherapist.AddPatient(patient);
                }
                else
                {
                    secondTherapist.AddPatient(patient);
                }
                System.Threading.Thread.Sleep(1000);
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
                    requireDoctor = CTherapist.DENTIST;
                    break;
                case 1:
                    requireDoctor = CTherapist.THERAPIST;
                    break;
                case 2:
                    requireDoctor = CTherapist.SURGEON;
                    break;
            }

            return requireDoctor;
        }
    }
}
   