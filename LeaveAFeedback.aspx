<%@ Page Title="Feedbacks" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="LeaveAFeedback.aspx.cs" Inherits="DoctorAppointment.LeaveAFeedback" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container mb-4">

        <div class="row mt-5">
            <div class="mx-auto col">
                <label>Your Feedback</label>
                <div class="form-group">
                    <asp:TextBox class="form-control" placeholder="Full Name" 
                        ID="TextBox1" runat="server" TextMode="MultiLine"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Button ID="Button1" class="btn btn-primary btn-block btn-lg" runat="server" Text="Submit My Feedback" OnClick="Button1_Click"  />
                </div>
            </div>            
        </div>

        <div class="row">
            <div class="col">
                <asp:GridView AutoGenerateColumns="false" 
                    DataKeyNames="ID" class="table table-striped table-bordered" ID="GridView1" runat="server">
                    <Columns>
                        <asp:BoundField DataField="ID" HeaderText="Feedback ID" />
                        <asp:BoundField DataField="MemberEmail" HeaderText="Member Email" />
                        <asp:BoundField DataField="FeedbackContent" HeaderText="Feedback" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>

    </div>


</asp:Content>
