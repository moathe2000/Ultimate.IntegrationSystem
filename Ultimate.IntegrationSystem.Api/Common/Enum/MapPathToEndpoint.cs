namespace Ultimate.IntegrationSystem.Api.Common.Enum
{

        public static class MuqeemEndpointMapper
        {
            public static MuqeemEndpoint MapPathToEndpoint(string path)
            {
                if (string.IsNullOrWhiteSpace(path))
                    return MuqeemEndpoint.Unknown;

                path = path.ToLowerInvariant();

                return path switch
                {
                    // 🔐 Authentication
                    "/api/authenticate" => MuqeemEndpoint.Authenticate,

                    // 🟢 Exit-Reentry Visa
                    "/api/v1/exit-reentry/issue" => MuqeemEndpoint.ExitReentry_Issue,
                    "/api/v1/exit-reentry/cancel" => MuqeemEndpoint.ExitReentry_Cancel,
                    "/api/v1/exit-reentry/reprint" => MuqeemEndpoint.ExitReentry_Reprint,
                    "/api/v1/exit-reentry/extend" => MuqeemEndpoint.ExitReentry_Extend,

                    // 🔴 Final Exit Visa
                    "/api/v1/final-exit/issue" => MuqeemEndpoint.FinalExit_Issue,
                    "/api/v1/final-exit/cancel" => MuqeemEndpoint.FinalExit_Cancel,
                    "/api/v1/final-exit/issue/probation-period" => MuqeemEndpoint.FinalExit_Probation,

                    // 🟡 Iqama
                    "/api/v1/iqama/renew" => MuqeemEndpoint.Iqama_Renew,
                    "/api/v1/iqama/issue" => MuqeemEndpoint.Iqama_Issue,
                    "/api/v1/iqama/transfer" => MuqeemEndpoint.Iqama_Transfer,

                    // 🟣 Update Information
                    "/api/v1/update-information/extend" => MuqeemEndpoint.UpdateInfo_Extend,
                    "/api/v1/update-information/renew" => MuqeemEndpoint.UpdateInfo_Renew,

                    // 🔵 Visit Visa
                    "/api/v1/visit-visa/extend" => MuqeemEndpoint.VisitVisa_Extend,

                    // 📊 Reports
                    "/api/v1/report/interactive-services-report" => MuqeemEndpoint.Report_Interactive,
                    "/api/v1/muqeem-report/print" => MuqeemEndpoint.MuqeemReport_Print,
                    "/api/v1/visitor-report/print" => MuqeemEndpoint.VisitorReport_Print,

                    // 🟤 Occupation
                    "/api/v1/occupation/check-mol-approval" => MuqeemEndpoint.Occupation_CheckApproval,
                    "/api/v1/occupation/change" => MuqeemEndpoint.Occupation_Change,

                    // 📌 Lookups
                    "/api/lookups/cities" => MuqeemEndpoint.Lookup_Cities,
                    "/api/lookups/countries" => MuqeemEndpoint.Lookup_Countries,
                    "/api/lookups/marital-statuses" => MuqeemEndpoint.Lookup_Marital,

                    _ => MuqeemEndpoint.Unknown
                };
            }
        }
    }
