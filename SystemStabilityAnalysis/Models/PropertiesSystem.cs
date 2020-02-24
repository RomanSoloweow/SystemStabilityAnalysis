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
            Rcyt1 = new ParameterWithCalculation(this, NameParameterWithCalculation.Rcyt1, (Rcyt1) => { return (N1*R1 + P1*Rv2)*Lc; });
            Rf1 = new ParameterWithCalculation(this, NameParameterWithCalculation.Rf1, (Rf1) => { return (N1*R1 + P1*Rv2)*Lc*deltaT; });
            Rcyt2 = new ParameterWithCalculation(this, NameParameterWithCalculation.Rcyt2, (Rcyt2) => { return (N2 * R2) * Lc; });
            Rf2 = new ParameterWithCalculation(this, NameParameterWithCalculation.Rf2, (Rf2) => { return (N2*R2)*Lc*deltaT; });
            Rcyt3 = new ParameterWithCalculation(this, NameParameterWithCalculation.Rcyt3, (Rcyt3) => { return (N3*R3)*Lc; });
            Rf3 = new ParameterWithCalculation(this, NameParameterWithCalculation.Rf3, (Rf3) => { return (N3 * R3) * Lc * deltaT; });
            Rcyt = new ParameterWithCalculation(this, NameParameterWithCalculation.Rcyt, (Rcyt) => { return ((N1 * R1 + P1 * Rv2) + (N2 * R2) + (N3 * R3))* Lc;  });
            R = new ParameterWithCalculation(this, NameParameterWithCalculation.R, (R) => { return ((N1 * R1 + P1 * Rv2) + (N2 * R2)+ (N3 * R3)) * Lc * deltaT; });
            Wсyt1 = new ParameterWithCalculation(this, NameParameterWithCalculation.Wсyt1, (Wсyt1) => { return N1*W1 + Wv2*P1; });
            Wf1 = new ParameterWithCalculation(this, NameParameterWithCalculation.Wf1, (Wf1) => { return (N1*W1 + Wv2*P1)*deltaT; });
            Wcyt2 = new ParameterWithCalculation(this, NameParameterWithCalculation.Wcyt2, (Wcyt2) => { return N2*W2; });
            Wf2 = new ParameterWithCalculation(this, NameParameterWithCalculation.Wf2, (Wf2) => { return N2*W2*deltaT; });
            Wcyt3 = new ParameterWithCalculation(this, NameParameterWithCalculation.Wcyt3, (Wcyt3) => { return N3 * W3; });
            Wf3 = new ParameterWithCalculation(this, NameParameterWithCalculation.Wf3, (Wf3) => { return N3 * W3 * deltaT; });
            Wcyt = new ParameterWithCalculation(this, NameParameterWithCalculation.Wcyt, (Wcyt) => { return N1*W1 + Wv2*P1 + N2*W2 + N3*W3; });
            W = new ParameterWithCalculation(this, NameParameterWithCalculation.W, (W) => { return (N1*W1 + Wv2*P1 + N2*W2 + N3*W3)*deltaT; });

            Smin1 = new ParameterWithCalculation(this, NameParameterWithCalculation.Smin1, (Smin1) => { return (N1*W1 + Wv2*P1)/(Lc*Tc); });
            Smin2 = new ParameterWithCalculation(this, NameParameterWithCalculation.Smin2, (Smin2) => { return (N2 * W2)/(Lc * Tc); });
            Smin3 = new ParameterWithCalculation(this, NameParameterWithCalculation.Smin3, (Smin3) => { return (N3 * W3) / (Lc * Tc); });
            SminC = new ParameterWithCalculation(this, NameParameterWithCalculation.SminC, (SminC) => { return (N1*W1 + Wv2*P1 + N2*W2 + N3*W3)/(Lc*Tc); });
            Smin = new ParameterWithCalculation(this, NameParameterWithCalculation.Smin, (Smin) => { return (N1*W1 + Wv2*P1 + N2*W2 + N3*W3) / (Tc); });
            S1 = new ParameterWithCalculation(this, NameParameterWithCalculation.S1, (S1) => { return (N1*W1 + Wv2*P1)/(Lc*Tc) + Sn1; });
            S2 = new ParameterWithCalculation(this, NameParameterWithCalculation.S2, (S2) => { return (N2 * W2)/(Lc * Tc) + Sn2;});
            S3 = new ParameterWithCalculation(this, NameParameterWithCalculation.S3, (S3) => { return (N3 * W3) / (Lc * Tc) + Sn3; });
            Sс = new ParameterWithCalculation(this, NameParameterWithCalculation.Sс, (Sс) => { return (N1*W1 + Wv2*P1 + N2*W2 + N3*W3) / (Lc*Tc) + Sn1 + Sn2 + Sn3; });
            S = new ParameterWithCalculation(this, NameParameterWithCalculation.S, (S) => { return (N1 * W1 + Wv2 * P1 + N2 * W2 + N3 * W3) * deltaT / Tc + (Sn1 + Sn2 + Sn3) * Lc * deltaT; });
            SN1 = new ParameterWithCalculation(this, NameParameterWithCalculation.SN1, (SN1) => { return (N1*W1 + Wv2*P1)*deltaT /Tc + Sn1*Lc*deltaT; });
            SN2 = new ParameterWithCalculation(this, NameParameterWithCalculation.SN2, (SN2) => { return (N2 * W2) * deltaT/Tc + Sn2 * Lc * deltaT; });
            SN3 = new ParameterWithCalculation(this, NameParameterWithCalculation.SN3, (SN3) => { return (N3 * W3) * deltaT / Tc + Sn3 * Lc * deltaT; });
        }

        public Dictionary<NameParameterWithCalculation, ParameterWithCalculation> ParametersWithCalculation { get; protected set; } = new Dictionary<NameParameterWithCalculation, ParameterWithCalculation>();


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
        #endregion ParametersWithCalculation

        #region ParametersForAnalysis

        public void InitialParametersForAnalysis()
        {

        }
        public Dictionary<NameParameterForAnalysis, ParameterForAnalysis> ParametersForAnalysis { get; protected set; } = new Dictionary<NameParameterForAnalysis, ParameterForAnalysis>();



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
            Lc = new ParameterWithEnter(this, NameParameterWithEnter.Lс);
            Tc = new ParameterWithEnter(this, NameParameterWithEnter.Tс);
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
        public ParameterWithEnter Lc { get; set; }
        public ParameterWithEnter Tc { get; set; }
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
}
