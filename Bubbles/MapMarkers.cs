using Mindjet.MindManager.Interop;
using PRAManager;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bubbles
{
    internal class MapMarkers
    {
        public static bool AddTagToTopic(Topic aTopic, string tagName, string tagID, string groupName, string groupID, string tagColor = "", bool mutex = false)
        {
            if (aTopic == null)
                return false;

            MapMarkerGroup _mmg = GetMapMarkerGroup(groupName, groupID, true, mutex);
            if (_mmg == null)
                return false;

            string groupId = _mmg.GroupId;

            // if group doesn't have this tag, add it to group
            if (!AddTextLabelMarkerToGroup(_mmg, tagName, tagID, tagColor))
                return false;

            try
            {
                // add tag to the topic
                aTopic.TextLabels.AddTextLabelFromGroup(tagName, groupId);
                return true;
            }
            catch { }
            return false;
        }

        /// <summary>
        /// Get or add MapMarkerGroup (only text label group)
        /// </summary>
        /// <param name="aDocument"></param>
        /// <param name="groupName">Name of marker group</param>
        /// <param name="aCreateNew">if to create marker group if not found</param>
        /// <param name="mutex">If group must be MutuallyExclusive</param>
        /// <returns>MapMarkerGroup</returns>
		public static MapMarkerGroup GetMapMarkerGroup(string groupName, string groupID = "", bool aCreateNew = true, bool mutex = false)
        {
            foreach (MapMarkerGroup _mmg1 in MMUtils.ActiveDocument.MapMarkerGroups)
            {
                //string group_ID = "";

                if (_mmg1.Type == MmMapMarkerGroupType.mmMapMarkerGroupTypeTextLabel)
                {
                    if (groupID == _mmg1.GroupId) // group found by given ID
                    {
                        if (groupName != "" && _mmg1.Name != groupName)
                            _mmg1.Name = groupName;
                        return _mmg1;
                    }

                    // not found by ID, search by name
                    if (_mmg1.Name == groupName)
                        return _mmg1;

                }
            }
            if (aCreateNew)
            {
                // Not found - create new
                try
                {
                    MapMarkerGroup MarkerGroup = MMUtils.ActiveDocument.MapMarkerGroups.AddTextLabelMarkerGroup(groupName);
                    MarkerGroup.MutuallyExclusive = mutex;
                    return MarkerGroup;
                }
                catch { }
            }
            return null;
        }

        /// <summary>
        /// Get or add MapMarkerGroup (icon group)
        /// </summary>
        /// <param name="aDocument"></param>
        /// <param name="groupName">Name of icon group</param>
        /// <param name="aCreateNew">if to create marker group if not found</param>
        /// <param name="mutex">If group must be MutuallyExclusive</param>
        /// <returns>MapMarkerGroup</returns>
		public static MapMarkerGroup GetIconGroup(string groupName, string groupID = "", 
            bool aCreateNew = true, bool mutex = false, bool askuser = true)
        {
            MapMarkerGroup mgroup = null;

            foreach (MapMarkerGroup mg in MMUtils.ActiveDocument.MapMarkerGroups)
            {
                if (mg.Type == MmMapMarkerGroupType.mmMapMarkerGroupTypeIcon)
                {
                    if (groupID == mg.GroupId) // group found by given ID
                    {
                        if (groupName != "" && mg.Name != groupName)
                            mg.Name = groupName;

                        mgroup = mg; break;
                    }

                    // not found by ID, search by name
                    if (mg.Name == groupName)
                    {
                        mgroup = mg; break;
                    }
                }
            }
            if (mgroup != null)
            {
                if (MessageBox.Show(Utils.getString("AddIconGroupDlg.IconGroupExists"), "", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                { return null; } // User doesn't want use this group
                return mgroup;
            }

            if (aCreateNew)
            {
                // Not found - create new
                try
                {
                    MapMarkerGroup mg = MMUtils.ActiveDocument.MapMarkerGroups.AddIconMarkerGroup(groupName);
                    mg.MutuallyExclusive = mutex;
                    return mg;
                }
                catch { }
            }
            return null; // We can't create group
        }

        /// <summary>
        /// Adds marker (tag or resource) to Marker Group
        /// </summary>
        /// <param name="mg">Marker Group to add marker</param>
        /// <param name="tagName">marker name</param>
        /// <param name="color">marker color</param>
        /// <param name="changeColor">if marker exists in the group but has another color, then change color</param>
        /// <returns></returns>
        public static bool AddTextLabelMarkerToGroup(MapMarkerGroup mg, string tagName, string tagID, string color = "", bool changeColor = false, bool resourceMarker = false)
        {
            string groupId = mg.GroupId;

            MapMarker tag = GetTagFromGroup(mg, tagName);

            if (tag == null) // There is no marker in the group
            {
                try
                {
                    if (resourceMarker)
                        tag = mg.AddResourceMarker(tagName);
                    else
                        tag = mg.AddTextLabelMarker(tagName);

                    if (tagID != "")
                    {
                        tag.GetAttributes(ATTR_MARKERS).SetAttributeValue(URI_MARKERS, tagID);
                    }
                    if (color != "")
                    {
                        int c = int.Parse(color.Substring(1), NumberStyles.HexNumber);
                        tag.Color.SetValue(c);
                    }
                }
                catch { MessageBox.Show("Problem with adding tag"); return false; }
            }
            else if (changeColor) // tag exists. Check for tag color
            {
                int tagColor = tag.Color.Value;

                if (color == "")
                {
                    if (tagColor != 0)
                        tag.Color.SetValue(0);
                }
                else
                {
                    int c = int.Parse(color.Substring(1), System.Globalization.NumberStyles.HexNumber);
                    if (tagColor != c)
                        tag.Color.SetValue(c);
                }
            }
            return true;
        }

        /// <summary>
        /// Check if given tag group contains tag
        /// </summary>
        /// <param name="group"></param>
        /// <param name="tagName"></param>
        /// <returns>True if tag group contains tag</returns>
        public static MapMarker GetTagFromGroup(MapMarkerGroup group, string tagName)
        {
            foreach (MapMarker mm in group)
            {
                if (mm.Label == tagName)
                    return mm;
            }
            return null;
        }

        /// <summary>
        /// Check if icon exists in the Map Index. If not, add icon to Map Index
        /// </summary>
        /// <param name="aIcon">Stock Icon to check</param>
        /// <param name="signature">Custom Icon signature</param>
        /// <param name="iconName">Icon name</param>
        /// <param name="path">Path to custom icon</param>
        /// <param name="addtomap">If method is called by AddToMap command</param>
        /// <return>False if icon is not in the specified group</return>
        public static bool GetIcon(MmStockIcon aIcon, string signature, string iconName, string path,
            string groupName = "", bool mutex = false, bool addtomap = false)
        {
            foreach (MapMarkerGroup mg in MMUtils.ActiveDocument.MapMarkerGroups)
            {
                bool found = false;
                if (mg.Type == MmMapMarkerGroupType.mmMapMarkerGroupTypeIcon || mg.Type == MmMapMarkerGroupType.mmMapMarkerGroupTypeSingleIcon)
                {
                    foreach (MapMarker icon in mg)
                    {
                        if (icon.Icon.Type == MmIconType.mmIconTypeStock)
                        {
                            if (icon.Icon.StockIcon == aIcon)
                            {
                                if (groupName == "" || mg.Name == groupName)
                                    return true; // icon exists in the right group already

                                if (!addtomap) // adding icon to topic
                                    return false; // icon is located in the other group

                                // Icon should be added to the specified map marker group,
                                // but it's located in the other group.
                                if (!RelocateIcon(mg, groupName, icon, iconName))
                                    return false; // user wants to remain icon in the other group.

                                // Icon is deleted. Will be added to group below
                                found = true;
                                break;
                            }
                        }
                        else // custom icon
                        {
                            if (icon.Icon.CustomIconSignature == signature)
                            {
                                if (groupName == "" || mg.Name == groupName)
                                    return true; // icon exists in the right group already

                                if (!addtomap) // adding icon to topic
                                    return false; // icon is located in the other group

                                // Ups. Icon should be added to the specified map marker group,
                                // but it's located in the other group.
                                if (!RelocateIcon(mg, groupName, icon, iconName))
                                    return false; // user wants remain icon in another group.

                                // Icon is deleted. Will be added to group below
                                found = true;
                                break;
                            }
                        }
                        if (found) break;
                    }
                }
            }

            // Icon not found or deleted. Add icon to the group
            MapMarkerGroup _mg;
            if (groupName == "")
                _mg = MMUtils.ActiveDocument.MapMarkerGroups.GetMandatoryMarkerGroup(MmMapMarkerGroupType.mmMapMarkerGroupTypeSingleIcon);
            else
                _mg = GetIconGroup(groupName, mutex, true);

            if (_mg != null)
            {
                if (signature == "")
                    _mg.AddStockIconMarker(iconName, aIcon);
                else
                    _mg.AddCustomIconMarker(iconName, path);
            }
            return true;
        }

        /// <summary>
        /// Get icon group or create one.
        /// </summary>
        /// <param name="groupName">Group name</param>
        /// <param name="mutex"></param>
        /// <returns></returns>
        static MapMarkerGroup GetIconGroup(string groupName, bool mutex, bool createNew = false)
        {
            foreach (MapMarkerGroup mg in MMUtils.ActiveDocument.MapMarkerGroups)
            {
                if (mg.Type == MmMapMarkerGroupType.mmMapMarkerGroupTypeIcon)
                    if (mg.Name == groupName) return mg;
            }

            // Group doesn't exist. Create group.
            if (createNew)
            {
                MapMarkerGroup _mg = MMUtils.ActiveDocument.MapMarkerGroups.AddIconMarkerGroup(groupName);
                if (_mg != null)
                    _mg.MutuallyExclusive = mutex;
                return _mg;
            }
            return null;
        }

        /// <summary>
        /// Delete or leave icon in the icon group
        /// </summary>
        /// <param name="mg">Marker group where icon is located.</param>
        /// <param name="groupName">User marker group name.</param>
        /// <param name="icon">Icon to operate with.</param>
        /// <returns>False - icon remains in the group. True - icon will be deleted.</returns>
        static bool RelocateIcon(MapMarkerGroup mg, string groupName, MapMarker icon, string iconName)
        {
            if (mg.Type == MmMapMarkerGroupType.mmMapMarkerGroupTypeSingleIcon)
            {
                // Icon found in the Single Icons group.
                // Delete it. Will be added to our group further.
                icon.Delete();
            }
            else // Icon found in another group. Ask user what to do...
            {
                if (MessageBox.Show(String.Format(Utils.getString("icons.askuser_whattodo_withicon"), iconName, mg.Name, groupName), "",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    icon.Delete(); // User wants to move icon to his group.
                                   // Delete icon. Will be added to the group further.
                else
                    return false; // Icon remains in another group.
            }
            return true;
        }

        public const string ATTR_MARKERS = "PALMAROSS$$$MARKERS";
        public const string URI_MARKERS = "UriUniqueID";
    }
}
