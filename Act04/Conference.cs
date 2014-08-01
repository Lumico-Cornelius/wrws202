using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Act04
{
    class Conference
    {
        public String CName;
        public String City;
        public int MaxSlots;  // maximum number of presentation slots 
        public double Cost;
        public Stack Delegates;

        public Conference(String C, String Loc, int M, double Cst)
        //post: constructor
        {
            CName = C;
            City = Loc;
            MaxSlots = M;
            Cost = Cst;
            Delegates = new Stack();
        }

        private int availSlots()
        /* pre:  Have list of delegates which could be empty.  Not all delegates in the list are necessarily presenting.
         * post: Compute and return a count of the number of presentation slots available. 
         */

        {

            int totSlots = 0;
            Stack temp = new Stack();
            Delegate cur;
            while (Delegates.Count > 0)
            {
                cur = (Delegate)Delegates.Pop();
                temp.Push(cur);
                if (cur.Presenter)
                    totSlots++;
            }
            while (temp.Count > 0)
                Delegates.Push(temp.Pop());
            return (MaxSlots - totSlots);
        }

        public void registerDelegate(Delegate newOne)
        /* pre:  Have a list of delegates which could be empty. For the new delegate, have delegate identifier, name, topic 
         *       and whether presenting (true) or not (false).
         * post: Register a delegate by adding him/her to the top of the delegates stack. A delegate may only be a presenter
         *       if there is a presentation slot available (use availSlots), otherwise register the delegate and display an
         *       appropriate message. NO DUPLICATE DELEGATES ARE REGISTERED (on delegate identifier) - display an appropriate 
         *       message in such a case. 
         */
        {

            Stack temp = new Stack();
            Delegate cur;
            bool foundPos = false;
            while ((Delegates.Count > 0) && (!foundPos))
            {
                cur = (Delegate)Delegates.Pop();
                temp.Push(cur);
                if (cur.Equals(newOne))
                    foundPos = true;
            }
            while (temp.Count > 0)
                Delegates.Push(temp.Pop());
            if (foundPos)
                Console.WriteLine("Delegate " + newOne.DelID + " already registered.");
            else
            {
                if (newOne.Presenter)
                    if (availSlots() < 1)
                    {
                        Console.WriteLine("Delegate {0} registered but cannot present - not enough slots", newOne.DelID);
                        newOne.Presenter = false;
                    }
                Delegates.Push(newOne);
            }
        }

        // COMPULSORY TASKS - must be completed before Thursday's session

        public void displayDelegates()
        /* pre:  Have list of delegates, possibly empty.
         * post: RECURSIVELY display details for delegates on the list.  List must retain original ordering of delegates.
         */
        {
			if (Delegates.Count == 0) 
			{
				Console.WriteLine ("Empty List");
				return;
			}
			Stack Copy = duplicateList ();
			displayD (Copy);

        }

		public void displayD(Stack disp)
		{
			if (disp.Count  == 0) 
			{
				return;
			}
			Delegate Cur = (Delegate)disp.Pop() ;
			Cur.display ();
			displayD (disp);
		}

        public void makePayment(int D, double Amnt)
        /* pre:  Have list of delegates, possibly empty. Have delegate identifier and amount paid.
         * post: Modify the relevant property for the given delegate.  Retain the original ordering of delegates in the list.
         */
        {

			if (Delegates.Count == 0)
				return;

			Stack remover = new Stack ();
			while (Delegates.Count != 0) {
				Delegate add = (Delegate)Delegates.Pop();
				if (add.DelID == D) {
					add.processPayment (Amnt);
				} 
					remover.Push (add);
			}

			Delegates = (Stack)remover.Clone();

            
        }
 
        public Stack duplicateList()
        /* pre:  Have list of delegates, possibly empty.
         * post: Return exact copy (clone) of the list.  Original list must retain original ordering.
         */
        {

			Stack Dup = (Stack)Delegates.Clone();

			return Dup;
            
        }

        public void reverseList()
        /* pre:  Have list of delegates, possibly empty.
         * post: Reverse list of delegates.
         */
        {
			Stack Rev = new Stack ();
			while (Delegates.Count != 0) {
				Delegate Add = (Delegate)Delegates.Pop ();
				Rev.Push (Add);
			}
			Delegates = Rev;
            

        }


        public int noStillOwing()
        /* pre:  Have list of delegates, possibly empty.
         * post: Calculate and return number of delegates who still owe an amount. Original list must retain original 
         *       ordering.
         */
        {
			if (Delegates.Count == 0)
				return 0;
			int x = 0;
			Stack dup = duplicateList();
			int test = (int)dup.Count;
			while (test != 0) {
				Delegate Check = (Delegate)dup.Pop();
				if (Check.getDue() > 0)
					x++;
				test--;
			}
			return x;
        }


        // OPTIONAL TASKS 

        public bool deleteDelegate(int D)
        /* pre:  Have list of delegates, possibly empty.  Have identifier of delegate to remove
         * post: Remove the specified delegate from the list of delegates and return true.  If delegate is not in the list, 
         *       return false. If the removed delegate is also a presenter, display an appropriate warning message.
         *       NOTE: The list of delegates must retain the original ordering of the remaining delegates after processing 
         *       has been completed.
         */
        {
			int x = Delegates.Count;
			if (x == 0)
				return false;
			reverseList ();

			Stack remover = new Stack ();
			while (Delegates.Count != 0) {
				Delegate add = (Delegate)Delegates.Pop();
				if (add.DelID == D) {
					if (add.Presenter)
					Console.WriteLine ("Deleting a Presenter");
				} else {
					remover.Push (add);
				}
			}
			Delegates = (Stack)remover.Clone();
			if(remover.Count < x)
            return true;

			return false;


        }

        public double totalDue()
        /* pre:  Have list of delegates, possibly empty.
         * post: Calculate and return total amount outstanding for all delegates. Original list must retain original 
         *       ordering.
         */
        {

			double Total = 0;
			if (Delegates.Count == 0)
				return 0;

			reverseList ();
			Stack remover = new Stack ();
			while (Delegates.Count != 0) {
				Delegate add = (Delegate)Delegates.Pop();
				Total += add.getDue ();
				remover.Push (add);

			}
			Delegates = (Stack)remover.Clone();

			return Total;        
        }
    }
}
