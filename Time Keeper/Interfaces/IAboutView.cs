using Time_Keeper.Controllers;

namespace Time_Keeper.Interfaces
{
    public interface IAboutView
    {
        #region Form Controls
        string AssemblyTitle { get; set; }
        string AssemblyVersion { get; set; }
        string AssemblyDescription { get; set; }
        string AssemblyProduct { get; set; }
        string AssemblyCopyright { get; set; }
        string AssemblyCompany { get; set; }
        #endregion

        #region Form Methods
        void SetController(AboutController controller);
        void SetTitle();
        void SetVersion();
        void SetDescription();
        void SetProduct();
        void SetCopyright();
        void SetCompany(); 
        #endregion
    }
}
