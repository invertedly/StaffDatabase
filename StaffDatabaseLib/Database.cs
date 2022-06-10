using System.Collections.Generic;

namespace StaffDatabaseLib
{
    public sealed class StaffDatabase : Dictionary<EmployeeId, HashSet<Employee>>
    {
    }
}
