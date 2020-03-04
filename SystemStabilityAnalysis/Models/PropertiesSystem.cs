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
        public PropertiesSystem(string name)
        {
            Name = name;

            InitialParametersWithEnter();
            InitialParametersForAnalysis();
            InitialParametersWithCalculation();
            U = new ParameterU(this, () => { return (ρ * f * (h.Pow(3.0))) / ((a * (q.Pow(3.0))) * (q + d)) - ((b + f) * (h.Pow(4.0))) / (a * (q.Pow(4.0))); });
        }

        public string Name { get; set; } = null;

        public ParameterU U { get; set; }

        #region ParametersWithCalculation
        public void InitialParametersWithCalculation()
        {

            Rcyt1 = new ParameterWithCalculation(this, NameParameterWithCalculation.Rcyt1, () => { return (N1 * R1 + P1 * Rv2); });
            Rf1 = new ParameterWithCalculation(this, NameParameterWithCalculation.Rf1, () => { return (N1 * R1 + P1 * Rv2) * deltaT; });
            Rcyt2 = new ParameterWithCalculation(this, NameParameterWithCalculation.Rcyt2, () => { return (N2 * R2); });
            Rf2 = new ParameterWithCalculation(this, NameParameterWithCalculation.Rf2, () => { return (N2 * R2) * deltaT; });
            Rcyt3 = new ParameterWithCalculation(this, NameParameterWithCalculation.Rcyt3, () => { return (N3 * R3); });
            Rf3 = new ParameterWithCalculation(this, NameParameterWithCalculation.Rf3, () => { return (N3 * R3) * deltaT; });
            Rcyt = new ParameterWithCalculation(this, NameParameterWithCalculation.Rcyt, () => { return ((N1 * R1 + P1 * Rv2) + (N2 * R2) + (N3 * R3)); });
            R = new ParameterWithCalculation(this, NameParameterWithCalculation.R, () => { return ((N1 * R1 + P1 * Rv2) + (N2 * R2) + (N3 * R3)) * deltaT; });
            Wсyt1 = new ParameterWithCalculation(this, NameParameterWithCalculation.Wсyt1, () => { return N1 * W1 + Wv2 * P1; });
            Wf1 = new ParameterWithCalculation(this, NameParameterWithCalculation.Wf1, () => { return (N1 * W1 + Wv2 * P1) * deltaT; });
            Wcyt2 = new ParameterWithCalculation(this, NameParameterWithCalculation.Wcyt2, () => { return N2 * W2; });
            Wf2 = new ParameterWithCalculation(this, NameParameterWithCalculation.Wf2, () => { return N2 * W2 * deltaT; });
            Wcyt3 = new ParameterWithCalculation(this, NameParameterWithCalculation.Wcyt3, () => { return N3 * W3; });
            Wf3 = new ParameterWithCalculation(this, NameParameterWithCalculation.Wf3, () => { return N3 * W3 * deltaT; });
            Wcyt = new ParameterWithCalculation(this, NameParameterWithCalculation.Wcyt, () => { return N1 * W1 + Wv2 * P1 + N2 * W2 + N3 * W3; });
            W = new ParameterWithCalculation(this, NameParameterWithCalculation.W, () => { return (N1 * W1 + Wv2 * P1 + N2 * W2 + N3 * W3) * deltaT; });

            Smin1 = new ParameterWithCalculation(this, NameParameterWithCalculation.Smin1, () => { return (N1 * W1 + Wv2 * P1) / (Lc * Tc); });
            Smin2 = new ParameterWithCalculation(this, NameParameterWithCalculation.Smin2, () => { return (N2 * W2) / (Lc * Tc); });
            Smin3 = new ParameterWithCalculation(this, NameParameterWithCalculation.Smin3, () => { return (N3 * W3) / (Lc * Tc); });
            SminC = new ParameterWithCalculation(this, NameParameterWithCalculation.SminC, () => { return (Smin1+Smin2+Smin3) ; });
            Smin = new ParameterWithCalculation(this, NameParameterWithCalculation.Smin, () => { return ((Smin1 + Smin2 + Smin3)*Lc); });
            S1 = new ParameterWithCalculation(this, NameParameterWithCalculation.S1, () => { return (Smin1 + Sn1); });
            S2 = new ParameterWithCalculation(this, NameParameterWithCalculation.S2, () => { return (Smin2 + Sn2); });
            S3 = new ParameterWithCalculation(this, NameParameterWithCalculation.S3, () => { return (Smin3 + Sn3); });
            Sс = new ParameterWithCalculation(this, NameParameterWithCalculation.Sс, () => { return (S1+S2+S3); });
            S = new ParameterWithCalculation(this, NameParameterWithCalculation.S, () => { return ((Smin1+Smin2+Smin3)+(Sn1+Sn2+Sn3)*deltaT)*Lc; });
            SN1 = new ParameterWithCalculation(this, NameParameterWithCalculation.SN1, () => { return ((Smin1 + deltaT * Sn1) * Lc); });
            SN2 = new ParameterWithCalculation(this, NameParameterWithCalculation.SN2, () => { return ((Smin2 + deltaT * Sn2) * Lc); });
            SN3 = new ParameterWithCalculation(this, NameParameterWithCalculation.SN3, () => { return ((Smin3 + deltaT * Sn3) * Lc); });

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

            ρ = new ParameterForAnalysis(this, NameParameterForAnalysis.ρ, () => { return P1 / N1; });
            a = new ParameterForAnalysis(this, NameParameterForAnalysis.a, () => { return A1 / N1; });
            b = new ParameterForAnalysis(this, NameParameterForAnalysis.b, () => { return B1 / N1; });
            f = new ParameterForAnalysis(this, NameParameterForAnalysis.f, () => { return F1 / N1; });
            q = new ParameterForAnalysis(this, NameParameterForAnalysis.q, () => { return Q2 / N2; });
            d = new ParameterForAnalysis(this, NameParameterForAnalysis.d, () => { return D2 / N2; });
            h = new ParameterForAnalysis(this, NameParameterForAnalysis.h, () => { return H3 / N3; });

        }
        public Dictionary<NameParameterForAnalysis, ParameterForAnalysis> ParametersForAnalysis { get; protected set; } = new Dictionary<NameParameterForAnalysis, ParameterForAnalysis>();


        public ParameterForAnalysis ρ { get; set; }
        public ParameterForAnalysis a { get; set; }
        public ParameterForAnalysis b { get; set; }
        public ParameterForAnalysis f { get; set; }
        public ParameterForAnalysis q { get; set; }
        public ParameterForAnalysis d { get; set; }
        public ParameterForAnalysis h { get; set; }
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
            Lc = new ParameterWithEnter(this, NameParameterWithEnter.Lc);
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

        public bool VerificationParametersForAnalysis(List<NameParameterForAnalysis> parametersForAnalysis, out List<string> message)
        {
            message = new List<string>();
            bool result = true;
            foreach (var parameterForAnalysis in parametersForAnalysis)
            {
                if (!ParametersForAnalysis[parameterForAnalysis].isCorrect)
                {
                    message.Add(ParametersForAnalysis[parameterForAnalysis].Designation);
                    result = false;
                }
            }

            return result;
        }

        public bool VerificationParametersWithEnter(List<NameParameterWithEnter> parametersWithEnter, out List<string> message)
        {
            message = new List<string>();
            bool result = true;
            foreach (var parameterWithEnter in parametersWithEnter)
            {
                if (!ParametersWithEnter[parameterWithEnter].isCorrect)
                {
                    message.Add(ParametersWithEnter[parameterWithEnter].Designation);
                    result = false;
                }
            }

            return result;
        }

        public bool VerificationParametersWithCalculation(List<NameParameterWithCalculation> parametersWithCalculation, out List<string> message)
        {
            message = new List<string>();
            bool result = true;
            foreach (var parameterWithCalculation in parametersWithCalculation)
            {
                if (!ParametersWithCalculation[parameterWithCalculation].isCorrect)
                {
                    message.Add(ParametersWithCalculation[parameterWithCalculation].Designation);
                    result = false;
                }
            }

            return result;
        }


    }
}


