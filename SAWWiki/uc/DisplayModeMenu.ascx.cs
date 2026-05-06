using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace SAWWiki
{
    public partial class DisplayModeMenu : System.Web.UI.UserControl 
    {
        // Use a field to reference the current WebPartManager control.
        WebPartManager _manager;

        void Page_Init(object sender, EventArgs e)
        {
            Page.InitComplete += new EventHandler(InitComplete);
        }

        void InitComplete(object sender, System.EventArgs e)
        {

            _manager = WebPartManager.GetCurrentWebPartManager(Page);
            if (_manager.Personalization.CanEnterSharedScope)
            {
                //Page layout changes will propogate to all users
                if (_manager.Personalization.Scope == PersonalizationScope.User)
                    _manager.Personalization.ToggleScope();

                String browseModeName = WebPartManager.BrowseDisplayMode.Name;

                // Fill the drop-down list with the names of supported display modes.
                foreach (WebPartDisplayMode mode in
                  _manager.SupportedDisplayModes)
                {
                    String modeName = mode.Name;
                    // Make sure a mode is enabled before adding it.
                    if (mode.IsEnabled(_manager))
                    {
                        ListItem item = new ListItem(modeName, modeName);
                        DisplayModeDropdown.Items.Add(item);
                    }
                }
            }

        }

        // Change the page to the selected display mode.
        protected void DisplayModeDropdown_SelectedIndexChanged(object sender,
          EventArgs e)
        {
            String selectedMode = DisplayModeDropdown.SelectedValue;

            WebPartDisplayMode mode =
             _manager.SupportedDisplayModes[selectedMode];
            if (mode != null)
                _manager.DisplayMode = mode;
        }

        // Set the selected item equal to the current display mode.
        void Page_PreRender(object sender, EventArgs e)
        {
            ListItemCollection items = DisplayModeDropdown.Items;
            int selectedIndex =
              items.IndexOf(items.FindByText(_manager.DisplayMode.Name));
            DisplayModeDropdown.SelectedIndex = selectedIndex;
        }

    }
}
