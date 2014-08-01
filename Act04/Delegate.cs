using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Act04
{
    class Delegate
    {
        public int DelID;
        public String DName;
        public String Subj;
        public double Due;   
        public bool Presenter;
   
        public Delegate(int D, String N, String Sub, double Cost, bool P)
        {
   	        DelID = D;
   	        DName = N;
   	        Subj = Sub;
   	        Due = Cost;
   	        Presenter = P;
        }

        public bool Equals(Delegate obj)
        {
            return this.DelID == obj.DelID;
        }

        public void processPayment(double Amnt)
        {
            Due -= Amnt;
        }

        public void display()
        {
            Console.Write("DelID: {0} Name: {1} Due: {2}", DelID, DName, Due);
            if (Presenter)
                Console.WriteLine(" is presenting");
            else
                Console.WriteLine();
        }

        // ADD ANY ADDITIONAL CODE FOR CLASS Delegate BELOW

        public Delegate clone()
        {
            return new Delegate(this.DelID, this.DName, this.Subj, this.Due, this.Presenter);
        }
		public double getDue()
		{
			return Due;
		}
       
    }
}
