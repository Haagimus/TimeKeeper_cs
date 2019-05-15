using System.Windows.Forms;
using Time_Keeper.Controllers;

namespace Time_Keeper.Interfaces
{
    interface IWhatsNewView
    {
        #region Form Controls
        string AssemblyVersion { get; set; }
        string ChangeLog { get; set; }
        CheckBox CheckBoxShowAtStart { get; set; }
        #endregion

        #region Form Methods
        void SetController(WhatsNewController controller);
        void SetVersion();
        void SetChangeLog();
        void SetCheckbox();
        void CheckedChanged();
        #endregion
    }
}