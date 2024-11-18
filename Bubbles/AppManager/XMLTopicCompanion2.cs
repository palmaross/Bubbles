using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using System.Xml;
using Mindjet.MindManager.Interop;
using MMAppManager;

namespace MMMapCompanion
{
    /// <summary>
    /// This class is the extension to TopicCompanion untended to utilize XML represrentation of the Topic
    /// </summary>
    /// <seealso cref="MMMapCompanion.TopicCompanion" />
    internal class XMLTopicCompanion : TopicCompanion
	{
		public XMLTopicCompanion() { }

		/// <summary>
		/// Initializes a new instance of the <see cref="XMLTopicCompanion"/> class.
		/// </summary>
		/// <param name="aRoot">XML node that covers the Topic</param>
		/// <param name="aManager">XML namespace manager</param>
		public XMLTopicCompanion(XmlNode aRoot, XmlNamespaceManager aManager, XMLMapCompanion aXMLDocument)
		{
			m_root = aRoot;
			m_manager = aManager;
			m_xmlDocument = aXMLDocument;
		}

		protected XMLTopicCompanion(String aXML, XMLMapCompanion aXMLDocument)
		{
			if (aXML == "")
				return;
			XmlDocument _srcxmlDoc = new XmlDocument();
			_srcxmlDoc.LoadXml(aXML);
			m_root = _srcxmlDoc.DocumentElement;
			m_xmlDocument = aXMLDocument;
		}

		public XMLTopicCompanion(Topic aTopic, XMLMapCompanion aXMLDocument = null)
		{
			if (aTopic == null)
				return;
			m_xmlDocument = aXMLDocument;
			if (m_xmlDocument == null)
				m_xmlDocument = XMLMapCompanion.Get(aTopic.Document);
			XMLTopicCompanion _xmltc = m_xmlDocument.GetTopic(aTopic.Guid) as XMLTopicCompanion;
			if (_xmltc == null)
				return;
			m_root = _xmltc.m_root;
			m_manager = _xmltc.NSManager;
		}

		public XMLMapCompanion XMLDocument
		{
			get
			{
				return m_xmlDocument;
			}
		}

		/// <summary>
		/// Gets Topic Text
		/// </summary>
		/// <exception cref="System.Exception">XMLTopicCompanion:Text.Set not implemented.</exception>
		public override string Text
		{
			get
			{
				if (m_root == null)
					return "";
				XmlNode _node = m_root.SelectSingleNode("ap:Text/@PlainText", NSManager);
				if (_node == null)
					return "";
				return _node.Value;
			}
			set
			{
				throw new Exception("XMLTopicCompanion:Text.Set not implemented.");
			}
		}

		/// <summary>
		/// Gets Topic's MindManager-Guid
		/// </summary>
		public override String Guid
		{
			get
			{
				if (m_root == null)
					return "";
				try
				{
					return m_root.Attributes["OId"].Value;
				}
				catch
				{
					return "";
				}
			}
			set
			{
				if (m_root != null)
				{
					m_root.Attributes["OId"].Value = value;
				}
			}
		}

		/// <summary>
		/// Gets the parent topic.
		/// </summary>
		/// <value>
		/// The parent topic.
		/// </value>
		public virtual XMLTopicCompanion ParentTopic
		{
			get
			{
				if (m_root == null)
					return null;
				try
				{
					XmlNode _p1 = m_root.ParentNode;
					if (_p1 == null || (_p1.Name != "ap:SubTopics" && _p1.Name != "ap:FloatingTopics"))
						return null;
					_p1 = _p1.ParentNode;
					if (_p1 == null || _p1.Name != "ap:Topic")
						return null;
					return (_p1 is XmlElement)  ? new XMLTopicCompanion(_p1 as XmlElement, NSManager, m_xmlDocument) : null;
				}
				catch
				{
					return null;
				}
			}
		}

		/// <summary>
		/// Gets parent topic's Guid
		/// </summary>
		public override String ParentGuid
		{
			get
			{
				XMLTopicCompanion _xmltc = this.ParentTopic;
				return _xmltc != null ? _xmltc.Guid : "";
			}
		}

		/// <summary>
		/// true, if contained topic is Central Topic
		/// </summary>
		public override bool IsCentralTopic
		{
			get
			{
				if (m_root == null)
					return false;
				try
				{
					XmlNode _p1 = m_root.ParentNode;
					return (_p1 != null && _p1.Name == "ap:OneTopic");
				}
				catch
				{
					return false;
				}
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance is floating or callout topic.
		/// </summary>
		/// <value>
		/// <c>true</c> if this instance is floating or callout topic; otherwise, <c>false</c>.
		/// </value>
		protected virtual bool IsFloatingOrCalloutTopic
		{
			get
			{
				if (m_root == null)
					return false;
				try
				{
					XmlNode _p1 = m_root.ParentNode;
					return (_p1 != null && _p1.Name == "ap:FloatingTopics");
				}
				catch
				{
					return false;
				}
			}
		}

		/// <summary>
		/// true, if contained topic is Main Topic
		/// </summary>
		public override bool IsMainTopic
		{
			get
			{
				if (IsFloatingOrCalloutTopic)
					return false;
				XMLTopicCompanion _xmltc = ParentTopic;
				if (_xmltc == null)
					return false;
				return _xmltc.IsCentralTopic;
			}
		}

		public XMLTopicCompanion CentralTopic
		{
			get
			{
				if (m_root == null)
					return null;
				XmlNode _centralTopicNode = m_root.OwnerDocument.DocumentElement.SelectSingleNode("//ap:OneTopic/ap:Topic", NSManager);
				if (_centralTopicNode != null)
					return new XMLTopicCompanion(_centralTopicNode, NSManager, m_xmlDocument);
				return null;
			}
		}

		/// <summary>
		/// true, if contained topic is Callout topic
		/// </summary>
		public override bool IsCalloutTopic
		{
			get
			{
				if (! IsFloatingOrCalloutTopic)
					return false;
				XMLTopicCompanion _xmltc = ParentTopic;
				if (_xmltc == null)
					return false;
				return ! _xmltc.IsCentralTopic;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="aVirtualRootGuid">Central Topic guid</param>
		/// <returns></returns>
		public override String VirtualMainTopicGuid(String aVirtualRootGuid = "")
		{
			if (ParentGuid == "")
				return "";
			String _parentGuid = ParentGuid;
			XMLMapCompanion _xmlmc = this.XMLDocument;
			if (_xmlmc == null)
				return "";
			TopicCompanion _parentTC = _xmlmc.GetTopic(_parentGuid);
			while (
				_parentTC != null &&
				!_parentTC.IsCentralTopic &&
				!_parentTC.IsMainTopic &&
				_parentGuid != "" &&
				_parentTC.Guid != aVirtualRootGuid)
			{
				_parentGuid = _parentTC.ParentGuid;
				_parentTC = _xmlmc.GetTopic(_parentGuid);
			}
			return _parentGuid;
		}

		public override String FillColor
		{
			get
			{
				if (m_root == null)
					return "AUTOMATIC";
				try
				{
					XmlNode _color = m_root.SelectSingleNode("ap:Color", NSManager);
					if (_color == null)
						return "AUTOMATIC";
					XmlAttribute _fillColor = _color.Attributes["FillColor"];
					if (_fillColor == null)
						return "AUTOMATIC";
					return _fillColor.Value;
				}
				catch
				{
					return "AUTOMATIC";
				}
			}
		}

		public String LineColor
		{
			get
			{
				if (m_root == null)
					return "AUTOMATIC";
				try
				{
					XmlNode _color = m_root.SelectSingleNode("ap:Color", NSManager);
					if (_color == null)
						return "AUTOMATIC";
					XmlAttribute _lineColor = _color.Attributes["LineColor"];
					if (_lineColor == null)
						return "AUTOMATIC";
					return _lineColor.Value;
				}
				catch
				{
					return "AUTOMATIC";
				}
			}
		}

		public override String TextColor
		{
			get
			{
				if (m_root == null)
					return "AUTOMATIC";
				try
				{
					XmlNode _font = m_root.SelectSingleNode("ap:Text/ap:Font", NSManager);
					if (_font == null)
						return "AUTOMATIC";
					XmlAttribute _color = _font.Attributes["Color"];
					if (_color == null)
						return "AUTOMATIC";
					return _color.Value;
				}
				catch
				{
					return "AUTOMATIC";
				}
			}
		}

		/// <summary>
		/// Gets a value indicating whether topic's task is empty.
		/// </summary>
		/// <value>
		///   <c>true</c> if topic's task is empty; otherwise, <c>false</c>.
		/// </value>
		public override bool TaskIsEmpty
		{
			get
			{
				if (MMOptionsDlg.taskattr_all == false)
				{
					return !(
						(MMOptionsDlg.dates == 1 && HasDueDate) ||
						(MMOptionsDlg.dates == 1 && HasStartDate) ||
						(MMOptionsDlg.progress == 1 && Complete >= 0) ||
						(MMOptionsDlg.resources == 1 && Resources.Count > 0) ||
						(MMOptionsDlg.effort == 1 && HasEffort));
				}
				else
				{
					return
                        (MMOptionsDlg.dates == 11 && !HasDueDate) ||
                        (MMOptionsDlg.dates == 11 && !HasStartDate) ||
                        (MMOptionsDlg.progress == 11 && Complete == -1) ||
                        (MMOptionsDlg.resources == 11 && Resources.Count == 0) ||
                        (MMOptionsDlg.effort == 11 && !HasEffort) ||
						false;
                }
			}
		}

		public bool IsTask
		{
			get
			{
				return !TaskIsEmpty && !IsRollUp && !IsInRollupBranch;
			}
		}

		public bool Milestone
		{
			get
			{
				if (m_root == null)
					return false;
				try
				{
					XmlNode _task = m_root.SelectSingleNode("ap:Task", NSManager);
					if (_task == null)
						return false;
					XmlAttribute _milestoneAttr = _task.Attributes["Milestone"];
					if (_milestoneAttr == null)
						return false;
					return Convert.ToBoolean(_milestoneAttr.Value);
				}
				catch
				{
					return false;
				}
			}
		}

		/// <summary>
		/// Gets or sets task General Costs. Returns rounded integral part
		/// </summary>
		public override double GeneralCost
		{
			get
			{
				if (m_root == null)
					return 0;
				try
				{
					XmlNode _task = m_root.SelectSingleNode("ap:Task", NSManager);
					if (_task == null)
						return 0;
					XmlAttribute _costAttr = _task.Attributes["GeneralCost"];
					if (_costAttr == null)
						return 0;
					return Convert.ToDouble(_costAttr.Value.TrimEnd('.'));
				}
				catch
				{
					return 0;
				}
			}
			set
			{
				throw new Exception("XMLTopicCompanion:GeneralCost.Set not implemented.");
			}
		}

		/// <summary>
		/// Gets or sets task Resources Costs
		/// </summary>
		public override double ResourcesCost
		{
			get
			{
				if (m_root == null)
					return 0;
				try
				{
					XmlNode _task = m_root.SelectSingleNode("ap:Task", NSManager);
					if (_task == null)
						return 0;
					XmlAttribute _costAttr = _task.Attributes["ResourcesCost"];
					if (_costAttr == null)
						return 0;
					return Convert.ToDouble(_costAttr.Value.TrimEnd('.'));
				}
				catch
				{
					return 0;
				}
			}
			set
			{
				throw new Exception("XMLTopicCompanion:ResourcesCost.Set not implemented.");
			}
		}

		/// <summary>
		/// Gets or sets task Total Costs
		/// </summary>
		public override double TotalCost
		{
			get
			{
				if (m_root == null)
					return 0;
				try
				{
					XmlNode _task = m_root.SelectSingleNode("ap:Task", NSManager);
					if (_task == null)
						return 0;
					XmlAttribute _costAttr = _task.Attributes["TotalCost"];
					if (_costAttr == null)
						return 0;
					return Convert.ToDouble(_costAttr.Value.TrimEnd('.'));
				}
				catch
				{
					return 0;
				}
			}
			set
			{
				throw new Exception("XMLTopicCompanion:TotalCost.Set not implemented.");
			}
		}

		/// <summary>
		/// Gets or sets task Progress
		/// </summary>
		public override int Complete
		{
			get
			{
				if (m_root == null)
					return -1;
				try
				{
					XmlNode _task = m_root.SelectSingleNode("ap:Task", NSManager);
					if (_task == null)
						return -1;
					XmlAttribute _completeAttr = _task.Attributes["TaskPercentage"];
					if (_completeAttr == null)
						return -1;
					return Convert.ToInt32(_completeAttr.Value);
				}
				catch
				{
					return -1;
				}
			}
			set
			{
				throw new Exception("XMLTopicCompanion:Complete.Set not implemented.");
			}
		}


		/// <summary>
		/// Gets task's start date
		/// </summary>
		/// <exception cref="System.Exception">XMLTopicCompanion:StartDate.Set not implemented.</exception>
		public override DateTime StartDate
		{
			get
			{
				if (m_root == null)
					return MMUtils.NULLDATE;
				try
				{
					XmlNode _task = m_root.SelectSingleNode("ap:Task", NSManager);
					if (_task == null)
						return MMUtils.NULLDATE;
					XmlAttribute _startDateAttr = _task.Attributes["StartDate"];
					if (_startDateAttr == null)
						return MMUtils.NULLDATE;
					return Convert.ToDateTime(_startDateAttr.Value);
				}
				catch
				{
					return MMUtils.NULLDATE;
				}
			}
			set
			{
				throw new Exception("XMLTopicCompanion:StartDate.Set not implemented.");
			}
		}

		/// <summary>
		/// Gets task due date
		/// </summary>
		/// <exception cref="System.Exception">XMLTopicCompanion:DueDate.Set not implemented.</exception>
		public override DateTime DueDate
		{
			get
			{
				if (m_root == null)
					return MMUtils.NULLDATE;
				try
				{
					XmlNode _task = m_root.SelectSingleNode("ap:Task", NSManager);
					if (_task == null)
						return MMUtils.NULLDATE;
					XmlAttribute _dueDateAttr = _task.Attributes["DeadlineDate"];
					if (_dueDateAttr == null)
						return MMUtils.NULLDATE;
					return Convert.ToDateTime(_dueDateAttr.Value);
				}
				catch
				{
					return MMUtils.NULLDATE;
				}
			}
			set
			{
				throw new Exception("XMLTopicCompanion:DueDate.Set not implemented.");
			}
		}

		/// <summary>
		/// Converts xml-string representation of duration unit to its MindManager value.
		/// </summary>
		/// <param name="aStrDurationUnit">an XML value of duration unit.</param>
		/// <returns>MindManager's duration unit</returns>
		public static MmDurationUnit DurationUnitFromString(string aStrDurationUnit)
		{
			switch (aStrDurationUnit)
			{
				case "urn:mindjet:Minute":
					return MmDurationUnit.mmDurationUnitMinute;
				case "urn:mindjet:Hour":
					return MmDurationUnit.mmDurationUnitHour;
				case "urn:mindjet:Day":
					return MmDurationUnit.mmDurationUnitDay;
				case "urn:mindjet:Week":
					return MmDurationUnit.mmDurationUnitWeek;
				case "urn:mindjet:Month":
					return MmDurationUnit.mmDurationUnitMonth;
				default:
					return 0;
			}
		}

		public static MmRateUnit RateUnitFromString(string aStrRateUnit)
		{
			switch (aStrRateUnit)
			{
				case "hour":
					return MmRateUnit.mmRateUnitHour;
				case "day":
					return MmRateUnit.mmRateUnitDay;
				case "week":
					return MmRateUnit.mmRateUnitWeek;
				case "usage":
					return MmRateUnit.mmRateUnitUsage;
				default:
					return 0;
			}
		}

		/// <summary>
		/// Gets task duration unit
		/// </summary>
		/// <exception cref="System.Exception">XMLTopicCompanion:DurationUnit.Set not implemented.</exception>
		public override MmDurationUnit DurationUnit
		{
			get
			{
				if (m_root == null)
					return 0;
				try
				{
					XmlNode _task = m_root.SelectSingleNode("ap:Task", NSManager);
					if (_task == null)
						return 0;
					XmlAttribute _durationUnitAttr = _task.Attributes["DurationUnit"];
					if (_durationUnitAttr == null)
						return 0;
					return DurationUnitFromString(_durationUnitAttr.Value);
				}
				catch
				{
					return 0;
				}
			}
			set
			{
				throw new Exception("XMLTopicCompanion:DurationUnit.Set not implemented.");
			}
		}

		/// <summary>
		/// Gets task duration
		/// </summary>
		/// <exception cref="System.Exception">XMLTopicCompanion:Duration.Set not implemented.</exception>
		public override int Duration
		{
			get
			{
				if (m_root == null)
					return 0;
				try
				{
					XmlNode _task = m_root.SelectSingleNode("ap:Task", NSManager);
					if (_task == null)
						return -1;
					XmlAttribute _durationUnitAttr = _task.Attributes["DurationUnit"];
					if (_durationUnitAttr == null)
						return -1;
					MmDurationUnit _durationUnit = DurationUnitFromString(_durationUnitAttr.Value);
					if (_durationUnit == 0)
						return -1;
					switch (_durationUnit)
					{
						case MmDurationUnit.mmDurationUnitMinute:
							return Convert.ToInt32(_task.Attributes["DurationMinutes"].Value);
						case MmDurationUnit.mmDurationUnitHour:
							return Convert.ToInt32(_task.Attributes["DurationHours"].Value);
						case MmDurationUnit.mmDurationUnitDay:
							return Convert.ToInt32(_task.Attributes["DurationHours"].Value) /
                                doc.Calendar.WorkHours;
						case MmDurationUnit.mmDurationUnitWeek:
							return Convert.ToInt32(_task.Attributes["DurationHours"].Value) /
								(doc.Calendar.WorkHours * Document.Calendar.NumberOfWorkdays);
						case MmDurationUnit.mmDurationUnitMonth:
							return Convert.ToInt32(_task.Attributes["DurationHours"].Value) /
								(doc.Calendar.WorkHours * Document.Calendar.NumberOfWorkdays * 4);
						default:
							return -1;
					}
				}
				catch
				{
					return 0;
				}
			}
			set
			{
				throw new Exception("XMLTopicCompanion:Duration.Set not implemented.");
			}
		}

        /// <summary>
		/// True if Topic has Duration set and used
		/// </summary>
		public bool HasDuration
        {
            get
            {
                if (m_root == null)
                    return false;
                try
                {
                    XmlNode _task = m_root.SelectSingleNode("ap:Task", NSManager);
                    if (_task == null)
                        return false;
                    XmlAttribute _DurationUnitAttr = _task.Attributes["DurationUnit"];
                    return _DurationUnitAttr != null;
                }
                catch
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// True if Topic has Effort set and used
        /// </summary>
        public override bool HasEffort
		{
			get
			{
				if (m_root == null)
					return false;
				try
				{
					XmlNode _task = m_root.SelectSingleNode("ap:Task", NSManager);
					if (_task == null)
						return false;
					XmlAttribute _EffortUnitAttr = _task.Attributes["EffortUnit"];
					return _EffortUnitAttr != null;
				}
				catch
				{
					return false;
				}
			}
		}

		/// <summary>
		/// Gets Effort unit
		/// </summary>
		/// <exception cref="System.Exception">XMLTopicCompanion:EffortUnit.Set not implemented.</exception>
		public override MmDurationUnit EffortUnit
		{
			get
			{
				if (m_root == null)
					return 0;
				try
				{
					XmlNode _task = m_root.SelectSingleNode("ap:Task", NSManager);
					if (_task == null)
						return 0;
					XmlAttribute _effortUnitAttr = _task.Attributes["EffortUnit"];
					if (_effortUnitAttr == null)
						return 0;
					return DurationUnitFromString(_effortUnitAttr.Value);
				}
				catch
				{
					return 0;
				}
			}
			set
			{
				throw new Exception("XMLTopicCompanion:EffortUnit.Set not implemented.");
			}
		}

		/// <summary>
		/// Gets Effort value
		/// </summary>
		/// <exception cref="System.Exception">XMLTopicCompanion:Effort.Set not implemented.</exception>
		public override int Effort
		{
			get
			{
				if (m_root == null)
					return 0;
				try
				{
					XmlNode _task = m_root.SelectSingleNode("ap:Task", NSManager);
					if (_task == null)
						return -1;
					XmlAttribute _effortUnitAttr = _task.Attributes["EffortUnit"];
					if (_effortUnitAttr == null)
						return -1;
					MmDurationUnit _durationUnit = DurationUnitFromString(_effortUnitAttr.Value);
					if (_durationUnit == 0)
						return -1;
					switch (_durationUnit)
					{
						case MmDurationUnit.mmDurationUnitMinute:
							return Convert.ToInt32(_task.Attributes["EffortMinutes"].Value);
						case MmDurationUnit.mmDurationUnitHour:
							return Convert.ToInt32(_task.Attributes["EffortMinutes"].Value) / 60;
						case MmDurationUnit.mmDurationUnitDay:
							return Convert.ToInt32(_task.Attributes["EffortMinutes"].Value) /
								(doc.Calendar.WorkHours * 60);
						case MmDurationUnit.mmDurationUnitWeek:
							return Convert.ToInt32(_task.Attributes["EffortMinutes"].Value) /
								(doc.Calendar.WorkHours * Document.Calendar.NumberOfWorkdays * 60);
						case MmDurationUnit.mmDurationUnitMonth:
							return Convert.ToInt32(_task.Attributes["EffortMinutes"].Value) /
								(doc.Calendar.WorkHours * Document.Calendar.NumberOfWorkdays * 240);
						default:
							return -1;
					}
				}
				catch { return -1; }
			}
			set
			{
				throw new Exception("XMLTopicCompanion:Effort.Set not implemented.");
			}
		}

		/// <summary>
		/// Converts string XML task priority representation to MM enum value
		/// </summary>
		/// <param name="aStrTaskPriority">a string representing Task Priority.</param>
		/// <returns></returns>
		public static MmTaskPriority TaskPriorityFromString(String aStrTaskPriority)
		{
			switch (aStrTaskPriority)
			{
				case "urn:mindjet:Prio1":
					return MmTaskPriority.mmTaskPriority1;
				case "urn:mindjet:Prio2":
					return MmTaskPriority.mmTaskPriority2;
				case "urn:mindjet:Prio3":
					return MmTaskPriority.mmTaskPriority3;
				case "urn:mindjet:Prio4":
					return MmTaskPriority.mmTaskPriority4;
				case "urn:mindjet:Prio5":
					return MmTaskPriority.mmTaskPriority5;
				case "urn:mindjet:Prio6":
					return MmTaskPriority.mmTaskPriority6;
				case "urn:mindjet:Prio7":
					return MmTaskPriority.mmTaskPriority7;
				case "urn:mindjet:Prio8":
					return MmTaskPriority.mmTaskPriority8;
				case "urn:mindjet:Prio9":
					return MmTaskPriority.mmTaskPriority9;
				default:
					return MmTaskPriority.mmTaskPriorityNone;
			}
		}

		/// <summary>
		/// Gets task Priority
		/// </summary>
		/// <exception cref="System.Exception">XMLTopicCompanion:Priority.Set not implemented.</exception>
		public override MmTaskPriority Priority
		{
			get
			{
				if (m_root == null)
					return MmTaskPriority.mmTaskPriorityNone;
				try
				{
					XmlNode _task = m_root.SelectSingleNode("ap:Task", NSManager);
					if (_task == null)
						return MmTaskPriority.mmTaskPriorityNone;
					XmlAttribute _taskPriorityAttr = _task.Attributes["TaskPriority"];
					if (_taskPriorityAttr == null)
						return MmTaskPriority.mmTaskPriorityNone;
					return TaskPriorityFromString(_taskPriorityAttr.Value);
				}
				catch
				{
					return MmTaskPriority.mmTaskPriorityNone;
				}
			}
			set
			{
				throw new Exception("XMLTopicCompanion:Priority.Set not implemented.");
			}
		}

		/// <summary>
		/// Gets a value indicating whether Topic is rollup topic.
		/// </summary>
		/// <value>
		/// <c>true</c> if Topic is root rollup topic; otherwise, <c>false</c>.
		/// </value>
		public override bool IsRollUp
		{
			get
			{
				if (m_root == null)
					return false;
				try
				{
                    var _custom = m_root.SelectNodes("cor:Custom", NSManager); // topic can have multiple Custom nodes
                    if (_custom == null)
						return false;
					else
					{
						foreach (XmlNode _node in _custom)
						{
                            XmlAttribute uri = _node.Attributes["Uri"];
							if (uri != null && uri.Value == "http://schemas.iaresearch.com/JCVGantt")
							{
								XmlAttribute rollupAttr = _node.Attributes["cst2:Filtered"];
								if (rollupAttr == null)
                                    rollupAttr = _node.Attributes["cst0:Filtered"];
                                if (rollupAttr != null && rollupAttr.Value.ToUpper() == "FALSE")
									return true;
							}
						}
                        return false;
                    }
				}
				catch
				{
					return false;
				}
			}
		}

        /// <summary>
        /// Gets a value indicating whether a topic contains a task rollup calculation but is not the root rollup topic.
        /// </summary>
        /// <value>
        /// <c>true</c> if Topic is calculated topic; otherwise, <c>false</c>.
        /// </value>
        public override bool IsInRollupBranch
        {
            get
            {
                if (m_root == null)
                    return false;
                try
                {
                    var _custom = m_root.SelectNodes("cor:Custom", NSManager); // topic can have multiple Custom nodes
                    if (_custom == null)
                        return false;
                    else
                    {
                        foreach (XmlNode _node in _custom)
                        {
                            XmlAttribute uri = _node.Attributes["Uri"];
                            if (uri != null && uri.Value == "http://schemas.iaresearch.com/JCVGantt")
                            {
                                XmlAttribute attrCalculated = _node.Attributes["cst2:CalculatedTask"]; // intermedia rollup
                                if (attrCalculated == null)
                                    attrCalculated = _node.Attributes["cst0:CalculatedTask"];
                                if (attrCalculated != null && attrCalculated.Value.ToUpper() == "TRUE")
                                    return true;
                            }
                        }
                        return false;
                    }
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool IsFormula
		{
			get
			{
				if (m_root == null)
					return false;
				try
				{
					XmlNode _task = m_root.SelectSingleNode
						("ap:BusinessDataGroup/ap:CustomPropertiesBusinessData/ap:CustomPropertyGroup/ap:CustomProperty[@IsFormulaTarget='true']", NSManager);
					return (_task != null);
				}
				catch
				{
					return false;
				}
			}
		}

        /// <summary>
        /// Get a collection of text labels
        /// </summary>
        /// <param name="tagGroups">with group ID and group name</param>
        public List<Dashboard.TagsContainer> TextLabels(Dictionary<string, string> tagGroups)
		{
			//get
			{
                textLabels = new List<Dashboard.TagsContainer>();
				if (m_root == null)
					return textLabels;
				try
				{
					XmlNodeList _textLabelNodes = m_root.SelectNodes("ap:TextLabels/ap:TextLabel", NSManager);
					if (_textLabelNodes == null)
						return textLabels;

					foreach (XmlNode _textLabelNode in _textLabelNodes)
					{
						XmlAttribute _textLabelNameAttr = _textLabelNode.Attributes["TextLabelName"];
                        XmlAttribute _textLabelGroupIdAttr = _textLabelNode.Attributes["SetId"];
                        if (null == _textLabelNameAttr || null == _textLabelGroupIdAttr)
                            continue;

                        string groupName = "";
                        tagGroups.TryGetValue(_textLabelGroupIdAttr.Value, out groupName);

                        Dashboard.TagsContainer item = new Dashboard.TagsContainer(groupName, _textLabelNameAttr.Value);
                        if (!textLabels.Contains(item))
							textLabels.Add(item);
					}
                }
				catch
				{
				}
				return textLabels;
			}
		}

        public List<Dashboard.PropertiesContainer> TopicProperties
        {
            get
            {
                List<Dashboard.PropertiesContainer> Properties = new List<Dashboard.PropertiesContainer>();
                if (m_root == null)
                    return Properties;
                try
                {
                    XmlNodeList _textPropertyNodes = m_root.SelectNodes("ap:BusinessDataGroup/ap:CustomPropertiesBusinessData/ap:CustomPropertyGroup/ap:CustomProperty", NSManager);
                    XmlNodeList _textLabelNodes = m_root.SelectNodes("ap:TextLabels/ap:TextLabel", NSManager);
                    if (_textPropertyNodes == null)
                        return Properties;

                    foreach (XmlNode _textPropertyNode in _textPropertyNodes)
                    {
                        // Property's name
                        XmlAttribute _name = _textPropertyNode.Attributes["CustomPropertyName"];
                        if (_name == null || _name.Value == "")
                            continue; ;

                        string format = "", value = "";
                        foreach (XmlNode _node in _textPropertyNode.ChildNodes)
                        {
                            if (_node.Name == "ap:CustomPropertyValue")
                            {
                                XmlAttribute _type = _node.Attributes["Type"];
                                XmlAttribute _format = _node.Attributes["Format"];
                                if (_type == null || _format == null)
                                    break;

                                if (_type.Value == "urn:mindjet:Text")
                                {
                                    //formats: Text, MultiLineText, List, MaskedField, FileLink
                                    if (_format.Value == "urn:mindjet:Text" || 
                                        _format.Value == "urn:mindjet:MultiLineText" ||
                                        _format.Value == "urn:mindjet:List")
                                    {
                                        XmlAttribute _text = _node.Attributes["Text"];
                                        if (_text != null)
                                        {
                                            value = _text.Value;
                                            format = "text";
                                        }
                                    }
                                }
                                else if (_type.Value == "urn:mindjet:Number" || _type.Value == "urn:mindjet:Integer")
                                {
                                    XmlAttribute _value = null;
                                    //formats: Real, Currency, Percentage, Integer, Boolean
                                    if (_format.Value == "urn:mindjet:Real")
                                    {
                                        _value = _node.Attributes["Number"];
                                        format = "number";
                                    }
                                    else if (_format.Value == "urn:mindjet:Currency")
                                    {
                                        _value = _node.Attributes["Number"];
                                        format = "currency";
                                    }
                                    else if (_format.Value == "urn:mindjet:Integer")
                                    {
                                        _value = _node.Attributes["Integer"];
                                        format = "number";
                                    }
                                    else
                                        _value = null;

                                    if (_value != null)
                                        value = _value.Value;
                                    else
                                        format = "";
                                }
                                Properties.Add(new Dashboard.PropertiesContainer(_name.Value, value, value, format));
                            }
                        }
                    }
                }
                catch
                {
                }
                return Properties;
            }
        }

		/// <summary>
		/// Loads the resources from the underlying topic.
		/// </summary>
		public override void LoadResources()
		{
			if (null == m_root)
				return;

			m_resources.Clear();
			try
			{
				XmlNode _task = m_root.SelectSingleNode("ap:Task", NSManager);
				if (_task == null)
					return;
				XmlAttribute _taskResources = _task.Attributes["Resources"];
				if (_taskResources == null)
					return;

				string[] _resources = _taskResources.Value.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
				int i = 2;
				foreach (string _resource in _resources)
				{
					m_resources.Add(i++, _resource.Trim());
				}
			}
			catch
			{
				return;
			}
		}

		/// <summary>
		/// Add new single resource
		/// </summary>
		/// <param name="aResource">Resource to add</param>
		/// <param name="aPosition">Position for added resource</param>
		public override void AddResource(string aResource, int aPosition = 99999)
		{
			if (m_root == null)
				return;
			base.AddResource(aResource, aPosition);
			String _strRes = "";
			foreach (String _res in m_resources.Values)
			{
				if (_strRes != "")
					_strRes += ",";
				_strRes += _res;
			}
			XmlNode _task = m_root.SelectSingleNode("ap:Task", NSManager);
			if (_task == null)
				return;
			XmlAttribute _taskResources = _task.Attributes["Resources"];
			if (_taskResources == null)
				XMLAddAttribute(_task, "Resources", _strRes);
			else
				_taskResources.Value = _strRes;
		}

		/// <summary>
		/// Gets all icons.
		/// </summary>
		/// <value>
		/// All icons.
		/// </value>
		public virtual IconList AllIcons
		{
			get
			{
				if (!m_iconsLoaded && null != m_root)
				{
					m_icons.Clear();
					XmlNodeList _stockIcons = m_root.SelectNodes("ap:IconsGroup/ap:Icons/ap:Icon", NSManager);
					if (_stockIcons != null)
					{
						foreach (XmlNode _stockIconNode in _stockIcons)
						{
							XmlAttribute _stockIconId = _stockIconNode.Attributes["IconType"];
							if (null != _stockIconId)
								m_icons.Add(new IconCompanion(_stockIconId.Value));
						}
					}
					XmlNodeList _customIcons = m_root.SelectNodes("ap:IconsGroup/ap:CustomIconImageData", NSManager);
					if (_customIcons != null)
					{
						foreach (XmlNode _customIconNode in _customIcons)
						{
							XmlAttribute _customIconId = _customIconNode.Attributes["IconSignature"];
							if (null != _customIconId)
								m_icons.Add(new IconCompanion(_customIconId.Value));
						}
					}
					m_iconsLoaded = true;
				}
				return m_icons;
			}
		}

		/// <summary>
		/// Determines whether underlying topic contains given icon.
		/// </summary>
		/// <param name="aIcon">a icon.</param>
		/// <returns></returns>
		public override bool HasIcon(IconCompanion aIcon)
		{
			return AllIcons.HasIcon(aIcon);
		}

		/// <summary>
		/// Gets topic Notes
		/// </summary>
		/// <exception cref="System.Exception">XMLTopicCompanion:Notes.Set not implemented.</exception>
		public override string Notes
		{
			get
			{
				if (m_root == null)
					return "";
				try
				{
					XmlNode _notes = m_root.SelectSingleNode("ap:NotesGroup/ap:NotesXhtmlData", NSManager);
                    if (_notes == null)
						return "";
					XmlAttribute _previewTextAttr = _notes.Attributes["PreviewPlainText"];
					return (null != _previewTextAttr) ? _previewTextAttr.Value.Replace("<br>", "\n") : "";
				}
				catch { return ""; }
			}
			set
			{
				throw new Exception("XMLTopicCompanion:Notes.Set not implemented.");
			}
		}

        /// <summary>
        /// Gets plain topic Notes from XHTMLNotes
        /// </summary>
        public override string NotesHtml
        {
            get
            {
                if (m_root == null)
                    return "";
                try
                {
                    XmlNode _notes = m_root.SelectSingleNode("ap:NotesGroup/ap:NotesXhtmlData", NSManager);
					if (null == _notes) return "";
					return HtmlToPlainText(_notes.InnerXml);
                }
                catch { return ""; }
            }
        }

        static string HtmlToPlainText(string html)
        {
            string block = "address|article|aside|blockquote|canvas|dd|div|dl|dt|" +
              "fieldset|figcaption|figure|footer|form|h\\d|header|hr|li|main|nav|" +
              "noscript|ol|output|p|pre|section|table|tfoot|ul|video";

            // Remove the first <p> to avoid the \r\n in the begin of text
            var regex = new Regex(Regex.Escape("<p>"));
            string buf = regex.Replace(html, "", 1);

            string patNestedBlock = $"(\\s*?</?({block})[^>]*?>)+\\s*";
            buf = Regex.Replace(buf, patNestedBlock, "\n\n", RegexOptions.IgnoreCase);

            // Replace br tag to newline.
            buf = Regex.Replace(buf, @"<(br)[^>]*>", "\n", RegexOptions.IgnoreCase);

            // (Optional) remove styles and scripts.
            buf = Regex.Replace(buf, @"<(script|style)[^>]*?>.*?</\1>", "", RegexOptions.Singleline);

            // Remove all tags.
            buf = Regex.Replace(buf, @"<[^>]*(>|$)", "", RegexOptions.Multiline);

            // Replace HTML entities.
            buf = WebUtility.HtmlDecode(buf);
            return buf;
        }

        /// <summary>
        /// Gets the topic's Subtopics.
        /// </summary>
        /// <value>
        /// The Subtopics collection.
        /// </value>
        public virtual List<XMLTopicCompanion> SubTopics
		{
			get
			{
				List<XMLTopicCompanion> _subtopics = new List<XMLTopicCompanion>();
				if (m_root != null)
				{
					XmlNodeList _subNodes = m_root.SelectNodes("ap:SubTopics/ap:Topic", NSManager);
					if (_subNodes != null)
					{
						foreach (XmlNode _subNode in _subNodes)
							_subtopics.Add(new XMLTopicCompanion(_subNode, NSManager, m_xmlDocument));
					}
				}
				return _subtopics;
			}
		}

		/// <summary>
		/// Gets the callout topics, or floationg topics if called on Central topic.
		/// </summary>
		public virtual List<XMLTopicCompanion> Callouts
		{
			get
			{
				List<XMLTopicCompanion> _subtopics = new List<XMLTopicCompanion>();
				if (m_root != null)
				{
					XmlNodeList _subNodes = m_root.SelectNodes("ap:FloatingTopics/ap:Topic", NSManager);
					if (_subNodes != null)
					{
						foreach (XmlNode _subNode in _subNodes)
							_subtopics.Add(new XMLTopicCompanion(_subNode, NSManager, m_xmlDocument));
					}
				}
				return _subtopics;
			}
		}

		/// <summary>
		/// Gets the hyperlinks.
		/// </summary>
		/// <value>
		/// The hyperlinks.
		/// </value>
		public virtual HashSet<XMLHyperlinkCompanion> Hyperlinks
		{
			get
			{
				if (m_hyperLinks == null && m_root != null)
				{
					m_hyperLinks = new HashSet<XMLHyperlinkCompanion>();
                    string _rootPath = m_xmlDocument == null ? "" : m_xmlDocument.Path;

                    XmlNodeList _hyperlinkNodes = m_root.SelectNodes("ap:Hyperlink", NSManager);
					foreach (XmlNode _hyperlinkNode in _hyperlinkNodes)
					{
						if (_hyperlinkNode != null)
						{
							
							XmlNode _attrURL = _hyperlinkNode.Attributes["Url"];
							string _attrTitle = "";
                            try
                            {
                                _attrTitle = _hyperlinkNode.Attributes["CachedName"].Value;
								if (_attrTitle == "" || _attrTitle == "\\")
									_attrTitle = _hyperlinkNode.Attributes["Title"].Value;
								if (_attrTitle == "\\")
									_attrTitle = "";
                            }
                            catch { }
                            if (_attrURL != null)
								m_hyperLinks.Add(new XMLHyperlinkCompanion(_attrURL.Value, _rootPath, _attrTitle));
						}
					}

					_hyperlinkNodes = m_root.SelectNodes("ap:HyperlinkGroup/ap:IndexedHyperlink", NSManager);
					foreach (XmlNode _hyperlinkNode in _hyperlinkNodes)
					{
						if (_hyperlinkNode != null)
						{
							XmlNode _attrURL = _hyperlinkNode.Attributes["Url"];
							if (_attrURL != null)
								m_hyperLinks.Add(new XMLHyperlinkCompanion(_attrURL.Value, _rootPath));
						}
					}
                    _hyperlinkNodes = m_root.SelectNodes("ap:AttachmentGroup/ap:AttachmentData", NSManager);
                    foreach (XmlNode _hyperlinkNode in _hyperlinkNodes)
                    {
                        if (_hyperlinkNode != null)
                        {
                            XmlNode _attrURL = _hyperlinkNode.Attributes["FileName"];
                            if (_attrURL != null)
                                m_hyperLinks.Add(new XMLHyperlinkCompanion("attachment:" + _attrURL.Value, _rootPath));
                        }
                    }
				}
				return m_hyperLinks;
			}
		}

		/// <summary>
		/// Creates a new hyperlink XML node.
		/// </summary>
		/// <returns>Created node</returns>
		public XmlNode CreateNewHyperlinkNode()
		{
			if (m_root == null)
				return null;
			XmlNode _n = m_root.SelectSingleNode("ap:Hyperlink", NSManager);
			XmlNode _rc = null;
			if (_n == null)
			{
				// Create "Hyperlink" node
				XmlElement _newnode = m_root.OwnerDocument.CreateElement("ap:Hyperlink", "http://schemas.mindjet.com/MindManager/Application/2003");
				_rc = XMLMapCompanion.XMLInsertChildNode(m_root, _newnode, m_nodeOrder);
			}
			else
			{
				XmlElement _hyperlinkGroup = null;
				foreach (XmlNode _node in m_root.ChildNodes)
				{
					if (_node.Name == "ap:HyperlinkGroup")
					{
						_hyperlinkGroup = _node as XmlElement;
						break;
					}
				}
				if (_hyperlinkGroup == null)
				{
					_hyperlinkGroup = m_root.OwnerDocument.CreateElement("ap:HyperlinkGroup", "http://schemas.mindjet.com/MindManager/Application/2003");
					_hyperlinkGroup = XMLMapCompanion.XMLInsertChildNode(m_root, _hyperlinkGroup, m_nodeOrder) as XmlElement;
				}
				
				// Compute new index
				XmlNodeList _attrs = _hyperlinkGroup.SelectNodes("ap:IndexedHyperlink", NSManager);
				int _index = -1;
				foreach (XmlNode _node in _attrs)
				{
					if (_node.Attributes["Index"] != null)
					{
						int _thisIndex = -1;
						try
						{
							_thisIndex = Convert.ToInt32(_node.Attributes["Index"].Value);
						} catch {}
						if (_thisIndex > _index)
							_index = _thisIndex;
					}
				}
				_index++;

				XmlElement _newnode = m_root.OwnerDocument.CreateElement("ap:IndexedHyperlink", "http://schemas.mindjet.com/MindManager/Application/2003");
				_rc = _hyperlinkGroup.AppendChild(_newnode);
				XMLAddAttribute(_rc, "Index", _index.ToString());
				XMLAddAttribute(_rc, "HyperlinkId", MMUtils.NewMMGuid());
			}

			if (_rc != null)
			{
				XMLAddAttribute(_rc, "HyperlinkSourceHandling", "urn:mindjet:CopySource");
				XMLAddAttribute(_rc, "HyperlinkType", "urn:mindjet:Unknown");
				XMLAddAttribute(_rc, "Name", "");
				XMLAddAttribute(_rc, "Hidden", "false");
				XMLAddAttribute(_rc, "Target", "");
				XMLAddAttribute(_rc, "Url", "");
				XMLAddAttribute(_rc, "Absolute", "false");
				XMLAddAttribute(_rc, "CachedName", "");
				XMLAddAttribute(_rc, "CachedDocumentExtension", "");
				XMLAddAttribute(_rc, "Title", "");
			}

			return _rc;
		}

		/// <summary>
		/// Creates the hyperlink to the topic.
		/// </summary>
		/// <param name="aTarget">Target topic.</param>
		/// <param name="bAbsolute">if set to <c>true</c> indicates that Hyperlink should be marked as Absolute.</param>
		/// <returns>true if hyperlink was successfully created</returns>
		public bool CreateHyperlink(XMLTopicCompanion aTarget, bool bAbsolute = false, string title = "")
		{
			if (aTarget == null)
				return false;
			if (aTarget.XMLDocument.Path == "" && bAbsolute)
				return false;
			XmlNode _hyperlink = CreateNewHyperlinkNode();
			if (_hyperlink == null)
				return false;

			if (title == "") title = aTarget.Text;

			if (aTarget.XMLDocument.Path.StartsWith("https"))
			{
				_hyperlink.Attributes["Url"].Value = aTarget.XMLDocument.Path
					+ "&topicid=" + MMUtils.MindManager.Utilities.GuidBase64ToRegistry(aTarget.Guid)
					.Replace("{", "").Replace("}", "").ToLower();
			}
            else if (aTarget.XMLDocument.Path.StartsWith("mj-") && !aTarget.XMLDocument.Path.StartsWith("mj-map"))
            { // link to cloud map or map topic / link to local topic
				_hyperlink.Attributes["Url"].Value = "\"" + aTarget.XMLDocument.Path +
					"#xpointer(/descendant-or-self::ap:Topic[@OId='" + aTarget.Guid + "'])\"";
			}
            // link to topic in the same map... \ for Mac path, \\ for server path
			else if (! (aTarget.XMLDocument.Path.Contains(":") || aTarget.XMLDocument.Path.StartsWith("\\")))
			{
				_hyperlink.Attributes["Url"].Value =
					"#xpointer(/descendant-or-self::ap:Topic[@OId='" + aTarget.Guid + "'])";
			}
            // link to topic in the other map
            else
            {
				_hyperlink.Attributes["Url"].Value = "\"" + aTarget.XMLDocument.Path +
					"#xpointer(/descendant-or-self::ap:Topic[@OId='" + aTarget.Guid + "'])\"";
 			}

			_hyperlink.Attributes["Absolute"].Value = bAbsolute ? "true" : "false";
			_hyperlink.Attributes["CachedName"].Value = title; // Hyperlink title
			m_hyperLinks = null;
			return true;
		}

		public void CreateHyperlink(string path = "", string title = "")
        {
			XmlNode _hyperlink = CreateNewHyperlinkNode();
			if (_hyperlink == null)
				return;

			_hyperlink.Attributes["Url"].Value = path;
			if (title != "")
				_hyperlink.Attributes["Title"].Value = title;
            m_hyperLinks = null;
		}

		/// <summary>
		/// Gets the attribute.
		/// </summary>
		/// <param name="aAttrNS">Attribute namespace.</param>
		/// <param name="aAttrName">Name of an attribute.</param>
		/// <param name="aDefValue">Default attribute value.</param>
		/// <returns></returns>
		public override string GetAttribute(string aAttrNS, string aAttrName, string aDefValue = "")
		{
			if (m_root == null)
				return aDefValue;
			XmlNamespaceManager _manager = null;
			try
			{
				_manager = new XmlNamespaceManager(m_root.OwnerDocument.NameTable);
				_manager.AddNamespace("xsd", "http://www.w3.org/2001/XMLSchema");
				_manager.AddNamespace("ap", "http://schemas.mindjet.com/MindManager/Application/2003");
				_manager.AddNamespace("pri", "http://schemas.mindjet.com/MindManager/Primitive/2003");
				_manager.AddNamespace("cor", "http://schemas.mindjet.com/MindManager/Core/2003");
				_manager.AddNamespace("xsi", "http://www.w3.org/2001/XMLSchema-instance");
				_manager.AddNamespace("cst0", aAttrNS);

			}
			catch
			{
				return aDefValue;
			}
			XmlNode _attr = m_root.SelectSingleNode("cor:Custom[@Uri='" + aAttrNS + "']/@cst0:" + aAttrName, _manager);
			return _attr == null ? aDefValue : _attr.Value;
		}

		/// <summary>
		/// Determines whether this Tolic contains an attribute in the specified namespace.
		/// </summary>
		/// <param name="aAttrNS">Namespace</param>
		/// <param name="aAttrName">Name of the attribute.</param>
		/// <returns>true if attribute in given namespace exists; false otherwise</returns>
		public override bool HasAttribute(string aAttrNS, string aAttrName, bool aUseCache = true)
		{
			if (m_root == null)
				return false;
			XmlNamespaceManager _manager = null;
			try
			{
				_manager = new XmlNamespaceManager(m_root.OwnerDocument.NameTable);
				_manager.AddNamespace("xsd", "http://www.w3.org/2001/XMLSchema");
				_manager.AddNamespace("ap",  "http://schemas.mindjet.com/MindManager/Application/2003");
				_manager.AddNamespace("pri", "http://schemas.mindjet.com/MindManager/Primitive/2003");
				_manager.AddNamespace("cor", "http://schemas.mindjet.com/MindManager/Core/2003");
				_manager.AddNamespace("xsi", "http://www.w3.org/2001/XMLSchema-instance");
				_manager.AddNamespace("cst0", aAttrNS);

			}
			catch
			{
				return false;
			}
			XmlNode _attr = m_root.SelectSingleNode("cor:Custom[@Uri='" + aAttrNS + "']/@cst0:" + aAttrName, _manager);
			return _attr != null;
		}

		/// <summary>
		/// Sets the new attribute value.
		/// </summary>
		/// <param name="aAttrNS">Attribute namespace</param>
		/// <param name="aAttrName">Name of an attribute</param>
		/// <param name="aAttrValue">Attribute value</param>
		public override void SetAttribute(string aAttrNS, string aAttrName, string aAttrValue)
		{
			XmlNode _attrNode = GetAttributesNode(aAttrNS);
			if (_attrNode == null)
				return;
			XmlAttribute _attrVal = _attrNode.Attributes["cst0:" + aAttrName];
			if (_attrVal != null)
				_attrVal.Value = aAttrValue;
			else
				XMLAddAttribute(_attrNode, "cst0:" + aAttrName, aAttrValue, aAttrNS);
		}

		protected override void Commit()
		{
		}

		/// <summary>
		/// Commits XML to given Topic.
		/// </summary>
		/// <param name="aTopic">a topic to commit XML to.</param>
		/// <returns></returns>
		public bool CommitTo(Topic aTopic)
		{
			if (m_root == null || aTopic == null)
				return false;
			try
			{
				aTopic.Xml = m_root.OuterXml;
			}
			catch (Exception _e)
			{
#if DEBUG
				System.Windows.Forms.MessageBox.Show("Exception: " + _e.Message + "\r\nSource: " + _e.Source + "\r\nStack: " + _e.StackTrace);
#endif
			}

			return true;
		}

		public override int GetHashCode()
		{
			return Guid.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			return (obj != null && obj is XMLTopicCompanion && (obj as XMLTopicCompanion).Guid == Guid);
		}

		protected XmlNamespaceManager NSManager
		{
			get
			{
				if (m_manager == null)
				{
					m_manager = new XmlNamespaceManager(m_root.OwnerDocument.NameTable);
					m_manager.AddNamespace("xsd", "http://www.w3.org/2001/XMLSchema");
					m_manager.AddNamespace("ap", "http://schemas.mindjet.com/MindManager/Application/2003");
					m_manager.AddNamespace("pri", "http://schemas.mindjet.com/MindManager/Primitive/2003");
					m_manager.AddNamespace("cor", "http://schemas.mindjet.com/MindManager/Core/2003");
					m_manager.AddNamespace("xsi", "http://www.w3.org/2001/XMLSchema-instance");
				}
				return m_manager;
			}
		}

		public bool IsOutlookItem()
		{
            XmlElement aXML = m_root as XmlElement;

            if (aXML == null)
				return false;

			foreach (XmlNode _xmlNode in aXML.ChildNodes)
			{
				if (_xmlNode.LocalName.ToUpper() == "BUSINESSDATAGROUP")
				{
                    foreach (XmlNode __xmlNode in _xmlNode.ChildNodes)
					{
						if (__xmlNode.LocalName == "BusinessData")
						{
                            foreach (XmlAttribute attr in __xmlNode.Attributes)
							{
								if (attr.LocalName == "Uri" && attr.Value.StartsWith("http://uri.mindjet.com/Mm6OutlookLinker"))
									return true;
							}
                        }
					}
                }
			}
			return false;
        }

        /// <summary>
        /// Delete given XML node
        /// </summary>
        /// <param name="aXML">Reference of XML</param>
        /// <param name="aNodeNameToRemove">Local Name of the node to remove, case-insensitive</param>
        /// <param name="aAttrName">Remove only the node that has this attribute, case sensitive</param>
        /// <param name="aAttrValue">Remove only the node that has given attribute's value, case sensitive</param>
        /// <returns>Deleted node</returns>
        public static XmlNode XMLDeleteNode(XmlElement aXML, string aNodeNameToRemove, string aAttrName = "", string aAttrValue = "")
		{
			XmlNode _rc = null;
			aNodeNameToRemove = aNodeNameToRemove.ToUpper();
			if (aXML == null)
				return _rc;
			XmlNode _xmlNodeToRemove = null;
			foreach (XmlNode _xmlNode in aXML.ChildNodes)
			{
				if (_xmlNode.LocalName.ToUpper() == aNodeNameToRemove)
				{
					if (aAttrName != "")
					{
						XmlNode _attr = _xmlNode.Attributes.GetNamedItem(aAttrName);
						if (_attr == null || (aAttrValue != "" && _attr.Value != aAttrValue))
							continue;
					}
					_xmlNodeToRemove = _xmlNode;
					break;
				}
			}
			if (_xmlNodeToRemove != null)
			{
				_rc = _xmlNodeToRemove.Clone();
				_xmlNodeToRemove.ParentNode.RemoveChild(_xmlNodeToRemove);
			}
			return _rc;
		}

		public XmlNode XMLDeleteNode(String aNodeNameToRemove, String aAttrName = "", String aAttrValue = "")
		{
			return XMLDeleteNode(m_root as XmlElement, aNodeNameToRemove, aAttrName, aAttrValue);
		}

		public void XMLDeleteNodesByXPath(String aXPath)
		{
			XmlNodeList _nodesToRemove = m_root.SelectNodes(aXPath, NSManager);

			foreach (XmlNode _xmlNode in _nodesToRemove)
				if (_xmlNode.NodeType == XmlNodeType.Attribute)
					(_xmlNode as XmlAttribute).OwnerElement.Attributes.Remove(_xmlNode as XmlAttribute);
				else
				_xmlNode.ParentNode.RemoveChild(_xmlNode);
		}

		/// <summary>
		/// Check if given XML document contains the given Node (top-level only)
		/// </summary>
		/// <param name="aXML">XML to validate</param>
		/// <param name="aNodeNameToFind">Node name to find.</param>
		/// <param name="aAttrName">Name of an attribute (optional).</param>
		/// <param name="aAttrValue">Attribute value (optional).</param>
		/// <returns>true if node exists</returns>
		public static XmlNode XMLHasNode(String aXML, String aNodeNameToFind, String aAttrName = "", String aAttrValue = "")
		{
			aNodeNameToFind = aNodeNameToFind.ToUpper();

			XmlDocument _xmlDoc = new XmlDocument();
			_xmlDoc.LoadXml(aXML);
			XmlElement _xmlElement = _xmlDoc.DocumentElement;
			if (_xmlElement == null)
				return null;

			foreach (XmlNode _xmlNode in _xmlElement.ChildNodes)
			{
				if (_xmlNode.LocalName.ToUpper() == aNodeNameToFind)
				{
					if (aAttrName != "")
					{
						XmlNode _attr = _xmlNode.Attributes.GetNamedItem(aAttrName);
						if (_attr == null || (aAttrValue != "" && _attr.Value != aAttrValue))
							continue;
					}
					return _xmlNode;
				}
			}
			return null;
		}

		/// <summary>
		/// Copy all subtopics from source topic to target topic using XML
		/// </summary>
		/// <param name="aTopicFrom">a Topic to copy from.</param>
		/// <param name="aTopicTo">a Topic to copy to.</param>
		public static void XMLCopySubtopicsTo(Topic aTopicFrom, Topic aTopicTo)
		{
			if (aTopicFrom == null || aTopicTo == null)
				return;
			aTopicTo.AllSubTopics.Xml = aTopicFrom.AllSubTopics.Xml;
		}

		/// <summary>
		/// Copy topic and paste it under the another topic
		/// </summary>
		/// <param name="aTopicCopy"></param>
		/// <param name="aTopicPaste"></param>
        public static void XMLPasteSubtopic(Topic aTopicCopy, Topic aTopicPaste)
		{
			XMLTopicCompanion tc = new XMLTopicCompanion(aTopicCopy);
            XMLTopicCompanion tp = new XMLTopicCompanion(aTopicPaste);
            tp.AddSubTopic(tc, true);

            aTopicPaste.AllSubTopics.Xml = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"no\"?>" + tp.SubtopicsNode.OuterXml;
        }

        public void XMLCopyCalloutsTo(Topic aTopicTo)
		{
			if (aTopicTo == null)
				return;
			XmlNode _callouts = m_root.SelectSingleNode("ap:FloatingTopics", NSManager);
			if (_callouts != null)
				aTopicTo.AllCalloutTopics.Xml = _callouts.OuterXml;
		}

		/// <summary>
		/// Add topic as a subtopic to given topic using XML
		/// </summary>
		/// <param name="aTopicToAddTo">A topic to add to.</param>
		/// <param name="aTopicToAdd">A topic to be added.</param>
		public static void XMLAddSubtopic(Topic aTopicToAddTo, Topic aTopicToAdd)
		{
			if (aTopicToAdd == null || aTopicToAddTo == null)
				return;
			try
			{
				XmlDocument _xmlDocTo = new XmlDocument();
				_xmlDocTo.LoadXml(aTopicToAddTo.AllSubTopics.Xml);
				XmlElement _xmlTo = _xmlDocTo.DocumentElement;

				XmlDocument _xmlDoc = new XmlDocument();
				_xmlDoc.LoadXml(aTopicToAdd.Xml);
				XmlElement _xml = _xmlDoc.DocumentElement;

				XmlNode _importNode = _xmlTo.OwnerDocument.ImportNode(_xml.CloneNode(true), true);
				_xmlTo.AppendChild(_importNode.Clone());

				aTopicToAddTo.AllSubTopics.Xml = _xmlTo.OuterXml;
			}
			catch (Exception _e)
			{
#if DEBUG
				System.Windows.Forms.MessageBox.Show("Exception: " + _e.Message + "\r\nSource: " + _e.Source + "\r\nStack: " + _e.StackTrace);
#endif
			}
		}

		public XmlNode SubtopicsNode
		{
			get
			{
				if (m_root == null)
					return null;
				if (m_root.HasChildNodes)
				{
					foreach (XmlNode _node in m_root.ChildNodes)
					{
						if (_node.Name == "ap:SubTopics")
							return _node;
					}
				}
				// Need to create Subtopics collection
				XmlElement _newnode = m_root.OwnerDocument.CreateElement("ap:SubTopics", "http://schemas.mindjet.com/MindManager/Application/2003");
				return XMLMapCompanion.XMLInsertChildNode(m_root, _newnode, m_nodeOrder);
			}
		}

		public XmlNode GetAttributesNode(String aNamespace)
		{
			if (m_root == null)
				return null;
			if (m_root.HasChildNodes)
			{
				foreach (XmlNode _node in m_root.ChildNodes)
				{
					if (_node.Name == "cor:Custom")
					{
						XmlAttribute _uri = _node.Attributes["Uri"];
						if (_uri != null && _uri.Value == aNamespace)
							return _node;
					}
				}
			}
			// Need to create Subtopics collection
			// Compute new index
			XmlNodeList _attrs = m_root.SelectNodes("cor:Custom", NSManager);
			int _index = -1;
			foreach (XmlNode _node in _attrs)
			{
				if (_node.Attributes["Index"] != null)
				{
					int _thisIndex = -1;
					try
					{
						_thisIndex = Convert.ToInt32(_node.Attributes["Index"].Value);
					} catch {}
					if (_thisIndex > _index)
						_index = _thisIndex;
				}
			}
			_index++;
			XmlElement _newnode = m_root.OwnerDocument.CreateElement("cor:Custom", "http://schemas.mindjet.com/MindManager/Core/2003");
			XMLAddAttribute(_newnode, "Uri", aNamespace);
			XMLAddAttribute(_newnode, "Index", _index.ToString());
			XMLAddAttribute(_newnode, "Dirty", "0000000000000001");
			return m_root.PrependChild(_newnode); // XMLMapCompanion.XMLInsertChildNode(m_root, _newnode, m_nodeOrder);
		}
	
		protected XmlNode TextLabelsNode
		{
			get
			{
				if (m_root == null)
					return null;
				if (m_root.HasChildNodes)
				{
					foreach (XmlNode _node in m_root.ChildNodes)
					{
						if (_node.Name == "ap:TextLabels")
							return _node;
					}
				}
				// Need to create Subtopics collection
				XmlElement _newnode = m_root.OwnerDocument.CreateElement("ap:TextLabels", "http://schemas.mindjet.com/MindManager/Application/2003");
				XMLAddAttribute(_newnode, "Dirty", "0000000000000000");
				return XMLMapCompanion.XMLInsertChildNode(m_root, _newnode, m_nodeOrder);
			}
		}
	
		public static void XMLAddAttribute(XmlNode aNodeToAddAttributeTo, String aAttrName, String aAttrValue, String aSchema = "")
		{
			XmlNode _attr = aNodeToAddAttributeTo.OwnerDocument.CreateNode(XmlNodeType.Attribute, aAttrName, aSchema);
			_attr.Value = aAttrValue;
			aNodeToAddAttributeTo.Attributes.Append(_attr as XmlAttribute);
		}
	
		public XMLTopicCompanion AddSubTopic(String aTopicText)
		{
			XmlElement _newnode = m_root.OwnerDocument.CreateElement("ap:Topic", "http://schemas.mindjet.com/MindManager/Application/2003");

			XMLAddAttribute(_newnode, "Dirty", "0000000000000000");
			XMLAddAttribute(_newnode, "Gen", "0000000000000000");
			XMLAddAttribute(_newnode, "OId", MMUtils.NewMMGuid());

			XmlElement _topicViewGroup = m_root.OwnerDocument.CreateElement("ap:TopicViewGroup", "http://schemas.mindjet.com/MindManager/Application/2003");
			XMLAddAttribute(_topicViewGroup, "ViewIndex", "0");

			XmlElement _font = m_root.OwnerDocument.CreateElement("ap:Font", "http://schemas.mindjet.com/MindManager/Application/2003");

			XmlElement _text = m_root.OwnerDocument.CreateElement("ap:Text", "http://schemas.mindjet.com/MindManager/Application/2003");
			XMLAddAttribute(_text, "Dirty", "0000000000000000");
			XMLAddAttribute(_text, "ReadOnly", "false");
			XMLAddAttribute(_text, "PlainText", aTopicText);

			XMLMapCompanion.XMLInsertChildNode(_newnode, _topicViewGroup, m_nodeOrder);
			_text.AppendChild(_font);
			XMLMapCompanion.XMLInsertChildNode(_newnode, _text, m_nodeOrder);

			SubtopicsNode.AppendChild(_newnode);

			return m_xmlDocument != null ?
				m_xmlDocument.RegisterTopic(new XMLTopicCompanion(_newnode, NSManager, m_xmlDocument)) :
				new XMLTopicCompanion(_newnode, NSManager, m_xmlDocument);
		}

		public XMLTopicCompanion AddSubTopic(XMLTopicCompanion aTopicToAdd, bool bPreserveCustomAttributes = false)
		{
			if (m_root == null || aTopicToAdd == null || aTopicToAdd.m_root == null)
				return null;
			XmlNode _importNode = m_root.OwnerDocument.ImportNode(aTopicToAdd.m_root.CloneNode(true), true);
			_importNode.Attributes["OId"].Value = MMUtils.NewMMGuid();

            XmlNode subtopics = _importNode.SelectSingleNode("ap:SubTopics", NSManager);
			if (subtopics != null)
			{
				foreach (XmlNode item in subtopics.ChildNodes)
					item.Attributes["OId"].Value = MMUtils.NewMMGuid();
			}

            SubtopicsNode.AppendChild(_importNode);
			XMLTopicCompanion _rc = new XMLTopicCompanion(_importNode, NSManager, m_xmlDocument);
			if (!bPreserveCustomAttributes)
				_rc.XMLDeleteNode("Custom");

			return m_xmlDocument != null ? m_xmlDocument.RegisterTopic(_rc) : _rc; ;
		}

		/// <summary>
		/// Removes the decorations.
		/// </summary>
		public void RemoveDecorations(bool KeepTopicLayout = false)
		{
			if (m_root == null)
				return;
			if (!KeepTopicLayout)
				XMLDeleteNode("Color");
			XMLDeleteNode("OneBoundary");
            //if (!KeepTopicLayout)
                XMLDeleteNode("TopicLayout");
            XMLDeleteNode("ImageThumbnailSize");
            XMLDeleteNode("CustomControlStripData");
            XMLDeleteNode("TopicReview");
            XMLDeleteNode("TopicBookmark");
            XMLDeleteNode("NamedDefaultsId");
            XMLDeleteNode("Formulas");
            XMLDeleteNode("TopicFormulas");
            XMLDeleteNode("Hyperlink");
            XMLDeleteNode("HyperlinkGroup");
            XMLDeleteNode("AttachmentGroup");
            XMLDeleteNode("ConditionalRules");

            XmlNode _textNode = m_root.SelectSingleNode("ap:Text", NSManager);
			if (_textNode == null)
				return;
			while (_textNode.HasChildNodes)
			{
				XmlNode _nodeToRemove = _textNode.FirstChild;
				_textNode.RemoveChild(_nodeToRemove);
			}

			XmlElement _font = m_root.OwnerDocument.CreateElement("ap:Font", "http://schemas.mindjet.com/MindManager/Application/2003");
			_textNode.AppendChild(_font);
		}

        public void RemoveSmartRules()
		{
            if (m_root == null)
                return;
            XMLDeleteNode("ConditionalFormattingSettingsGroup");
        }

        /// <summary>
        /// Disconnects this Topic from the underlying Document
        /// </summary>
        public void Delete()
		{
			if (m_root == null || m_root.ParentNode == null)
				return;
			if (m_xmlDocument != null)
				m_xmlDocument.UnregisterTopic(Guid);
			m_root.ParentNode.RemoveChild(m_root);
		}

		/// <summary>
		/// Adds the text label.
		/// </summary>
		/// <param name="aLabel">a label.</param>
		public void AddTextLabel(String aLabel, String aGroupId)
		{
			if (aLabel == "" || m_root == null)
				return;
			XmlElement _newnode = m_root.OwnerDocument.CreateElement("ap:TextLabel", "http://schemas.mindjet.com/MindManager/Application/2003");
            XmlNode _refnode = null;

            if (TextLabelsNode.HasChildNodes)
                _refnode = TextLabelsNode.FirstChild; // first tag

            if (_refnode == null)
                TextLabelsNode.AppendChild(_newnode); // if there are not tags, append
            else
                TextLabelsNode.InsertBefore(_newnode, _refnode); // if there are tags, insert as first

            XMLAddAttribute(_newnode, "TextLabelName", aLabel);
            if (aGroupId != "")
                XMLAddAttribute(_newnode, "SetId", aGroupId);
        }

		public DateTime CreationTime
		{
			get
			{
				DateTime _rc = MMUtils.NULLDATE;
				XmlNode _creationInfo = m_root.SelectSingleNode("ap:CreationInfo/@LocalTimestamp", NSManager);
				if (m_root == null)
					return _rc;
				try
				{
					_rc = XmlConvert.ToDateTime(_creationInfo.Value, XmlDateTimeSerializationMode.Local);
				}
				catch { }
				return _rc;
			}
		}

		public DateTime CreationTimeUTC
		{
			get
			{
				DateTime _rc = MMUtils.NULLDATE;
				XmlNode _creationInfo = m_root.SelectSingleNode("ap:CreationInfo/@Timestamp", NSManager);
				if (m_root == null)
					return _rc;
				try
				{
					_rc = XmlConvert.ToDateTime(_creationInfo.Value, XmlDateTimeSerializationMode.Local);
				}
				catch { }
				return _rc;
			}
		}

		public DateTime LastModificationTime
		{
			get
			{
				DateTime _rc = MMUtils.NULLDATE;
				XmlNode _creationInfo = m_root.SelectSingleNode("ap:ModificationStamp/@LocalTimestamp", NSManager);
				if (m_root == null)
					return _rc;
				try
				{
					_rc = XmlConvert.ToDateTime(_creationInfo.Value, XmlDateTimeSerializationMode.Local);
				}
				catch { }
				return _rc;
			}
		}

		public DateTime LastModificationTimeUTC
		{
			get
			{
				DateTime _rc = MMUtils.NULLDATE;
				XmlNode _creationInfo = m_root.SelectSingleNode("ap:ModificationStamp/@Timestamp", NSManager);
				if (m_root == null)
					return _rc;
				try
				{
					_rc = XmlConvert.ToDateTime(_creationInfo.Value, XmlDateTimeSerializationMode.Local);
				}
				catch { }
				return _rc;
			}
		}


		/// <summary>
		/// Add topic as a subtopic to given topic using XML
		/// </summary>
		/// <param name="aTopicToAddTo">A topic to add to.</param>
		/// <param name="aTopicToAdd">A topic to be added.</param>
		public void XMLAddSubtopicTo(Topic aTopicToAddTo)
		{
			if (aTopicToAddTo == null)
				return;
			try
			{
				XmlDocument _xmlDocTo = new XmlDocument();
				_xmlDocTo.LoadXml(aTopicToAddTo.AllSubTopics.Xml);
				XmlElement _xmlTo = _xmlDocTo.DocumentElement;

				XmlNode _importNode = _xmlTo.OwnerDocument.ImportNode(m_root.CloneNode(true), true);
				_xmlTo.AppendChild(_importNode.Clone());

				aTopicToAddTo.AllSubTopics.Xml = _xmlTo.OuterXml;
			}
			catch (Exception _e)
			{
#if DEBUG
				System.Windows.Forms.MessageBox.Show("Exception: " + _e.Message + "\r\nSource: " + _e.Source + "\r\nStack: " + _e.StackTrace);
#endif
			}
		}

		public static void XMLCopyTopicData(Topic aSrc, Topic aDst, bool bConvertRollupData = false, bool bStripSyncinfo = true, bool bCopySubtopics = false)
		{
            if (aSrc == null || aDst == null)
				return;
			try
			{
				XmlDocument _srcxmlDoc = new XmlDocument();
				_srcxmlDoc.LoadXml(aSrc.Xml);
				XmlElement _srcxmlElement = _srcxmlDoc.DocumentElement;

				XmlDocument _dstxmlDoc = new XmlDocument();
				_dstxmlDoc.LoadXml(aDst.Xml);
				XmlElement _dstxmlElement = _dstxmlDoc.DocumentElement;
                
                XmlNode _SrcSubtopics = XMLDeleteNode(_srcxmlElement, "Subtopics");

				XmlNode _DstSubtopics = XMLDeleteNode(_dstxmlElement, "Subtopics");
                
				// Preserve GUID
				if (_dstxmlElement.HasAttribute("OId"))
					_srcxmlElement.SetAttribute("OId", _dstxmlElement.GetAttribute("OId"));

				// Now let's treat src xml as dst.

				if (bConvertRollupData)
					XMLDeleteNode(_srcxmlElement, "TopicFormulas");

				XMLMapCompanion.XMLInsertChildNode(_srcxmlElement, bCopySubtopics ? _SrcSubtopics : _DstSubtopics, m_nodeOrder);

				// Need to preserve offset
				if (aDst.IsFloatingTopic)
				{
					float cx, cy;
					aDst.GetOffset(out cx, out cy);
					aDst.Xml = _srcxmlDoc.OuterXml;
					aDst.SetOffset(cx, cy);
				}
				else
				{
					aDst.Xml = _srcxmlDoc.OuterXml;
				}
			}
			catch (System.Exception _e)
			{
#if DEBUG
				System.Windows.Forms.MessageBox.Show("Exception: " + _e.Message + "\r\nSource: " + _e.Source + "\r\nStack: " + _e.StackTrace);
#endif
			}
		}

		/// <summary>
		/// Copy all subtopics of this instance to given MM topic using XML
		/// </summary>
		/// <param name="aTopicTo">a topic to copy subtopics to.</param>
		public void XMLCopySubtopicsTo(Topic aTopicTo)
		{
			if (m_root == null || aTopicTo == null)
				return;
			XmlNode _subtopics = m_root.SelectSingleNode("ap:Subtopics", NSManager);
			if (_subtopics != null)
				aTopicTo.AllSubTopics.Xml = _subtopics.OuterXml;
			else
			{
				XmlDocument _xmlDocTo = new XmlDocument();
				_xmlDocTo.LoadXml(aTopicTo.Xml);
				XmlElement _xmlTo = _xmlDocTo.DocumentElement;
				XMLDeleteNode(_xmlTo, "Subtopics");
			}
		}

		protected XmlNamespaceManager m_manager = null;
		public XmlNode m_root = null;
		protected bool m_iconsLoaded = false;
		protected HashSet<XMLHyperlinkCompanion> m_hyperLinks = null;
		protected XMLMapCompanion m_xmlDocument = null;
		public List<String> m_subtopics = new List<string>();
        public List<Dashboard.TagsContainer> textLabels = null;
        public Document doc = null;
        public bool keepsubtopics = false;
        public bool showlinks = false;

        protected static String[] m_nodeOrder = {
			"cor:Custom",
			"ap:SubTopics",
			"ap:FloatingTopics",
			"ap:OneBoundary",
			"ap:OneImage",
			"ap:TopicViewGroup",
			"ap:Text",
			"ap:Color",
			"ap:NotesGroup",
			"ap:Offset",
			"ap:IconsGroup",
            "ap:SubTopicShape",
			"ap:LabelFloatingTopicShape",
			"ap:CalloutFloatingTopicShape",
			"ap:TopicLayout",
			"ap:SubTopicsShape",
			"ap:SubTopicsVisibility",
			"ap:Hyperlink",
			"ap:Task",
			"ap:Bookmark",
			"ap:Comments",
			"ap:SpellingOptions",
			"ap:InkGroup",
			"ap:OneInkSketch",
			"ap:AttachmentGroup",
			"ap:TextLabels",
			"ap:BusinessDataGroup",
			"ap:CustomControlStripData",
			"ap:TopicReview",
			"ap:TopicBookmark",
			"ap:ModificationStamp",
			"ap:CreationInfo",
			"ap:LoadingState",
			"ap:NamedDefaultsId",
			"ap:HyperlinkGroup",
			"ap:Formulas",
			"ap:TopicFormulas",
			"ap:ConditionalRules",
            "ap:FilterSettings",
            "ap:ConditionalFormattingSettingsGroup",
            "ap:ImageThumbnailSize",
            "ap:RevisionGroup"
        };
	}
}
