/*
 * Proje	    : Outlook 2003 Style SideBar v 1.1
 *
 * Hazırlayan   : Muhammed ŞAHİN
 * eMail        : muhammed.sahin@gmail.com
 *
 * Açıklama	    : Outlook 2003 Style Sidebar
 *              : TabButtonlar için kolleksiyon
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace SMLERPControl
{
    /// <summary>
    /// TabButton collection
    /// </summary>
    public sealed class OutlookStyleTabButtonCollection : IList<OutlookStyleTabButton>, IDisposable
    {

        /// <summary>
        /// TabButtonların listesi
        /// </summary>
        List<OutlookStyleTabButton> tabButtonList = new List<OutlookStyleTabButton>();

        #region Delegate Tanımları

        internal delegate void OnItemAddedEventHandler(OutlookStyleTabButtonEventArgs e);
        /// <summary>
        /// Collection içerisinden TabButton çıkarıldığında
        /// </summary>
        internal event OnItemAddedEventHandler OnTabButtonItemAdded;

        internal delegate void OnItemRemovedEventHandler(OutlookStyleTabButtonEventArgs e);
        /// <summary>
        /// Collection içerisine yeni TabButton eklendiğinde
        /// </summary>
        internal event OnItemRemovedEventHandler OnTabButtonItemRemoved;

        #endregion

        #region IList<OutlookStyleTabButton>

        public int IndexOf(OutlookStyleTabButton item)
        {
            return tabButtonList.IndexOf(item);
        }

        public void Insert(int index, OutlookStyleTabButton item)
        {
            tabButtonList.Insert(index, item);

            if (OnTabButtonItemAdded != null)
                OnTabButtonItemAdded(new OutlookStyleTabButtonEventArgs(item));
        }

        public void RemoveAt(int index)
        {

            tabButtonList.RemoveAt(index);

            if (OnTabButtonItemRemoved != null)
                OnTabButtonItemRemoved(new OutlookStyleTabButtonEventArgs(tabButtonList[index]));

        }

        public OutlookStyleTabButton this[int index]
        {
            get
            {
                return tabButtonList[index];
            }
            set
            {
                tabButtonList[index] = value;
            }
        }

        #endregion

        #region ICollection<OutlookStyleTabButton>

        public void Add(OutlookStyleTabButton item)
        {
            tabButtonList.Add(item);

            if (OnTabButtonItemAdded != null)
                OnTabButtonItemAdded(new OutlookStyleTabButtonEventArgs(item));
        }

        public void Clear()
        {
            tabButtonList.Clear();
        }

        public bool Contains(OutlookStyleTabButton item)
        {
            return tabButtonList.Contains(item);
        }

        public void CopyTo(OutlookStyleTabButton[] array, int arrayIndex)
        {
            tabButtonList.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return tabButtonList.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(OutlookStyleTabButton item)
        {

            bool isRemoved = tabButtonList.Remove(item);

            if (OnTabButtonItemRemoved != null)
                OnTabButtonItemRemoved(new OutlookStyleTabButtonEventArgs(item));

            return isRemoved;
        }

        #endregion

        #region IEnumerable<OutlookStyleTabButton>

        public IEnumerator<OutlookStyleTabButton> GetEnumerator()
        {
            IEnumerator<OutlookStyleTabButton> enumator = tabButtonList.GetEnumerator();
            return enumator;
        }

        #endregion

        #region IEnumerable

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return (System.Collections.IEnumerator)tabButtonList;
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            tabButtonList = null;
            GC.SuppressFinalize(this);
        }

        #endregion

    }

}
