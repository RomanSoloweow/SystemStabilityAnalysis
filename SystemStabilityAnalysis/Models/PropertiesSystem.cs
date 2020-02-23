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
            deltaT = new Property(this, ParametersName.deltaT);
            N1 = new Property(this, ParametersName.N1);
            N2 = new Property(this, ParametersName.N2);
            N3 = new Property(this, ParametersName.N3);
            P1 = new Property(this, ParametersName.P1);
            A1 = new Property(this, ParametersName.A1);
            B1 = new Property(this, ParametersName.B1);
            F1 = new Property(this, ParametersName.F1);
            Q2 = new Property(this, ParametersName.Q2);
            D2 = new Property(this, ParametersName.D2);
            H3 = new Property(this, ParametersName.H3);
            Lс = new Property(this, ParametersName.Lс);
            Tс = new Property(this, ParametersName.Tс);
            R1 = new Property(this, ParametersName.R1);
            Rv2 = new Property(this, ParametersName.Rv2);
            Rcyt1 = new Property(this, ParametersName.Rcyt1);
            Rf1 = new Property(this, ParametersName.Rf1);
            R2 = new Property(this, ParametersName.R2);
            Rcyt2 = new Property(this, ParametersName.Rcyt2);
            Rf2 = new Property(this, ParametersName.Rf2);
            R3 = new Property(this, ParametersName.R3);
            Rcyt3 = new Property(this, ParametersName.Rcyt3);
            Rf3 = new Property(this, ParametersName.Rf3);
            Rcyt = new Property(this, ParametersName.Rcyt);
            R = new Property(this, ParametersName.R);
            W1 = new Property(this, ParametersName.W1);
            Wv2 = new Property(this, ParametersName.Wv2);
            Wсyt1 = new Property(this, ParametersName.Wсyt1);
            Wf1 = new Property(this, ParametersName.Wf1);
            W2 = new Property(this, ParametersName.W2);
            Wcyt2 = new Property(this, ParametersName.Wcyt2);
            Wf2 = new Property(this, ParametersName.Wf2);
            W3 = new Property(this, ParametersName.W3);
            Wcyt3 = new Property(this, ParametersName.Wcyt3);
            Wf3 = new Property(this, ParametersName.Wf3);
            Wcyt = new Property(this, ParametersName.Wcyt);
            W = new Property(this, ParametersName.W);
            Smin1 = new Property(this, ParametersName.Smin1);
            Smin2 = new Property(this, ParametersName.Smin2);
            Smin3 = new Property(this, ParametersName.Smin3);
            SminC = new Property(this, ParametersName.SminC);
            Smin = new Property(this, ParametersName.Smin);
            Sn1 = new Property(this, ParametersName.Sn1);
            Sn2 = new Property(this, ParametersName.Sn2);
            Sn3 = new Property(this, ParametersName.Sn3);
            S1 = new Property(this, ParametersName.S1);
            S2 = new Property(this, ParametersName.S2);
            S3 = new Property(this, ParametersName.S3);
            Sс = new Property(this, ParametersName.Sс);
            S = new Property(this, ParametersName.S);
            SN1 = new Property(this, ParametersName.SN1);
            SN2 = new Property(this, ParametersName.SN2);
            SN3 = new Property(this, ParametersName.SN3);
        }
        public Dictionary<string, Property> Properties { get; protected set; } = new Dictionary<string, Property>();

        public Property deltaT { get; set; }
        public Property N1 { get; set; }
        public Property N2 { get; set; }
        public Property N3 { get; set; }
        public Property P1 { get; set; }
        public Property A1 { get; set; }
        public Property B1 { get; set; }
        public Property F1 { get; set; }
        public Property Q2 { get; set; }
        public Property D2 { get; set; }
        public Property H3 { get; set; }
        public Property Lс { get; set; }
        public Property Tс { get; set; }
        public Property R1 { get; set; }
        public Property Rv2 { get; set; }
        public Property Rcyt1 { get; set; }
        public Property Rf1 { get; set; }
        public Property R2 { get; set; }
        public Property Rcyt2 { get; set; }
        public Property Rf2 { get; set; }
        public Property R3 { get; set; }
        public Property Rcyt3 { get; set; }
        public Property Rf3 { get; set; }
        public Property Rcyt { get; set; }
        public Property R { get; set; }
        public Property W1 { get; set; }
        public Property Wv2 { get; set; }
        public Property Wсyt1 { get; set; }
        public Property Wf1 { get; set; }
        public Property W2 { get; set; }
        public Property Wcyt2 { get; set; }
        public Property Wf2 { get; set; }
        public Property W3 { get; set; }
        public Property Wcyt3 { get; set; }
        public Property Wf3 { get; set; }
        public Property Wcyt { get; set; }
        public Property W { get; set; }
        public Property Smin1 { get; set; }
        public Property Smin2 { get; set; }
        public Property Smin3 { get; set; }
        public Property SminC { get; set; }
        public Property Smin { get; set; }
        public Property Sn1 { get; set; }
        public Property Sn2 { get; set; }
        public Property Sn3 { get; set; }
        public Property S1 { get; set; }
        public Property S2 { get; set; }
        public Property S3 { get; set; }
        public Property Sс { get; set; }
        public Property S { get; set; }
        public Property SN1 { get; set; }
        public Property SN2 { get; set; }
        public Property SN3 { get; set; }

    }
}
