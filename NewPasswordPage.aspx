<%@ Page Title="New Password" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="NewPasswordPage.aspx.cs" Inherits="DoctorAppointment.NewPasswordPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="container">
        <div class="row">
            <div class="col-md-8 mx-auto">
                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col">
                                <center>
                                    <h4>
                                        Update Password
                                    </h4>
                                </center>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <hr />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <label>Enter New Password</label>
                                <div class="form-group">
                                    <asp:TextBox class="form-control" Visible="false" placeholder="Enter New Password" 
                                        ID="TextBox2" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <label>Confirm New Password</label>
                                <div class="form-group">
                                    <asp:TextBox class="form-control" Visible="false" placeholder="Confirm Password" 
                                        ID="TextBox3" runat="server" ></asp:TextBox>
                                </div>
                            </div>
                        </div>


                        <div class="row">
                            <div class="col">
                                <div class="form-group">
                                    <asp:Button ID="Button2" Visible="false" class="btn btn-info btn-block btn-lg" runat="server" Text="Update Password" OnClick="Button2_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            
                <a href="HomePage.aspx">
                    <b>
                        << Back To Home
                    </b>
                </a>
                <br />
                <br />

            </div>
        </div>
    </div>

</asp:Content>
