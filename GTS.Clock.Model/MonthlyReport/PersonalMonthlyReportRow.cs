using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure;
using GTS.Clock.Model;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Infrastructure.Utility;
using System.Text.RegularExpressions;

namespace GTS.Clock.Model.MonthlyReport
{
    public class PersonalMonthlyReportRow
    {
        const string WHITE = "Transparent";

        #region Cunstractor

        public PersonalMonthlyReportRow()
            : this(new Person(), DateTime.Now, LanguagesName.English, new List<CurrentProceedTraffic>(), new Dictionary<string, ScndCnpValue>(), new Dictionary<string, ScndCnpValue>())
        { }

        public PersonalMonthlyReportRow(Person prs, DateTime Date, LanguagesName LangName,
                                        IList<CurrentProceedTraffic> DailyProceedTrafficList,
                                        IDictionary<string, ScndCnpValue> DailyScndCnpValueDic,
                                        IDictionary<string, ScndCnpValue> PeriodicScndCnpValueDic)
        {
            this.Person = prs;
            this.LanguagesName = LangName;
            this.Date = Date;
            this.DailyScndCnpValueDic = DailyScndCnpValueDic;
            this.DailyProceedTrafficList = DailyProceedTrafficList;
            this.PeriodicScndCnpValueDic = PeriodicScndCnpValueDic;
        }

        #endregion

        private string dayState_BC = "";
        private string dayHolidayState_BC = "";
        private string dayStateTitle = "";
        private bool dayStatesIsSet = false;

        #region Properties

        /// <summary>
        /// جهت استفاده در واسط کاربر
        /// </summary>
        public virtual string ID
        {
            get
            {
                return Guid.NewGuid().ToString("N");
            }
        }

        public virtual Person Person { get; set; }

        /// <summary>
        /// Gets or sets the Date value.
        /// </summary>
        public virtual DateTime Date { get; set; }

        /// <summary>
        /// جهت استفاده در واسط کاربر
        /// </summary>
        public virtual String TheDate
        {
            get
            {
                if (LanguagesName == LanguagesName.Parsi)
                {
                    return Utility.ToParsiCharacter(Utility.ToPersianDate(this.Date));
                }
                return Utility.ToString(this.Date);
            }
        }

        /// <summary>
        /// آیا در این روز مفهومی که مقدار داشته باشد وجود دارد
        /// </summary>
        public virtual bool HasRowValue
        {
            get
            {
                if (this.DailyScndCnpValueDic.Count > 0)
                    return true;
                return false;
            }
        }

        public virtual string DayName
        {
            get
            {
                if (this.LanguagesName == LanguagesName.Parsi)
                    return PersianDateTime.GetPershianDayName(Date);
                else
                    return PersianDateTime.GetEnglishDayName(Date);
            }
        }

        public virtual LanguagesName LanguagesName
        {
            get;
            set;
        }

        #region Daily Properties

        /// <summary>
        /// Gets or sets the FirstEntrance value.
        /// </summary>
        public virtual String FirstEntrance
        {
            get
            {
                if (this.DailyProceedTrafficList.Count >= 1)
                {
                    return Utility.IntTimeToRealTimeWithZero(this.DailyProceedTrafficList[0].From);
                }
                else
                    return "";
            }
        }

        /// <summary>
        /// Gets or sets the FirstExit value.
        /// </summary>
        public virtual String FirstExit
        {
            get
            {
                if (this.DailyProceedTrafficList.Count >= 1)
                {
                    return Utility.IntTimeToRealTimeWithZero(this.DailyProceedTrafficList[0].To);
                }
                else
                    return "";
            }
        }

        /// <summary>
        /// Gets or sets the SecondEntrance value.
        /// </summary>
        public virtual String SecondEntrance
        {
            get
            {
                if (this.DailyProceedTrafficList.Count >= 2)
                {
                    return Utility.IntTimeToRealTimeWithZero(this.DailyProceedTrafficList[1].From);
                }
                else
                    return "";
            }
        }

        /// <summary>
        /// Gets or sets the SecondExit value.
        /// </summary>
        public virtual String SecondExit
        {
            get
            {
                if (this.DailyProceedTrafficList.Count >= 2)
                {
                    return Utility.IntTimeToRealTimeWithZero(this.DailyProceedTrafficList[1].To);
                }
                else
                    return "";
            }
        }

        /// <summary>
        /// Gets or sets the ThirdEntrance value.
        /// </summary>
        public virtual String ThirdEntrance
        {
            get
            {
                if (this.DailyProceedTrafficList.Count >= 3)
                {
                    return Utility.IntTimeToRealTimeWithZero(this.DailyProceedTrafficList[2].From);
                }
                else
                    return "";
            }

        }

        /// <summary>
        /// Gets or sets the ThirdExit value.
        /// </summary>
        public virtual String ThirdExit
        {
            get
            {
                if (this.DailyProceedTrafficList.Count >= 3)
                {
                    return Utility.IntTimeToRealTimeWithZero(this.DailyProceedTrafficList[2].To);
                }
                else
                    return "";
            }

        }

        /// <summary>
        /// Gets or sets the FourthEntrance value.
        /// </summary>
        public virtual String FourthEntrance
        {
            get
            {
                if (this.DailyProceedTrafficList.Count >= 4)
                {
                    return Utility.IntTimeToRealTimeWithZero(this.DailyProceedTrafficList[3].From);
                }
                else
                    return "";
            }

        }

        /// <summary>
        /// Gets or sets the FourthExit value.
        /// </summary>
        public virtual String FourthExit
        {
            get
            {
                if (this.DailyProceedTrafficList.Count >= 4)
                {
                    return Utility.IntTimeToRealTimeWithZero(this.DailyProceedTrafficList[3].To);
                }
                else
                    return "";
            }
        }

        /// <summary>
        /// Gets or sets the FifthEntrance value.
        /// </summary>
        public virtual String FifthEntrance
        {
            get
            {
                if (this.DailyProceedTrafficList.Count >= 5)
                {
                    return Utility.IntTimeToRealTimeWithZero(this.DailyProceedTrafficList[4].From);
                }
                else
                    return "";
            }
        }

        /// <summary>
        /// Gets or sets the FifthExit value.
        /// </summary>
        public virtual String FifthExit
        {
            get
            {
                if (this.DailyProceedTrafficList.Count >= 5)
                {
                    return Utility.IntTimeToRealTimeWithZero(this.DailyProceedTrafficList[4].To);
                }
                else
                    return "";
            }
        }

        /// <summary>
        /// Gets or sets the LastExit value.
        /// </summary>
        public virtual String LastExit
        {
            get
            {
                CurrentProceedTraffic currentProceedTraffic = this.DailyProceedTrafficList.LastOrDefault();
                if (currentProceedTraffic == null)
                    return "";
                else
                {
                    return Utility.IntTimeToRealTimeWithZero(currentProceedTraffic.To);
                }

            }
        }

        /// <summary>
        /// Gets or sets the NecessaryOperation value.
        /// </summary>
        public virtual String NecessaryOperation
        {
            get
            {
                ScndCnpValue monthlyScndCnpValue = null;
                this.DailyScndCnpValueDic.TryGetValue("gridFields_NecessaryOperation", out monthlyScndCnpValue);
                string value = Utility.IntTimeToTime(monthlyScndCnpValue == null ? 0 : (int)monthlyScndCnpValue.Value);
                return value;
            }
        }

        /// <summary>
        /// Gets or sets the HourlyPureOperation value.
        /// </summary>
        public virtual String HourlyPureOperation
        {
            get
            {
                ScndCnpValue monthlyScndCnpValue = null;
                this.DailyScndCnpValueDic.TryGetValue("gridFields_HourlyPureOperation", out monthlyScndCnpValue);
                string value = Utility.IntTimeToTime(monthlyScndCnpValue == null ? 0 : (int)monthlyScndCnpValue.Value);
                return value;
            }
        }

        /// <summary>
        /// Gets or sets the DailyPureOperation value.
        /// </summary>
        public virtual String DailyPureOperation
        {
            get
            {
                ScndCnpValue monthlyScndCnpValue = null;
                this.DailyScndCnpValueDic.TryGetValue("gridFields_DailyPureOperation", out monthlyScndCnpValue);
                string value = monthlyScndCnpValue == null ? "" : monthlyScndCnpValue.Value.ToString();
                return value;
            }
        }

        /// <summary>
        /// Gets or sets the ImpureOperation value.
        /// </summary>
        public virtual String ImpureOperation
        {
            get
            {
                ScndCnpValue monthlyScndCnpValue = null;
                this.DailyScndCnpValueDic.TryGetValue("gridFields_ImpureOperation", out monthlyScndCnpValue);
                string value = Utility.IntTimeToTime(monthlyScndCnpValue == null ? 0 : (int)monthlyScndCnpValue.Value);
                return value;
            }
        }

        /// <summary>
        /// Gets or sets the AllowableOverTime value.
        /// </summary>
        public virtual String AllowableOverTime
        {
            get
            {
                ScndCnpValue monthlyScndCnpValue = null;
                this.DailyScndCnpValueDic.TryGetValue("gridFields_AllowableOverTime", out monthlyScndCnpValue);
                string value = Utility.IntTimeToTime(monthlyScndCnpValue == null ? 0 : (int)monthlyScndCnpValue.Value);
                return value;
            }
        }

        /// <summary>
        /// Gets or sets the UnallowableOverTime value.
        /// </summary>
        public virtual String UnallowableOverTime
        {
            get
            {
                ScndCnpValue monthlyScndCnpValue = null;
                this.DailyScndCnpValueDic.TryGetValue("gridFields_UnallowableOverTime", out monthlyScndCnpValue);
                string value = Utility.IntTimeToTime(monthlyScndCnpValue == null ? 0 : (int)monthlyScndCnpValue.Value);
                return value;
            }
        }

        /// <summary>
        /// Gets or sets the HourlyAllowableAbsence value.
        /// </summary>
        public virtual String HourlyAllowableAbsence
        {
            get
            {
                ScndCnpValue monthlyScndCnpValue = null;
                this.DailyScndCnpValueDic.TryGetValue("gridFields_HourlyAllowableAbsence", out monthlyScndCnpValue);
                string value = Utility.IntTimeToTime(monthlyScndCnpValue == null ? 0 : (int)monthlyScndCnpValue.Value);
                return value;
            }
        }

        /// <summary>
        /// Gets or sets the HourlyUnallowableAbsence value.
        /// </summary>
        public virtual String HourlyUnallowableAbsence
        {
            get
            {
                ScndCnpValue monthlyScndCnpValue = null;
                this.DailyScndCnpValueDic.TryGetValue("gridFields_HourlyUnallowableAbsence", out monthlyScndCnpValue);
                string value = Utility.IntTimeToTime(monthlyScndCnpValue == null ? 0 : (int)monthlyScndCnpValue.Value);
                return value;
            }
        }

        /// <summary>
        /// Gets or sets the DailyAbsence value.
        /// </summary>
        public virtual String DailyAbsence
        {
            get
            {
                ScndCnpValue monthlyScndCnpValue = null;
                this.DailyScndCnpValueDic.TryGetValue("gridFields_DailyAbsence", out monthlyScndCnpValue);
                string value = monthlyScndCnpValue == null ? "" : monthlyScndCnpValue.Value.ToString();
                return value;
            }
        }

        /// <summary>
        /// Gets or sets the HourlyMission value.
        /// </summary>
        public virtual String HourlyMission
        {
            get
            {
                ScndCnpValue monthlyScndCnpValue = null;
                this.DailyScndCnpValueDic.TryGetValue("gridFields_HourlyMission", out monthlyScndCnpValue);
                string value = Utility.IntTimeToTime(monthlyScndCnpValue == null ? 0 : (int)monthlyScndCnpValue.Value);
                return value;
            }
        }

        /// <summary>
        /// Gets or sets the DailyMission value.
        /// </summary>
        public virtual String DailyMission
        {
            get
            {
                ScndCnpValue monthlyScndCnpValue = null;
                this.DailyScndCnpValueDic.TryGetValue("gridFields_DailyMission", out monthlyScndCnpValue);
                string value = monthlyScndCnpValue == null ? "" : monthlyScndCnpValue.Value.ToString();
                return value;
            }
        }

        /// <summary>
        /// Gets or sets the HostelryMission value.
        /// </summary>
        public virtual String HostelryMission
        {
            get
            {
                ScndCnpValue monthlyScndCnpValue = null;
                this.DailyScndCnpValueDic.TryGetValue("gridFields_HostelryMission", out monthlyScndCnpValue);
                string value = Utility.IntTimeToTime(monthlyScndCnpValue == null ? 0 : (int)monthlyScndCnpValue.Value);
                return value;
            }
        }

        /// <summary>
        /// Gets or sets the HourlySickLeave value.
        /// </summary>
        public virtual String HourlySickLeave
        {
            get
            {
                ScndCnpValue monthlyScndCnpValue = null;
                this.DailyScndCnpValueDic.TryGetValue("gridFields_HourlySickLeave", out monthlyScndCnpValue);
                string value = Utility.IntTimeToTime(monthlyScndCnpValue == null ? 0 : (int)monthlyScndCnpValue.Value);
                return value;
            }
        }

        /// <summary>
        /// Gets or sets the DailySickLeave value.
        /// </summary>
        public virtual String DailySickLeave
        {
            get
            {
                ScndCnpValue monthlyScndCnpValue = null;
                this.DailyScndCnpValueDic.TryGetValue("gridFields_DailySickLeave", out monthlyScndCnpValue);
                string value = monthlyScndCnpValue == null ? "" : monthlyScndCnpValue.Value.ToString();
                return value;
            }
        }

        /// <summary>
        /// Gets or sets the HourlyMeritoriouslyLeave value.
        /// </summary>
        public virtual String HourlyMeritoriouslyLeave
        {
            get
            {
                ScndCnpValue monthlyScndCnpValue = null;
                this.DailyScndCnpValueDic.TryGetValue("gridFields_HourlyMeritoriouslyLeave", out monthlyScndCnpValue);
                string value = Utility.IntTimeToTime(monthlyScndCnpValue == null ? 0 : (int)monthlyScndCnpValue.Value);
                return value;
            }
        }

        /// <summary>
        /// Gets or sets the DailyMeritoriouslyLeave value.
        /// </summary>
        public virtual String DailyMeritoriouslyLeave
        {
            get
            {
                ScndCnpValue monthlyScndCnpValue = null;
                this.DailyScndCnpValueDic.TryGetValue("gridFields_DailyMeritoriouslyLeave", out monthlyScndCnpValue);
                string value = monthlyScndCnpValue == null ? "" : monthlyScndCnpValue.Value.ToString();
                return value;
            }
        }

        /// <summary>
        /// Gets or sets the HourlyWithoutPayLeave value.
        /// </summary>
        public virtual String HourlyWithoutPayLeave
        {
            get
            {
                ScndCnpValue monthlyScndCnpValue = null;
                this.DailyScndCnpValueDic.TryGetValue("gridFields_HourlyWithoutPayLeave", out monthlyScndCnpValue);
                string value = Utility.IntTimeToTime(monthlyScndCnpValue == null ? 0 : (int)monthlyScndCnpValue.Value);
                return value;
            }
        }

        /// <summary>
        /// کد وضعیت روز که در قانون 208 مقداردهی میگردد
        /// </summary>
        public virtual int DayStateCode
        {
            get
            {
                ScndCnpValue monthlyScndCnpValue = null;
                this.DailyScndCnpValueDic.TryGetValue("gridFields_DayStateCode", out monthlyScndCnpValue);

                return (int)monthlyScndCnpValue.Value;
            }
        }

        /// <summary>
        /// Gets or sets the PresenceDuration value.
        /// </summary>
        public virtual String PresenceDuration
        {
            get
            {
                ScndCnpValue monthlyScndCnpValue = null;
                this.DailyScndCnpValueDic.TryGetValue("gridFields_PresenceDuration", out monthlyScndCnpValue);
                string value = Utility.IntTimeToTime(monthlyScndCnpValue == null ? 0 : (int)monthlyScndCnpValue.Value);
                return value;
            }
        }

        /// <summary>
        /// Gets or sets the DailyWithoutPayLeave value.
        /// </summary>
        public virtual String DailyWithoutPayLeave
        {
            get
            {
                ScndCnpValue monthlyScndCnpValue = null;
                this.DailyScndCnpValueDic.TryGetValue("gridFields_DailyWithoutPayLeave", out monthlyScndCnpValue);
                string value = monthlyScndCnpValue == null ? "" : monthlyScndCnpValue.Value.ToString();
                return value;
            }
        }

        /// <summary>
        /// Gets or sets the HourlyWithPayLeave value.
        /// </summary>
        public virtual String HourlyWithPayLeave
        {
            get
            {
                ScndCnpValue monthlyScndCnpValue = null;
                this.DailyScndCnpValueDic.TryGetValue("gridFields_HourlyWithPayLeave", out monthlyScndCnpValue);
                string value = Utility.IntTimeToTime(monthlyScndCnpValue == null ? 0 : (int)monthlyScndCnpValue.Value);
                return value;
            }
        }

        /// <summary>
        /// Gets or sets the DailyWithPayLeave value.
        /// </summary>
        public virtual String DailyWithPayLeave
        {
            get
            {
                ScndCnpValue monthlyScndCnpValue = null;
                this.DailyScndCnpValueDic.TryGetValue("gridFields_DailyWithPayLeave", out monthlyScndCnpValue);
                string value = monthlyScndCnpValue == null ? "" : monthlyScndCnpValue.Value.ToString();
                return value;
            }
        }

        public virtual String Shift
        {
            get
            {
                BaseShift sh = this.Person.GetShiftByDate(this.Date);
                return sh == null ? "" : sh.Name;
            }
        }

        public virtual String ShiftPairs
        {
            get
            {
                //this.Person.InitializePersonToLoadShiftPair
                string Result = "";
                BaseShift sh = this.Person.GetShiftByDate(this.Date);
                if (sh == null)
                    return Result;
                IList<ShiftPair> pairs = sh.Pairs;
                foreach (ShiftPair shiftpair in pairs)
                {
                    Result += shiftpair.ToString() + " ";
                }
                return Result;
            }
        }

        /// <summary>
        /// رزرو روزانه 1
        /// </summary>
        public virtual String ReserveField1
        {
            get
            {
                ScndCnpValue monthlyScndCnpValue = null;
                this.DailyScndCnpValueDic.TryGetValue("gridFields_ReserveField1", out monthlyScndCnpValue);
                string value = monthlyScndCnpValue == null ? "" : monthlyScndCnpValue.Value.ToString();
                if (value.Equals("-1")) 
                {
                    value = monthlyScndCnpValue == null ? "" : monthlyScndCnpValue.FromPairs;
                }
                return value;
            }
        }

        /// <summary>
        /// رزرو روزانه 2
        /// </summary>
        public virtual String ReserveField2
        {
            get
            {
                ScndCnpValue monthlyScndCnpValue = null;
                this.DailyScndCnpValueDic.TryGetValue("gridFields_ReserveField2", out monthlyScndCnpValue);
                string value = monthlyScndCnpValue == null ? "" : monthlyScndCnpValue.Value.ToString();
                if (value.Equals("-1"))
                {
                    value = monthlyScndCnpValue == null ? "" : monthlyScndCnpValue.FromPairs;
                }
                return value;
            }
        }

        /// <summary>
        /// رزرو روزانه 3
        /// </summary>
        public virtual String ReserveField3
        {
            get
            {
                ScndCnpValue monthlyScndCnpValue = null;
                this.DailyScndCnpValueDic.TryGetValue("gridFields_ReserveField3", out monthlyScndCnpValue);
                string value = monthlyScndCnpValue == null ? "" : monthlyScndCnpValue.Value.ToString();
                if (value.Equals("-1"))
                {
                    value = monthlyScndCnpValue == null ? "" : monthlyScndCnpValue.FromPairs;
                }
                return value;
            }
        }

        /// <summary>
        /// رزرو روزانه 4
        /// </summary>
        public virtual String ReserveField4
        {
            get
            {
                ScndCnpValue monthlyScndCnpValue = null;
                this.DailyScndCnpValueDic.TryGetValue("gridFields_ReserveField4", out monthlyScndCnpValue);
                string value = monthlyScndCnpValue == null ? "" : monthlyScndCnpValue.Value.ToString();
                if (value.Equals("-1"))
                {
                    value = monthlyScndCnpValue == null ? "" : monthlyScndCnpValue.FromPairs;
                }
                return value;
            }
        }

        /// <summary>
        /// رزرو ساعتی 5 
        /// </summary>
        public virtual String ReserveField5
        {
            get
            {
                ScndCnpValue monthlyScndCnpValue = null;
                this.DailyScndCnpValueDic.TryGetValue("gridFields_ReserveField5", out monthlyScndCnpValue);
                string value = Utility.IntTimeToTime(monthlyScndCnpValue == null ? 0 : (int)monthlyScndCnpValue.Value);
                if (value.Equals("-1"))
                {
                    value = monthlyScndCnpValue == null ? "" : monthlyScndCnpValue.FromPairs;
                }
                return value;
            }
        }

        /// <summary>
        /// رزرو ساعتی 6
        /// </summary>
        public virtual String ReserveField6
        {
            get
            {
                ScndCnpValue monthlyScndCnpValue = null;
                this.DailyScndCnpValueDic.TryGetValue("gridFields_ReserveField6", out monthlyScndCnpValue);
                string value = Utility.IntTimeToTime(monthlyScndCnpValue == null ? 0 : (int)monthlyScndCnpValue.Value);
                if (value.Equals("-1"))
                {
                    value = monthlyScndCnpValue == null ? "" : monthlyScndCnpValue.FromPairs;
                }
                return value;
            }
        }

        /// <summary>
        /// رزرو ساعتی 7
        /// </summary>
        public virtual String ReserveField7
        {
            get
            {
                ScndCnpValue monthlyScndCnpValue = null;
                this.DailyScndCnpValueDic.TryGetValue("gridFields_ReserveField7", out monthlyScndCnpValue);
                string value = Utility.IntTimeToTime(monthlyScndCnpValue == null ? 0 : (int)monthlyScndCnpValue.Value);
                if (value.Equals("-1"))
                {
                    value = monthlyScndCnpValue == null ? "" : monthlyScndCnpValue.FromPairs;
                }
                return value;
            }
        }

        /// <summary>
        /// رزرو ساعتی 8
        /// </summary>
        public virtual String ReserveField8
        {
            get
            {
                ScndCnpValue monthlyScndCnpValue = null;
                this.DailyScndCnpValueDic.TryGetValue("gridFields_ReserveField8", out monthlyScndCnpValue);
                string value = Utility.IntTimeToTime(monthlyScndCnpValue == null ? 0 : (int)monthlyScndCnpValue.Value);
                if (value.Equals("-1"))
                {
                    value = monthlyScndCnpValue == null ? "" : monthlyScndCnpValue.FromPairs;
                }
                return value;
            }
        }

        /// <summary>
        /// رزرو ساعتی 9
        /// </summary>
        public virtual String ReserveField9
        {
            get
            {
                ScndCnpValue monthlyScndCnpValue = null;
                this.DailyScndCnpValueDic.TryGetValue("gridFields_ReserveField9", out monthlyScndCnpValue);
                string value = Utility.IntTimeToTime(monthlyScndCnpValue == null ? 0 : (int)monthlyScndCnpValue.Value);
                if (value.Equals("-1"))
                {
                    value = monthlyScndCnpValue == null ? "" : monthlyScndCnpValue.FromPairs;
                }
                return value;
            }
        }

        /// <summary>
        /// رزرو ساعتی 10
        /// </summary>
        public virtual String ReserveField10
        {
            get
            {
                ScndCnpValue monthlyScndCnpValue = null;
                this.DailyScndCnpValueDic.TryGetValue("gridFields_ReserveField10", out monthlyScndCnpValue);
                string value = Utility.IntTimeToTime(monthlyScndCnpValue == null ? 0 : (int)monthlyScndCnpValue.Value);
                if (value.Equals("-1"))
                {
                    value = monthlyScndCnpValue == null ? "" : monthlyScndCnpValue.FromPairs;
                }
                return value;
            }
        }

        #endregion

        #region کانت چارت

        /// <summary>
        /// حضور بدون اضافه کار
        /// </summary>
        public virtual PersonalMonthlyReportRowDetail PairlyPresenceDuration
        {
            get
            {
                ScndCnpValue monthlyScndCnpValue = null;
                this.DailyScndCnpValueDic.TryGetValue("gridFields_PureHourlyPresentWidoutOverwork", out monthlyScndCnpValue);
                ScndCnpValue value = monthlyScndCnpValue == null ? new ScndCnpValue() : monthlyScndCnpValue;
                PersonalMonthlyReportRowDetail detail = new PersonalMonthlyReportRowDetail();
                detail.ImpureValue = this.PresenceDuration;
                detail.Color = !Utility.IsEmpty(value.Color) ? value.Color : "Transparent";
                detail.Date = value.FromDate;
                detail.Froms = value.FromPairs;
                detail.Tos = value.ToPairs;
                detail.PersonId = value.PersonId;

                return detail;
            }
        }

        /// <summary>
        /// غیبت ساعتی
        /// </summary>
        public virtual PersonalMonthlyReportRowDetail PairlyHourlyUnallowableAbsence
        {
            get
            {
                ScndCnpValue monthlyScndCnpValue = null;
                this.DailyScndCnpValueDic.TryGetValue("gridFields_PureHourlyUnallowableAbsence", out monthlyScndCnpValue);
                ScndCnpValue value = monthlyScndCnpValue == null ? new ScndCnpValue() : monthlyScndCnpValue;
                PersonalMonthlyReportRowDetail detail = new PersonalMonthlyReportRowDetail();
                detail.ImpureValue = this.HourlyUnallowableAbsence;
                detail.Color = !Utility.IsEmpty(value.Color) ? value.Color : "Transparent";
                detail.Date = value.FromDate;
                detail.Froms = value.FromPairs;
                detail.Tos = value.ToPairs;
                detail.PersonId = value.PersonId;
                
                return detail;
            }
        }

        /// <summary>
        /// اضافه کار ساعتی
        /// </summary>
        public virtual PersonalMonthlyReportRowDetail PairlyAllowedOverTime
        {
            get
            {
                ScndCnpValue monthlyScndCnpValue = null;
                this.DailyScndCnpValueDic.TryGetValue("gridFields_PureAllowableOverTime", out monthlyScndCnpValue);
                ScndCnpValue value = monthlyScndCnpValue == null ? new ScndCnpValue() : monthlyScndCnpValue;
                PersonalMonthlyReportRowDetail detail = new PersonalMonthlyReportRowDetail();
                detail.ImpureValue = this.AllowableOverTime;
                detail.Color = !Utility.IsEmpty(value.Color) ? value.Color : "Transparent";
                detail.Date = value.FromDate;
                detail.Froms = value.FromPairs;
                detail.Tos = value.ToPairs;
                detail.PersonId = value.PersonId;

                return detail;
            }
        }

        /// <summary>
        /// مرخصی ساعتی استعلاجی
        /// </summary>
        public virtual PersonalMonthlyReportRowDetail PairlyHourlySickLeave
        {
            get
            {
                ScndCnpValue monthlyScndCnpValue = null;
                this.DailyScndCnpValueDic.TryGetValue("gridFields_PureHourlySickLeave", out monthlyScndCnpValue);
                ScndCnpValue value = monthlyScndCnpValue == null ? new ScndCnpValue() : monthlyScndCnpValue;
                PersonalMonthlyReportRowDetail detail = new PersonalMonthlyReportRowDetail();
                detail.ImpureValue = this.HourlySickLeave;
                detail.Color = !Utility.IsEmpty(value.Color) ? value.Color : "Transparent";
                detail.Date = value.FromDate;
                detail.Froms = value.FromPairs;
                detail.Tos = value.ToPairs;
                detail.PersonId = value.PersonId;

                return detail;
            }
        }

        /// <summary>
        /// مرخصی ساعتی استحقاقی
        /// </summary>
        public virtual PersonalMonthlyReportRowDetail PairlyHourlyMeritoriouslyLeave
        {
            get
            {
                ScndCnpValue monthlyScndCnpValue = null;
                this.DailyScndCnpValueDic.TryGetValue("gridFields_PureHourlyMeritoriouslyLeave", out monthlyScndCnpValue);
                ScndCnpValue value = monthlyScndCnpValue == null ? new ScndCnpValue() : monthlyScndCnpValue;
                PersonalMonthlyReportRowDetail detail = new PersonalMonthlyReportRowDetail();
                detail.ImpureValue = this.HourlyMeritoriouslyLeave;
                detail.Color = !Utility.IsEmpty(value.Color) ? value.Color : "Transparent";
                detail.Date = value.FromDate;
                detail.Froms = value.FromPairs;
                detail.Tos = value.ToPairs;
                detail.PersonId = value.PersonId;

                return detail;
            }
        }

        /// <summary>
        /// مرخصی ساعتی بی حقوق
        /// </summary>
        public virtual PersonalMonthlyReportRowDetail PairlyHourlyWithoutPayLeave
        {
            get
            {
                ScndCnpValue monthlyScndCnpValue = null;
                this.DailyScndCnpValueDic.TryGetValue("gridFields_PureHourlyWithoutPayLeave", out monthlyScndCnpValue);
                ScndCnpValue value = monthlyScndCnpValue == null ? new ScndCnpValue() : monthlyScndCnpValue;
                PersonalMonthlyReportRowDetail detail = new PersonalMonthlyReportRowDetail();
                detail.ImpureValue = this.HourlyWithoutPayLeave;
                detail.Color = !Utility.IsEmpty(value.Color) ? value.Color : "Transparent";
                detail.Date = value.FromDate;
                detail.Froms = value.FromPairs;
                detail.Tos = value.ToPairs;
                detail.PersonId = value.PersonId;

                return detail;
            }
        }

        /// <summary>
        /// مرخصی ساعتی با حقوق
        /// </summary>
        public virtual PersonalMonthlyReportRowDetail PairlyHourlyWithPayLeave
        {
            get
            {
                ScndCnpValue monthlyScndCnpValue = null;
                this.DailyScndCnpValueDic.TryGetValue("gridFields_PureHourlyWithPayLeave", out monthlyScndCnpValue);
                ScndCnpValue value = monthlyScndCnpValue == null ? new ScndCnpValue() : monthlyScndCnpValue;
                PersonalMonthlyReportRowDetail detail = new PersonalMonthlyReportRowDetail();
                detail.ImpureValue = this.HourlyWithPayLeave;
                detail.Color = !Utility.IsEmpty(value.Color) ? value.Color : "Transparent";
                detail.Date = value.FromDate;
                detail.Froms = value.FromPairs;
                detail.Tos = value.ToPairs;
                detail.PersonId = value.PersonId;

                return detail;
            }
        }

        /// <summary>
        /// ماموریت ساعتی 
        /// </summary>
        public virtual PersonalMonthlyReportRowDetail PairlyHourlyMission
        {
            get
            {
                ScndCnpValue monthlyScndCnpValue = null;
                this.DailyScndCnpValueDic.TryGetValue("gridFields_PureHourlyMission", out monthlyScndCnpValue);
                ScndCnpValue value = monthlyScndCnpValue == null ? new ScndCnpValue() : monthlyScndCnpValue;
                PersonalMonthlyReportRowDetail detail = new PersonalMonthlyReportRowDetail();
                detail.ImpureValue = this.HourlyMission;
                detail.Color = !Utility.IsEmpty(value.Color) ? value.Color : "Transparent";
                detail.Date = value.FromDate;
                detail.Froms = value.FromPairs;
                detail.Tos = value.ToPairs;
                detail.PersonId = value.PersonId;

                return detail;
            }
        }

        /// <summary>
        /// اضافه کار غیر مجاز
        /// مقدار خالص و ناخالص برابر است
        /// </summary>
        public virtual PersonalMonthlyReportRowDetail PairlyUnallowableOverTime
        {
            get
            {
                ScndCnpValue monthlyScndCnpValue = null;
                this.DailyScndCnpValueDic.TryGetValue("gridFields_UnallowableOverTime", out monthlyScndCnpValue);
                ScndCnpValue value = monthlyScndCnpValue == null ? new ScndCnpValue() : monthlyScndCnpValue;
                PersonalMonthlyReportRowDetail detail = new PersonalMonthlyReportRowDetail();
                detail.ImpureValue = this.UnallowableOverTime;
                detail.Color = !Utility.IsEmpty(value.Color) ? value.Color : "Transparent";
                detail.Date = value.FromDate;
                detail.Froms = value.FromPairs;
                detail.Tos = value.ToPairs;
                detail.PersonId = value.PersonId;

                return detail;
            }
        }

        /// <summary>
        /// غیبت ساعتی مجاز
        /// مقدار خالص و ناخالص آن برابر است
        /// </summary>
        public virtual PersonalMonthlyReportRowDetail PairlyHourlyAllowableAbsence
        {
            get
            {
                ScndCnpValue monthlyScndCnpValue = null;
                this.DailyScndCnpValueDic.TryGetValue("gridFields_HourlyAllowableAbsence", out monthlyScndCnpValue);
                ScndCnpValue value = monthlyScndCnpValue == null ? new ScndCnpValue() : monthlyScndCnpValue;
                PersonalMonthlyReportRowDetail detail = new PersonalMonthlyReportRowDetail();
                detail.ImpureValue = this.HourlyAllowableAbsence;
                detail.Color = !Utility.IsEmpty(value.Color) ? value.Color : "Transparent";
                detail.Date = value.FromDate;
                detail.Froms = value.FromPairs;
                detail.Tos = value.ToPairs;
                detail.PersonId = value.PersonId;

                return detail;
            }
        }

        /// <summary>
        /// غیبت روزانه
        /// </summary>
        public virtual PersonalMonthlyReportRowDetail PairlyDailyAbsence
        {
            get
            {
                ScndCnpValue monthlyScndCnpValue = null;
                this.DailyScndCnpValueDic.TryGetValue("gridFields_DailyAbsence", out monthlyScndCnpValue);
                ScndCnpValue value = monthlyScndCnpValue == null ? new ScndCnpValue() : monthlyScndCnpValue;
                PersonalMonthlyReportRowDetail detail = new PersonalMonthlyReportRowDetail();
                detail.ImpureValue = this.DailyAbsence;
                detail.Color = !Utility.IsEmpty(value.Color) ? value.Color : "Transparent";
                detail.Date = value.FromDate;
                detail.PersonId = value.PersonId;

                if (detail.ImpureValue.Contains("1")) 
                {
                    string froms = "", tos = "";
                    foreach (IPair pair in this.CurrentShift.Pairs) 
                    {
                        froms += pair.From.ToString() + ";";
                        tos += pair.To.ToString() + ";";
                    }
                    detail.Froms = froms;
                    detail.Tos = tos;
                }

                return detail;
            }
        }


        /// <summary>
        /// ماموریت شبانه روزی
        /// </summary>
        public virtual PersonalMonthlyReportRowDetail PairlyDailyNightMission
        {
            get
            {               
                PersonalMonthlyReportRowDetail detail = new PersonalMonthlyReportRowDetail();
                detail.Color = "Transparent";
                return detail;
            }
        }

        /// <summary>
        /// ماموریت روزانه
        /// </summary>
        public virtual PersonalMonthlyReportRowDetail PairlyDailyMission
        {
            get
            {
                ScndCnpValue monthlyScndCnpValue = null;
                this.DailyScndCnpValueDic.TryGetValue("gridFields_DailyMission", out monthlyScndCnpValue);
                ScndCnpValue value = monthlyScndCnpValue == null ? new ScndCnpValue() : monthlyScndCnpValue;
                PersonalMonthlyReportRowDetail detail = new PersonalMonthlyReportRowDetail();
                detail.ImpureValue = this.DailyMission;
                detail.Color = !Utility.IsEmpty(value.Color) ? value.Color : "Transparent";
                detail.Date = value.FromDate;
                detail.PersonId = value.PersonId;

                if (detail.ImpureValue.Contains("1"))
                {
                    string froms = "", tos = "";
                    foreach (IPair pair in this.CurrentShift.Pairs)
                    {
                        froms += pair.From.ToString() + ";";
                        tos += pair.To.ToString() + ";";
                    }
                    detail.Froms = froms;
                    detail.Tos = tos;
                }

                return detail;
            }
        }

        /// <summary>
        /// مرخصی روزانه استحقاقی
        /// </summary>
        public virtual PersonalMonthlyReportRowDetail PairlyDailyMeritoriouslyLeave
        {
            get
            {
                ScndCnpValue monthlyScndCnpValue = null;
                this.DailyScndCnpValueDic.TryGetValue("gridFields_DailyMeritoriouslyLeave", out monthlyScndCnpValue);
                ScndCnpValue value = monthlyScndCnpValue == null ? new ScndCnpValue() : monthlyScndCnpValue;
                PersonalMonthlyReportRowDetail detail = new PersonalMonthlyReportRowDetail();
                detail.ImpureValue = this.DailyMeritoriouslyLeave;
                detail.Color = !Utility.IsEmpty(value.Color) ? value.Color : "Transparent";
                detail.Date = value.FromDate;
                detail.PersonId = value.PersonId;

                if (detail.ImpureValue.Contains("1"))
                {
                    string froms = "", tos = "";
                    foreach (IPair pair in this.CurrentShift.Pairs)
                    {
                        froms += pair.From.ToString() + ";";
                        tos += pair.To.ToString() + ";";
                    }
                    detail.Froms = froms;
                    detail.Tos = tos;
                }

                return detail;
            }
        }

        /// <summary>
        /// مرخصی روزانه استعلاجی
        /// </summary>
        public virtual PersonalMonthlyReportRowDetail PairlyDailySickLeave
        {
            get
            {
                ScndCnpValue monthlyScndCnpValue = null;
                this.DailyScndCnpValueDic.TryGetValue("gridFields_DailySickLeave", out monthlyScndCnpValue);
                ScndCnpValue value = monthlyScndCnpValue == null ? new ScndCnpValue() : monthlyScndCnpValue;
                PersonalMonthlyReportRowDetail detail = new PersonalMonthlyReportRowDetail();
                detail.ImpureValue = this.DailySickLeave;
                detail.Color = !Utility.IsEmpty(value.Color) ? value.Color : "Transparent";
                detail.Date = value.FromDate;
                detail.PersonId = value.PersonId;

                if (detail.ImpureValue.Contains("1"))
                {
                    string froms = "", tos = "";
                    foreach (IPair pair in this.CurrentShift.Pairs)
                    {
                        froms += pair.From.ToString() + ";";
                        tos += pair.To.ToString() + ";";
                    }
                    detail.Froms = froms;
                    detail.Tos = tos;
                }

                return detail;
            }
        }

        /// <summary>
        /// مرخصی روزانه بی حقوق
        /// </summary>
        public virtual PersonalMonthlyReportRowDetail PairlyDailyWithoutPayLeave
        {
            get
            {
                ScndCnpValue monthlyScndCnpValue = null;
                this.DailyScndCnpValueDic.TryGetValue("gridFields_DailyWithoutPayLeave", out monthlyScndCnpValue);
                ScndCnpValue value = monthlyScndCnpValue == null ? new ScndCnpValue() : monthlyScndCnpValue;
                PersonalMonthlyReportRowDetail detail = new PersonalMonthlyReportRowDetail();
                detail.ImpureValue = this.DailyWithoutPayLeave;
                detail.Color = !Utility.IsEmpty(value.Color) ? value.Color : "Transparent";
                detail.Date = value.FromDate;
                detail.PersonId = value.PersonId;

                if (detail.ImpureValue.Contains("1"))
                {
                    string froms = "", tos = "";
                    foreach (IPair pair in this.CurrentShift.Pairs)
                    {
                        froms += pair.From.ToString() + ";";
                        tos += pair.To.ToString() + ";";
                    }
                    detail.Froms = froms;
                    detail.Tos = tos;
                }

                return detail;
            }
        }

        /// <summary>
        /// مرخصی روزانه با حقوق
        /// </summary>
        public virtual PersonalMonthlyReportRowDetail PairlyDailyWithPayLeave
        {
            get
            {
                ScndCnpValue monthlyScndCnpValue = null;
                this.DailyScndCnpValueDic.TryGetValue("gridFields_DailyWithPayLeave", out monthlyScndCnpValue);
                ScndCnpValue value = monthlyScndCnpValue == null ? new ScndCnpValue() : monthlyScndCnpValue;
                PersonalMonthlyReportRowDetail detail = new PersonalMonthlyReportRowDetail();
                detail.ImpureValue = this.DailyWithPayLeave;
                detail.Color = !Utility.IsEmpty(value.Color) ? value.Color : "Transparent";
                detail.Date = value.FromDate;
                detail.PersonId = value.PersonId;

                if (detail.ImpureValue.Contains("1"))
                {
                    string froms = "", tos = "";
                    foreach (IPair pair in this.CurrentShift.Pairs)
                    {
                        froms += pair.From.ToString() + ";";
                        tos += pair.To.ToString() + ";";
                    }
                    detail.Froms = froms;
                    detail.Tos = tos;
                }

                return detail;
            }
        }



        #endregion

        #region ماهانه

        /// <summary>
        /// Gets or sets the NecessaryOperation value.
        /// </summary>
        public virtual String PeriodicNecessaryOperation
        {
            get
            {
                ScndCnpValue PeriodicScndCnpValue = null;
                this.PeriodicScndCnpValueDic.TryGetValue("gridFields_NecessaryOperation", out PeriodicScndCnpValue);
                string value = Utility.IntTimeToTime(PeriodicScndCnpValue == null ? 0 : (int)PeriodicScndCnpValue.PeriodicValue);
                if (LanguagesName == LanguagesName.Parsi)
                {
                    value = Utility.ToParsiCharacter(value);
                }
                return value;
            }
        }

        /// <summary>
        /// Gets or sets the HourlyPureOperation value.
        /// </summary>
        public virtual String PeriodicHourlyPureOperation
        {
            get
            {
                ScndCnpValue PeriodicScndCnpValue = null;
                this.PeriodicScndCnpValueDic.TryGetValue("gridFields_HourlyPureOperation", out PeriodicScndCnpValue);
                string value = Utility.IntTimeToTime(PeriodicScndCnpValue == null ? 0 : (int)PeriodicScndCnpValue.PeriodicValue);
                if (LanguagesName == LanguagesName.Parsi)
                {
                    value = Utility.ToParsiCharacter(value);
                }
                return value;
            }
        }

        /// <summary>
        /// Gets or sets the DailyPureOperation value.
        /// </summary>
        public virtual String PeriodicDailyPureOperation
        {
            get
            {
                ScndCnpValue PeriodicScndCnpValue = null;
                this.PeriodicScndCnpValueDic.TryGetValue("gridFields_DailyPureOperation", out PeriodicScndCnpValue);
                string value = PeriodicScndCnpValue == null ? "0" : PeriodicScndCnpValue.PeriodicValue.ToString();
                if (LanguagesName == LanguagesName.Parsi)
                {
                    value = Utility.ToParsiCharacter(value);
                }
                return value;
            }
        }

        /// <summary>
        /// Gets or sets the ImpureOperation value.
        /// </summary>
        public virtual String PeriodicImpureOperation
        {
            get
            {
                ScndCnpValue PeriodicScndCnpValue = null;
                this.PeriodicScndCnpValueDic.TryGetValue("gridFields_ImpureOperation", out PeriodicScndCnpValue);
                string value = Utility.IntTimeToTime(PeriodicScndCnpValue == null ? 0 : (int)PeriodicScndCnpValue.PeriodicValue);
                if (LanguagesName == LanguagesName.Parsi)
                {
                    value = Utility.ToParsiCharacter(value);
                }
                return value;
            }
        }

        /// <summary>
        /// اضافه کار ماهانه
        /// اگر برابر 1 بود یعنی با اضافه کار جبران شده پس خالی برمیگردانیم
        /// </summary>
        public virtual String PeriodicAllowableOverTime
        {
            get
            {
                ScndCnpValue PeriodicScndCnpValue = null;
                this.PeriodicScndCnpValueDic.TryGetValue("gridFields_AllowableOverTime", out PeriodicScndCnpValue);
                string value = Utility.IntTimeToTime(PeriodicScndCnpValue == null ? 0 : (int)PeriodicScndCnpValue.PeriodicValue == 1 ? 0 : (int)PeriodicScndCnpValue.PeriodicValue);
                if (LanguagesName == LanguagesName.Parsi)
                {
                    value = Utility.ToParsiCharacter(value);
                }
                return value;
            }
        }

        /// <summary>
        /// Gets or sets the UnallowableOverTime value.
        /// </summary>
        public virtual String PeriodicUnallowableOverTime
        {
            get
            {
                ScndCnpValue PeriodicScndCnpValue = null;
                this.PeriodicScndCnpValueDic.TryGetValue("gridFields_UnallowableOverTime", out PeriodicScndCnpValue);
                string value = Utility.IntTimeToTime(PeriodicScndCnpValue == null ? 0 : (int)PeriodicScndCnpValue.PeriodicValue);
                if (LanguagesName == LanguagesName.Parsi)
                {
                    value = Utility.ToParsiCharacter(value);
                }
                return value;
            }
        }

        /// <summary>
        /// Gets or sets the HourlyAllowableAbsence value.
        /// </summary>
        public virtual String PeriodicHourlyAllowableAbsence
        {
            get
            {
                ScndCnpValue PeriodicScndCnpValue = null;
                this.PeriodicScndCnpValueDic.TryGetValue("gridFields_HourlyAllowableAbsence", out PeriodicScndCnpValue);
                string value = Utility.IntTimeToTime(PeriodicScndCnpValue == null ? 0 : (int)PeriodicScndCnpValue.PeriodicValue);
                if (LanguagesName == LanguagesName.Parsi)
                {
                    value = Utility.ToParsiCharacter(value);
                }
                return value;
            }
        }

        /// <summary>
        /// غیبت ساعتی ماهانه
        /// اگر برابر 1 بود یعنی با اضافه کار جبران شده پس خالی برمیگردانیم
        /// 
        /// </summary>
        public virtual String PeriodicHourlyUnallowableAbsence
        {
            get
            {
                ScndCnpValue PeriodicScndCnpValue = null;
                this.PeriodicScndCnpValueDic.TryGetValue("gridFields_HourlyUnallowableAbsence", out PeriodicScndCnpValue);
                string value = Utility.IntTimeToTime(PeriodicScndCnpValue == null ? 0 : (int)PeriodicScndCnpValue.PeriodicValue == 1 ? 0 : (int)PeriodicScndCnpValue.PeriodicValue);
                if (LanguagesName == LanguagesName.Parsi)
                {
                    value = Utility.ToParsiCharacter(value);
                }                
                return value;
            }            
        }

        /// <summary>
        /// Gets or sets the DailyAbsence value.
        /// </summary>
        public virtual String PeriodicDailyAbsence
        {
            get
            {
                ScndCnpValue PeriodicScndCnpValue = null;
                this.PeriodicScndCnpValueDic.TryGetValue("gridFields_DailyAbsence", out PeriodicScndCnpValue);
                string value = PeriodicScndCnpValue == null ? "0" : PeriodicScndCnpValue.PeriodicValue.ToString();
                if (LanguagesName == LanguagesName.Parsi)
                {
                    value = Utility.ToParsiCharacter(value);
                }
                return value;
            }
        }

        /// <summary>
        /// Gets or sets the HourlyMission value.
        /// </summary>
        public virtual String PeriodicHourlyMission
        {
            get
            {
                ScndCnpValue PeriodicScndCnpValue = null;
                this.PeriodicScndCnpValueDic.TryGetValue("gridFields_HourlyMission", out PeriodicScndCnpValue);
                string value = Utility.IntTimeToTime(PeriodicScndCnpValue == null ? 0 : (int)PeriodicScndCnpValue.PeriodicValue);
                if (LanguagesName == LanguagesName.Parsi)
                {
                    value = Utility.ToParsiCharacter(value);
                }
                return value;
            }
        }

        /// <summary>
        /// Gets or sets the DailyMission value.
        /// </summary>
        public virtual String PeriodicDailyMission
        {
            get
            {
                ScndCnpValue PeriodicScndCnpValue = null;
                this.PeriodicScndCnpValueDic.TryGetValue("gridFields_DailyMission", out PeriodicScndCnpValue);
                string value = PeriodicScndCnpValue == null ? "0" : PeriodicScndCnpValue.PeriodicValue.ToString();
                if (LanguagesName == LanguagesName.Parsi)
                {
                    value = Utility.ToParsiCharacter(value);
                }
                return value;
            }
        }

        /// <summary>
        /// Gets or sets the HostelryMission value.
        /// </summary>
        public virtual String PeriodicHostelryMission
        {
            get
            {
                ScndCnpValue PeriodicScndCnpValue = null;
                this.PeriodicScndCnpValueDic.TryGetValue("gridFields_HostelryMission", out PeriodicScndCnpValue);
                string value = Utility.IntTimeToTime(PeriodicScndCnpValue == null ? 0 : (int)PeriodicScndCnpValue.PeriodicValue);
                if (LanguagesName == LanguagesName.Parsi)
                {
                    value = Utility.ToParsiCharacter(value);
                }
                return value;
            }
        }

        /// <summary>
        /// Gets or sets the HourlySickLeave value.
        /// </summary>
        public virtual String PeriodicHourlySickLeave
        {
            get
            {
                ScndCnpValue PeriodicScndCnpValue = null;
                this.PeriodicScndCnpValueDic.TryGetValue("gridFields_HourlySickLeave", out PeriodicScndCnpValue);
                string value = Utility.IntTimeToTime(PeriodicScndCnpValue == null ? 0 : (int)PeriodicScndCnpValue.PeriodicValue);
                if (LanguagesName == LanguagesName.Parsi)
                {
                    value = Utility.ToParsiCharacter(value);
                }
                return value;
            }
        }

        /// <summary>
        /// Gets or sets the DailySickLeave value.
        /// </summary>
        public virtual String PeriodicDailySickLeave
        {
            get
            {
                ScndCnpValue PeriodicScndCnpValue = null;
                this.PeriodicScndCnpValueDic.TryGetValue("gridFields_DailySickLeave", out PeriodicScndCnpValue);
                string value = PeriodicScndCnpValue == null ? "0" : PeriodicScndCnpValue.PeriodicValue.ToString();
                if (LanguagesName == LanguagesName.Parsi)
                {
                    value = Utility.ToParsiCharacter(value);
                }
                return value;
            }
        }

        /// <summary>
        /// Gets or sets the HourlyMeritoriouslyLeave value.
        /// </summary>
        public virtual String PeriodicHourlyMeritoriouslyLeave
        {
            get
            {
                ScndCnpValue PeriodicScndCnpValue = null;
                this.PeriodicScndCnpValueDic.TryGetValue("gridFields_HourlyMeritoriouslyLeave", out PeriodicScndCnpValue);
                string value = Utility.IntTimeToTime(PeriodicScndCnpValue == null ? 0 : (int)PeriodicScndCnpValue.PeriodicValue);
                if (LanguagesName == LanguagesName.Parsi)
                {
                    value = Utility.ToParsiCharacter(value);
                }
                return value;
            }
        }

        /// <summary>
        /// Gets or sets the DailyMeritoriouslyLeave value.
        /// </summary>
        public virtual String PeriodicDailyMeritoriouslyLeave
        {
            get
            {
                ScndCnpValue PeriodicScndCnpValue = null;
                this.PeriodicScndCnpValueDic.TryGetValue("gridFields_DailyMeritoriouslyLeave", out PeriodicScndCnpValue);
                string value = PeriodicScndCnpValue == null ? "0" : PeriodicScndCnpValue.PeriodicValue.ToString();
                if (LanguagesName == LanguagesName.Parsi)
                {
                    value = Utility.ToParsiCharacter(value);
                }
                return value;
            }
        }

        /// <summary>
        /// Gets or sets the HourlyWithoutPayLeave value.
        /// </summary>
        public virtual String PeriodicHourlyWithoutPayLeave
        {
            get
            {
                ScndCnpValue PeriodicScndCnpValue = null;
                this.PeriodicScndCnpValueDic.TryGetValue("gridFields_HourlyWithoutPayLeave", out PeriodicScndCnpValue);
                string value = Utility.IntTimeToTime(PeriodicScndCnpValue == null ? 0 : (int)PeriodicScndCnpValue.PeriodicValue);
                if (LanguagesName == LanguagesName.Parsi)
                {
                    value = Utility.ToParsiCharacter(value);
                }
                return value;
            }
        }

        /// <summary>
        /// Gets or sets the PresenceDuration value.
        /// </summary>
        public virtual String PeriodicPresenceDuration
        {
            get
            {
                ScndCnpValue PeriodicScndCnpValue = null;
                this.PeriodicScndCnpValueDic.TryGetValue("gridFields_PresenceDuration", out PeriodicScndCnpValue);
                string value = Utility.IntTimeToTime(PeriodicScndCnpValue == null ? 0 : (int)PeriodicScndCnpValue.PeriodicValue);
                if (LanguagesName == LanguagesName.Parsi)
                {
                    value = Utility.ToParsiCharacter(value);
                }
                return value;
            }
        }

        /// <summary>
        /// Gets or sets the DailyWithoutPayLeave value.
        /// </summary>
        public virtual String PeriodicDailyWithoutPayLeave
        {
            get
            {
                ScndCnpValue PeriodicScndCnpValue = null;
                this.PeriodicScndCnpValueDic.TryGetValue("gridFields_DailyWithoutPayLeave", out PeriodicScndCnpValue);
                string value = PeriodicScndCnpValue == null ? "0" : PeriodicScndCnpValue.PeriodicValue.ToString();
                if (LanguagesName == LanguagesName.Parsi)
                {
                    value = Utility.ToParsiCharacter(value);
                }
                return value;
            }
        }

        /// <summary>
        /// Gets or sets the HourlyWithPayLeave value.
        /// </summary>
        public virtual String PeriodicHourlyWithPayLeave
        {
            get
            {
                ScndCnpValue PeriodicScndCnpValue = null;
                this.PeriodicScndCnpValueDic.TryGetValue("gridFields_HourlyWithPayLeave", out PeriodicScndCnpValue);
                string value = Utility.IntTimeToTime(PeriodicScndCnpValue == null ? 0 : (int)PeriodicScndCnpValue.PeriodicValue);
                if (LanguagesName == LanguagesName.Parsi)
                {
                    value = Utility.ToParsiCharacter(value);
                }
                return value;
            }
        }

        /// <summary>
        /// Gets or sets the DailyWithPayLeave value.
        /// </summary>
        public virtual String PeriodicDailyWithPayLeave
        {
            get
            {
                ScndCnpValue PeriodicScndCnpValue = null;
                this.PeriodicScndCnpValueDic.TryGetValue("gridFields_DailyWithPayLeave", out PeriodicScndCnpValue);
                string value = PeriodicScndCnpValue == null ? "0" : PeriodicScndCnpValue.PeriodicValue.ToString();
                if (LanguagesName == LanguagesName.Parsi)
                {
                    value = Utility.ToParsiCharacter(value);
                }
                return value;
            }
        }

        /// <summary>
        /// رزرو ماهانه روزانه 1
        /// </summary>
        public virtual String PeriodicReserveField1
        {
            get
            {
                ScndCnpValue PeriodicScndCnpValue = null;
                this.PeriodicScndCnpValueDic.TryGetValue("gridFields_ReserveField1", out PeriodicScndCnpValue);
                string value = PeriodicScndCnpValue == null ? "0" : PeriodicScndCnpValue.PeriodicValue.ToString();
                if (LanguagesName == LanguagesName.Parsi)
                {
                    value = Utility.ToParsiCharacter(value);
                }
                if (value.Equals("-1"))
                {
                    value = "" ;
                }
                return value;          
            }
        }

        /// <summary>
        /// رزرو ماهانه روزانه 2
        /// </summary>
        public virtual String PeriodicReserveField2
        {
            get
            {
                ScndCnpValue PeriodicScndCnpValue = null;
                this.PeriodicScndCnpValueDic.TryGetValue("gridFields_ReserveField2", out PeriodicScndCnpValue);
                string value = PeriodicScndCnpValue == null ? "0" : PeriodicScndCnpValue.PeriodicValue.ToString();
                if (LanguagesName == LanguagesName.Parsi)
                {
                    value = Utility.ToParsiCharacter(value);
                }
                if (value.Equals("-1"))
                {
                    value = "" ;
                }
                return value;
            }
        }

        /// <summary>
        /// رزرو ماهانه روزانه 3
        /// </summary>
        public virtual String PeriodicReserveField3
        {
            get
            {
                ScndCnpValue PeriodicScndCnpValue = null;
                this.PeriodicScndCnpValueDic.TryGetValue("gridFields_ReserveField3", out PeriodicScndCnpValue);
                string value = PeriodicScndCnpValue == null ? "0" : PeriodicScndCnpValue.PeriodicValue.ToString();
                if (LanguagesName == LanguagesName.Parsi)
                {
                    value = Utility.ToParsiCharacter(value);
                }
                if (value.Equals("-1"))
                {
                    value = "" ;
                }
                return value;
            }
        }

        /// <summary>
        /// رزرو ماهانه روزانه 4
        /// </summary>
        public virtual String PeriodicReserveField4
        {
            get
            {
                ScndCnpValue PeriodicScndCnpValue = null;
                this.PeriodicScndCnpValueDic.TryGetValue("gridFields_ReserveField4", out PeriodicScndCnpValue);
                string value = PeriodicScndCnpValue == null ? "0" : PeriodicScndCnpValue.PeriodicValue.ToString();
                if (LanguagesName == LanguagesName.Parsi)
                {
                    value = Utility.ToParsiCharacter(value);
                }
                if (value.Equals("-1"))
                {
                    value = "" ;
                }
                return value;
            }
        }

        /// <summary>
        /// Gets or sets the ReserveField5 value.
        /// </summary>
        public virtual String PeriodicReserveField5
        {
            get
            {
                ScndCnpValue PeriodicScndCnpValue = null;
                this.PeriodicScndCnpValueDic.TryGetValue("gridFields_ReserveField5", out PeriodicScndCnpValue);
                string value = Utility.IntTimeToTime(PeriodicScndCnpValue == null ? 0 : (int)PeriodicScndCnpValue.PeriodicValue);
                if (LanguagesName == LanguagesName.Parsi)
                {
                    value = Utility.ToParsiCharacter(value);
                }
                if (value.Equals("-1"))
                {
                    value = "" ;
                }
                return value;
            }
        }

        /// <summary>
        /// Gets or sets the ReserveField6 value.
        /// </summary>
        public virtual String PeriodicReserveField6
        {
            get
            {
                ScndCnpValue PeriodicScndCnpValue = null;
                this.PeriodicScndCnpValueDic.TryGetValue("gridFields_ReserveField6", out PeriodicScndCnpValue);
                string value = Utility.IntTimeToTime(PeriodicScndCnpValue == null ? 0 : (int)PeriodicScndCnpValue.PeriodicValue);
                if (LanguagesName == LanguagesName.Parsi)
                {
                    value = Utility.ToParsiCharacter(value);
                }
                if (value.Equals("-1"))
                {
                    value = "" ;
                }
                return value;
            }
        }

        /// <summary>
        /// Gets or sets the ReserveField7 value.
        /// </summary>
        public virtual String PeriodicReserveField7
        {
            get
            {
                ScndCnpValue PeriodicScndCnpValue = null;
                this.PeriodicScndCnpValueDic.TryGetValue("gridFields_ReserveField7", out PeriodicScndCnpValue);
                string value = Utility.IntTimeToTime(PeriodicScndCnpValue == null ? 0 : (int)PeriodicScndCnpValue.PeriodicValue);
                if (LanguagesName == LanguagesName.Parsi)
                {
                    value = Utility.ToParsiCharacter(value);
                }
                if (value.Equals("-1"))
                {
                    value = "" ;
                }
                return value;
            }
        }

        /// <summary>
        /// Gets or sets the ReserveField8 value.
        /// </summary>
        public virtual String PeriodicReserveField8
        {
            get
            {
                ScndCnpValue PeriodicScndCnpValue = null;
                this.PeriodicScndCnpValueDic.TryGetValue("gridFields_ReserveField8", out PeriodicScndCnpValue);
                string value = Utility.IntTimeToTime(PeriodicScndCnpValue == null ? 0 : (int)PeriodicScndCnpValue.PeriodicValue);
                if (LanguagesName == LanguagesName.Parsi)
                {
                    value = Utility.ToParsiCharacter(value);
                }
                if (value.Equals("-1"))
                {
                    value = "" ;
                }
                return value;
            }
        }

        /// <summary>
        /// Gets or sets the ReserveField9 value.
        /// </summary>
        public virtual String PeriodicReserveField9
        {
            get
            {
                ScndCnpValue PeriodicScndCnpValue = null;
                this.PeriodicScndCnpValueDic.TryGetValue("gridFields_ReserveField9", out PeriodicScndCnpValue);
                string value = Utility.IntTimeToTime(PeriodicScndCnpValue == null ? 0 : (int)PeriodicScndCnpValue.PeriodicValue);
                if (LanguagesName == LanguagesName.Parsi)
                {
                    value = Utility.ToParsiCharacter(value);
                }
                if (value.Equals("-1"))
                {
                    value = "" ;
                }
                return value;
            }
        }

        /// <summary>
        /// Gets or sets the ReserveField10 value.
        /// </summary>
        public virtual String PeriodicReserveField10
        {
            get
            {
                ScndCnpValue PeriodicScndCnpValue = null;
                this.PeriodicScndCnpValueDic.TryGetValue("gridFields_ReserveField10", out PeriodicScndCnpValue);
                string value = Utility.IntTimeToTime(PeriodicScndCnpValue == null ? 0 : (int)PeriodicScndCnpValue.PeriodicValue);
                if (LanguagesName == LanguagesName.Parsi)
                {
                    value = Utility.ToParsiCharacter(value);
                }
                if (value.Equals("-1"))
                {
                    value = "" ;
                }
                return value;
            }
        }


        #endregion

        #region Colors

        public virtual String FirstEntrance_BC
        {
            get;
            set;
        }

        public virtual String FirstExit_BC
        {
            get;
            set;
        }

        public virtual String SecondEntrance_BC
        {
            get;
            set;
        }

        public virtual String SecondExit_BC
        {
            get;
            set;
        }

        public virtual String ThirdEntrance_BC
        {
            get;
            set;

        }

        public virtual String ThirdExit_BC
        {
            get;
            set;

        }

        public virtual String FourthEntrance_BC
        {
            get;
            set;

        }

        public virtual String FourthExit_BC
        {
            get;
            set;
        }

        public virtual String FifthEntrance_BC
        {
            get;
            set;
        }

        public virtual String FifthExit_BC
        {
            get;
            set;
        }

        public virtual String LastExit_BC
        {
            get
            {
                return WHITE;
            }
        }

        public virtual String HourlyUnallowableAbsence_BC
        {
            get
            {
                return WHITE;
            }
        }

        public virtual String DailyAbsence_BC
        {
            get
            {
                return WHITE;
            }
        }

        /// <summary>
        /// رنگ وضعیت روز
        /// </summary>
        public virtual String DayState_BC
        {
            get
            {
                if (Utility.IsEmpty(this.dayState_BC))
                {
                    SetColors();
                }
                return this.dayState_BC;
            }
            set
            {
                this.dayState_BC = value;
            }
        }

        /// <summary>
        /// نام وضعیت روز
        /// </summary>
        public virtual String DayStateTitle
        {
            get
            {
                if (Utility.IsEmpty(this.dayStateTitle))
                {
                    SetColors();
                }
                return this.dayStateTitle;
            }
            set
            {
                this.dayStateTitle = value;
            }
        }

        /// <summary>
        /// رنگ متن نام روز هفته اگر تعطیل بود قرمز شود
        /// </summary>
        public virtual String DayHolidayState_BC
        {
            get
            {
                if (Utility.IsEmpty(this.dayHolidayState_BC))
                {
                    SetColors();
                }
                return this.dayHolidayState_BC;
            }
            set
            {
                this.dayHolidayState_BC = value;
            }
        }

        #endregion

        public virtual IDictionary<string, ScndCnpValue> PeriodicScndCnpValueDic
        {
            get;
            set;
        }

        public virtual IList<CurrentProceedTraffic> DailyProceedTrafficList
        {
            get;
            set;
        }

        public virtual IDictionary<string, ScndCnpValue> DailyScndCnpValueDic
        {
            get;
            set;
        }

        public override string ToString()
        {
            return string.Format(" {0}:{1} ", this.DayName, this.Date);
        }

        private void SetColors()
        {
            //this.SecondEntrance_BC = "";
            if (!this.dayStatesIsSet)
            {
                ScndCnpValue monthlyScndCnpValue = null;
                this.DailyScndCnpValueDic.TryGetValue("gridFields_DayStateCode", out monthlyScndCnpValue);
                //Style: DayColor;HolidayColor
                if (monthlyScndCnpValue != null)
                {
                    int numberOfColors = Utility.Spilit(monthlyScndCnpValue.FromPairs, ";").Count();
                    int valueCode = (int)monthlyScndCnpValue.Value;
                    if (numberOfColors >= 2) 
                    {
                        this.dayState_BC = Utility.Spilit(monthlyScndCnpValue.FromPairs, ';')[0];
                        this.dayHolidayState_BC = Utility.Spilit(monthlyScndCnpValue.FromPairs, ';')[1];
                    }
                    //if ((valueCode & 2) > 0) //holiday
                    //{
                    //    if (monthlyScndCnpValue.FromPairs.Contains(';'))
                    //    {
                    //        this.dayState_BC = Utility.Spilit(monthlyScndCnpValue.FromPairs, ';')[0];
                    //        this.dayHolidayState_BC = Utility.Spilit(monthlyScndCnpValue.FromPairs, ';')[1];
                    //    }
                    //    else
                    //    {
                    //        this.dayHolidayState_BC = monthlyScndCnpValue.FromPairs;
                    //    }
                    //}
                    if (numberOfColors >= 3)
                    {
                        if ((valueCode & 4) > 0) //first Entrance
                        {
                            this.FirstEntrance_BC = Utility.Spilit(monthlyScndCnpValue.FromPairs, ';')[2];
                        }
                        if ((valueCode & 8) > 0) //first Exit
                        {
                            this.FirstExit_BC = Utility.Spilit(monthlyScndCnpValue.FromPairs, ';')[2];
                        }
                        if ((valueCode & 16) > 0) //second Entrance
                        {
                            this.SecondEntrance_BC = Utility.Spilit(monthlyScndCnpValue.FromPairs, ';')[2];
                        }
                        if ((valueCode & 32) > 0) //second Exit
                        {
                            this.SecondExit_BC = Utility.Spilit(monthlyScndCnpValue.FromPairs, ';')[2];
                        }
                        if ((valueCode & 64) > 0) //third Entrance
                        {
                            this.ThirdEntrance_BC = Utility.Spilit(monthlyScndCnpValue.FromPairs, ';')[2];
                        }
                        if ((valueCode & 128) > 0) //third Exit
                        {
                            this.ThirdExit_BC = Utility.Spilit(monthlyScndCnpValue.FromPairs, ';')[2];
                        }
                        if ((valueCode & 256) > 0) //4th Entrance
                        {
                            this.FourthEntrance_BC = Utility.Spilit(monthlyScndCnpValue.FromPairs, ';')[2];
                        }
                        if ((valueCode & 512) > 0) //4th Exit
                        {
                            this.FourthExit_BC = Utility.Spilit(monthlyScndCnpValue.FromPairs, ';')[2];
                        }
                        if ((valueCode & 1024) > 0) //5th Entrance
                        {
                            this.FifthEntrance_BC = Utility.Spilit(monthlyScndCnpValue.FromPairs, ';')[2];
                        }
                        if ((valueCode & 2048) > 0) //5th Exit
                        {
                            this.FifthExit_BC = Utility.Spilit(monthlyScndCnpValue.FromPairs, ';')[2];
                        }
                    }
                    //else
                    //{
                    //    this.dayState_BC = monthlyScndCnpValue.FromPairs;
                    //}

                    if (!Utility.IsEmpty(monthlyScndCnpValue.ToPairs))
                    {
                        this.dayStateTitle = monthlyScndCnpValue.ToPairs;
                    }
                }
                this.dayStatesIsSet = true;
            }
        }

        #endregion

        private BaseShift CurrentShift
        {
            get
            {
                BaseShift sh = this.Person.GetShiftByDate(this.Date);
                return sh == null ? new BaseShift() : sh;
            }
        }
    }
}