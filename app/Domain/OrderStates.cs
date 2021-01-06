using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    /// <summary>
    /// Enumerator is used to translates different order states from int to string.
    /// So if someone decides to add another order state - it can all be changed in one place - this enumerator (and also inside SQL)
    /// Also makes code more read-able, as instead of states being 1,2,3,4, I can instead write it as "New" "Pending" "Rejected" "Complete"
    /// </summary>
    public enum OrderStates
    {
        New = 1,
        Pending = 2,
        Rejected = 3,
        Complete = 4
    }
}