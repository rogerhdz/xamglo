using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace XamarinPO.Extensions
{
    /// <summary> 
    /// Represents a dynamic data collection that provides notifications when items get added, removed, or when the whole list is refreshed. 
    /// </summary> 
    public static class ObservableCollectionExtension
    {
        /// <summary> 
        /// Adds the elements of the specified collection to the end of the ObservableCollection(Of T). 
        /// </summary> 
        /// <param name="collection">Original collection</param>
        /// <param name="newCollection">Collection to insert to original observable</param>
        public static void AddRange<T>(this ObservableCollection<T> collection, List<T> newCollection)
        {
            if (collection == null) throw new ArgumentNullException("collection");
            foreach (var i in newCollection) collection.Add(i);
        }
    }
}




