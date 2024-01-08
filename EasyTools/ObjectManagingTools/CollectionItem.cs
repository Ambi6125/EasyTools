using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTools.ObjectManagingTools
{
    public class CollectionItem : ManagableObject, IManagable<string>
    {
        public int Amount { get; private set; }
        public CollectionItem(string itemDescription) : base(itemDescription)
        {
            Amount = 0;
        }

        public CollectionItem(string itemDescription, int initialAmount) : base (itemDescription)
        {
            Amount = initialAmount;
        }

        public void ModifyAmount(int amount)
        {
            Amount += amount;
        }
    }
    public sealed class CollectionItem<T> : CollectionItem, IManagable<string>
    {
        public Type Type
        {
            get
            {
                return typeof(T);
            }
        }

        public T HeldItem { get; set; }

        public CollectionItem(string itemDescrption, T item) : base (itemDescrption)
        {
            HeldItem = item;
        }

        public CollectionItem(string itemDescrption, T item, int initialAmount) : base(itemDescrption, initialAmount)
        {
            HeldItem = item;
        }

    }
}
