namespace MedicalStaff.WebService.Core.Helpers.Properties
{
    /// <summary>
    /// Specifies the role of that target when referenced in any proper derived context.
    /// </summary>
    public enum Role : System.UInt16
    {
        /// <summary>
        /// Specifies that no role was set.
        /// </summary>
        Unspecified = 0,

        /// <summary>
        /// Specifies that target role is Physician.
        /// </summary>
        Physician = 1,

        /// <summary>
        ///Specifies that target role is Patient.
        /// </summary>
        Patient = 2
    }
}