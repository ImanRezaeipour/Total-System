using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using GTS.Clock.ModelEngine;
using System.Reflection;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Infrastructure.Log;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.ModelEngine.Concepts;
using GTS.Clock.ModelEngine.Concepts.Operations;
using GTS.Clock.ModelEngine.AppSetting;
using GTS.Clock.ModelEngine.ELE;

namespace GTS.Clock.Business.Calculator
{
    public class RuleCalculator : GeneralRuleCalculator
    {

        #region Constructors
        /// <summary>
        /// ."تنها سازنده کلاس "محاسبه گر اشياء
        /// </summary>
        /// <param name="Person">پرسنلي که محاسبات براي او در حال انجام است</param>
        /// <param name="CategorisedRule">قانوني که منجر به فراخواني مفاهيم از کلاس "محاسبه گر قانون" خواهد شد</param>
        /// <param name="CalculateDate">تاريخ انجام محاسبات</param>
        public RuleCalculator(IEngineEnvironment engineEnvironment)
            : base(engineEnvironment, new ConceptCalculator(engineEnvironment))
        {
            this.logLock = !GTSAppSettings.RuleDebug;
        }
        #endregion

        #region Defined Methods

        #region قوانين متفرقه
       
        #endregion

        #region قوانين کارکرد

        #endregion

        #region قوانين مرخصي

        /// <summary>
        /// مرخصی ساعتی چنانچه از ... تا ... بود به میزان ... ساعت لحاظ گردد
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R3501(AssignedRule MyRule)
        {
            var from = MyRule["First", this.RuleCalculateDate].ToInt();
            var to = MyRule["Second", this.RuleCalculateDate].ToInt();
            var maxLeave = MyRule["Third", this.RuleCalculateDate].ToInt();
            GetLog(MyRule, DebugRuleState.Before, 1003,1090);
            // نگهداری شناسه های مفاهیم
            var conceptList = new List<int>();
            if (this.DoConcept(1003).Value > from && this.DoConcept(1003).Value <= to)
            {
                this.Person.AddUsedLeave(this.RuleCalculateDate, maxLeave - this.DoConcept(1003).Value, null);
                this.DoConcept(1003).Value = maxLeave;
            }
            this.ReCalculate(1090);
            GetLog(MyRule, DebugRuleState.After, 1003,1090);
        }


        /// <summary>
        /// سقف مرخصی ساعتی (بی حقوق 12)
        /// </summary>
        /// <param name="MyRule"></param>
        public virtual void R3502(AssignedRule MyRule)
        {
            //1501 سقف بی حقوق ساعتی 12
            var maxLeave = MyRule["First", this.RuleCalculateDate].ToInt();
            GetLog(MyRule, DebugRuleState.Before , 1501);
            this.DoConcept(1501).Value = maxLeave;
            GetLog(MyRule, DebugRuleState.After, 1501);
        }

       

        #endregion

        #region قوانين ماموريت

        public override void R4003(AssignedRule MyRule)
        {
            //ماموريت ساعتي 2004
            //4007 مفهوم اضافه کارساعتي بعد ازوقت           
            //4008 مفهوم اضافه کارساعتي قبل ازوقت
            //2012 مجوز ماموريت در ساعات غيرکاري           
            //4001 اضافه خالص کارساعتي
            //4002 اضافه کارساعتي
            //2023 مفهوم مجموع ماموريت ساعتي           
            //2028 مفهوم ماموریت خارج از شیفت در روز کاری
            GetLog(MyRule, DebugRuleState.Before, 13, 4008, 2004, 4007, 2012, 4002, 2023);
            if (this.DoConcept(2028).Value > 0)
            {
                foreach (IPair pair in ((PairableScndCnpValue)this.DoConcept(2028)).Pairs)
                {
                    ((PairableScndCnpValue)this.DoConcept(4001))
                        .AppendPairs(Operation.Differance(pair, this.Person.GetShiftByDate(this.RuleCalculateDate)));

                    ((PairableScndCnpValue)this.DoConcept(4002))
                        .AppendPairs(Operation.Differance(pair, this.Person.GetShiftByDate(this.RuleCalculateDate)));

                    if (Operation.Intersect(pair, this.DoConcept(2004)).Value == 0)
                    {
                        ((PairableScndCnpValue)this.DoConcept(2004)).AppendPair(pair);
                    }
                }
                this.ReCalculate(13, 2023, 4007, 4008);
            }
            GetLog(MyRule, DebugRuleState.After , 2004, 4007, 2012, 4002, 2023);
        }
        #endregion

        #region قوانين کم کاري

     

        /// <summary>
        /// شیردهی
        /// </summary>
        /// <param name="MyRule"></param>
        //public override void R5021(AssignedRule MyRule)
        //{
        //    //3020 غیبت ساعتی مجاز
        //    //3029 تاخیر
        //    //3030 تعجیل
        //    //3028 غیبت ساعتی
        //    //3021 تاخیر مجاز
        //    //3022 تعجیل مجاز
        //    //3040 غیبت مجاز شیردهی
        //    //1082 مجموع انواع مرخصی ساعتی
        //    //2023 مفهوم مجموع ماموريت ساعتي
        //    var conceptList = new[] { 2, 3020, 3028, 3040 };


        //    GetLog(MyRule, " Before Execute State:", conceptList);

        //    this.DoConcept(1082);
        //    this.DoConcept(2023);

        //    var personParam_takhir = this.Person.PersonTASpec.GetParamValue(this.Person.ID, "kasre_shirdehi_takhir", this.RuleCalculateDate);
        //    var personParam_tajil = this.Person.PersonTASpec.GetParamValue(this.Person.ID, "kasre_shirdehi_tajil", this.RuleCalculateDate);

        //    if (
        //        this.Person.GetShiftByDate(this.RuleCalculateDate).Value > 0 &&
        //        this.DoConcept(1).Value > 0 &&
        //         (personParam_takhir != null || personParam_tajil != null) &&
        //        this.DoConcept(3028).Value > 0
        //        )
        //    {

        //        var minutes_takhir = personParam_takhir != null ? Utility.ToInteger(personParam_takhir.Value) : 0;
        //        var minutes_tajil = personParam_tajil != null ? Utility.ToInteger(personParam_tajil.Value) : 0;
        //        IPair takhir = ((PairableScndCnpValue)this.DoConcept(3028)).Pairs.OrderBy(x => x.From).FirstOrDefault();
        //        IPair tagil = ((PairableScndCnpValue)this.DoConcept(3028)).Pairs.OrderBy(x => x.From).LastOrDefault();

        //        IPair tempPair = null;
        //        if (takhir != null && takhir.Value > 0 && minutes_takhir>0)
        //        {
        //            if (takhir.Value >= minutes_takhir)
        //            {
        //                tempPair = new BasePair(takhir.From, takhir.From + minutes_takhir);
        //            }
        //            else
        //            {
        //                tempPair = takhir;
        //            }
        //        }
        //        else if (tagil != null && tagil.Value > 0 && minutes_tajil > 0)
        //        {
        //            if (tagil.Value >= minutes_tajil)
        //            {
        //                tempPair = new BasePair(tagil.To - minutes_tajil, tagil.To);
        //            }
        //            else
        //            {
        //                tempPair = tagil;
        //            }
        //        }
        //        if (tempPair != null && tempPair.Value > 0)
        //        {
        //            this.DoConcept(3020).Value += tempPair.Value;

        //            var pairableScndCnpValue = Operation.Differance(this.DoConcept(3028), tempPair);

        //            ((PairableScndCnpValue)this.DoConcept(3028)).AddPairs(pairableScndCnpValue.Pairs);

        //            // غيبت ساعتي مجاز شيردهي
        //            ((PairableScndCnpValue)this.DoConcept(3040)).AddPair(tempPair);

        //            ((PairableScndCnpValue)this.DoConcept(2)).AppendPair(tempPair);
        //        }
        //    }

        //    GetLog(MyRule, " After Execute State:", conceptList);
        //}

        /// <summary>
        /// کسر مهد
        /// </summary>
        /// <param name="MyRule"></param>
        public override void R5022(AssignedRule MyRule)
        {
            //2	کارکردخالص ساعتی
            //3020	غیبت ساعتی مجاز
            //3028	غیبت ساعتی غیرمجاز
            //3042	غیبت مجاز مهد
            //1082 مجموع انواع مرخصی ساعتی
            //1056 مرخصی بی حقوق 12
            //2023 مفهوم مجموع ماموريت ساعتي
            //3029  تاخیر
            //3030تعجیل
            GetLog(MyRule, DebugRuleState.Before ,1082,1056,2023, 2, 3042, 3020, 3028,3029,3030);
            this.DoConcept(1082);
            this.DoConcept(1056);
            this.DoConcept(2023);

            var personParam_takhir = this.Person.PersonTASpec.GetParamValue(this.Person.ID, "kasre_mahd_takhir", this.RuleCalculateDate);
            var personParam_tajil = this.Person.PersonTASpec.GetParamValue(this.Person.ID, "kasre_mahd_tajil", this.RuleCalculateDate);

            if (
                this.Person.GetShiftByDate(this.RuleCalculateDate).Value > 0 &&
                this.DoConcept(1).Value > 0 &&
                (personParam_takhir != null || personParam_tajil != null) &&
                this.DoConcept(3028).Value > 0
                )
            {
                int startOfShift = this.Person.GetShiftByDate(this.RuleCalculateDate).Pairs.OrderBy(x => x.From).First().From;
                int endOfShift = this.Person.GetShiftByDate(this.RuleCalculateDate).Pairs.OrderBy(x => x.From).Last().To;

                var minutes_takhir = personParam_takhir != null ? Utility.ToInteger(personParam_takhir.Value) : 0;
                var minutes_tajil = personParam_tajil != null ? Utility.ToInteger(personParam_tajil.Value) : 0;
              
                //IPair takhir = ((PairableScndCnpValue)this.DoConcept(3028)).Pairs.Where(x=>x.From==startOfShift) .OrderBy(x => x.From).FirstOrDefault();
                //IPair tagil = ((PairableScndCnpValue)this.DoConcept(3028)).Pairs.Where(x => x.To == endOfShift).OrderBy(x => x.From).LastOrDefault();

                IPair takhir = ((PairableScndCnpValue)this.DoConcept(3029)).Pairs.OrderBy(x => x.From).FirstOrDefault();
                IPair tagil = ((PairableScndCnpValue)this.DoConcept(3030)).Pairs.OrderBy(x => x.From).LastOrDefault();
               
                IPair tempPair = null;
                if (takhir != null && takhir.Value > 0 && takhir.Value <= minutes_takhir)
                {
                    if (takhir.Value >= minutes_takhir)
                    {
                        tempPair = new BasePair(takhir.From, takhir.From + minutes_takhir);
                    }
                    else
                    {
                        tempPair = takhir;
                    }
                }
                else if (tagil != null && tagil.Value > 0 && tagil.Value <= minutes_tajil)
                {
                    if (tagil.Value >= minutes_tajil)
                    {
                        tempPair = new BasePair(tagil.To - minutes_tajil, tagil.To);
                    }
                    else
                    {
                        tempPair = tagil;
                    }
                }
                if (tempPair != null && tempPair.Value > 0)
                {
                    this.DoConcept(3020).Value += tempPair.Value;

                    var pairableScndCnpValue = Operation.Differance(this.DoConcept(3028), tempPair);
                    ((PairableScndCnpValue)this.DoConcept(3028)).AddPairs(pairableScndCnpValue.Pairs);

                    pairableScndCnpValue = Operation.Differance(this.DoConcept(3029), tempPair);
                    ((PairableScndCnpValue)this.DoConcept(3029)).AddPairs(pairableScndCnpValue.Pairs);

                    pairableScndCnpValue = Operation.Differance(this.DoConcept(3030), tempPair);
                    ((PairableScndCnpValue)this.DoConcept(3030)).AddPairs(pairableScndCnpValue.Pairs);

                    // غيبت ساعتي مجاز مهد
                    ((PairableScndCnpValue)this.DoConcept(3042)).AddPair(tempPair);

                    ((PairableScndCnpValue)this.DoConcept(2)).AppendPair(tempPair);
                }
            }
            GetLog(MyRule, DebugRuleState.After, 1082, 1056, 2023, 2, 3042, 3020, 3028, 3029, 3030);
        }

        /// <summary>
        /// کسر تقلیل
        /// </summary>
        /// <param name="MyRule"></param>
        public override void R5023(AssignedRule MyRule)
        {
            //1082 مجموع انواع مرخصی ساعتی
            //2023 مفهوم مجموع ماموريت ساعتي
            //3044 غيبت ساعتي مجاز تقليل
            GetLog(MyRule, DebugRuleState.Before, 2, 3, 13, 3020, 3028, 3044, 4002, 4005, 4006, 4007);
            this.DoConcept(1082);
            this.DoConcept(2023);
            var personParam = this.Person.PersonTASpec.GetParamValue(this.Person.ID, "kasre_taghlil", this.RuleCalculateDate);

            if (
                this.Person.GetShiftByDate(this.RuleCalculateDate).Value > 0 &&
                this.DoConcept(1).Value > 0 &&
                // مفدار غیبت مجاز برای تقلیل
                this.Person.GetShiftByDate(this.RuleCalculateDate).Value > 0 &&
                this.DoConcept(1).Value > 0 &&
                personParam != null
                )
            {
                var minutes = Utility.ToInteger(personParam.Value);

                if (MyRule["First", this.RuleCalculateDate].ToInt() == 1)
                {
                    IPair takhir = ((PairableScndCnpValue)this.DoConcept(3028)).Pairs.OrderBy(x => x.From).FirstOrDefault();
                    IPair tagil = ((PairableScndCnpValue)this.DoConcept(3028)).Pairs.OrderBy(x => x.From).LastOrDefault();
                    IPair tempPair = null;
                    #region اعمال روی غیبت

                    if (takhir != null && takhir.Value > 0)
                    {
                        if (takhir.Value >= minutes)
                        {
                            tempPair = new BasePair(takhir.From, takhir.From + minutes);
                        }
                        else
                        {
                            tempPair = takhir;
                        }
                    }
                    else if (tagil != null && tagil.Value > 0)
                    {
                        if (tagil.Value >= minutes)
                        {
                            tempPair = new BasePair(tagil.To - minutes, tagil.To);
                        }
                        else
                        {
                            tempPair = tagil;
                        }
                    }
                    if (tempPair != null && tempPair.Value > 0)
                    {
                        this.DoConcept(3020).Value += tempPair.Value;

                        var pairableScndCnpValue = Operation.Differance(this.DoConcept(3028), tempPair);

                        ((PairableScndCnpValue)this.DoConcept(3028)).AddPairs(pairableScndCnpValue.Pairs);

                        // غيبت ساعتي مجاز تقليل
                        ((PairableScndCnpValue)this.DoConcept(3044)).AppendPair(tempPair);

                        ((PairableScndCnpValue)this.DoConcept(2)).AppendPair(tempPair);
                    }

                    #endregion
                }

                if (MyRule["Second", this.RuleCalculateDate].ToInt() == 1)
                {
                    #region اعمال روی اضافه کار
                    if (minutes > 0)
                    {
                        // اعمال روی اضافه کار
                        ((PairableScndCnpValue)this.DoConcept(4002)).IncreaseValueEx(minutes);

                        var basePair = new BasePair(
                              ((PairableScndCnpValue)this.DoConcept(4002)).Pairs.OrderBy(x => x.To).Last().To - minutes,
                              ((PairableScndCnpValue)this.DoConcept(4002)).Pairs.OrderBy(x => x.To).Last().To
                              );

                        // غيبت ساعتي مجاز تقليل
                        ((PairableScndCnpValue)this.DoConcept(3044)).AddPair(basePair);

                        this.ReCalculate(3, 13, 4005, 4006, 4007);
                    }
                    #endregion
                }
            }
            GetLog(MyRule, DebugRuleState.After , 2, 3, 13, 3020, 3028, 3044, 4002, 4005, 4006, 4007);

        }

        #endregion

        #region قوانين اضافه کاري

        /// <summary>
        /// اضافه کار در مرخصی
        /// </summary>
        /// <param name="MyRule"></param>
        public override void R6022(AssignedRule MyRule)
        {
            //ماموريت روزانه 2005            
            //اضافه کار ساعتي 4002
            //1 مفهوم حضور
            if (this.DoConcept(2005).Value > 0)
            {
                GetLog(MyRule, DebugRuleState.Before,13, 4002,4003);
                if (this.DoConcept(1).Value > 0 && MyRule["First", this.RuleCalculateDate].ToInt() > 0)
                {

                    ((PairableScndCnpValue)this.DoConcept(4002)).AddPairs(((PairableScndCnpValue)this.DoConcept(1)).Pairs);

                    //float coEfficient = 1 + (MyRule["First", this.RuleCalculateDate].ToInt() / 100f);
                    var coEfficient = (int)Math.Round((decimal)(this.DoConcept(4002).Value * (MyRule["First", this.RuleCalculateDate].ToInt() / 100)));

                    //((PairableScndCnpValue)this.DoConcept(4002)).IncreaseValue((int)Math.Round(this.DoConcept(4002).Value * coEfficient));
                    ((PairableScndCnpValue)this.DoConcept(4002)).IncreaseValue(coEfficient);
                    this.ReCalculate(13);

                }
                else
                {
                    ((PairableScndCnpValue)this.DoConcept(4003)).AddPairs(this.DoConcept(1));
                    ((PairableScndCnpValue)this.DoConcept(4002)).ClearPairs();
                }
                GetLog(MyRule, DebugRuleState.After , 13, 4002,4003);

            }
        }

        /// <summary>
        /// اضافه کار در ماموریت
        /// </summary>
        /// <param name="MyRule"></param>
        public override void R6023(AssignedRule MyRule)
        {
            //3028 غیبت ساعتی
            //مجموع انواع مرخصي روزانه 1090            
            //اضافه کار ساعتي 4002
            //2030 کار خارج از اداره
            //2023 مجموع ماموريت ساعتي
            //1 مفهوم حضور
            //4025 تبدیل حضور به اضافه کار در روز مرخصی
            if (this.DoConcept(1090).Value > 0)
            {
                GetLog(MyRule, DebugRuleState.Before , 13, 4002,4003, 4005);

                if (MyRule["First", this.RuleCalculateDate].ToInt() > 0)// && this.DoConcept(4025).Value == 0)
                {

                    ((PairableScndCnpValue)this.DoConcept(4002)).ClearPairs();

                    foreach (var pair in ((PairableScndCnpValue)this.DoConcept(1)).Pairs)
                    {
                        ((PairableScndCnpValue)this.DoConcept(4002)).AppendPair(pair);

                        var coEfficient = (int)Math.Round((decimal)((pair.Value * MyRule["First", this.RuleCalculateDate].ToInt()) / 100));
                        ((PairableScndCnpValue)this.DoConcept(4002)).IncreaseValue(coEfficient);
                    }

                    this.ReCalculate(13);
                    if (this.DoConcept(9).Value > 0)
                    {
                        this.ReCalculate(4005);
                    }

                }
                else
                {
                    ((PairableScndCnpValue)this.DoConcept(4003)).AddPairs(this.DoConcept(1));
                    ((PairableScndCnpValue)this.DoConcept(4002)).ClearPairs();
                }
                GetLog(MyRule, DebugRuleState.After , 13, 4002, 4003, 4005);

            }
        }

        #endregion

        #endregion

    }
}
