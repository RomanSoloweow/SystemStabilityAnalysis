using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemStabilityAnalysis.Helpers;

namespace SystemStabilityAnalysis.Models
{
    public class PropertiesSystem
    {
        public PropertiesSystem()
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
        public Dictionary<string, ParameterWithEnter> Properties { get; protected set; } = new Dictionary<string, ParameterWithEnter>();

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

    //    public ParameterWithRestriction deltaT { get; set; }
    //    public ParameterWithRestriction N1 { get; set; }
    //    public ParameterWithRestriction N2 { get; set; }
    //    public ParameterWithRestriction N3 { get; set; }
    //    public ParameterWithRestriction P1 { get; set; }
    //    public ParameterWithRestriction A1 { get; set; }
    //    public ParameterWithRestriction B1 { get; set; }
    //    public ParameterWithRestriction F1 { get; set; }
    //    public ParameterWithRestriction Q2 { get; set; }
    //    public ParameterWithRestriction D2 { get; set; }
    //    public ParameterWithRestriction H3 { get; set; }
    //    public ParameterWithRestriction Lс { get; set; }
    //    public ParameterWithRestriction Tс { get; set; }
    //    public ParameterWithRestriction R1 { get; set; }
    //    public ParameterWithRestriction Rv2 { get; set; }
    //    public ParameterWithRestriction Rcyt1 { get; set; }
    //    public ParameterWithRestriction Rf1 { get; set; }
    //    public ParameterWithRestriction R2 { get; set; }
    //    public ParameterWithRestriction Rcyt2 { get; set; }
    //    public ParameterWithRestriction Rf2 { get; set; }
    //    public ParameterWithRestriction R3 { get; set; }
    //    public ParameterWithRestriction Rcyt3 { get; set; }
    //    public ParameterWithRestriction Rf3 { get; set; }
    //    public ParameterWithRestriction Rcyt { get; set; }
    //    public ParameterWithRestriction R { get; set; }
    //    public ParameterWithRestriction W1 { get; set; }
    //    public ParameterWithRestriction Wv2 { get; set; }
    //    public ParameterWithRestriction Wсyt1 { get; set; }
    //    public ParameterWithRestriction Wf1 { get; set; }
    //    public ParameterWithRestriction W2 { get; set; }
    //    public ParameterWithRestriction Wcyt2 { get; set; }
    //    public ParameterWithRestriction Wf2 { get; set; }
    //    public ParameterWithRestriction W3 { get; set; }
    //    public ParameterWithRestriction Wcyt3 { get; set; }
    //    public ParameterWithRestriction Wf3 { get; set; }
    //    public ParameterWithRestriction Wcyt { get; set; }
    //    public ParameterWithRestriction W { get; set; }
    //    public ParameterWithRestriction Smin1 { get; set; }
    //    public ParameterWithRestriction Smin2 { get; set; }
    //    public ParameterWithRestriction Smin3 { get; set; }
    //    public ParameterWithRestriction SminC { get; set; }
    //    public ParameterWithRestriction Smin { get; set; }
    //    public ParameterWithRestriction Sn1 { get; set; }
    //    public ParameterWithRestriction Sn2 { get; set; }
    //    public ParameterWithRestriction Sn3 { get; set; }
    //    public ParameterWithRestriction S1 { get; set; }
    //    public ParameterWithRestriction S2 { get; set; }
    //    public ParameterWithRestriction S3 { get; set; }
    //    public ParameterWithRestriction Sс { get; set; }
    //    public ParameterWithRestriction S { get; set; }
    //    public ParameterWithRestriction SN1 { get; set; }
    //    public ParameterWithRestriction SN2 { get; set; }
    //    public ParameterWithRestriction SN3 { get; set; }

    //}
}
