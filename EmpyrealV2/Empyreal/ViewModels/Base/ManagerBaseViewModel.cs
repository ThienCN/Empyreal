using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Empyreal.ViewModels.Base
{
    /// <summary>
    /// BaseClass: Use for Manager
    /// </summary>
    /// <history>
    /// [Lương Mỹ] Create [14/04/2019]
    /// </history>
    public class ManagerBaseViewModel
    {
        /// <summary>
        /// Action: Create or Update.
        /// True: Update || False: Create
        /// </summary>
        /// <history>
        /// [Lương Mỹ] Create [14/04/2019]
        /// </history>
        public bool IsUpdate { get; set; }

        /// <summary>
        /// Action Return.
        /// True: Error || False: Success
        /// </summary>
        /// <history>
        /// [Lương Mỹ] Create [14/04/2019]
        /// </history>
        public bool IsError { get; set; }

        /// <summary>
        /// Message Notification
        /// </summary>
        /// <history>
        /// [Lương Mỹ] Create [14/04/2019]
        /// </history>
        public string Message { get; set; }

        /// <summary>
        /// Attribute Form:
        /// True: Hidden || False: Show
        /// </summary>
        /// <history>
        /// [Lương Mỹ] Create [14/04/2019]
        /// </history>
        public bool IsHidden { get; set; }
    }
}
