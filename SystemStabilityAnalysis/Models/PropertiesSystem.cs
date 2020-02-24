using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemStabilityAnalysis.Helpers;
using SystemStabilityAnalysis.Models.Parameters;

namespace SystemStabilityAnalysis.Models
{
    public class PropertiesSystem
    {
        public PropertiesSystem()
        {
            InitialParametersWithEnter();
            InitialParametersForAnalysis();
            InitialParametersWithCalculation();
        }


        
        #region ParametersWithCalculation
        public void InitialParametersWithCalculation()
        {

        }

        public Dictionary<NameParameterWithCalculation, ParameterWithCalculation> ParametersWithCalculation { get; protected set; } = new Dictionary<NameParameterWithCalculation, ParameterWithCalculation>();



        #endregion ParametersWithCalculation

        #region ParametersForAnalysis

        public void InitialParametersForAnalysis()
        {
            Rcyt1 = new ParameterWithCalculation(this, NameParameterWithCalculation.Rcyt1, (Rcyt1) => { return Rcyt1 * Rcyt1; });
            Rf1 = new ParameterWithCalculation(this, NameParameterWithCalculation.Rf1, (Rf1) => { return Rf1 * Rf1; });
            Rcyt2 = new ParameterWithCalculation(this, NameParameterWithCalculation.Rcyt2, (Rcyt2) => { return Rcyt2 * Rcyt2; });
            Rf2 = new ParameterWithCalculation(this, NameParameterWithCalculation.Rf2, (Rf2) => { return Rf2 * Rf2; });
            Rcyt3 = new ParameterWithCalculation(this, NameParameterWithCalculation.Rcyt3, (Rcyt3) => { return Rcyt3 * Rcyt3; });
            Rf3 = new ParameterWithCalculation(this, NameParameterWithCalculation.Rf3, (Rf3) => { return Rf3 * Rf3; });
            Rcyt = new ParameterWithCalculation(this, NameParameterWithCalculation.Rcyt, (Rcyt) => { return Rcyt * Rcyt; });
            R = new ParameterWithCalculation(this, NameParameterWithCalculation.R, (R) => { return R * R; });
            Wсyt1 = new ParameterWithCalculation(this, NameParameterWithCalculation.Wсyt1, (Wсyt1) => { return Wсyt1 * Wсyt1; });
            Wf1 = new ParameterWithCalculation(this, NameParameterWithCalculation.Wf1, (Wf1) => { return Wf1 * Wf1; });
            Wcyt2 = new ParameterWithCalculation(this, NameParameterWithCalculation.Wcyt2, (Wcyt2) => { return Wcyt2 * Wcyt2; });
            Wf2 = new ParameterWithCalculation(this, NameParameterWithCalculation.Wf2, (Wf2) => { return Wf2 * Wf2; });
            Wcyt3 = new ParameterWithCalculation(this, NameParameterWithCalculation.Wcyt3, (Wcyt3) => { return Wcyt3 * Wcyt3; });
            Wf3 = new ParameterWithCalculation(this, NameParameterWithCalculation.Wf3, (Wf3) => { return Wf3 * Wf3; });
            Wcyt = new ParameterWithCalculation(this, NameParameterWithCalculation.Wcyt, (Wcyt) => { return Wcyt * Wcyt; });
            W = new ParameterWithCalculation(this, NameParameterWithCalculation.W, (W) => { return W * W; });

            Smin1 = new ParameterWithCalculation(this, NameParameterWithCalculation.Smin1, (Smin1) => { return Smin1 * Smin1; });
            Smin2 = new ParameterWithCalculation(this, NameParameterWithCalculation.Smin2, (Smin2) => { return Smin2 * Smin2; });
            Smin3 = new ParameterWithCalculation(this, NameParameterWithCalculation.Smin3, (Smin3) => { return Smin3 * Smin3; });
            SminC = new ParameterWithCalculation(this, NameParameterWithCalculation.SminC, (SminC) => { return SminC * SminC; });
            Smin = new ParameterWithCalculation(this, NameParameterWithCalculation.Smin, (Smin) => { return Smin * Smin; });
            S1 = new ParameterWithCalculation(this, NameParameterWithCalculation.S1, (S1) => { return S1 * S1; });
            S2 = new ParameterWithCalculation(this, NameParameterWithCalculation.S2, (S2) => { return S2 * S2; });
            S3 = new ParameterWithCalculation(this, NameParameterWithCalculation.S3, (S3) => { return S3 * S3; });
            Sс = new ParameterWithCalculation(this, NameParameterWithCalculation.Sс, (Sс) => { return Sс * Sс; });
            S = new ParameterWithCalculation(this, NameParameterWithCalculation.S, (S) => { return S * S; });
            SN1 = new ParameterWithCalculation(this, NameParameterWithCalculation.SN1, (SN1) => { return SN1 * SN1; });
            SN2 = new ParameterWithCalculation(this, NameParameterWithCalculation.SN2, (SN2) => { return SN2 * SN2; });
            SN3 = new ParameterWithCalculation(this, NameParameterWithCalculation.SN3, (SN3) => { return SN3 * SN3; });
        }
        public Dictionary<NameParameterForAnalysis, ParameterForAnalysis> ParametersForAnalysis { get; protected set; } = new Dictionary<NameParameterForAnalysis, ParameterForAnalysis>();
        
        public ParameterWithCalculation Rcyt1 { get; set; }
        public ParameterWithCalculation Rf1 { get; set; }
        public ParameterWithCalculation Rcyt2 { get; set; }
        public ParameterWithCalculation Rf2 { get; set; }
        public ParameterWithCalculation Rcyt3 { get; set; }
        public ParameterWithCalculation Rf3 { get; set; }
        public ParameterWithCalculation Rcyt { get; set; }
        public ParameterWithCalculation R { get; set; }
        public ParameterWithCalculation Wсyt1 { get; set; }
        public ParameterWithCalculation Wf1 { get; set; }
        public ParameterWithCalculation Wcyt2 { get; set; }
        public ParameterWithCalculation Wf2 { get; set; }
        public ParameterWithCalculation Wcyt3 { get; set; }
        public ParameterWithCalculation Wf3 { get; set; }
        public ParameterWithCalculation Wcyt { get; set; }
        public ParameterWithCalculation W { get; set; }
        public ParameterWithCalculation Smin1 { get; set; }
        public ParameterWithCalculation Smin2 { get; set; }
        public ParameterWithCalculation Smin3 { get; set; }
        public ParameterWithCalculation SminC { get; set; }
        public ParameterWithCalculation Smin { get; set; }
        public ParameterWithCalculation S1 { get; set; }
        public ParameterWithCalculation S2 { get; set; }
        public ParameterWithCalculation S3 { get; set; }
        public ParameterWithCalculation Sс { get; set; }
        public ParameterWithCalculation S { get; set; }
        public ParameterWithCalculation SN1 { get; set; }
        public ParameterWithCalculation SN2 { get; set; }
        public ParameterWithCalculation SN3 { get; set; }

        #endregion ParametersForAnalysis

        #region ParametersWithEnter

        public void InitialParametersWithEnter()
        {
            deltaT = new ParameterWithEnter(this, NameParameterWithEnter.DeltaT);
            N1 = new ParameterWithEnter(this, NameParameterWithEnter.N1);
            N2 = new ParameterWithEnter(this, NameParameterWithEnter.N2);
            N3 = new ParameterWithEnter(this, NameParameterWithEnter.N3);
            P1 = new ParameterWithEnter(this, NameParameterWithEnter.P1);
            A1 = new ParameterWithEnter(this, NameParameterWithEnter.A1);
            B1 = new ParameterWithEnter(this, NameParameterWithEnter.B1);
            F1 = new ParameterWithEnter(this, NameParameterWithEnter.F1);
            Q2 = new ParameterWithEnter(this, NameParameterWithEnter.Q2);
            D2 = new ParameterWithEnter(this, NameParameterWithEnter.D2);
            H3 = new ParameterWithEnter(this, NameParameterWithEnter.H3);
            Lс = new ParameterWithEnter(this, NameParameterWithEnter.Lс);
            Tс = new ParameterWithEnter(this, NameParameterWithEnter.Tс);
            R1 = new ParameterWithEnter(this, NameParameterWithEnter.R1);
            Rv2 = new ParameterWithEnter(this, NameParameterWithEnter.Rv2);
            R2 = new ParameterWithEnter(this, NameParameterWithEnter.R2);
            R3 = new ParameterWithEnter(this, NameParameterWithEnter.R3);
            W1 = new ParameterWithEnter(this, NameParameterWithEnter.W1);
            Wv2 = new ParameterWithEnter(this, NameParameterWithEnter.Wv2);
            W2 = new ParameterWithEnter(this, NameParameterWithEnter.W2);
            W3 = new ParameterWithEnter(this, NameParameterWithEnter.W3);
            Sn1 = new ParameterWithEnter(this, NameParameterWithEnter.Sn1);
            Sn2 = new ParameterWithEnter(this, NameParameterWithEnter.Sn2);
            Sn3 = new ParameterWithEnter(this, NameParameterWithEnter.Sn3);
        }

        public Dictionary<NameParameterWithEnter, ParameterWithEnter> ParametersWithEnter { get; protected set; } = new Dictionary<NameParameterWithEnter, ParameterWithEnter>();

        public ParameterWithEnter deltaT { get; set; }
        public ParameterWithEnter N1 { get; set; }
        public ParameterWithEnter N2 { get; set; }
        public ParameterWithEnter N3 { get; set; }
        public ParameterWithEnter P1 { get; set; }
        public ParameterWithEnter A1 { get; set; }
        public ParameterWithEnter B1 { get; set; }
        public ParameterWithEnter F1 { get; set; }
        public ParameterWithEnter Q2 { get; set; }
        public ParameterWithEnter D2 { get; set; }
        public ParameterWithEnter H3 { get; set; }
        public ParameterWithEnter Lс { get; set; }
        public ParameterWithEnter Tс { get; set; }
        public ParameterWithEnter R1 { get; set; }
        public ParameterWithEnter R2 { get; set; }
        public ParameterWithEnter Rv2 { get; set; }
        public ParameterWithEnter R3 { get; set; }
        public ParameterWithEnter W1 { get; set; }
        public ParameterWithEnter Wv2 { get; set; }
        public ParameterWithEnter W2 { get; set; }
        public ParameterWithEnter W3 { get; set; }
        public ParameterWithEnter Sn1 { get; set; }
        public ParameterWithEnter Sn2 { get; set; }
        public ParameterWithEnter Sn3 { get; set; }

        #endregion ParametersWithEnter


    }
    //public class PropertiesSystem
    //{
    //    public PropertiesSystem()
    //    {
    //        deltaT = new ParameterWithRestriction(this, ParameterWithRestriction.DeltaT);
    //        N1 = new ParameterWithRestriction(this, ParameterWithRestriction.N1);
    //        N2 = new ParameterWithRestriction(this, ParameterWithRestriction.N2);
    //        N3 = new ParameterWithRestriction(this, ParameterWithRestriction.N3);
    //        P1 = new ParameterWithRestriction(this, ParameterWithRestriction.P1);
    //        A1 = new ParameterWithRestriction(this, ParameterWithRestriction.A1);
    //        B1 = new ParameterWithRestriction(this, ParameterWithRestriction.B1);
    //        F1 = new ParameterWithRestriction(this, ParameterWithRestriction.F1);
    //        Q2 = new ParameterWithRestriction(this, ParameterWithRestriction.Q2);
    //        D2 = new ParameterWithRestriction(this, ParameterWithRestriction.D2);
    //        H3 = new ParameterWithRestriction(this, ParameterWithRestriction.H3);
    //        Lс = new ParameterWithRestriction(this, ParameterWithRestriction.Lс);
    //        Tс = new ParameterWithRestriction(this, ParameterWithRestriction.Tс);
    //        R1 = new ParameterWithRestriction(this, ParameterWithRestriction.R1);
    //        Rv2 = new ParameterWithRestriction(this, ParameterWithRestriction.Rv2);
    //        Rcyt1 = new ParameterWithRestriction(this, ParameterWithRestriction.Rcyt1);
    //        Rf1 = new ParameterWithRestriction(this, ParameterWithRestriction.Rf1);
    //        R2 = new ParameterWithRestriction(this, ParameterWithRestriction.R2);
    //        Rcyt2 = new ParameterWithRestriction(this, ParameterWithRestriction.Rcyt2);
    //        Rf2 = new ParameterWithRestriction(this, ParameterWithRestriction.Rf2);
    //        R3 = new ParameterWithRestriction(this, ParameterWithRestriction.R3);
    //        Rcyt3 = new ParameterWithRestriction(this, ParameterWithRestriction.Rcyt3);
    //        Rf3 = new ParameterWithRestriction(this, ParameterWithRestriction.Rf3);
    //        Rcyt = new ParameterWithRestriction(this, ParameterWithRestriction.Rcyt);
    //        R = new ParameterWithRestriction(this, ParameterWithRestriction.R);
    //        W1 = new ParameterWithRestriction(this, ParameterWithRestriction.W1);
    //        Wv2 = new ParameterWithRestriction(this, ParameterWithRestriction.Wv2);
    //        Wсyt1 = new ParameterWithRestriction(this, ParameterWithRestriction.Wсyt1);
    //        Wf1 = new ParameterWithRestriction(this, ParameterWithRestriction.Wf1);
    //        W2 = new ParameterWithRestriction(this, ParameterWithRestriction.W2);
    //        Wcyt2 = new ParameterWithRestriction(this, ParameterWithRestriction.Wcyt2);
    //        Wf2 = new ParameterWithRestriction(this, ParameterWithRestriction.Wf2);
    //        W3 = new ParameterWithRestriction(this, ParameterWithRestriction.W3);
    //        Wcyt3 = new ParameterWithRestriction(this, ParameterWithRestriction.Wcyt3);
    //        Wf3 = new ParameterWithRestriction(this, ParameterWithRestriction.Wf3);
    //        Wcyt = new ParameterWithRestriction(this, ParameterWithRestriction.Wcyt);
    //        W = new ParameterWithRestriction(this, ParameterWithRestriction.W);
    //        Smin1 = new ParameterWithRestriction(this, ParameterWithRestriction.Smin1);
    //        Smin2 = new ParameterWithRestriction(this, ParameterWithRestriction.Smin2);
    //        Smin3 = new ParameterWithRestriction(this, ParameterWithRestriction.Smin3);
    //        SminC = new ParameterWithRestriction(this, ParameterWithRestriction.SminC);
    //        Smin = new ParameterWithRestriction(this, ParameterWithRestriction.Smin);
    //        Sn1 = new ParameterWithRestriction(this, ParameterWithRestriction.Sn1);
    //        Sn2 = new ParameterWithRestriction(this, ParameterWithRestriction.Sn2);
    //        Sn3 = new ParameterWithRestriction(this, ParameterWithRestriction.Sn3);
    //        S1 = new ParameterWithRestriction(this, ParameterWithRestriction.S1);
    //        S2 = new ParameterWithRestriction(this, ParameterWithRestriction.S2);
    //        S3 = new ParameterWithRestriction(this, ParameterWithRestriction.S3);
    //        Sс = new ParameterWithRestriction(this, ParameterWithRestriction.Sс);
    //        S = new ParameterWithRestriction(this, ParameterWithRestriction.S);
    //        SN1 = new ParameterWithRestriction(this, ParameterWithRestriction.SN1);
    //        SN2 = new ParameterWithRestriction(this, ParameterWithRestriction.SN2);
    //        SN3 = new ParameterWithRestriction(this, ParameterWithRestriction.SN3);
    //    }
    //    public Dictionary<string, ParameterWithRestriction> Properties { get; protected set; } = new Dictionary<string, ParameterWithRestriction>();



    //}
}
