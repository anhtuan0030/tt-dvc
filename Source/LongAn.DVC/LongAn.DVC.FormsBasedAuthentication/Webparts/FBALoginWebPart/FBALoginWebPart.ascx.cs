using System;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.SharePoint.IdentityModel;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System.IdentityModel.Tokens;

namespace LongAn.DVC.FormsBasedAuthentication.Webparts.FBALoginWebPart
{
    [ToolboxItemAttribute(false)]
    public partial class FBALoginWebPart : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public FBALoginWebPart()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (SPContext.Current.Web.CurrentUser != null)
            {
                Login1.Visible = false;
                pnlAuthorize.Visible = true;

                lblUserName.Text = SPContext.Current.Web.CurrentUser.Name;
            }
            else
            {
                Login1.Visible = true;
                pnlAuthorize.Visible = false;
            }
        }

        protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
        {
            string membership = "FBAMembershipProvider";
            string role = "FBARoleProvider";

            e.Authenticated = SPClaimsUtility.AuthenticateFormsUser(new Uri(SPContext.Current.Web.Url), Login1.UserName, Login1.Password);

            if (!e.Authenticated) return;

            SecurityToken token = SPSecurityContext.SecurityTokenForFormsAuthentication(new Uri(SPContext.Current.Web.Url), membership, role, Login1.UserName, Login1.Password);
            if (token == null)
            {

                e.Authenticated = false;
                return;
            }
            else
            {

                SPFederationAuthenticationModule module = SPFederationAuthenticationModule.Current;
                module.SetPrincipalAndWriteSessionToken(token);
                e.Authenticated = true;

                SPUtility.Redirect(SPContext.Current.Site.RootWeb.Url.TrimEnd('/') + "/Pages/DVC.aspx", SPRedirectFlags.Trusted, this.Context);

            }
        }
    }
}
