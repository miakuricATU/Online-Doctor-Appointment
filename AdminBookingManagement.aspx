<%@ Page Title="Booking Management - Ease Appointment" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AdminBookingManagement.aspx.cs" Inherits="DoctorAppointment.AdminBookingManagement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script>
        $(document).read(function () {
            $(".table").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable();
        })
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container-fluid">
        <div class="row">
            <div class="col-md-5">
                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col">
                                <center>
                                    <img width="100px" src="imgs/my_booking.png" />
                                </center>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <center>
                                    <h4>Manage Bookings</h4>
                                </center>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <hr>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <label>Booking ID (for updating)</label>
                                <div class="form-group">
                                    <div class="input-group">
                                        <asp:TextBox class="form-control" ID="TextBox1" runat="server">
                                        </asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <label>Clinic Location</label>
                                <div class="form-group">
                                    <div class="input-group">
                                        <asp:DropDownList ID="DropDownList1" class="form-control"  runat="server" >
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <label>Booking Date</label>
                                <div class="form-group">
                                    <div class="input-group">
                                        <asp:DropDownList ID="DropDownList2" class="form-control"  runat="server" >
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <label>Appointment Slot</label>
                                <div class="form-group">
                                    <asp:DropDownList ID="DropDownList3"  class="form-control" runat="server"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <label>Member Email Address</label>
                                <div class="form-group">
                                    <asp:DropDownList ID="DropDownList4"  class="form-control" runat="server"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <label>Fee</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ReadOnly="true" ID="TextBox5" runat="server" placeholder="Start Date">
                                    </asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-6">
                                <asp:Button ID="Button2" class="btn btn-lg btn-block btn-success" runat="server" Text="Add" OnClick="Button2_Click"  />
                            </div>
                            <div class="col-6">
                                <asp:Button ID="Button3" class="btn btn-lg btn-block btn-warning" runat="server" Text="Update" OnClick="Button3_Click"  />
                            </div>
                        </div>
                    </div>
                </div>
                <a href="homepage.aspx"><< Back to Home</a>
                <br>
                <br>
            </div>
            <div class="col-md-7">
                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col">
                                <center>
                                    <h4>All Bookings</h4>
                                </center>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <hr>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <asp:GridView AutoGenerateColumns="false" OnRowDeleting="GridView1_RowDeleting"
                                    DataKeyNames="ID" class="table table-striped table-bordered" ID="GridView1" runat="server" >
                                    <Columns>
                                        <asp:BoundField DataField="ID" HeaderText="Booking ID" />
                                        <asp:BoundField DataField="BookingDateTime" HeaderText="Date" />
                                        <asp:BoundField DataField="MemberEmailAddress" HeaderText="Email Address" />
                                        <asp:BoundField DataField="ClinicLocationID" HeaderText="Clinic ID" />
                                        <asp:BoundField DataField="BookingFee" HeaderText="Fee" />
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:Button  CommandName="Delete" CssClass="btn btn-danger" runat="server" Text="Delete" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


</asp:Content>
