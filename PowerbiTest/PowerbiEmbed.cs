using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.PowerBI.Api.V1;
using Microsoft.PowerBI.Api.V1.Models;
using Microsoft.PowerBI.Security;
using Microsoft.Rest;
using Microsoft.Rest.Serialization;

namespace PowerbiTest
{
    class PowerbiEmbed
    {
        private string collectionName;
        private ServiceClientCredentials cred;
        private PowerBIClient client;
        private Microsoft.PowerBI.Api.V1.Models.Workspace currentWorkSpace;

        private IList<Import> powerbiImports;
        private IList<Report> powerbiReports;

        private string accessKey = "1H2F82h8GCQORHD4ulJzu+IEBaS+9UaqBOvaIIaP50nGcNq+Mea8uAiDsMY9DFxA/fz/lC87J+hvyL8rpz2LyQ==";

        public PowerbiEmbed(string cn)
        {
            collectionName = cn;
            // Create a token credentials with "AppKey" type
            cred = new TokenCredentials(accessKey, "AppKey");

            // Instantiate your Power BI client passing in the required credentials
            client = new PowerBIClient(cred);
            client.BaseUri = new Uri("https://api.powerbi.com");
        }

        public void GetWorkSpace(string workspaceNamePrefix)
        {
            var workspaces = client.Workspaces.GetWorkspacesByCollectionName(collectionName);
            foreach (var instance in workspaces.Value)
            {
                Console.WriteLine("Collection: {0}, Id: {1}, Display Name: {2}", instance.WorkspaceCollectionName, instance.WorkspaceId, instance.DisplayName);
                if (instance.DisplayName.StartsWith(workspaceNamePrefix))
                {
                    currentWorkSpace = instance;
                }
            }
        }

        public void GetImportsInWorkspace()
        {
            var imports = client.Imports.GetImports(collectionName, currentWorkSpace.WorkspaceId).Value;
            foreach(var importPowerbi in imports)
            {
                Console.WriteLine("Import: {0}, id: {1}", importPowerbi.Name, importPowerbi.Id);
            }

            powerbiImports = imports;
        }

        public void GetReports()
        {
            var reports = client.Reports.GetReports(collectionName, currentWorkSpace.WorkspaceId).Value;
            foreach(var report in reports)
            {
                Console.WriteLine("Report: {0}, id {1}, webUrl {2}, embedUrl {3}", report.Name, report.Id, report.WebUrl, report.EmbedUrl);
            }
            powerbiReports = reports;
        }

        public void GetEmbedToken()
        {
            foreach(var report in powerbiReports)
            {
                PowerBIToken token = PowerBIToken.CreateReportEmbedToken(collectionName, currentWorkSpace.WorkspaceId, report.Id);
                var jwt = token.Generate(accessKey);

                Console.WriteLine("report {0}, access token: {1}", report.Name, jwt);
            }
            
        }
    }
}
