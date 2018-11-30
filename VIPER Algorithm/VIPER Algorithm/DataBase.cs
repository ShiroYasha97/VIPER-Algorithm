using System;
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
    //This class is dedicated to containing all the itemsets seen
    class DataBase
    {
        private List<ItemSet> database;
        public DataBase()
        {
            //Initialize the database
            database = new List<ItemSet>();
        }

        public void Add(ItemSet i)
        {
            //Add the item to the database. Checking will be done in the VIPER class
            database.Add(i);
        }

        //Filter the database and remove infrequent itemsets
        public void Filter(int numTransactions, int minSup)
        {
            int size = GetSize();
            List<ItemSet> itemsToRemove = new List<ItemSet>();
            //Create a new list of items by checking their supports
            //If the support of the itemset is less than the minimum support, add it to the new list
            foreach (ItemSet item in database)
            {
                //If the item is in the database and has less support than minsup
                if (item.GetSupport() < minSup)
                {
                    itemsToRemove.Add(item);
                }
            }

            //Now we can remove each item from the database
            foreach (ItemSet item in itemsToRemove)
            {
                database.Remove(item);
            }
        }

        //Sort the database, not much to note
        public void Sort()
        {
            database.Sort();
        }

        //Return the size of the database
        public int GetSize()
        {
            //Return the number of items in the database
            return database.Count;
        }

        //Get the item that contains string
        public ItemSet Get(String s)
        {
            ItemSet item = null;
            //Search through database looking for an itemset that has "s" as the item
            foreach (ItemSet i in database)
            {
                //If the item is in the databas
                if (i.GetItems() == s)
                {
                    item = i;
                    break;
                }
            }
            return item;
        }

        //Get an element at the index i
        public ItemSet Get(int i)
        {
            return database.ElementAt(i);
        }

        //Check to see if the database contains such an item
        public bool Contains(String s)
        {
            bool isContained = false;
            //Similar to Get, return true if the item is in the database
            foreach (ItemSet i in database)
            {
                if (i.GetItems() == s)
                {
                    isContained = true;
                    break;
                }
            }
            return isContained;
        }

        //Return the database to the user
        public List<ItemSet> GetDataBase()
        {
            return database;
        }
    }
}
