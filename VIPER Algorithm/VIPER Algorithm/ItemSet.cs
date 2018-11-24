using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIPER_Algorithm
{
    //Class is used to create itemsets for the database
    class ItemSet : IComparable<ItemSet>
    {
        private bool[] inTransactions;
        private String items;
        private int support;
        public ItemSet(String s, int numTransactions)
        {
            items = s;
            support = 0;
            //Should be populated with false at the start
            inTransactions = new bool[numTransactions];
        }

        //Set the support of the item
        public void SetSupport(int s)
        {
            support = s;
        }

        //Increment the support of the item by s
        public void IncrementSupport(int s)
        {
            support += s;
        }

        //The item is in the i transaction
        public void IsInTransaction(int i)
        {
            inTransactions[i] = true;
        }

        //Return the support of the itemset
        public int GetSupport()
        {
            return support;
        }

        //Get the elements of the itemset
        public String GetItems()
        {
            return items;
        }

        public int[] ItemsToIntArray()
        {
            string[] stringSeparators = new string[] { ", " };
            string[] i = items.Split(stringSeparators, StringSplitOptions.None);
            int[] array = Array.ConvertAll(i, int.Parse);
            return array;
        }

        public int CompareTo(ItemSet other)
        {
            int[] thisArray = ItemsToIntArray();
            int[] otherArray = other.ItemsToIntArray();
            for (int i = 0; (i < thisArray.Length && i < otherArray.Length); i++)
            {
                int diff = thisArray[i] - otherArray[i];
                switch (diff)
                {
                    case 0:
                    {
                            break;
                    }

                    default:
                    {
                            return diff;
                    }
                }
            }
            return 0;
        }

        //Return the entire transaction array for this itemset
        public bool[] GetInTransactions()
        {
            return inTransactions;
        }

        //If it is in the i transaction
        public bool InTransaction(int i)
        {
            return inTransactions[i];
        }
    }
}
