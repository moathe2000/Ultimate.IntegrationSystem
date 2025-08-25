namespace Ultimate.IntegrationSystem.Api.Common.Enum
{
   
        public enum MuqeemEndpoint
        {
            Unknown = 0,

            // 🔐 Authentication
            Authenticate,

            // 🟢 Exit-Reentry Visa
            ExitReentry_Issue=101,
            ExitReentry_Cancel=102,
            ExitReentry_Reprint=103,
            ExitReentry_Extend=104,

            // 🔴 Final Exit Visa
            FinalExit_Issue=105,
            FinalExit_Cancel=106,
            FinalExit_Probation=107,

            // 🟡 Iqama
            Iqama_Renew=108,
            Iqama_Issue=109,
            Iqama_Transfer=110,

            // 🟣 Update Information
            UpdateInfo_Extend=111,
            UpdateInfo_Renew=112,

            // 🔵 Visit Visa
            VisitVisa_Extend=113,

            // 📊 Reports
            Report_Interactive=114,
            MuqeemReport_Print=115,
            VisitorReport_Print=116,

            // 🟤 Occupation
            Occupation_CheckApproval=117,
            Occupation_Change=118,

            // 📌 Lookups
            Lookup_Cities=109,
            Lookup_Countries=110,
            Lookup_Marital=111
        }
    }

