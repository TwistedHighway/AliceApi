<%@ Page Language="C#"%>
<%@ Import Namespace="System" %>
<%@ Import Namespace="System.Web" %>
<%@ Import Namespace="System.Text" %>
<%@ Import Namespace="System.Net.Mail" %>
<%@ Import Namespace="System.Net" %>
<script type="text/C#" runat="server">
   protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack)
        {

            try
            {
                ////////////////////////////////////////////////////////////////////////////
                // From Postage
                //string mTo = ConfigurationManager.AppSettings["AppEmailTo"];
                //string mTo = "idstates@hotmail.com";
                //MailAddress MailTo = new MailAddress(mTo, "xxxx");
                ////////////////////////////////////////////////////////////////////////////

                ltlMessage.Text = "";
                //MailAddress mailTo = new MailAddress(txtToAddress.Text, txtToName.Text);
                MailAddress mailFrom = new MailAddress(txtFromAddress.Text, txtFromName.Text);
                MailAddress mailTo = new MailAddress(txtToAddress.Text, txtToName.Text);
                MailMessage myMessage = new MailMessage(mailFrom, mailTo);

                myMessage.Subject = txtSubject.Text;
                myMessage.Body = txtMessage.Text;

                SmtpClient mySMTP = new SmtpClient();
                mySMTP.Host = txtMailServer.Text;

                if (chkDefaultCredentials.Checked)
                {
                    mySMTP.UseDefaultCredentials = true;
                }
                else
                {
                    mySMTP.UseDefaultCredentials = false;
                    NetworkCredential basecredentials = new NetworkCredential(txtUserName.Text, txtPassword.Text);
                    mySMTP.Credentials = basecredentials;
                    mySMTP.Credentials.GetCredential(txtMailServer.Text, Convert.ToInt16(txtPort.Text), "TLS");
                }

                mySMTP.Port = Convert.ToInt16(txtPort.Text);
                mySMTP.EnableSsl = true;
                mySMTP.Send(myMessage);
                ltlMessage.Text = "mail sent";


            }
            catch (SmtpException smtpex)
            {
                // A problem occurred when sending the email message
                //ClientScript.RegisterStartupScript(this.GetType(), "Boom!", String.Format("alert('There was a problem in sending the email: {0}');", ex.Message.Replace("'", "\'")), true);
                ltlMessage.Text = smtpex.Message.ToString();
            }
            catch (Exception ex)
            {
                //Some other problem occurred
                //ClientScript.RegisterStartupScript(this.GetType(), "Boom!", String.Format("alert('There was a general problem: {0}');", ex.Message.Replace("'", "\'")), true);
                ltlMessage.Text = ex.Message.ToString();
            }

        }
    }

</script>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
  
</head>
<body>
    <form id="form1" runat="server">
    <div>
                               <fieldset>
                            <legend>Legend</legend>

                <div>

                    <div>From Address:</div>
                    <div>
                        <asp:TextBox ID="txtFromAddress" runat="server" Width="208px">jonforst@cableone.net</asp:TextBox>
                    </div>
      
                    <div>From Name:</div>
                    <div>
                        <asp:TextBox ID="txtFromName" runat="server" Width="207px">no-reply</asp:TextBox>
                    </div>

                    <div>To Address:</div>
                    <div>
                        <asp:TextBox ID="txtToAddress" runat="server" Width="208px">idstates@hotmail.com</asp:TextBox>
                    </div>
            
                    <div>To Name:</div>
                    <div>
                        <asp:TextBox ID="txtToName" runat="server" Width="208px">Webmaster</asp:TextBox></div>
           
                    <div>Subject:</div>
                    <div>
                        <asp:TextBox ID="txtSubject" runat="server" Width="207px">Test Message</asp:TextBox></div>
           
                    <div>Message:</div>
                    <div>
                        <asp:TextBox ID="txtMessage" runat="server" Rows="3" TextMode="MultiLine" Width="208px">Lorem Ipsum</asp:TextBox>
                    </div>

                    <div>Mail Server:</div>
                    <div>
                        <asp:TextBox ID="txtMailServer" runat="server" Width="207px">mail.domain.com or IP address</asp:TextBox> 
                        Mail Port:<asp:TextBox ID="txtPort" runat="server" Width="50">25</asp:TextBox>
                    </div>

                    <div>Credentials: </div>
                    <div><asp:CheckBox runat="server" ID="chkDefaultCredentials" Checked="true" Text="Use Default" /> | 
                        <asp:CheckBox runat="server" ID="chkUseSsl" Checked="false" Text="Use Ssl" />
                    </div>

                    <div></div>
                    <div></div>

                    <div>Network Credentials</div>
                    <div>
                        Username: <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>  Password: <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox>
                    </div>


                    <div>
                        <asp:Button ID="btnSend" runat="server" Text="  Send  " /> | cancel</div>
          
                    <asp:Literal ID="ltlMessage" runat="server"/>      
              </div>
        


                        <%--Your custom form goes here.
                        <asp:TextBox ID="TextBox1" runat="server" CssClass="widetextbox" />
                        <portal:mojoButton ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />

                        <asp:HyperLink ID="lnkDefault" runat="server" Text="Supporting Page" 
                        Visible='<%# IsEditable %>' />--%>
    </fieldset>
        </div>
    </form>
</body>
</html>
