using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;

/*
 * @Author Raymond Strohschein
 * UNBC Winter 2018 Semester
 * CPSC473 Final Project
 */

namespace VIPER_Algorithm
{
    //This class runs the algorithm
    class Viper
    {
        private int minimumSupportPercent, minimumSupport, numTransactions, numFrequentPatterns;
        private long totalTime, minutes, seconds;
        private StreamReader reader;
        private MainWindow mw;
        private String path;
        private Stopwatch sw;
        private DataBase database, currentItemSets;
        private List<String> frequentItemSets;
        public Viper(MainWindow mainWindow, String p, int s)
        {
            mw = mainWindow;
            mw.Result_Box.Content = ("");
            mw.Frequent_Patterns_Label.Content = "FPs = 0";
            path = p;
            minimumSupportPercent = s;
            database = new DataBase();
            sw = new Stopwatch();
            frequentItemSets = new List<String>();
            Configure();
        }

        //Configure the file and database
        public void Configure()
        {
            reader = new StreamReader(path);
            string line = reader.ReadLine();
            string[] stringSeparators = new string[] {" ", ""};
            string[] tokens = line.Split(stringSeparators, StringSplitOptions.None);

            //Take the very first item, that is the number of transactions
            numTransactions = Int32.Parse(tokens[0]);
            sw.Start();
            CreateDatabase();
            //Mine the previous dataset until there is no more frequent patterns
            while (database.GetSize() != 0)
            {
                MineFrequentItemSets(database);
            }
            sw.Stop();
            totalTime = sw.ElapsedMilliseconds;
            minutes = totalTime / 1000 / 60;
            seconds = totalTime / 1000 % 60;
            mw.Frequent_Patterns_Label.Content = "FPs = "+numFrequentPatterns;
            if (totalTime > 999)
            {
                mw.Time_Taken_Label.Content = "Time Taken:\n" + minutes+ " min "+seconds+" s";
            }
            else
            {
                mw.Time_Taken_Label.Content = "Time Taken:\n" + totalTime + " ms";
            }
            PrintToResultBox();
        }

        public void CreateDatabase()
        {
            while (!reader.EndOfStream)
            {
                try
                {
                    string line = reader.ReadLine();
                    string[] tokens = line.Split(null);

                    int transactionID = Int32.Parse(tokens[0]);
                    for (int i = 2; i < tokens.Length; i++)
                    {
                        //Condition if the database contains the itemset
                        if (database.Contains(tokens[i]))
                        {
                            //Increment the support by 1 and change the boolean at TransactionID to true
                            database.Get(tokens[i]).IncrementSupport(1);
                            database.Get(tokens[i]).IsInTransaction(transactionID - 1);
                        }
                        //Condition if it is not in the database
                        else if(tokens[i] != "" && tokens[i] != " ")
                        {
                            ItemSet item = new ItemSet(tokens[i], numTransactions);
                            item.IsInTransaction(transactionID - 1);
                            item.IncrementSupport(1);
                            database.Add(item);
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
            minimumSupport = minimumSupportPercent * numTransactions / 100;
            if (minimumSupport == 0)
            {
                minimumSupport = 1;
            }
            //Filter the database and commence the mining
            database.Filter(numTransactions, minimumSupport);
            database.Sort();
            foreach (ItemSet item in database.GetDataBase())
            {
                frequentItemSets.Add("["+item.GetItems() + "]: " + item.GetSupport());
                numFrequentPatterns++;
            }
        }

        //Method to create new frequent itemsets
        public void MineFrequentItemSets(DataBase previousItemSets)
        {
            //Create the new dataset based on previous ItemSets
            currentItemSets = new DataBase();
            for (int i = 0; i < previousItemSets.GetSize() - 1; i++)
            {
                ItemSet left = previousItemSets.Get(i);
                for (int j = i + 1; j < previousItemSets.GetSize(); j++)
                {
                    ItemSet right = previousItemSets.Get(j);
                    ItemSet item = Combine(left, right);
                    //Putting this here saves a ton of time
                    //Went from a 4 average minute runtime to 950 milliseconds average runtime
                    //If the head does not match, we break since the heads are ordered
                    if (!Compare(left.ItemsToIntArray(), right.ItemsToIntArray(), left.ItemsToIntArray().Length - 1))
                    {
                        break;
                    }
                    //If the item is not null we want to add it to the list
                    if (item != null)
                    {
                        currentItemSets.Add(item);
                        numFrequentPatterns++;
                        frequentItemSets.Add("[" + item.GetItems() + "]: " + item.GetSupport());
                    }
                }
            }
            currentItemSets.Filter(numTransactions, minimumSupport);
            database = currentItemSets;
        }

        //If the support of the combined itemset is greater than the minsup, return item else null;
        public ItemSet Combine(ItemSet left, ItemSet right)
        {
            //This won't work for more than 1 item
            int[] leftArr = left.ItemsToIntArray();
            int[] rightArr = right.ItemsToIntArray();
            //If the heads of both arrays are the same with 1 element missing we go into the loop
            if (Compare(leftArr, rightArr, leftArr.Length - 1))
            {
                ItemSet item = new ItemSet(left.GetItems() + ", " + rightArr[rightArr.Length - 1], numTransactions);
                for (int i = 0; i < numTransactions; i++)
                {
                    if (left.InTransaction(i) == true && right.InTransaction(i) == true)
                    {
                        item.IsInTransaction(i);
                        item.IncrementSupport(1);
                    }
                }

                //If the support of the item is greater than or equal to min sup we want to keep it
                if (item.GetSupport() >= minimumSupport)
                {
                    return item;
                }
                //Else we do not want to keep it
                else
                {
                    return null;
                }
            }
            return null;
        }

        //Method to check if the heads of an ItemSet are equivalent minus the last item
        public bool Compare(int[] a, int[] b, int threshold)
        {
            bool c = false;
            int numSameElements = 0;
            for (int i = 0; i < threshold; i++)
            {
                if (a[i] == b[i])
                {
                    numSameElements++;
                }

                else
                {
                    break;
                }
            }
            if (numSameElements == threshold)
            {
                c = true;
            }
            return c;
        }

        //Print something to the viewbox
        public void Print(String s)
        {
            mw.Result_Box.Content += s + "\n";
        }

        //Print the results to the viewbox
        public void PrintToResultBox()
        {
            Print("Running VIPER Algorithm on a Minimum Support of " + minimumSupportPercent + "% and " + numTransactions + " Transactions");
            frequentItemSets.Insert(0, "FPs = " + numFrequentPatterns);
            if (totalTime > 999)
            {
                Print("Time taken: " + minutes + " min " + seconds + " s");
                frequentItemSets.Insert(0, "Time taken: " + minutes + " min " + seconds + " s");
            }
            else
            {
                Print("Time taken: " + totalTime + " ms");
                frequentItemSets.Insert(0, "Time taken: " + totalTime + " ms");
            }
            Print("FPs = " + numFrequentPatterns);
            frequentItemSets.Insert(0, "Running VIPER Algorithm on a Minimum Support of " + minimumSupportPercent + "% and " + numTransactions + " Transactions");
            //Send the data over to the main window
            mw.SetFrequentItemSets(frequentItemSets);
        }
    }
}
