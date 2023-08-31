<%@ Page Title="Book an Appointment - Ease Appointments" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="BookAppointmentPage.aspx.cs" Inherits="DoctorAppointment.BookAppointmentPage" %>
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
                                    <img width="100px" src="imgs/book-online.png" />
                                </center>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col">
                                <center>
                                    <h4>
                                        Book an Appointment
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
                                <label>Clinic Location</label>
                                <div class="form-group">
                                    <asp:DropDownList class="form-control" ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <label>Booking Date</label>
                                <asp:DropDownList ID="DropDownList3" class="form-control" runat="server" >
                                </asp:DropDownList>
                            </div>
                        </div>


                        <div class="row">
                            <div class="col-md-6">
                                <label>Select Available Slot</label>
                                <div class="form-group">
                                    <asp:DropDownList class="form-control" ID="DropDownList2" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            
                            <div class="col-md-6">
                                <label>Fees</label>
                                <div class="form-group">
                                    <asp:TextBox class="form-control" placeholder="Fee" 
                                        ID="TextBox4" runat="server" Disabled="true"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col">
                                <div class="form-group">
                                    <asp:Button ID="SignUpButton" class="btn btn-info btn-block btn-lg" runat="server" Text="Book Appointment" OnClick="SignUpButton_Click" />
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
