<%@ Page Title="Password Reset" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ResetPasswordPage.aspx.cs" Inherits="DoctorAppointment.ResetPasswordPage" %>
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
                                        Password Reset
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
                            <div class="col">
                                <label>Enter your email address</label>
                                <div class="form-group">
                                    <asp:TextBox class="form-control" placeholder="Email Address" 
                                        ID="TextBox1" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>


                        <div class="row">
                            <div class="col">
                                <div class="form-group">
                                    <asp:Button ID="Button1" class="btn btn-warning btn-block btn-lg" runat="server" Text="Send Code" OnClick="Button1_Click" />
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
