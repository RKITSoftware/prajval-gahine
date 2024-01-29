namespace FirmWebApiDemo.Models
{
    public class USR01_EMP01
    {
        /// <summary>
        /// User id
        /// </summary>
        public int p01f01;

        /// <summary>
        /// Employee id
        /// </summary>
        public int p01f02;

        public USR01_EMP01(int usedId, int EmployeeId)
        {
            this.p01f01 = usedId;
            this.p01f02 = EmployeeId;
        }
    }
}