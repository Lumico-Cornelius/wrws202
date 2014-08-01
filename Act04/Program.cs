using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;

namespace Act04
{
    class Program
    {
        static void Main(string[] args)
       	{
		    Conference ACM = new Conference("ACM", "Barcelona", 8, 4000);

            Console.WriteLine("List of registered delegates - should be empty:");
            ACM.displayDelegates();
            Console.WriteLine("--------------------------- Check that output is as expected - press enter to continue");
            Console.ReadLine();

            Console.WriteLine("Registering delegates .........");
            Console.WriteLine();

            // Delegates read from file and added to stack

            StreamReader Input = new StreamReader("Delegates.txt");
            while (!Input.EndOfStream)
            {
                Delegate newOne = new Delegate(int.Parse(Input.ReadLine()), Input.ReadLine(), Input.ReadLine(), ACM.Cost, bool.Parse(Input.ReadLine()));
                ACM.registerDelegate(newOne);
            }
            Console.WriteLine("List of registered delegates:");
            ACM.displayDelegates();
  		    Console.WriteLine("--------------------------- Check that output is as expected - press enter to continue");
            Console.ReadLine();

            Console.WriteLine("Processing payments for delegates .........");
            Console.WriteLine();

            // Payments read from file and relevant delegate payments modified

            Input = new StreamReader("Payments.txt");
            while (!Input.EndOfStream)
                ACM.makePayment(int.Parse(Input.ReadLine()), double.Parse(Input.ReadLine()));
            Console.WriteLine("List of registered delegates:");
            ACM.displayDelegates();
            Console.WriteLine("--------------------------- Check that output is as expected - press enter to continue");
            Console.ReadLine();

            Console.WriteLine("Duplicating list of delegates .........");
            Console.WriteLine();

            // Duplicated list is stored, changes made to it & contents displayed

            Stack duplList = ACM.duplicateList();
            if (duplList.Count > 0)
            {
                Delegate top = (Delegate)duplList.Peek();
                top.DName = "THIS ONE HAS BEEN CHANGED";
            }
            Stack temp = new Stack();
            Console.WriteLine("Registered delegates in duplicated list - note changed data:");
            while (duplList.Count > 0)
            {
                Delegate cur = (Delegate)duplList.Pop();
                cur.display();
                temp.Push(cur);
            }
            duplList = temp;
            Console.WriteLine();
            
            Console.WriteLine("List of registered delegates in original list:");
            ACM.displayDelegates();
            Console.WriteLine("--------------------------- Check that output is as expected - press enter to continue");
            Console.ReadLine();

            Console.WriteLine("Reversing list of delegates .........");
            Console.WriteLine();

            // List of registered delegates is reversed and displayed

            ACM.reverseList();
            Console.WriteLine("List of registered delegates after reversing it:");
            ACM.displayDelegates();
            Console.WriteLine("--------------------------- Check that output is as expected - press enter to continue");
            Console.ReadLine();

            Console.WriteLine("Determining how many delegates still owe for the conference .........");
            Console.WriteLine();

            // Checking to see how many delegates owe more than 0 for conference fees

            Console.WriteLine("Number of delegates who still owe: {0}", ACM.noStillOwing());
            Console.WriteLine();

            Console.WriteLine("List of registered delegates:");
            ACM.displayDelegates();
            Console.WriteLine("--------------------------- Check that output is as expected - press enter to continue");
            Console.ReadLine();

            Console.WriteLine("Processing cancellations for delegates (deleting from list) .........");
            Console.WriteLine();

            // Delegate identifiers read from file and relevant delegates deleted from list

            Input = new StreamReader("Cancellations.txt");
            while (!Input.EndOfStream)
            {
                int DelID = int.Parse(Input.ReadLine());
                if (ACM.deleteDelegate(DelID))
                    Console.WriteLine("Delegate {0} deleted", DelID);
            }
            Console.WriteLine("List of registered delegates after deletions:");
            ACM.displayDelegates();
            Console.WriteLine("--------------------------- Check that output is as expected - press enter to continue");
            Console.ReadLine();

            Console.WriteLine("Determining total amount due by delegates .........");
            Console.WriteLine();

            // Determining total amount still owing from all delegates

            Console.WriteLine("Total amount owing: {0}", ACM.totalDue());
            Console.WriteLine();

            Console.WriteLine("List of registered delegates:");
            ACM.displayDelegates();
            Console.WriteLine("--------------------------- Check that output is as expected - press enter to continue");
            Console.ReadLine();

            Console.WriteLine("Processing terminated successfully");
            Console.ReadLine();
        }
    }
}
