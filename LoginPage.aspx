<%@ Page Title="Login - Ease Appointment" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="DoctorAppointment.LoginPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container">
        <div class="row">
            <div class="col-md-6 mx-auto">
                <div class="card">

                    <div class="card-body">

                        <div class="row">
                            <div class="col">
                                <center>
                                    <img width="150px" src="imgs/generaluser.png" />
                                </center>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col">
                                <center>
                                    <h3>
                                        Login
                                    </h3>
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
                                <label>Email Address</label>
                                <div class="form-group">
                                    <asp:TextBox class="form-control" placeholder="Email Address" 
                                        ID="TextBox1" runat="server"></asp:TextBox>
                                </div>

                                <label>Password</label>
                                <div class="form-group">
                                    <asp:TextBox class="form-control"  placeholder="Password" 
                                        ID="TextBox2" runat="server" TextMode="Password"></asp:TextBox>
                                </div>

                                <div class="form-group">
                                    <asp:Button class="btn btn-success btn-block btn-lg" ID="Button1" runat="server" Text="Login" OnClick="Button1_Click" />
                                </div>

                                <div class="form-group">
                                    <a href="SignUpPage.aspx">
                                        <input class="btn btn-info btn-block btn-lg" id="Button2" type="button" value="Sign Up" />
                                    </a>        
                                </div>

                                <div class="form-group">
                                    <a href="ResetPasswordPage.aspx">
                                        <input class="btn btn-warning btn-block btn-lg" id="Button3" type="button" value="Recover Password" />
                                    </a>        
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            
                <a href="HomePage.aspx">
                    << Back To Home
                </a>
                
                <br />
                <br />


            </div>
        </div>
    </div>

</asp:Content>
