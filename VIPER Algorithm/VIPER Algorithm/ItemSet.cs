using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * @Author Raymond Strohschein
 * UNBC Winter 2018 Semester
 * CPSC473 Final Project
 */

namespace VIPER_Algorithm
{
    //Class is used to create itemsets for the database
    class ItemSet : IComparable<ItemSet>
    {
        //private bool[] inTransactions;
        private BitArray inTransactions;
        private String items;
        private int support;
        public ItemSet(String s, int numTransactions)
        {
            SetItems(s);
            SetSupport(0);
            //Should be populated with false at the start
            //inTransactions = new bool[numTransactions];
            inTransactions = new BitArray(numTransactions);
        }

        //Set the support of the item
        private void SetSupport(int s)
        {
            support = s;
        }

        private void SetItems(String s)
        {
            items = s;
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

        //Return the items as an int array
        public int[] ItemsToIntArray()
        {
            string[] stringSeparators = new string[] { ", " };
            string[] i = items.Split(stringSeparators, StringSplitOptions.None);
            int[] array = Array.ConvertAll(i, int.Parse);
            return array;
        }

        //Method to compare 2 itemsets
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

        //If it is in the i transaction
        public bool InTransaction(int i)
        {
            return inTransactions[i];
        }
    }
}
